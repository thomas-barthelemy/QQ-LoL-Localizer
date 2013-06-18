using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace QQ_LoL_Localizer.Commands
{
    public abstract class BaseAppCommand : ICommand
    {
        protected ListView Files;

        public bool CanExecute(object parameter)
        {
            return !Helper.IsWorking;
        }

        public virtual void Execute(object parameter)
        {
            if (parameter == null) return;

            var listView = parameter as ListView;
            if (listView == null) return;

            Files = listView;
        }

        public event EventHandler CanExecuteChanged;
    }
}
