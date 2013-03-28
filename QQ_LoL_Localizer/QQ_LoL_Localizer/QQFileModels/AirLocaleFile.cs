using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    class AirLocaleFile : BackableFile
    {
        public override bool? IsFixed
        {
            get
            {
                if (!IsFileFixed.HasValue)
                {
                    if (!File.Exists(FilePath))
                        return (IsFileFixed = false);

                    var allText = File.ReadAllText(FilePath);
                    IsFileFixed = allText.Contains("en_US");
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
            if(IsFixed.GetValueOrDefault(false))
                return;
            await Task.Run(() =>
                {
                    Backup();
                    Helper.ReplaceInFile(FilePath, "zh_CN", "en_US");
                    IsFixed = null;
                });
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "air\\locale.properties"); } }
    }
}
