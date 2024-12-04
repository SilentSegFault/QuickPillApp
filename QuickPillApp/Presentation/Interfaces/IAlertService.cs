using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Presentation.Interfaces
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message, string cancel = "OK");
        void ShowConfirmation(string title, string message, Action<bool> callback,
                              string accept = "Yes", string cancel = "No");
    }
}
