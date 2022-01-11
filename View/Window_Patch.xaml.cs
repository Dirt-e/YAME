using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using YAME.Model;

namespace YAME.View
{
    public partial class Window_Patcher : Window
    {
        public Window_Patcher()
        {
            InitializeComponent();
        }

        private void btn_Patch_DCS_Click(object sender, RoutedEventArgs e)
        {
            if (IsPatched_DCS())
            {
                MessageBox.Show("DCS is already patched for motion data export.\n" +
                                "There's really nothing for me to do here.",
                                "Patched already",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string DCS = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS";

            if (!Directory.Exists(DCS))                                                          //Is DCS installed?
            {
                MessageBox.Show("DCS is not installed on your system.",
                                "DCS patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Directory.CreateDirectory(DCS + "\\Scripts\\Hooks");                            //Make sure directory is there 

            var content = Resource.YAME_Export_Hook;                                        //Grab content...
            File.WriteAllBytes(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua", content);    //...and write it to file.

            MessageBox.Show("Patched DCS for motion data export.",                          //Brag about it :-)
                            "DCS patched",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_Patch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
        {
            if (IsPatched_DCSopenbeta())
            {
                MessageBox.Show("DCS.openbeta is already patched for motion data export.\n" +
                                "There's really nothing for me to do here.",
                                "Patched already",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string DCSob = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS.openbeta";

            if (!Directory.Exists(DCSob))                                                   //Is DCS installed?
            {
                MessageBox.Show("DCS.openbeta is not installed on your system.",
                                "DCS.openbeta patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Directory.CreateDirectory(DCSob + "\\Scripts\\Hooks");                          //Make sure directory is there 

            var content = Resource.YAME_Export_Hook;                                        //Grab content...
            File.WriteAllBytes(DCSob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua", content);  //...and write it to file.

            MessageBox.Show("Patched DCS.openbeta for motion data export.",                          //Brag about it :-)
                            "DCS patched",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void btn_Unpatch_DCS_Click(object sender, RoutedEventArgs e)
        {
            string DCS = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS";

            if (!Directory.Exists(DCS))
            {
                MessageBox.Show("Could not patch DCS. It is not installed on your system.",
                                "DCS not found",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (!File.Exists(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua"))
            {
                MessageBox.Show("DCS was already unpatched.",
                                "DCS patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            File.Delete(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua");

                MessageBox.Show("Unpatched DCS.\n " +
                                "Motion data export suspended.",
                                "DCS patch removed",
                                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_Unpatch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
        {
            string DCSob = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS.openbeta";

            if (!Directory.Exists(DCSob))
            {
                MessageBox.Show("Could not unpatch DCS.openbeta.\n" +
                                "It is not installed on your system.",
                                "DCS not found",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!File.Exists(DCSob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua"))
            {
                MessageBox.Show("DCS.openbeta was already unpatched.\n" +
                                "There's really nothing for me to do here.",
                                "DCS unpatch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            File.Delete(DCSob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua");

            MessageBox.Show("Unpatched DCS.openbeta.\n " +
                            "Motion data export suspended.",
                            "DCS.openbeta patch removed",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_Patch_XPlane_Click(object sender, RoutedEventArgs e)
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string installs_xp9 = userFolder + "\\AppData\\Local\\x-plane_install.txt";
            string installs_xp10 = userFolder + "\\AppData\\Local\\x-plane_install_10.txt";
            string installs_xp11 = userFolder + "\\AppData\\Local\\x-plane_install_11.txt";

            List<string> allFiles = new List<string>();
            List<string> allPaths = new List<string>();
            allPaths.DefaultIfEmpty<string>("C:");
            
            allFiles.Add(installs_xp9);
            allFiles.Add(installs_xp10);
            allFiles.Add(installs_xp11);

            //Is it even installed?
            foreach (var file in allFiles)              
            {
                if (File.Exists(file)) goto IsInstalled;
            }
            
            MessageBox.Show("Looks like you don't have X-Plane installed :-/",
                            "Can't find X-Plane",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
            return;

            //---------------------------------------------------------------------------
            
            IsInstalled:

            //Collect all paths
            foreach (var file in allFiles)
            {
                if (File.Exists(file))
                {
                    var paths = File.ReadAllLines(file);
                    foreach (var path in paths)
                    {
                        allPaths.Add(path);
                    }
                }
            }

            StringBuilder list = new StringBuilder();
            foreach (var path in allPaths)
            {
                list.AppendLine(path);
            }

            MessageBox.Show("So, you want to patch X-Plane for motion data export?\n" +
                "That's great! I can even help you with that. All you gotta do is " +
                "point me to your X-Plane installation folder.\n\n" +
                "In case you don't know where that is, I have a sneakin' suspicion you're " +
                "gonna find it in one these locations:\n\n" +
                $"{list}\n" +
                "...but YOU get to have the last word on where I will install the patch. \n\n" +
                "Now point me to your X-Plane install folder, will you.",
                            "Need your help!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            string mostProbablePath = allPaths.Last();

            asdfsf

            for (int i = mostProbablePath.Length; i < 0; i--)
            {
                if ((char)mostProbablePath[i] != '\\')
                {
                    mostProbablePath = mostProbablePath.Substring(0, mostProbablePath.Length - 1);
                }
            }

            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = mostProbablePath,
                IsFolderPicker = true,
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string xplane_install = dialog.FileName;
            }


                //Check if "Aircraft", "Airfoils" and "Resources" folder exists
        }
        private void btn_Unpatch_XPlane_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //---------- Helpers ----------
        public bool IsPatched_DCS()
        {
            string ExpScript = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + "\\Saved Games\\DCS\\Scripts\\Hooks\\YAME_Export_Hook.lua";

            if (File.Exists(ExpScript)) return true;
            return false;
        }
        public bool IsPatched_DCSopenbeta()
        {
            string ExpScript = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + "\\Saved Games\\DCS.openbeta\\Scripts\\Hooks\\YAME_Export_Hook.lua";

            if (File.Exists(ExpScript)) return true;
            return false;
        }
    }
}
