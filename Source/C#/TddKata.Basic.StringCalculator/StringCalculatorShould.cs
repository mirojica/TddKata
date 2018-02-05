using System;
using FluentAssertions;
using Xunit;

namespace TddKata.Basic.StringCalculatorKata
{
    public class StringCalculatorAddShould
    {
        [Fact]
        public void Test()
        {
            true.Should().BeTrue();
        }

        [Fact]
        public void ReturnZero_WhenEmptyStringIsProvided()
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add("");

            actualResult.Should().Be(0);
        }

        [Theory]
        [InlineData("5", 5)]
        [InlineData("44", 44)]
        [InlineData("957", 957)]
        public void ReturnSameNumberAsProvided_WhenSingleNumberIsProvided(string number, int expectedResult)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(number);

            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("10,15", 25)]
        [InlineData("100,15", 115)]
        [InlineData("753,26", 779)]
        public void ReturnSum_WhenTwoNumbersAreProvided(string numbers, int expectedResult)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbers);

            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("50,20,10", 80)]
        [InlineData("50,20,10,150,30", 260)]
        public void ReturnSum_WhenManyNumbersAreProvided(string numbers, int expectedResult)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbers);

            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("1\n2,3")]
        [InlineData("1,2\n3")]
        [InlineData("1\n2\n3")]
        public void ReturnSum_WhenNewLineIsUsedAsADelimiter(string numbersWithNewLineDelimiter)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbersWithNewLineDelimiter);

            actualResult.Should().Be(6);
        }

        [Theory]
        [InlineData("//;\n1;2;3", 6)]
        [InlineData("//*\n11*27*36", 74)]
        public void ReturnSum_WhenCustomeDelimiterIsUsed(string numbersWithCustomDelimiter, int expectedResult)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbersWithCustomDelimiter);

            actualResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData("-1,2,3", -1)]
        [InlineData("-1\n2,3", -1)]
        [InlineData("//;\n-1;2;3", -1)]
        [InlineData("1,-2,3", -2)]
        [InlineData("1\n2,-3", -3)]
        [InlineData("//;\n1;-2;3", -2)]
        public void ThrowException_WhenNegativeNumberIsProvided(string numbersThatContainsNegativeValue, int expectedNegativeValue)
        {
            var stringCalculator = new StringCalculator();

            Action action = () => stringCalculator.Add(numbersThatContainsNegativeValue);

            action.ShouldThrow<Exception>().And.Message.Should().Be($"negatives not allowed: {expectedNegativeValue}");
        }

        [Theory]
        [InlineData("-1,2,-3", "-1, -3")]
        [InlineData("-1\n-2,3", "-1, -2")]
        [InlineData("//;\n-1;-2;-3", "-1, -2, -3")]
        public void ThrowException_WhenNegativeNumbersAreProvided(string numbersThatContainsNegativeValue, string expectedNegativeValues)
        {
            var stringCalculator = new StringCalculator();

            Action action = () => stringCalculator.Add(numbersThatContainsNegativeValue);

            action.ShouldThrow<Exception>().And.Message.Should().Be($"negatives not allowed: {expectedNegativeValues}");
        }

        [Theory]
        [InlineData("2,1001", 2)]
        [InlineData("//*\n2*1001", 2)]
        [InlineData("2000,1001", 0)]
        [InlineData("//*\n2000*1001", 0)]
        public void ReturnSumIgnoringValuesBiggerThan1000_WhenProvidedNumbersContainsValuesBiggerThan1000(string numbersThatContainsValuesBiggerThanThousend, int expectedResult)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbersThatContainsValuesBiggerThanThousend);

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void ReturnSum_WhenNumbersWithAnyLenghtCustomeDelimiterIsProvided()
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add("//[***]\n10***20***30");

            actualResult.Should().Be(60);
        }

        [Theory]
        [InlineData("//[*][%]\n1*2%3")]
        [InlineData("//[###][$$]\n1###2$$3")]
        [InlineData("//[*][@@@]\n1@@@2*3")]
        public void ReturnSum_WhenNumbersWithMultipleCustomDelimitersAreProvided(string numbersWithMultipleCustomDelimiter)
        {
            var stringCalculator = new StringCalculator();

            var actualResult = stringCalculator.Add(numbersWithMultipleCustomDelimiter);

            actualResult.Should().Be(6);
        }
    }
}
