using System.Windows;
using NUnit.Framework;
using MyMessanger;

namespace MyMessanger.Tests
{
    [TestFixture]
    public class LoginPageTests
    {
        [Test]
        public void TestLoginWithValidCredentials()
        {
            // Создайте LoginPage и проверьте, что после ввода правильных данных переход происходит на ChatPage
            LoginPage loginPage = new LoginPage();
            bool success = loginPage.Login("ValidUsername", "ValidPassword");

            Assert.IsTrue(success);
            Assert.IsInstanceOf<ChatPage>(loginPage.NavigationService.Content);
        }

        [Test]
        public void TestLoginWithInvalidCredentials()
        {
            // Создайте LoginPage и проверьте, что после ввода неправильных данных ошибка отображается
            LoginPage loginPage = new LoginPage();
            bool success = loginPage.Login("InvalidUsername", "InvalidPassword");

            Assert.IsFalse(success);
            Assert.AreEqual(Visibility.Visible, loginPage.ErrorMessage.Visibility);
        }
    }
}