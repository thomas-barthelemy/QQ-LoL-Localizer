namespace QQ_LoL_Localizer.Commands
{
    class RestoreSelectedCommand : BaseAppCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            Helper.ProcessCommand(Files.SelectedItems, Helper.CommandType.Restore);
        }
    }
}
