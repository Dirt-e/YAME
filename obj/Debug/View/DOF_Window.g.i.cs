﻿#pragma checksum "..\..\..\View\DOF_Window.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6D6D58441E7CAAB6709B10B65F10A8C4B5162F326C0FF36923C18322619F251A"
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
    /// DOF_Window
    /// </summary>
    public partial class DOF_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 84 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Roll;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Yaw;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Pitch;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Surge;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Pitch_LFC;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Heave;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Sway;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\View\DOF_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_DOF_Roll_LFC;
        
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
            System.Uri resourceLocater = new System.Uri("/YAME;component/view/dof_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\DOF_Window.xaml"
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
            
            #line 9 "..\..\..\View\DOF_Window.xaml"
            ((YAME.View.DOF_Window)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\View\DOF_Window.xaml"
            ((YAME.View.DOF_Window)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\View\DOF_Window.xaml"
            ((YAME.View.DOF_Window)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\View\DOF_Window.xaml"
            ((YAME.View.DOF_Window)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.sld_DOF_Roll = ((System.Windows.Controls.Slider)(target));
            
            #line 84 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Roll.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.sld_DOF_Yaw = ((System.Windows.Controls.Slider)(target));
            
            #line 85 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Yaw.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.sld_DOF_Pitch = ((System.Windows.Controls.Slider)(target));
            
            #line 86 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Pitch.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.sld_DOF_Surge = ((System.Windows.Controls.Slider)(target));
            
            #line 87 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Surge.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sld_DOF_Pitch_LFC = ((System.Windows.Controls.Slider)(target));
            
            #line 88 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Pitch_LFC.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.sld_DOF_Heave = ((System.Windows.Controls.Slider)(target));
            
            #line 89 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Heave.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.sld_DOF_Sway = ((System.Windows.Controls.Slider)(target));
            
            #line 90 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Sway.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.sld_DOF_Roll_LFC = ((System.Windows.Controls.Slider)(target));
            
            #line 91 "..\..\..\View\DOF_Window.xaml"
            this.sld_DOF_Roll_LFC.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

