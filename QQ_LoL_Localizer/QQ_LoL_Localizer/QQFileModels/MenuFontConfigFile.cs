using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class MenuFontConfigFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                    IsFileFixed = File.ReadAllText(FilePath).Contains("fonts_ch.swf");

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
            if(IsFixed.GetValueOrDefault(false))
                return;
            await Task.Run(() =>
                {
                    Backup();
                    Helper.ReplaceInFile(FilePath, "fonts_latin.swf", "fonts_ch.swf");
                    Helper.ReplaceInFile(FilePath, "Gill Sans MT Pro Medium", "FZLanTingHei-L-GBK");
                    IsFixed = null;
                });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Menu\\fontconfig_en_US.txt"); } }

    }
}
