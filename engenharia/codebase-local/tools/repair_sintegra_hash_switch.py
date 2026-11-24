from pathlib import Path

path = Path(__file__).parents[1] / "projects-auto/DigisatServer.Infrastructure.Crosscutting.Sintegra/source/Classes/Registro60ADefault.cs"
source = path.read_text(encoding="utf-8")
start = source.index("\t\t\t\tuint num = <PrivateImplementationDetails>.ComputeStringHash(text2);")
end = source.index("\n\t\t\t\tIL_0125:", start)
replacement = '''\t\t\t\tif (text2 == "CAN-S" || text2 == "CAN-T") return "CANC";
\t\t\t\tif (text2 == "I1") return "I";
\t\t\t\tif (text2 == "S") return "ISS";
\t\t\t\tif (text2 == "N1") return "N";
\t\t\t\tif (text2 == "DT") return "DESC";
\t\t\t\tif (text2 == "F1") return "F";'''
source = source[:start] + replacement + source[end:]
source = source.replace("\t\t\t\tIL_0125:\n", "")
path.write_text(source, encoding="utf-8")
print("Replaced generated hash dispatch with the same string cases.")
