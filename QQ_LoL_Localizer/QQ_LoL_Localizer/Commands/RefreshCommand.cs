using System.Windows.Controls;

namespace QQ_LoL_Localizer.Commands
{
    class RefreshCommand : BaseAppCommand
    {
        public RefreshCommand(DataGrid dataGrid) : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            Helper.ProcessCommand(DgFiles.Items, Helper.CommandType.Refresh);
        }
    }
}
