﻿#pragma checksum "..\..\..\View\SourceSelect_Window.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "223A4EE66D1B4DCB28BC1D14CDA78AF4567DB176A1A63898625E346A8BEEF67E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using YAME.Model;
using YAME.View;
using YAME.View.Converters;
using YAME.View.UserControls;


namespace YAME.View {
    
    
    /// <summary>
    /// SourceSelect_Window
    /// </summary>
    public partial class SourceSelect_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbbx_Source;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_DCS;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_DCS;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_DCSopenbeta;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_DCSopenbeta;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_FS2020;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_FS2020;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_XPlane;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_XPlane;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_Condor2;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_Condor2;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rct_IsPatched_DCS;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rct_IsPatched_DCS_openbeta;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rct_IsPatched_FS2020;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rct_IsPatched_X_Plane;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\View\SourceSelect_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rct_IsPatched_Condor2;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/YAME;component/view/sourceselect_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\SourceSelect_Window.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\..\View\SourceSelect_Window.xaml"
            ((YAME.View.SourceSelect_Window)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\View\SourceSelect_Window.xaml"
            ((YAME.View.SourceSelect_Window)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\View\SourceSelect_Window.xaml"
            ((YAME.View.SourceSelect_Window)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\View\SourceSelect_Window.xaml"
            ((YAME.View.SourceSelect_Window)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbbx_Source = ((System.Windows.Controls.ComboBox)(target));
            
            #line 44 "..\..\..\View\SourceSelect_Window.xaml"
            this.cmbbx_Source.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.cmbbx_Source_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Patch_DCS = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Patch_DCS.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_DCS_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Unpatch_DCS = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Unpatch_DCS.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_DCS_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Patch_DCSopenbeta = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Patch_DCSopenbeta.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_DCS_openbeta_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_Unpatch_DCSopenbeta = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Unpatch_DCSopenbeta.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_DCS_openbeta_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Patch_FS2020 = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Patch_FS2020.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_FS2020_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_Unpatch_FS2020 = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Unpatch_FS2020.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_FS2020_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btn_Patch_XPlane = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Patch_XPlane.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_XPlane_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_Unpatch_XPlane = ((System.Windows.Controls.Button)(target));
            
            #line 80 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Unpatch_XPlane.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_XPlane_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btn_Patch_Condor2 = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Patch_Condor2.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_Condor2_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btn_Unpatch_Condor2 = ((System.Windows.Controls.Button)(target));
            
            #line 88 "..\..\..\View\SourceSelect_Window.xaml"
            this.btn_Unpatch_Condor2.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_Condor2_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.rct_IsPatched_DCS = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 14:
            this.rct_IsPatched_DCS_openbeta = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 15:
            this.rct_IsPatched_FS2020 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 16:
            this.rct_IsPatched_X_Plane = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 17:
            this.rct_IsPatched_Condor2 = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

