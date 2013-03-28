using System.Windows.Controls;
using System.Linq;
using QQ_LoL_Localizer.QQFileModels;

namespace QQ_LoL_Localizer.Commands
{
    class RestoreSelectedCommand : BaseAppCommand
    {
        public RestoreSelectedCommand(DataGrid dataGrid) : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            foreach (var file in DgFiles.SelectedItems.OfType<IFixableFile>())
            {
                file.RestoreAsync();
            }
        }
    }
}
