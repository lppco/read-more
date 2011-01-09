using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace read_more
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 对节点的操作类型，分为添加，重命名，删除，保持树形窗口和列表窗口同步
        /// </summary>
        private enum Operation
        {
            add,
            rename,
            delete
        } ;
        /// <summary>
        /// 根节点名称
        /// </summary>
        private readonly string rootNodeName = read_more.Properties.Settings.Default.rootNodeName;
        /// <summary>
        /// 列表排序类
        /// </summary>
        //private readonly ListViewColumnSorter lvwColumnSorter = new ListViewColumnSorter();
         /// <summary>
        /// 设置输出书籍信息的窗体
        /// </summary>
        private frmSetExport frmExport = null;
        /// <summary>
        /// 作者介绍窗体
        /// </summary>
        private frmAboutAuthor frmMe = null;
        /// <summary>
        /// 介绍软件的窗体
        /// </summary>
        private frmAboutSoft frmSoft = null;
        /// <summary>
        /// 在treeview进行增删改时，与listview同步用
        /// </summary>
        private TreeNode curShowNode = null;
        /// <summary>
        /// 错误日志记录
        /// </summary>
        private TraceLog errLog = TraceLog.GetInstance();
        /// <summary>
        /// 设置语言类型
        /// </summary>
        private static bool choice = false;
        /// <summary>
        /// 根据查找到的书籍信息更新列表窗口的委托
        /// </summary>
        /// <param name="list"></param>
        private delegate void UpdateListViewHandler(ICollection<NodeBookMap> list);

        public frmMain()
        {
            InitializeComponent();
            InitBookDir();
            InitTreeView();
            InitListView();
            InitUI();
        }

        #region 程序相关的初始化工作
        /// <summary>
        /// 初始化存放信息的跟目录
        /// </summary>
        private void InitBookDir()
        {
            string rootPath = CommonUtil.GetRelativeAppDir(rootNodeName);
            CommonUtil.CreateDir(rootPath);
        }

        private void InitUI()
        {
            this.splittb.Panel2Collapsed = true;
            this.curShowNode = this.tvwMain.Nodes[0];
            //this.lvwMain.ListViewItemSorter = lvwColumnSorter;
           
        }

        /// <summary>
        /// 初始化treeView中的节点
        /// </summary>
        private void InitTreeView()
        {
            TreeNode rootNode = FormUtil.GetNewNode(rootNodeName, rootNodeName,
                ImgUtil.GetIconIndex(Constants.FOLDEREXT), this.cntMenuRootNode,null);
            RestoreTvwMain(rootNode.Text, rootNode);
            this.tvwMain.Nodes.Add(rootNode);
            ExpandSelectNode(rootNode);
            SetTreeSelectNode(rootNode);
        }

        /// <summary>
        /// 初始化ListView中的项
        /// </summary>
        private void InitListView()
        {
            TreeNodeCollection nodes = this.tvwMain.Nodes[0].Nodes;
            foreach (TreeNode node in nodes)
            {
                AddParentNodeToListView(node);
            }
        }
        #endregion

        /// <summary>
        /// 把treeView中根节点下的目录添加到listview中
        /// </summary>
        /// <param name="node"></param>
        private void AddParentNodeToListView(TreeNode node)
        {
            ListViewItem item = FormUtil.GetNewListItem(ImgUtil.GetIconIndex(Constants.FOLDEREXT), node);
            this.lvwMain.Items.Add(item);
        }

        /// <summary>
        /// 设置选中树的节点
        /// </summary>
        /// <param name="node"></param>
        private void SetTreeSelectNode(TreeNode node)
        {
            this.tvwMain.SelectedNode = node;
        }

        /// <summary>
        /// 展开选中节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandSelectNode(TreeNode node)
        {
            node.Expand();
        }

        /// <summary>
        /// 初始化treeview时，添加树中的父节点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        private void RestoreTvwMain(string path,TreeNode node)
        {
            string filePath = CommonUtil.GetRelativeAppDir(path);
            string fileName = filePath + Constants.FILENAME;
            if (File.Exists(fileName))
            {
                AddLeafNodes(fileName, node);
            }
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                TreeNode parentNode =
                    FormUtil.GetNewNode(CommonUtil.GetFolderName(folder), CommonUtil.GetFolderName(folder),
                                        ImgUtil.GetIconIndex(Constants.FOLDEREXT), this.cntMenuParentNode, null);
                node.Nodes.Add(parentNode);
                RestoreTvwMain(path + "\\" + parentNode.Text, parentNode);
            }
        }

        /// <summary>
        /// 初始化treeview时，添加树中的叶子节点
        /// </summary>
        /// <param name="file"></param>
        /// <param name="node"></param>
        private void AddLeafNodes(string file, TreeNode node)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file);
                XmlNodeList listNodes = xmlDoc.SelectNodes("//book");
                foreach (XmlNode nodeIter in listNodes)
                {
                    string nodeName = nodeIter.ChildNodes[BookInfoIndex.NAME].InnerText;
                    string tag = nodeIter.ChildNodes[BookInfoIndex.REALPATH].InnerText;
                    int imgIndex =
                        ImgUtil.GetIconIndex(Path.GetExtension(nodeIter.ChildNodes[BookInfoIndex.REALPATH].InnerText));
                    TreeNode leafNode = FormUtil.GetNewNode(nodeName, nodeName, imgIndex, this.cntMenuLeafNode, tag);
                    node.Nodes.Add(leafNode);
                }
                xmlDoc.Save(file);
            }
            catch (Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 在treeview添加一个父节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode AddTreeParentNode(string nodeName, TreeNode node)
        {
            string realNodeName = CommonUtil.GetFileName(nodeName);
            if (!node.Nodes.ContainsKey(realNodeName))
            {
                TreeNode newNode =
                    FormUtil.GetNewNode(realNodeName, realNodeName, ImgUtil.GetIconIndex(Constants.FOLDEREXT),
                                        this.cntMenuParentNode, null);
                node.Nodes.Add(newNode);
                return newNode;
            }
            else
            {
                SetStatueTipInfo(Constants.REPEATEDNODE);
                return null;
            }
        }

        /// <summary>
        /// 把treeview中添加一个叶子节点
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="node"></param>
        private void AddTreeLeafNode(string nodeName, TreeNode node)
        {
            string realNodeName = CommonUtil.GetFileName(nodeName);
            if (!node.Nodes.ContainsKey(realNodeName))
            {
                TreeNode newNode =
                    FormUtil.GetNewNode(realNodeName, realNodeName, ImgUtil.GetIconIndex(Path.GetExtension(nodeName)),
                                        this.cntMenuLeafNode, nodeName);
                node.Nodes.Add(newNode);
                FormUtil.RecordBookInfo(newNode, node);
            }
            else
            {
                SetStatueTipInfo(Constants.REPEATEDNODE);
            }
        }

        /// <summary>
        /// 删除treeview中的一个父节点
        /// </summary>
        /// <param name="node"></param>
        private void DelTreeParentNode(TreeNode node)
        {
            string path = CommonUtil.GetRelativeAppDir(node.FullPath);
            CommonUtil.DelDir(path);
        }

        /// <summary>
        /// 删除treeview中的一个叶子节点
        /// </summary>
        /// <param name="node"></param>
        private void DelTreeLeafNode(TreeNode node)
        {
            try
            {
                string fileName = CommonUtil.GetRelativeAppDir(node.Parent.FullPath + Constants.FILENAME);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode delElem = xmlDoc.SelectSingleNode("//book[path=\"" + node.FullPath + "\"]");
                delElem.ParentNode.RemoveChild(delElem);
                xmlDoc.Save(fileName);
            }
            catch(Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 重命名treeview中的一个父节点
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="node"></param>
        private void RenTreeParentNode(string oldName,TreeNode node)
        {
            string path = CommonUtil.GetRelativeAppDir(oldName);
            if (Directory.Exists(path))
            {
                while (true)
                {
                    try
                    {
                        Directory.Move(path, CommonUtil.GetRelativeAppDir(node.FullPath));
                        break;
                    }
                    catch (Exception )
                    {
                        MessageBox.Show(Constants.RENNODEFAIL,
                            Constants.ERRORTIP,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }

                }
                Thread thrd = new Thread(ModifyBookFile);
                thrd.Start(node);
                //FormUtil.ModifyBookFile(node);
            }
        }

        private static void ModifyBookFile(object node)
        {
            FormUtil.ModifyBookFile((TreeNode)node);
        }

        /// <summary>
        /// 重命名treeview中的一个叶子节点
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="node"></param>
        private void RenTreeLeafNode(string oldName, TreeNode node)
        {
            try
            {
                string fileName = CommonUtil.GetRelativeAppDir(node.Parent.FullPath + Constants.FILENAME);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode renameElem = xmlDoc.SelectSingleNode("//book[path=\"" + oldName + "\"]");
                XmlNode nodePath = renameElem.ChildNodes[BookInfoIndex.PATH];
                nodePath.InnerText = node.FullPath;
                XmlNode nodeName = renameElem.ChildNodes[BookInfoIndex.NAME];
                nodeName.InnerText = CommonUtil.GetFolderName(node.FullPath);
                xmlDoc.Save(fileName);
            }
            catch(Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 在把一个文件夹添加到treeview时，递归添加树中的节点
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="node"></param>
        private void AddFolderToTreeView(string folderPath, TreeNode node)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    AddTreeLeafNode(file, node);
                }
                string[] folders = Directory.GetDirectories(folderPath);
                foreach (string folder in folders)
                {
                    TreeNode parentNode = AddTreeParentNode(folder, node);
                    CommonUtil.CreateDir(CommonUtil.GetRelativeAppDir(parentNode.FullPath));
                    AddFolderToTreeView(folder, parentNode);
                }
            }
            catch (Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetStatueTipInfo(string info)
        {
            this.lblStatusTip.Text = info;
        }


        /// <summary>
        /// 在点击treeview时设置选中的节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvwMain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SetTreeSelectNode(e.Node);
            if (e.Node.Tag == null)
            {
                OpenSelectedListItem(e.Node);
            }
            if (!this.splittb.Panel2Collapsed)
            {
                ShowBookInfoPanel(e.Node);
            }
        }

        #region 右键根节点事件处理
        private void tlsMnuRootCollapse_Click(object sender, EventArgs e)
        {
            this.tvwMain.SelectedNode.Collapse();
        }

        private void tlsMnuRootAddFolder_Click(object sender, EventArgs e)
        {
            DialogResult dr = this.fbdlgAddFolder.ShowDialog();
            if (dr == DialogResult.OK)
            {

                TreeNode parentNode = AddTreeParentNode(this.fbdlgAddFolder.SelectedPath, this.tvwMain.SelectedNode);
                if (parentNode != null)
                {
                    CommonUtil.CreateDir(CommonUtil.GetRelativeAppDir(parentNode.FullPath));
    
                    this.tvwMain.BeginUpdate();
                    AddFolderToTreeView(this.fbdlgAddFolder.SelectedPath, parentNode);
                    this.tvwMain.EndUpdate();
                  
                    ExpandSelectNode(parentNode.Parent);
                    this.RefreshListView(Operation.add);
                }
            }
        }

        private void tlsMnuRootNewNode_Click(object sender, EventArgs e)
        {
            frmNewNode dlgNew = new frmNewNode(this.tvwMain.SelectedNode);
            DialogResult dr = dlgNew.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string newName = dlgNew.NewNodeName;
                string path = CommonUtil.GetRelativeAppDir(this.tvwMain.SelectedNode.FullPath + "\\" + newName);
                CommonUtil.CreateDir(path);
                AddTreeParentNode(newName, this.tvwMain.SelectedNode);
                ExpandSelectNode(this.tvwMain.SelectedNode);
                this.RefreshListView(Operation.add);
            }

        }

        private void tlsMnuRootRenNode_Click(object sender, EventArgs e)
        {
            this.tlsMenuParentRenNode.PerformClick();
        }
        #endregion

        #region 右键父点事件处理
        private void tlsMnuParentAddFile_Click(object sender, EventArgs e)
        {
            this.odlgAddFile.Filter = Constants.FILEFILTER;
            DialogResult dr = this.odlgAddFile.ShowDialog();
            if (dr == DialogResult.OK)
            {
                foreach (string file in this.odlgAddFile.FileNames)
                {
                    AddTreeLeafNode(file, this.tvwMain.SelectedNode);
                }
                ExpandSelectNode(this.tvwMain.SelectedNode);
            }
            this.RefreshListView(Operation.add);
        }

        private void tlsMnuParentCollapse_Click(object sender, EventArgs e)
        {
            this.tlsMnuRootCollapse.PerformClick();
        }

        private void tlsMnuParentAddFolder_Click(object sender, EventArgs e)
        {
            this.tlsMnuRootAddFolder.PerformClick();
        }

        private void tlsMnuParentNewNode_Click(object sender, EventArgs e)
        {
            this.tlsMnuRootNewNode.PerformClick();
        }

        private void tlsMnuParentDelNode_Click(object sender, EventArgs e)
        {
            DelNode(this.tvwMain.SelectedNode);
        }

        private void DelNode(TreeNode node)
        {
            if (node.Tag == null)
            {
                DelTreeParentNode(node);
            }
            else
            {
                DelTreeLeafNode(node);
            }
            //*****可能会有问题，暂时没发现
            if(this.curShowNode==node)
            {
                this.curShowNode = null;
            }
            node.Remove();
            this.RefreshListView(Operation.delete);
        }

        private void tlsMenuParentRenNode_Click(object sender, EventArgs e)
        {
            RenNode(this.tvwMain.SelectedNode);
        }

        private void RenNode(TreeNode node)
        {
            string oldName = node.FullPath;
            TreeNode parentNode = oldName.IndexOf("\\") == -1 ? null : node.Parent;
            frmRenNode dlgRename = new frmRenNode(node.Text, parentNode);
            DialogResult dr = dlgRename.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (oldName.IndexOf("\\") == -1)
                {
                    read_more.Properties.Settings.Default.rootNodeName = dlgRename.RenNodeName;
                }
                node.Text = dlgRename.RenNodeName;
                this.RefreshListView(Operation.rename);
                SetStatueTipInfo(Constants.BEGINUPDATE);
                this.Refresh();
                if (node.Tag == null)
                {
                    RenTreeParentNode(oldName, node);
                }
                else
                {
                    RenTreeLeafNode(oldName, node);
                }
                SetStatueTipInfo(Constants.ENDUPDATE);
            }
        }
        #endregion

        #region 右键叶子点事件处理
        private void tlsMnuLeafOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(this.tvwMain.SelectedNode.Tag.ToString());
            }
            catch(Win32Exception ex)
            {
                if(ex.NativeErrorCode==Constants.NOTASSOCIATEDEXECODE)
                {
                    MessageBox.Show(Constants.OPERATEFAIL,
                        Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DelNotFoundFile(this.tvwMain.SelectedNode);
                }
            }
            
        }

        private void tlsMnuLeafOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string path = CommonUtil.GetParentPath(this.tvwMain.SelectedNode.Tag.ToString());
                Process.Start(path);             
            }
            catch(Win32Exception)
            {
                DelNotFoundFile(this.tvwMain.SelectedNode);
            }
        }

        private void DelNotFoundFile(TreeNode node)
        {
            DialogResult dr = MessageBox.Show(Constants.FILEOPENFAIL,
                Constants.ERRORTIP, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                DelNode(node);
            }
        }

        private void tlsMnuLeafDelNode_Click(object sender, EventArgs e)
        {
            this.tlsMnuParentDelNode.PerformClick();
        }

        private void tlsMnuLeafRenNode_Click(object sender, EventArgs e)
        {
            this.tlsMenuParentRenNode.PerformClick();
        }

        private void tlsMnuLeafMfyInfo_Click(object sender, EventArgs e)
        {
            ShowBookInfoPanel(this.tvwMain.SelectedNode);
        }
        #endregion

        #region 主窗体事件处理
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                read_more.Properties.Settings.Default.Save();
            }
            catch(Exception)
            {
            }
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ntfyMain.Visible = true;
                this.ntfyMain.ShowBalloonTip(30);
            }
            this.Refresh();
        }
        #endregion

        #region 双击列表项，进行相关的处理
        private void lvwMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = ((ListView)sender).GetItemAt(e.X, e.Y);
            TreeNode node = item.Tag as TreeNode;
            if (node != null)
            {
                OpenSelectedListItem(node);
                ExpandSelectNode(node);
            }
        }

       private void OpenSelectedListItem(TreeNode node)
        {
            if (node.Tag != null)
            {
                OpenSelectedFile(node);
            }
            else
            {
                OpenSelectedFolder(node);
            }
        }

       /// <summary>
       /// 如果双击的是文件，则打开
       /// </summary>
       /// <param name="node"></param>
        private void OpenSelectedFile(TreeNode node)
        {
            try
            {
                Process.Start(node.Tag.ToString());
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == Constants.NOTASSOCIATEDEXECODE)
                {
                    MessageBox.Show(Constants.OPERATEFAIL,
                        Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DelNotFoundFile(node);
                }
            }
        }

        /// <summary>
        /// 如果双击的是文件夹，则更新列表
        /// </summary>
        /// <param name="node"></param>
       private void OpenSelectedFolder(TreeNode node)
       {
           SetTreeSelectNode(node);
           GetIntoFolder(node);
       }

       private void GetIntoFolder(TreeNode treeNode)
       {
            this.curShowNode = treeNode;
            this.lvwMain.Items.Clear();
            TreeNodeCollection nodes = treeNode.Nodes;
            foreach (TreeNode node in nodes)
            {
                string ext = node.Tag == null ? Constants.FOLDEREXT : Path.GetExtension(node.Tag.ToString());
                ListViewItem item =
                    FormUtil.GetNewListItem(ImgUtil.GetIconIndex(ext), node);
                this.lvwMain.Items.Add(item);
            }
        }
        #endregion

        /// <summary>
        /// 需要更新listview的情况分为4种
        /// 1）treeview添加文件，文件夹以及新建节点
        /// 2）重命名treeview节点并且listview中显示的内容包括当前节点
        /// 3）删除treeview节点并且listview中显示的内容包括当前节点，
        ///    而且删除的不是listview正在显示的内容
        /// 4）删除treeview节点并且删除的是listview正在显示的内容，
        ///    则需要重新显示下一个选中的节点
        /// </summary>
        /// <param name="oprt">操作类型</param>
       private void RefreshListView(Operation oprt)
       {
           if (oprt == Operation.add)
           {
               this.GetIntoFolder(this.curShowNode);
           }
           else if(oprt == Operation.rename
               && this.curShowNode.Nodes.ContainsKey(this.tvwMain.SelectedNode.Name))
           {
               this.GetIntoFolder(this.curShowNode);
           }
           else if(oprt==Operation.delete && this.curShowNode!=null &&
               this.curShowNode.Nodes.ContainsKey(this.tvwMain.SelectedNode.Name))
           {
               this.GetIntoFolder(this.curShowNode);
           }
           else if(oprt==Operation.delete && this.curShowNode==null)
           {
               this.GetIntoFolder(this.tvwMain.SelectedNode);
           }
       }

       #region listview右键处理
       private void tlsMnuLvwOpen_Click(object sender, EventArgs e)
       {
           if (this.lvwMain.SelectedIndices.Count != 0)
           {
               TreeNode node = this.lvwMain.SelectedItems[0].Tag as TreeNode;
               this.tvwMain.SelectedNode = node;
               if (node != null)
               {
                   OpenSelectedListItem(node);
                   ExpandSelectNode(node);
               }
           }
           else
           {
               SetStatueTipInfo(Constants.CHOOSELISTITEM);
           }
       }

       private void tlsMnuLvwBack_Click(object sender, EventArgs e)
       {
           if (this.lvwMain.SelectedIndices.Count != 0)
           {
               TreeNode node = this.lvwMain.SelectedItems[0].Tag as TreeNode;              
               if (node != null)
               {
                   TreeNode parentNode = (node.Parent != this.tvwMain.Nodes[0]
                                             ? node.Parent.Parent : this.tvwMain.Nodes[0]);                                           
                   node.Parent.Collapse();
                   this.tvwMain.SelectedNode = parentNode;
                   OpenSelectedListItem(parentNode);
               }
           }
           else
           {
               SetStatueTipInfo(Constants.CHOOSELISTITEM);
           }
       }

       private void tlsMnuLvwRenItem_Click(object sender, EventArgs e)
       {
           if (this.lvwMain.SelectedIndices.Count != 0)
           {
               TreeNode node = this.lvwMain.SelectedItems[0].Tag as TreeNode;
               if (node != null)
               {
                   RenNode(node);
               }
           }
           else
           {
               SetStatueTipInfo(Constants.CHOOSELISTITEM);
           }
       }

       private void tlsMnuLvwDelItem_Click(object sender, EventArgs e)
       {
           if (this.lvwMain.SelectedIndices.Count != 0)
           {
               TreeNode node = this.lvwMain.SelectedItems[0].Tag as TreeNode;
               if (node != null)
               {
                   DelNode(node);
               }
           }
           else
           {
               SetStatueTipInfo(Constants.CHOOSELISTITEM);
           }
       }

       private void tlsMnuLvwMdfInfo_Click(object sender, EventArgs e)
       {
           if (this.lvwMain.SelectedIndices.Count != 0)
           {
               TreeNode selectNode = this.lvwMain.SelectedItems[0].Tag as TreeNode;
               ShowBookInfoPanel(selectNode);
           }
           else
           {
               SetStatueTipInfo(Constants.CHOOSELISTITEM);
           }
       }

       private void tlsMnuLvwClearItems_Click(object sender, EventArgs e)
       {
           this.lvwMain.Items.Clear();
       }
       #endregion

       #region treeview的拖拽事件处理
       private void tvwMain_ItemDrag(object sender, ItemDragEventArgs e)
       {
           SetStatueTipInfo(Constants.DRAGDROPTIP);
           if (e.Button == MouseButtons.Left)
           {
               DoDragDrop(e.Item, DragDropEffects.Move);
           }
           else if (e.Button == MouseButtons.Right)
           {
               DoDragDrop(e.Item, DragDropEffects.Copy);
           }
       }

       private void tvwMain_DragEnter(object sender, DragEventArgs e)
       {
           e.Effect = e.AllowedEffect;
       }

       private void tvwMain_DragOver(object sender, DragEventArgs e)
       {
           Point targetPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
           ((TreeView)sender).SelectedNode = ((TreeView)sender).GetNodeAt(targetPoint);
       }

       private void tvwMain_DragDrop(object sender, DragEventArgs e)
       {
           SetStatueTipInfo(Constants.DRAGFILEFAIL);
           Point targetPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
           TreeNode targetNode = ((TreeView)sender).GetNodeAt(targetPoint);
           TreeNode draggedNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));
           ListViewItem draggedItem = (ListViewItem)(e.Data.GetData(typeof(ListViewItem)));

           if (e.Data.GetDataPresent(DataFormats.FileDrop))
           {
               DragSysToTreeView(targetNode, e);
           }
           else if (draggedItem != null)
           {
               TreeNode draggedItemNode = draggedItem.Tag as TreeNode;
               if (draggedItemNode != null && FormUtil.CanDrag(draggedItemNode, targetNode))
               {
                   if (e.Effect == DragDropEffects.Move)
                   {
                       this.lvwMain.Items.Remove(draggedItem);
                   }
                   DragListViewToTreeView(draggedItemNode, targetNode, e);
               }
           }
           else
           {
               DragTreeNodeToTreeView(draggedNode, targetNode, e);
           }
       }
       #endregion

       #region 保存和加载eBook信息的辅助函数
       private void ShowBookInfoPanel(TreeNode node)
       {
           this.splittb.Panel2Collapsed = false;
           HideSaveBookInfo();
           this.splittb.IsSplitterFixed = true;
           if (node != null)
           {
               if (node.Tag == null)
               {
                   this.pnlBookInfo.Enabled = false;
               }
               else
               {
                   LoadBookInfo(node);
                   this.pnlBookInfo.Enabled = true;
               }
           }
       }

       private void LoadBookInfo(TreeNode selectNode)
       {
           string fileName = CommonUtil.GetRelativeAppDir(selectNode.Parent.FullPath + Constants.FILENAME);
           try
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(fileName);
               XmlNode node = xmlDoc.SelectSingleNode("//book[name=\"" + selectNode.Text + "\"]");
               if (node != null)
               {
                   Book book = new Book(node);
                   UpdateBookInfoPnl(book);
               }
           }
           catch (Exception e)
           {
               errLog.WriteError(e);
               MessageBox.Show(Constants.UNKNOWERROR,
                   Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
       }

       private void UpdateBookInfoPnl(Book book)
       {
           this.txtBookName.Text = book.Name;
           this.txtAuthor.Text = book.Author;
           this.txtDescription.Text = book.Description;
           this.dtpPressDate.Value = book.Date == "" ? DateTime.Now : DateTime.Parse(book.Date);
       }

       private void SaveBookInfo()
       {
           TreeNode saveNode = CheckCanSave();
           if (saveNode != null)
           {
              FormUtil.SaveBookInfo(GetBook(),saveNode);
           }
       }

       private Book GetBook()
       {
           Book book = new Book();
           book.Author = this.txtAuthor.Text;
           book.Date = this.dtpPressDate.Text;
           book.Description = this.txtDescription.Text;
           return book;
       }

       private TreeNode CheckCanSave()
       {
           ListViewItem item=this.lvwMain.FindItemWithText(this.txtBookName.Text);
           if(item!=null)
           {
               TreeNode node = item.Tag as TreeNode;
               if (node != null && node.Tag != null)
               {
                   return node;
               }
           }
           if (this.tvwMain.SelectedNode != null && this.tvwMain.SelectedNode.Tag != null)
           {
               return this.tvwMain.SelectedNode;
           }
           MessageBox.Show(Constants.UPDATEBOOKINFO, Constants.ERRORTIP, MessageBoxButtons.OK,
                           MessageBoxIcon.Information);
           SetStatueTipInfo(Constants.UPDATEBOOKINFO);
           return null;
       }

        private void SaveBookInfoSuc()
        {
            this.lblSaveSuccess.Visible = true;
            this.picSaveOK.Visible = true;
        }

        private void HideSaveBookInfo()
        {
            this.lblSaveSuccess.Visible = false;
            this.picSaveOK.Visible = false;
        }
       #endregion

       #region 拖拽事件的辅助函数
        private void DragSysToTreeView(TreeNode targetNode, DragEventArgs e)
       {
           string[] myFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
           foreach (string file in myFiles)
           {
               if (Path.HasExtension(file) && File.Exists(file))
               {
                   if (targetNode.FullPath.IndexOf("\\") != -1 && targetNode.Tag == null)
                   {
                       AddTreeLeafNode(file, targetNode);
                       ExpandSelectNode(targetNode);
                       //targetNode.Expand();
                   }
               }
               else
               {
                   if (targetNode.Tag == null)
                   {
                       TreeNode parentNode = AddTreeParentNode(file, targetNode);
                       if (parentNode != null)
                       {
                           CommonUtil.CreateDir(CommonUtil.GetRelativeAppDir(parentNode.FullPath));
                           AddFolderToTreeView(file, parentNode);
                           //parentNode.Parent.Expand();
                           ExpandSelectNode(parentNode.Parent);
                       }
                   }
               }
           }
       }

       private void DragTreeNodeToTreeView(TreeNode draggedNode, TreeNode targetNode, DragEventArgs e)
       {
           if (FormUtil.CanDrag(draggedNode, targetNode))
           {
               BeginDoDragNode(draggedNode, targetNode, e);
           }

       }

       private void BeginDoDragNode(TreeNode draggedNode, TreeNode targetNode, DragEventArgs e)
       {
           if (e.Effect == DragDropEffects.Move)
           {
               if (draggedNode.Tag == null)
               {
                   try
                   {
                       Directory.Move(CommonUtil.GetRelativeAppDir(draggedNode.FullPath),
                          CommonUtil.GetRelativeAppDir(targetNode.FullPath + "\\" + draggedNode.Text));
                   }
                   catch (Exception ex)
                   {
                       errLog.WriteError(ex);
                       MessageBox.Show(Constants.UNKNOWERROR,
                           Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }
               }
               else
               {
                   DelTreeLeafNode(draggedNode);
                   FormUtil.RecordBookInfo(draggedNode, targetNode);
               }
               draggedNode.Remove();
               targetNode.Nodes.Add(draggedNode);
               FormUtil.ModifyBookFile(draggedNode);
           }
           else if (e.Effect == DragDropEffects.Copy)
           {
               if (draggedNode.Tag == null)
               {
                   Directory.CreateDirectory(
                       CommonUtil.GetRelativeAppDir(targetNode.FullPath + "\\" + draggedNode.Text));                                                                     
                   CommonUtil.CopyDirExt( 
                       CommonUtil.GetRelativeAppDir(draggedNode.FullPath + "\\"),
                       CommonUtil.GetRelativeAppDir(targetNode.FullPath + "\\" + draggedNode.Text + "\\"));
               }
               else
               {
                   FormUtil.RecordBookInfo(draggedNode, targetNode);
               }
               TreeNode cloneNode = (TreeNode)draggedNode.Clone();
               targetNode.Nodes.Add(cloneNode);
               FormUtil.ModifyBookFile(cloneNode);
           }
           //targetNode.Expand();
           ExpandSelectNode(targetNode);
       }

       private void DragListViewToTreeView(TreeNode draggedNode, TreeNode targetNode, DragEventArgs e)
       {
           BeginDoDragNode(draggedNode, targetNode, e);
       }
       #endregion

       #region listview事件处理
       private void lvwMain_ItemDrag(object sender, ItemDragEventArgs e)
       {
           SetStatueTipInfo(Constants.DRAGDROPTIP);
           if (e.Button == MouseButtons.Left)
           {
               DoDragDrop(e.Item, DragDropEffects.Move);
           }
           else if (e.Button == MouseButtons.Right)
           {
               DoDragDrop(e.Item, DragDropEffects.Copy);
           }
       }

      /* private void lvwMain_ColumnClick(object sender, ColumnClickEventArgs e)
       {
           if (e.Column == lvwColumnSorter.SortColumn)
           {
               if (lvwColumnSorter.Order == SortOrder.Ascending)
               {
                   lvwColumnSorter.Order = SortOrder.Descending;
               }
               else
               {
                   lvwColumnSorter.Order = SortOrder.Ascending;
               }
           }
           else
           {
               lvwColumnSorter.SortColumn = e.Column;
               lvwColumnSorter.Order = SortOrder.Ascending;
           }
           this.lvwMain.Sort();
       }*/

       private void lvwMain_SelectedIndexChanged(object sender, EventArgs e)
       {
           if (!this.splittb.Panel2Collapsed)
           {
               tlsMnuLvwMdfInfo.PerformClick();
           }
           
       }
       #endregion

       #region 图书信息面板相关事件处理
       private void tlsMnuBookHidePnl_Click(object sender, EventArgs e)
       {
           HideSaveBookInfo();
           this.splittb.Panel2Collapsed = true;
       }

       
       #endregion

        /// <summary>
        /// 把查找路径添加到主窗体
        /// </summary>
       private void LoadSearchPath()
       {
           this.cmbxSearchPath.Items.Clear();
           TreeNode rootNode = this.tvwMain.Nodes[0];
           List<object> items=new List<object>();
           this.cmbxSearchPath.Items.Add(new ComboBoxExItem(rootNode.Text, ImgUtil.GetIconIndex(Constants.LIBRARYEXT)));
           foreach (TreeNode node in rootNode.Nodes)
           {
               items.Add(new ComboBoxExItem(node.Text, ImgUtil.GetIconIndex(Constants.FOLDEREXT)));
           }
           this.cmbxSearchPath.Items.AddRange(items.ToArray());
           this.cmbxSearchPath.SelectedIndex = 0;
       }

       #region tab控件选择事件
       private void tctlLeft_Selected(object sender, TabControlEventArgs e)
       {
           if (e.TabPageIndex == Constants.EBOOKSEARCHTAB)
           {             
               this.splitlr.SplitterDistance = Constants.SIZEOFLEFT;
               this.splitlr.IsSplitterFixed = true;
               this.chklSearchContent.SetItemChecked(0, true);
               this.chklSearchContent.SetSelected(0, true);
               LoadSearchPath();
               this.txtSearchKeyWord.Focus();
           }
           else if(e.TabPageIndex == Constants.EBOOKSTATTAB)
           {
               this.splitlr.SplitterDistance = Constants.SIZEOFLEFT;
               this.splitlr.IsSplitterFixed = true;
           }
           else
           {
               this.splitlr.IsSplitterFixed = false;
           }
       }
        #endregion

       #region 选添查找选项处理
       private void chkSearchFileType_CheckedChanged(object sender, EventArgs e)
       {
           grpFileType.Enabled = chkSearchFileType.Checked;
       }

      
       #endregion

       private void btnSearch_Click(object sender, EventArgs e)
       {
           if(CanSearch())
           {
               SearchCondition cndt = new SearchCondition(this.txtSearchKeyWord.Text, this.chklSearchContent.CheckedIndices);
               cndt.Path = this.cmbxSearchPath.SelectedIndex == 0 ?
                  this.tvwMain.Nodes[0] : this.tvwMain.Nodes[0].Nodes[this.cmbxSearchPath.SelectedIndex - 1];

               cndt.IsFilterFileType = this.chkSearchFileType.Checked;
               cndt.FileType = this.txtSearchFileType.Text;

               ShowSearchTip();

               Thread thrd = new Thread(BeginSearch);
               thrd.Start(cndt);
           }
       }

       private void txtSearchKeyWord_KeyPress(object sender, KeyPressEventArgs e)
       {
           if (e.KeyChar == (char)Keys.Enter)
           {
               btnSearch.PerformClick();
           }
       }

       #region 查询辅助函数
       private void BeginSearch(object condition)
        {
            SearchCondition cndt = (SearchCondition) condition;
            SearchBook search=new SearchBook();
            List<NodeBookMap> list=search.Search(cndt);
            this.Invoke(new UpdateListViewHandler(AddSearchResultToView), list);
        }

        private void AddSearchResultToView(ICollection<NodeBookMap> list)
        {
            BeginAddResult(list);
            if(list.Count!=0)
            {
                this.lblSearchCount.Text = string.Format(Constants.SEARCHRESULT, list.Count);
            }
            else
            {
                this.lblSearchCount.Text = Constants.SEARCHNORESULT;
            }
            HideSearchTip();
        }

        private void BeginAddResult(IEnumerable<NodeBookMap> list)
        {
            this.lvwMain.Items.Clear();
            foreach (NodeBookMap nodeBook in list)
            {
                string ext = Path.GetExtension(nodeBook.Node.Tag.ToString());
                ListViewItem item = FormUtil.GetNewListItem(ImgUtil.GetIconIndex(ext), nodeBook.Node);
                this.lvwMain.Items.Add(item);
            }
        }

        private bool CanSearch()
        {
            if (this.txtSearchKeyWord.Text == "" || this.chklSearchContent.CheckedIndices.Count == 0)
            {
                MessageBox.Show(Constants.MUSTITEMTIP,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.grpFileType.Enabled)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.txtSearchFileType.Text, Constants.PATTERN))
                {
                    MessageBox.Show(Constants.FILETYPETIP,
                         Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
          
            return true;
        }

        private void ShowSearchTip()
        {
            this.picSearchPrg.Visible = true;
            this.btnSearch.Text = "正在查找";
            this.btnSearch.Enabled = false;
        }

        private void HideSearchTip()
        {
            this.picSearchPrg.Visible = false;
            this.btnSearch.Text = "查找";
            this.btnSearch.Enabled = true;
        }

       #endregion

        // 清空列表
        private void lnkClearItems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.lvwMain.Items.Clear();
        }

        private void lnkModifyBookInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.tlsMnuLvwMdfInfo.PerformClick();
        }

        #region 统计面板事件处理
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton) sender;
            if(rad.Name=="radFileType")
            {
                this.grpStatFileType.Visible = true;
                
            }
            
        }

        private void btnStat_Click(object sender, EventArgs e)
        {
            if(CanStat())
            {
                SearchCondition cndt = new SearchCondition();
                cndt.Path = this.tvwMain.Nodes[0];
                cndt.AddSearchRange(new int[] { });
                IComparer<NodeBookMap> comparer = null;
                if (this.radFileType.Checked)
                {
                    comparer = new SortType();
                    cndt.IsFilterFileType = true;
                    cndt.FileType = this.txtStatFileType.Text;
                }
                
                cndt.SortComparer = comparer;
                ShowStatTip();
                Thread thrd = new Thread(BeginStat);
                thrd.Start(cndt);
            }
        }
        #endregion

        #region 统计辅助函数
        private bool CanStat()
        {
            if(this.radFileType.Checked)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(this.txtStatFileType.Text, Constants.PATTERN))
                {
                    MessageBox.Show(Constants.FILETYPETIP,
                         Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
           
            return true;
        }

        private void ShowStatTip()
        {
            this.picStatPrg.Visible = true;
            this.btnStat.Text = "正在查找";
            this.btnStat.Enabled = false;
        }

        private void HideStatTip()
        {
            this.picStatPrg.Visible = false;
            this.btnStat.Text = "统计";
            this.btnStat.Enabled = true;
        }

        private void BeginStat(object condition)
        {
            SearchCondition cndt = (SearchCondition)condition;
            SearchBook search = new SearchBook();
            List<NodeBookMap> list = search.Search(cndt);
            list.Sort(cndt.SortComparer);
            this.Invoke(new UpdateListViewHandler(AddStatResultToView), list);
        }

        private void AddStatResultToView(ICollection<NodeBookMap> list)
        {
            BeginAddResult(list);
            if (list.Count != 0)
            {
                this.lblSearchCount.Text = string.Format(Constants.STATRESULT, list.Count);
            }
            else
            {
                this.lblSearchCount.Text = Constants.STATNORESULT;
            }
            HideStatTip();
        }
        #endregion

        #region NotifyIcon事件处理
        private void ntfyMain_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left)
            {
                ShowMainForm();
            }
        }

        private void ShowMainForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.ntfyMain.Visible = false;
        }

        private void tlsMnuNtfyShow_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void tlsMnuNtfyAuthor_Click(object sender, EventArgs e)
        {
            frmMe = frmAboutAuthor.GetInstance();
            frmMe.ShowDialog();
        }

        private void tlsMnuNtfyExit_Click(object sender, EventArgs e)
        {
            this.ntfyMain.Visible = false;
            Application.Exit();
        }
        #endregion

        #region 主菜单事件处理
        private void tlsMnuFileAddFile_Click(object sender, EventArgs e)
        {
            if(this.tvwMain.SelectedNode==this.tvwMain.Nodes[0])
            {
                SetStatueTipInfo(Constants.DRAGFILEFAIL);
                MessageBox.Show(Constants.DRAGFILEFAIL,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (this.tvwMain.SelectedNode.Tag!=null)
            {
                SetStatueTipInfo(Constants.DRAGFILEFAIL);
                MessageBox.Show(Constants.DRAGFILEFAIL,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                tlsMnuParentAddFile.PerformClick();
            }           
        }

        private void tlsMnuFileAddFolder_Click(object sender, EventArgs e)
        {
            if (this.tvwMain.SelectedNode.Tag!=null)
            {
                SetStatueTipInfo(Constants.DRAGFILEFAIL);
                MessageBox.Show(Constants.DRAGFILEFAIL,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                tlsMnuRootAddFolder.PerformClick();
            }          
        }

        private void tlsMnuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tlsMnuToolSearch_Click(object sender, EventArgs e)
        {
            tctlLeft.SelectedIndex = Constants.EBOOKSEARCHTAB;
            btnSearch.PerformClick();
        }

        private void tlsMnuToolStat_Click(object sender, EventArgs e)
        {
            tctlLeft.SelectedIndex = Constants.EBOOKSTATTAB;
            btnStat.PerformClick();
        }

        private void tlsMnuToolClearList_Click(object sender, EventArgs e)
        {
            this.lvwMain.Items.Clear();
        }

        private void tlsMnuToolSetExport_Click(object sender, EventArgs e)
        {
            frmExport = frmSetExport.GetInstance();
            frmExport.ShowDialog();
        }

        private void tlsMnuHelpDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(CommonUtil.GetRelativeAppDir("help.chm"));

            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == Constants.FILENOTFOUNDCODE)
                {
                    MessageBox.Show(Constants.HELPFILEDELETE,
                        Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void tlsMmuHelpSoft_Click(object sender, EventArgs e)
        {
            frmSoft = frmAboutSoft.GetInstance();
            frmSoft.ShowDialog();
        }

        private void tlsMnuHelpAuthor_Click(object sender, EventArgs e)
        {
            frmMe = frmAboutAuthor.GetInstance();
            frmMe.ShowDialog();
        }
        #endregion

        #region 工具栏事件处理
        private void tlsBtnAddFile_Click(object sender, EventArgs e)
        {
            tlsMnuFileAddFile.PerformClick();
        }

        private void tlsBtnAddFolder_Click(object sender, EventArgs e)
        {
            tlsMnuFileAddFolder.PerformClick();
        }

        private void tlsBtnBack_Click(object sender, EventArgs e)
        {
            tlsMnuLvwBack.PerformClick();
        }

        private void tlsBtnClearList_Click(object sender, EventArgs e)
        {
            this.lvwMain.Items.Clear();
        }

        private void tlsBtnSearch_Click(object sender, EventArgs e)
        {
            tlsMnuToolSearch.PerformClick();
        }

        private void tlsBtnStat_Click(object sender, EventArgs e)
        {
            tlsMnuToolStat.PerformClick();
        }

        private void tlsMnuViewIcon_Click(object sender, EventArgs e)
        {
            this.lvwMain.View = View.LargeIcon;

        }

        private void tlsMnuViewList_Click(object sender, EventArgs e)
        {
            this.lvwMain.View = View.List;
        }

        private void tlsMnuViewDetails_Click(object sender, EventArgs e)
        {
            this.lvwMain.View = View.Details;
        }
        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

       
         private void changeLanguage_Click(object sender, EventArgs e)
         {
               if (choice == false)
                    choice = true;
                else
                choice = false;

              setLanguage();
              this.Refresh();
         }

         /// <summary>
         /// 返回语言类型
         /// </summary>
         public static bool isChinese()
         {
             return choice;
         }

        /// <summary>
        /// 设置语言
        /// </summary>
        private void setLanguage()
        {
            if (choice == false)
            {
                this.tlsMenuFile.Text = "文件";
                this.tlsMnuFileAddFile.Text = "添加文件";
                this.tlsMnuFileAddFolder.Text = "添加文件夹";
                this.tlsMnuFileExit.Text = "退出";
                this.tlsMenuTool.Text = "编辑";
                this.tlsMnuToolSearch.Text = "搜索";
                this.tlsMnuToolStat.Text = "分类";
                this.tlsMnuToolClearList.Text = "清空列表";
                this.tlsMenuHelp.Text = "帮助";
                this.tlsMmuHelpSoft.Text = "此软件介绍";
                this.tlsMnuHelpAuthor.Text = "关于作者";
                this.tlsBtnAddFile.Text = "添加文件";
                this.tlsBtnAddFolder.Text = "添加文件夹";
                this.tlsBtnBack.Text = "向上";
                this.tlsBtnClearList.Text = "清空列表";
                this.tpgManagement.Text = "管理";
                this.tpgSearch.Text = "搜索";
                this.btnSearch.Text = "搜索";
                this.grpSearchOptionalItem.Text = "选添查找选项";
                this.grpFileType.Text = "文件类型";
                this.tltInfo.SetToolTip(this.grpFileType, "请按照以下格式输入：\r\n*.pdf *.chm ...");
                this.tltInfo.SetToolTip(this.txtSearchFileType, "请按照以下格式输入：\r\n*.pdf *.chm ...");
                this.grpSearchPath.Text = "查找路径";
                this.chkSearchFileType.Text = "查找文件类型";
                this.tltInfo.SetToolTip(this.chkSearchFileType, "请按照以下格式输入：\r\n*.pdf *.chm ...");
                this.grpSearchMustItem.Text = "必填查找选项";
                this.grpSearchContent.Text = "查找信息范围";
                this.chklSearchContent.Items.RemoveAt(0);
                this.chklSearchContent.Items.AddRange(new object[] {
            "书名"});
                this.grpKeyWord.Text = "查找关键字";
                this.tpgStat.Text = "分类";
                this.btnStat.Text = "分类";
                this.grpStatType.Text = "统计类型";
                this.radFileType.Text = "按文件类型";
                this.grpStatFileType.Text = "文件类型";
                this.tltInfo.SetToolTip(this.grpStatFileType, "请按照以下格式输入：\r\n*.pdf *.chm ...");
                this.tltInfo.SetToolTip(this.txtStatFileType, "请按照以下格式输入：\r\n*.pdf *.chm ...");
                this.name.Text = "名称";
                this.size.Text = "大小(字节)";
                this.type.Text = "类型";
                this.date.Text = "最后访问日期";
                this.tlsMnuLvwOpen.Text = "打开";
                this.tlsMnuLvwBack.Text = "向上";
                this.tlsMnuLvwRenItem.Text = "重命名此项";
                this.tlsMnuLvwDelItem.Text = "删除此项";
                this.tlsMnuLvwClearItems.Text = "清空列表";
                this.tlsMnuLvwMdfInfo.Text = "修改eBook信息";
                this.lnkModifyBookInfo.Text = "修改eBook信息";
                this.lnkClearItems.Text = "清空列表";
                this.tlsMnuBookHidePnl.Text = "隐藏";
                this.grpBookInfo.Text = "eBook信息";
                this.lblBookName.Text = "书名";
                this.lblSaveSuccess.Text = "保存信息成功";
                this.btnSave.Text = "保存";
                this.lblAuthor.Text = "作者";
                this.lblPressDate.Text = "创建日期";
                this.lblDescription.Text = "描述";
                this.tlsMnuRootCollapse.Text = "折叠";
                this.tlsMnuRootAddFolder.Text = "添加文件夹";
                this.tlsMnuRootNewNode.Text = "新建节点";
                this.tlsMnuRootRenNode.Text = "重命名节点";
                this.tlsMnuParentCollapse.Text = "折叠";
                this.tlsMnuParentAddFile.Text = "添加文件";
                this.tlsMnuParentAddFolder.Text = "添加文件夹";
                this.tlsMnuParentNewNode.Text = "新建节点";
                this.tlsMnuParentDelNode.Text = "删除节点";
                this.tlsMenuParentRenNode.Text = "重命名节点";
                this.tlsMnuLeafOpenFile.Text = "打开文件";
                this.tlsMnuLeafOpenFolder.Text = "打开文件目录";
                this.tlsMnuLeafDelNode.Text = "删除节点";
                this.tlsMnuLeafRenNode.Text = "重命名节点";
                this.tlsMnuLeafMfyInfo.Text = "修改eBook信息";
                this.fbdlgAddFolder.Description = "请选择要添加的文件夹";
                this.tltInfo.ToolTipTitle = "信息提示";
                this.sdlgSaveList.Title = "保存eBook列表";
                this.ntfyMain.BalloonTipText = "嘿，我在这里~";
                this.ntfyMain.BalloonTipTitle = "信息提示";
                this.ntfyMain.Text = "eBook信息管理软件V1.0";
                this.tlsMnuNtfyShow.Text = "显示";
                this.tlsMnuNtfyAuthor.Text = "关于作者";
                this.tlsMnuNtfyExit.Text = "退出";
                this.Text = "“读”善其身：eBook信息管理软件Beta版";
                this.changeLanguage.Text = "切换语言";
            }
            else
            {
                this.tlsMenuFile.Text = "File";
                this.tlsMnuFileAddFile.Text = "Add file";
                this.tlsMnuFileAddFolder.Text = "Add folder";
                this.tlsMnuFileExit.Text = "退出";
                this.tlsMenuTool.Text = "Edit";
                this.tlsMnuToolSearch.Text = "Search";
                this.tlsMnuToolStat.Text = "Stat";
                this.tlsMnuToolClearList.Text = "Clear";
                this.tlsMenuHelp.Text = "Help";
                this.tlsMmuHelpSoft.Text = "About the soft";
                this.tlsMnuHelpAuthor.Text = "About the Author";
                this.tlsBtnAddFile.Text = "Add file";
                this.tlsBtnAddFolder.Text = "Add folder";
                this.tlsBtnBack.Text = "Up";
                this.tlsBtnClearList.Text = "Clear list";
                this.tpgManagement.Text = "Manage";
                this.tpgSearch.Text = "Search";
                this.btnSearch.Text = "Search";
                this.grpSearchOptionalItem.Text = "Choose the Item searched";
                this.grpFileType.Text = "File Type";
                this.tltInfo.SetToolTip(this.grpFileType, "Follow these formats: \r\n*.pdf *.chm ...");
                this.tltInfo.SetToolTip(this.txtSearchFileType, "Follow these formats: \r\n*.pdf *.chm ...");
                this.grpSearchPath.Text = "Searched path";
                this.chkSearchFileType.Text = "Searched file type";
                this.tltInfo.SetToolTip(this.chkSearchFileType, "Follow these formats: \r\n*.pdf *.chm ...");
                this.grpSearchMustItem.Text = "Items needed";
                this.grpSearchContent.Text = "Searched range";
                this.chklSearchContent.Items.RemoveAt(0);
                this.chklSearchContent.Items.AddRange(new object[] {
            "Name of book"});
                this.grpKeyWord.Text = "Keyword";
                this.tpgStat.Text = "Sort";
                this.btnStat.Text = "Sort";
                this.grpStatType.Text = "Stated type";
                this.radFileType.Text = "Accord to file type";
                this.grpStatFileType.Text = "File type";
                this.tltInfo.SetToolTip(this.grpStatFileType, "Follow these formats: \r\n*.pdf *.chm ...");
                this.tltInfo.SetToolTip(this.txtStatFileType, "Follow these formats: \r\n*.pdf *.chm ...");
                this.name.Text = "Name";
                this.size.Text = "Size";
                this.type.Text = "Type";
                this.date.Text = "Date";
                this.tlsMnuLvwOpen.Text = "Open";
                this.tlsMnuLvwBack.Text = "Back";
                this.tlsMnuLvwRenItem.Text = "Rename";
                this.tlsMnuLvwDelItem.Text = "Delete";
                this.tlsMnuLvwClearItems.Text = "Clear list";
                this.tlsMnuLvwMdfInfo.Text = "Change infomation of eBook";
                this.lnkModifyBookInfo.Text = "Change info";
                this.lnkClearItems.Text = "Clear list";
                this.tlsMnuBookHidePnl.Text = "Hide";
                this.grpBookInfo.Text = "Infomation of eBook";
                this.lblBookName.Text = "BookName";
                this.lblSaveSuccess.Text = "Success!";
                this.btnSave.Text = "Save";
                this.lblAuthor.Text = "Author";
                this.lblPressDate.Text = "Create Date";
                this.lblDescription.Text = "Description";
                this.tlsMnuRootCollapse.Text = "Collapse";
                this.tlsMnuRootAddFolder.Text = "Add folder";
                this.tlsMnuRootNewNode.Text = "New node";
                this.tlsMnuRootRenNode.Text = "Rename node";
                this.tlsMnuParentCollapse.Text = "Collapse";
                this.tlsMnuParentAddFile.Text = "Add file";
                this.tlsMnuParentAddFolder.Text = "Add folder";
                this.tlsMnuParentNewNode.Text = "New node";
                this.tlsMnuParentDelNode.Text = "Delete node";
                this.tlsMenuParentRenNode.Text = "Rename node";
                this.tlsMnuLeafOpenFile.Text = "Open file";
                this.tlsMnuLeafOpenFolder.Text = "Open folder";
                this.tlsMnuLeafDelNode.Text = "Delete node";
                this.tlsMnuLeafRenNode.Text = "Rename node";
                this.tlsMnuLeafMfyInfo.Text = "Modify Infomation about the book";
                this.fbdlgAddFolder.Description = "Please choose the folder";
                this.tltInfo.ToolTipTitle = "Tips";
                this.sdlgSaveList.Title = "Save list";
                this.ntfyMain.BalloonTipText = "Hi, I'm Here~";
                this.ntfyMain.BalloonTipTitle = "Tips";
                this.ntfyMain.Text = "eBook Manage Soft V1.0";
                this.tlsMnuNtfyShow.Text = "Display";
                this.tlsMnuNtfyAuthor.Text = "About author";
                this.tlsMnuNtfyExit.Text = "Exit";
                this.Text = "Read_More：eBook Manage Soft Beta Version";
                this.changeLanguage.Text = "Change Language";
            }//end else
        }//end setLanguage  

    
    }
}
