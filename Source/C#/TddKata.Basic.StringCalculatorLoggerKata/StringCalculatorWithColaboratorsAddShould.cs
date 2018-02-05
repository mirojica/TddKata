using Moq;
using Xunit;

namespace TddKata.Basic.StringCalculatorWithColaboratorsKata
{
    public class StringCalculatorWithColaboratorsAddShould
    {
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IWebService> _webServiceMock;
        private readonly Mock<IOutput> _outputMock;

        public StringCalculatorWithColaboratorsAddShould()
        {
            _loggerMock = new Mock<ILogger>();
            _webServiceMock = new Mock<IWebService>();
            _outputMock = new Mock<IOutput>();
        }

        [Theory]
        [InlineData("1,2,3", "6")]
        [InlineData("10,20,30", "60")]
        public void LogResult(string numbers, string expectedResult)
        {
            StringCalculatorWithColaborators stringCalculatorWithColaborators = AStringCalculatorWithColaborators
                                                                    .With(_loggerMock);

            stringCalculatorWithColaborators.Add(numbers);

            _loggerMock.Verify(logger => logger.Write(expectedResult));
        }

        [Fact]
        public void NotifyWebService_WhenLoggerTrowsException()
        {
            StringCalculatorWithColaborators stringCalculatorWithColaborators = AStringCalculatorWithColaborators
                                                                    .With(_webServiceMock)
                                                                    .With(_loggerMock)
                                                                    .ThatThrowsExceptionWithMessage("exception");

            stringCalculatorWithColaborators.Add("1,2,3");

            _webServiceMock.Verify(webService => webService.Notify("exception"));
        }

        [Fact]
        public void GenerateOutput_WhenGetResultOfCalculation()
        {
            StringCalculatorWithColaborators stringCalculator = AStringCalculatorWithColaborators
                                                            .With(_outputMock);
                                                        
            stringCalculator.Add("1,2,3");

            _outputMock.Verify(output => output.Write("6"));
        }

        public StringCalculatorWithColaboratorsBuilder AStringCalculatorWithColaborators =>
            new StringCalculatorWithColaboratorsBuilder();
    }

    public interface IOutput
    {
        void Write(string outputResult);
    }
}