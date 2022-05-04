using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using YAME.Model;
using static Folders;

namespace YAME.View
{
    public partial class Window_Patcher : Window
    {
        SnappyDragger snappyDragger;

        public Window_Patcher()
        {
            InitializeComponent();

            snappyDragger = new SnappyDragger(this);
        }

        //---------- DCS ----------
        void btn_Patch_DCS_Click(object sender, RoutedEventArgs e)
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

            MessageBox.Show("Patched DCS for motion data export.\n" +
                            "Restart DCS now!",                                             //Brag about it :-)
                            "DCS patched",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void btn_Unpatch_DCS_Click(object sender, RoutedEventArgs e)
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

                MessageBox.Show("Unpatched DCS.\n" +
                                "Motion data export suspended.",
                                "DCS patch removed",
                                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        bool IsPatched_DCS()
        {
            string ExpScript = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                                + "\\Saved Games\\DCS\\Scripts\\Hooks\\YAME_Export_Hook.lua";

            if (File.Exists(ExpScript)) return true;
            return false;
        }

        //---------- DCS openbeta ----------
        void btn_Patch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
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

            MessageBox.Show("Patched DCS.openbeta for motion data export.\n" +
                            "Restart DCS now!",                                             //Brag about it :-)
                            "DCS patched",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void btn_Unpatch_DCSopenbeta_Click(object sender, RoutedEventArgs e)
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
        bool IsPatched_DCSopenbeta()
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

        void btn_Patch_XPlane_Click(object sender, RoutedEventArgs e)
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
        void Patch_XPlane()
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
            //----------------------------------------------------------------------------------Create All paths()
            StringBuilder list = new StringBuilder();
            foreach (var path in allPaths)
            {
                list.AppendLine(path);
            }

            MessageBox.Show("So, you want to patch X-Plane for motion data export?\n" +
                "That's great! Let me help you with that. All you gotta do is " +
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

            try
            {
                ExtractPluginToFolder(dialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("I was unable to apply the motion data patch to X-Plane. Usually this happens, when you " +
                                "have X-Plane running while trying to apply the patch. Make sure X-Plane is NOT running!" +
                                "You might even have to reboot to be absolutely sure. \n\n" +
                                "X-Plane was NOT patched!",
                                "Something went wrong",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            

            MessageBox.Show("X-Plane was successfully patched for motion data export to YAME.",
                            "Patch successful",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void btn_Unpatch_XPlane_Click(object sender, RoutedEventArgs e)
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
        void Unpatch_XPlane()
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
                MessageBox.Show("This looks like an X-Plane installation Folder, but there " +
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
        bool IsInstalled_XPlane()
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
        bool Isconfirmed_XPlaneFolder(string path)
        {
            if (Directory.Exists(path + "\\Airfoils") && Directory.Exists(path + "\\Aircraft"))
            {
                return true;
            }
            return false;
        }
        void ExtractPluginToFolder(string folder)
        {
            string _tempZipFile = Environment.GetEnvironmentVariable("TEMP") + @"\XPlaneGetter.zip";
            var res = Resource.XPlaneGetter;
            
            using (FileStream fs = new FileStream(_tempZipFile, FileMode.Create))
            {
                fs.Write(res, 0, res.Length);
            }

            string destination = folder + "\\Resources\\plugins\\";
                
            if (Directory.Exists(destination + "XPlaneGetter"))
            {
                MessageBox.Show("X-Plane is already patched for motion data export. But I'm " +
                                "gonna re-apply the patch, just to be sure.",
                                "Overwriting Patch",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                Directory.Delete(destination + "XPlaneGetter", true);
            }

            ZipFile.ExtractToDirectory(_tempZipFile, destination);
        }

        //---------- FS2020 ----------
        void btn_Patch_FS2020_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInstalled_FS2020_ANY())
            {
                MessageBox.Show("FS2020 is not installed on your system.",
                                "Patch aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Create_FS2020_Motion_Exporter_EXE();
            if (IsInstalled_FS2020(false))  Patch_FS2020(false);
            if (IsInstalled_FS2020(true))   Patch_FS2020(true);
        }
        void btn_Unpatch_FS2020_Click(object sender, RoutedEventArgs e)
        {
            if (!IsInstalled_FS2020_ANY())
            {
                MessageBox.Show("FS2020 is not installed on your system.",
                                "Operation aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsInstalled_FS2020(false))  unPatch_FS2020(false);
            if (IsInstalled_FS2020(true))   unPatch_FS2020(true);
        }
        private void Patch_FS2020(bool steam = false)
        {
            switch (steam)
            {
                case false:
                    if (IsPatchedFS2020(false))     unPatch_FS2020(false);
                    break;
                case true:
                    if (IsPatchedFS2020(true))      unPatch_FS2020(true);
                    break;
            }

            string filePath = string.Empty;
            if (steam)  filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
            else        filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";
            
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, Resource.exe_xml_Example_BLANK);
            }

            Modify_exe_xml(filePath);
            
            MessageBox.Show(    "Patched FS2020 for motion data export.",
                                "FS2020 patched",
                                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void unPatch_FS2020(bool steam = false)
        {   
            string filePath     = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";
            if (steam)
            {
                filePath        = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
            }
            
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode SimBaseDocument = doc.SelectSingleNode("SimBase.Document");
            if (SimBaseDocument != null)
            {
                XmlNodeList Nodes = SimBaseDocument.ChildNodes;
                foreach (XmlNode Node in Nodes)
                {
                    if (Node.Name == "Launch.Addon")
                    {
                        XmlNodeList LaunchNodes = Node.ChildNodes;
                        foreach (XmlNode LaunchNode in LaunchNodes)
                        {
                            if (LaunchNode.InnerText == "YAME Motion Data Exporter")
                            {
                                SimBaseDocument.RemoveChild(Node);
                            }
                        }
                    }
                }
            }

            doc.Save(filePath);

            MessageBox.Show(    "Removed FS2020 motion data patch.",
                                "FS2020 unpatched",
                                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private bool IsInstalled_FS2020_ANY()
        {
            return (IsInstalled_FS2020(false) || IsInstalled_FS2020(true));
        }
        private bool IsInstalled_FS2020(bool steam = false)
        {   
            if (steam)  return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder);
            else        return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder);
        }
        private bool IsPatchedFS2020(bool steam = false)
        {
            string filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";

            if (steam)
            {
                filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
            }
            
            if (!File.Exists(filePath))
            {
                return false;
            }
            
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode SimBaseDocument = doc.SelectSingleNode("SimBase.Document");
            if (SimBaseDocument != null)
            {
                XmlNodeList Nodes = SimBaseDocument.ChildNodes;
                foreach (XmlNode Node in Nodes)
                {
                    if (Node.Name == "Launch.Addon")
                    {
                        XmlNodeList LaunchNodes = Node.ChildNodes;
                        foreach (XmlNode LaunchNode in LaunchNodes)
                        {
                            if (LaunchNode.InnerText == "YAME Motion Data Exporter")
                            {
                                return true;
                            }
                        }    
                    }
                }
            }
            return false;
        }
        private void Create_FS2020_Motion_Exporter_EXE()
        {
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_FS2020_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            Directory.CreateDirectory(ExportersFolder);
            
            var res = Resource.FS2020_MotionExporter;
            using (FileStream fs = new FileStream(Exporter_EXE, FileMode.Create))
            {
                fs.Write(res, 0, res.Length);
            }
        }
        private void Modify_exe_xml(string filePath)
        {
            //Open XML document:
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            //Create all the XML elements:
            XmlElement Name = doc.CreateElement("Name");
            Name.InnerText = "YAME Motion Data Exporter";

            XmlElement Disabled = doc.CreateElement("Disabled");
            Disabled.InnerText = "False";

            XmlElement Path = doc.CreateElement("Path");
            Path.InnerText = Folders.UserFolder
                + Properties.Settings.Default.Patcher_FS2020_ExportersFolder
                + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            XmlElement CommandLine = doc.CreateElement("CommandLine");
            CommandLine.InnerText = " ";

            //Combine elements to one 'Launch.Addon' element
            XmlElement LaunchAddon = doc.CreateElement("Launch.Addon");
            LaunchAddon.AppendChild(Name);
            LaunchAddon.AppendChild(Disabled);
            LaunchAddon.AppendChild(Path);
            LaunchAddon.AppendChild(CommandLine);

            //...and append it to the parent element ('Simbase.Document')
            XmlNode SimBaseDocument = doc.SelectSingleNode("SimBase.Document");
            SimBaseDocument.AppendChild(LaunchAddon);

            doc.Save(filePath);
        }
        //---------- Window ----------
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StartDrag();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            snappyDragger.StopDrag();
        }

        //---------- Helpers ----------
        static string RemoveLastDirectoryFrom(string FolderPath)
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
        void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LastClosed values
            Left = Properties.Settings.Default.Window_Patcher_Position_X;
            Top  = Properties.Settings.Default.Window_Patcher_Position_Y;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //LastClosed values
            Properties.Settings.Default.Window_Patcher_Position_X = (float)Left;
            Properties.Settings.Default.Window_Patcher_Position_Y = (float)Top;

            Properties.Settings.Default.Save();
        }

        
    }
}
