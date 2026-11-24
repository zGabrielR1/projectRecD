// Dump compiler-generated nested types (closures, caches, state machines) from a
// reconstructed binding assembly so their C# declarations can be synthesized.
// Usage: NestedClosureDump <assembly> [--all]
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Mono.Cecil;

static class NestedClosureDump
{
    static readonly Dictionary<string, string> Keywords = new()
    {
        ["System.Boolean"] = "bool", ["System.Byte"] = "byte", ["System.SByte"] = "sbyte",
        ["System.Char"] = "char", ["System.Decimal"] = "decimal", ["System.Double"] = "double",
        ["System.Single"] = "float", ["System.Int32"] = "int", ["System.UInt32"] = "uint",
        ["System.Int64"] = "long", ["System.UInt64"] = "ulong", ["System.Int16"] = "short",
        ["System.UInt16"] = "ushort", ["System.Object"] = "object", ["System.String"] = "string",
        ["System.Void"] = "void"
    };

    static string Nice(TypeReference type)
    {
        if (type is GenericParameter parameter)
            return parameter.Name;
        if (type is GenericInstanceType generic)
        {
            var name = Nice(generic.ElementType);
            var args = new List<string>();
            foreach (var argument in generic.GenericArguments)
                args.Add(Nice(argument));
            return name + "<" + string.Join(", ", args) + ">";
        }
        if (type is ArrayType array)
            return Nice(array.ElementType) + "[" + new string(',', array.Rank - 1) + "]";
        if (type is ByReferenceType byReference)
            return Nice(byReference.ElementType);
        if (type is PointerType pointer)
            return Nice(pointer.ElementType) + "*";
        if (type is OptionalModifierType optional)
            return Nice(optional.ElementType);
        if (type is RequiredModifierType required)
            return Nice(required.ElementType);
        if (Keywords.TryGetValue(type.FullName, out var keyword))
            return keyword;
        var text = type.FullName.Replace('/', '.').Replace('+', '.');
        if (type.IsNested && type.DeclaringType != null)
            text = Nice(type.DeclaringType) + "." + type.Name;
        text = System.Text.RegularExpressions.Regex.Replace(text, "`\\d+", "");
        return text;
    }

    static bool Generated(string name) =>
        name.Contains("DisplayClass") || name == "<>c" || name.Contains("f__AnonymousType")
        || name.Contains("t__") || name.Contains("__wrap") || name == "<PrivateImplementationDetails>";

    static IEnumerable<TypeDefinition> Walk(TypeDefinition type)
    {
        foreach (var nested in type.NestedTypes)
        {
            yield return nested;
            foreach (var deeper in Walk(nested))
                yield return deeper;
        }
    }

    static void Main(string[] args)
    {
        using var module = ModuleDefinition.ReadModule(args[0]);
        var records = new List<object>();
        foreach (var type in module.Types)
        {
            foreach (var nested in Walk(type))
            {
                if (!Generated(nested.Name)) continue;
                var fields = new List<object>();
                foreach (var field in nested.Fields)
                    fields.Add(new { name = field.Name, type = Nice(field.FieldType), isStatic = field.IsStatic });
                records.Add(new
                {
                    declaring = nested.DeclaringType.FullName.Replace('/', '.'),
                    name = nested.Name,
                    isStatic = nested.IsAbstract && nested.IsSealed,
                    baseType = nested.BaseType == null ? null : Nice(nested.BaseType),
                    fields
                });
            }
        }
        Console.WriteLine(JsonSerializer.Serialize(new { assembly = Path.GetFileName(args[0]), types = records },
            new JsonSerializerOptions { WriteIndented = true }));
    }
}
