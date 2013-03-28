using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace QQ_LoL_Localizer.Commands
{
    abstract class BaseAppCommand : ICommand
    {
        protected DataGrid DgFiles;

        protected BaseAppCommand(DataGrid dataGrid)
        {
            DgFiles = dataGrid;
        }

        public bool CanExecute(object parameter)
        {
            return !Helper.CurrentApp.IsWorking;
        }

        public abstract void Execute(object parameter);
      

        public event EventHandler CanExecuteChanged;
    }
}
