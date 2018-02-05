using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.PasswordVerifierKata
{
    public class PasswordVerifierVerifyShould
    {
        [Theory]
        [InlineData("aaaaaaaa")]
        [InlineData("1111111a")]
        [InlineData("AAAAAAAa")]
        public void ThrowException_WhenPasswordIsNotLongerThanEightAndAtLeastOneMoreConditionIsNotSatisfied(string passwordWhichIsNotLongerThanEightCharactersAndOneMoreFailingCondition)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichIsNotLongerThanEightCharactersAndOneMoreFailingCondition);

            action.ShouldThrow<Exception>().Which.Message.Should().Contain("Password must be longer than 8 characters.");
        }

        [Fact]
        public void VerifyThatPasswordIsNotNull()
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(null);

            action.ShouldThrow<Exception>().Which.Message.Should().Be("Password can't be null.");
        }

        [Theory]
        [InlineData("aaaaaaaa")]
        [InlineData("aaaaaaa1")]
        [InlineData("111111a")]
        public void ThrowsException_WhenPasswordDoesntHaveAtLeastOneUppercaseLetterAndAtLeastOneMoreConditionIsNotSatisfied(string passwordWhichDoesntContainsUpercaseCharacterAndOneMoreFailingCondition)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsUpercaseCharacterAndOneMoreFailingCondition);

            action.ShouldThrow<Exception>().Which.Message.Should().Contain("Password must have at least 1 uppercase letter.");
        }

        [Theory]
        [InlineData("AAAAAAAA")]
        [InlineData("AAAAAAAAAA")]
        [InlineData("111111111")]
        public void ThrowsException_WhenPasswordDoesntHaveAtLeastOneLowercase(string passwordWhichDoesntContainsLowercaseCharacter)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsLowercaseCharacter);

            action.ShouldThrow<Exception>().Which.Message.Should().Be("Password must have at least 1 lowercase letter.");
        }

        [Theory]
        [InlineData("aaaaaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaAAAA")]
        public void ThrowsException_WhenPasswordDoesntHaveAtLeastOneNumberAndAtLeastOneMoreConditionIsNotSatisfied(string passwordWhichDoesntContainsNumberAndOneMoreFailingCondition)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsNumberAndOneMoreFailingCondition);

            action.ShouldThrow<Exception>().Which.Message.Should().Contain("Password must have at least 1 number.");
        }

        [Fact]
        public void VerifyThatPasswordIsCorrect_WhenAtLeastThreeConditionsAreTrue()
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify("EEEeee11");

            action.ShouldNotThrow<Exception>();
        }
    }

    public class PasswordVerifier
    {
        public void Verify(string password)
        {
            var errorMessages = new List<string>();

            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password can't be null.");
            }

            if (password.Equals(password.ToUpper()))
            {
                throw new Exception("Password must have at least 1 lowercase letter.");
            }

            if (password.Length < 9)
            {
                errorMessages.Add("Password must be longer than 8 characters.");
            }

            if (password.Equals(password.ToLower()))
            {
                errorMessages.Add("Password must have at least 1 uppercase letter.");
            }

            if (!password.Any(char.IsDigit))
            {
                errorMessages.Add("Password must have at least 1 number.");
            }

            if (errorMessages.Count > 1)
            {
                throw new Exception(string.Join(" ", errorMessages));
            }
        }
    }
}