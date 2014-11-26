using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Win32;
using QQ_LoL_Localizer.Annotations;
using QQ_LoL_Localizer.Commands;
using QQ_LoL_Localizer.Properties;
using QQ_LoL_Localizer.QQFileModels;
using System.Linq;

namespace QQ_LoL_Localizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged, IDisposable
    {
        private Timer _timer;

        public MainWindow()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.Path)
                || !Directory.Exists(Settings.Default.Path)
                || File.Exists(Path.Combine(Settings.Default.Path, "\\TCLS\\client.exe")))
                if (!SetLoLPath())
                    Application.Current.Shutdown();

            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Title = "QQ LoL Localizer - " + version;

        }

        #region Helpers

        private async void RefreshTimerTick()
        {
            if (await Dispatcher.InvokeAsync(() => IsWorking)) return;

            var gameProcess = Process.GetProcessesByName("Lolclient");
            if (gameProcess.Length > 0)
            {
                _timer.Change(Timeout.Infinite, 15000);
                if (BehaviorOnStartGame == Behavior.Close)
                {
                    Dispatcher.Invoke(Close);
                    return;
                }
            }

            var cmd = (RefreshCommand) FindResource("RefreshCommand");
            if (cmd.CanExecute(null))
                cmd.Execute(FilesListView);
        }

        public static bool SetLoLPath()
        {
            var fileDialog = new OpenFileDialog
                   {
                       CheckFileExists = true,
                       CheckPathExists = true,
                       Title = "TCLS Client Path",
                       DefaultExt = "exe",
                       Filter = "TCLS Client|Client.exe"
                   };
            var result = fileDialog.ShowDialog();
            if (result == false)
                return false;

            var tmp = Directory.GetParent(fileDialog.FileName).Parent;
            if (tmp == null)
                return false;

            Settings.Default.Path = tmp.FullName;
            Settings.Default.Save();
            return true;
        }

        #endregion

        #region Dependency Properties
        public static readonly DependencyProperty IsWorkingProperty =
            DependencyProperty.Register("IsWorking", typeof (bool), typeof (MainWindow), new PropertyMetadata(default(bool)));
        #endregion

        #region Binding Properties

        public ObservableCollection<IFixableFile> FixableFiles { get { return Helper.Files; } }

        public bool IsGameFixed
        {
            get { return FixableFiles.All(f => f.IsFixed.GetValueOrDefault(false)); }
            set
            {
                Console.WriteLine(value);
                NotifyPropertyChanged("IsGameFixed");
            }
        }
        public bool IsAdvancedView
        {
            get
            {
                return Settings.Default.IsAdvancedView;
            }
            set
            {
                if (Settings.Default.IsAdvancedView == value)
                    return;
                Settings.Default.IsAdvancedView = value;
                Settings.Default.Save();
                NotifyPropertyChanged("IsAdvancedView");
            }
        }
        public bool IsWorking
        {
            get { return (bool) GetValue(IsWorkingProperty); }
            set { SetValue(IsWorkingProperty, value); }

        }
        public Behavior BehaviorOnStartGame
        {
            get
            {
                var strValue = Settings.Default.BehaviorOnGameStart;
                if (string.IsNullOrEmpty(strValue))
                    return (BehaviorOnStartGame = Behavior.Nothing);
                return (Behavior)Enum.Parse(typeof(Behavior), strValue, true);
            }
            set
            {
                Settings.Default.BehaviorOnGameStart = value.ToString();
                Settings.Default.Save();
                NotifyPropertyChanged("BehaviorOnStartGame");
            }
        }
        public bool LaunchIfFixed
        {
            get { return Settings.Default.LaunchIfFixed; }
            set
            {
                Settings.Default.LaunchIfFixed = value;
                Settings.Default.Save();
            }
        }

        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region IDisposable Members

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            // Avoid redundant calls
            if (_disposed) return;

            // Disposing managed ressources
            if (disposing)
            {
                if(_timer != null)
                    _timer.Dispose();
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Events
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var cmd = (RunGameCommand) FindResource("RunGameCommand");
            if ( LaunchIfFixed && cmd.CanExecute(null))
                cmd.Execute(BehaviorOnStartGame);

            _timer = new Timer(state => RefreshTimerTick(), null, 10000, 15000);
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
        #endregion

    }
}
