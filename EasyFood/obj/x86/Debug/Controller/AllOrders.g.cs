﻿#pragma checksum "H:\Programming\Windows Phone\10\EasyFood\EasyFood\Controller\AllOrders.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EE69E617606D9A7A692A3D88A984167A"
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
    partial class AllOrders : 
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
                    this.AllOrdersListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    #line 18 "..\..\..\Controller\AllOrders.xaml"
                    ((global::Windows.UI.Xaml.Controls.ListView)this.AllOrdersListView).SelectionChanged += this.AllOrdersListView_SelectionChanged;
                    #line default
                }
                break;
            case 2:
                {
                    this.AllOrdersDetailsFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
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

