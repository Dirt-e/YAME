﻿#pragma checksum "..\..\..\View\CrashDetectorWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D30F9AAB4975683B3D87248A23EE2FEB763069A8886BCC5A71096C5F5331FBB9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MOTUS.View;
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


namespace MOTUS.View {
    
    
    /// <summary>
    /// CrashDetectorWindow
    /// </summary>
    public partial class CrashDetectorWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Ax_Threshold;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Ay_Threshold;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Az_Threshold;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Wx_Threshold;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Wy_Threshold;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbx_Wz_Threshold;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Indicator;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblk_Line1;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\View\CrashDetectorWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtblk_Line2;
        
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
            System.Uri resourceLocater = new System.Uri("/MOTUS;component/view/crashdetectorwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\CrashDetectorWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 10 "..\..\..\View\CrashDetectorWindow.xaml"
            ((MOTUS.View.CrashDetectorWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtbx_Ax_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtbx_Ay_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtbx_Az_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtbx_Wx_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtbx_Wy_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtbx_Wz_Threshold = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btn_Indicator = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.txtblk_Line1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.txtblk_Line2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
