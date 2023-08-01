using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YAME.Model
{
    public static class Automator
    {
        static string ConfigFilePath
        {
            get
            {
                return Folders.UserFolder + Properties.Settings.Default.Configurator_ConfigFilePath;
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

        public static void ProcessConfigFile()
        {   
            var mw = Application.Current.MainWindow as MainWindow;
            
            if (IsConfigFilePresent)
            {
                string[] x = File.ReadAllLines(ConfigFilePath);

                foreach (string line in x)
                {
                    string conditionedLine = line.Replace(" ", "");        //remove whitespace

                    string[] tokens = conditionedLine.Split('=');
                    
                    switch (tokens[0])
                    {
                        case "ForceComPortOpen_OnStartUp":
                            mw.engine.aasd_talker.IsOpen = bool.Parse(tokens[1]);
                            break;
                        case "ForceHotRigWarningSilent":
                            Properties.Settings.Default.Configurator_ForceHotRigWarningSilent = bool.Parse(tokens[1]);
                            break;
                        case "MotionControlSelector_OnStartUp":
                            mw.engine.integrator.Lerp_3Way.Command = (Lerp3_Command)Enum.Parse(typeof(Lerp3_Command), tokens[1]);
                            break;
                        case "ShowAboutWindow_OnStartup":
                            Properties.Settings.Default.Configurator_ShowAboutWindowOnStartup = bool.Parse(tokens[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }






    }
}
