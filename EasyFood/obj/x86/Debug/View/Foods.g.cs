﻿#pragma checksum "H:\Programming\Windows Phone\10\EasyFood\EasyFood\View\Foods.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B2623DD6676B1DBD329822DE70E6065"
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
    partial class Foods : 
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
                    this.CategoryListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 41 "..\..\..\View\Foods.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.CategoryListView).SelectionChanged += this.CategoryListView_SelectionChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.ItemListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 3:
                {
                    this.OrderListTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4:
                {
                    this.OrderListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 5:
                {
                    this.SubmitCommentButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 97 "..\..\..\View\Foods.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SubmitCommentButton).Click += this.SubmitCommentButton_Click;
                    #line default
                }
                break;
            case 6:
                {
                    this.WarningTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
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

