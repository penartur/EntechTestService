using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntechTestService.Services.Validation.UnitTests
{
    [TestClass]
    public class PhoneValidatorTest
    {
        private readonly PhoneValidator phoneValidator = new PhoneValidator();

        [TestMethod]
        public void TestSimplePhoneCorrect()
        {
            Assert.IsTrue(phoneValidator.IsPhoneCorrect("+61234567890"));
        }

        [TestMethod]
        public void TestSimplePhoneWithSpacesCorrect()
        {
            Assert.IsTrue(phoneValidator.IsPhoneCorrect("+61 2 3456 7890"));
        }

        [TestMethod]
        public void TestPhoneWithoutIcpIncorrect()
        {
            Assert.IsFalse(phoneValidator.IsPhoneCorrect("61234567890"));
        }

        [TestMethod]
        public void TestPhoneWithLettersIncorrect()
        {
            Assert.IsFalse(phoneValidator.IsPhoneCorrect("+61234567890abc"));
        }

        [TestMethod]
        public void TestEmptyPhoneIncorrect()
        {
            Assert.IsFalse(phoneValidator.IsPhoneCorrect("+"));
        }
    }
}
