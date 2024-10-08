﻿using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using YAME.Model;
using static Utility;

namespace YAME.Model
{
    public class Patcher : MyObject
    {
        #region ViewModel
        bool _isPatchedDCS_prop;
        public bool IsPatched_DCS_prop
        {
            get { return _isPatchedDCS_prop; }
            private set { _isPatchedDCS_prop = value; OnPropertyChanged(nameof(IsPatched_DCS_prop)); }
        }

        bool _isPatchedDCS_openbeta_prop;
        public bool IsPatched_DCS_openbeta_prop
        {
            get { return _isPatchedDCS_openbeta_prop; }
            private set { _isPatchedDCS_openbeta_prop = value; OnPropertyChanged(nameof(IsPatched_DCS_openbeta_prop)); }
        }

        //bool _isPatchedFS2020_prop;
        //public bool IsPatched_FS2020_prop
        //{
        //    get { return _isPatchedFS2020_prop; }
        //    private set { _isPatchedFS2020_prop = value; OnPropertyChanged(nameof(IsPatched_FS2020_prop)); }
        //}

        bool _isPatchedX_Plane_prop;
        public bool IsPatched_X_Plane_prop
        {
            get { return _isPatchedX_Plane_prop; }
            private set { _isPatchedX_Plane_prop = value; OnPropertyChanged(nameof(IsPatched_X_Plane_prop)); }
        }

        bool _isPatched_Condor2_prop;
        public bool IsPatched_Condor2_prop 
        { 
            get { return _isPatched_Condor2_prop; }
            private set { _isPatched_Condor2_prop = value; OnPropertyChanged(nameof(IsPatched_Condor2_prop)); } 
        }
        #endregion

        //Process Exporter_Relay;
        DispatcherTimer timer;
        List<Process> ExporterProcesses;

        public Patcher()
        {
            RefreshPatchStatusOfAllSims();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5000);
            timer.Tick += delegate
            {
                RefreshPatchStatusOfAllSims();
            };
            timer.Start();

            ExporterProcesses = new List<Process>();
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
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_Script;

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
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_Script;

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
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_Script;

            if (File.Exists(ExportScript))
            {
                File.Delete(ExportScript);
            }
            
            IsPatched_DCS_prop = false;
        }
        bool IsPatched_DCS()
        {
            string DCS = Folders.SavedGamesFolder + @"\DCS";
            string ExportScript = DCS + Properties.Settings.Default.Patcher_DCS_Script;

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
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_Script;

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
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_Script;

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
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_Script;

            if (File.Exists(ExportScript))
            {
                File.Delete(ExportScript);
            }
            IsPatched_DCS_openbeta_prop = false;
        }
        bool IsPatched_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";
            string ExportScript = DCS_openbeta + Properties.Settings.Default.Patcher_DCS_Script;

