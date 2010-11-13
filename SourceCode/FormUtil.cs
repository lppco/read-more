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
        public static TreeNode GetNewNode(string nodeName, string nodeText, int nodeImgIndex,
            ContextMenuStrip nodeCntMnu, string nodeTag)
        {
        }

        /// <summary>
        /// 根据树节点新建一个列表项
        /// </summary>
        /// <param name="imgIndex"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static ListViewItem GetNewListItem(int imgIndex, TreeNode node)
        {

        }

        /// <summary>
        /// 如果还没有存放ebook信息的文件，则新建一个
        /// </summary>
        /// <param name="fileName"></param>
        public static void InitBookInfoFile(string fileName)
        {

        }

        /// <summary>
        /// 把一本eBook的信息写到文件中
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        public static void WriteInfoToFile(string fileName, TreeNode sourceNode, TreeNode targetNode)
        {
            
        }

        /// <summary>
        /// 把源节点添加到目标节点上
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        public static void RecordBookInfo(TreeNode sourceNode, TreeNode targetNode)
        {
        
        }

        /// <summary>
        /// 重命名节点时更新相应的book.xml文件信息
        /// </summary>
        /// <param name="node"></param>
        public static void ModifyBookFile(TreeNode node)
        {

        }

        /// <summary>
        /// 更新book具体信息中的path信息
        /// </summary>
        /// <param name="filePath"></param>
        private static void ModifyBookInfo(string filePath)
        {
 

        }

        /// <summary>
        /// 得到一个节点的叶子节点和包含叶子节点的父节点
        /// </summary>
        /// <param name="curNode"></param>
        /// <param name="leafNodes"></param>
        /// <param name="parentNodes"></param>
        public static void AnalyseCurNode(TreeNode curNode, ref List<TreeNode> leafNodes, ref List<TreeNode> parentNodes)
        {
 
        }

        /// <summary>
        /// 判断源节点是否在目标节点下
        /// </summary>
        /// <param name="sourceNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static bool ContainNode(TreeNode sourceNode, TreeNode targetNode)
        {
        }

        /// <summary>
        /// 判断源节点是否可以拖拽到目标节点
        /// </summary>
        /// <param name="draggedNode"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        public static bool CanDrag(TreeNode draggedNode, TreeNode targetNode)
        {
  
        }

        /// <summary>
        /// 保存一个book信息到book.xml文件
        /// </summary>
        /// <param name="book"></param>
        /// <param name="node"></param>
        public static void SaveBookInfo(Book book, TreeNode node)
        {

        }

        /// <summary>
        /// 根据node节点创建一个xmlnode节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static XmlNode CreateBookNode(TreeNode node)
        {
 
        }
    }
}
