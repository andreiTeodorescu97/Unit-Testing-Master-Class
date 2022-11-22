﻿using System;
using TestNinja.Mocking.Mocks;

namespace TestNinja.Mocking
{
    public class HousekeeperService
    {
        private readonly IFileSaver _fileSaver;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IXtraMessageBox _xtraMessageBox;

        public HousekeeperService(IFileSaver fileSaver, IEmailService emailService, IUnitOfWork unitOfWork, IXtraMessageBox xtraMessageBox)
        {
            _fileSaver = fileSaver;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _xtraMessageBox = xtraMessageBox;
        }

        public void SendStatementEmails(DateTime statementDate)
        {
            var housekeepers = _unitOfWork.Query<Housekeeper>();

            foreach (var housekeeper in housekeepers)
            {
                if (string.IsNullOrWhiteSpace(housekeeper.Email))
                    continue;

                var statementFilename = _fileSaver.SaveHousekeeperStatementReport(housekeeper.Oid, housekeeper.FullName, statementDate);

                if (string.IsNullOrWhiteSpace(statementFilename))
                    continue;

                var emailAddress = housekeeper.Email;
                var emailBody = housekeeper.StatementEmailBody;

                try
                {
                    _emailService.SendEmailFile(emailAddress, emailBody, statementFilename,
                        string.Format("Sandpiper Statement {0:yyyy-MM} {1}", statementDate, housekeeper.FullName));
                }
                catch (Exception e)
                {
                    _xtraMessageBox.Show(e.Message, string.Format("Email failure: {0}", emailAddress),
                        MessageBoxButtons.OK);
                }
            }
        }

        public enum MessageBoxButtons
        {
            OK
        }

        public interface IXtraMessageBox
        {
            void Show(string s, string housekeeperStatements, MessageBoxButtons ok);
        }

        public class XtraMessageBox : IXtraMessageBox
        {
            public void Show(string s, string housekeeperStatements, MessageBoxButtons ok)
            {
            }
        }

        public class MainForm
        {
            public bool HousekeeperStatementsSending { get; set; }
        }

        public class DateForm
        {
            public DateForm(string statementDate, object endOfLastMonth)
            {
            }

            public DateTime Date { get; set; }

            public DialogResult ShowDialog()
            {
                return DialogResult.Abort;
            }
        }

        public enum DialogResult
        {
            Abort,
            OK
        }

        public class SystemSettingsHelper
        {
            public static string EmailSmtpHost { get; set; }
            public static int EmailPort { get; set; }
            public static string EmailUsername { get; set; }
            public static string EmailPassword { get; set; }
            public static string EmailFromEmail { get; set; }
            public static string EmailFromName { get; set; }
        }

        public class Housekeeper
        {
            public string Email { get; set; }
            public int Oid { get; set; }
            public string FullName { get; set; }
            public string StatementEmailBody { get; set; }
        }

        public class HousekeeperStatementReport
        {
            public HousekeeperStatementReport(int housekeeperOid, DateTime statementDate)
            {
            }

            public bool HasData { get; set; }

            public void CreateDocument()
            {
            }

            public void ExportToPdf(string filename)
            {
            }
        }
    }
}