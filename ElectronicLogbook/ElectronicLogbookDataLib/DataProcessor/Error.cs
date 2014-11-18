using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElectronicLogbookDataLib.DataProcessor
{
    class Error
    {
        public Boolean mErrorCode { get; private set; }
        public string mErrorInfo { get; private set; }

        public Error(string aErrorInfo) 
        {
            mErrorCode = false;
            mErrorInfo = aErrorInfo;
        }

        public Error() 
        {
            mErrorCode = true;
            mErrorInfo = string.Empty;
        }

        public static Error OK = new Error();
    }
}
