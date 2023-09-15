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
                return Folders.UserFolder + Properties.Settings.Default.Automator_ConfigFilePath;
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
                string[] lines = File.ReadAllLines(ConfigFilePath);

                foreach (string line in lines)
                {
                    string conditionedLine;
                    conditionedLine = RemoveWhitespaceFrom(line);
                    conditionedLine = RemoveCommentsFrom(conditionedLine);

                    string[] tokens = conditionedLine.Split('=');
                    
                    switch (tokens[0])
                    {
                        case "ForceComPortOpen_OnStartUp":
                            mw.engine.aasd_talker.IsOpen = bool.Parse(tokens[1]);
                            break;
                        case "ForceHotRigWarningSilent":
                            Properties.Settings.Default.Automator_ForceHotRigWarningSilent = bool.Parse(tokens[1]);
                            break;
                        case "MotionControlSelector_OnStartUp":
                            mw.engine.integrator.Lerp_3Way.Command = (Lerp3_Command)Enum.Parse(typeof(Lerp3_Command), tokens[1]);
                            break;
                        case "ShowAboutWindow_OnStartup":
                            Properties.Settings.Default.Automator_ShowAboutWindowOnStartup = bool.Parse(tokens[1]);
                            break;
                        case "AutoCrashRecovery":
                            Properties.Settings.Default.Automator_AutoCrashRecovery = bool.Parse(tokens[1]);
                            break;
                            //...
                            //...
                            //...
                            //...
                        default:
                            break;
                    }
                }
            }
        }

        //------- Helpers -------
        static string RemoveWhitespaceFrom(string s)
        {
            return s.Replace(" ", "");
        }
        static string RemoveCommentsFrom(string s)
        {
            if (s.Contains("#"))
            {
                int index = s.IndexOf('#');
                return s.Substring(0, index);
            }
            return s;
        }
    }
}
