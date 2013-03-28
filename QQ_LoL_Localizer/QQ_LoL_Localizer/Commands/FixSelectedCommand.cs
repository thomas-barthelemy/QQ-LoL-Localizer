using System.Windows.Controls;

namespace QQ_LoL_Localizer.Commands
{
    class FixSelectedCommand : BaseAppCommand
    {
        public FixSelectedCommand(DataGrid dataGrid) : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            Helper.ProcessCommand(DgFiles.SelectedItems, Helper.CommandType.Fix);
        }
    }
}
