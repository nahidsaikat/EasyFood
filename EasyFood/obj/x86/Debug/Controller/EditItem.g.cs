﻿#pragma checksum "H:\Programming\Windows Phone\10\EasyFood\EasyFood\Controller\EditItem.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B50C958DC8C8F746D8284DB89DB770D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EasyFood.Controller
{
    partial class EditItem : 
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
                    #line 57 "..\..\..\Controller\EditItem.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CategoryComboBox).SelectionChanged += this.CategoryComboBox_SelectionChanged;
                    #line default
                }
                break;
            case 3:
                {
                    this.ItemComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 71 "..\..\..\Controller\EditItem.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.ItemComboBox).SelectionChanged += this.ItemComboBox_SelectionChanged;
                    #line default
                }
                break;
            case 4:
                {
                    this.CategoryItemComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    #line 87 "..\..\..\Controller\EditItem.xaml"
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.CategoryItemComboBox).SelectionChanged += this.CategoryItemComboBox_SelectionChanged;
                    #line default
                }
                break;
            case 5:
                {
                    this.AddItemTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.DescriptionTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.SizeTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 8:
                {
                    this.PrizeTextBox = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 9:
                {
                    this.AvailableCheckBox = (global::Windows.UI.Xaml.Controls.CheckBox)(target);
                }
                break;
            case 10:
                {
                    this.AddItemButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 141 "..\..\..\Controller\EditItem.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.AddItemButton).Click += this.AddItemButton_Click;
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
