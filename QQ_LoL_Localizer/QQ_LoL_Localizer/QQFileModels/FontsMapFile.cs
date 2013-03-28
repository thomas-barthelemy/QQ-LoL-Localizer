using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class FontsMapFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                return !File.ReadAllText(FilePath).Contains("arial.ttf");
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
                    Helper.ReplaceInFile(FilePath, "arialbd.ttf", "ARIALUNI.ttf");
                    Helper.ReplaceInFile(FilePath, "arial.ttf", "ARIALUNI.ttf");
                    Helper.ReplaceInFile(FilePath, "ariblk.ttf", "ARIALUNI.ttf");
                    IsFixed = null;
                });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Fonts\\FontMappings.txt"); } }

    }
}
