using FluentAssertions;
using Moq;
using PasswordManager.Domain;
using PassworManager.App.Interface;
using PassworManager.App.Manager;

namespace PasswordManager.Test
{
    public class UnitTest1
    {
        //Arrange
        //Act
        //Assert
        [Fact]
        public void Test1()
        {
            Password pass=new Password{ UserName="dzidek",Website="google",UserPassword="dupaaaa"};
            var mock = new Mock<IService<Password>>();
            mock.Setup(s=>s.GetPasswordByName("google")).Returns(pass);
            var manager = new PasswordManagerApp(mock.Object);

            var returnedPassword=manager.GetPasswordByName(pass.Website);

            //Assert.Equal(pass, returnedPassword);
            returnedPassword.Should().BeSameAs(pass);
            returnedPassword.Should().NotBeNull();
            returnedPassword.Should().BeOfType(typeof(Password));
        }
       
        [Fact]
        public void CanGetAllPasswords()
        {
            var mock =new Mock<IService<Password>>();
            mock.Setup(x => x.GetAllPasswords());

            var manager = new PasswordManagerApp(mock.Object);
            manager.GetAllPasswords();
            mock.Verify(m => m.GetAllPasswords());
        }
    }
}