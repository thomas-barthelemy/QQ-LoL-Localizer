using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using QQ_LoL_Localizer.Commands;
using QQ_LoL_Localizer.Properties;
using QQ_LoL_Localizer.QQFileModels;

namespace QQ_LoL_Localizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.Path) || !Directory.Exists(Settings.Default.Path))
                SetLoLPath();

            InitializeComponent();
            InitCommands();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Title = "QQ LoL Localizer - " + version;
            DataContext = Application.Current;
            DgFiles.ItemsSource = Helper.Files;
        }

        #region Helpers

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
        
        private void InitCommands()
        {
            BtnFix.Command = new FixSelectedCommand(DgFiles);
            BtnFixAll.Command = new FixAllCommand(DgFiles);
            BtnRestoreSelected.Command = new RestoreSelectedCommand(DgFiles);
            BtnRestoreAll.Command = new RestoreAllCommand(DgFiles);
            BtnRefresh.Command = new RefreshCommand(DgFiles);
        }

        #endregion

        #region Events
        private void DgFiles_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var element = e.MouseDevice.DirectlyOver;
            if (element == null || !(element is FrameworkElement)) return;
            if (!(((FrameworkElement) element).Parent is DataGridCell)) return;
            var grid = sender as DataGrid;
            if (grid == null || grid.SelectedItems == null || grid.SelectedItems.Count != 1) return;
            var file = grid.SelectedItem as BackableFile;
            if (file != null)
            {
                file.OpenFolder();
            }
        }
        #endregion
    }
}
