﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YAME {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YAME.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [General]
        ///Enabled=1
        ///
        ///[Connection]
        ///Host=127.0.0.1
        ///Port=31090
        ///
        ///[Misc]
        ///SendIntervalMs=10
        ///ExtendedData=1
        ///LogToFile=0.
        /// </summary>
        public static string Condor2_UDPini {
            get {
                return ResourceManager.GetString("Condor2_UDPini", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;Windows-1252&quot;?&gt;
        ///&lt;SimBase.Document Type=&quot;SimConnect&quot; version=&quot;1,0&quot;&gt;
        ///	&lt;Descr&gt;SimConnect&lt;/Descr&gt;
        ///	&lt;Filename&gt;SimConnect.xml&lt;/Filename&gt;
        ///	&lt;Disabled&gt;False&lt;/Disabled&gt;
        ///  
        ///&lt;/SimBase.Document&gt;.
        /// </summary>
        public static string exe_xml_Example_BLANK {
            get {
                return ResourceManager.GetString("exe_xml_Example_BLANK", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] FS2020_MotionExporter {
            get {
                object obj = ResourceManager.GetObject("FS2020_MotionExporter", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] iRacing_MotionExporter {
            get {
                object obj = ResourceManager.GetObject("iRacing_MotionExporter", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] XPlane_Motion_Exporter {
            get {
                object obj = ResourceManager.GetObject("XPlane_Motion_Exporter", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] YAME {
            get {
                object obj = ResourceManager.GetObject("YAME", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        public static byte[] YAME_Export_Hook {
            get {
                object obj = ResourceManager.GetObject("YAME_Export_Hook", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
