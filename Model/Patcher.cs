using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using YAME.Properties;

namespace YAME.Model
{
    public static class Patcher
    {
        public static void PatchDCS()
        {
            string DCS = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS";
            
            if (Directory.Exists(DCS))                                                          //Is DCS installed?
            {
                Directory.CreateDirectory(DCS + "\\Scripts\\Hooks");                            //Make sure directory is there 

                var content = YAME.Resource.YAME_Export_Hook;                                   //Grab content...
                File.WriteAllBytes(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua", content);    //...and write it to file.

                MessageBox.Show("Patched DCS for motion data export.",                          //Brag about it :-)
                                "DCS patched",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("DCS is not installed on your system.",
                                "DCS patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public static void PatchDCS_openbeta()
        {
            string DCS_ob = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS.openbeta";
            if (Directory.Exists(DCS_ob))                                                       //Is DCS installed?
            {
                Directory.CreateDirectory(DCS_ob + "\\Scripts\\Hooks");                         //Make sure directory is there 

                var content = YAME.Resource.YAME_Export_Hook;                                   //Grab content...
                File.WriteAllBytes(DCS_ob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua", content); //...and write it to file.

                MessageBox.Show("Patched DCS.openbeta for motion data export.",                 //Brag about it :-)
                                "DCS.openbeta patched",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("DCS.openbeta is not installed on your system.",
                                "DCS.openbeta patch failed!",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public static void UnPatchDCS()
        {
            string DCS = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS";

            if (!Directory.Exists(DCS))
            {
                MessageBox.Show("Could not patch DCS. It is not installed on your system.",
                                "DCS not found",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (File.Exists(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua"))
            {
                File.Delete(DCS + "\\Scripts\\Hooks\\YAME_Export_Hook.lua");
                
                MessageBox.Show("Unpatched DCS. Motion data export suspended.",
                            "DCS patch removed",
                            MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("DCS was already unpatched.",
                            "DCS patch failed!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }
        public static void UnPatchDCS_openbeta()
        {
            string DCS_ob = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Saved Games\\DCS.openbeta";

            if (!Directory.Exists(DCS_ob))
            {
                MessageBox.Show("Could not unpatch DCS.openbeta. It is not installed on your system.",
                                "DCS.openbeta not found",
                                MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (File.Exists(DCS_ob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua"))
            {
                File.Delete(DCS_ob + "\\Scripts\\Hooks\\YAME_Export_Hook.lua");
                
                MessageBox.Show("Unpatched DCS.openbeta. Motion data export suspended.",
                            "DCS.openbeta patch removed",
                            MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("DCS.openbeta was already unpatched.",
                            "DCS.openbeta unpatch failed!",
                            MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
