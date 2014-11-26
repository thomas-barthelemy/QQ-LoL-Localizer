namespace QQ_LoL_Localizer.Commands
{
    class RefreshCommand : BaseAppCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            Helper.ProcessCommand(Files.Items, Helper.CommandType.Refresh);
        }
    }
}
