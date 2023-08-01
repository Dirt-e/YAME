using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAME.Model
{
    public static class Configurator
    {
        static string ConfigFilePath
        {
            get
            {
                return Folders.UserFolder + Properties.Settings.Default.ConfigFilePath;
            }
        }
        static bool IsConfigFilePresent
        {
            get
            {
                if (File.Exists(ConfigFilePath))    return true;
                else                                return false;
            }
        }
        static Dictionary<string, string> Settings = new Dictionary<string, string>();

        public static void ProcessConfigFile()
        {
            if (IsConfigFilePresent)
            {
                var x = File.ReadAllLines(ConfigFilePath);

                foreach (string line in x)
                {
                    line.Replace("  ","") ;                         //remove whitespace

                    string[] tokens = line.Split('=');

                    switch (tokens[0])
                    {
                        case "ForceComPortOpen_OnStartUp":
                            break;
                        case "ForceHotRigWarningSilent":
                            break;
                        case "MotionControlSelector_OnStartUp":
                            break;
                        case "ShowAboutWindow_OnStartup":
                            Properties.Settings.Default.ShowAboutWindowOnStartup = bool.Parse(tokens[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }






    }
}
