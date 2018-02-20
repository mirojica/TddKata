using System;
using System.Collections.Generic;
using System.Linq;

namespace TddKata.Basic.PasswordVerifierKata
{
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