            if (File.Exists(ExportScript)) return true;
            return false;
        }
        bool IsInstalled_DCS_openbeta()
        {
            string DCS_openbeta = Folders.SavedGamesFolder + @"\DCS.openbeta";

            return Directory.Exists(DCS_openbeta);                                                   //Is DCS installed?
        }

        //---------- FS2020 ----------
        #region Unpatch FS2020 deprecated
        //void Modify_exe_xml(string filePath)
        //{
        //    //Open XML document:
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);

        //    //Create all the XML elements:
        //    XmlElement Name = doc.CreateElement("Name");
        //    Name.InnerText = "YAME Motion Data Exporter";

        //    XmlElement Disabled = doc.CreateElement("Disabled");
        //    Disabled.InnerText = "False";

        //    XmlElement Path = doc.CreateElement("Path");
        //    Path.InnerText = Folders.UserFolder
        //        + Properties.Settings.Default.Patcher_ExportersFolder
        //        + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

        //    XmlElement CommandLine = doc.CreateElement("CommandLine");
        //    CommandLine.InnerText = " ";

        //    //Combine elements to one 'Launch.Addon' element
        //    XmlElement LaunchAddon = doc.CreateElement("Launch.Addon");
        //    LaunchAddon.AppendChild(Name);
        //    LaunchAddon.AppendChild(Disabled);
        //    LaunchAddon.AppendChild(Path);
        //    LaunchAddon.AppendChild(CommandLine);

        //    //...and append it to the parent element ('Simbase.Document')
        //    XmlNode SimBaseDocument = doc.SelectSingleNode("SimBase.Document");
        //    SimBaseDocument.AppendChild(LaunchAddon);

        //    doc.Save(filePath);
        //} 
        
        //public void btn_Unpatch_FS2020_Click()
        //{
        //    if (IsInstalled_FS2020(false))  unPatch_FS2020(false);
        //    if (IsInstalled_FS2020(true))   unPatch_FS2020(true);
        //}
        //void unPatch_FS2020(bool steamVersion = false)
        //{
        //    string filePath;

        //    if (steamVersion)   filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder + @"\exe.xml";
        //    else                filePath = Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder + @"\exe.xml";
            
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(filePath);

        //    XmlNode SimBaseDocument = doc.SelectSingleNode("SimBase.Document");
        //    if (SimBaseDocument != null)
        //    {
        //        XmlNodeList Nodes = SimBaseDocument.ChildNodes;
        //        foreach (XmlNode Node in Nodes)
        //        {
        //            if (Node.Name == "Launch.Addon")
        //            {
        //                XmlNodeList LaunchNodes = Node.ChildNodes;
        //                foreach (XmlNode LaunchNode in LaunchNodes)
        //                {
        //                    if (LaunchNode.InnerText == "YAME Motion Data Exporter")
        //                    {
        //                        SimBaseDocument.RemoveChild(Node);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    doc.Save(filePath);        
        //}
        //bool IsInstalled_FS2020(bool steam = false)
        //{
        //    if (steam)  return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STEAM_Folder);
        //    else        return Directory.Exists(Folders.UserFolder + Properties.Settings.Default.Patcher_FS2020_STORE_Folder);
        //}
        #endregion
        public void Run_FS2020_Motion_Exporter()
        {
            KillAllMotionExporters();
            Create_FS2020_Motion_Exporter_EXE();

            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            //Process p = new Process();
            //ProcessStartInfo info = new ProcessStartInfo(Exporter_EXE);
            //info.CreateNoWindow = true;
            //info.UseShellExecute = false;
            //p = Process.Start(info);

            Process.Start(Exporter_EXE);
        }
        public void Create_FS2020_Motion_Exporter_EXE()
        {
            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_FS2020_Exporter_exe;

            if (!File.Exists(Exporter_EXE))
            {
                Directory.CreateDirectory(ExportersFolder);

                var res = Resource.FS2020_MotionExporter;
                using (FileStream fs = new FileStream(Exporter_EXE, FileMode.Create))
                {
                    fs.Write(res, 0, res.Length);
                }
            }
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
                ExtractXPlanePluginToFolder(dialog.FileName);

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
            string installs_xp12 = userFolder + "\\AppData\\Local\\x-plane_install_12.txt";

            List<string> allFiles = new List<string>();
            List<string> allPaths = new List<string>();
            allPaths.DefaultIfEmpty<string>("C:");

            allFiles.Add(installs_xp9);
            allFiles.Add(installs_xp10);
            allFiles.Add(installs_xp11);
            allFiles.Add(installs_xp12);

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
        void ExtractXPlanePluginToFolder(string folder)
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
            allFiles.Add(installs_xp12);

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

        //----------- iRacing ----------
        public void Run_iRacing_Motion_Exporter()
        {
            KillAllMotionExporters();
            Create_iRacing_Motion_Exporter_EXE();

            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_iRacing_Exporter_exe;

            //Process p = new Process();
            //ProcessStartInfo info = new ProcessStartInfo(Exporter_EXE);
            //info.CreateNoWindow = true;
            //info.UseShellExecute = false;
            //p = Process.Start(info);

            Process.Start(Exporter_EXE);

        }
        public void Create_iRacing_Motion_Exporter_EXE()
        {
            string userFolder = Folders.UserFolder;
            string ExportersFolder = userFolder + Properties.Settings.Default.Patcher_ExportersFolder;
            string Exporter_EXE = ExportersFolder + Properties.Settings.Default.Patcher_iRacing_Exporter_exe;

            if (!File.Exists(Exporter_EXE))
            {
                Directory.CreateDirectory(ExportersFolder);

                var res = Resource.iRacing_MotionExporter;
                using (FileStream fs = new FileStream(Exporter_EXE, FileMode.Create))
                {
                    fs.Write(res, 0, res.Length);
                }
            }
        }

        //---------- Condor2 ---------
        public void btn_Patch_Condor2_Click()
        {
            string directory = @"C:\Condor2\";

            MessageBox.Show("OK, I get it. You want to patch Condor2 so that it sends you motion data. \n" +
                            "I can do that for you! All you gotta do is " +
                            "point me to your Condor2 installation folder.\n\n" +
                            "You should normally find it here:\n" +
                            $"{directory}"  + "\n\n" +
                            "Now point me to your Condor2 install folder, will you.",
                            "Need your help!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = directory,
                IsFolderPicker = true,
            };
            var Result = dialog.ShowDialog();

            if (Result != CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You aborted the patch routine.\n\n" +
                                "Condor2 was NOT patched!",
                                "Patch aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;                                                                     //End Process
            }
            if (!Isconfirmed_Condor2_Folder(dialog.FileName))
            {
                MessageBox.Show("This is not the Condor2 installation folder! It does not contain " +
                                "all the usual subfolder structure that I expect to see :-/\n\n" +
                                "Condor2 was NOT patched!",
                                "Not a Condor2 installation Folder",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                WriteCondor2_UDPini_ToFolder(dialog.FileName);
            }
            catch (Exception)
            {
                MessageBox.Show("I was unable to apply the motion data patch to Condor2. Usually this happens, when you " +
                                "have Condor 2 running while trying to apply the patch. Make sure Condor2 is NOT running! " +
                                "You might even have to reboot to be absolutely sure. \n\n" +
                                "Condor2 was NOT patched!",
                                "Something went wrong",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsPatched_Condor2_prop = true;

            MessageBox.Show("Condor2 was successfully patched for motion data export to YAME. " +
                            "You may need to restart Condor2 for the changes to take effect.",
                            "Patch successful",
                            MessageBoxButton.OK, MessageBoxImage.Information);

        }
        public void btn_Unpatch_Condor2_Click()
        {
            string probable_directory = @"C:\Condor2\";
            
            MessageBox.Show("You want to unpatch Condor2 so that it WON'T send motion data anymore? \n" +
                            "I can do that for you! All you gotta do is " +
                            "point me to your Condor2 installation folder.\n\n" +
                            "You should normally find it here:\n" +
                            $"{probable_directory}" + "\n\n" +
                            "Now point me to your Condor2 install folder, will you.",
                            "Need your help!",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            CommonOpenFileDialog dialog = new CommonOpenFileDialog()
            {
                InitialDirectory = probable_directory,
                IsFolderPicker = true,
            };
            var Result = dialog.ShowDialog();

            if (Result != CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You aborted the unpatch routine.\n\n" +
                                "I made no changes to your Condor2 installation",
                                "Process aborted",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;                                                                     //End Process
            }
            if (!Isconfirmed_Condor2_Folder(dialog.FileName))
            {
                MessageBox.Show("This is not the Condor2 installation folder! It does not contain " +
                                "all the usual subfolder structure that I expect to see :-/\n\n" +
                                "Condor2 was NOT unpatched! I made NO changes to your Condor2 installation.",
                                "Not a Condor2 installation Folder",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //string UDPini_file = dialog.FileName + @"\Settings\UDP.ini";
            string UDPini_file = Path.Combine(dialog.FileName, "Settings" , "UDP.ini");
            string Backupfile = UDPini_file + "_bak";

            try
            {
                if (File.Exists(Backupfile))        //Restore backup
                {
                    File.Copy(Backupfile, UDPini_file, true);
                    File.Delete(Backupfile);
                }
                else                                //Just "Enabled=0"
                {
                    string[] lines = File.ReadAllLines(UDPini_file);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] == "[General]" && lines[i+1].StartsWith("Enabled"))
                        {
                            lines[i + 1] = "Enabled=0";
                            break;
                        }
                    }

                    File.WriteAllLines(UDPini_file, lines);  
                }
            }
            catch (Exception)
            {
                MessageBox.Show("I was unable to unpatch Condor2. Make sure that the file" +
                                @"Condor2\Settings\UDP.ini is NOT opened in any program!" +
                                "You might even have to reboot to be absolutely sure. \n\n" +
                                "Condor2 was NOT unpatched! I made no changes to your Condor2 installation.",
                                "Something went wrong",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsPatched_Condor2_prop = false;

            MessageBox.Show("Condor2 was successfully patched for motion data export to YAME. " +
                            "You may need to restart Condor2 for the changes to take effect.",
                            "Patch successful",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
        bool IsPatched_Condor2()
        {
            string UDP_ini_path = @"C:\Condor2\Settings\UDP.ini";

            if (File.Exists(UDP_ini_path))
            {
                string UDPini_OnHardDrive = File.ReadAllText(@"C:\Condor2\Settings\UDP.ini");
                string UDPini_Patched = Resource.Condor2_UDPini;

                return UDPini_OnHardDrive == UDPini_Patched;
            }
            return false;
            
        }
        bool Isconfirmed_Condor2_Folder(string path)
        {
            if (Directory.Exists(path + @"\Landscapes") && Directory.Exists(path + @"\Effects"))
            {
                return true;
            }
            return false;
        }
        void WriteCondor2_UDPini_ToFolder(string fileName)
        {
            string UDPini_file = fileName + @"\Settings\UDP.ini";
            string Backupfile = UDPini_file + "_bak";

            if (!File.Exists(Backupfile))
            {
                File.Copy(UDPini_file, Backupfile, false);
            }
            
            File.WriteAllText(UDPini_file, Resource.Condor2_UDPini);
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
            //IsPatched_FS2020_prop = IsPatched_FS2020();
            IsPatched_X_Plane_prop = IsPatched_XPlane();
            IsPatched_Condor2_prop = IsPatched_Condor2();
        }
        public void KillAllMotionExporters()
        {
            var processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                if (p.ProcessName.Contains("MotionExporter"))
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception)
                    {
                        //ignore :-)
                    }
                }
            }
        }
    }
}
