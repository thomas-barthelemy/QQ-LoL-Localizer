using System.Windows.Controls;

namespace QQ_LoL_Localizer.Commands
{
    internal class FixAllCommand : BaseAppCommand
    {
        public FixAllCommand(DataGrid dataGrid)
            : base(dataGrid)
        {
        }

        public override void Execute(object parameter)
        {
            Helper.ProcessCommand(DgFiles.Items, Helper.CommandType.Fix);
        }
    }
}