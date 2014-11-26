using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace QQ_LoL_Localizer.Commands
{
    class RunGameCommand : ICommand
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

        public void Execute(object parameter)
        {
            try
            {
                var behavior = (Behavior) parameter;

                var clientPath = Path.Combine(Helper.LoLPath, "tcls\\client.exe");

                if (File.Exists(clientPath))
                    Process.Start(clientPath);

                if(behavior == Behavior.Minimize)
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
