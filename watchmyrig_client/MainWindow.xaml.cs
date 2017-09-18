using System;
using System.Linq;
using System.Windows;
using System.Management;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;
using IWshRuntimeLibrary;
using Forms = System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Collections.Generic;
using OpenHardwareMonitor.Hardware;

namespace watchmyrig_client
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<File> startFiles;
        private Forms.NotifyIcon ni;
        private List<Gpu> gpuList;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Hide();
            startFiles = new ObservableCollection<File>();
            OnLoad();
            
            dgFiles.ItemsSource = startFiles;
        }

        private void BtFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = ".bat files (*.bat)|*.bat";
            //ofd.Multiselect = true;
            if (ofd.ShowDialog() == true)
            {
                startFiles.Add(new File(ofd.SafeFileName, ofd.FileName, string.Empty));                
            }
            ReloadList();
        }

        /// <summary>
        /// Reloads list containing selected files.
        /// </summary>
        private void ReloadList()
        {
            dgFiles.ItemsSource = startFiles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtSet_Click(object sender, RoutedEventArgs e)
        {
            foreach (File f in startFiles)
            {
                WshShell wshShell = new WshShell();

                IWshRuntimeLibrary.IWshShortcut shortcut;
                string startUpFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
                f.StartupPath = startUpFolderPath + "\\" + f.FileName + ".lnk";

                // Create the shortcut
                shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(startUpFolderPath + "\\" + f.FileName + ".lnk");

                shortcut.TargetPath = f.Path;
                shortcut.Description = "Launch My Application";
                shortcut.Save();
            }

            MessageBox.Show("Files successfully copied to startup folder.");
        }

        private void GetGpuTemp()
        {
            var myComputer = new Computer();
            
            myComputer.GPUEnabled = true;
            myComputer.Open();

            foreach (var hardwareItem in myComputer.Hardware)
            {
                hardwareItem.Update();
                hardwareItem.GetReport();

                MessageBox.Show(hardwareItem.GetReport());

                foreach (var sensor in hardwareItem.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature)
                    {
                        MessageBox.Show(sensor.Name+" "+ sensor.Hardware+ " "+ sensor.SensorType+ " "+ sensor.Value);
                    }

                }
            }
        }

        /// <summary>
        /// Event handler for delete button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (System.IO.File.Exists(((File)item).StartupPath))
                        {
                            try
                            {
                                System.IO.File.Delete(((File)item).StartupPath);
                                startFiles.Remove((File)item);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The file you want to delete doesn't exist."); 
                        }
                    }
                }
            }

            ReloadList();
        }

        /// <summary>
        /// Restarting function.
        /// </summary>
        private void RestartRig()
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 10 -c 'Restarting'");
        }

        /// <summary>
        /// Handles window minimizing in tray bar.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
                ni.Visible = true;
            }
            base.OnStateChanged(e);
        }

        /// <summary>
        /// Handles notify icon when app isn't in tray bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mi_Click(object sender, EventArgs e)
        {
            ni.Visible = false;
            this.Close();
        }

        /// <summary>
        /// Serializes list of files when closing the app.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("file.bin", FileMode.Create, FileAccess.Write, FileShare.None);

                formatter.Serialize(stream, startFiles);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves objects, creates traybar item and handles closing event.
        /// </summary>
        private void OnLoad()
        {
            try
            {
                // Deserializing
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("file.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                startFiles = (ObservableCollection<File>)formatter.Deserialize(stream);
                stream.Close();

                // Adding items to traybar menu
                Forms.MenuItem mi = new Forms.MenuItem("Exit");
                mi.Click += new System.EventHandler(Mi_Click);
                Forms.ContextMenu cm = new Forms.ContextMenu();
                cm.MenuItems.Add(mi);

                // Setting notify icon properties
                ni = new Forms.NotifyIcon();
                ni.Visible = true;
                ni.Text = "Double click to restore";
                ni.Icon = Properties.Resources.pick;
                ni.ContextMenu = cm;
                ni.DoubleClick +=
                    delegate (object sender, EventArgs args)
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                        ni.Visible = false;
                    };

                // Add event handler for app closing
                Closing += new CancelEventHandler(OnApplicationExit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetGpuTemp();
        }
    }
}
