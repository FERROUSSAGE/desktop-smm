#pragma checksum "..\..\..\Pages\Mail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "69A6CC45F9BA5522957D071401DC0FE86A319166860B6948837CAE1B7CA2971B"
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
using desktop_smm.Pages;


namespace desktop_smm.Pages {
    
    
    /// <summary>
    /// Mail
    /// </summary>
    public partial class Mail : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridButtons;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border endDayPattern;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border incorrectPattern;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbMailTo;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCaption;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbHtml;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\Pages\Mail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSendMail;
        
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
            System.Uri resourceLocater = new System.Uri("/desktop-smm;component/pages/mail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Mail.xaml"
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
            this.gridButtons = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.endDayPattern = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.incorrectPattern = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.tbMailTo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbCaption = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbHtml = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnSendMail = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\Pages\Mail.xaml"
            this.btnSendMail.Click += new System.Windows.RoutedEventHandler(this.btnSendMail_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

