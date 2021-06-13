using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XApplication = Xamarin.Forms.Application;

#nullable disable

namespace BetterPractice.BetterMvvm.Services
{
    public class DefaultAlertService : IAlertService
    {
        protected Page MainPage => XApplication.Current.MainPage;

        protected Page TopPage => MainPage?.Navigation.ModalStack.LastOrDefault() ?? MainPage;

        public Task ShowAlert(string message, string title = null, string action = "Dismiss")
        {
            return MainThread.InvokeOnMainThreadAsync(() => TopPage.DisplayAlert(title, message, action));
        }

        public Task<bool> ShowConfirmation(string message, string title = null, string confirmationAction = "Confirm", string cancelationAction = "Abort")
        {
            return MainThread.InvokeOnMainThreadAsync(() => TopPage.DisplayAlert(title, message, confirmationAction, cancelationAction));
        }

        public Task<string> ShowPrompt(string title, string destructiveAction, string cancelationAction = "Dismiss", params string[] buttons)
        {
            return MainThread.InvokeOnMainThreadAsync(() => TopPage.DisplayActionSheet(title, cancelationAction, destructiveAction, buttons));
        }
    }
}
