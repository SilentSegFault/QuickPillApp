using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuickPillApp.Presentation.Interfaces
{
    public interface IViewModel : INotifyPropertyChanged
    {
        bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null);
    }
}
