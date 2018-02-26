using System;
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
        public void ThrowException_WhenMinimumConditionsAreNotSatisfiedAndOneOfThemIsPasswordMinimumLenght(string passwordWhichIsNotLongerThanEightCharactersAndOneMoreFailingCondition)
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
        public void ThrowException_WhenMinimumConditionsAreNotSatisfiedAndOneOfThemIsUppercaseLetterCondition(string passwordWhichDoesntContainsUpercaseCharacterAndOneMoreFailingCondition)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsUpercaseCharacterAndOneMoreFailingCondition);

            action.ShouldThrow<Exception>().Which.Message.Should().Contain("Password must have at least 1 uppercase letter.");
        }

        [Theory]
        [InlineData("AAAAAAAA")]
        [InlineData("AAAAAAAAAA")]
        [InlineData("111111111")]
        public void ThrowsException_WhenPasswordDoesntSatisfiedLowercaseCondition(string passwordWhichDoesntContainsLowercaseCharacter)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsLowercaseCharacter);

            action.ShouldThrow<Exception>().Which.Message.Should().Be("Password must have at least 1 lowercase letter.");
        }

        [Theory]
        [InlineData("aaaaaaaaa")]
        [InlineData("aaaaaaa")]
        [InlineData("aaaaAAAA")]
        public void ThrowsException_WhenPasswordDoesntSatisfiedNumberConditionAndAtLeastOneMoreConditionIsNotSatisfied(string passwordWhichDoesntContainsNumberAndOneMoreFailingCondition)
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify(passwordWhichDoesntContainsNumberAndOneMoreFailingCondition);

            action.ShouldThrow<Exception>().Which.Message.Should().Contain("Password must have at least 1 number.");
        }

        [Fact]
        public void VerifyThatPasswordIsCorrect_WhenAtLeastThreeOptionalConditionsAreTrue()
        {
            var passwordVerifier = new PasswordVerifier();

            Action action = () => passwordVerifier.Verify("EEEeee11");

            action.ShouldNotThrow<Exception>();
        }
    }
}