﻿#pragma checksum "..\..\Polzovatel.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "06CEDAC69851BC29540BEECAC501DEE283C47CDD29ECA4AD9E6A5215DABB8A53"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using Technics;


namespace Technics {
    
    
    /// <summary>
    /// Polzovatel
    /// </summary>
    public partial class Polzovatel : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox poisk;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sort_ter;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ott;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox doo;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sort;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox pom;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Polzovatel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listviewTechnics;
        
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
            System.Uri resourceLocater = new System.Uri("/Technics;component/polzovatel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Polzovatel.xaml"
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
            
            #line 10 "..\..\Polzovatel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 11 "..\..\Polzovatel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 12 "..\..\Polzovatel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 4:
            this.poisk = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 15 "..\..\Polzovatel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sort_ter = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\Polzovatel.xaml"
            this.sort_ter.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.sort_ter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ott = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.doo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            
            #line 24 "..\..\Polzovatel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_4);
            
            #line default
            #line hidden
            return;
            case 10:
            this.sort = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\Polzovatel.xaml"
            this.sort.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.pom = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\Polzovatel.xaml"
            this.pom.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.pom_TextChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.listviewTechnics = ((System.Windows.Controls.ListView)(target));
            
            #line 35 "..\..\Polzovatel.xaml"
            this.listviewTechnics.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

