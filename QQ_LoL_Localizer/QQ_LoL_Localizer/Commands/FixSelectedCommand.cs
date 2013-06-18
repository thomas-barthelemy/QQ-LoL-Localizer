namespace QQ_LoL_Localizer.Commands
{
    public class FixSelectedCommand : BaseAppCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            Helper.ProcessCommand(Files.SelectedItems, Helper.CommandType.Fix);
        }
    }
}
