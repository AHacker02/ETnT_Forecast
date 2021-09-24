using System;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IAsyncRunner
    {
        Task Run<T>(Action<T> action);
    }
}