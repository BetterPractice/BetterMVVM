using System;
namespace BetterPractice.BetterMvvm.ViewModels
{
    public interface IValidated
    {
        bool IsValid { get; }
    }
}
