.assembly extern mscorlib
{
	.publickeytoken = (
		b7 7a 5c 56 19 34 e0 89
	)
	.ver 4:0:0:0
}
.assembly extern MongoDB.Bson
{
	.ver 2:4:4:0
}
.assembly extern mscorlib
{
	.publickeytoken = (
		b7 7a 5c 56 19 34 e0 89
	)
	.ver 65535:65535:65535:65535
}
.assembly extern DigisatServer.Infrastructure.Data.Core
{
	.ver 1:0:0:0
}
.assembly extern DigisatServer.Infrastructure.Crosscutting
{
	.ver 1:1:1:1
}
.assembly extern System.Core
{
	.publickeytoken = (
		b7 7a 5c 56 19 34 e0 89
	)
	.ver 4:0:0:0
}
.assembly DigisatServer.Domain.Core
{
	.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = (
		01 00 0b 32 2e 30 2e 32 36 33 30 37 2e 34 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = (
		01 00 01 00 54 02 16 57 72 61 70 4e 6f 6e 45 78
		63 65 70 74 69 6f 6e 54 68 72 6f 77 73 01
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = (
		01 00 08 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyKeyNameAttribute::.ctor(string) = (
		01 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyDelaySignAttribute::.ctor(bool) = (
		01 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = (
		01 00 1a 2e 4e 45 54 46 72 61 6d 65 77 6f 72 6b
		2c 56 65 72 73 69 6f 6e 3d 76 34 2e 36 01 00 54
		0e 14 46 72 61 6d 65 77 6f 72 6b 44 69 73 70 6c
		61 79 4e 61 6d 65 12 2e 4e 45 54 20 46 72 61 6d
		65 77 6f 72 6b 20 34 2e 36
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.SuppressIldasmAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = (
		01 00 02 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyTrademarkAttribute::.ctor(string) = (
		01 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = (
		01 00 12 43 6f 70 79 72 69 67 68 74 20 c2 a9 20
		20 32 30 31 35 00 00
	)
	.custom instance void [mscorlib]System.Runtime.InteropServices.GuidAttribute::.ctor(string) = (
		01 00 24 32 36 63 64 38 62 38 39 2d 64 66 32 34
		2d 34 30 38 66 2d 38 63 30 39 2d 30 35 31 32 39
		32 32 65 30 35 31 31 00 00
	)
	.custom instance void [mscorlib]System.Runtime.InteropServices.ComVisibleAttribute::.ctor(bool) = (
		01 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = (
		01 00 19 44 69 67 69 73 61 74 53 65 72 76 65 72
		2e 44 6f 6d 61 69 6e 2e 43 6f 72 65 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = (
		01 00 00 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = (
		01 00 19 44 69 67 69 73 61 74 53 65 72 76 65 72
		2e 44 6f 6d 61 69 6e 2e 43 6f 72 65 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = (
		01 00 17 44 69 67 69 73 61 74 20 54 65 63 6e 6f
		6c 6f 67 69 61 20 4c 74 64 61 00 00
	)
	.custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = (
		01 00 00 00 00
	)
	.hash algorithm 0x00008004 // SHA1
	.ver 1:0:0:0
}
.module DigisatServer.Domain.Core.dll
// MVID: {4CDD4D74-F808-4419-8645-E7924228ECB7}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003 // WindowsCui
.corflags 0x00000001 // ILOnly
.class /* 02000001 */ private auto ansi '<Module>'
{
} // end of class <Module>

.class /* 02000002 */ private auto ansi abstract sealed beforefieldinit jmvaIPxi7ar7Sl8uOk.CkWRbk2EftNA16p4Ja
	extends [mscorlib]System.Object
{
} // end of class jmvaIPxi7ar7Sl8uOk.CkWRbk2EftNA16p4Ja

.class /* 02000003 */ public auto ansi abstract beforefieldinit DigisatServer.Domain.Core.Entity
	extends DigisatServer.Domain.Core.EntityVersao
	implements [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao,
	           class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityBase
{
	// Fields
	.field /* 04000001 */ private bool A14HXcQSo
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method /* 06000001 */ family hidebysig specialname rtspecialname 
		instance void .ctor (
			[opt] bool ativo,
			[opt] valuetype [mscorlib]System.TimeSpan versao,
			[opt] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		.param [1] = bool(true)
		.param [2] = nullref
		.param [3] = nullref
		// Method begins at RVA 0x2050
		// Header size: 12
		// Code size: 155 (0x9b)
		.maxstack 4
		.locals /* 11000001 */ init (
			[0] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x51
		           .emitbyte 0xAC
		           .emitbyte 0x26
		           .emitbyte 0x3A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4 3
		IL_0013: br IL_003b
		IL_0018: ldarg 0
		IL_001c: ldarg versao
		IL_0020: ldarg id
		IL_0024: call instance void DigisatServer.Domain.Core.EntityVersao::.ctor(valuetype [mscorlib]System.TimeSpan, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600007C */
		IL_0029: br IL_0058
		IL_002e: ldc.i4 3
		IL_0033: stloc 0
		IL_0037: ldloc 0
		IL_003b: switch (IL_0018, IL_0018, IL_0058, IL_007f, IL_0068, IL_009a)
		IL_0058: br IL_0068
		IL_005d: pop
		IL_005e: ldc.i4 4
		IL_0063: br IL_003b
		IL_0068: ldarg 0
		IL_006c: ldarg ativo
		IL_0070: call void DigisatServer.Domain.Core.Entity::ECS7XKv7e8vY1G82cd(object, bool) /* 06000008 */
		IL_0075: ldc.i4 5
		IL_007a: br IL_003b
		IL_007f: call void DigisatServer.Domain.Core.Entity::V6CCH9rWyJyY4EDByV() /* 06000007 */
		IL_0084: ldc.i4.1
		IL_0085: call bool DigisatServer.Domain.Core.Entity::DsmTNaeVXPvdLhnMrb() /* 0600000A */
		IL_008a: brtrue IL_005d
		IL_008f: pop
		IL_0090: ldc.i4 0
		IL_0095: br IL_003b
		IL_009a: ret
	} // end of method Entity::.ctor

	.method /* 06000002 */ public hidebysig specialname newslot virtual 
		instance bool get_Ativo () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x20f8
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4B
		           .emitbyte 0x68
		           .emitbyte 0x38
		           .emitbyte 0x48
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld bool DigisatServer.Domain.Core.Entity::A14HXcQSo /* 04000001 */
		IL_0017: ret
	} // end of method Entity::get_Ativo

	.method /* 06000003 */ family hidebysig specialname newslot virtual 
		instance void set_Ativo (
			bool 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x2114
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xB8
		           .emitbyte 0xC3
		           .emitbyte 0x47
		           .emitbyte 0x45
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld bool DigisatServer.Domain.Core.Entity::A14HXcQSo /* 04000001 */
		IL_001b: ret
	} // end of method Entity::set_Ativo

	.method /* 06000004 */ public hidebysig newslot virtual 
		instance void Ativar () cil managed noinlining 
	{
		// Method begins at RVA 0x2134
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x2A
		           .emitbyte 0xB0
		           .emitbyte 0x10
		           .emitbyte 0x64
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldc.i4 1
		IL_0017: call void DigisatServer.Domain.Core.Entity::ECS7XKv7e8vY1G82cd(object, bool) /* 06000008 */
		IL_001c: ret
	} // end of method Entity::Ativar

	.method /* 06000005 */ public hidebysig newslot virtual 
		instance void Desativar () cil managed noinlining 
	{
		// Method begins at RVA 0x2154
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x8F
		           .emitbyte 0x2C
		           .emitbyte 0x1E
		           .emitbyte 0x50
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldc.i4 0
		IL_0017: call void DigisatServer.Domain.Core.Entity::ECS7XKv7e8vY1G82cd(object, bool) /* 06000008 */
		IL_001c: ret
	} // end of method Entity::Desativar

	.method /* 06000006 */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x2174
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x91
		           .emitbyte 0xB0
		           .emitbyte 0x19
		           .emitbyte 0x4F
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void DigisatServer.Domain.Core.Entity::V6CCH9rWyJyY4EDByV() /* 06000007 */
		IL_0013: ldarg 0
		IL_0017: call void DigisatServer.Domain.Core.Entity::TDmEOTW6PcWLCQUei4(object) /* 0600000B */
		IL_001c: ret
	} // end of method Entity::.ctor

	.method /* 06000007 */ assembly hidebysig static 
		void V6CCH9rWyJyY4EDByV () cil managed noinlining 
	{
		// Method begins at RVA 0x2194
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x78
		           .emitbyte 0xE8
		           .emitbyte 0x32
		           .emitbyte 0x52
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0014: ret
	} // end of method Entity::V6CCH9rWyJyY4EDByV

	.method /* 06000008 */ assembly hidebysig static 
		void ECS7XKv7e8vY1G82cd (
			object '',
			bool 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x21ac
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x6C
		           .emitbyte 0x9B
		           .emitbyte 0x09
		           .emitbyte 0x63
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: callvirt instance void DigisatServer.Domain.Core.Entity::set_Ativo(bool) /* 06000003 */
		IL_001b: ret
	} // end of method Entity::ECS7XKv7e8vY1G82cd

	.method /* 06000009 */ assembly hidebysig static 
		bool caQaku3NhlCHmNlRXD () cil managed noinlining 
	{
		// Method begins at RVA 0x21cc
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA8
		           .emitbyte 0x6D
		           .emitbyte 0x1E
		           .emitbyte 0x66
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method Entity::caQaku3NhlCHmNlRXD

	.method /* 0600000A */ assembly hidebysig static 
		bool DsmTNaeVXPvdLhnMrb () cil managed noinlining 
	{
		// Method begins at RVA 0x21e0
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA3
		           .emitbyte 0xF6
		           .emitbyte 0x0A
		           .emitbyte 0x5A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method Entity::DsmTNaeVXPvdLhnMrb

	.method /* 0600000B */ assembly hidebysig static 
		void TDmEOTW6PcWLCQUei4 (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x21f4
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x08
		           .emitbyte 0xF8
		           .emitbyte 0x1B
		           .emitbyte 0x62
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance void DigisatServer.Domain.Core.EntityVersao::.ctor() /* 0A000013 */
		IL_0017: ret
	} // end of method Entity::TDmEOTW6PcWLCQUei4

	// Properties
	.property /* 17000001 */ instance bool Ativo()
	{
		.get instance bool DigisatServer.Domain.Core.Entity::get_Ativo()
		.set instance void DigisatServer.Domain.Core.Entity::set_Ativo(bool)
	}

} // end of class DigisatServer.Domain.Core.Entity

.class /* 02000004 */ public auto ansi abstract beforefieldinit DigisatServer.Domain.Core.EntityBase
	extends [mscorlib]System.Object
	implements class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityBase
{
	// Nested Types
	.class /* 02000005 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass11_0'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000004 */ public string informacaoTratada

		// Methods
		.method /* 06000027 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x28f0
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x41
			           .emitbyte 0x84
			           .emitbyte 0x74
			           .emitbyte 0x5A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::uuB69hjaxsig6rNVpe() /* 06000029 */
			IL_0013: ldarg 0
			IL_0017: call void DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::kXo16Sybabd1Ug65ti(object) /* 0600002A */
			IL_001c: ret
		} // end of method '<>c__DisplayClass11_0'::.ctor

		.method /* 06000028 */ assembly hidebysig 
			instance bool '<AdicionarInformacaoPesquisa>b__0' (
				string x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x2910
			// Header size: 1
			// Code size: 33 (0x21)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xD3
			           .emitbyte 0x13
			           .emitbyte 0x05
			           .emitbyte 0x61
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: ldarg 0
			IL_0016: ldfld string DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::informacaoTratada /* 04000004 */
			IL_001b: call bool DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::cpKQHUADPR9PIj0Z00(object, object) /* 0600002D */
			IL_0020: ret
		} // end of method '<>c__DisplayClass11_0'::'<AdicionarInformacaoPesquisa>b__0'

		.method /* 06000029 */ assembly hidebysig static 
			void uuB69hjaxsig6rNVpe () cil managed noinlining 
		{
			// Method begins at RVA 0x2934
			// Header size: 1
			// Code size: 21 (0x15)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x61
			           .emitbyte 0xEA
			           .emitbyte 0x4E
			           .emitbyte 0x3F
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: nop
			IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0014: ret
		} // end of method '<>c__DisplayClass11_0'::uuB69hjaxsig6rNVpe

		.method /* 0600002A */ assembly hidebysig static 
			void kXo16Sybabd1Ug65ti (
				object ''
			) cil managed noinlining 
		{
			// Method begins at RVA 0x294c
			// Header size: 1
			// Code size: 24 (0x18)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xCE
			           .emitbyte 0x55
			           .emitbyte 0x16
			           .emitbyte 0x34
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg 0
			IL_0012: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_0017: ret
		} // end of method '<>c__DisplayClass11_0'::kXo16Sybabd1Ug65ti

		.method /* 0600002B */ assembly hidebysig static 
			bool Q8VdJMxSxPGVL5lfuK () cil managed noinlining 
		{
			// Method begins at RVA 0x2968
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x9B
			           .emitbyte 0xCB
			           .emitbyte 0x50
			           .emitbyte 0x55
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass11_0'::Q8VdJMxSxPGVL5lfuK

		.method /* 0600002C */ assembly hidebysig static 
			bool BaPabwcqZOua1hWXUo () cil managed noinlining 
		{
			// Method begins at RVA 0x297c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x61
			           .emitbyte 0x09
			           .emitbyte 0x69
			           .emitbyte 0x57
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass11_0'::BaPabwcqZOua1hWXUo

		.method /* 0600002D */ assembly hidebysig static 
			bool cpKQHUADPR9PIj0Z00 (
				object '',
				object ''
			) cil managed noinlining 
		{
			// Method begins at RVA 0x2990
			// Header size: 1
			// Code size: 28 (0x1c)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xD7
			           .emitbyte 0x01
			           .emitbyte 0x3B
			           .emitbyte 0x4A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg 0
			IL_0012: ldarg 1
			IL_0016: callvirt instance bool [mscorlib]System.String::Equals(string) /* 0A00002A */
			IL_001b: ret
		} // end of method '<>c__DisplayClass11_0'::cpKQHUADPR9PIj0Z00

	} // end of class <>c__DisplayClass11_0


	// Fields
	.field /* 04000002 */ private class [mscorlib]System.Collections.Generic.IList`1<string> Wc82gHTVB
	.field /* 04000003 */ private valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId hdkxWRbkE
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method /* 0600000C */ family hidebysig specialname rtspecialname 
		instance void .ctor (
			[opt] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		.param [1] = nullref
		// Method begins at RVA 0x2210
		// Header size: 12
		// Code size: 185 (0xb9)
		.maxstack 3
		.locals /* 11000001 */ init (
			[0] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x48
		           .emitbyte 0x0C
		           .emitbyte 0x34
		           .emitbyte 0x40
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: call bool DigisatServer.Domain.Core.EntityBase::pU2vkIH6RKpZTn9V4K() /* 0600001E */
		IL_0014: brtrue IL_002e
		IL_0019: pop
		IL_001a: ldc.i4 4
		IL_001f: br IL_006c
		IL_0024: call void DigisatServer.Domain.Core.EntityBase::O4K2lCO2sHFT2OYwfN() /* 06000018 */
		IL_0029: br IL_004c
		IL_002e: pop
		IL_002f: ldc.i4 3
		IL_0034: br IL_006c
		IL_0039: ldarg 0
		IL_003d: call void DigisatServer.Domain.Core.EntityBase::u5adlrnPhJT05x1B9r(object) /* 06000019 */
		IL_0042: ldc.i4 5
		IL_0047: br IL_006c
		IL_004c: ldarg 0
		IL_0050: newobj instance void class [mscorlib]System.Collections.Generic.List`1<string>::.ctor() /* 0A000014 */
		IL_0055: stfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_005a: br IL_0039
		IL_005f: ldc.i4 4
		IL_0064: stloc 0
		IL_0068: ldloc 0
		IL_006c: switch (IL_0024, IL_0029, IL_0039, IL_004c, IL_0024, IL_0093)
		IL_0089: ldc.i4 2
		IL_008e: br IL_006c
		IL_0093: ldarg 0
		IL_0097: ldarg id
		IL_009b: call bool DigisatServer.Domain.Core.EntityBase::F5Gcgngd6QxoOnjOJ4(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600001A */
		IL_00a0: brtrue IL_00ae
		IL_00a5: ldarg id
		IL_00a9: br IL_00b3
		IL_00ae: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::akPSbgMb3fqMQvmI94() /* 0600001B */
		IL_00b3: call void DigisatServer.Domain.Core.EntityBase::qANLLJuo8dxWgeAoyK(object, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600001C */
		IL_00b8: ret
	} // end of method EntityBase::.ctor

	.method /* 0600000D */ public hidebysig specialname newslot virtual 
		instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId get_Id () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x22d8
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x16
		           .emitbyte 0xC7
		           .emitbyte 0x6B
		           .emitbyte 0x6A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::hdkxWRbkE /* 04000003 */
		IL_0017: ret
	} // end of method EntityBase::get_Id

	.method /* 0600000E */ family hidebysig specialname newslot virtual 
		instance void set_Id (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x22f4
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xB7
		           .emitbyte 0xC1
		           .emitbyte 0x30
		           .emitbyte 0x4D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::hdkxWRbkE /* 04000003 */
		IL_001b: ret
	} // end of method EntityBase::set_Id

	.method /* 0600000F */ public hidebysig specialname newslot virtual 
		instance valuetype [mscorlib]System.DateTime get_DataInclusao () cil managed noinlining 
	{
		// Method begins at RVA 0x2314
		// Header size: 12
		// Code size: 47 (0x2f)
		.maxstack 3
		.locals /* 11000002 */ init (
			[0] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xF8
		           .emitbyte 0x2E
		           .emitbyte 0x13
		           .emitbyte 0x4E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::LWVjphKG65SQu5bTGl(object) /* 0600001F */
		IL_0017: stloc 0
		IL_001b: ldloca 0
		IL_001f: call instance valuetype [mscorlib]System.DateTime [MongoDB.Bson]MongoDB.Bson.ObjectId::get_CreationTime() /* 0A000015 */
		IL_0024: call object DigisatServer.Domain.Core.EntityBase::ARfy3l9WACCTRq2Qhj() /* 06000020 */
		IL_0029: call valuetype [mscorlib]System.DateTime DigisatServer.Domain.Core.EntityBase::W8iaDTtxOZraHXCROG(valuetype [mscorlib]System.DateTime, object) /* 06000021 */
		IL_002e: ret
	} // end of method EntityBase::get_DataInclusao

	.method /* 06000010 */ public hidebysig specialname newslot virtual 
		instance class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string> get_InformacoesPesquisa () cil managed noinlining 
	{
		// Method begins at RVA 0x2350
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x5B
		           .emitbyte 0x00
		           .emitbyte 0x1E
		           .emitbyte 0x3F
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_0017: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000001 */
		IL_001c: ret
	} // end of method EntityBase::get_InformacoesPesquisa

	.method /* 06000011 */ family hidebysig specialname newslot virtual 
		instance void set_InformacoesPesquisa (
			class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string> 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2370
		// Header size: 1
		// Code size: 48 (0x30)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x33
		           .emitbyte 0xDC
		           .emitbyte 0x3F
		           .emitbyte 0x5C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 'value'
		IL_0012: call bool [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperObject::IsNull(object) /* 0A000017 */
		IL_0017: brfalse IL_001d
		IL_001c: ret
		IL_001d: ldarg 0
		IL_0021: ldarg 'value'
		IL_0025: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000001 */
		IL_002a: stfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_002f: ret
	} // end of method EntityBase::set_InformacoesPesquisa

	.method /* 06000012 */ family hidebysig newslot virtual 
		instance void AdicionarInformacaoPesquisa (
			string informacao
		) cil managed noinlining 
	{
		// Method begins at RVA 0x23a4
		// Header size: 12
		// Code size: 308 (0x134)
		.maxstack 4
		.locals /* 11000003 */ init (
			[0] class DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0',
			[1] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA0
		           .emitbyte 0x55
		           .emitbyte 0x10
		           .emitbyte 0x34
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4 6
		IL_0013: br IL_0100
		IL_0018: newobj instance void DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::.ctor() /* 06000027 */
		IL_001d: stloc 0
		IL_0021: call bool DigisatServer.Domain.Core.EntityBase::hKI0uGhjhZaSnUMGaR() /* 0600001D */
		IL_0026: call bool DigisatServer.Domain.Core.EntityBase::pU2vkIH6RKpZTn9V4K() /* 0600001E */
		IL_002b: brtrue IL_004e
		IL_0030: pop
		IL_0031: ldc.i4 4
		IL_0036: br IL_0100
		IL_003b: ldarg informacao
		IL_003f: call bool DigisatServer.Domain.Core.EntityBase::mfCleRFrcYlfOZLXfS(object) /* 06000022 */
		IL_0044: brfalse IL_00a5
		IL_0049: br IL_00a4
		IL_004e: pop
		IL_004f: ldc.i4 0
		IL_0054: call bool DigisatServer.Domain.Core.EntityBase::pU2vkIH6RKpZTn9V4K() /* 0600001E */
		IL_0059: brtrue IL_0100
		IL_005e: br IL_0100
		IL_0063: ret
		IL_0064: ldloc 0
		IL_0068: ldfld string DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::informacaoTratada /* 04000004 */
		IL_006d: call bool DigisatServer.Domain.Core.EntityBase::mfCleRFrcYlfOZLXfS(object) /* 06000022 */
		IL_0072: brtrue IL_0133
		IL_0077: ldc.i4 3
		IL_007c: ldc.i4.0
		IL_007d: brfalse IL_0100
		IL_0082: pop
		IL_0083: ldarg 0
		IL_0087: ldfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_008c: ldloc 0
		IL_0090: ldfld string DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::informacaoTratada /* 04000004 */
		IL_0095: callvirt instance void class [mscorlib]System.Collections.Generic.ICollection`1<string>::Add(!0) /* 0A000018 */
		IL_009a: ldc.i4 8
		IL_009f: br IL_0100
		IL_00a4: ret
		IL_00a5: ldloc 0
		IL_00a9: ldarg informacao
		IL_00ad: call object DigisatServer.Domain.Core.EntityBase::uvqDC3q6vpZbRLksOD(object) /* 06000023 */
		IL_00b2: call object DigisatServer.Domain.Core.EntityBase::Fs7mGDbx8DN38PjTCj(object) /* 06000024 */
		IL_00b7: stfld string DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::informacaoTratada /* 04000004 */
		IL_00bc: ldc.i4 5
		IL_00c1: call bool DigisatServer.Domain.Core.EntityBase::hKI0uGhjhZaSnUMGaR() /* 0600001D */
		IL_00c6: brtrue IL_0100
		IL_00cb: pop
		IL_00cc: ldarg 0
		IL_00d0: ldfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_00d5: ldloc 0
		IL_00d9: ldftn instance bool DigisatServer.Domain.Core.EntityBase/'<>c__DisplayClass11_0'::'<AdicionarInformacaoPesquisa>b__0'(string) /* 06000028 */
		IL_00df: newobj instance void class [mscorlib]System.Func`2<string, bool>::.ctor(object, native int) /* 0A000019 */
		IL_00e4: call bool [System.Core]System.Linq.Enumerable::Any<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, bool>) /* 2B000002 */
		IL_00e9: brfalse IL_0064
		IL_00ee: br IL_0063
		IL_00f3: ldc.i4 6
		IL_00f8: stloc 1
		IL_00fc: ldloc 1
		IL_0100: switch (IL_00a4, IL_003b, IL_0049, IL_0083, IL_003b, IL_00cc, IL_0018, IL_0063, IL_0133)
		IL_0129: ldc.i4 7
		IL_012e: br IL_0100
		IL_0133: ret
	} // end of method EntityBase::AdicionarInformacaoPesquisa

	.method /* 06000013 */ family hidebysig newslot virtual 
		instance void AdicionarInformacaoPesquisa (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<string> informacoes
		) cil managed noinlining 
	{
		// Method begins at RVA 0x24e4
		// Header size: 12
		// Code size: 122 (0x7a)
		.maxstack 3
		.locals /* 11000004 */ init (
			[0] class [mscorlib]System.Collections.Generic.List`1<string>,
			[1] valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>,
			[2] string
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x7D
		           .emitbyte 0x47
		           .emitbyte 0x0F
		           .emitbyte 0x57
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg informacoes
		IL_0012: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000001 */
		IL_0017: stloc 0
		IL_001b: ldloc 0
		IL_001f: call bool [System.Core]System.Linq.Enumerable::Any<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000003 */
		IL_0024: brtrue IL_002a
		IL_0029: ret
		IL_002a: ldloc 0
		IL_002e: callvirt instance valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<!0> class [mscorlib]System.Collections.Generic.List`1<string>::GetEnumerator() /* 0A00001C */
		IL_0033: stloc 1

		.try {
		IL_0037: br IL_0056
		IL_003c: ldloca 1
		IL_0040: call instance !0 valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>::get_Current() /* 0A00001D */
		IL_0045: stloc 2
		IL_0049: ldarg 0
		IL_004d: ldloc 2
		IL_0051: callvirt instance void DigisatServer.Domain.Core.EntityBase::AdicionarInformacaoPesquisa(string) /* 06000012 */
		IL_0056: ldloca 1
		IL_005a: call instance bool valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>::MoveNext() /* 0A00001E */
		IL_005f: brtrue IL_003c
		IL_0064: leave IL_0079

		} finally {
		IL_0069: ldloca 1
		IL_006d: constrained. valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string> /* 1B000005 */
		IL_0073: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_0078: endfinally

		}
		IL_0079: ret

	} // end of method EntityBase::AdicionarInformacaoPesquisa

	.method /* 06000014 */ family hidebysig newslot virtual 
		instance void RemoverInformacaoPesquisa (
			string[] informacoes
		) cil managed noinlining 
	{
		.param [1]
			.custom instance void [mscorlib]System.ParamArrayAttribute::.ctor() = (
				01 00 00 00
			)
		// Method begins at RVA 0x257c
		// Header size: 12
		// Code size: 354 (0x162)
		.maxstack 3
		.locals /* 11000005 */ init (
			[0] class [mscorlib]System.Collections.Generic.List`1<string>,
			[1] valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>,
			[2] string,
			[3] int32,
			[4] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x63
		           .emitbyte 0xAD
		           .emitbyte 0x21
		           .emitbyte 0x3C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4 4
		IL_0013: br IL_0075
		IL_0018: ldarg informacoes
		IL_001c: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000001 */
		IL_0021: stloc 0
		IL_0025: ldc.i4.0
		IL_0026: call bool DigisatServer.Domain.Core.EntityBase::hKI0uGhjhZaSnUMGaR() /* 0600001D */
		IL_002b: brfalse IL_0097
		IL_0030: pop
		IL_0031: ldc.i4 3
		IL_0036: ldc.i4.0
		IL_0037: brfalse IL_0075
		IL_003c: pop
		IL_003d: ret
		IL_003e: ldloc 0
		IL_0042: callvirt instance valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<!0> class [mscorlib]System.Collections.Generic.List`1<string>::GetEnumerator() /* 0A00001C */
		IL_0047: stloc 1
		IL_004b: ldc.i4 5
		IL_0050: br IL_0075
		IL_0055: ldloc 0
		IL_0059: call bool [System.Core]System.Linq.Enumerable::Any<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000003 */
		IL_005e: brtrue IL_003e
		IL_0063: br IL_0092
		IL_0068: ldc.i4 4
		IL_006d: stloc 4
		IL_0071: ldloc 4
		IL_0075: switch (IL_003d, IL_0055, IL_0092, IL_0055, IL_0018, IL_00a2)
		IL_0092: br IL_003d
		IL_0097: pop
		IL_0098: ldc.i4 0
		IL_009d: br IL_0075

		.try {
		IL_00a2: br IL_013e
		IL_00a7: ldloca 1
		IL_00ab: call instance !0 valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>::get_Current() /* 0A00001D */
		IL_00b0: stloc 2
		IL_00b4: call bool DigisatServer.Domain.Core.EntityBase::hKI0uGhjhZaSnUMGaR() /* 0600001D */
		IL_00b9: call bool DigisatServer.Domain.Core.EntityBase::pU2vkIH6RKpZTn9V4K() /* 0600001E */
		IL_00be: brtrue IL_010c
		IL_00c3: pop
		IL_00c4: ldc.i4 3
		IL_00c9: br IL_00ee
		IL_00ce: ldloc 2
		IL_00d2: call bool DigisatServer.Domain.Core.EntityBase::w8uZOrLEIZyF5Qj5SJ(object) /* 06000025 */
		IL_00d7: brtrue IL_013e
		IL_00dc: br IL_0107
		IL_00e1: ldc.i4 3
		IL_00e6: stloc 3
		IL_00ea: ldloc 3
		IL_00ee: switch (IL_00ce, IL_0107, IL_0117, IL_00ce, IL_013e)
		IL_0107: br IL_0117
		IL_010c: pop
		IL_010d: ldc.i4 2
		IL_0112: br IL_00ee
		IL_0117: ldarg 0
		IL_011b: ldfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_0120: ldloc 2
		IL_0124: call object DigisatServer.Domain.Core.EntityBase::uvqDC3q6vpZbRLksOD(object) /* 06000023 */
		IL_0129: call object DigisatServer.Domain.Core.EntityBase::Fs7mGDbx8DN38PjTCj(object) /* 06000024 */
		IL_012e: callvirt instance bool class [mscorlib]System.Collections.Generic.ICollection`1<string>::Remove(!0) /* 0A000020 */
		IL_0133: pop
		IL_0134: ldc.i4 4
		IL_0139: br IL_00ee
		IL_013e: ldloca 1
		IL_0142: call instance bool valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string>::MoveNext() /* 0A00001E */
		IL_0147: brtrue IL_00a7
		IL_014c: leave IL_0161

		} finally {
		IL_0151: ldloca 1
		IL_0155: constrained. valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<string> /* 1B000005 */
		IL_015b: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_0160: endfinally

		}
		IL_0161: ret

	} // end of method EntityBase::RemoverInformacaoPesquisa

	.method /* 06000015 */ family hidebysig newslot virtual 
		instance void LimparInformacoesPesquisa () cil managed noinlining 
	{
		// Method begins at RVA 0x26fc
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD7
		           .emitbyte 0x53
		           .emitbyte 0x69
		           .emitbyte 0x5D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: newobj instance void class [mscorlib]System.Collections.Generic.List`1<string>::.ctor() /* 0A000014 */
		IL_0017: stfld class [mscorlib]System.Collections.Generic.IList`1<string> DigisatServer.Domain.Core.EntityBase::Wc82gHTVB /* 04000002 */
		IL_001c: ret
	} // end of method EntityBase::LimparInformacoesPesquisa

	.method /* 06000016 */ public hidebysig newslot virtual 
		instance void Visit (
			class [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Interfaces.IVisitor visitor
		) cil managed noinlining 
	{
		// Method begins at RVA 0x271c
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x81
		           .emitbyte 0x70
		           .emitbyte 0x23
		           .emitbyte 0x63
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg visitor
		IL_0012: ldarg 0
		IL_0016: call void DigisatServer.Domain.Core.EntityBase::zgO4XNm88EwWdcF0wc(object, object) /* 06000026 */
		IL_001b: ret
	} // end of method EntityBase::Visit

	.method /* 06000017 */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x273c
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x33
		           .emitbyte 0x5C
		           .emitbyte 0x01
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void DigisatServer.Domain.Core.EntityBase::O4K2lCO2sHFT2OYwfN() /* 06000018 */
		IL_0013: ldarg 0
		IL_0017: call void DigisatServer.Domain.Core.EntityBase::u5adlrnPhJT05x1B9r(object) /* 06000019 */
		IL_001c: ret
	} // end of method EntityBase::.ctor

	.method /* 06000018 */ assembly hidebysig static 
		void O4K2lCO2sHFT2OYwfN () cil managed noinlining 
	{
		// Method begins at RVA 0x275c
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xDB
		           .emitbyte 0x9E
		           .emitbyte 0x63
		           .emitbyte 0x50
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0014: ret
	} // end of method EntityBase::O4K2lCO2sHFT2OYwfN

	.method /* 06000019 */ assembly hidebysig static 
		void u5adlrnPhJT05x1B9r (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2774
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x2B
		           .emitbyte 0x75
		           .emitbyte 0x17
		           .emitbyte 0x3A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_0017: ret
	} // end of method EntityBase::u5adlrnPhJT05x1B9r

	.method /* 0600001A */ assembly hidebysig static 
		bool F5Gcgngd6QxoOnjOJ4 (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId objectId
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2790
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD9
		           .emitbyte 0xBA
		           .emitbyte 0x5A
		           .emitbyte 0x56
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg objectId
		IL_0013: call bool DigisatServer.Domain.Core.HelperObjectId::IsDefaultOrEmpty(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 06000097 */
		IL_0018: ret
	} // end of method EntityBase::F5Gcgngd6QxoOnjOJ4

	.method /* 0600001B */ assembly hidebysig static 
		valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId akPSbgMb3fqMQvmI94 () cil managed noinlining 
	{
		// Method begins at RVA 0x27ac
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xF7
		           .emitbyte 0x10
		           .emitbyte 0x4F
		           .emitbyte 0x67
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::GenerateNewId() /* 0A000022 */
		IL_0014: ret
	} // end of method EntityBase::akPSbgMb3fqMQvmI94

	.method /* 0600001C */ assembly hidebysig static 
		void qANLLJuo8dxWgeAoyK (
			object '',
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x27c4
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4B
		           .emitbyte 0xAA
		           .emitbyte 0x05
		           .emitbyte 0x32
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: callvirt instance void DigisatServer.Domain.Core.EntityBase::set_Id(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600000E */
		IL_001b: ret
	} // end of method EntityBase::qANLLJuo8dxWgeAoyK

	.method /* 0600001D */ assembly hidebysig static 
		bool hKI0uGhjhZaSnUMGaR () cil managed noinlining 
	{
		// Method begins at RVA 0x27e4
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x7B
		           .emitbyte 0x5F
		           .emitbyte 0x1A
		           .emitbyte 0x58
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityBase::hKI0uGhjhZaSnUMGaR

	.method /* 0600001E */ assembly hidebysig static 
		bool pU2vkIH6RKpZTn9V4K () cil managed noinlining 
	{
		// Method begins at RVA 0x27f8
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC8
		           .emitbyte 0xCF
		           .emitbyte 0x58
		           .emitbyte 0x35
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityBase::pU2vkIH6RKpZTn9V4K

	.method /* 0600001F */ assembly hidebysig static 
		valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId LWVjphKG65SQu5bTGl (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x280c
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4C
		           .emitbyte 0xCB
		           .emitbyte 0x77
		           .emitbyte 0x39
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::get_Id() /* 0600000D */
		IL_0017: ret
	} // end of method EntityBase::LWVjphKG65SQu5bTGl

	.method /* 06000020 */ assembly hidebysig static 
		object ARfy3l9WACCTRq2Qhj () cil managed noinlining 
	{
		// Method begins at RVA 0x2828
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x11
		           .emitbyte 0xD1
		           .emitbyte 0x32
		           .emitbyte 0x38
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call class [mscorlib]System.TimeZoneInfo [mscorlib]System.TimeZoneInfo::get_Local() /* 0A000023 */
		IL_0014: ret
	} // end of method EntityBase::ARfy3l9WACCTRq2Qhj

	.method /* 06000021 */ assembly hidebysig static 
		valuetype [mscorlib]System.DateTime W8iaDTtxOZraHXCROG (
			valuetype [mscorlib]System.DateTime '',
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2840
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x62
		           .emitbyte 0xD5
		           .emitbyte 0x23
		           .emitbyte 0x35
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: ldarg 1
		IL_0017: call valuetype [mscorlib]System.DateTime [mscorlib]System.TimeZoneInfo::ConvertTime(valuetype [mscorlib]System.DateTime, class [mscorlib]System.TimeZoneInfo) /* 0A000024 */
		IL_001c: ret
	} // end of method EntityBase::W8iaDTtxOZraHXCROG

	.method /* 06000022 */ assembly hidebysig static 
		bool mfCleRFrcYlfOZLXfS (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2860
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4C
		           .emitbyte 0x3A
		           .emitbyte 0x04
		           .emitbyte 0x4D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: call bool [mscorlib]System.String::IsNullOrWhiteSpace(string) /* 0A000025 */
		IL_0018: ret
	} // end of method EntityBase::mfCleRFrcYlfOZLXfS

	.method /* 06000023 */ assembly hidebysig static 
		object uvqDC3q6vpZbRLksOD (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x287c
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x74
		           .emitbyte 0x52
		           .emitbyte 0x50
		           .emitbyte 0x5A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: call string [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperString::RemoveDiacritics(string) /* 0A000026 */
		IL_0018: ret
	} // end of method EntityBase::uvqDC3q6vpZbRLksOD

	.method /* 06000024 */ assembly hidebysig static 
		object Fs7mGDbx8DN38PjTCj (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2898
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD2
		           .emitbyte 0xAF
		           .emitbyte 0x4A
		           .emitbyte 0x3C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance string [mscorlib]System.String::ToLower() /* 0A000027 */
		IL_0017: ret
	} // end of method EntityBase::Fs7mGDbx8DN38PjTCj

	.method /* 06000025 */ assembly hidebysig static 
		bool w8uZOrLEIZyF5Qj5SJ (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x28b4
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x50
		           .emitbyte 0xE4
		           .emitbyte 0x32
		           .emitbyte 0x67
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: call bool [mscorlib]System.String::IsNullOrEmpty(string) /* 0A000028 */
		IL_0018: ret
	} // end of method EntityBase::w8uZOrLEIZyF5Qj5SJ

	.method /* 06000026 */ assembly hidebysig static 
		void zgO4XNm88EwWdcF0wc (
			object '',
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x28d0
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x09
		           .emitbyte 0xB0
		           .emitbyte 0x2A
		           .emitbyte 0x3D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 1
		IL_0016: callvirt instance void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Interfaces.IVisitor::Visit(object) /* 0A000029 */
		IL_001b: ret
	} // end of method EntityBase::zgO4XNm88EwWdcF0wc

	// Properties
	.property /* 17000002 */ instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId Id()
	{
		.get instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.EntityBase::get_Id()
		.set instance void DigisatServer.Domain.Core.EntityBase::set_Id(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId)
	}
	.property /* 17000003 */ instance valuetype [mscorlib]System.DateTime DataInclusao()
	{
		.get instance valuetype [mscorlib]System.DateTime DigisatServer.Domain.Core.EntityBase::get_DataInclusao()
	}
	.property /* 17000004 */ instance class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string> InformacoesPesquisa()
	{
		.custom instance void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Attribute.IndexAttribute::.ctor(string) = (
			01 00 13 49 6e 66 6f 72 6d 61 63 6f 65 73 50 65
			73 71 75 69 73 61 00 00
		)
		.get instance class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string> DigisatServer.Domain.Core.EntityBase::get_InformacoesPesquisa()
		.set instance void DigisatServer.Domain.Core.EntityBase::set_InformacoesPesquisa(class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string>)
	}

} // end of class DigisatServer.Domain.Core.EntityBase

.class /* 02000006 */ public auto ansi abstract sealed beforefieldinit DigisatServer.Domain.Core.EntityBaseSpecification`1<class (class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>) T>
	extends [mscorlib]System.Object
{
	// Nested Types
	.class /* 02000007 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass1_0'<class (class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000005 */ public string id

		// Methods
		.method /* 06000033 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x2df4
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x67
			           .emitbyte 0xB2
			           .emitbyte 0x65
			           .emitbyte 0x46
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass1_0'::.ctor

		.method /* 06000034 */ assembly hidebysig static 
			bool cAA5mc1FbWCG4E5LpQ () cil managed noinlining 
		{
			// Method begins at RVA 0x2e14
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xD3
			           .emitbyte 0x7C
			           .emitbyte 0x33
			           .emitbyte 0x67
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass1_0'::cAA5mc1FbWCG4E5LpQ

		.method /* 06000035 */ assembly hidebysig static 
			bool rq8SAa6Gn5BdVgxIAb () cil managed noinlining 
		{
			// Method begins at RVA 0x2e28
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xB7
			           .emitbyte 0xF7
			           .emitbyte 0x63
			           .emitbyte 0x49
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass1_0'::rq8SAa6Gn5BdVgxIAb

	} // end of class <>c__DisplayClass1_0

	.class /* 02000008 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass2_0'<class (class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000006 */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id

		// Methods
		.method /* 06000036 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x2e3c
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xE1
			           .emitbyte 0x3B
			           .emitbyte 0x0A
			           .emitbyte 0x6D
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass2_0'::.ctor

		.method /* 06000037 */ assembly hidebysig static 
			bool txcjgBiuvPG13dZB0A () cil managed noinlining 
		{
			// Method begins at RVA 0x2e5c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x9E
			           .emitbyte 0xD7
			           .emitbyte 0x4B
			           .emitbyte 0x4D
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass2_0'::txcjgBiuvPG13dZB0A

		.method /* 06000038 */ assembly hidebysig static 
			bool nykCrNTqRpYNJKsyP6 () cil managed noinlining 
		{
			// Method begins at RVA 0x2e70
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x08
			           .emitbyte 0x00
			           .emitbyte 0x2F
			           .emitbyte 0x32
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass2_0'::nykCrNTqRpYNJKsyP6

	} // end of class <>c__DisplayClass2_0

	.class /* 02000009 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass3_0'<class (class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000007 */ public string informacao

		// Methods
		.method /* 06000039 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x2e84
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xA5
			           .emitbyte 0x66
			           .emitbyte 0x6D
			           .emitbyte 0x36
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass3_0'::.ctor

		.method /* 0600003A */ assembly hidebysig static 
			bool C9VXN45O76MYGFaRiw () cil managed noinlining 
		{
			// Method begins at RVA 0x2ea4
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xD5
			           .emitbyte 0x59
			           .emitbyte 0x33
			           .emitbyte 0x62
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::C9VXN45O76MYGFaRiw

		.method /* 0600003B */ assembly hidebysig static 
			bool iiyPRafJN24tssaRRd () cil managed noinlining 
		{
			// Method begins at RVA 0x2eb8
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x76
			           .emitbyte 0x92
			           .emitbyte 0x34
			           .emitbyte 0x6B
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::iiyPRafJN24tssaRRd

	} // end of class <>c__DisplayClass3_0

	.class /* 0200000A */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass4_0'<class (class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000008 */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId inicial
		.field /* 04000009 */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId 'final'

		// Methods
		.method /* 0600003C */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x2ecc
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xF5
			           .emitbyte 0x3F
			           .emitbyte 0x06
			           .emitbyte 0x44
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass4_0'::.ctor

		.method /* 0600003D */ assembly hidebysig static 
			bool WOBWPyERRJ8MAIH6af () cil managed noinlining 
		{
			// Method begins at RVA 0x2eec
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x85
			           .emitbyte 0x56
			           .emitbyte 0x1E
			           .emitbyte 0x51
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass4_0'::WOBWPyERRJ8MAIH6af

		.method /* 0600003E */ assembly hidebysig static 
			bool h4fxt6DBCuVaiwZeoF () cil managed noinlining 
		{
			// Method begins at RVA 0x2f00
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xAD
			           .emitbyte 0x9D
			           .emitbyte 0x3C
			           .emitbyte 0x6C
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass4_0'::h4fxt6DBCuVaiwZeoF

	} // end of class <>c__DisplayClass4_0


	// Methods
	.method /* 0600002E */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> All () cil managed noinlining 
	{
		// Method begins at RVA 0x29b0
		// Header size: 1
		// Code size: 20 (0x14)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x91
		           .emitbyte 0x59
		           .emitbyte 0x48
		           .emitbyte 0x6D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.TrueSpecification`1<!T>::.ctor() /* 0A00002B */
		IL_0013: ret
	} // end of method EntityBaseSpecification`1::All

	.method /* 0600002F */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorId (
			string id
		) cil managed noinlining 
	{
		// Method begins at RVA 0x29c8
		// Header size: 12
		// Code size: 235 (0xeb)
		.maxstack 14
		.locals /* 11000006 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xEF
		           .emitbyte 0x96
		           .emitbyte 0x29
		           .emitbyte 0x6E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T>::.ctor() /* 0A00002C */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg id
		IL_001f: stfld string class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T>::id /* 0A00002D */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0045: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_004a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_004f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0054: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0059: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_005e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0063: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0068: ldc.i4 1
		IL_006d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0072: dup
		IL_0073: ldc.i4 0
		IL_0078: ldnull
		IL_0079: ldtoken method valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::Parse(string) /* 0A000035 */
		IL_007e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0083: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0088: ldc.i4 1
		IL_008d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0092: dup
		IL_0093: ldc.i4 0
		IL_0098: ldloc 0
		IL_009c: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T> /* 1B000007 */
		IL_00a1: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00a6: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00ab: ldtoken field string class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T>::id /* 0A00002D */
		IL_00b0: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass1_0'<!T> /* 1B000007 */
		IL_00b5: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00ba: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00bf: stelem.ref
		IL_00c0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00c5: stelem.ref
		IL_00c6: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00cb: ldc.i4 1
		IL_00d0: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00d5: dup
		IL_00d6: ldc.i4 0
		IL_00db: ldloc 1
		IL_00df: stelem.ref
		IL_00e0: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00e5: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00ea: ret
	} // end of method EntityBaseSpecification`1::PorId

	.method /* 06000030 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorId (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2ac0
		// Header size: 12
		// Code size: 197 (0xc5)
		.maxstack 9
		.locals /* 11000007 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4B
		           .emitbyte 0x87
		           .emitbyte 0x57
		           .emitbyte 0x58
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T>::.ctor() /* 0A00003C */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg id
		IL_001f: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T>::id /* 0A00003D */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0045: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_004a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_004f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0054: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0059: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_005e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0063: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0068: ldc.i4 1
		IL_006d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0072: dup
		IL_0073: ldc.i4 0
		IL_0078: ldloc 0
		IL_007c: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T> /* 1B00000A */
		IL_0081: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0086: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_008b: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T>::id /* 0A00003D */
		IL_0090: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass2_0'<!T> /* 1B00000A */
		IL_0095: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_009a: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_009f: stelem.ref
		IL_00a0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00a5: ldc.i4 1
		IL_00aa: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00af: dup
		IL_00b0: ldc.i4 0
		IL_00b5: ldloc 1
		IL_00b9: stelem.ref
		IL_00ba: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00bf: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00c4: ret
	} // end of method EntityBaseSpecification`1::PorId

	.method /* 06000031 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorInformacaoPesquisa (
			string informacao
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2b94
		// Header size: 12
		// Code size: 268 (0x10c)
		.maxstack 14
		.locals /* 11000008 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD5
		           .emitbyte 0x08
		           .emitbyte 0x03
		           .emitbyte 0x3C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T>::.ctor() /* 0A00003E */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg informacao
		IL_001f: stfld string class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T>::informacao /* 0A00003F */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldnull
		IL_003d: ldtoken method bool [System.Core]System.Linq.Enumerable::Contains<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, !!0) /* 2B000005 */
		IL_0042: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0047: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_004c: ldc.i4 2
		IL_0051: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0056: dup
		IL_0057: ldc.i4 0
		IL_005c: ldloc 1
		IL_0060: ldtoken method instance class [mscorlib]System.Collections.Generic.IReadOnlyCollection`1<string> class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_InformacoesPesquisa() /* 0A000041 */
		IL_0065: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_006a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_006f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0074: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0079: stelem.ref
		IL_007a: dup
		IL_007b: ldc.i4 1
		IL_0080: ldnull
		IL_0081: ldtoken method string [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperString::RemoveDiacritics(string) /* 0A000026 */
		IL_0086: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_008b: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0090: ldc.i4 1
		IL_0095: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_009a: dup
		IL_009b: ldc.i4 0
		IL_00a0: ldloc 0
		IL_00a4: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B00000B */
		IL_00a9: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00ae: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00b3: ldtoken field string class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T>::informacao /* 0A00003F */
		IL_00b8: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B00000B */
		IL_00bd: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00c2: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00c7: stelem.ref
		IL_00c8: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00cd: ldtoken method instance string [mscorlib]System.String::ToLower() /* 0A000027 */
		IL_00d2: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_00d7: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00dc: call !!0[] [mscorlib]System.Array::Empty<class [System.Core]System.Linq.Expressions.Expression>() /* 2B000006 */
		IL_00e1: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00e6: stelem.ref
		IL_00e7: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00ec: ldc.i4 1
		IL_00f1: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00f6: dup
		IL_00f7: ldc.i4 0
		IL_00fc: ldloc 1
		IL_0100: stelem.ref
		IL_0101: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_0106: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_010b: ret
	} // end of method EntityBaseSpecification`1::PorInformacaoPesquisa

	.method /* 06000032 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorDataInclusao (
			valuetype [mscorlib]System.DateTime dataInicial,
			valuetype [mscorlib]System.DateTime dataFinal
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2cac
		// Header size: 12
		// Code size: 316 (0x13c)
		.maxstack 6
		.locals /* 11000009 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x94
		           .emitbyte 0xA4
		           .emitbyte 0x66
		           .emitbyte 0x33
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>::.ctor() /* 0A000043 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarga dataInicial
		IL_001f: call instance valuetype [mscorlib]System.DateTime [mscorlib]System.DateTime::get_Date() /* 0A000044 */
		IL_0024: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::GenerateNewId(valuetype [mscorlib]System.DateTime) /* 0A000045 */
		IL_0029: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>::inicial /* 0A000046 */
		IL_002e: ldloc 0
		IL_0032: ldarg dataFinal
		IL_0036: call valuetype [mscorlib]System.DateTime [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperDate::ToFinalDia(valuetype [mscorlib]System.DateTime) /* 0A000047 */
		IL_003b: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::GenerateNewId(valuetype [mscorlib]System.DateTime) /* 0A000045 */
		IL_0040: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>::'final' /* 0A000048 */
		IL_0045: ldtoken !T /* 1B000008 */
		IL_004a: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_004f: ldstr "x" /* 70000001 */
		IL_0054: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0059: stloc 1
		IL_005d: ldloc 1
		IL_0061: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0066: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_006b: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_0070: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0075: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_007a: ldloc 0
		IL_007e: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B00000C */
		IL_0083: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0088: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_008d: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>::inicial /* 0A000046 */
		IL_0092: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B00000C */
		IL_0097: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_009c: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00a1: ldc.i4 0
		IL_00a6: ldtoken method bool [MongoDB.Bson]MongoDB.Bson.ObjectId::op_GreaterThanOrEqual(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000049 */
		IL_00ab: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_00b0: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00b5: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::GreaterThanOrEqual(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression, bool, class [mscorlib]System.Reflection.MethodInfo) /* 0A00004A */
		IL_00ba: ldloc 1
		IL_00be: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_00c3: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_00c8: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_00cd: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00d2: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_00d7: ldloc 0
		IL_00db: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B00000C */
		IL_00e0: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00e5: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00ea: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T>::'final' /* 0A000048 */
		IL_00ef: ldtoken class DigisatServer.Domain.Core.EntityBaseSpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B00000C */
		IL_00f4: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00f9: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00fe: ldc.i4 0
		IL_0103: ldtoken method bool [MongoDB.Bson]MongoDB.Bson.ObjectId::op_LessThanOrEqual(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A00004B */
		IL_0108: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_010d: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0112: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::LessThanOrEqual(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression, bool, class [mscorlib]System.Reflection.MethodInfo) /* 0A00004C */
		IL_0117: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::AndAlso(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression) /* 0A00004D */
		IL_011c: ldc.i4 1
		IL_0121: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_0126: dup
		IL_0127: ldc.i4 0
		IL_012c: ldloc 1
		IL_0130: stelem.ref
		IL_0131: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_0136: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_013b: ret
	} // end of method EntityBaseSpecification`1::PorDataInclusao

} // end of class DigisatServer.Domain.Core.EntityBaseSpecification`1

.class /* 0200000B */ public auto ansi beforefieldinit DigisatServer.Domain.Core.EntityEmpresaSpecification`1<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity, [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
	extends class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<!T>
{
	// Nested Types
	.class /* 0200000C */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass2_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity, [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400000A */ public string id

		// Methods
		.method /* 06000047 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x31a4
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x27
			           .emitbyte 0x40
			           .emitbyte 0x07
			           .emitbyte 0x5A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass2_0'::.ctor

		.method /* 06000048 */ assembly hidebysig static 
			bool YAb32AYWtEbeB54XfS () cil managed noinlining 
		{
			// Method begins at RVA 0x31c4
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xB5
			           .emitbyte 0xE4
			           .emitbyte 0x7A
			           .emitbyte 0x57
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass2_0'::YAb32AYWtEbeB54XfS

		.method /* 06000049 */ assembly hidebysig static 
			bool OdB2phw1XtjXNMpwil () cil managed noinlining 
		{
			// Method begins at RVA 0x31d8
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x11
			           .emitbyte 0x9A
			           .emitbyte 0x7D
			           .emitbyte 0x2F
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass2_0'::OdB2phw1XtjXNMpwil

	} // end of class <>c__DisplayClass2_0

	.class /* 0200000D */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass3_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity, [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400000B */ public class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> parsedIds

		// Methods
		.method /* 0600004A */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x31ec
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x7C
			           .emitbyte 0xE0
			           .emitbyte 0x0E
			           .emitbyte 0x5C
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass3_0'::.ctor

		.method /* 0600004B */ assembly hidebysig static 
			bool yPFG9MUtkiySdqVTNq () cil managed noinlining 
		{
			// Method begins at RVA 0x320c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xAD
			           .emitbyte 0x24
			           .emitbyte 0x70
			           .emitbyte 0x6A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::yPFG9MUtkiySdqVTNq

		.method /* 0600004C */ assembly hidebysig static 
			bool seea5W7JGEmrOT7bTO () cil managed noinlining 
		{
			// Method begins at RVA 0x3220
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xB6
			           .emitbyte 0xA5
			           .emitbyte 0x65
			           .emitbyte 0x45
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::seea5W7JGEmrOT7bTO

	} // end of class <>c__DisplayClass3_0


	// Methods
	.method /* 0600003F */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> AtivoPorEmpresa (
			string idEmpresa
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2f14
		// Header size: 1
		// Code size: 34 (0x22)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x50
		           .emitbyte 0x48
		           .emitbyte 0x31
		           .emitbyte 0x5E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg idEmpresa
		IL_0012: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<!T>::PorEmpresa(string) /* 0A00004E */
		IL_0017: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntitySpecification`1<!T>::Ativo() /* 0A00004F */
		IL_001c: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.AndSpecification`1<!T>::.ctor(class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>, class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>) /* 0A000050 */
		IL_0021: ret
	} // end of method EntityEmpresaSpecification`1::AtivoPorEmpresa

	.method /* 06000040 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> AtivoPorEmpresa (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idEmpresa
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2f38
		// Header size: 1
		// Code size: 34 (0x22)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xF0
		           .emitbyte 0x21
		           .emitbyte 0x09
		           .emitbyte 0x67
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg idEmpresa
		IL_0012: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<!T>::PorEmpresa(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000051 */
		IL_0017: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntitySpecification`1<!T>::Ativo() /* 0A00004F */
		IL_001c: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.AndSpecification`1<!T>::.ctor(class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>, class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>) /* 0A000050 */
		IL_0021: ret
	} // end of method EntityEmpresaSpecification`1::AtivoPorEmpresa

	.method /* 06000041 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorId (
			string id
		) cil managed noinlining 
	{
		// Method begins at RVA 0x2f5c
		// Header size: 12
		// Code size: 235 (0xeb)
		.maxstack 14
		.locals /* 1100000A */ init (
			[0] class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x01
		           .emitbyte 0xD3
		           .emitbyte 0x5F
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T>::.ctor() /* 0A000052 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg id
		IL_001f: stfld string class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T>::id /* 0A000053 */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0045: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_004a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_004f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0054: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0059: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_005e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0063: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0068: ldc.i4 1
		IL_006d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0072: dup
		IL_0073: ldc.i4 0
		IL_0078: ldnull
		IL_0079: ldtoken method valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::Parse(string) /* 0A000035 */
		IL_007e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0083: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0088: ldc.i4 1
		IL_008d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0092: dup
		IL_0093: ldc.i4 0
		IL_0098: ldloc 0
		IL_009c: ldtoken class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T> /* 1B000010 */
		IL_00a1: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00a6: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00ab: ldtoken field string class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T>::id /* 0A000053 */
		IL_00b0: ldtoken class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass2_0'<!T> /* 1B000010 */
		IL_00b5: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00ba: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00bf: stelem.ref
		IL_00c0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00c5: stelem.ref
		IL_00c6: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00cb: ldc.i4 1
		IL_00d0: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00d5: dup
		IL_00d6: ldc.i4 0
		IL_00db: ldloc 1
		IL_00df: stelem.ref
		IL_00e0: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00e5: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00ea: ret
	} // end of method EntityEmpresaSpecification`1::PorId

	.method /* 06000042 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorIds (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<string> ids
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3054
		// Header size: 12
		// Code size: 213 (0xd5)
		.maxstack 9
		.locals /* 1100000B */ init (
			[0] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xB2
		           .emitbyte 0xD0
		           .emitbyte 0x63
		           .emitbyte 0x61
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass3_0'<!T>::.ctor() /* 0A000054 */
		IL_0013: dup
		IL_0014: ldarg ids
		IL_0018: ldnull
		IL_0019: ldftn valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::Parse(string) /* 0A000035 */
		IL_001f: newobj instance void class [mscorlib]System.Func`2<string, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::.ctor(object, native int) /* 0A000055 */
		IL_0024: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1> [System.Core]System.Linq.Enumerable::Select<string, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, !!1>) /* 2B000007 */
		IL_0029: call class [mscorlib]System.Collections.Generic.List`1<!!0> [System.Core]System.Linq.Enumerable::ToList<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000008 */
		IL_002e: stfld class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass3_0'<!T>::parsedIds /* 0A000058 */
		IL_0033: ldtoken !T /* 1B000008 */
		IL_0038: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_003d: ldstr "x" /* 70000001 */
		IL_0042: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0047: stloc 0
		IL_004b: ldtoken class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B000011 */
		IL_0050: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0055: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_005a: ldtoken field class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass3_0'<!T>::parsedIds /* 0A000058 */
		IL_005f: ldtoken class DigisatServer.Domain.Core.EntityEmpresaSpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B000011 */
		IL_0064: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_0069: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_006e: ldtoken method instance bool class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::Contains(!0) /* 0A000059 */
		IL_0073: ldtoken class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000013 */
		IL_0078: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_007d: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0082: ldc.i4 1
		IL_0087: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_008c: dup
		IL_008d: ldc.i4 0
		IL_0092: ldloc 0
		IL_0096: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_009b: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_00a0: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_00a5: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00aa: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_00af: stelem.ref
		IL_00b0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00b5: ldc.i4 1
		IL_00ba: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00bf: dup
		IL_00c0: ldc.i4 0
		IL_00c5: ldloc 0
		IL_00c9: stelem.ref
		IL_00ca: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00cf: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00d4: ret
	} // end of method EntityEmpresaSpecification`1::PorIds

	.method /* 06000043 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> InativoPorEmpresa (
			string idEmpresa
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3138
		// Header size: 1
		// Code size: 34 (0x22)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x32
		           .emitbyte 0x2A
		           .emitbyte 0x61
		           .emitbyte 0x41
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg idEmpresa
		IL_0012: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<!T>::PorEmpresa(string) /* 0A00004E */
		IL_0017: call class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0> class DigisatServer.Domain.Core.EntitySpecification`1<!T>::Inativo() /* 0A00005A */
		IL_001c: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.AndSpecification`1<!T>::.ctor(class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>, class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!0>) /* 0A000050 */
		IL_0021: ret
	} // end of method EntityEmpresaSpecification`1::InativoPorEmpresa

	.method /* 06000044 */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x315c
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x81
		           .emitbyte 0x13
		           .emitbyte 0x37
		           .emitbyte 0x49
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<!T>::.ctor() /* 0A00005B */
		IL_001c: ret
	} // end of method EntityEmpresaSpecification`1::.ctor

	.method /* 06000045 */ assembly hidebysig static 
		bool prWbKrl3DCrZQfVllw () cil managed noinlining 
	{
		// Method begins at RVA 0x317c
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xAC
		           .emitbyte 0x50
		           .emitbyte 0x07
		           .emitbyte 0x4A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityEmpresaSpecification`1::prWbKrl3DCrZQfVllw

	.method /* 06000046 */ assembly hidebysig static 
		bool UGYttuRKLVnhA52LJb () cil managed noinlining 
	{
		// Method begins at RVA 0x3190
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x77
		           .emitbyte 0x0B
		           .emitbyte 0x01
		           .emitbyte 0x4F
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityEmpresaSpecification`1::UGYttuRKLVnhA52LJb

} // end of class DigisatServer.Domain.Core.EntityEmpresaSpecification`1

.class /* 0200000E */ public auto ansi abstract beforefieldinit DigisatServer.Domain.Core.EntityTree`1<(class DigisatServer.Domain.Core.EntityTree`1<!T>) T>
	extends DigisatServer.Domain.Core.Entity
	implements [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityTree,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao,
	           class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityBase
{
	// Nested Types
	.class /* 0200000F */ nested private auto ansi sealed serializable beforefieldinit '<>c'<(class DigisatServer.Domain.Core.EntityTree`1<!T>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000012 */ public static initonly class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T> '<>9'
		.field /* 04000013 */ public static class [mscorlib]System.Func`2<!T, string> '<>9__24_0'
		.field /* 04000014 */ public static class [mscorlib]System.Func`2<!T, class [mscorlib]System.Collections.Generic.IEnumerable`1<!T>> '<>9__27_0'
		.field /* 04000015 */ public static class [mscorlib]System.Func`2<!T, string> '<>9__27_1'
		.field /* 04000016 */ public static class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, valuetype [mscorlib]System.Decimal> '<>9__31_0'
		.field /* 04000017 */ public static class [mscorlib]System.Func`2<!T, valuetype [mscorlib]System.Decimal> '<>9__31_1'
		.field /* 04000018 */ public static class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> '<>9__35_0'
		.field /* 04000019 */ public static class [mscorlib]System.Func`2<!T, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> '<>9__35_1'

		// Methods
		.method /* 0600006D */ private hidebysig specialname rtspecialname static 
			void .cctor () cil managed noinlining 
		{
			// Method begins at RVA 0x3c58
			// Header size: 1
			// Code size: 30 (0x1e)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x80
			           .emitbyte 0xDB
			           .emitbyte 0x30
			           .emitbyte 0x60
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: newobj instance void class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::.ctor() /* 0A0000A5 */
			IL_0018: stsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
			IL_001d: ret
		} // end of method '<>c'::.cctor

		.method /* 0600006E */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x3c78
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x80
			           .emitbyte 0xC2
			           .emitbyte 0x03
			           .emitbyte 0x3E
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c'::.ctor

		.method /* 0600006F */ assembly hidebysig 
			instance string '<get_CodigosUnico>b__24_0' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3c98
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x7E
			           .emitbyte 0xAD
			           .emitbyte 0x52
			           .emitbyte 0x53
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
			IL_001c: ret
		} // end of method '<>c'::'<get_CodigosUnico>b__24_0'

		.method /* 06000070 */ assembly hidebysig 
			instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!T> '<Obter>b__27_0' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3cb8
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x4E
			           .emitbyte 0xDC
			           .emitbyte 0x3C
			           .emitbyte 0x6A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::Obter() /* 0A000067 */
			IL_001c: ret
		} // end of method '<>c'::'<Obter>b__27_0'

		.method /* 06000071 */ assembly hidebysig 
			instance string '<Obter>b__27_1' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3cd8
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x60
			           .emitbyte 0x88
			           .emitbyte 0x0E
			           .emitbyte 0x4A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
			IL_001c: ret
		} // end of method '<>c'::'<Obter>b__27_1'

		.method /* 06000072 */ assembly hidebysig 
			instance valuetype [mscorlib]System.Decimal '<get_Valor>b__31_0' (
				class DigisatServer.Domain.Core.IItemEntityTreeValor x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3cf8
			// Header size: 1
			// Code size: 24 (0x18)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xF9
			           .emitbyte 0xF4
			           .emitbyte 0x00
			           .emitbyte 0x5D
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: callvirt instance valuetype [mscorlib]System.Decimal DigisatServer.Domain.Core.IItemEntityTreeValor::get_Valor() /* 060000A6 */
			IL_0017: ret
		} // end of method '<>c'::'<get_Valor>b__31_0'

		.method /* 06000073 */ assembly hidebysig 
			instance valuetype [mscorlib]System.Decimal '<get_Valor>b__31_1' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3d14
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x49
			           .emitbyte 0x64
			           .emitbyte 0x56
			           .emitbyte 0x3C
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance valuetype [mscorlib]System.Decimal class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Valor() /* 0A000087 */
			IL_001c: ret
		} // end of method '<>c'::'<get_Valor>b__31_1'

		.method /* 06000074 */ assembly hidebysig 
			instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> '<get_Itens>b__35_0' (
				class DigisatServer.Domain.Core.IItemEntityTreeValor x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3d34
			// Header size: 1
			// Code size: 24 (0x18)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x3E
			           .emitbyte 0x13
			           .emitbyte 0x42
			           .emitbyte 0x61
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> DigisatServer.Domain.Core.IItemEntityTreeValor::get_Itens() /* 060000A7 */
			IL_0017: ret
		} // end of method '<>c'::'<get_Itens>b__35_0'

		.method /* 06000075 */ assembly hidebysig 
			instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> '<get_Itens>b__35_1' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3d50
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x45
			           .emitbyte 0x6C
			           .emitbyte 0x0E
			           .emitbyte 0x6C
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Itens() /* 0A0000A6 */
			IL_001c: ret
		} // end of method '<>c'::'<get_Itens>b__35_1'

		.method /* 06000076 */ assembly hidebysig static 
			bool a4l73was83Zr7UcFZ3 () cil managed noinlining 
		{
			// Method begins at RVA 0x3d70
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x98
			           .emitbyte 0x84
			           .emitbyte 0x77
			           .emitbyte 0x44
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c'::a4l73was83Zr7UcFZ3

		.method /* 06000077 */ assembly hidebysig static 
			bool G72oUfVPmLd9AuENeD () cil managed noinlining 
		{
			// Method begins at RVA 0x3d84
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x22
			           .emitbyte 0x3B
			           .emitbyte 0x7B
			           .emitbyte 0x39
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c'::G72oUfVPmLd9AuENeD

	} // end of class <>c

	.class /* 02000010 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass42_0'<(class DigisatServer.Domain.Core.EntityTree`1<!T>) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400001A */ public string codigoUnico

		// Methods
		.method /* 06000078 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x3d98
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x49
			           .emitbyte 0x77
			           .emitbyte 0x1E
			           .emitbyte 0x54
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass42_0'::.ctor

		.method /* 06000079 */ assembly hidebysig 
			instance bool '<Obter>b__0' (
				!T x
			) cil managed noinlining 
		{
			// Method begins at RVA 0x3db8
			// Header size: 1
			// Code size: 43 (0x2b)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xD6
			           .emitbyte 0xB4
			           .emitbyte 0x21
			           .emitbyte 0x49
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldarg x
			IL_0012: box !T /* 1B000008 */
			IL_0017: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
			IL_001c: ldarg 0
			IL_0020: ldfld string class DigisatServer.Domain.Core.EntityTree`1/'<>c__DisplayClass42_0'<!T>::codigoUnico /* 0A00009E */
			IL_0025: call bool [mscorlib]System.String::op_Equality(string, string) /* 0A000090 */
			IL_002a: ret
		} // end of method '<>c__DisplayClass42_0'::'<Obter>b__0'

		.method /* 0600007A */ assembly hidebysig static 
			bool ysQU50IXII5K8xalLQ () cil managed noinlining 
		{
			// Method begins at RVA 0x3de4
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xCB
			           .emitbyte 0x40
			           .emitbyte 0x54
			           .emitbyte 0x35
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass42_0'::ysQU50IXII5K8xalLQ

		.method /* 0600007B */ assembly hidebysig static 
			bool vXWB9PoDA7G6asQgBG () cil managed noinlining 
		{
			// Method begins at RVA 0x3df8
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x37
			           .emitbyte 0x0A
			           .emitbyte 0x46
			           .emitbyte 0x4F
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass42_0'::vXWB9PoDA7G6asQgBG

	} // end of class <>c__DisplayClass42_0


	// Fields
	.field /* 0400000C */ private class [mscorlib]System.Collections.Generic.List`1<!T> mFkeoSC4f
	.field /* 0400000D */ private class [mscorlib]System.Collections.Generic.IEnumerable`1<string> NJfTBUq3f
	.field /* 0400000E */ private string HbMlvdXYW
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field /* 0400000F */ private string g0aqpfC53
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field /* 04000010 */ private string zw7hUj2SC
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field /* 04000011 */ family class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor> _itens

	// Methods
	.method /* 0600004D */ family hidebysig specialname rtspecialname 
		instance void .ctor (
			string descricao,
			[opt] string codigoUnico,
			[opt] string codigoUnicoAntecessor,
			[opt] bool ativo,
			[opt] valuetype [mscorlib]System.TimeSpan versao,
			[opt] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		.param [2] = ""
		.param [3] = ""
		.param [4] = bool(true)
		.param [5] = nullref
		.param [6] = nullref
		// Method begins at RVA 0x3234
		// Header size: 12
		// Code size: 252 (0xfc)
		.maxstack 5
		.locals /* 11000001 */ init (
			[0] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x96
		           .emitbyte 0xE4
		           .emitbyte 0x53
		           .emitbyte 0x57
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4 5
		IL_0013: br IL_00af
		IL_0018: ldarg 0
		IL_001c: newobj instance void class [mscorlib]System.Collections.Generic.List`1<!T>::.ctor() /* 0A00005C */
		IL_0021: stfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_0026: ldc.i4.0
		IL_0027: ldc.i4.1
		IL_0028: brfalse IL_00d9
		IL_002d: pop
		IL_002e: ldc.i4 6
		IL_0033: br IL_00af
		IL_0038: ldarg 0
		IL_003c: ldarg codigoUnico
		IL_0040: call bool [mscorlib]System.String::IsNullOrEmpty(string) /* 0A000028 */
		IL_0045: brtrue IL_0053
		IL_004a: ldarg codigoUnico
		IL_004e: br IL_0058
		IL_0053: ldstr "0" /* 70000005 */
		IL_0058: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::set_CodigoUnico(string) /* 0A00005E */
		IL_005d: ldarg 0
		IL_0061: ldarg codigoUnicoAntecessor
		IL_0065: call string [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperString::ToStringEmptyWhenNull(string) /* 0A00005F */
		IL_006a: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::set_CodigoUnicoAntecessor(string) /* 0A000060 */
		IL_006f: ldc.i4 7
		IL_0074: br IL_00af
		IL_0079: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_007e: ldc.i4 0
		IL_0083: br IL_00af
		IL_0088: ldarg 0
		IL_008c: ldarg ativo
		IL_0090: ldarg versao
		IL_0094: ldarg id
		IL_0098: call instance void DigisatServer.Domain.Core.Entity::.ctor(bool, valuetype [mscorlib]System.TimeSpan, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 06000001 */
		IL_009d: br IL_00d4
		IL_00a2: ldc.i4 5
		IL_00a7: stloc 0
		IL_00ab: ldloc 0
		IL_00af: switch (IL_0018, IL_00e4, IL_0088, IL_00d4, IL_0038, IL_0079, IL_0088, IL_00fb)
		IL_00d4: br IL_00e4
		IL_00d9: pop
		IL_00da: ldc.i4 1
		IL_00df: br IL_00af
		IL_00e4: ldarg 0
		IL_00e8: ldarg descricao
		IL_00ec: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::T7aVr7Sl8(string) /* 0A000061 */
		IL_00f1: ldc.i4 4
		IL_00f6: br IL_00af
		IL_00fb: ret
	} // end of method EntityTree`1::.ctor

	.method /* 0600004E */ public hidebysig specialname newslot virtual 
		instance string get_Descricao () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x333c
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x9E
		           .emitbyte 0xBA
		           .emitbyte 0x0F
		           .emitbyte 0x2F
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::HbMlvdXYW /* 0A000062 */
		IL_0017: ret
	} // end of method EntityTree`1::get_Descricao

	.method /* 0600004F */ family hidebysig specialname newslot virtual 
		instance void set_Descricao (
			string 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3358
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x80
		           .emitbyte 0x52
		           .emitbyte 0x49
		           .emitbyte 0x6A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::HbMlvdXYW /* 0A000062 */
		IL_001b: ret
	} // end of method EntityTree`1::set_Descricao

	.method /* 06000050 */ public hidebysig specialname newslot virtual 
		instance string get_CodigoUnicoAntecessor () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3378
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC4
		           .emitbyte 0x98
		           .emitbyte 0x3C
		           .emitbyte 0x62
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::g0aqpfC53 /* 0A000063 */
		IL_0017: ret
	} // end of method EntityTree`1::get_CodigoUnicoAntecessor

	.method /* 06000051 */ family hidebysig specialname newslot virtual 
		instance void set_CodigoUnicoAntecessor (
			string 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3394
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x62
		           .emitbyte 0x71
		           .emitbyte 0x34
		           .emitbyte 0x63
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::g0aqpfC53 /* 0A000063 */
		IL_001b: ret
	} // end of method EntityTree`1::set_CodigoUnicoAntecessor

	.method /* 06000052 */ public hidebysig specialname newslot virtual 
		instance string get_CodigoUnico () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x33b4
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA9
		           .emitbyte 0x76
		           .emitbyte 0x5F
		           .emitbyte 0x41
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::zw7hUj2SC /* 0A000064 */
		IL_0017: ret
	} // end of method EntityTree`1::get_CodigoUnico

	.method /* 06000053 */ family hidebysig specialname newslot virtual 
		instance void set_CodigoUnico (
			string 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x33d0
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC0
		           .emitbyte 0xA7
		           .emitbyte 0x51
		           .emitbyte 0x66
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld string class DigisatServer.Domain.Core.EntityTree`1<!T>::zw7hUj2SC /* 0A000064 */
		IL_001b: ret
	} // end of method EntityTree`1::set_CodigoUnico

	.method /* 06000054 */ public hidebysig specialname newslot virtual 
		instance bool get_IsPrincipal () cil managed noinlining 
	{
		// Method begins at RVA 0x33f0
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x9A
		           .emitbyte 0x4F
		           .emitbyte 0x1B
		           .emitbyte 0x67
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnicoAntecessor() /* 0A000065 */
		IL_0017: call bool [mscorlib]System.String::IsNullOrEmpty(string) /* 0A000028 */
		IL_001c: ret
	} // end of method EntityTree`1::get_IsPrincipal

	.method /* 06000055 */ public hidebysig specialname newslot virtual 
		instance bool get_IsUltimoNivel () cil managed noinlining 
	{
		// Method begins at RVA 0x3410
		// Header size: 1
		// Code size: 36 (0x24)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x00
		           .emitbyte 0xAB
		           .emitbyte 0x17
		           .emitbyte 0x6B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_0017: call bool [System.Core]System.Linq.Enumerable::Any<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000009 */
		IL_001c: ldc.i4 0
		IL_0021: ceq
		IL_0023: ret
	} // end of method EntityTree`1::get_IsUltimoNivel

	.method /* 06000056 */ public hidebysig specialname newslot virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!T> get_Branches () cil managed noinlining 
	{
		// Method begins at RVA 0x3438
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x16
		           .emitbyte 0xFD
		           .emitbyte 0x08
		           .emitbyte 0x66
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_0017: ret
	} // end of method EntityTree`1::get_Branches

	.method /* 06000057 */ family hidebysig specialname newslot virtual 
		instance void set_Branches (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<!T> 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3454
		// Header size: 1
		// Code size: 33 (0x21)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC1
		           .emitbyte 0x9F
		           .emitbyte 0x7B
		           .emitbyte 0x48
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B00000A */
		IL_001b: stfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_0020: ret
	} // end of method EntityTree`1::set_Branches

	.method /* 06000058 */ public hidebysig specialname newslot virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<string> get_CodigosUnico () cil managed noinlining 
	{
		// Method begins at RVA 0x3478
		// Header size: 12
		// Code size: 81 (0x51)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xEA
		           .emitbyte 0xC3
		           .emitbyte 0x1E
		           .emitbyte 0x6E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 0
		IL_0016: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::Obter() /* 0A000067 */
		IL_001b: ldsfld class [mscorlib]System.Func`2<!0, string> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__24_0' /* 0A000068 */
		IL_0020: dup
		IL_0021: brtrue IL_003d
		IL_0026: pop
		IL_0027: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_002c: ldftn instance string class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<get_CodigosUnico>b__24_0'(!0) /* 0A00006A */
		IL_0032: newobj instance void class [mscorlib]System.Func`2<!T, string>::.ctor(object, native int) /* 0A00006B */
		IL_0037: dup
		IL_0038: stsfld class [mscorlib]System.Func`2<!0, string> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__24_0' /* 0A000068 */
		IL_003d: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1> [System.Core]System.Linq.Enumerable::Select<!T, string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, !!1>) /* 2B00000B */
		IL_0042: stfld class [mscorlib]System.Collections.Generic.IEnumerable`1<string> class DigisatServer.Domain.Core.EntityTree`1<!T>::NJfTBUq3f /* 0A00006C */
		IL_0047: ldarg 0
		IL_004b: ldfld class [mscorlib]System.Collections.Generic.IEnumerable`1<string> class DigisatServer.Domain.Core.EntityTree`1<!T>::NJfTBUq3f /* 0A00006C */
		IL_0050: ret
	} // end of method EntityTree`1::get_CodigosUnico

	.method /* 06000059 */ family hidebysig specialname newslot virtual 
		instance void set_CodigosUnico (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<string> 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x34d8
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x1D
		           .emitbyte 0x61
		           .emitbyte 0x40
		           .emitbyte 0x51
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld class [mscorlib]System.Collections.Generic.IEnumerable`1<string> class DigisatServer.Domain.Core.EntityTree`1<!T>::NJfTBUq3f /* 0A00006C */
		IL_001b: ret
	} // end of method EntityTree`1::set_CodigosUnico

	.method /* 0600005A */ public hidebysig newslot virtual 
		instance void DesativarTodos () cil managed noinlining 
	{
		// Method begins at RVA 0x34f8
		// Header size: 12
		// Code size: 106 (0x6a)
		.maxstack 3
		.locals /* 1100000C */ init (
			[0] class [mscorlib]System.Collections.Generic.IEnumerator`1<!T>
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x81
		           .emitbyte 0x20
		           .emitbyte 0x50
		           .emitbyte 0x57
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_0017: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerator`1<!0> class [mscorlib]System.Collections.Generic.IEnumerable`1<!T>::GetEnumerator() /* 0A00006D */
		IL_001c: stloc 0

		.try {
		IL_0020: br IL_0043
		IL_0025: ldloc 0
		IL_0029: callvirt instance !0 class [mscorlib]System.Collections.Generic.IEnumerator`1<!T>::get_Current() /* 0A00006E */
		IL_002e: dup
		IL_002f: box !T /* 1B000008 */
		IL_0034: callvirt instance void DigisatServer.Domain.Core.Entity::Desativar() /* 06000005 */
		IL_0039: box !T /* 1B000008 */
		IL_003e: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::DesativarTodos() /* 0A00006F */
		IL_0043: ldloc 0
		IL_0047: callvirt instance bool [mscorlib]System.Collections.IEnumerator::MoveNext() /* 0A000070 */
		IL_004c: brtrue IL_0025
		IL_0051: leave IL_0069

		} finally {
		IL_0056: ldloc 0
		IL_005a: brfalse IL_0068
		IL_005f: ldloc 0
		IL_0063: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_0068: endfinally

		}
		IL_0069: ret

	} // end of method EntityTree`1::DesativarTodos

	.method /* 0600005B */ public hidebysig newslot virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!T> Obter () cil managed noinlining 
	{
		// Method begins at RVA 0x3580
		// Header size: 12
		// Code size: 141 (0x8d)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA1
		           .emitbyte 0x56
		           .emitbyte 0x3B
		           .emitbyte 0x68
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_0017: ldarg 0
		IL_001b: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_0020: ldsfld class [mscorlib]System.Func`2<!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<!0>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__27_0' /* 0A000071 */
		IL_0025: dup
		IL_0026: brtrue IL_0042
		IL_002b: pop
		IL_002c: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_0031: ldftn instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<Obter>b__27_0'(!0) /* 0A000072 */
		IL_0037: newobj instance void class [mscorlib]System.Func`2<!T, class [mscorlib]System.Collections.Generic.IEnumerable`1<!T>>::.ctor(object, native int) /* 0A000073 */
		IL_003c: dup
		IL_003d: stsfld class [mscorlib]System.Func`2<!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<!0>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__27_0' /* 0A000071 */
		IL_0042: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1> [System.Core]System.Linq.Enumerable::SelectMany<!T, !T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1>>) /* 2B00000C */
		IL_0047: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0> [System.Core]System.Linq.Enumerable::Union<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B00000D */
		IL_004c: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B00000A */
		IL_0051: dup
		IL_0052: ldarg 0
		IL_0056: isinst !T /* 1B000008 */
		IL_005b: unbox.any !T /* 1B000008 */
		IL_0060: callvirt instance void class [mscorlib]System.Collections.Generic.List`1<!T>::Add(!0) /* 0A000076 */
		IL_0065: ldsfld class [mscorlib]System.Func`2<!0, string> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__27_1' /* 0A000077 */
		IL_006a: dup
		IL_006b: brtrue IL_0087
		IL_0070: pop
		IL_0071: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_0076: ldftn instance string class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<Obter>b__27_1'(!0) /* 0A000078 */
		IL_007c: newobj instance void class [mscorlib]System.Func`2<!T, string>::.ctor(object, native int) /* 0A00006B */
		IL_0081: dup
		IL_0082: stsfld class [mscorlib]System.Func`2<!0, string> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__27_1' /* 0A000077 */
		IL_0087: call class [System.Core]System.Linq.IOrderedEnumerable`1<!!0> [System.Core]System.Linq.Enumerable::OrderBy<!T, string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, !!1>) /* 2B00000E */
		IL_008c: ret
	} // end of method EntityTree`1::Obter

	.method /* 0600005C */ public hidebysig newslot virtual 
		instance void ProcessarItens (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor> _movimentos
		) cil managed noinlining 
	{
		// Method begins at RVA 0x361c
		// Header size: 12
		// Code size: 132 (0x84)
		.maxstack 5
		.locals /* 1100000C */ init (
			[0] class [mscorlib]System.Collections.Generic.IEnumerator`1<!T>
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xBF
		           .emitbyte 0xC8
		           .emitbyte 0x30
		           .emitbyte 0x60
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg _movimentos
		IL_0016: ldarg 0
		IL_001a: ldftn instance bool class DigisatServer.Domain.Core.EntityTree`1<!T>::KVmEbgwxo(class DigisatServer.Domain.Core.IItemEntityTreeValor) /* 0A00007A */
		IL_0020: newobj instance void class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, bool>::.ctor(object, native int) /* 0A00007B */
		IL_0025: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0> [System.Core]System.Linq.Enumerable::Where<class DigisatServer.Domain.Core.IItemEntityTreeValor>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, bool>) /* 2B00000F */
		IL_002a: stfld class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor> class DigisatServer.Domain.Core.EntityTree`1<!T>::_itens /* 0A00007D */
		IL_002f: ldarg 0
		IL_0033: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_0038: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerator`1<!0> class [mscorlib]System.Collections.Generic.IEnumerable`1<!T>::GetEnumerator() /* 0A00006D */
		IL_003d: stloc 0

		.try {
		IL_0041: br IL_005d
		IL_0046: ldloc 0
		IL_004a: callvirt instance !0 class [mscorlib]System.Collections.Generic.IEnumerator`1<!T>::get_Current() /* 0A00006E */
		IL_004f: box !T /* 1B000008 */
		IL_0054: ldarg _movimentos
		IL_0058: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::ProcessarItens(class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor>) /* 0A00007E */
		IL_005d: ldloc 0
		IL_0061: callvirt instance bool [mscorlib]System.Collections.IEnumerator::MoveNext() /* 0A000070 */
		IL_0066: brtrue IL_0046
		IL_006b: leave IL_0083

		} finally {
		IL_0070: ldloc 0
		IL_0074: brfalse IL_0082
		IL_0079: ldloc 0
		IL_007d: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_0082: endfinally

		}
		IL_0083: ret

	} // end of method EntityTree`1::ProcessarItens

	.method /* 0600005D */ public hidebysig specialname newslot virtual 
		instance valuetype [mscorlib]System.Decimal get_Valor () cil managed noinlining 
	{
		// Method begins at RVA 0x36bc
		// Header size: 12
		// Code size: 121 (0x79)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x04
		           .emitbyte 0x1E
		           .emitbyte 0x25
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor> class DigisatServer.Domain.Core.EntityTree`1<!T>::_itens /* 0A00007D */
		IL_0017: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<class DigisatServer.Domain.Core.IItemEntityTreeValor>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000010 */
		IL_001c: ldsfld class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, valuetype [mscorlib]System.Decimal> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__31_0' /* 0A00007F */
		IL_0021: dup
		IL_0022: brtrue IL_003e
		IL_0027: pop
		IL_0028: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_002d: ldftn instance valuetype [mscorlib]System.Decimal class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<get_Valor>b__31_0'(class DigisatServer.Domain.Core.IItemEntityTreeValor) /* 0A000080 */
		IL_0033: newobj instance void class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, valuetype [mscorlib]System.Decimal>::.ctor(object, native int) /* 0A000081 */
		IL_0038: dup
		IL_0039: stsfld class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, valuetype [mscorlib]System.Decimal> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__31_0' /* 0A00007F */
		IL_003e: call valuetype [mscorlib]System.Decimal [System.Core]System.Linq.Enumerable::Sum<class DigisatServer.Domain.Core.IItemEntityTreeValor>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, valuetype [mscorlib]System.Decimal>) /* 2B000011 */
		IL_0043: ldarg 0
		IL_0047: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_004c: ldsfld class [mscorlib]System.Func`2<!0, valuetype [mscorlib]System.Decimal> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__31_1' /* 0A000083 */
		IL_0051: dup
		IL_0052: brtrue IL_006e
		IL_0057: pop
		IL_0058: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_005d: ldftn instance valuetype [mscorlib]System.Decimal class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<get_Valor>b__31_1'(!0) /* 0A000084 */
		IL_0063: newobj instance void class [mscorlib]System.Func`2<!T, valuetype [mscorlib]System.Decimal>::.ctor(object, native int) /* 0A000085 */
		IL_0068: dup
		IL_0069: stsfld class [mscorlib]System.Func`2<!0, valuetype [mscorlib]System.Decimal> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__31_1' /* 0A000083 */
		IL_006e: call valuetype [mscorlib]System.Decimal [System.Core]System.Linq.Enumerable::Sum<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, valuetype [mscorlib]System.Decimal>) /* 2B000012 */
		IL_0073: call valuetype [mscorlib]System.Decimal [mscorlib]System.Decimal::op_Addition(valuetype [mscorlib]System.Decimal, valuetype [mscorlib]System.Decimal) /* 0A000086 */
		IL_0078: ret
	} // end of method EntityTree`1::get_Valor

	.method /* 0600005E */ public hidebysig specialname newslot virtual 
		instance valuetype [mscorlib]System.Decimal get_ValorVisivelDre () cil managed noinlining 
	{
		// Method begins at RVA 0x3744
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x84
		           .emitbyte 0xBB
		           .emitbyte 0x46
		           .emitbyte 0x48
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance valuetype [mscorlib]System.Decimal class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Valor() /* 0A000087 */
		IL_0017: ret
	} // end of method EntityTree`1::get_ValorVisivelDre

	.method /* 0600005F */ public hidebysig specialname newslot virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> get_Itens () cil managed noinlining 
	{
		// Method begins at RVA 0x3760
		// Header size: 12
		// Code size: 121 (0x79)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x94
		           .emitbyte 0x74
		           .emitbyte 0x69
		           .emitbyte 0x4C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemEntityTreeValor> class DigisatServer.Domain.Core.EntityTree`1<!T>::_itens /* 0A00007D */
		IL_0017: call class [mscorlib]System.Collections.Generic.List`1<!!0> [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Helper.HelperIEnumerable::ToListEmptyWhenNull<class DigisatServer.Domain.Core.IItemEntityTreeValor>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000010 */
		IL_001c: ldsfld class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__35_0' /* 0A000088 */
		IL_0021: dup
		IL_0022: brtrue IL_003e
		IL_0027: pop
		IL_0028: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_002d: ldftn instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<get_Itens>b__35_0'(class DigisatServer.Domain.Core.IItemEntityTreeValor) /* 0A000089 */
		IL_0033: newobj instance void class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>>::.ctor(object, native int) /* 0A00008A */
		IL_0038: dup
		IL_0039: stsfld class [mscorlib]System.Func`2<class DigisatServer.Domain.Core.IItemEntityTreeValor, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__35_0' /* 0A000088 */
		IL_003e: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1> [System.Core]System.Linq.Enumerable::SelectMany<class DigisatServer.Domain.Core.IItemEntityTreeValor, class DigisatServer.Domain.Core.IItemAgrupamento>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1>>) /* 2B000013 */
		IL_0043: ldarg 0
		IL_0047: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Branches() /* 0A000066 */
		IL_004c: ldsfld class [mscorlib]System.Func`2<!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__35_1' /* 0A00008B */
		IL_0051: dup
		IL_0052: brtrue IL_006e
		IL_0057: pop
		IL_0058: ldsfld class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!0> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9' /* 0A000069 */
		IL_005d: ldftn instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<get_Itens>b__35_1'(!0) /* 0A00008C */
		IL_0063: newobj instance void class [mscorlib]System.Func`2<!T, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>>::.ctor(object, native int) /* 0A00008D */
		IL_0068: dup
		IL_0069: stsfld class [mscorlib]System.Func`2<!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento>> class DigisatServer.Domain.Core.EntityTree`1/'<>c'<!T>::'<>9__35_1' /* 0A00008B */
		IL_006e: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1> [System.Core]System.Linq.Enumerable::SelectMany<!T, class DigisatServer.Domain.Core.IItemAgrupamento>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, class [mscorlib]System.Collections.Generic.IEnumerable`1<!!1>>) /* 2B000014 */
		IL_0073: call class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0> [System.Core]System.Linq.Enumerable::Union<class DigisatServer.Domain.Core.IItemAgrupamento>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000015 */
		IL_0078: ret
	} // end of method EntityTree`1::get_Itens

	.method /* 06000060 */ private hidebysig 
		instance !T xtNoA16p4 (
			string ' '
		) cil managed noinlining 
	{
		// Method begins at RVA 0x37e8
		// Header size: 12
		// Code size: 234 (0xea)
		.maxstack 6
		.locals /* 1100000D */ init (
			[0] !T,
			[1] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xAF
		           .emitbyte 0x26
		           .emitbyte 0x0F
		           .emitbyte 0x38
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldloca 0
		IL_0012: initobj !T /* 1B000008 */
		IL_0018: ldc.i4 0
		IL_001d: stloc 1
		IL_0021: br IL_00ce
		IL_0026: ldarg 0
		IL_002a: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_002f: ldloc 1
		IL_0033: callvirt instance !0 class [mscorlib]System.Collections.Generic.List`1<!T>::get_Item(int32) /* 0A00008E */
		IL_0038: box !T /* 1B000008 */
		IL_003d: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_0042: ldstr "0" /* 70000005 */
		IL_0047: call bool [mscorlib]System.String::op_Equality(string, string) /* 0A000090 */
		IL_004c: brfalse IL_0067
		IL_0051: ldarg 0
		IL_0055: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_005a: ldloc 1
		IL_005e: callvirt instance !0 class [mscorlib]System.Collections.Generic.List`1<!T>::get_Item(int32) /* 0A00008E */
		IL_0063: stloc 0
		IL_0067: ldarg 0
		IL_006b: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_0070: ldloc 1
		IL_0074: callvirt instance !0 class [mscorlib]System.Collections.Generic.List`1<!T>::get_Item(int32) /* 0A00008E */
		IL_0079: box !T /* 1B000008 */
		IL_007e: ldstr "{0}.{1}" /* 70000009 */
		IL_0083: ldarg ' '
		IL_0087: ldloc 1
		IL_008b: ldc.i4 1
		IL_0090: add
		IL_0091: box [mscorlib]System.Int32 /* 01000011 */
		IL_0096: call string [mscorlib]System.String::Format(string, object, object) /* 0A000091 */
		IL_009b: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::SetarCodigoUnico(string) /* 0A000092 */
		IL_00a0: ldarg 0
		IL_00a4: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_00a9: ldloc 1
		IL_00ad: callvirt instance !0 class [mscorlib]System.Collections.Generic.List`1<!T>::get_Item(int32) /* 0A00008E */
		IL_00b2: box !T /* 1B000008 */
		IL_00b7: ldarg ' '
		IL_00bb: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::xalamvaIP(string) /* 0A000093 */
		IL_00c0: ldloc 1
		IL_00c4: ldc.i4 1
		IL_00c9: add
		IL_00ca: stloc 1
		IL_00ce: ldloc 1
		IL_00d2: ldarg 0
		IL_00d6: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_00db: callvirt instance int32 class [mscorlib]System.Collections.Generic.List`1<!T>::get_Count() /* 0A000094 */
		IL_00e0: blt IL_0026
		IL_00e5: ldloc 0
		IL_00e9: ret
	} // end of method EntityTree`1::xtNoA16p4

	.method /* 06000061 */ private hidebysig 
		instance void xalamvaIP (
			string ' '
		) cil managed noinlining 
	{
		// Method begins at RVA 0x38e0
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x11
		           .emitbyte 0x2D
		           .emitbyte 0x38
		           .emitbyte 0x69
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg ' '
		IL_0016: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::set_CodigoUnicoAntecessor(string) /* 0A000060 */
		IL_001b: ret
	} // end of method EntityTree`1::xalamvaIP

	.method /* 06000062 */ public hidebysig newslot virtual 
		instance void SetarCodigoUnico (
			string codigoUnico
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3900
		// Header size: 1
		// Code size: 42 (0x2a)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD8
		           .emitbyte 0x57
		           .emitbyte 0x12
		           .emitbyte 0x42
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg codigoUnico
		IL_0016: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::set_CodigoUnico(string) /* 0A00005E */
		IL_001b: ldarg 0
		IL_001f: ldarg codigoUnico
		IL_0023: call instance !0 class DigisatServer.Domain.Core.EntityTree`1<!T>::xtNoA16p4(string) /* 0A000095 */
		IL_0028: pop
		IL_0029: ret
	} // end of method EntityTree`1::SetarCodigoUnico

	.method /* 06000063 */ private hidebysig 
		instance void T7aVr7Sl8 (
			string ' '
		) cil managed noinlining 
	{
		// Method begins at RVA 0x392c
		// Header size: 12
		// Code size: 166 (0xa6)
		.maxstack 5
		.locals /* 11000001 */ init (
			[0] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xFF
		           .emitbyte 0x77
		           .emitbyte 0x61
		           .emitbyte 0x46
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ldc.i4.1
		IL_0010: brfalse IL_009a
		IL_0015: pop
		IL_0016: ldc.i4 2
		IL_001b: br IL_0056
		IL_0020: ldarg 0
		IL_0024: ldarg ' '
		IL_0028: callvirt instance void DigisatServer.Domain.Core.EntityBase::AdicionarInformacaoPesquisa(string) /* 06000012 */
		IL_002d: ldc.i4 5
		IL_0032: br IL_0056
		IL_0037: ldarg 0
		IL_003b: ldarg ' '
		IL_003f: call instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::set_Descricao(string) /* 0A000096 */
		IL_0044: br IL_0020
		IL_0049: ldc.i4 2
		IL_004e: stloc 0
		IL_0052: ldloc 0
		IL_0056: switch (IL_007d, IL_0095, IL_007d, IL_0020, IL_0037, IL_00a5)
		IL_0073: ldc.i4 3
		IL_0078: br IL_0056
		IL_007d: ldarg ' '
		IL_0081: ldc.i4 1
		IL_0086: ldc.i4 2147483647
		IL_008b: ldstr "Descrição inválida!" /* 70000019 */
		IL_0090: call void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Validation.AssertionConcern::AssertArgumentLength(string, int32, int32, string) /* 0A000097 */
		IL_0095: br IL_0037
		IL_009a: pop
		IL_009b: ldc.i4 4
		IL_00a0: br IL_0056
		IL_00a5: ret
	} // end of method EntityTree`1::T7aVr7Sl8

	.method /* 06000064 */ private hidebysig 
		instance void AOkMdVyPD (
			bool ' '
		) cil managed noinlining 
	{
		// Method begins at RVA 0x39e0
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xD2
		           .emitbyte 0xE3
		           .emitbyte 0x3D
		           .emitbyte 0x37
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg ' '
		IL_0016: callvirt instance void DigisatServer.Domain.Core.Entity::set_Ativo(bool) /* 06000003 */
		IL_001b: ret
	} // end of method EntityTree`1::AOkMdVyPD

	.method /* 06000065 */ public hidebysig newslot virtual 
		instance !T Adicionar (
			!T entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3a00
		// Header size: 12
		// Code size: 186 (0xba)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xBE
		           .emitbyte 0x3A
		           .emitbyte 0x70
		           .emitbyte 0x57
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg entity
		IL_0012: box !T /* 1B000008 */
		IL_0017: ldtoken !T /* 1B000008 */
		IL_001c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0021: callvirt instance string [mscorlib]System.Reflection.MemberInfo::get_Name() /* 0A000098 */
		IL_0026: call void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Validation.AssertionConcern::AssertArgumentNotNull(object, string) /* 0A000099 */
		IL_002b: ldarg entity
		IL_002f: box !T /* 1B000008 */
		IL_0034: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnicoAntecessor() /* 0A000065 */
		IL_0039: ldarg 0
		IL_003d: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_0042: call bool [mscorlib]System.String::op_Equality(string, string) /* 0A000090 */
		IL_0047: brfalse IL_0071
		IL_004c: ldarg 0
		IL_0050: ldfld class [mscorlib]System.Collections.Generic.List`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::mFkeoSC4f /* 0A00005D */
		IL_0055: ldarg entity
		IL_0059: callvirt instance void class [mscorlib]System.Collections.Generic.List`1<!T>::Add(!0) /* 0A000076 */
		IL_005e: ldarg 0
		IL_0062: ldarg 0
		IL_0066: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_006b: call instance !0 class DigisatServer.Domain.Core.EntityTree`1<!T>::xtNoA16p4(string) /* 0A000095 */
		IL_0070: ret
		IL_0071: ldarg 0
		IL_0075: ldarg entity
		IL_0079: box !T /* 1B000008 */
		IL_007e: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnicoAntecessor() /* 0A000065 */
		IL_0083: call instance !0 class DigisatServer.Domain.Core.EntityTree`1<!T>::Obter(string) /* 0A00009A */
		IL_0088: dup
		IL_0089: box !T /* 1B000008 */
		IL_008e: ldstr "Não localizado! Código:" /* 70000041 */
		IL_0093: ldarg entity
		IL_0097: box !T /* 1B000008 */
		IL_009c: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnicoAntecessor() /* 0A000065 */
		IL_00a1: call string [mscorlib]System.String::Concat(string, string) /* 0A00009B */
		IL_00a6: call void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Validation.AssertionConcern::AssertArgumentNotNull(object, string) /* 0A000099 */
		IL_00ab: box !T /* 1B000008 */
		IL_00b0: ldarg entity
		IL_00b4: callvirt instance !0 class DigisatServer.Domain.Core.EntityTree`1<!T>::Adicionar(!0) /* 0A00009C */
		IL_00b9: ret
	} // end of method EntityTree`1::Adicionar

	.method /* 06000066 */ public hidebysig newslot virtual 
		instance !T Obter (
			string codigoUnico
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3ac8
		// Header size: 12
		// Code size: 66 (0x42)
		.maxstack 4
		.locals /* 1100000E */ init (
			[0] class DigisatServer.Domain.Core.EntityTree`1/'<>c__DisplayClass42_0'<!T>
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x5F
		           .emitbyte 0xD8
		           .emitbyte 0x43
		           .emitbyte 0x5F
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityTree`1/'<>c__DisplayClass42_0'<!T>::.ctor() /* 0A00009D */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg codigoUnico
		IL_001f: stfld string class DigisatServer.Domain.Core.EntityTree`1/'<>c__DisplayClass42_0'<!T>::codigoUnico /* 0A00009E */
		IL_0024: ldarg 0
		IL_0028: call instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> class DigisatServer.Domain.Core.EntityTree`1<!T>::Obter() /* 0A000067 */
		IL_002d: ldloc 0
		IL_0031: ldftn instance bool class DigisatServer.Domain.Core.EntityTree`1/'<>c__DisplayClass42_0'<!T>::'<Obter>b__0'(!0) /* 0A00009F */
		IL_0037: newobj instance void class [mscorlib]System.Func`2<!T, bool>::.ctor(object, native int) /* 0A0000A0 */
		IL_003c: call !!0 [System.Core]System.Linq.Enumerable::FirstOrDefault<!T>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>, class [mscorlib]System.Func`2<!!0, bool>) /* 2B000016 */
		IL_0041: ret
	} // end of method EntityTree`1::Obter

	.method /* 06000067 */ public hidebysig newslot virtual 
		instance !T Atualizar (
			!T entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3b18
		// Header size: 12
		// Code size: 152 (0x98)
		.maxstack 5

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x12
		           .emitbyte 0x7A
		           .emitbyte 0x08
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg entity
		IL_0012: box !T /* 1B000008 */
		IL_0017: ldtoken !T /* 1B000008 */
		IL_001c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0021: callvirt instance string [mscorlib]System.Reflection.MemberInfo::get_Name() /* 0A000098 */
		IL_0026: call void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Validation.AssertionConcern::AssertArgumentNotNull(object, string) /* 0A000099 */
		IL_002b: ldarg 0
		IL_002f: ldarg entity
		IL_0033: box !T /* 1B000008 */
		IL_0038: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_003d: call instance !0 class DigisatServer.Domain.Core.EntityTree`1<!T>::Obter(string) /* 0A00009A */
		IL_0042: dup
		IL_0043: box !T /* 1B000008 */
		IL_0048: ldstr "Não localizado! Código:" /* 70000041 */
		IL_004d: ldarg entity
		IL_0051: box !T /* 1B000008 */
		IL_0056: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_005b: call string [mscorlib]System.String::Concat(string, string) /* 0A00009B */
		IL_0060: call void [DigisatServer.Infrastructure.Crosscutting]DigisatServer.Infrastructure.Crosscutting.Validation.AssertionConcern::AssertArgumentNotNull(object, string) /* 0A000099 */
		IL_0065: dup
		IL_0066: box !T /* 1B000008 */
		IL_006b: ldarg entity
		IL_006f: box !T /* 1B000008 */
		IL_0074: callvirt instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Descricao() /* 0A0000A2 */
		IL_0079: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::T7aVr7Sl8(string) /* 0A000061 */
		IL_007e: dup
		IL_007f: box !T /* 1B000008 */
		IL_0084: ldarg entity
		IL_0088: box !T /* 1B000008 */
		IL_008d: callvirt instance bool DigisatServer.Domain.Core.Entity::get_Ativo() /* 06000002 */
		IL_0092: callvirt instance void class DigisatServer.Domain.Core.EntityTree`1<!T>::AOkMdVyPD(bool) /* 0A0000A3 */
		IL_0097: ret
	} // end of method EntityTree`1::Atualizar

	.method /* 06000068 */ public hidebysig virtual 
		instance string ToString () cil managed noinlining 
	{
		// Method begins at RVA 0x3bbc
		// Header size: 1
		// Code size: 43 (0x2b)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xA5
		           .emitbyte 0xE2
		           .emitbyte 0x16
		           .emitbyte 0x39
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldstr "{0} - {1}" /* 70000071 */
		IL_0013: ldarg 0
		IL_0017: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_001c: ldarg 0
		IL_0020: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_Descricao() /* 0A0000A2 */
		IL_0025: call string [mscorlib]System.String::Format(string, object, object) /* 0A000091 */
		IL_002a: ret
	} // end of method EntityTree`1::ToString

	.method /* 06000069 */ private hidebysig 
		instance bool KVmEbgwxo (
			class DigisatServer.Domain.Core.IItemEntityTreeValor ' '
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3be8
		// Header size: 1
		// Code size: 38 (0x26)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x19
		           .emitbyte 0x9B
		           .emitbyte 0x13
		           .emitbyte 0x54
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg ' '
		IL_0012: callvirt instance string DigisatServer.Domain.Core.IItemEntityTreeValor::get_CodigoUnico() /* 060000A5 */
		IL_0017: ldarg 0
		IL_001b: call instance string class DigisatServer.Domain.Core.EntityTree`1<!T>::get_CodigoUnico() /* 0A00008F */
		IL_0020: call bool [mscorlib]System.String::op_Equality(string, string) /* 0A000090 */
		IL_0025: ret
	} // end of method EntityTree`1::KVmEbgwxo

	.method /* 0600006A */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x3c10
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x93
		           .emitbyte 0xFB
		           .emitbyte 0x3A
		           .emitbyte 0x50
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void DigisatServer.Domain.Core.Entity::.ctor() /* 0A0000A4 */
		IL_001c: ret
	} // end of method EntityTree`1::.ctor

	.method /* 0600006B */ assembly hidebysig static 
		bool TpPTa52OkTmfMiq338 () cil managed noinlining 
	{
		// Method begins at RVA 0x3c30
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4E
		           .emitbyte 0xA5
		           .emitbyte 0x28
		           .emitbyte 0x60
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityTree`1::TpPTa52OkTmfMiq338

	.method /* 0600006C */ assembly hidebysig static 
		bool YQsM2iS5LAMDhLC35o () cil managed noinlining 
	{
		// Method begins at RVA 0x3c44
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x07
		           .emitbyte 0x86
		           .emitbyte 0x49
		           .emitbyte 0x33
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityTree`1::YQsM2iS5LAMDhLC35o

	// Properties
	.property /* 17000005 */ instance string Descricao()
	{
		.get instance string DigisatServer.Domain.Core.EntityTree`1::get_Descricao()
		.set instance void DigisatServer.Domain.Core.EntityTree`1::set_Descricao(string)
	}
	.property /* 17000006 */ instance string CodigoUnicoAntecessor()
	{
		.get instance string DigisatServer.Domain.Core.EntityTree`1::get_CodigoUnicoAntecessor()
		.set instance void DigisatServer.Domain.Core.EntityTree`1::set_CodigoUnicoAntecessor(string)
	}
	.property /* 17000007 */ instance string CodigoUnico()
	{
		.get instance string DigisatServer.Domain.Core.EntityTree`1::get_CodigoUnico()
		.set instance void DigisatServer.Domain.Core.EntityTree`1::set_CodigoUnico(string)
	}
	.property /* 17000008 */ instance bool IsPrincipal()
	{
		.get instance bool DigisatServer.Domain.Core.EntityTree`1::get_IsPrincipal()
	}
	.property /* 17000009 */ instance bool IsUltimoNivel()
	{
		.get instance bool DigisatServer.Domain.Core.EntityTree`1::get_IsUltimoNivel()
	}
	.property /* 1700000A */ instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!T> Branches()
	{
		.get instance class [mscorlib]System.Collections.Generic.IEnumerable`1<!0> DigisatServer.Domain.Core.EntityTree`1::get_Branches()
		.set instance void DigisatServer.Domain.Core.EntityTree`1::set_Branches(class [mscorlib]System.Collections.Generic.IEnumerable`1<!0>)
	}
	.property /* 1700000B */ instance class [mscorlib]System.Collections.Generic.IEnumerable`1<string> CodigosUnico()
	{
		.get instance class [mscorlib]System.Collections.Generic.IEnumerable`1<string> DigisatServer.Domain.Core.EntityTree`1::get_CodigosUnico()
		.set instance void DigisatServer.Domain.Core.EntityTree`1::set_CodigosUnico(class [mscorlib]System.Collections.Generic.IEnumerable`1<string>)
	}
	.property /* 1700000C */ instance valuetype [mscorlib]System.Decimal Valor()
	{
		.get instance valuetype [mscorlib]System.Decimal DigisatServer.Domain.Core.EntityTree`1::get_Valor()
	}
	.property /* 1700000D */ instance valuetype [mscorlib]System.Decimal ValorVisivelDre()
	{
		.get instance valuetype [mscorlib]System.Decimal DigisatServer.Domain.Core.EntityTree`1::get_ValorVisivelDre()
	}
	.property /* 1700000E */ instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> Itens()
	{
		.get instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> DigisatServer.Domain.Core.EntityTree`1::get_Itens()
	}

} // end of class DigisatServer.Domain.Core.EntityTree`1

.class /* 02000011 */ public auto ansi abstract beforefieldinit DigisatServer.Domain.Core.EntityVersao
	extends DigisatServer.Domain.Core.EntityBase
	implements [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao,
	           class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>,
	           [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityBase
{
	// Fields
	.field /* 0400001B */ private valuetype [mscorlib]System.TimeSpan X2Lt3AOJk
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method /* 0600007C */ family hidebysig specialname rtspecialname 
		instance void .ctor (
			[opt] valuetype [mscorlib]System.TimeSpan versao,
			[opt] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		.param [1] = nullref
		.param [2] = nullref
		// Method begins at RVA 0x3e0c
		// Header size: 12
		// Code size: 102 (0x66)
		.maxstack 4
		.locals /* 1100000F */ init (
			[0] valuetype [mscorlib]System.TimeSpan,
			[1] valuetype [mscorlib]System.DateTime
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x7A
		           .emitbyte 0xB6
		           .emitbyte 0x46
		           .emitbyte 0x6A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void DigisatServer.Domain.Core.EntityVersao::QVg4lsBqJVBFLDwLAT() /* 06000082 */
		IL_0013: ldarg 0
		IL_0017: ldarg id
		IL_001b: call instance void DigisatServer.Domain.Core.EntityBase::.ctor(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600000C */
		IL_0020: ldarg 0
		IL_0024: ldarga versao
		IL_0028: ldloca 0
		IL_002c: initobj [mscorlib]System.TimeSpan /* 0100001B */
		IL_0032: ldloc 0
		IL_0036: call instance bool [mscorlib]System.TimeSpan::Equals(valuetype [mscorlib]System.TimeSpan) /* 0A0000A7 */
		IL_003b: brtrue IL_0049
		IL_0040: ldarg versao
		IL_0044: br IL_0060
		IL_0049: call valuetype [mscorlib]System.DateTime DigisatServer.Domain.Core.EntityVersao::a8vhqvCEXFZo2FNmMH() /* 06000083 */
		IL_004e: stloc 1
		IL_0052: ldloca 1
		IL_0056: call instance int64 [mscorlib]System.DateTime::get_Ticks() /* 0A0000A8 */
		IL_005b: call valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::cDLr3gQ7PlXQ94fbWX(int64) /* 06000084 */
		IL_0060: call void DigisatServer.Domain.Core.EntityVersao::AdwjCiX63884kqQ7wU(object, valuetype [mscorlib]System.TimeSpan) /* 06000085 */
		IL_0065: ret
	} // end of method EntityVersao::.ctor

	.method /* 0600007D */ public hidebysig specialname newslot virtual 
		instance valuetype [mscorlib]System.TimeSpan get_Versao () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3e80
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x3E
		           .emitbyte 0xE7
		           .emitbyte 0x52
		           .emitbyte 0x6A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::X2Lt3AOJk /* 0400001B */
		IL_0017: ret
	} // end of method EntityVersao::get_Versao

	.method /* 0600007E */ family hidebysig specialname newslot virtual 
		instance void set_Versao (
			valuetype [mscorlib]System.TimeSpan 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x3e9c
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x6B
		           .emitbyte 0x56
		           .emitbyte 0x4D
		           .emitbyte 0x33
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::X2Lt3AOJk /* 0400001B */
		IL_001b: ret
	} // end of method EntityVersao::set_Versao

	.method /* 0600007F */ public hidebysig newslot virtual 
		instance void AtualizarVersao (
			[opt] valuetype [mscorlib]System.TimeSpan versao
		) cil managed noinlining 
	{
		.param [1] = nullref
		// Method begins at RVA 0x3ebc
		// Header size: 12
		// Code size: 84 (0x54)
		.maxstack 4
		.locals /* 1100000F */ init (
			[0] valuetype [mscorlib]System.TimeSpan,
			[1] valuetype [mscorlib]System.DateTime
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC7
		           .emitbyte 0x76
		           .emitbyte 0x1B
		           .emitbyte 0x66
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarga versao
		IL_0016: ldloca 0
		IL_001a: initobj [mscorlib]System.TimeSpan /* 0100001B */
		IL_0020: ldloc 0
		IL_0024: call instance bool [mscorlib]System.TimeSpan::Equals(valuetype [mscorlib]System.TimeSpan) /* 0A0000A7 */
		IL_0029: brtrue IL_0037
		IL_002e: ldarg versao
		IL_0032: br IL_004e
		IL_0037: call valuetype [mscorlib]System.DateTime DigisatServer.Domain.Core.EntityVersao::a8vhqvCEXFZo2FNmMH() /* 06000083 */
		IL_003c: stloc 1
		IL_0040: ldloca 1
		IL_0044: call instance int64 [mscorlib]System.DateTime::get_Ticks() /* 0A0000A8 */
		IL_0049: call valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::cDLr3gQ7PlXQ94fbWX(int64) /* 06000084 */
		IL_004e: call void DigisatServer.Domain.Core.EntityVersao::AdwjCiX63884kqQ7wU(object, valuetype [mscorlib]System.TimeSpan) /* 06000085 */
		IL_0053: ret
	} // end of method EntityVersao::AtualizarVersao

	.method /* 06000080 */ public hidebysig newslot virtual 
		instance void TestarVersao (
			class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3f1c
		// Header size: 1
		// Code size: 63 (0x3f)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x2E
		           .emitbyte 0x76
		           .emitbyte 0x02
		           .emitbyte 0x6C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg entity
		IL_0012: brfalse IL_003e
		IL_0017: ldarg entity
		IL_001b: call valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::mI17n8sdtUKRBa7qbp(object) /* 06000088 */
		IL_0020: ldarg 0
		IL_0024: call valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::p070uNPMyZNhXrHFd1(object) /* 06000089 */
		IL_0029: call bool DigisatServer.Domain.Core.EntityVersao::aO1kTedCNlbeDNbulA(valuetype [mscorlib]System.TimeSpan, valuetype [mscorlib]System.TimeSpan) /* 0600008A */
		IL_002e: brfalse IL_003e
		IL_0033: ldstr "Objeto atualizado por outra conexão" /* 70000085 */
		IL_0038: newobj instance void [mscorlib]System.Exception::.ctor(string) /* 0A0000A9 */
		IL_003d: throw
		IL_003e: ret
	} // end of method EntityVersao::TestarVersao

	.method /* 06000081 */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x3f5c
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x68
		           .emitbyte 0xF5
		           .emitbyte 0x18
		           .emitbyte 0x3B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void DigisatServer.Domain.Core.EntityVersao::QVg4lsBqJVBFLDwLAT() /* 06000082 */
		IL_0013: ldarg 0
		IL_0017: call void DigisatServer.Domain.Core.EntityVersao::hkTHgKz8G4stmIFPG1(object) /* 0600008B */
		IL_001c: ret
	} // end of method EntityVersao::.ctor

	.method /* 06000082 */ assembly hidebysig static 
		void QVg4lsBqJVBFLDwLAT () cil managed noinlining 
	{
		// Method begins at RVA 0x3f7c
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x40
		           .emitbyte 0xE3
		           .emitbyte 0x18
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0014: ret
	} // end of method EntityVersao::QVg4lsBqJVBFLDwLAT

	.method /* 06000083 */ assembly hidebysig static 
		valuetype [mscorlib]System.DateTime a8vhqvCEXFZo2FNmMH () cil managed noinlining 
	{
		// Method begins at RVA 0x3f94
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xCF
		           .emitbyte 0x5F
		           .emitbyte 0x18
		           .emitbyte 0x3D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call valuetype [mscorlib]System.DateTime [mscorlib]System.DateTime::get_Now() /* 0A0000AA */
		IL_0014: ret
	} // end of method EntityVersao::a8vhqvCEXFZo2FNmMH

	.method /* 06000084 */ assembly hidebysig static 
		valuetype [mscorlib]System.TimeSpan cDLr3gQ7PlXQ94fbWX (
			int64 ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3fac
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x8B
		           .emitbyte 0x5A
		           .emitbyte 0x07
		           .emitbyte 0x30
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: call valuetype [mscorlib]System.TimeSpan [mscorlib]System.TimeSpan::FromTicks(int64) /* 0A0000AB */
		IL_0018: ret
	} // end of method EntityVersao::cDLr3gQ7PlXQ94fbWX

	.method /* 06000085 */ assembly hidebysig static 
		void AdwjCiX63884kqQ7wU (
			object '',
			valuetype [mscorlib]System.TimeSpan 'value'
		) cil managed noinlining 
	{
		// Method begins at RVA 0x3fc8
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4A
		           .emitbyte 0x0D
		           .emitbyte 0x5D
		           .emitbyte 0x4C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: callvirt instance void DigisatServer.Domain.Core.EntityVersao::set_Versao(valuetype [mscorlib]System.TimeSpan) /* 0600007E */
		IL_001b: ret
	} // end of method EntityVersao::AdwjCiX63884kqQ7wU

	.method /* 06000086 */ assembly hidebysig static 
		bool Cfw98848v9saM5SxxD () cil managed noinlining 
	{
		// Method begins at RVA 0x3fe8
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x9D
		           .emitbyte 0xFC
		           .emitbyte 0x79
		           .emitbyte 0x43
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityVersao::Cfw98848v9saM5SxxD

	.method /* 06000087 */ assembly hidebysig static 
		bool MVpUly0AP9vCAWPIiv () cil managed noinlining 
	{
		// Method begins at RVA 0x3ffc
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x68
		           .emitbyte 0x29
		           .emitbyte 0x0E
		           .emitbyte 0x4B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityVersao::MVpUly0AP9vCAWPIiv

	.method /* 06000088 */ assembly hidebysig static 
		valuetype [mscorlib]System.TimeSpan mI17n8sdtUKRBa7qbp (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4010
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x33
		           .emitbyte 0x41
		           .emitbyte 0x49
		           .emitbyte 0x58
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance valuetype [mscorlib]System.TimeSpan [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao::get_Versao() /* 0A0000AC */
		IL_0017: ret
	} // end of method EntityVersao::mI17n8sdtUKRBa7qbp

	.method /* 06000089 */ assembly hidebysig static 
		valuetype [mscorlib]System.TimeSpan p070uNPMyZNhXrHFd1 (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x402c
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x06
		           .emitbyte 0x0F
		           .emitbyte 0x76
		           .emitbyte 0x47
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::get_Versao() /* 0600007D */
		IL_0017: ret
	} // end of method EntityVersao::p070uNPMyZNhXrHFd1

	.method /* 0600008A */ assembly hidebysig static 
		bool aO1kTedCNlbeDNbulA (
			valuetype [mscorlib]System.TimeSpan '',
			valuetype [mscorlib]System.TimeSpan ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4048
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xDC
		           .emitbyte 0x4F
		           .emitbyte 0x4F
		           .emitbyte 0x36
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: ldarg 1
		IL_0017: call bool [mscorlib]System.TimeSpan::op_Inequality(valuetype [mscorlib]System.TimeSpan, valuetype [mscorlib]System.TimeSpan) /* 0A0000AD */
		IL_001c: ret
	} // end of method EntityVersao::aO1kTedCNlbeDNbulA

	.method /* 0600008B */ assembly hidebysig static 
		void hkTHgKz8G4stmIFPG1 (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4068
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x32
		           .emitbyte 0x7E
		           .emitbyte 0x73
		           .emitbyte 0x6D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance void DigisatServer.Domain.Core.EntityBase::.ctor() /* 0A0000AE */
		IL_0017: ret
	} // end of method EntityVersao::hkTHgKz8G4stmIFPG1

	// Properties
	.property /* 1700000F */ instance valuetype [mscorlib]System.TimeSpan Versao()
	{
		.get instance valuetype [mscorlib]System.TimeSpan DigisatServer.Domain.Core.EntityVersao::get_Versao()
		.set instance void DigisatServer.Domain.Core.EntityVersao::set_Versao(valuetype [mscorlib]System.TimeSpan)
	}

} // end of class DigisatServer.Domain.Core.EntityVersao

.class /* 02000012 */ public auto ansi beforefieldinit DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
	extends [mscorlib]System.Object
{
	// Nested Types
	.class /* 02000013 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass0_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400001C */ public string idEmpresa

		// Methods
		.method /* 06000091 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x42b4
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x84
			           .emitbyte 0xE3
			           .emitbyte 0x05
			           .emitbyte 0x46
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass0_0'::.ctor

		.method /* 06000092 */ assembly hidebysig static 
			bool JpIUxpk8ARW3WpJy83G () cil managed noinlining 
		{
			// Method begins at RVA 0x42d4
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xDF
			           .emitbyte 0x2E
			           .emitbyte 0x43
			           .emitbyte 0x69
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass0_0'::JpIUxpk8ARW3WpJy83G

		.method /* 06000093 */ assembly hidebysig static 
			bool DTAL3RkGKNHJuv69ac3 () cil managed noinlining 
		{
			// Method begins at RVA 0x42e8
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xAA
			           .emitbyte 0x1A
			           .emitbyte 0x6D
			           .emitbyte 0x5D
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass0_0'::DTAL3RkGKNHJuv69ac3

	} // end of class <>c__DisplayClass0_0

	.class /* 02000014 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass1_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400001D */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idEmpresa

		// Methods
		.method /* 06000094 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x42fc
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x0C
			           .emitbyte 0x63
			           .emitbyte 0x08
			           .emitbyte 0x5B
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass1_0'::.ctor

		.method /* 06000095 */ assembly hidebysig static 
			bool wyJbDFkJAFKQP8KiSqJ () cil managed noinlining 
		{
			// Method begins at RVA 0x431c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x75
			           .emitbyte 0x3D
			           .emitbyte 0x3A
			           .emitbyte 0x3F
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass1_0'::wyJbDFkJAFKQP8KiSqJ

		.method /* 06000096 */ assembly hidebysig static 
			bool KVlL5Mk3EsZpDwCm9y6 () cil managed noinlining 
		{
			// Method begins at RVA 0x4330
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xFD
			           .emitbyte 0xED
			           .emitbyte 0x74
			           .emitbyte 0x42
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass1_0'::KVlL5Mk3EsZpDwCm9y6

	} // end of class <>c__DisplayClass1_0


	// Methods
	.method /* 0600008C */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorEmpresa (
			string idEmpresa
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4084
		// Header size: 12
		// Code size: 271 (0x10f)
		.maxstack 14
		.locals /* 11000010 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression,
			[2] class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T>
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x18
		           .emitbyte 0x80
		           .emitbyte 0x6F
		           .emitbyte 0x3B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T>::.ctor() /* 0A0000AF */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg idEmpresa
		IL_001f: stfld string class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T>::idEmpresa /* 0A0000B0 */
		IL_0024: ldloc 0
		IL_0028: ldfld string class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T>::idEmpresa /* 0A0000B0 */
		IL_002d: call bool [mscorlib]System.String::IsNullOrWhiteSpace(string) /* 0A000025 */
		IL_0032: brtrue IL_0101
		IL_0037: ldtoken !T /* 1B000008 */
		IL_003c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0041: ldstr "x" /* 70000001 */
		IL_0046: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_004b: stloc 1
		IL_004f: ldloc 1
		IL_0053: ldtoken method instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa::get_EmpresaReferencia() /* 0A0000B1 */
		IL_0058: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_005d: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0062: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0067: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_006c: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0071: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0076: ldc.i4 1
		IL_007b: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0080: dup
		IL_0081: ldc.i4 0
		IL_0086: ldnull
		IL_0087: ldtoken method valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::Parse(string) /* 0A000035 */
		IL_008c: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0091: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0096: ldc.i4 1
		IL_009b: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_00a0: dup
		IL_00a1: ldc.i4 0
		IL_00a6: ldloc 0
		IL_00aa: ldtoken class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T> /* 1B000022 */
		IL_00af: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00b4: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00b9: ldtoken field string class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T>::idEmpresa /* 0A0000B0 */
		IL_00be: ldtoken class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass0_0'<!T> /* 1B000022 */
		IL_00c3: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00c8: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00cd: stelem.ref
		IL_00ce: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00d3: stelem.ref
		IL_00d4: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00d9: ldc.i4 1
		IL_00de: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00e3: dup
		IL_00e4: ldc.i4 0
		IL_00e9: ldloc 1
		IL_00ed: stelem.ref
		IL_00ee: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00f3: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00f8: stloc 2
		IL_00fc: ldloc 2
		IL_0100: ret
		IL_0101: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.TrueSpecification`1<!T>::.ctor() /* 0A00002B */
		IL_0106: stloc 2
		IL_010a: ldloc 2
		IL_010e: ret
	} // end of method EntityBaseEmpresaSpecification`1::PorEmpresa

	.method /* 0600008D */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorEmpresa (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idEmpresa
		) cil managed noinlining 
	{
		// Method begins at RVA 0x41a0
		// Header size: 12
		// Code size: 192 (0xc0)
		.maxstack 9
		.locals /* 11000011 */ init (
			[0] class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x08
		           .emitbyte 0x73
		           .emitbyte 0x41
		           .emitbyte 0x68
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T>::.ctor() /* 0A0000B2 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg idEmpresa
		IL_001f: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T>::idEmpresa /* 0A0000B3 */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityEmpresa::get_EmpresaReferencia() /* 0A0000B1 */
		IL_0045: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_004a: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_004f: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0054: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_0059: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_005e: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0063: ldc.i4 1
		IL_0068: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_006d: dup
		IL_006e: ldc.i4 0
		IL_0073: ldloc 0
		IL_0077: ldtoken class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T> /* 1B000023 */
		IL_007c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0081: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_0086: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T>::idEmpresa /* 0A0000B3 */
		IL_008b: ldtoken class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1/'<>c__DisplayClass1_0'<!T> /* 1B000023 */
		IL_0090: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_0095: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_009a: stelem.ref
		IL_009b: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00a0: ldc.i4 1
		IL_00a5: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00aa: dup
		IL_00ab: ldc.i4 0
		IL_00b0: ldloc 1
		IL_00b4: stelem.ref
		IL_00b5: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00ba: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00bf: ret
	} // end of method EntityBaseEmpresaSpecification`1::PorEmpresa

	.method /* 0600008E */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x426c
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x85
		           .emitbyte 0x3C
		           .emitbyte 0x52
		           .emitbyte 0x38
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_001c: ret
	} // end of method EntityBaseEmpresaSpecification`1::.ctor

	.method /* 0600008F */ assembly hidebysig static 
		bool QDXjUEkpaqvlQxNfeIf () cil managed noinlining 
	{
		// Method begins at RVA 0x428c
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x55
		           .emitbyte 0xEE
		           .emitbyte 0x35
		           .emitbyte 0x65
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityBaseEmpresaSpecification`1::QDXjUEkpaqvlQxNfeIf

	.method /* 06000090 */ assembly hidebysig static 
		bool VCot5ukkgBM4FBb0WBC () cil managed noinlining 
	{
		// Method begins at RVA 0x42a0
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x85
		           .emitbyte 0x6A
		           .emitbyte 0x7B
		           .emitbyte 0x56
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityBaseEmpresaSpecification`1::VCot5ukkgBM4FBb0WBC

} // end of class DigisatServer.Domain.Core.EntityBaseEmpresaSpecification`1

.class /* 02000015 */ public auto ansi abstract sealed beforefieldinit DigisatServer.Domain.Core.HelperObjectId
	extends [mscorlib]System.Object
{
	.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
		01 00 00 00
	)
	// Methods
	.method /* 06000097 */ public hidebysig static 
		bool IsDefaultOrEmpty (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId objectId
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x4344
		// Header size: 12
		// Code size: 63 (0x3f)
		.maxstack 3
		.locals /* 11000002 */ init (
			[0] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x67
		           .emitbyte 0xEA
		           .emitbyte 0x1F
		           .emitbyte 0x30
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarga objectId
		IL_0012: ldloca 0
		IL_0016: initobj [MongoDB.Bson]MongoDB.Bson.ObjectId /* 0100001C */
		IL_001c: ldloc 0
		IL_0020: call instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_0025: brtrue IL_0039
		IL_002a: ldarga objectId
		IL_002e: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.HelperObjectId::Kpb7ZvkvV7kSU92WB1c() /* 0600009C */
		IL_0033: call instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_0038: ret
		IL_0039: ldc.i4 1
		IL_003e: ret
	} // end of method HelperObjectId::IsDefaultOrEmpty

	.method /* 06000098 */ public hidebysig static 
		string ToStringEmptyWhenDefault (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId objectId
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x4390
		// Header size: 1
		// Code size: 50 (0x32)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC4
		           .emitbyte 0x82
		           .emitbyte 0x38
		           .emitbyte 0x42
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg objectId
		IL_0012: call bool DigisatServer.Domain.Core.HelperObjectId::yXo4YakWaIhNLWlncDD(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0600009F */
		IL_0017: brtrue IL_002c
		IL_001c: ldarga objectId
		IL_0020: constrained. [MongoDB.Bson]MongoDB.Bson.ObjectId /* 0100001C */
		IL_0026: callvirt instance string [mscorlib]System.Object::ToString() /* 0A0000B4 */
		IL_002b: ret
		IL_002c: ldsfld string [mscorlib]System.String::Empty /* 0A0000B5 */
		IL_0031: ret
	} // end of method HelperObjectId::ToStringEmptyWhenDefault

	.method /* 06000099 */ public hidebysig static 
		valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId TryParseReferencia (
			string referencia
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.ExtensionAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x43c4
		// Header size: 12
		// Code size: 33 (0x21)
		.maxstack 3
		.locals /* 11000002 */ init (
			[0] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x85
		           .emitbyte 0xB3
		           .emitbyte 0x3F
		           .emitbyte 0x68
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg referencia
		IL_0012: ldloca 0
		IL_0016: call bool [MongoDB.Bson]MongoDB.Bson.ObjectId::TryParse(string, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId&) /* 0A0000B6 */
		IL_001b: pop
		IL_001c: ldloc 0
		IL_0020: ret
	} // end of method HelperObjectId::TryParseReferencia

	.method /* 0600009A */ public hidebysig static 
		string GenerateId () cil managed noinlining 
	{
		// Method begins at RVA 0x43f4
		// Header size: 12
		// Code size: 39 (0x27)
		.maxstack 3
		.locals /* 11000002 */ init (
			[0] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x9D
		           .emitbyte 0xB8
		           .emitbyte 0x02
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId DigisatServer.Domain.Core.HelperObjectId::qNRamBkhNu0D4XDgiHA() /* 060000A0 */
		IL_0013: stloc 0
		IL_0017: ldloca 0
		IL_001b: constrained. [MongoDB.Bson]MongoDB.Bson.ObjectId /* 0100001C */
		IL_0021: callvirt instance string [mscorlib]System.Object::ToString() /* 0A0000B4 */
		IL_0026: ret
	} // end of method HelperObjectId::GenerateId

	.method /* 0600009B */ public hidebysig static 
		class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> ConverterStringParaObjectIds (
			class [mscorlib]System.Collections.Generic.IEnumerable`1<string> ids
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4428
		// Header size: 12
		// Code size: 148 (0x94)
		.maxstack 3
		.locals /* 11000012 */ init (
			[0] class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>,
			[1] class [mscorlib]System.Collections.Generic.IEnumerator`1<string>,
			[2] valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC2
		           .emitbyte 0x87
		           .emitbyte 0x0A
		           .emitbyte 0x42
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::.ctor() /* 0A0000B7 */
		IL_0013: stloc 0
		IL_0017: ldarg ids
		IL_001b: brfalse IL_002e
		IL_0020: ldarg ids
		IL_0024: call bool [System.Core]System.Linq.Enumerable::Any<string>(class [mscorlib]System.Collections.Generic.IEnumerable`1<!!0>) /* 2B000003 */
		IL_0029: brtrue IL_0033
		IL_002e: ldloc 0
		IL_0032: ret
		IL_0033: ldarg ids
		IL_0037: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerator`1<!0> class [mscorlib]System.Collections.Generic.IEnumerable`1<string>::GetEnumerator() /* 0A0000B8 */
		IL_003c: stloc 1

		.try {
		IL_0040: br IL_0069
		IL_0045: ldloc 1
		IL_0049: callvirt instance !0 class [mscorlib]System.Collections.Generic.IEnumerator`1<string>::get_Current() /* 0A0000B9 */
		IL_004e: ldloca 2
		IL_0052: call bool [MongoDB.Bson]MongoDB.Bson.ObjectId::TryParse(string, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId&) /* 0A0000B6 */
		IL_0057: brfalse IL_0069
		IL_005c: ldloc 0
		IL_0060: ldloc 2
		IL_0064: callvirt instance void class [mscorlib]System.Collections.Generic.List`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::Add(!0) /* 0A0000BA */
		IL_0069: ldloc 1
		IL_006d: callvirt instance bool [mscorlib]System.Collections.IEnumerator::MoveNext() /* 0A000070 */
		IL_0072: brtrue IL_0045
		IL_0077: leave IL_008f

		} finally {
		IL_007c: ldloc 1
		IL_0080: brfalse IL_008e
		IL_0085: ldloc 1
		IL_0089: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_008e: endfinally

		}
		IL_008f: ldloc 0
		IL_0093: ret

	} // end of method HelperObjectId::ConverterStringParaObjectIds

	.method /* 0600009C */ assembly hidebysig static 
		valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId Kpb7ZvkvV7kSU92WB1c () cil managed noinlining 
	{
		// Method begins at RVA 0x44d8
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x8E
		           .emitbyte 0x1C
		           .emitbyte 0x7E
		           .emitbyte 0x4A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::get_Empty() /* 0A0000BB */
		IL_0014: ret
	} // end of method HelperObjectId::Kpb7ZvkvV7kSU92WB1c

	.method /* 0600009D */ assembly hidebysig static 
		bool n303RDkefekWyw1cfrI () cil managed noinlining 
	{
		// Method begins at RVA 0x44f0
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xCE
		           .emitbyte 0xDA
		           .emitbyte 0x29
		           .emitbyte 0x3C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method HelperObjectId::n303RDkefekWyw1cfrI

	.method /* 0600009E */ assembly hidebysig static 
		bool nKcgjjkrqOB9vHWm5FB () cil managed noinlining 
	{
		// Method begins at RVA 0x4504
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x94
		           .emitbyte 0xE8
		           .emitbyte 0x66
		           .emitbyte 0x6D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method HelperObjectId::nKcgjjkrqOB9vHWm5FB

	.method /* 0600009F */ assembly hidebysig static 
		bool yXo4YakWaIhNLWlncDD (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId objectId
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4518
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x05
		           .emitbyte 0x56
		           .emitbyte 0x6F
		           .emitbyte 0x36
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg objectId
		IL_0013: call bool DigisatServer.Domain.Core.HelperObjectId::IsDefaultOrEmpty(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 06000097 */
		IL_0018: ret
	} // end of method HelperObjectId::yXo4YakWaIhNLWlncDD

	.method /* 060000A0 */ assembly hidebysig static 
		valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId qNRamBkhNu0D4XDgiHA () cil managed noinlining 
	{
		// Method begins at RVA 0x4534
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xF0
		           .emitbyte 0x65
		           .emitbyte 0x3F
		           .emitbyte 0x3C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::GenerateNewId() /* 0A000022 */
		IL_0014: ret
	} // end of method HelperObjectId::qNRamBkhNu0D4XDgiHA

} // end of class DigisatServer.Domain.Core.HelperObjectId

.class /* 02000016 */ interface public auto ansi abstract DigisatServer.Domain.Core.IItemAgrupamento
{
	// Methods
	.method /* 060000A1 */ public hidebysig specialname newslot abstract virtual 
		instance string get_CodigoUnico () cil managed 
	{
	} // end of method IItemAgrupamento::get_CodigoUnico

	.method /* 060000A2 */ public hidebysig specialname newslot abstract virtual 
		instance string get_Descricao () cil managed 
	{
	} // end of method IItemAgrupamento::get_Descricao

	.method /* 060000A3 */ public hidebysig specialname newslot abstract virtual 
		instance valuetype [mscorlib]System.Decimal get_Valor () cil managed 
	{
	} // end of method IItemAgrupamento::get_Valor

	.method /* 060000A4 */ public hidebysig specialname newslot abstract virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> get_Itens () cil managed 
	{
	} // end of method IItemAgrupamento::get_Itens

	// Properties
	.property /* 17000010 */ instance string CodigoUnico()
	{
		.get instance string DigisatServer.Domain.Core.IItemAgrupamento::get_CodigoUnico()
	}
	.property /* 17000011 */ instance string Descricao()
	{
		.get instance string DigisatServer.Domain.Core.IItemAgrupamento::get_Descricao()
	}
	.property /* 17000012 */ instance valuetype [mscorlib]System.Decimal Valor()
	{
		.get instance valuetype [mscorlib]System.Decimal DigisatServer.Domain.Core.IItemAgrupamento::get_Valor()
	}
	.property /* 17000013 */ instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> Itens()
	{
		.get instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> DigisatServer.Domain.Core.IItemAgrupamento::get_Itens()
	}

} // end of class DigisatServer.Domain.Core.IItemAgrupamento

.class /* 02000017 */ interface public auto ansi abstract DigisatServer.Domain.Core.IItemEntityTreeValor
{
	// Methods
	.method /* 060000A5 */ public hidebysig specialname newslot abstract virtual 
		instance string get_CodigoUnico () cil managed 
	{
	} // end of method IItemEntityTreeValor::get_CodigoUnico

	.method /* 060000A6 */ public hidebysig specialname newslot abstract virtual 
		instance valuetype [mscorlib]System.Decimal get_Valor () cil managed 
	{
	} // end of method IItemEntityTreeValor::get_Valor

	.method /* 060000A7 */ public hidebysig specialname newslot abstract virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> get_Itens () cil managed 
	{
	} // end of method IItemEntityTreeValor::get_Itens

	// Properties
	.property /* 17000014 */ instance string CodigoUnico()
	{
		.get instance string DigisatServer.Domain.Core.IItemEntityTreeValor::get_CodigoUnico()
	}
	.property /* 17000015 */ instance valuetype [mscorlib]System.Decimal Valor()
	{
		.get instance valuetype [mscorlib]System.Decimal DigisatServer.Domain.Core.IItemEntityTreeValor::get_Valor()
	}
	.property /* 17000016 */ instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> Itens()
	{
		.get instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.IItemAgrupamento> DigisatServer.Domain.Core.IItemEntityTreeValor::get_Itens()
	}

} // end of class DigisatServer.Domain.Core.IItemEntityTreeValor

.class /* 02000018 */ public auto ansi beforefieldinit DigisatServer.Domain.Core.EntitySpecification`1<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity) T>
	extends [mscorlib]System.Object
{
	// Nested Types
	.class /* 02000019 */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass3_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400001E */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id

		// Methods
		.method /* 060000B2 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x4a48
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x29
			           .emitbyte 0xC9
			           .emitbyte 0x34
			           .emitbyte 0x51
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass3_0'::.ctor

		.method /* 060000B3 */ assembly hidebysig static 
			bool eDLmZakKTDI6xq8n8gW () cil managed noinlining 
		{
			// Method begins at RVA 0x4a68
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xC9
			           .emitbyte 0x3F
			           .emitbyte 0x0C
			           .emitbyte 0x57
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::eDLmZakKTDI6xq8n8gW

		.method /* 060000B4 */ assembly hidebysig static 
			bool zmEKrlk9wTUcGrcJkqu () cil managed noinlining 
		{
			// Method begins at RVA 0x4a7c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xAC
			           .emitbyte 0x8A
			           .emitbyte 0x52
			           .emitbyte 0x56
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass3_0'::zmEKrlk9wTUcGrcJkqu

	} // end of class <>c__DisplayClass3_0

	.class /* 0200001A */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass4_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 0400001F */ public string id

		// Methods
		.method /* 060000B5 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x4a90
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x3D
			           .emitbyte 0xAA
			           .emitbyte 0x7F
			           .emitbyte 0x52
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass4_0'::.ctor

		.method /* 060000B6 */ assembly hidebysig static 
			bool UEk3STkt39DXwreS3jF () cil managed noinlining 
		{
			// Method begins at RVA 0x4ab0
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xFA
			           .emitbyte 0x41
			           .emitbyte 0x37
			           .emitbyte 0x53
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass4_0'::UEk3STkt39DXwreS3jF

		.method /* 060000B7 */ assembly hidebysig static 
			bool jDnDmekFcjBvB1slhUd () cil managed noinlining 
		{
			// Method begins at RVA 0x4ac4
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x55
			           .emitbyte 0x85
			           .emitbyte 0x3D
			           .emitbyte 0x38
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass4_0'::jDnDmekFcjBvB1slhUd

	} // end of class <>c__DisplayClass4_0

	.class /* 0200001B */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass5_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000020 */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idInicial
		.field /* 04000021 */ public valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idfinal

		// Methods
		.method /* 060000B8 */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x4ad8
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x1F
			           .emitbyte 0x80
			           .emitbyte 0x3C
			           .emitbyte 0x3E
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass5_0'::.ctor

		.method /* 060000B9 */ assembly hidebysig static 
			bool omuDQ5kqksPRmWpgYnu () cil managed noinlining 
		{
			// Method begins at RVA 0x4af8
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xF1
			           .emitbyte 0x9A
			           .emitbyte 0x74
			           .emitbyte 0x5B
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass5_0'::omuDQ5kqksPRmWpgYnu

		.method /* 060000BA */ assembly hidebysig static 
			bool nGhVqMkbrpT0mlcmLIS () cil managed noinlining 
		{
			// Method begins at RVA 0x4b0c
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x75
			           .emitbyte 0x42
			           .emitbyte 0x19
			           .emitbyte 0x63
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass5_0'::nGhVqMkbrpT0mlcmLIS

	} // end of class <>c__DisplayClass5_0

	.class /* 0200001C */ nested private auto ansi sealed beforefieldinit '<>c__DisplayClass6_0'<class ([DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity) T>
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field /* 04000022 */ public valuetype [mscorlib]System.TimeSpan versao

		// Methods
		.method /* 060000BB */ public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed noinlining 
		{
			// Method begins at RVA 0x4b20
			// Header size: 1
			// Code size: 29 (0x1d)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x6C
			           .emitbyte 0x02
			           .emitbyte 0x79
			           .emitbyte 0x61
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
			IL_0013: ldarg 0
			IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
			IL_001c: ret
		} // end of method '<>c__DisplayClass6_0'::.ctor

		.method /* 060000BC */ assembly hidebysig static 
			bool vXOFsAkLBZ69q62HHVU () cil managed noinlining 
		{
			// Method begins at RVA 0x4b40
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0xA9
			           .emitbyte 0x62
			           .emitbyte 0x0A
			           .emitbyte 0x4A
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.1
			IL_000f: ret
		} // end of method '<>c__DisplayClass6_0'::vXOFsAkLBZ69q62HHVU

		.method /* 060000BD */ assembly hidebysig static 
			bool gfy2bkkmw6UJEsHVqEp () cil managed noinlining 
		{
			// Method begins at RVA 0x4b54
			// Header size: 1
			// Code size: 16 (0x10)
			.maxstack 8

			IL_0000: br.s IL_000b
			IL_0002: .emitbyte 0x28
			           .emitbyte 0x12
			           .emitbyte 0xD9
			           .emitbyte 0x3B
			           .emitbyte 0x42
			IL_0007: ldnull
			IL_0008: ldc.i4.0
			IL_0009: ldelem.ref
			IL_000a: pop
			IL_000b: ldc.i4.0
			IL_000c: brtrue.s IL_0007
			IL_000e: ldc.i4.0
			IL_000f: ret
		} // end of method '<>c__DisplayClass6_0'::gfy2bkkmw6UJEsHVqEp

	} // end of class <>c__DisplayClass6_0


	// Methods
	.method /* 060000A8 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> Ativo () cil managed noinlining 
	{
		// Method begins at RVA 0x454c
		// Header size: 12
		// Code size: 94 (0x5e)
		.maxstack 6
		.locals /* 1100000B */ init (
			[0] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xCE
		           .emitbyte 0xD2
		           .emitbyte 0x20
		           .emitbyte 0x6E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldtoken !T /* 1B000008 */
		IL_0013: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0018: ldstr "x" /* 70000001 */
		IL_001d: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0022: stloc 0
		IL_0026: ldloc 0
		IL_002a: ldtoken method instance bool [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity::get_Ativo() /* 0A0000BC */
		IL_002f: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0034: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0039: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_003e: ldc.i4 1
		IL_0043: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_0048: dup
		IL_0049: ldc.i4 0
		IL_004e: ldloc 0
		IL_0052: stelem.ref
		IL_0053: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_0058: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_005d: ret
	} // end of method EntitySpecification`1::Ativo

	.method /* 060000A9 */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> Inativo () cil managed noinlining 
	{
		// Method begins at RVA 0x45b8
		// Header size: 12
		// Code size: 99 (0x63)
		.maxstack 6
		.locals /* 1100000B */ init (
			[0] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4C
		           .emitbyte 0x35
		           .emitbyte 0x3E
		           .emitbyte 0x45
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldtoken !T /* 1B000008 */
		IL_0013: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0018: ldstr "x" /* 70000001 */
		IL_001d: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0022: stloc 0
		IL_0026: ldloc 0
		IL_002a: ldtoken method instance bool [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity::get_Ativo() /* 0A0000BC */
		IL_002f: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0034: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0039: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_003e: call class [System.Core]System.Linq.Expressions.UnaryExpression [System.Core]System.Linq.Expressions.Expression::Not(class [System.Core]System.Linq.Expressions.Expression) /* 0A0000BD */
		IL_0043: ldc.i4 1
		IL_0048: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_004d: dup
		IL_004e: ldc.i4 0
		IL_0053: ldloc 0
		IL_0057: stelem.ref
		IL_0058: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_005d: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_0062: ret
	} // end of method EntitySpecification`1::Inativo

	.method /* 060000AA */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> All () cil managed noinlining 
	{
		// Method begins at RVA 0x4628
		// Header size: 1
		// Code size: 20 (0x14)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xAE
		           .emitbyte 0x27
		           .emitbyte 0x3D
		           .emitbyte 0x33
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.TrueSpecification`1<!T>::.ctor() /* 0A00002B */
		IL_0013: ret
	} // end of method EntitySpecification`1::All

	.method /* 060000AB */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorId (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId id
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4640
		// Header size: 12
		// Code size: 197 (0xc5)
		.maxstack 9
		.locals /* 11000013 */ init (
			[0] class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x3B
		           .emitbyte 0xAB
		           .emitbyte 0x30
		           .emitbyte 0x40
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T>::.ctor() /* 0A0000BE */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg id
		IL_001f: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T>::id /* 0A0000BF */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0045: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_004a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_004f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0054: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0059: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_005e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0063: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0068: ldc.i4 1
		IL_006d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0072: dup
		IL_0073: ldc.i4 0
		IL_0078: ldloc 0
		IL_007c: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B000026 */
		IL_0081: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0086: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_008b: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T>::id /* 0A0000BF */
		IL_0090: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass3_0'<!T> /* 1B000026 */
		IL_0095: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_009a: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_009f: stelem.ref
		IL_00a0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00a5: ldc.i4 1
		IL_00aa: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00af: dup
		IL_00b0: ldc.i4 0
		IL_00b5: ldloc 1
		IL_00b9: stelem.ref
		IL_00ba: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00bf: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00c4: ret
	} // end of method EntitySpecification`1::PorId

	.method /* 060000AC */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorId (
			string id
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4714
		// Header size: 12
		// Code size: 235 (0xeb)
		.maxstack 14
		.locals /* 11000014 */ init (
			[0] class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xEE
		           .emitbyte 0x47
		           .emitbyte 0x37
		           .emitbyte 0x4A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T>::.ctor() /* 0A0000C0 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg id
		IL_001f: stfld string class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T>::id /* 0A0000C1 */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0045: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_004a: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_004f: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0054: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0059: ldtoken method instance bool [MongoDB.Bson]MongoDB.Bson.ObjectId::Equals(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000033 */
		IL_005e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0063: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0068: ldc.i4 1
		IL_006d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0072: dup
		IL_0073: ldc.i4 0
		IL_0078: ldnull
		IL_0079: ldtoken method valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId [MongoDB.Bson]MongoDB.Bson.ObjectId::Parse(string) /* 0A000035 */
		IL_007e: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_0083: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0088: ldc.i4 1
		IL_008d: newarr [System.Core]System.Linq.Expressions.Expression /* 01000039 */
		IL_0092: dup
		IL_0093: ldc.i4 0
		IL_0098: ldloc 0
		IL_009c: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B000027 */
		IL_00a1: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00a6: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00ab: ldtoken field string class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T>::id /* 0A0000C1 */
		IL_00b0: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass4_0'<!T> /* 1B000027 */
		IL_00b5: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00ba: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00bf: stelem.ref
		IL_00c0: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00c5: stelem.ref
		IL_00c6: call class [System.Core]System.Linq.Expressions.MethodCallExpression [System.Core]System.Linq.Expressions.Expression::Call(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo, class [System.Core]System.Linq.Expressions.Expression[]) /* 0A000039 */
		IL_00cb: ldc.i4 1
		IL_00d0: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_00d5: dup
		IL_00d6: ldc.i4 0
		IL_00db: ldloc 1
		IL_00df: stelem.ref
		IL_00e0: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00e5: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00ea: ret
	} // end of method EntitySpecification`1::PorId

	.method /* 060000AD */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> PorIntervaloId (
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idInicial,
			valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId idfinal
		) cil managed noinlining 
	{
		// Method begins at RVA 0x480c
		// Header size: 12
		// Code size: 296 (0x128)
		.maxstack 6
		.locals /* 11000015 */ init (
			[0] class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x91
		           .emitbyte 0xF4
		           .emitbyte 0x60
		           .emitbyte 0x33
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>::.ctor() /* 0A0000C2 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg idInicial
		IL_001f: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>::idInicial /* 0A0000C3 */
		IL_0024: ldloc 0
		IL_0028: ldarg idfinal
		IL_002c: stfld valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>::idfinal /* 0A0000C4 */
		IL_0031: ldtoken !T /* 1B000008 */
		IL_0036: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_003b: ldstr "x" /* 70000001 */
		IL_0040: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0045: stloc 1
		IL_0049: ldloc 1
		IL_004d: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_0052: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_0057: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_005c: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_0061: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0066: ldloc 0
		IL_006a: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T> /* 1B000028 */
		IL_006f: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0074: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_0079: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>::idInicial /* 0A0000C3 */
		IL_007e: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T> /* 1B000028 */
		IL_0083: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_0088: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_008d: ldc.i4 0
		IL_0092: ldtoken method bool [MongoDB.Bson]MongoDB.Bson.ObjectId::op_GreaterThanOrEqual(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A000049 */
		IL_0097: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_009c: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00a1: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::GreaterThanOrEqual(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression, bool, class [mscorlib]System.Reflection.MethodInfo) /* 0A00004A */
		IL_00a6: ldloc 1
		IL_00aa: ldtoken method instance !0 class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId>::get_Id() /* 0A000030 */
		IL_00af: ldtoken class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntity`1<valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId> /* 1B000001 */
		IL_00b4: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000031 */
		IL_00b9: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00be: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_00c3: ldloc 0
		IL_00c7: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T> /* 1B000028 */
		IL_00cc: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_00d1: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_00d6: ldtoken field valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T>::idfinal /* 0A0000C4 */
		IL_00db: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass5_0'<!T> /* 1B000028 */
		IL_00e0: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_00e5: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_00ea: ldc.i4 0
		IL_00ef: ldtoken method bool [MongoDB.Bson]MongoDB.Bson.ObjectId::op_LessThanOrEqual(valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId, valuetype [MongoDB.Bson]MongoDB.Bson.ObjectId) /* 0A00004B */
		IL_00f4: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_00f9: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00fe: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::LessThanOrEqual(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression, bool, class [mscorlib]System.Reflection.MethodInfo) /* 0A00004C */
		IL_0103: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::AndAlso(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression) /* 0A00004D */
		IL_0108: ldc.i4 1
		IL_010d: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_0112: dup
		IL_0113: ldc.i4 0
		IL_0118: ldloc 1
		IL_011c: stelem.ref
		IL_011d: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_0122: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_0127: ret
	} // end of method EntitySpecification`1::PorIntervaloId

	.method /* 060000AE */ public hidebysig static 
		class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.ISpecification`1<!T> VersaoMaiorQue (
			valuetype [mscorlib]System.TimeSpan versao
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4940
		// Header size: 12
		// Code size: 180 (0xb4)
		.maxstack 6
		.locals /* 11000016 */ init (
			[0] class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T>,
			[1] class [System.Core]System.Linq.Expressions.ParameterExpression
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xFD
		           .emitbyte 0xA6
		           .emitbyte 0x16
		           .emitbyte 0x3D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: newobj instance void class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T>::.ctor() /* 0A0000C5 */
		IL_0013: stloc 0
		IL_0017: ldloc 0
		IL_001b: ldarg versao
		IL_001f: stfld valuetype [mscorlib]System.TimeSpan class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T>::versao /* 0A0000C6 */
		IL_0024: ldtoken !T /* 1B000008 */
		IL_0029: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_002e: ldstr "x" /* 70000001 */
		IL_0033: call class [System.Core]System.Linq.Expressions.ParameterExpression [System.Core]System.Linq.Expressions.Expression::Parameter(class [mscorlib]System.Type, string) /* 0A00002F */
		IL_0038: stloc 1
		IL_003c: ldloc 1
		IL_0040: ldtoken method instance valuetype [mscorlib]System.TimeSpan [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Interfaces.IEntityVersao::get_Versao() /* 0A0000AC */
		IL_0045: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_004a: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_004f: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Property(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.MethodInfo) /* 0A000032 */
		IL_0054: ldloc 0
		IL_0058: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T> /* 1B000029 */
		IL_005d: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0062: call class [System.Core]System.Linq.Expressions.ConstantExpression [System.Core]System.Linq.Expressions.Expression::Constant(object, class [mscorlib]System.Type) /* 0A000036 */
		IL_0067: ldtoken field valuetype [mscorlib]System.TimeSpan class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T>::versao /* 0A0000C6 */
		IL_006c: ldtoken class DigisatServer.Domain.Core.EntitySpecification`1/'<>c__DisplayClass6_0'<!T> /* 1B000029 */
		IL_0071: call class [mscorlib]System.Reflection.FieldInfo [mscorlib]System.Reflection.FieldInfo::GetFieldFromHandle(valuetype [mscorlib]System.RuntimeFieldHandle, valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A000037 */
		IL_0076: call class [System.Core]System.Linq.Expressions.MemberExpression [System.Core]System.Linq.Expressions.Expression::Field(class [System.Core]System.Linq.Expressions.Expression, class [mscorlib]System.Reflection.FieldInfo) /* 0A000038 */
		IL_007b: ldc.i4 0
		IL_0080: ldtoken method bool [mscorlib]System.TimeSpan::op_GreaterThan(valuetype [mscorlib]System.TimeSpan, valuetype [mscorlib]System.TimeSpan) /* 0A0000C7 */
		IL_0085: call class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.MethodBase::GetMethodFromHandle(valuetype [mscorlib]System.RuntimeMethodHandle) /* 0A000034 */
		IL_008a: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_008f: call class [System.Core]System.Linq.Expressions.BinaryExpression [System.Core]System.Linq.Expressions.Expression::GreaterThan(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.Expression, bool, class [mscorlib]System.Reflection.MethodInfo) /* 0A0000C8 */
		IL_0094: ldc.i4 1
		IL_0099: newarr [System.Core]System.Linq.Expressions.ParameterExpression /* 01000036 */
		IL_009e: dup
		IL_009f: ldc.i4 0
		IL_00a4: ldloc 1
		IL_00a8: stelem.ref
		IL_00a9: call class [System.Core]System.Linq.Expressions.Expression`1<!!0> [System.Core]System.Linq.Expressions.Expression::Lambda<class [mscorlib]System.Func`2<!T, bool>>(class [System.Core]System.Linq.Expressions.Expression, class [System.Core]System.Linq.Expressions.ParameterExpression[]) /* 2B000004 */
		IL_00ae: newobj instance void class [DigisatServer.Infrastructure.Data.Core]DigisatServer.Infrastructure.Data.Core.Specification.DirectSpecification`1<!T>::.ctor(class [System.Core]System.Linq.Expressions.Expression`1<class [mscorlib]System.Func`2<!0, bool>>) /* 0A00003B */
		IL_00b3: ret
	} // end of method EntitySpecification`1::VersaoMaiorQue

	.method /* 060000AF */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x4a00
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x90
		           .emitbyte 0xB2
		           .emitbyte 0x48
		           .emitbyte 0x59
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_001c: ret
	} // end of method EntitySpecification`1::.ctor

	.method /* 060000B0 */ assembly hidebysig static 
		bool PMdITIkMWECgOPEYidC () cil managed noinlining 
	{
		// Method begins at RVA 0x4a20
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xE4
		           .emitbyte 0x97
		           .emitbyte 0x21
		           .emitbyte 0x2E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntitySpecification`1::PMdITIkMWECgOPEYidC

	.method /* 060000B1 */ assembly hidebysig static 
		bool O6HOUYkuqJRgataKf1b () cil managed noinlining 
	{
		// Method begins at RVA 0x4a34
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x53
		           .emitbyte 0x1E
		           .emitbyte 0x1A
		           .emitbyte 0x6C
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntitySpecification`1::O6HOUYkuqJRgataKf1b

} // end of class DigisatServer.Domain.Core.EntitySpecification`1

.class /* 0200001D */ public auto ansi beforefieldinit DigisatServer.Domain.Core.Events.DepoisAdicionarEntityEvent`1<class T>
	extends class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>
{
	// Methods
	.method /* 060000BE */ public hidebysig specialname rtspecialname 
		instance void .ctor (
			!T entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4b68
		// Header size: 1
		// Code size: 33 (0x21)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xB3
		           .emitbyte 0xC6
		           .emitbyte 0x01
		           .emitbyte 0x64
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: ldarg entity
		IL_001b: call instance void class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>::.ctor(!0) /* 0A0000C9 */
		IL_0020: ret
	} // end of method DepoisAdicionarEntityEvent`1::.ctor

} // end of class DigisatServer.Domain.Core.Events.DepoisAdicionarEntityEvent`1

.class /* 0200001E */ public auto ansi beforefieldinit DigisatServer.Domain.Core.Events.DepoisAtualizarEntityEvent`1<class T>
	extends class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>
{
	// Methods
	.method /* 060000BF */ public hidebysig specialname rtspecialname 
		instance void .ctor (
			!T entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4b8c
		// Header size: 1
		// Code size: 33 (0x21)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xE8
		           .emitbyte 0x50
		           .emitbyte 0x2E
		           .emitbyte 0x3B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: ldarg entity
		IL_001b: call instance void class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>::.ctor(!0) /* 0A0000C9 */
		IL_0020: ret
	} // end of method DepoisAtualizarEntityEvent`1::.ctor

} // end of class DigisatServer.Domain.Core.Events.DepoisAtualizarEntityEvent`1

.class /* 0200001F */ public auto ansi abstract sealed beforefieldinit DigisatServer.Domain.Core.Events.DomainEvent
	extends [mscorlib]System.Object
{
	// Fields
	.field /* 04000023 */ private static class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> f1I1CmY5x
	.custom instance void [mscorlib]System.ThreadStaticAttribute::.ctor() = (
		01 00 00 00
	)
	.field /* 04000024 */ public static class DigisatServer.Domain.Core.Events.IEventContainer Container

	// Methods
	.method /* 060000C0 */ public hidebysig static 
		void Register<(DigisatServer.Domain.Core.Events.IDomainEvent) T> (
			class [mscorlib]System.Action`1<!!T> callback
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4bb0
		// Header size: 1
		// Code size: 49 (0x31)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x3F
		           .emitbyte 0x05
		           .emitbyte 0x77
		           .emitbyte 0x52
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldsfld class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> DigisatServer.Domain.Core.Events.DomainEvent::f1I1CmY5x /* 04000023 */
		IL_0013: brtrue IL_0022
		IL_0018: newobj instance void class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate>::.ctor() /* 0A0000CA */
		IL_001d: stsfld class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> DigisatServer.Domain.Core.Events.DomainEvent::f1I1CmY5x /* 04000023 */
		IL_0022: ldsfld class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> DigisatServer.Domain.Core.Events.DomainEvent::f1I1CmY5x /* 04000023 */
		IL_0027: ldarg callback
		IL_002b: callvirt instance void class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate>::Add(!0) /* 0A0000CB */
		IL_0030: ret
	} // end of method DomainEvent::Register

	.method /* 060000C1 */ public hidebysig static 
		void Raise<(DigisatServer.Domain.Core.Events.IDomainEvent) T> (
			!!T domainEvent
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4be4
		// Header size: 12
		// Code size: 209 (0xd1)
		.maxstack 3
		.locals /* 11000017 */ init (
			[0] class [mscorlib]System.Collections.Generic.IEnumerator`1<class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!T>>,
			[1] valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<class [mscorlib]System.Delegate>
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x06
		           .emitbyte 0xBB
		           .emitbyte 0x19
		           .emitbyte 0x46
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldsfld class DigisatServer.Domain.Core.Events.IEventContainer DigisatServer.Domain.Core.Events.DomainEvent::Container /* 04000024 */
		IL_0013: brfalse IL_006c
		IL_0018: ldsfld class DigisatServer.Domain.Core.Events.IEventContainer DigisatServer.Domain.Core.Events.DomainEvent::Container /* 04000024 */
		IL_001d: ldarg domainEvent
		IL_0021: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!0>> DigisatServer.Domain.Core.Events.IEventContainer::Handlers<!!T>(!!0) /* 2B000017 */
		IL_0026: callvirt instance class [mscorlib]System.Collections.Generic.IEnumerator`1<!0> class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!T>>::GetEnumerator() /* 0A0000CC */
		IL_002b: stloc 0

		.try {
		IL_002f: br IL_0046
		IL_0034: ldloc 0
		IL_0038: callvirt instance !0 class [mscorlib]System.Collections.Generic.IEnumerator`1<class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!T>>::get_Current() /* 0A0000CD */
		IL_003d: ldarg domainEvent
		IL_0041: callvirt instance void class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!T>::Handle(!0) /* 0A0000CE */
		IL_0046: ldloc 0
		IL_004a: callvirt instance bool [mscorlib]System.Collections.IEnumerator::MoveNext() /* 0A000070 */
		IL_004f: brtrue IL_0034
		IL_0054: leave IL_006c

		} finally {
		IL_0059: ldloc 0
		IL_005d: brfalse IL_006b
		IL_0062: ldloc 0
		IL_0066: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_006b: endfinally

		}
		IL_006c: ldsfld class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> DigisatServer.Domain.Core.Events.DomainEvent::f1I1CmY5x /* 04000023 */
		IL_0071: brtrue IL_0077
		IL_0076: ret
		IL_0077: ldsfld class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate> DigisatServer.Domain.Core.Events.DomainEvent::f1I1CmY5x /* 04000023 */
		IL_007c: callvirt instance valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<!0> class [mscorlib]System.Collections.Generic.List`1<class [mscorlib]System.Delegate>::GetEnumerator() /* 0A0000CF */
		IL_0081: stloc 1
		.try {
		IL_0085: br IL_00ad
		IL_008a: ldloca 1
		IL_008e: call instance !0 valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<class [mscorlib]System.Delegate>::get_Current() /* 0A0000D0 */
		IL_0093: isinst class [mscorlib]System.Action`1<!!T> /* 1B000030 */
		IL_0098: dup
		IL_0099: brtrue IL_00a4
		IL_009e: pop
		IL_009f: br IL_00ad
		IL_00a4: ldarg domainEvent
		IL_00a8: callvirt instance void class [mscorlib]System.Action`1<!!T>::Invoke(!0) /* 0A0000D1 */
		IL_00ad: ldloca 1
		IL_00b1: call instance bool valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<class [mscorlib]System.Delegate>::MoveNext() /* 0A0000D2 */
		IL_00b6: brtrue IL_008a
		IL_00bb: leave IL_00d0
		} finally {
		IL_00c0: ldloca 1
		IL_00c4: constrained. valuetype [mscorlib]System.Collections.Generic.List`1/Enumerator<class [mscorlib]System.Delegate> /* 1B00002F */
		IL_00ca: callvirt instance void [mscorlib]System.IDisposable::Dispose() /* 0A00001F */
		IL_00cf: endfinally
		}
		IL_00d0: ret


	} // end of method DomainEvent::Raise

} // end of class DigisatServer.Domain.Core.Events.DomainEvent

.class /* 02000020 */ interface public auto ansi abstract DigisatServer.Domain.Core.Events.IDomainEvent
{
} // end of class DigisatServer.Domain.Core.Events.IDomainEvent

.class /* 02000021 */ interface public auto ansi abstract DigisatServer.Domain.Core.Events.IDomainEventHandler`1<-(class DigisatServer.Domain.Core.Events.IDomainEvent) T>
{
	// Methods
	.method /* 060000C2 */ public hidebysig newslot abstract virtual 
		instance void Handle (
			!T domainEvent
		) cil managed 
	{
	} // end of method IDomainEventHandler`1::Handle

} // end of class DigisatServer.Domain.Core.Events.IDomainEventHandler`1

.class /* 02000022 */ interface public auto ansi abstract DigisatServer.Domain.Core.Events.IEventContainer
{
	// Methods
	.method /* 060000C3 */ public hidebysig newslot abstract virtual 
		instance class [mscorlib]System.Collections.Generic.IEnumerable`1<class DigisatServer.Domain.Core.Events.IDomainEventHandler`1<!!T>> Handlers<(DigisatServer.Domain.Core.Events.IDomainEvent) T> (
			!!T domainEvent
		) cil managed 
	{
	} // end of method IEventContainer::Handlers

	.method /* 060000C4 */ public hidebysig newslot abstract virtual 
		instance !!TContainer To<class TContainer> () cil managed 
	{
	} // end of method IEventContainer::To

} // end of class DigisatServer.Domain.Core.Events.IEventContainer

.class /* 02000023 */ public auto ansi abstract beforefieldinit DigisatServer.Domain.Core.Events.EntityEvent`1<class T>
	extends [mscorlib]System.Object
	implements DigisatServer.Domain.Core.Events.IDomainEvent
{
	// Fields
	.field /* 04000025 */ private !T UxtmTLZnk
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method /* 060000C5 */ public hidebysig specialname newslot virtual 
		instance !T get_Entity () cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x4ce0
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4F
		           .emitbyte 0x55
		           .emitbyte 0x7D
		           .emitbyte 0x3E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldfld !0 class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>::UxtmTLZnk /* 0A0000D3 */
		IL_0017: ret
	} // end of method EntityEvent`1::get_Entity

	.method /* 060000C6 */ family hidebysig specialname newslot virtual 
		instance void set_Entity (
			!T 'value'
		) cil managed noinlining 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Method begins at RVA 0x4cfc
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x3D
		           .emitbyte 0xFD
		           .emitbyte 0x02
		           .emitbyte 0x50
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 'value'
		IL_0016: stfld !0 class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>::UxtmTLZnk /* 0A0000D3 */
		IL_001b: ret
	} // end of method EntityEvent`1::set_Entity

	.method /* 060000C7 */ family hidebysig specialname rtspecialname 
		instance void .ctor (
			!T entity
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4d1c
		// Header size: 1
		// Code size: 42 (0x2a)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x3D
		           .emitbyte 0xAB
		           .emitbyte 0x12
		           .emitbyte 0x4E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_001c: ldarg 0
		IL_0020: ldarg entity
		IL_0024: call instance void class DigisatServer.Domain.Core.Events.EntityEvent`1<!T>::set_Entity(!0) /* 0A0000D4 */
		IL_0029: ret
	} // end of method EntityEvent`1::.ctor

	.method /* 060000C8 */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x4d48
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4B
		           .emitbyte 0x2B
		           .emitbyte 0x32
		           .emitbyte 0x37
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0013: ldarg 0
		IL_0017: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_001c: ret
	} // end of method EntityEvent`1::.ctor

	.method /* 060000C9 */ assembly hidebysig static 
		bool XnhwjHkfVX5Xj5XJdHf () cil managed noinlining 
	{
		// Method begins at RVA 0x4d68
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x28
		           .emitbyte 0xA3
		           .emitbyte 0x34
		           .emitbyte 0x45
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method EntityEvent`1::XnhwjHkfVX5Xj5XJdHf

	.method /* 060000CA */ assembly hidebysig static 
		bool kBx7bDkExq0AO6YNugv () cil managed noinlining 
	{
		// Method begins at RVA 0x4d7c
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x5F
		           .emitbyte 0xE0
		           .emitbyte 0x23
		           .emitbyte 0x53
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method EntityEvent`1::kBx7bDkExq0AO6YNugv

	// Properties
	.property /* 17000017 */ instance !T Entity()
	{
		.get instance !0 DigisatServer.Domain.Core.Events.EntityEvent`1::get_Entity()
		.set instance void DigisatServer.Domain.Core.Events.EntityEvent`1::set_Entity(!0)
	}

} // end of class DigisatServer.Domain.Core.Events.EntityEvent`1

.class /* 02000024 */ interface private auto ansi abstract JC4f4JafBUq3fNbMvd.CVyPDAoVmbgwxotFko
{
} // end of class JC4f4JafBUq3fNbMvd.CVyPDAoVmbgwxotFko

.class /* 02000025 */ private auto ansi '<Module>{4245238C-C1B8-4DAC-8D1E-A411B53EDB14}'
	extends [mscorlib]System.Object
{
} // end of class <Module>{4245238C-C1B8-4DAC-8D1E-A411B53EDB14}

.class /* 02000026 */ private auto ansi beforefieldinit JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2
	extends [mscorlib]System.Object
{
	// Nested Types
	.class /* 02000027 */ nested assembly auto ansi sealed SFU4mbT3GMret7THonf
		extends [mscorlib]System.MulticastDelegate
	{
		// Methods
		.method /* 060000D9 */ public hidebysig specialname rtspecialname 
			instance void .ctor (
				object 'object',
				native int 'method'
			) runtime managed 
		{
		} // end of method SFU4mbT3GMret7THonf::.ctor

		.method /* 060000DA */ public hidebysig newslot virtual 
			instance void Invoke (
				object o
			) runtime managed 
		{
		} // end of method SFU4mbT3GMret7THonf::Invoke

		.method /* 060000DB */ public hidebysig newslot virtual 
			instance class [mscorlib]System.IAsyncResult BeginInvoke (
				object o,
				class [mscorlib]System.AsyncCallback callback,
				object 'object'
			) runtime managed 
		{
		} // end of method SFU4mbT3GMret7THonf::BeginInvoke

		.method /* 060000DC */ public hidebysig newslot virtual 
			instance void EndInvoke (
				class [mscorlib]System.IAsyncResult result
			) runtime managed 
		{
		} // end of method SFU4mbT3GMret7THonf::EndInvoke

	} // end of class SFU4mbT3GMret7THonf


	// Fields
	.field /* 04000026 */ assembly static object GKcKVYbcA

	// Methods
	.method /* 060000CB */ assembly hidebysig static 
		void x8D7MLggnPBiu (
			int32 typemdt
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4d90
		// Header size: 12
		// Code size: 309 (0x135)
		.maxstack 5
		.locals /* 11000018 */ init (
			[0] class [mscorlib]System.Type,
			[1] class [mscorlib]System.Reflection.FieldInfo,
			[2] class [mscorlib]System.Reflection.MethodInfo,
			[3] class [mscorlib]System.Reflection.FieldInfo[],
			[4] int32,
			[5] int32
		)

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x47
		           .emitbyte 0x98
		           .emitbyte 0x25
		           .emitbyte 0x65
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4 3
		IL_0013: br IL_00d6
		IL_0018: ldloc.s 4
		IL_001a: ldloc.3
		IL_001b: ldlen
		IL_001c: conv.i4
		IL_001d: blt IL_0031
		IL_0022: ldc.i4 10
		IL_0027: br IL_00d6
		IL_002c: br IL_0018
		IL_0031: ldloc.3
		IL_0032: ldloc.s 4
		IL_0034: ldelem.ref
		IL_0035: stloc.1
		IL_0036: call bool JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::hFaIn2kU5EOIHkGZ25y() /* 060000D4 */
		IL_003b: call bool JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::XC8LbjkwVMhYgI7EFh9() /* 060000D3 */
		IL_0040: brfalse IL_010c
		IL_0045: pop
		IL_0046: ldc.i4 1
		IL_004b: br IL_00d6
		IL_0050: ldloc.0
		IL_0051: callvirt instance class [mscorlib]System.Reflection.FieldInfo[] [mscorlib]System.Type::GetFields() /* 0A0000D5 */
		IL_0056: stloc.3
		IL_0057: ldc.i4 2
		IL_005c: call bool JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::XC8LbjkwVMhYgI7EFh9() /* 060000D3 */
		IL_0061: brtrue IL_00d6
		IL_0066: pop
		IL_0067: ldsfld object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::GKcKVYbcA /* 04000026 */
		IL_006c: ldc.i4 33554432
		IL_0071: ldarg.0
		IL_0072: add
		IL_0073: call class [mscorlib]System.Type JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::QXMZcMk7DdWNyNK6E5y(object, int32) /* 060000CE */
		IL_0078: stloc.0
		IL_0079: ldc.i4 7
		IL_007e: br IL_00d6
		IL_0083: ldc.i4.0
		IL_0084: stloc.s 4
		IL_0086: ldc.i4 8
		IL_008b: ldc.i4.0
		IL_008c: brfalse IL_00d6
		IL_0091: pop
		IL_0092: ldloc.s 4
		IL_0094: ldc.i4.1
		IL_0095: add
		IL_0096: stloc.s 4
		IL_0098: ldc.i4 9
		IL_009d: call bool JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::XC8LbjkwVMhYgI7EFh9() /* 060000D3 */
		IL_00a2: brtrue IL_00d6
		IL_00a7: pop
		IL_00a8: ldsfld object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::GKcKVYbcA /* 04000026 */
		IL_00ad: ldloc.1
		IL_00ae: call int32 JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::oNXvFUk2l2Tni2pii2J(object) /* 060000CF */
		IL_00b3: ldc.i4 100663296
		IL_00b8: add
		IL_00b9: call object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::dRHLd8kSXHxQ3f3DGbv(object, int32) /* 060000D0 */
		IL_00be: castclass [mscorlib]System.Reflection.MethodInfo /* 0100003C */
		IL_00c3: stloc.2
		IL_00c4: br IL_0107
		IL_00c9: ldc.i4 3
		IL_00ce: stloc 5
		IL_00d2: ldloc 5
		IL_00d6: switch (IL_0117, IL_00a8, IL_0083, IL_0067, IL_00a8, IL_0107, IL_0092, IL_0050, IL_002c, IL_0018, IL_0134)
		IL_0107: br IL_0117
		IL_010c: pop
		IL_010d: ldc.i4 0
		IL_0112: br IL_00d6
		IL_0117: ldloc.1
		IL_0118: ldnull
		IL_0119: ldloc.0
		IL_011a: ldloc.2
		IL_011b: call object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::c0XeZykahZHaUUxqH0F(class [mscorlib]System.Type, object) /* 060000D1 */
		IL_0120: castclass [mscorlib]System.MulticastDelegate /* 01000019 */
		IL_0125: call void JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::lHdSRJkVlQ2UGohqYAg(object, object, object) /* 060000D2 */
		IL_012a: ldc.i4 6
		IL_012f: br IL_00d6
		IL_0134: ret
	} // end of method IYWM0aVpfC53gw7Uj2::x8D7MLggnPBiu

	.method /* 060000CC */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x4ed4
		// Header size: 1
		// Code size: 26 (0x1a)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x47
		           .emitbyte 0x57
		           .emitbyte 0x65
		           .emitbyte 0x44
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::GuiroIkIin5pcfkvt6i() /* 060000D5 */
		IL_0013: ldarg.0
		IL_0014: call void JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::yKdsr0koNYvoTTkHZa3(object) /* 060000D6 */
		IL_0019: ret
	} // end of method IYWM0aVpfC53gw7Uj2::.ctor

	.method /* 060000CD */ private hidebysig specialname rtspecialname static 
		void .cctor () cil managed noinlining 
	{
		// Method begins at RVA 0x4ef0
		// Header size: 1
		// Code size: 45 (0x2d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x17
		           .emitbyte 0xDA
		           .emitbyte 0x16
		           .emitbyte 0x58
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: call void JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::GuiroIkIin5pcfkvt6i() /* 060000D5 */
		IL_0013: ldtoken JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2 /* 02000026 */
		IL_0018: call class [mscorlib]System.Type JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::L8Cyspk4lYHNF6JYFVn(valuetype [mscorlib]System.RuntimeTypeHandle) /* 060000D7 */
		IL_001d: callvirt instance class [mscorlib]System.Reflection.Assembly [mscorlib]System.Type::get_Assembly() /* 0A0000D6 */
		IL_0022: call object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::r2auXyk0jFbsQVjb6FJ(object) /* 060000D8 */
		IL_0027: stsfld object JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2::GKcKVYbcA /* 04000026 */
		IL_002c: ret
	} // end of method IYWM0aVpfC53gw7Uj2::.cctor

	.method /* 060000CE */ assembly hidebysig static 
		class [mscorlib]System.Type QXMZcMk7DdWNyNK6E5y (
			object '',
			int32 ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4f20
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xC5
		           .emitbyte 0x9E
		           .emitbyte 0x2D
		           .emitbyte 0x56
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 1
		IL_0016: callvirt instance class [mscorlib]System.Type [mscorlib]System.Reflection.Module::ResolveType(int32) /* 0A0000D7 */
		IL_001b: ret
	} // end of method IYWM0aVpfC53gw7Uj2::QXMZcMk7DdWNyNK6E5y

	.method /* 060000CF */ assembly hidebysig static 
		int32 oNXvFUk2l2Tni2pii2J (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4f40
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x4C
		           .emitbyte 0x83
		           .emitbyte 0x44
		           .emitbyte 0x6D
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance int32 [mscorlib]System.Reflection.MemberInfo::get_MetadataToken() /* 0A0000D8 */
		IL_0017: ret
	} // end of method IYWM0aVpfC53gw7Uj2::oNXvFUk2l2Tni2pii2J

	.method /* 060000D0 */ assembly hidebysig static 
		object dRHLd8kSXHxQ3f3DGbv (
			object '',
			int32 ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4f5c
		// Header size: 1
		// Code size: 28 (0x1c)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x7D
		           .emitbyte 0x5E
		           .emitbyte 0x2E
		           .emitbyte 0x69
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 1
		IL_0016: callvirt instance class [mscorlib]System.Reflection.MethodBase [mscorlib]System.Reflection.Module::ResolveMethod(int32) /* 0A0000D9 */
		IL_001b: ret
	} // end of method IYWM0aVpfC53gw7Uj2::dRHLd8kSXHxQ3f3DGbv

	.method /* 060000D1 */ assembly hidebysig static 
		object c0XeZykahZHaUUxqH0F (
			class [mscorlib]System.Type '',
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4f7c
		// Header size: 1
		// Code size: 29 (0x1d)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x79
		           .emitbyte 0x59
		           .emitbyte 0x27
		           .emitbyte 0x6B
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: ldarg 1
		IL_0017: call class [mscorlib]System.Delegate [mscorlib]System.Delegate::CreateDelegate(class [mscorlib]System.Type, class [mscorlib]System.Reflection.MethodInfo) /* 0A0000DA */
		IL_001c: ret
	} // end of method IYWM0aVpfC53gw7Uj2::c0XeZykahZHaUUxqH0F

	.method /* 060000D2 */ assembly hidebysig static 
		void lHdSRJkVlQ2UGohqYAg (
			object '',
			object '',
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x4f9c
		// Header size: 1
		// Code size: 32 (0x20)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xBA
		           .emitbyte 0xAF
		           .emitbyte 0x2A
		           .emitbyte 0x6A
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: ldarg 1
		IL_0016: ldarg 2
		IL_001a: callvirt instance void [mscorlib]System.Reflection.FieldInfo::SetValue(object, object) /* 0A0000DB */
		IL_001f: ret
	} // end of method IYWM0aVpfC53gw7Uj2::lHdSRJkVlQ2UGohqYAg

	.method /* 060000D3 */ assembly hidebysig static 
		bool XC8LbjkwVMhYgI7EFh9 () cil managed noinlining 
	{
		// Method begins at RVA 0x4fc0
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xE8
		           .emitbyte 0xA2
		           .emitbyte 0x5E
		           .emitbyte 0x47
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.1
		IL_000f: ret
	} // end of method IYWM0aVpfC53gw7Uj2::XC8LbjkwVMhYgI7EFh9

	.method /* 060000D4 */ assembly hidebysig static 
		bool hFaIn2kU5EOIHkGZ25y () cil managed noinlining 
	{
		// Method begins at RVA 0x4fd4
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x0E
		           .emitbyte 0xE6
		           .emitbyte 0x18
		           .emitbyte 0x56
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldc.i4.0
		IL_000f: ret
	} // end of method IYWM0aVpfC53gw7Uj2::hFaIn2kU5EOIHkGZ25y

	.method /* 060000D5 */ assembly hidebysig static 
		void GuiroIkIin5pcfkvt6i () cil managed noinlining 
	{
		// Method begins at RVA 0x4fe8
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xFD
		           .emitbyte 0xE9
		           .emitbyte 0x02
		           .emitbyte 0x54
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp() /* 060000DD */
		IL_0014: ret
	} // end of method IYWM0aVpfC53gw7Uj2::GuiroIkIin5pcfkvt6i

	.method /* 060000D6 */ assembly hidebysig static 
		void yKdsr0koNYvoTTkHZa3 (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x5000
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xDE
		           .emitbyte 0xB2
		           .emitbyte 0x29
		           .emitbyte 0x2E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_0017: ret
	} // end of method IYWM0aVpfC53gw7Uj2::yKdsr0koNYvoTTkHZa3

	.method /* 060000D7 */ assembly hidebysig static 
		class [mscorlib]System.Type L8Cyspk4lYHNF6JYFVn (
			valuetype [mscorlib]System.RuntimeTypeHandle ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x501c
		// Header size: 1
		// Code size: 25 (0x19)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xB3
		           .emitbyte 0x54
		           .emitbyte 0x6C
		           .emitbyte 0x3E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ldarg 0
		IL_0013: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle) /* 0A00002E */
		IL_0018: ret
	} // end of method IYWM0aVpfC53gw7Uj2::L8Cyspk4lYHNF6JYFVn

	.method /* 060000D8 */ assembly hidebysig static 
		object r2auXyk0jFbsQVjb6FJ (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x5038
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xEB
		           .emitbyte 0x37
		           .emitbyte 0x7A
		           .emitbyte 0x51
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: callvirt instance class [mscorlib]System.Reflection.Module [mscorlib]System.Reflection.Assembly::get_ManifestModule() /* 0A0000DC */
		IL_0017: ret
	} // end of method IYWM0aVpfC53gw7Uj2::r2auXyk0jFbsQVjb6FJ

} // end of class JCR2L3MAOJkV1ICmY5.IYWM0aVpfC53gw7Uj2

.class /* 02000028 */ private auto ansi beforefieldinit UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80
	extends [mscorlib]System.Object
{
	// Fields
	.field /* 04000027 */ private static bool ffEZ65B3s

	// Methods
	.method /* 060000DD */ assembly hidebysig static 
		void j6U7MLgdAx2Lp () cil managed noinlining 
	{
		// Method begins at RVA 0x5098
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xE7
		           .emitbyte 0x76
		           .emitbyte 0x25
		           .emitbyte 0x38
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: nop
		IL_000f: ret
	} // end of method ffE65Be3sHFoumqO80::j6U7MLgdAx2Lp

	.method /* 060000DE */ public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed noinlining 
	{
		// Method begins at RVA 0x50ac
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0xF2
		           .emitbyte 0x7C
		           .emitbyte 0x4C
		           .emitbyte 0x5E
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg.0
		IL_000f: call void UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80::s1Vdl1ksRgTaYBSTA7v(object) /* 060000E0 */
		IL_0014: ret
	} // end of method ffE65Be3sHFoumqO80::.ctor

	.method /* 060000DF */ private hidebysig specialname rtspecialname static 
		void .cctor () cil managed noinlining 
	{
		// Method begins at RVA 0x50c4
		// Header size: 1
		// Code size: 1 (0x1)
		.maxstack 8

		IL_0000: ret
	} // end of method ffE65Be3sHFoumqO80::.cctor

	.method /* 060000E0 */ assembly hidebysig static 
		void s1Vdl1ksRgTaYBSTA7v (
			object ''
		) cil managed noinlining 
	{
		// Method begins at RVA 0x50c8
		// Header size: 1
		// Code size: 24 (0x18)
		.maxstack 8

		IL_0000: br.s IL_000b
		IL_0002: .emitbyte 0x28
		           .emitbyte 0x97
		           .emitbyte 0xA9
		           .emitbyte 0x08
		           .emitbyte 0x42
		IL_0007: ldnull
		IL_0008: ldc.i4.0
		IL_0009: ldelem.ref
		IL_000a: pop
		IL_000b: ldc.i4.0
		IL_000c: brtrue.s IL_0007
		IL_000e: ldarg 0
		IL_0012: call instance void [mscorlib]System.Object::.ctor() /* 0A000021 */
		IL_0017: ret
	} // end of method ffE65Be3sHFoumqO80::s1Vdl1ksRgTaYBSTA7v

} // end of class UCflwNTe7cPEp3Tm5f.ffE65Be3sHFoumqO80

.class /* 02000029 */ private auto ansi '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'
	extends [mscorlib]System.Object
{
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class /* 0200002A */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=256'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 256

	} // end of class __StaticArrayInitTypeSize=256

	.class /* 0200002B */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=40'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 40

	} // end of class __StaticArrayInitTypeSize=40

	.class /* 0200002C */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=30'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 30

	} // end of class __StaticArrayInitTypeSize=30

	.class /* 0200002D */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=32'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 32

	} // end of class __StaticArrayInitTypeSize=32

	.class /* 0200002E */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=16'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 16

	} // end of class __StaticArrayInitTypeSize=16

	.class /* 0200002F */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=64'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 64

	} // end of class __StaticArrayInitTypeSize=64

	.class /* 02000030 */ nested private explicit ansi sealed '__StaticArrayInitTypeSize=18'
		extends [mscorlib]System.ValueType
	{
		.pack 1
		.size 18

	} // end of class __StaticArrayInitTypeSize=18


	// Fields
	.field /* 04000028 */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=256' '$$method0x6000007-1' at D_0000C000
	.data /* .sdata */ D_0000C000 = bytearray (
		78 a4 6a d7 56 b7 c7 e8 db 70 20 24 ee ce bd c1
		af 0f 7c f5 2a c6 87 47 13 46 30 a8 01 95 46 fd
		d8 98 80 69 af f7 44 8b b1 5b ff ff be d7 5c 89
		22 11 90 6b 93 71 98 fd 8e 43 79 a6 21 08 b4 49
		62 25 1e f6 40 b3 40 c0 51 5a 5e 26 aa c7 b6 e9
		5d 10 2f d6 53 14 44 02 81 e6 a1 d8 c8 fb d3 e7
		e6 cd e1 21 d6 07 37 c3 87 0d d5 f4 ed 14 5a 45
		05 e9 e3 a9 f8 a3 ef fc d9 02 6f 67 8a 4c 2a 8d
		42 39 fa ff 81 f6 71 87 22 61 9d 6d 0c 38 e5 fd
		44 ea be a4 a9 cf de 4b 60 4b bb f6 70 bc bf be
		c6 7e 9b 28 fa 27 a1 ea 85 30 ef d4 05 1d 88 04
		39 d0 d4 d9 e5 99 db e6 f8 7c a2 1f 65 56 ac c4
		44 22 29 f4 97 ff 2a 43 a7 23 94 ab 39 a0 93 fc
		c3 59 5b 65 92 cc 0c 8f 7d f4 ef ff d1 5d 84 85
		4f 7e a8 6f e0 e6 2c fe 14 43 01 a3 a1 11 08 4e
		82 7e 53 f7 35 f2 3a bd bb d2 d7 2a 91 d3 86 eb
	)
	.field /* 04000029 */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=40' '$$method0x6000020-1' at D_0000C100
	.data /* .sdata */ D_0000C100 = bytearray (
		48 b8 00 00 00 00 00 00 00 00 49 39 40 08 74 0c
		48 b8 00 00 00 00 00 00 00 00 ff e0 48 b8 00 00
		00 00 00 00 00 00 ff e0
	)
	.field /* 0400002A */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=30' '$$method0x6000020-2' at D_0000C128
	.data /* .sdata */ D_0000C128 = bytearray (
		55 8b ec 8b 45 10 81 78 04 7d 1d ea 0c 74 07 b8
		b6 b1 4a 06 eb 05 b8 b6 92 40 0c 5d ff e0
	)
	.field /* 0400002B */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=32' '$$method0x600002a-1' at D_0000C146
	.data /* .sdata */ D_0000C146 = bytearray (
		7b 05 4a 0c f4 9c dd 9a 79 dd b7 29 79 41 09 2b
		43 51 17 2b 4a 3f 40 17 5f b9 e2 f4 2d c2 d3 2b
	)
	.field /* 0400002C */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=16' '$$method0x600002a-2' at D_0000C166
	.data /* .sdata */ D_0000C166 = bytearray (
		75 fe 29 79 41 34 09 2b dd 9a 0c 36 44 f1 44 42
	)
	.field /* 0400002D */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=32' '$$method0x6000039-1' at D_0000C176
	.data /* .sdata */ D_0000C176 = bytearray (
		52 66 68 6e 20 4d 18 22 76 b5 33 11 12 33 0c 6d
		0a 20 4d 18 22 9e a1 29 61 1c 76 b5 05 19 01 58
	)
	.field /* 0400002E */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=64' '$$method0x600005f-1' at D_0000C196
	.data /* .sdata */ D_0000C196 = bytearray (
		04 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00
		02 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00
		03 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00
		02 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00
	)
	.field /* 0400002F */ assembly static valuetype '<PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}'/'__StaticArrayInitTypeSize=18' '$$method0x600027a-1' at D_0000C1D6
	.data /* .sdata */ D_0000C1D6 = bytearray (
		30 20 30 0c 06 08 2a 86 48 86 f7 0d 02 00 05 00
		04 10
	)

} // end of class <PrivateImplementationDetails>{13807139-3F14-47B5-B987-024BC70ADDE2}

