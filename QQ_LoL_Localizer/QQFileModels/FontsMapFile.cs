using System.IO;
using System.Threading.Tasks;

namespace QQ_LoL_Localizer.QQFileModels
{
    /// <summary>
    /// This file no longer requires modification but is kept for retro-compatibility issue
    /// </summary>
    class FontsMapFile : BackableFile
    {
        public override bool? IsFixed
        {
            get { return !File.Exists(FilePath + ".backup"); }
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
            await RestoreAsync();
        }

        public override string FilePath { get { return Path.Combine(Helper.LoLPath, "Game\\Data\\Fonts\\FontMappings.txt"); } }

    }
}
