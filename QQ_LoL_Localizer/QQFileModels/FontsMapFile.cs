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
                var content = File.ReadAllText(FilePath);
                return !(content.Contains("arial.ttf")
                         || content.Contains("verdana.ttf")
                         || content.Contains("DejaVueSans.ttf")
                         || content.Contains("FRQITC01.ttf")
                         || content.Contains("Garuda.ttf")
                         || content.Contains("UttumDotum.ttf")
                         || content.Contains("SpiegelC"));
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
                // Bold Fonts
                Helper.ReplaceInFile(FilePath, "arialbd.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "verdanab.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "DejaVuSans-Bold.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "FRQITCBT.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "Garuda-Bold.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "SpiegelCDB.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "LucasFonts-SpiegelCdOT-SemiBold.otf", "FZDHTK.ttf");

                // Regular fonts
                Helper.ReplaceInFile(FilePath, "arial.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "ariblk.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "verdana.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "times.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "tahoma.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "DejaVuSans.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "FRQITC01.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "Garuda.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "UTMEssendineCaps.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "UttumDotum.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "LucasFonts-SpiegelCdOT-Regular.otf", "FZDHTK.ttf");
                IsFixed = null;
            });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Fonts\\FontMappings.txt"); } }

    }
}
