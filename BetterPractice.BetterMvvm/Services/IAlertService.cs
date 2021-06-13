using System;
using System.Threading.Tasks;

namespace BetterPractice.BetterMvvm.Services
{
    public interface IAlertService
    {
        Task ShowAlert(string message, string title = null, string action = "Dismiss");
        Task<bool> ShowConfirmation(string message, string title = null, string confirmationAction = "Confirm", string cancelationAction = "Abort");
        Task<string> ShowPrompt(string title, string destructiveAction, string cancelationAction = "Dismiss", params string[] buttons);
    }

    
}
