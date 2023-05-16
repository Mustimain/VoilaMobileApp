using System;
namespace VoilaMobileApp.Src.Utilts.Results
{
    public interface IResult
    {
        bool Status { get; }
        string Message { get; }

    }
}

