using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class GameAudioDirectory : BackableFile
    {
        public GameAudioDirectory()
        {
            IsNewFile = true;
            IsDirectory = true;
        }

        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                {
                    if (!Directory.Exists(FilePath))
                        return (IsFileFixed = false);

                    var dirEnUs = new DirectoryInfo(FilePath);
                    var dirZhCn = new DirectoryInfo(FilePath.Replace("en_US", "zh_CN"));
                    IsFileFixed = dirZhCn.GetFiles("*.*", SearchOption.AllDirectories).Sum(f => f.Length)
                        == dirEnUs.GetFiles("*.*", SearchOption.AllDirectories).Sum(f => f.Length);
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
                if(Directory.Exists(FilePath))
                    Directory.Delete(FilePath, true);

                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(
                    FilePath.Replace("en_US", "zh_CN"),
                    FilePath,
                    true
                );

                IsFixed = null;
            });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Sounds\\Wwise\\VO\\en_US"); } }
    }
}
