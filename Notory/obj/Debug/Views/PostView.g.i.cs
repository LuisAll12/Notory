﻿#pragma checksum "..\..\..\Views\PostView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "54C1D4437D22AA49FD424AFAB00548240D38C998A3F7D1082B16CAE37CCA3963"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Notory.Views;
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


namespace Notory.Views {
    
    
    /// <summary>
    /// PostView
    /// </summary>
    public partial class PostView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 83 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FontFamilyComboBox;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FontSizeComboBox;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BoldButton;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ItalicButton;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UnderLineButton;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OptionsButton;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\Views\PostView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox Editor;
        
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
            System.Uri resourceLocater = new System.Uri("/Notory;component/views/postview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\PostView.xaml"
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
            this.FontFamilyComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\..\Views\PostView.xaml"
            this.FontFamilyComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FontFamilyComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FontSizeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 84 "..\..\..\Views\PostView.xaml"
            this.FontSizeComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FontSizeComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BoldButton = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\Views\PostView.xaml"
            this.BoldButton.Click += new System.Windows.RoutedEventHandler(this.BoldButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ItalicButton = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\Views\PostView.xaml"
            this.ItalicButton.Click += new System.Windows.RoutedEventHandler(this.ItalicButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.UnderLineButton = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\..\Views\PostView.xaml"
            this.UnderLineButton.Click += new System.Windows.RoutedEventHandler(this.UnderlineButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 88 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ForegroundColorButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 89 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BackgroundColorButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 90 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CheckboxButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 91 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BulletListButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 92 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NumberedListButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.OptionsButton = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            
            #line 99 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 100 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 101 "..\..\..\Views\PostView.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.Editor = ((System.Windows.Controls.RichTextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

