using System;

namespace NScripto.Verification
{
    public interface IVerificationError
    {
        Type Type { get; }
        string Message { get; }
    }
}