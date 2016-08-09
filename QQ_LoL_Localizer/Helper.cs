using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using QQ_LoL_Localizer.Properties;
using QQ_LoL_Localizer.QQFileModels;
using System.Linq;

namespace QQ_LoL_Localizer
{

    public enum Behavior
    {
        Nothing,
        Minimize,
        Close
    }

    public class Helper
    {
        public static bool IsWorking
        {
            get
            {

                return (bool)CurrentApp.Dispatcher.Invoke(() =>
                    {
                        if (CurrentApp.MainWindow == null) return false;
                        return CurrentApp.MainWindow.GetValue(
                            MainWindow.IsWorkingProperty);
                    });
            }
            set
            {
                CurrentApp.Dispatcher.Invoke(() =>
                    {
                        if (CurrentApp.MainWindow == null)
                            return;
                        CurrentApp.MainWindow.SetValue(MainWindow.IsWorkingProperty, value);
                    });
            }
        }

        public static App CurrentApp
        {
            get { return Application.Current as App; }
        }

        private static ObservableCollection<IFixableFile> _files;

        public static ObservableCollection<IFixableFile> Files
        {
            get
            {
                return _files ?? (_files = new ObservableCollection<IFixableFile>
                    {
                        new AirLocaleFile(),
                        new AirLolFile(),
                        new AirFontsFile(),
                        //new FmodAudioFile(),
                        //new FmodVoBankFile(),
                        new FontsMapFile(),
                        new GameLocaleFile(),
                        //new MenuFontConfigFile()
                    });
            }
        }

        public static string LoLPath
        {
            get { return Path.GetFullPath(Settings.Default.Path); }
        }

        /// <summary>
        ///     Replaces text in a file.
        /// </summary>
        /// <param name="filePath">Path of the text file.</param>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="replaceText">Text to replace the search text.</param>
        public static void ReplaceInFile(string filePath, string searchText, string replaceText)
        {
            var reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            reader.Close();
            content = Regex.Replace(content, searchText, replaceText);

            var writer = new StreamWriter(filePath);
            writer.Write(content);
            writer.Close();
        }
        private static int FindBytes(byte[] src, byte[] find)
        {
            var index = -1;
            var matchIndex = 0;
            // handle the complete source array
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else
                {
                    matchIndex = 0;
                }

            }
            return index;
        }

        public static byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl)
        {
            byte[] dst = null;
            var index = FindBytes(src, search);
            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];
                // before found array
                Buffer.BlockCopy(src, 0, dst, 0, index);
                // repl copy
                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);
                // rest of src array
                Buffer.BlockCopy(
                    src,
                    index + search.Length,
                    dst,
                    index + repl.Length,
                    src.Length - (index + search.Length));
            }
            return dst;
        }

        public enum CommandType
        {
            Fix, Restore,
            Refresh
        }

        public static void ProcessCommand(IList files, CommandType cmdType)
        {
            IsWorking = true;
            try
            {
                var tasks = new List<Task>();

                foreach (var file in files.OfType<IFixableFile>())
                {
                    switch (cmdType)
                    {
                        case CommandType.Fix: tasks.Add(file.FixAsync());
                            break;
                        case CommandType.Restore: tasks.Add(file.RestoreAsync());
                            break;
                        case CommandType.Refresh: tasks.Add(file.RefreshAsync());
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("cmdType");
                    }
                }
                Task.WhenAll(tasks)
                    .ContinueWith(task =>
                    {
                        IsWorking = false;

                        CurrentApp.Dispatcher.Invoke(() =>
                            {
                                var mainWindow = CurrentApp.MainWindow as MainWindow;
                                if (mainWindow == null) return;
                                mainWindow.IsGameFixed = true;
                            });

                    });
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured during the operation", "Error =(" + Environment.NewLine + e.Message, MessageBoxButton.OK,
                                MessageBoxImage.Error);
                IsWorking = false;
            }

        }
    }
}