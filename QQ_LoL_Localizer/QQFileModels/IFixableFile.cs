using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    public interface IFixableFile
    {
        bool? IsFixed { get; set; }
        Task FixAsync();
        Task RestoreAsync();
        void Backup();
        string FilePath { get; }
        Task BackupAsync();
        Task RefreshAsync();
    }
}
