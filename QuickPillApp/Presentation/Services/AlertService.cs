using QuickPillApp.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Presentation.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message, string cancel = "OK")
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
                await ShowAlertAsync(title, message, cancel)
            );
        }
        public void ShowConfirmation(string title, string message, Action<bool> callback,
                                     string accept = "Yes", string cancel = "No")
        {
            Application.Current.MainPage.Dispatcher.Dispatch(async () =>
            {
                bool answer = await ShowConfirmationAsync(title, message, accept, cancel);
                callback(answer);
            });
        }

        private Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        private Task<bool> ShowConfirmationAsync(string title, string message, string accept = "Yes", string cancel = "No")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
