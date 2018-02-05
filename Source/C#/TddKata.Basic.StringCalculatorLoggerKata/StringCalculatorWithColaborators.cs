using System;

namespace TddKata.Basic.StringCalculatorWithColaboratorsKata
{
    public class StringCalculatorWithColaborators
    {
        private readonly StringCalculator _calculator;
        private readonly ILogger _logger;
        private readonly IWebService _webService;
        private readonly IOutput _output;

        public StringCalculatorWithColaborators(ILogger logger, IWebService webService, IOutput output)
        {
            _calculator = new StringCalculator();
            _logger = logger;
            _webService = webService;
            _output = output;
        }

        public void Add(string numbers)
        {
            var result = _calculator.Add(numbers).ToString();
            try
            {
                _logger.Write(result);
                _output.Write(result);
            }
            catch (Exception ex)
            {
                _webService.Notify(ex.Message);
            }
        }
    }

    public interface IWebService
    {
        void Notify(string exceptionMessage);
    }

    public interface ILogger
    {
        void Write(string message);
    }
}