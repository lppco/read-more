﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 10.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace UITest
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public partial class UIMap
    {
        
        /// <summary>
        /// NewNode - Use 'NewNodeParams' to pass parameters into this method.
        /// </summary>
        public void NewNode()
        {
            #region Variable Declarations
            WinEdit uITxtNewNodeNameEdit = this.UI新建节点Window.UITxtNewNodeNameWindow.UITxtNewNodeNameEdit;
            WinTreeItem uIRootTreeItem = this.UI读善其身eBook信息管理软件Beta版Window.UITvwMainWindow.UI读善其身TreeItem.UIRootTreeItem;
            #endregion

            // Last mouse action was not recorded.

            // Last keyboard action was not recorded.

            // Type 'root' in 'txtNewNodeName' text box
            uITxtNewNodeNameEdit.Text = this.NewNodeParams.UITxtNewNodeNameEditText;

            // Last mouse action was not recorded.

            // Right-Click '读善其身' -> 'root' tree item
            Mouse.Click(uIRootTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(-2, 9));

            // Last mouse action was not recorded.

            // Type 'branch' in 'txtNewNodeName' text box
            uITxtNewNodeNameEdit.Text = this.NewNodeParams.UITxtNewNodeNameEditText1;

            // Last mouse action was not recorded.
        }
        
        /// <summary>
        /// AssertNewNode - Use 'AssertNewNodeExpectedValues' to pass parameters into this method.
        /// </summary>
        public void AssertNewNode()
        {
            #region Variable Declarations
            WinWindow uI新建节点Window = this.UI新建节点Window;
            #endregion

            // Verify that '新建节点' window's property 'Name' equals '新建节点'
            Assert.AreEqual(this.AssertNewNodeExpectedValues.UI新建节点WindowName, uI新建节点Window.Name);
        }
        
        /// <summary>
        /// AddFile
        /// </summary>
        public void AddFile()
        {
            #region Variable Declarations
            WinTreeItem uIBranchTreeItem = this.UI读善其身eBook信息管理软件Beta版Window.UITvwMainWindow.UI读善其身TreeItem.UIRootTreeItem.UIBranchTreeItem;
            #endregion

            // Last mouse action was not recorded.

            // Right-Click '读善其身' -> 'root' -> 'branch' tree item
            Mouse.Click(uIBranchTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(19, 9));

            // Right-Click '读善其身' -> 'root' -> 'branch' tree item
            Mouse.Click(uIBranchTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(-4, 9));

            // Last mouse action was not recorded.
        }
        
        /// <summary>
        /// DeleteNode
        /// </summary>
        public void DeleteNode()
        {
            #region Variable Declarations
            WinTreeItem uIErrorloglogTreeItem = this.UI读善其身eBook信息管理软件Beta版Window.UITvwMainWindow.UI读善其身TreeItem.UIRootTreeItem.UIBranchTreeItem.UIErrorloglogTreeItem;
            WinTreeItem uITestResultsTreeItem = this.UI读善其身eBook信息管理软件Beta版Window.UITvwMainWindow.UI读善其身TreeItem.UIRootTreeItem.UITestResultsTreeItem;
            WinTreeItem uI读善其身TreeItem = this.UI读善其身eBook信息管理软件Beta版Window.UITvwMainWindow.UI读善其身TreeItem;
            #endregion

            // Last mouse action was not recorded.

            // Right-Click '读善其身' -> 'root' -> 'branch' -> 'errorlog.log' tree item
            Mouse.Click(uIErrorloglogTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(39, 8));

            // Last mouse action was not recorded.

            // Right-Click '读善其身' -> 'root' -> 'TestResults' tree item
            Mouse.Click(uITestResultsTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(30, 13));

            // Right-Click '读善其身' -> 'root' -> 'TestResults' tree item
            Mouse.Click(uITestResultsTreeItem, MouseButtons.Right, ModifierKeys.None, new Point(30, 10));

            // Last mouse action was not recorded.

            // Last mouse action was not recorded.

            // Right-Click '读善其身' tree item
            Mouse.Click(uI读善其身TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(18, 9));

            // Last mouse action was not recorded.

            // Click '读善其身' tree item
            Mouse.Click(uI读善其身TreeItem, new Point(26, 10));

            // Right-Click '读善其身' tree item
            Mouse.Click(uI读善其身TreeItem, MouseButtons.Right, ModifierKeys.None, new Point(26, 10));

            // Last mouse action was not recorded.

            // Last mouse action was not recorded.

            // Last mouse action was not recorded.
        }
        
        #region Properties
        public virtual NewNodeParams NewNodeParams
        {
            get
            {
                if ((this.mNewNodeParams == null))
                {
                    this.mNewNodeParams = new NewNodeParams();
                }
                return this.mNewNodeParams;
            }
        }
        
        public virtual AssertNewNodeExpectedValues AssertNewNodeExpectedValues
        {
            get
            {
                if ((this.mAssertNewNodeExpectedValues == null))
                {
                    this.mAssertNewNodeExpectedValues = new AssertNewNodeExpectedValues();
                }
                return this.mAssertNewNodeExpectedValues;
            }
        }
        
        public UI新建节点Window UI新建节点Window
        {
            get
            {
                if ((this.mUI新建节点Window == null))
                {
                    this.mUI新建节点Window = new UI新建节点Window();
                }
                return this.mUI新建节点Window;
            }
        }
        
        public UI读善其身eBook信息管理软件Beta版Window UI读善其身eBook信息管理软件Beta版Window
        {
            get
            {
                if ((this.mUI读善其身eBook信息管理软件Beta版Window == null))
                {
                    this.mUI读善其身eBook信息管理软件Beta版Window = new UI读善其身eBook信息管理软件Beta版Window();
                }
                return this.mUI读善其身eBook信息管理软件Beta版Window;
            }
        }
        #endregion
        
        #region Fields
        private NewNodeParams mNewNodeParams;
        
        private AssertNewNodeExpectedValues mAssertNewNodeExpectedValues;
        
        private UI新建节点Window mUI新建节点Window;
        
        private UI读善其身eBook信息管理软件Beta版Window mUI读善其身eBook信息管理软件Beta版Window;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'NewNode'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class NewNodeParams
    {
        
        #region Fields
        /// <summary>
        /// Type 'root' in 'txtNewNodeName' text box
        /// </summary>
        public string UITxtNewNodeNameEditText = "root";
        
        /// <summary>
        /// Type 'branch' in 'txtNewNodeName' text box
        /// </summary>
        public string UITxtNewNodeNameEditText1 = "branch";
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'AssertNewNode'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class AssertNewNodeExpectedValues
    {
        
        #region Fields
        /// <summary>
        /// Verify that '新建节点' window's property 'Name' equals '新建节点'
        /// </summary>
        public string UI新建节点WindowName = "新建节点";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UI新建节点Window : WinWindow
    {
        
        public UI新建节点Window()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "新建节点";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("新建节点");
            #endregion
        }
        
        #region Properties
        public UITxtNewNodeNameWindow UITxtNewNodeNameWindow
        {
            get
            {
                if ((this.mUITxtNewNodeNameWindow == null))
                {
                    this.mUITxtNewNodeNameWindow = new UITxtNewNodeNameWindow(this);
                }
                return this.mUITxtNewNodeNameWindow;
            }
        }
        #endregion
        
        #region Fields
        private UITxtNewNodeNameWindow mUITxtNewNodeNameWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITxtNewNodeNameWindow : WinWindow
    {
        
        public UITxtNewNodeNameWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "txtNewNodeName";
            this.WindowTitles.Add("新建节点");
            #endregion
        }
        
        #region Properties
        public WinEdit UITxtNewNodeNameEdit
        {
            get
            {
                if ((this.mUITxtNewNodeNameEdit == null))
                {
                    this.mUITxtNewNodeNameEdit = new WinEdit(this);
                    #region Search Criteria
                    this.mUITxtNewNodeNameEdit.SearchProperties[WinEdit.PropertyNames.Name] = "新节点名称：";
                    this.mUITxtNewNodeNameEdit.WindowTitles.Add("新建节点");
                    #endregion
                }
                return this.mUITxtNewNodeNameEdit;
            }
        }
        #endregion
        
        #region Fields
        private WinEdit mUITxtNewNodeNameEdit;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UI读善其身eBook信息管理软件Beta版Window : WinWindow
    {
        
        public UI读善其身eBook信息管理软件Beta版Window()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "“读”善其身：eBook信息管理软件Beta版";
            this.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
            #endregion
        }
        
        #region Properties
        public UITvwMainWindow UITvwMainWindow
        {
            get
            {
                if ((this.mUITvwMainWindow == null))
                {
                    this.mUITvwMainWindow = new UITvwMainWindow(this);
                }
                return this.mUITvwMainWindow;
            }
        }
        #endregion
        
        #region Fields
        private UITvwMainWindow mUITvwMainWindow;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UITvwMainWindow : WinWindow
    {
        
        public UITvwMainWindow(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ControlName] = "tvwMain";
            this.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
            #endregion
        }
        
        #region Properties
        public UI读善其身TreeItem UI读善其身TreeItem
        {
            get
            {
                if ((this.mUI读善其身TreeItem == null))
                {
                    this.mUI读善其身TreeItem = new UI读善其身TreeItem(this);
                }
                return this.mUI读善其身TreeItem;
            }
        }
        #endregion
        
        #region Fields
        private UI读善其身TreeItem mUI读善其身TreeItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UI读善其身TreeItem : WinTreeItem
    {
        
        public UI读善其身TreeItem(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinTreeItem.PropertyNames.Name] = "读善其身";
            this.SearchProperties["Value"] = "0";
            this.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
            #endregion
        }
        
        #region Properties
        public UIRootTreeItem UIRootTreeItem
        {
            get
            {
                if ((this.mUIRootTreeItem == null))
                {
                    this.mUIRootTreeItem = new UIRootTreeItem(this);
                }
                return this.mUIRootTreeItem;
            }
        }
        #endregion
        
        #region Fields
        private UIRootTreeItem mUIRootTreeItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIRootTreeItem : WinTreeItem
    {
        
        public UIRootTreeItem(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinTreeItem.PropertyNames.Name] = "root";
            this.SearchProperties["Value"] = "1";
            this.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            this.SearchConfigurations.Add(SearchConfiguration.NextSibling);
            this.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
            #endregion
        }
        
        #region Properties
        public UIBranchTreeItem UIBranchTreeItem
        {
            get
            {
                if ((this.mUIBranchTreeItem == null))
                {
                    this.mUIBranchTreeItem = new UIBranchTreeItem(this);
                }
                return this.mUIBranchTreeItem;
            }
        }
        
        public WinTreeItem UITestResultsTreeItem
        {
            get
            {
                if ((this.mUITestResultsTreeItem == null))
                {
                    this.mUITestResultsTreeItem = new WinTreeItem(this);
                    #region Search Criteria
                    this.mUITestResultsTreeItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "TestResults";
                    this.mUITestResultsTreeItem.SearchProperties["Value"] = "2";
                    this.mUITestResultsTreeItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                    this.mUITestResultsTreeItem.SearchConfigurations.Add(SearchConfiguration.NextSibling);
                    this.mUITestResultsTreeItem.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
                    #endregion
                }
                return this.mUITestResultsTreeItem;
            }
        }
        #endregion
        
        #region Fields
        private UIBranchTreeItem mUIBranchTreeItem;
        
        private WinTreeItem mUITestResultsTreeItem;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "10.0.30319.1")]
    public class UIBranchTreeItem : WinTreeItem
    {
        
        public UIBranchTreeItem(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinTreeItem.PropertyNames.Name] = "branch";
            this.SearchProperties["Value"] = "2";
            this.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            this.SearchConfigurations.Add(SearchConfiguration.NextSibling);
            this.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
            #endregion
        }
        
        #region Properties
        public WinTreeItem UIErrorloglogTreeItem
        {
            get
            {
                if ((this.mUIErrorloglogTreeItem == null))
                {
                    this.mUIErrorloglogTreeItem = new WinTreeItem(this);
                    #region Search Criteria
                    this.mUIErrorloglogTreeItem.SearchProperties[WinTreeItem.PropertyNames.Name] = "errorlog.log";
                    this.mUIErrorloglogTreeItem.SearchProperties["Value"] = "3";
                    this.mUIErrorloglogTreeItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                    this.mUIErrorloglogTreeItem.SearchConfigurations.Add(SearchConfiguration.NextSibling);
                    this.mUIErrorloglogTreeItem.WindowTitles.Add("“读”善其身：eBook信息管理软件Beta版");
                    #endregion
                }
                return this.mUIErrorloglogTreeItem;
            }
        }
        #endregion
        
        #region Fields
        private WinTreeItem mUIErrorloglogTreeItem;
        #endregion
    }
}
