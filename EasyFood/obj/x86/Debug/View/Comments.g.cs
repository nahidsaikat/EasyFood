﻿#pragma checksum "H:\Programming\Windows Phone\10\EasyFood\EasyFood\View\Comments.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA19086CD336B8E31502ADAA66DA00B2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyFood.View
{
    partial class Comments : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.WarningTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2:
                {
                    this.CategoryComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 48 "..\..\..\View\Comments.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CategoryComboBox).SelectionChanged += this.CategoryComboBox_SelectionChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.ItemComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 63 "..\..\..\View\Comments.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ItemComboBox).SelectionChanged += this.ItemComboBox_SelectionChanged;
                    #line default
                }
                break;
            case 4:
                {
                    this.CommentTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.SubmitCommentButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 82 "..\..\..\View\Comments.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SubmitCommentButton).Click += this.SubmitCommentButton_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

