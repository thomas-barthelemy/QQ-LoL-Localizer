namespace QQ_LoL_Localizer.Commands
{
    public class FixAllCommand : BaseAppCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            Helper.ProcessCommand(Files.Items, Helper.CommandType.Fix);
        }
    }
}