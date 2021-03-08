using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWithbasicAuthentication.Common
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; set; }

        public CustomException(string _errorCode, string errorMessage):base(errorMessage)
        {
            ErrorCode = _errorCode;
        }

    }
}
