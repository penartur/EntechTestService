using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntechTestService.Services.Validation.UnitTests
{
    [TestClass]
    public class EmailValidatorTest
    {
        private readonly EmailValidator emailValidator = new EmailValidator();

        [TestMethod]
        public void TestSimpleEmailCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("test@example.com"));
        }

        [TestMethod]
        public void TestLongEmailCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("test.name@subdomain.example.com"));
        }

        [TestMethod]
        public void TestUnicodeEmailCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("münchen@münchen.example.com"));
        }

        [TestMethod]
        public void TestPunycodeEmailCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("münchen@xn--mnchen-3ya.example.com"));
        }

        [TestMethod]
        public void TestNewGtldCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("test@example.google"));
        }

        [TestMethod]
        public void TestEmailWithNameCorrect()
        {
            Assert.IsTrue(emailValidator.IsEmailCorrect("John Doe <test@example.com>"));
        }

        [TestMethod]
        public void TestEmailWithoutAtIncorrect()
        {
            Assert.IsFalse(emailValidator.IsEmailCorrect("test.email"));
        }

        [TestMethod]
        public void TestEmailWithoutDomainIncorrect()
        {
            Assert.IsFalse(emailValidator.IsEmailCorrect("test.name@"));
        }

        [TestMethod]
        public void TestEmailWithoutNameIncorrect()
        {
            Assert.IsFalse(emailValidator.IsEmailCorrect("@example.com"));
        }

        [TestMethod]
        public void TestEmailWithoutSldIncorrect()
        {
            Assert.IsFalse(emailValidator.IsEmailCorrect("test@com"));
        }
    }
}
