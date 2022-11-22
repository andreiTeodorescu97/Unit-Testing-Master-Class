using System;
using System.IO;
using static TestNinja.Mocking.HousekeeperService;

namespace TestNinja.Mocking.Mocks
{
    public interface IFileSaver
    {
        string SaveHousekeeperStatementReport(int housekeeperOid, string housekeeperName, DateTime statementDate);
    }

    public class FileSaver : IFileSaver
    {
        public string SaveHousekeeperStatementReport(int housekeeperOid, string housekeeperName, DateTime statementDate)
        {
            var report = new HousekeeperStatementReport(housekeeperOid, statementDate);

            if (!report.HasData)
                return string.Empty;

            report.CreateDocument();

            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                string.Format("Sandpiper Statement {0:yyyy-MM} {1}.pdf", statementDate, housekeeperName));

            report.ExportToPdf(filename);

            return filename;
        }
    }
}
