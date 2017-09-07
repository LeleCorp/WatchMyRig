using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using IWshRuntimeLibrary;

namespace watchmyrig_client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<File> StartFiles { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            StartFiles = new ObservableCollection<File>();
            dgFiles.ItemsSource = StartFiles;
        }

        private void BtFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = ".bat files (*.bat)|*.bat";
            if (ofd.ShowDialog() == true)
            {
                StartFiles.Add(new File(ofd.SafeFileName, ofd.FileName));
                
            }
            ReloadList();
        }

        private void ReloadList()
        {
            dgFiles.ItemsSource = null;
            dgFiles.ItemsSource = StartFiles;
        }

        private void BtSet_Click(object sender, RoutedEventArgs e)
        {
            foreach (File f in StartFiles)
            {
                WshShell wshShell = new WshShell();

                IWshRuntimeLibrary.IWshShortcut shortcut;
                string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                // Create the shortcut
                shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(startUpFolderPath + "\\" + f.FileName + ".lnk");

                shortcut.TargetPath = f.Path;
                shortcut.Description = "Launch My Application";
                // shortcut.IconLocation = Application.StartupPath + @"\App.ico";
                shortcut.Save();
            }

        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            string result = MessageBox.Show("Are you sure ?", "Delete files", MessageBoxButton.YesNo, MessageBoxImage.Warning).ToString();

            if (result == "Yes")
            {
                for (int i = 0; i < dgFiles.Items.Count; i++)
                {
                    var item = dgFiles.Items[i];
                    var index = dgFiles.Columns.Single(c => c.Header.ToString() == "Delete").DisplayIndex;
                    var checkbox = dgFiles.Columns[index].GetCellContent(item) as CheckBox;

                    if ((bool)checkbox.IsChecked)
                    {

                    }
                }
            }
        }
    }

    public partial class File
    {
        private string filename;
        private string path;

        public File(string _fileName, string _path)
        {
            filename = _fileName;
            path = _path;
        }

        public string FileName
        {
            get
            {
                return this.filename;
            }
            set
            {
                this.filename = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }
    }
}
