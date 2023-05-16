using System;
namespace VoilaMobileApp.Src.Utilts.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }

    }
}

