using System;
using Moq;

namespace TddKata.Basic.StringCalculatorWithColaboratorsKata
{
    public class StringCalculatorWithColaboratorsBuilder
    {
        private Mock<IWebService> _webServiceMock;
        private Mock<ILogger> _loggerMock;
        private Mock<IOutput> _outputMock;

        public StringCalculatorWithColaboratorsBuilder()
        {
            _loggerMock = new Mock<ILogger>();
            _outputMock = new Mock<IOutput>();
            _webServiceMock = new Mock<IWebService>();
        }

        public StringCalculatorWithColaboratorsBuilder With(Mock<IWebService> webServiceMock)
        {
            _webServiceMock = webServiceMock;
            return this;
        }

        internal StringCalculatorWithColaboratorsBuilder With(Mock<ILogger> loggerMock)
        {
            _loggerMock = loggerMock;
            return this;
        }

        public StringCalculatorWithColaboratorsBuilder ThatThrowsExceptionWithMessage(string message)
        {
            _loggerMock.Setup(logger => logger.Write("6")).Throws(new Exception(message));
            return this;
        }

        public StringCalculatorWithColaboratorsBuilder With(Mock<IOutput> outputMock)
        {
            _outputMock = outputMock;
            return this;
        }

        public static implicit operator StringCalculatorWithColaborators(StringCalculatorWithColaboratorsBuilder builder)
        {
            return new StringCalculatorWithColaborators(builder._loggerMock.Object, builder._webServiceMock.Object, builder._outputMock.Object);
        }
    }
}