from pathlib import Path

path = Path(__file__).parents[1] / "projects-auto/DigisatServer.Infrastructure.Crosscutting.ArquivoOfx/source/ArquivoOfxService.cs"
source = path.read_text(encoding="utf-8")
start = source.index("\t\t\t\t\t\tuint num = <PrivateImplementationDetails>.ComputeStringHash(text);")
end = source.index("\n\t\t\t\t\t}", start)
replacement = '''\t\t\t\t\t\tif (text == "DTPOSTED")
\t\t\t\t\t\t{
\t\t\t\t\t\t\tif (transaction != null) transaction.Date = ArquivoOfxService.ConvertOfxDateToDateTime(xmlTextReader.Value, extract);
\t\t\t\t\t\t}
\t\t\t\t\t\telse if (text == "ACCTTYPE") { bankAccount.Type = xmlTextReader.Value; flag2 = true; }
\t\t\t\t\t\telse if (text == "LANGUAGE") { headerExtract.Language = xmlTextReader.Value; flag = true; }
\t\t\t\t\t\telse if (text == "NAME" || text == "MEMO")
\t\t\t\t\t\t{
\t\t\t\t\t\t\tif (transaction != null) transaction.Description = string.IsNullOrEmpty(xmlTextReader.Value) ? "" : xmlTextReader.Value.Trim().Replace("  ", " ");
\t\t\t\t\t\t}
\t\t\t\t\t\telse if (text == "FITID") { if (transaction != null) transaction.Id = xmlTextReader.Value; }
\t\t\t\t\t\telse if (text == "BANKID") { bankAccount.Bank = new Bank(ArquivoOfxService.GetBankId(xmlTextReader.Value, extract), ""); flag2 = true; }
\t\t\t\t\t\telse if (text == "TRNAMT") { if (transaction != null) transaction.TransactionValue = ArquivoOfxService.GetTransactionValue(xmlTextReader.Value, extract); }
\t\t\t\t\t\telse if (text == "DTSTART") extract.InitialDate = ArquivoOfxService.ConvertOfxDateToDateTime(xmlTextReader.Value, extract);
\t\t\t\t\t\telse if (text == "ORG") { headerExtract.BankName = xmlTextReader.Value; flag = true; }
\t\t\t\t\t\telse if (text == "DTSERVER") { headerExtract.ServerDate = ArquivoOfxService.ConvertOfxDateToDateTime(xmlTextReader.Value, extract); flag = true; }
\t\t\t\t\t\telse if (text == "BRANCHID") { bankAccount.AgencyCode = xmlTextReader.Value; flag2 = true; }
\t\t\t\t\t\telse if (text == "TRNTYPE") { if (transaction != null) transaction.Type = xmlTextReader.Value; }
\t\t\t\t\t\telse if (text == "ACCTID") { bankAccount.AccountCode = xmlTextReader.Value; flag2 = true; }
\t\t\t\t\t\telse if (text == "DTEND") extract.FinalDate = ArquivoOfxService.ConvertOfxDateToDateTime(xmlTextReader.Value, extract);'''
path.write_text(source[:start] + replacement + source[end:], encoding="utf-8")
print("Replaced generated hash dispatch with equivalent OFX tag comparisons.")
