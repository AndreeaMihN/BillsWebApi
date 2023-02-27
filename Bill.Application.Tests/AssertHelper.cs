using Bill.Application.Features.Users.Commands.CreateUser;
using Bill.Domain.Users;
using Xunit;

namespace Bill.Application.Tests
{
    public static class AssertHelper
    {
        public static bool AssertThatUserIsSameAs(this User actualUser, User expectedUser)
        {
            Assert.Multiple(() =>
            {
                Assert.Equal(actualUser.Id, expectedUser.Id);
                Assert.Equal(actualUser.UserName, expectedUser.UserName);
                Assert.Equal(actualUser.Email, expectedUser.Email);
                Assert.Equal(actualUser.IsActive, expectedUser.IsActive);
                Assert.Equal(actualUser.PersonalIdentificationNumber, expectedUser.PersonalIdentificationNumber);
            });

            return true;
        }

        public static bool AssertThatUserIsSameAs(this CreateUserDto actualUser, CreateUserDto expectedUser)
        {
            Assert.Multiple(() =>
            {
                Assert.Equal(actualUser.FirstName, expectedUser.FirstName);
                Assert.Equal(actualUser.LastName, expectedUser.LastName);
                Assert.Equal(actualUser.Email, expectedUser.Email);
                Assert.Equal(actualUser.PersonalIdentificationNumber, expectedUser.PersonalIdentificationNumber);
            });

            return true;
        }
    }
}