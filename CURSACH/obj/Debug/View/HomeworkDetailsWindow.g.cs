﻿#pragma checksum "..\..\..\View\HomeworkDetailsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B6429E1C213CE868A20B6503A58E8F70601FB0E47E0636244A0716EBFCC57688"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CURSACH.View;
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


namespace CURSACH.View {
    
    
    /// <summary>
    /// HomeworkDetailsWindow
    /// </summary>
    public partial class HomeworkDetailsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 90 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCloseHomeworkDetails;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Description;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Solution;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbLanguage;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbStatus;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteHomework;
        
        #line default
        #line hidden
        
        
        #line 282 "..\..\..\View\HomeworkDetailsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/CURSACH;component/view/homeworkdetailswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\HomeworkDetailsWindow.xaml"
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
            this.btnCloseHomeworkDetails = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\View\HomeworkDetailsWindow.xaml"
            this.btnCloseHomeworkDetails.Click += new System.Windows.RoutedEventHandler(this.btnCloseWordDetails_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Solution = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cbLanguage = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.cbStatus = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnDeleteHomework = ((System.Windows.Controls.Button)(target));
            
            #line 260 "..\..\..\View\HomeworkDetailsWindow.xaml"
            this.btnDeleteHomework.Click += new System.Windows.RoutedEventHandler(this.btnDeleteHomework_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 290 "..\..\..\View\HomeworkDetailsWindow.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

