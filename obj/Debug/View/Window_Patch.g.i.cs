﻿#pragma checksum "..\..\..\View\Window_Patch.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FDBC86E487D3D8E22912077E1010A27CFDEB25F95CE282B8F2638F05A030BBC2"
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
using YAME.View;


namespace YAME.View {
    
    
    /// <summary>
    /// Window_Patcher
    /// </summary>
    public partial class Window_Patcher : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_DCS;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_DCS;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_DCSopenbeta;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_DCSopenbeta;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Patch_XPlane;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Unpatch_XPlane;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\View\Window_Patch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Close;
        
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
            System.Uri resourceLocater = new System.Uri("/YAME;component/view/window_patch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Window_Patch.xaml"
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
            this.btn_Patch_DCS = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\View\Window_Patch.xaml"
            this.btn_Patch_DCS.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_DCS_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_Unpatch_DCS = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\View\Window_Patch.xaml"
            this.btn_Unpatch_DCS.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_DCS_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_Patch_DCSopenbeta = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\View\Window_Patch.xaml"
            this.btn_Patch_DCSopenbeta.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_DCSopenbeta_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Unpatch_DCSopenbeta = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\View\Window_Patch.xaml"
            this.btn_Unpatch_DCSopenbeta.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_DCSopenbeta_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Patch_XPlane = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\View\Window_Patch.xaml"
            this.btn_Patch_XPlane.Click += new System.Windows.RoutedEventHandler(this.btn_Patch_XPlane_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_Unpatch_XPlane = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\View\Window_Patch.xaml"
            this.btn_Unpatch_XPlane.Click += new System.Windows.RoutedEventHandler(this.btn_Unpatch_XPlane_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Close = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\View\Window_Patch.xaml"
            this.btn_Close.Click += new System.Windows.RoutedEventHandler(this.btn_Close_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
