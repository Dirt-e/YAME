using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using YAME.Model;

namespace YAME.Model
{
    public class Patcher : MyObject
    {
        //---------- ViewModel ---------
        bool _isPatchedDCS_prop;
        public bool IsPatched_DCS_prop
        {
            get { return _isPatchedDCS_prop; }
            set { _isPatchedDCS_prop = value; OnPropertyChanged(nameof(IsPatched_DCS_prop)); }
        }

        bool _isPatchedDCS_openbeta_prop;
        public bool IsPatched_DCS_openbeta_prop
        {
            get { return _isPatchedDCS_openbeta_prop; }
            set { _isPatchedDCS_openbeta_prop = value; OnPropertyChanged(nameof(IsPatched_DCS_openbeta_prop)); }
        }

        bool _isPatchedFS2020_prop;
        public bool IsPatched_FS2020_prop
        {
            get { return _isPatchedFS2020_prop; }
            set { _isPatchedFS2020_prop = value; OnPropertyChanged(nameof(IsPatched_FS2020_prop)); }
        }

        bool _isPatchedX_Plane_prop;
        public bool IsPatched_X_Plane_prop
        {
            get { return _isPatchedX_Plane_prop; }
            set { _isPatchedX_Plane_prop = value; OnPropertyChanged(nameof(IsPatched_X_Plane_prop)); }
        }


        //-------------- DCS -------------
        public void btn_Patch_DCS_Click()
        {
            if (IsPatched_DCS()) unPatch_DCS();

            patch_DCS();
        }
        public void btn_Unpatch_DCS_Click()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_YAME;

