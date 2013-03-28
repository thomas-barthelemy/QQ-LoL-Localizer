using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class FmodAudioFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                {
                    if (!File.Exists(FilePath))
                        return (IsFileFixed = false);

                    var text = File.ReadAllLines(FilePath);
                    IsFileFixed = text.Count(s => s.Contains("en_US")) == 1;
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
                File.Copy(FilePath.Replace("en_US", "zh_CN"), FilePath, true);

                var bytes = File.ReadAllBytes(FilePath);

                var searchBytes = Encoding.UTF8.GetBytes("LoL_Audio_zh_CN");
                var replaceBytes = Encoding.UTF8.GetBytes("LoL_Audio_en_US");

                bytes = Helper.ReplaceBytes(bytes, searchBytes, replaceBytes);

                File.WriteAllBytes(FilePath, bytes);

                IsFixed = null;
            });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Sounds\\FMOD\\LoL_Audio_en_US.fev"); } }

    }
}
