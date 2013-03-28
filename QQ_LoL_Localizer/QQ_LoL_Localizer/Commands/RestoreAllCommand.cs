using System.Linq;
using System.Windows.Controls;
using QQ_LoL_Localizer.QQFileModels;

namespace QQ_LoL_Localizer.Commands
{
    class RestoreAllCommand : BaseAppCommand
    {
        public RestoreAllCommand(DataGrid dataGrid) : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            foreach (var file in DgFiles.Items.OfType<IFixableFile>())
            {
                file.BackupAsync();
            }
        }
    }
}
