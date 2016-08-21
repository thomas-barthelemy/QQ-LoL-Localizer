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
                Helper.ReplaceInFile(FilePath, "arialbd.ttf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "verdanab.ttf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "DejaVuSans-Bold.ttf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "FRQITCBT.ttf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "Garuda-Bold.ttf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "SpiegelCDB.otf", "msyhbd.ttf");
                Helper.ReplaceInFile(FilePath, "LucasFonts-SpiegelCdOT-SemiBold.otf", "msyhbd.ttf");

                // Regular fonts
                Helper.ReplaceInFile(FilePath, "arial.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "ariblk.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "verdana.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "times.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "tahoma.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "DejaVuSans.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "FRQITC01.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "Garuda.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "UTMEssendineCaps.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "UttumDotum.ttf", "msyh.ttf");
                Helper.ReplaceInFile(FilePath, "LucasFonts-SpiegelCdOT-Regular.otf", "msyh.ttf");
                IsFixed = null;
            });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Fonts\\FontMappings.txt"); } }

    }
}
