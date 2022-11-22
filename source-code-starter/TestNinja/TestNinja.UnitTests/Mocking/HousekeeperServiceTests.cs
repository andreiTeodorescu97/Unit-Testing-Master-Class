using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using TestNinja.Mocking.Mocks;
using static TestNinja.Mocking.HousekeeperService;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private List<Housekeeper> _list;
        private Mock<IFileSaver> _fileSaver;
        private Mock<IEmailService> _emailService;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private HousekeeperService _service;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _list = new List<Housekeeper>();
            _list.Add(new Housekeeper
            {
                Email = "one@one.com",
                Oid = 1,
                FullName = "Andrew",
                StatementEmailBody = "Hello Sir!"
            });
            _list.Add(new Housekeeper
            {
                Email = null,
                Oid = 2,
                FullName = "Bogdan",
                StatementEmailBody = "Hello Sir!"
            });
            _list.Add(new Housekeeper
            {
                Email = "",
                Oid = 3,
                FullName = "Alex",
                StatementEmailBody = "Hello Sir!"
            });
            _unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(_list.AsQueryable());

            _fileSaver = new Mock<IFileSaver>();
            _emailService = new Mock<IEmailService>();
            _xtraMessageBox = new Mock<IXtraMessageBox>();
            _service = new HousekeeperService(
                _fileSaver.Object,
                _emailService.Object,
                _unitOfWork.Object,
                _xtraMessageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenEmailIsNullOrEmpty_GenerateStatementIsNotCalled()
        {
            _service.SendStatementEmails(new DateTime(2022, 11, 01));

            _fileSaver.Verify(fs => fs.SaveHousekeeperStatementReport(_list[1].Oid, _list[1].FullName, new DateTime(2022, 11, 01)), Times.Never());
            _fileSaver.Verify(fs => fs.SaveHousekeeperStatementReport(_list[2].Oid, _list[2].FullName, new DateTime(2022, 11, 01)), Times.Never());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(new DateTime(2022, 11, 01));

            _fileSaver.Verify(fs => fs.SaveHousekeeperStatementReport(_list[0].Oid, _list[0].FullName, new DateTime(2022, 11, 01)));
        }

        [Test]
        public void SendStatementEmails_WhenReportNameIsEmptyOrNull_SendEmailIsNotCalled()
        {
            _fileSaver.Setup(f => f.SaveHousekeeperStatementReport(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("");

            _service.SendStatementEmails(new DateTime(2022, 11, 01));

            _emailService.Verify(fs => fs.SendEmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailHouseKeeper()
        {
            _fileSaver.Setup(f => f.SaveHousekeeperStatementReport(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("thisIsTheFileName");

            _service.SendStatementEmails(new DateTime(2022, 11, 01));

            _emailService.Verify(fs => fs.SendEmailFile(_list[0].Email, _list[0].StatementEmailBody, "thisIsTheFileName", It.IsAny<string>()));
        }

        [Test]
        public void SendStatementEmails_ThrowsException()
        {
            _fileSaver.Setup(f => f.SaveHousekeeperStatementReport(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns("thisIsTheFileName");

            _emailService.Setup(e => e.SendEmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

            _service.SendStatementEmails(new DateTime(2022, 11, 01));

            _xtraMessageBox.Verify(x => x.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }
    }

}