            if (!Directory.Exists(DCS))
            {
                MessageBox.Show(
                    "Could not unpatch DCS. It is not installed on your system.",
                    "DCS not found",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!File.Exists(ExportScript))
            {
                MessageBox.Show(
                    "DCS was already unpatched.",
                    "No patch found",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            unPatch_DCS();

            MessageBox.Show(
                "Unpatched DCS.\n" +
                "Motion data export suspended.",
                "DCS patch removed",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            
            
        }
        void patch_DCS()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_YAME;

            if (!IsInstalled_DCS())                                                          //Is DCS installed?
            {
                MessageBox.Show("DCS is not installed on your system.",
                                "DCS patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(ExportScript)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ExportScript));             //Make sure directory is there 
            }


            var content = Resource.YAME_Export_Hook;                                        //Grab content...
            File.WriteAllBytes(ExportScript, content);                                      //...and write it to file.

            IsPatched_DCS_prop = true;

            MessageBox.Show(
                "Patched DCS for motion data export.\n" +
                "Restart DCS now!",                                                         //Brag about it :-)
                "DCS patched",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void unPatch_DCS()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_YAME;

            if (File.Exists(ExportScript))
            {
                File.Delete(ExportScript);
            }
            
            IsPatched_DCS_prop = false;
        }
        bool IsPatched_DCS()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_YAME;

            if (File.Exists(ExportScript)) return true;
            return false;
        }
        bool IsInstalled_DCS()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";

            return Directory.Exists(DCS);                                                   //Is DCS installed?
        }

        //---------- DCS openbeta ----------
        public void btn_Patch_DCS_openbeta_Click()
        {
            if (IsPatched_DCS_openbeta()) unPatch_DCS_openbeta();

            patch_DCS_openbeta();
        }
        public void btn_Unpatch_DCS_openbeta_Click()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_YAME;

            if (!Directory.Exists(DCS_openbeta))
            {
                MessageBox.Show(
                    "Could not unpatch DCS.openbeta. It is not installed on your system.",
                    "DCS.openbeta not found",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (!File.Exists(ExportScript))
            {
                MessageBox.Show(
                    "DCS.openbeta was already unpatched.",
                    "No patch found",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            unPatch_DCS_openbeta();

            MessageBox.Show(
                "Unpatched DCS.openbeta.\n" +
                "Motion data export suspended.",
                "DCS_openbeta patch removed",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        void patch_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_YAME;

            if (!IsInstalled_DCS_openbeta())                                                          //Is DCS installed?
            {
                MessageBox.Show("DCS.openbeta is not installed on your system.",
                                "DCS.openbeta patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(ExportScript)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ExportScript));             //Make sure directory is there 
            }


            var content = Resource.YAME_Export_Hook;                                        //Grab content...
            File.WriteAllBytes(ExportScript, content);                                      //...and write it to file.

            IsPatched_DCS_openbeta_prop = true;

            MessageBox.Show(
                "Patched DCS.openbeta for motion data export.\n" +
                "Restart DCS.openbeta now!",                                                         //Brag about it :-)
                "DCS patched",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void unPatch_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_YAME;

            if (File.Exists(ExportScript))
            {
                File.Delete(ExportScript);
            }
            IsPatched_DCS_openbeta_prop = false;
        }
        bool IsPatched_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_YAME;

            if (File.Exists(ExportScript)) return true;
            return false;
        }
        bool IsInstalled_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";

            return Directory.Exists(DCS_openbeta);                                                   //Is DCS installed?
        }

        //---------- FS2020 ----------
        public void btn_Patch_FS2020_Click()
        {
            if (!IsInstalled_FS2020_ANY())
            {
                MessageBox.Show("FS2020 is not installed on your system.",
                                "Patch aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Create_FS2020_Motion_Exporter_EXE();
            if (IsInstalled_FS2020(false)) Patch_FS2020(false);
            if (IsInstalled_FS2020(true)) Patch_FS2020(true);

        }
        public void btn_Unpatch_FS2020_Click()
        {
            if (!IsInstalled_FS2020_ANY())
            {
                MessageBox.Show("FS2020 is not installed on your system.",
                                "Operation aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                
                IsPatched_FS2020_prop = false;
                return;
            }

            if (IsInstalled_FS2020(false)) unPatch_FS2020(false);
            if (IsInstalled_FS2020(true)) unPatch_FS2020(true);

            if (IsInstalled_FS2020_ANY())
            {
                MessageBox.Show(
                    "Removed FS2020 motion data patch.",
                    "FS2020 unpatched",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

            IsPatched_FS2020_prop = false;
        }
        void Patch_FS2020(bool steam = false)
        {
            switch (steam)
            {
                case false:
                    if (IsPatched_FS2020(false)) unPatch_FS2020(false);
                    break;
                case true:
                    if (IsPatched_FS2020(true)) unPatch_FS2020(true);
                    break;
            }

            string filePath = string.Empty;
            if (steam) filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
            else filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, Resource.exe_xml_Example_BLANK);
            }

            Modify_exe_xml(filePath);

            IsPatched_FS2020_prop = true;

            MessageBox.Show(
                "Patched FS2020 for motion data export.",
                "FS2020 patched",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        void unPatch_FS2020(bool steamVersion = false)
        {
            string filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";
            if (steamVersion)
            {
                filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
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

            IsPatched_FS2020_prop = false;

            //We don't need to delete it. Maybe someone can do something useful with it.
            //Delete_FS2020_Motion_Exporter_EXE();          
        }
        bool IsInstalled_FS2020_ANY()
        {
            return (IsInstalled_FS2020(false) || IsInstalled_FS2020(true));
        }
        bool IsInstalled_FS2020(bool steam = false)
        {
            if (steam)  return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder);
            else        return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder);
        }
        bool IsPatched_FS2020(bool steamVersion = false)
        {
            string filePath = String.Empty;

            if (steamVersion)   filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder;
            else                filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder;

            filePath += @"\exe.xml";

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
        void Create_FS2020_Motion_Exporter_EXE()
        {
            Delete_FS2020_Motion_Exporter_EXE();            //Cleanup

            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_FS2020_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            Directory.CreateDirectory(ExportersFolder);

            var res = Resource.FS2020_MotionExporter;
            using (FileStream fs = new FileStream(Exporter_EXE, FileMode.Create))
            {
                fs.Write(res, 0, res.Length);
            }
        }
        void Delete_FS2020_Motion_Exporter_EXE()
        {
            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_FS2020_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            if (File.Exists(Exporter_EXE))
            {
                File.Delete(Exporter_EXE);
            }
        }
        void Modify_exe_xml(string filePath)
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
        
        //---------- X-Plane ----------
        const string xPlane9 = "x-plane_install.txt";
        const string xPlane10 = "x-plane_install_10.txt";
        const string xPlane11 = "x-plane_install_11.txt";
        const string xPlane12 = "x-plane_install_12.txt";

        public void btn_Patch_XPlane_Click()
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
        public void btn_Unpatch_XPlane_Click()
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
        void Patch_XPlane()
        {
            List<string> allPaths = All_Xplane_Installations();

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

                IsPatched_X_Plane_prop = true;
            }
            catch (Exception)
            {
                MessageBox.Show("I was unable to apply the motion data patch to X-Plane. Usually this happens, when you " +
                                "have X-Plane running while trying to apply the patch. Make sure X-Plane is NOT running! " +
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
        void Unpatch_XPlane()
        {
            List<string> allPaths = All_Xplane_Installations();

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

            string MyPlugin = dialog.FileName + @"\Resources\plugins" + Properties.Settings.Default.Patcher_Xplane_Exporter;

            if (!Directory.Exists(MyPlugin))
            {
                MessageBox.Show(
                    "This looks like an X-Plane installation Folder, but there " +
                    "was no patch to remove. I did not make any changes to your X-Plane installation.",
                    "No patch found",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Directory.Delete(MyPlugin, true);

            IsPatched_X_Plane_prop = false;

            MessageBox.Show("X-Plane was successfully un-patched. No more motion Data " +
                            "is being exported to YAME.",
                            "Patch removed successfully",
                            MessageBoxButton.OK, MessageBoxImage.Information);

        }
        bool IsInstalled_XPlane()
        {
            //Function checks if any version of X-Plane is installed on the computer
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
        bool IsPatched_XPlane()
        {
            List<string> allPaths = All_Xplane_Installations();

            if (allPaths.Count == 0)
            {
                return false;
            }

            string MyPlugin = allPaths.Last()+ @"\Resources\plugins" + Properties.Settings.Default.Patcher_Xplane_Exporter;

            if (Directory.Exists(MyPlugin))
            {
                return true;
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
            //string _tempZipFile = Environment.GetEnvironmentVariable("TEMP") + @"\XPlaneGetter.zip";
            string _tempZipFile = 
                Environment.GetEnvironmentVariable("TEMP") 
                + Properties.Settings.Default.Patcher_Xplane_Exporter + ".zip";
            var res = Resource.XPlane_Motion_Exporter;

            using (FileStream fs = new FileStream(_tempZipFile, FileMode.Create))
            {
                fs.Write(res, 0, res.Length);
            }

            string destination = folder + "\\Resources\\plugins\\";

            if (Directory.Exists(destination + Properties.Settings.Default.Patcher_Xplane_Exporter))
            {
                MessageBox.Show(
                    "X-Plane is already patched for motion data export. But I'm " +
                    "gonna re-apply the patch, just to be sure.",
                    "Overwriting Patch",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                Directory.Delete(destination + Properties.Settings.Default.Patcher_Xplane_Exporter, true);
            }

            ZipFile.ExtractToDirectory(_tempZipFile, destination);
        }
        List<string> All_Xplane_Installations()
        {
            string userFolder = Folders.UserFolder;
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

            return allPaths;
        }

        //---------- Helpers ----------
        static string RemoveLastDirectoryFrom(string FolderPath)
        {
            if (FolderPath.EndsWith("/") || FolderPath.EndsWith("\\"))
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
        public void RefreshPatchStatusOfAllSims()
        {
            IsPatched_DCS_prop = IsPatched_DCS();
            IsPatched_DCS_openbeta_prop = IsPatched_DCS_openbeta();
            IsPatched_FS2020_prop = IsPatched_FS2020();
            IsPatched_X_Plane_prop = IsPatched_XPlane();
        }
    }
}
