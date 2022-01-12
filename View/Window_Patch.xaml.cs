using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;


namespace YAME.View
{
    public partial class Window_Patcher : Window
    {
        public Window_Patcher()
        {
            InitializeComponent();
        }

        //---------- DCS ----------
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
        private bool IsPatched_DCS()
        {
            string ExpScript = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + "\\Saved Games\\DCS\\Scripts\\Hooks\\YAME_Export_Hook.lua";

            if (File.Exists(ExpScript)) return true;
            return false;
        }
        private bool IsPatched_DCSopenbeta()
        {
            string ExpScript = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + "\\Saved Games\\DCS.openbeta\\Scripts\\Hooks\\YAME_Export_Hook.lua";

            if (File.Exists(ExpScript)) return true;
            return false;
        }
        
        //---------- X-Plane ----------
        const string xPlane9  = "x-plane_install.txt";
        const string xPlane10 = "x-plane_install_10.txt";
        const string xPlane11 = "x-plane_install_11.txt";
        const string xPlane12 = "x-plane_install_12.txt";

        private void btn_Patch_XPlane_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInstalled_XPlane())
            {
                MessageBox.Show("Looks like you don't have X-Plane installed :-/",
                                "Can't find X-Plane",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            Patch_XPlane();
        }
        private void Patch_XPlane()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string installs_xp9 = userFolder + "\\AppData\\Local\\" + xPlane9;
            string installs_xp10 = userFolder + "\\AppData\\Local\\" + xPlane10;
            string installs_xp11 = userFolder + "\\AppData\\Local\\" + xPlane11;
            string installs_xp12 = userFolder + "\\AppData\\Local\\" + xPlane12;

            List<string> allFiles = new List<string>();
            allFiles.Add(installs_xp9);
            allFiles.Add(installs_xp10);
            allFiles.Add(installs_xp11);

            List<string> allPaths = new List<string>();
            allPaths.DefaultIfEmpty<string>("C:");

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
            //----------------------------------------------------------------------------------Create ALl paths()
            StringBuilder list = new StringBuilder();
            foreach (var path in allPaths)
            {
                list.AppendLine(path);
            }

            MessageBox.Show("So, you want to patch X-Plane for motion data export?\n" +
                "That's great! I can even help you with that. All you gotta do is " +
                "point me to your X-Plane installation folder.\n\n" +
                "In case you don't know where that is, I have a sneakin' suspicion you're " +
                "gonna find it in one of these locations:\n\n" +
                $"{list}\n" +
                "Now point me to your X-Plane install folder, will you.",
                            "Need your help!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            string mostProbablePath = allPaths.Last();                                          //X-Plane install directory
            string mostProbableContainingFolder = RemoveLastDirectoryFrom(mostProbablePath);    //Where to open the file browser

            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = mostProbableContainingFolder,
                IsFolderPicker = true,
            };
            var Result = dialog.ShowDialog();

            if (Result != CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You aborted the patch routine.\n\n" +
                                "X-Plane was NOT patched!",
                                "Patch aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;                                                                     //End Process
            }
            if (!Isconfirmed_XPlaneFolder(dialog.FileName))
            {
                MessageBox.Show("This is not an X-Plane installation folder! It does not contain " +
                                "all the usual subfolder structure that I expect to see :-/\n\n" +
                                "X-Plane was NOT patched!",
                                "Not an X-Plane installation Folder",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ExtractPluginToFolder(dialog.FileName);

            MessageBox.Show("X-Plane was successfully patched for motion data export to YAME.",
                            "Patch successful",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void btn_Unpatch_XPlane_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInstalled_XPlane())
            {
                MessageBox.Show("Looks like you don't have X-Plane installed on your computer :-/",
                                "Can't find X-Plane",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }
            
            Unpatch_XPlane();
        }
        private void Unpatch_XPlane()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string installs_xp9 = userFolder + "\\AppData\\Local\\" + xPlane9;
            string installs_xp10 = userFolder + "\\AppData\\Local\\" + xPlane10;
            string installs_xp11 = userFolder + "\\AppData\\Local\\" + xPlane11;
            string installs_xp12 = userFolder + "\\AppData\\Local\\" + xPlane12;

            List<string> allFiles = new List<string>();
            allFiles.Add(installs_xp9);
            allFiles.Add(installs_xp10);
            allFiles.Add(installs_xp11);

            List<string> allPaths = new List<string>();
            allPaths.DefaultIfEmpty<string>("C:");

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

            MessageBox.Show("So, you want to remove the motion export patch for X-Plane?\n" +
                "I can do that for you! All you gotta do is " +
                "point me to your X-Plane installation folder.\n\n" +
                "In case you don't know where that is, I have a sneakin' suspicion you're " +
                "gonna find it in one of these locations:\n\n" +
                $"{list}\n" +
                "Now point me to your X-Plane install folder, will you.",
                            "Need your help!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            string mostProbablePath = allPaths.Last();                                      //X-Plane install directory
            string mostProbableContainingFolder = RemoveLastDirectoryFrom(mostProbablePath);    //Where to open the file browser

            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = mostProbableContainingFolder,
                IsFolderPicker = true,
            };
            var Result = dialog.ShowDialog();

            if (Result != CommonFileDialogResult.Ok)
            {
                return;     //End Process
            }

            if (!Isconfirmed_XPlaneFolder(dialog.FileName))
            {
                MessageBox.Show("This is not an X-Plane installation folder! It does not contain " +
                                "all the usual subfolder structure that I expect to see :-/\n\n" +
                                "I did NOT make any changes to your X-Plane installation, if you have one.",
                                "Not an X-Plane installation Folder",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!Directory.Exists(dialog.FileName + "\\Resources\\plugins\\XPlaneGetter"))
            {
                MessageBox.Show("This lookes like an X-Plane installation Folder, but there " +
                    "was no patch to remove. I did not make any changes to your X-Plane installation.",
                                "No patch found",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Directory.Delete(dialog.FileName + "\\Resources\\plugins\\XPlaneGetter", true);

            MessageBox.Show("X-Plane was successfully un-patched. No more motion Data " +
                            "is being exported to YAME.",
                            "Patch removed successfully",
                            MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void ExtractPluginToFolder(string folder)
        {
            string plugin = ".\\Resources\\X-Plane\\XPlaneGetter.zip";
            string destination = folder + "\\Resources\\plugins\\";
                
            if (Directory.Exists(destination + "XPlaneGetter"))
            {
                MessageBox.Show("X-Plane is already patched for motion data export. I'm " +
                                "gonna re-apply the patch, just to be sure.",
                                "Overwriting Patch",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Directory.Delete(destination + "XPlaneGetter", true);
            }

            ZipFile.ExtractToDirectory(plugin, destination);
        }

        //---------- Helpers ----------
        private bool IsInstalled_XPlane()
        {
            //Function checks if X-Plane is installed on the computer
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
                if (File.Exists(file)) return true;
            }

            return false;
        }
        private bool Isconfirmed_XPlaneFolder(string path)
        {
            if (Directory.Exists(path + "\\Airfoils") && Directory.Exists(path + "\\Aircraft"))
            {
                return true;
            }
            return false;
        }
        private static string RemoveLastDirectoryFrom(string FolderPath)
        {
            if (FolderPath.EndsWith("/")  || FolderPath.EndsWith("\\"))
            {
                FolderPath = Path.GetDirectoryName(FolderPath);
            }
            FolderPath = Path.GetDirectoryName(FolderPath);

            if (FolderPath.EndsWith("/") || FolderPath.EndsWith("\\"))
            {
                return FolderPath;
            }
           
            return FolderPath + "/";
        }

        //---------- Close ----------
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
