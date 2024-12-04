using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Presentation.Interfaces
{
    public abstract class ViewModelBase : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(property, value))
            {
                return false;
            }

            property = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public void LogDebug(string msg)
        {
            Debug.WriteLine($"[{this.GetType().Name}] {msg}");
        }
    }
}
