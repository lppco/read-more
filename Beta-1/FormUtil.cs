using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace read_more
{
    /// <summary>
    /// 和窗体操作有关的工具类
    /// </summary>
    public static class FormUtil
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        private static TraceLog errLog = TraceLog.GetInstance();
        
        /// <summary>
        /// 新建一个treeview节点
        /// </summary>
        /// <param name="nodeName">节点名字</param>
        /// <param name="nodeText">节点显示文字</param>
        /// <param name="nodeImgIndex">节点图标索引</param>
        /// <param name="nodeCntMnu">节点右键菜单</param>
        /// <param name="nodeTag">节点Tag信息</param>
        /// <returns>一个新treeview节点</returns>
        public static TreeNode GetNewNode(string nodeName,string nodeText,int nodeImgIndex,
            ContextMenuStrip nodeCntMnu,string nodeTag)
        {
            TreeNode newNode=new TreeNode();
            newNode.Name = nodeName;
            newNode.Text = nodeText;
            newNode.ImageIndex = nodeImgIndex;
            newNode.ContextMenuStrip = nodeCntMnu;
            newNode.Tag = nodeTag;
            return newNode;
        }

        /// <summary>
        /// 根据树节点新建一个列表项
        /// </summary>
        /// <param name="imgIndex"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static ListViewItem GetNewListItem(int imgIndex,TreeNode node)
        {
            ListViewItem item = new ListViewItem(node.Text);
            item.Tag = node;
            item.ImageIndex = imgIndex;
            string fileType = null, fileSize = null, fileAscDate = null;
            if(node.Tag==null)  
            {
                fileType = Constants.FOLDERSTR;
                fileSize = String.Empty;
                fileAscDate =
                    Directory.GetLastAccessTime(CommonUtil.GetRelativeAppDir(node.FullPath)).ToShortDateString();
            }
            else
            {
                string fileName = node.Tag.ToString();
                fileType = Path.GetExtension(fileName).Substring(1)+"文件";
                if(File.Exists(fileName))
                {
                    //fileType = Constants.FILESTR;
                    FileInfo fileInfo = new FileInfo(node.Tag.ToString());
                    fileSize = fileInfo.Length.ToString();
                    fileAscDate = File.GetLastAccessTime(node.Tag.ToString()).ToShortDateString();
                }
                else
                {
                    //fileType = Constants.FILESTR;
                    fileSize = "0";
                    fileAscDate = DateTime.Now.ToShortDateString();
                }
            }
            ListViewItem.ListViewSubItem subItemType = new ListViewItem.ListViewSubItem(item, fileType);
            ListViewItem.ListViewSubItem subItemSize = new ListViewItem.ListViewSubItem(item, fileSize);
            ListViewItem.ListViewSubItem subItemDate = new ListViewItem.ListViewSubItem(item, fileAscDate);
            item.SubItems.Add(subItemSize);
            item.SubItems.Add(subItemType);
            item.SubItems.Add(subItemDate);
            return item;
        }

        /// <summary>
        /// 如果还没有存放ebook信息的文件，则新建一个
        /// </summary>
        /// <param name="fileName"></param>
        public static void InitBookInfoFile(string fileName)
        {
            XmlTextWriter objXml = new XmlTextWriter(fileName, null);
            objXml.Formatting = Formatting.Indented;
            objXml.Indentation = 4;
            objXml.WriteStartDocument();
            objXml.WriteStartElement("MyLibrary");
            objXml.WriteStartElement("Books");
            objXml.WriteEndElement();
            objXml.WriteEndElement();
            objXml.Flush();
            objXml.Close();
        }

        /// <summary>
        /// 把一本eBook的信息写到文件中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        public static  void WriteInfoToFile(string fileName, TreeNode sourceNode, TreeNode targetNode)
        {
           try
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(fileName);
               XmlNode rootElement = xmlDoc.SelectSingleNode("MyLibrary/Books");
               XmlElement elem = xmlDoc.CreateElement("book");
               XmlElement elemPath = xmlDoc.CreateElement("path");
               elemPath.InnerText = targetNode.FullPath + "\\" + sourceNode.Text;
               XmlElement elemName = xmlDoc.CreateElement("name");
               elemName.InnerText = sourceNode.Text;
               XmlElement elemRealPath = xmlDoc.CreateElement("realpath");
               elemRealPath.InnerText = sourceNode.Tag.ToString();
               elem.AppendChild(elemPath);
               elem.AppendChild(elemName);
               elem.AppendChild(elemRealPath);
               rootElement.AppendChild(elem);
               xmlDoc.Save(fileName); 
           }
           catch (Exception e)
           {
               errLog.WriteError(e);
               MessageBox.Show(Constants.UNKNOWERROR,
                    Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
           }

        }

        /// <summary>
        /// 把源节点添加到目标节点上
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        public static void RecordBookInfo(TreeNode sourceNode, TreeNode targetNode)
        {
            try
            {
                string fileName = CommonUtil.GetRelativeAppDir(targetNode.FullPath + Constants.FILENAME);
                if (!File.Exists(fileName))
                {
                    InitBookInfoFile(fileName);
                }
                WriteInfoToFile(fileName, sourceNode, targetNode);
            }
            catch (Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                     Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 重命名节点时更新相应的book.xml文件信息
        /// </summary>
        /// <param name="node"></param>
        public static void ModifyBookFile(TreeNode node)
        {
            if (node.Nodes.Count != 0)
            {
                try
                {
                    List<TreeNode> leafNodes = new List<TreeNode>();
                    List<TreeNode> parentNodes = new List<TreeNode>();
                    AnalyseCurNode(node, ref leafNodes, ref parentNodes);
                    if (leafNodes.Count != 0)
                    {
                        ModifyBookInfo(leafNodes[0].Parent.FullPath);
                    }
                    if (parentNodes.Count != 0)
                    {
                        foreach (TreeNode parentNode in parentNodes)
                        {
                            ModifyBookFile(parentNode);
                        }
                    }
                }
                catch (Exception e)
                {
                    errLog.WriteError(e);
                    MessageBox.Show(Constants.UNKNOWERROR,
                         Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        /// <summary>
        /// 更新book具体信息中的path信息
        /// </summary>
        /// <param name="filePath"></param>
        private static void ModifyBookInfo(string filePath)
        {
            try
            {
                string file = CommonUtil.GetRelativeAppDir(filePath + Constants.FILENAME);
                //判断是否为空文件夹
                if (File.Exists(file))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(file);
                    XmlNodeList listNodes = xmlDoc.SelectNodes("//book");
                    foreach (XmlNode node in listNodes)
                    {
                        XmlElement elemPath = xmlDoc.CreateElement("path");
                        string fileName = node.ChildNodes[BookInfoIndex.NAME].InnerText;
                        elemPath.InnerText = filePath + "\\" + fileName;
                        node.ReplaceChild(elemPath, node.FirstChild);
                    }
                    xmlDoc.Save(file);
                }
            }
            catch (Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                     Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        /// <summary>
        /// 得到一个节点的叶子节点和包含叶子节点的父节点
        /// </summary>
        /// <param name="curNode"></param>
        /// <param name="leafNodes"></param>
        /// <param name="parentNodes"></param>
        public static void AnalyseCurNode(TreeNode curNode, ref List<TreeNode> leafNodes, ref List<TreeNode> parentNodes)
        {
            TreeNodeCollection nodes = curNode.Nodes;
            foreach (TreeNode node in nodes)
            {
                //可以把空文件夹算成叶子节点，到ModifyBookInfo去处理
                //if (node.Nodes.Count != 0)
                if(node.Tag==null)
                {
                    parentNodes.Add(node);
                }
                else
                {
                    leafNodes.Add(node);
                }
            }

        }

        /// <summary>
        /// 判断源节点是否在目标节点下
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static bool ContainNode(TreeNode sourceNode, TreeNode targetNode)
        {
            if (targetNode.Parent == null)
            {
                return false;
            }
            if (targetNode.Parent.Equals(sourceNode))
            {
                return true;
            }
            return ContainNode(sourceNode, targetNode.Parent);
        }

        /// <summary>
        /// 判断源节点是否可以拖拽到目标节点
        /// </summary>
        /// <param name="draggedNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static bool CanDrag(TreeNode draggedNode, TreeNode targetNode)
        {
            if (targetNode.Tag != null
                || (targetNode.FullPath.IndexOf("\\") == -1 && draggedNode.Tag != null)
                || draggedNode.Parent == targetNode
                || draggedNode.Equals(targetNode)
                || ContainNode(draggedNode, targetNode)
                || targetNode.Nodes.ContainsKey(draggedNode.Name))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存一个book信息到book.xml文件
        /// </summary>
        /// <param name="book"></param>
        /// <param name="node"></param>
        public static void SaveBookInfo(Book book,TreeNode node)
        {
            string fileName = CommonUtil.GetRelativeAppDir(node.Parent.FullPath + Constants.FILENAME);
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode selectNode = xmlDoc.SelectSingleNode("//book[name=\"" + node.Text + "\"]");
                if (selectNode != null)
                {
                    selectNode.RemoveAll();
                    XmlElement path = xmlDoc.CreateElement("path");
                    path.InnerText = node.FullPath;
                    XmlElement nodeName = xmlDoc.CreateElement("name");
                    nodeName.InnerText = node.Text;
                    XmlElement realpath = xmlDoc.CreateElement("realpath");
                    realpath.InnerText = node.Tag.ToString();
                    XmlElement author = xmlDoc.CreateElement("author");
                    author.InnerText = book.Author;
                    XmlElement bookDate = xmlDoc.CreateElement("date");
                    bookDate.InnerText = book.Date;
                    XmlElement description = xmlDoc.CreateElement("description");
                    description.InnerText = book.Description;
                    selectNode.AppendChild(path);
                    selectNode.AppendChild(nodeName);
                    selectNode.AppendChild(realpath);
                    selectNode.AppendChild(author);
                    selectNode.AppendChild(bookDate);
                    selectNode.AppendChild(description);
                }
                xmlDoc.Save(fileName);
            }
            catch (Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                     Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据node节点创建一个xmlnode节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XmlNode CreateBookNode(TreeNode node)
        {
           try
           {
               string file = CommonUtil.GetRelativeAppDir(node.Parent.FullPath + Constants.FILENAME);
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(file);
               XmlNode searchNode = xmlDoc.SelectSingleNode("//book[name=\"" + node.Text + "\"]");
               return searchNode;
           }
            catch(Exception e)
            {
                errLog.WriteError(e);
                MessageBox.Show(Constants.UNKNOWERROR,
                     Constants.ERRORTIP, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
    }
}
