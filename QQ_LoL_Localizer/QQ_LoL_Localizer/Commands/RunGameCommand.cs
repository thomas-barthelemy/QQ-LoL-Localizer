using System.Diagnostics;
using System.IO;
using System.Windows.Controls;

namespace QQ_LoL_Localizer.Commands
{
    class RunGameCommand : BaseAppCommand
    {
        public RunGameCommand(DataGrid dataGrid) : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            var clientPath = Path.Combine(Helper.LoLPath, "tcls\\client.exe");

            if (File.Exists(clientPath))
                Process.Start(clientPath);
        }

    }
}
