using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class FmodVoBankFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                {
                    if (!File.Exists(FilePath))
                        return (IsFileFixed = false);

                    var fileEnUs = new FileInfo(FilePath);
                    var fileZhCn = new FileInfo(FilePath.Replace("en_US", "zh_CN"));
                    IsFileFixed = fileEnUs.Length == fileZhCn.Length;
                }
                return IsFileFixed;
            }
            set
            {
                IsFileFixed = value;
                OnPropertyChanged();
            }
        }
        public override async Task FixAsync()
        {
            if (IsFixed.GetValueOrDefault(false))
                return;
            await Task.Run(() =>
                {
                    Backup();
                    File.Copy(FilePath.Replace("en_US", "zh_CN"), FilePath, true);
                    IsFixed = null;
                });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Sounds\\FMOD\\VOBANK_en_US.fsb"); } }
    }
}
