using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class GameLocaleFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                {
                    var allText = File.ReadAllText(FilePath);
                    IsFileFixed = allText.Contains("LanguageLocaleRegion=en_SG");
                }
                return IsFileFixed.Value;
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
                    Helper.ReplaceInFile(FilePath, "zh_CN", "en_SG");
                    Helper.ReplaceInFile(FilePath, "en_US", "en_SG");
                    IsFixed = null;
                });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "game\\data\\CFG\\locale.cfg"); } }

    }
}
