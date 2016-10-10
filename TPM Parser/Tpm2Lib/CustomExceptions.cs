﻿/*++

Copyright (c) 2010-2015 Microsoft Corporation
Microsoft Confidential

*/
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Tpm2Lib
{
    /// <summary>
    /// Represents exceptions generated by the TSS.Net library. Encapsulates call stack
    /// at the point where the exception was generated. Serves as the base for the
    /// TpmException class.
    /// </summary>
    public class TssException : Exception
    {
#if !TSS_NO_STACK && !WINDOWS_UWP
        public StackTrace CallerStack;
#endif

        public TssException(string message)
            : base(message)
        {
#if !TSS_NO_STACK && !WINDOWS_UWP
            CallerStack = new StackTrace(true);
#endif
        }
    }

    /// <summary>
    /// Represents and encapsulates TPM error codes. Generally TSS.Net propagates TPM
    /// errors as exceptions, although this behavior can be overridden with _ExpectError(),
    /// _AllowError(), etc.
    /// </summary>
    public class TpmException : TssException
    {
        public string ErrorString = "None";
        public TpmRc RawResponse = TpmRc.Success;

        public TpmException(TpmRc rawResponse, string errorDescription)
            : base(errorDescription)
        {
            ErrorString = TpmErrorHelpers.ErrorNumber(rawResponse).ToString();
            RawResponse = rawResponse;
        }
    }

    public class TpmFailure : Exception
    {
        public TpmFailure(string s) : base(s)
        {

        }
    }
}
