using System;
using System.Windows.Controls;
using System.Windows.Input;
using QQ_LoL_Localizer.QQFileModels;

namespace QQ_LoL_Localizer.Commands
{
    public class OpenFileCommand : ICommand
    {
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public bool CanExecute(object parameter)
        {
            return !Helper.IsWorking;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            var viewItem = parameter as ListViewItem;
            if (viewItem == null) return;

            var file = viewItem.Content as BackableFile;
            if (file == null) return;

            file.OpenFolder();
        }

        public event EventHandler CanExecuteChanged;
    }
}
