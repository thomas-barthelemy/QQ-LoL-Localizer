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
                return !(
                    // Bold fonts
                    content.Contains("arialbd.ttf")
                    || content.Contains("verdanab.ttf")
                    || content.Contains("DejaVuSans-Bold.ttf")
                    || content.Contains("FRQITCBT.ttf")
                    || content.Contains("Garuda-Bold.ttf")
                    || content.Contains("SpiegelCDB.otf")
                    || content.Contains("Spiegel-Bold.otf")
                    || content.Contains("BeaufortforLOL-Bold.otf")

                    || content.Contains("arial.ttf")
                    || content.Contains("arialbk.ttf")
                    || content.Contains("verdana.ttf")
                    || content.Contains("times.ttf")
                    || content.Contains("tahoma.ttf")
                    || content.Contains("DejaVueSans.ttf")
                    || content.Contains("FRQITC01.ttf")
                    || content.Contains("Garuda.ttf")
                    || content.Contains("UTMEssendineCaps.ttf")
                    || content.Contains("UttumDotum.ttf")
                    || content.Contains("GillSansMTPro-Medium.otf")
                    || content.Contains("Spiegel-Regular.otf")
                    || content.Contains("Spiegel-SemiBold.otf")
                    || content.Contains("AxisStd-Regular.otf")
                );
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
                Helper.ReplaceInFile(FilePath, "Spiegel-Bold.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "BeaufortforLOL-Bold.otf", "FZDHTK.ttf");

                // Regular fonts
                Helper.ReplaceInFile(FilePath, "arial.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "ariblk.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "DATA\\\\Fonts\\\\verdana.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "times.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "tahoma.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "DejaVuSans.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "FRQITC01.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "Garuda.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "UTMEssendineCaps.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "UttumDotum.ttf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "GillSansMTPro-Medium.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "Spiegel-Regular.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "Spiegel-SemiBold.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "BeaufortforLOL-Regular.otf", "FZDHTK.ttf");
                Helper.ReplaceInFile(FilePath, "AxisStd-Regular.otf", "FZDHTK.ttf");

                IsFixed = null;
            });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Fonts\\FontMappings.txt"); } }

    }
}
