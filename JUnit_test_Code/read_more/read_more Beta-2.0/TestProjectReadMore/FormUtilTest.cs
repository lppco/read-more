using read_more;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;

namespace TestProjectReadMore
{
    /// <summary>
    ///这是 FormUtilTest 的测试类，旨在
    ///包含所有 FormUtilTest 单元测试
    ///</summary>
    [TestClass()]
    public class FormUtilTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///GetNewNode 的测试
        ///</summary>
        [TestMethod()]
        public void GetNewNodeTest()
        {
            string nodeName = "readmore"; // TODO: 初始化为适当的值
            string nodeText = "oujun"; // TODO: 初始化为适当的值
            int nodeImgIndex = 1; // TODO: 初始化为适当的值
            ContextMenuStrip nodeCntMnu = new ContextMenuStrip(); // TODO: 初始化为适当的值
            nodeCntMnu = null;
            string nodeTag = "yuewo"; // TODO: 初始化为适当的值

            TreeNode expected = new TreeNode();// TODO: 初始化为适当的值
            expected.Name = "readmore";
            expected.Text = "oujun";
            expected.ImageIndex = 1;
            expected.ContextMenuStrip = null;
            expected.Tag = "yuewo";

            TreeNode actual;
            actual = FormUtil.GetNewNode(nodeName, nodeText, nodeImgIndex, nodeCntMnu, nodeTag);
            // Assert.AreSame(expected, actual);
            // Assert.AreEqual(expected, actual);
            //  Assert.AreEqual<TreeNode>(expected, actual);
            //Assert.Equals(expected, actual);
            Assert.AreEqual(expected.Text, actual.Text);
            Assert.AreEqual(expected.Tag, actual.Tag);
            Assert.AreEqual(expected.ImageIndex, actual.ImageIndex);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ContextMenuStrip, actual.ContextMenuStrip);
        }


        /// <summary>
        ///ContainNode 的测试
        ///</summary>
        [TestMethod()]
        public void ContainNodeTest()
        {
            TreeNode targetNode = new TreeNode();// 第一个树节点
            targetNode.Name = "read";
            targetNode.Text = "read";
            targetNode.ImageIndex = 1;
            targetNode.ContextMenuStrip = null;
            targetNode.Tag = "read";

            TreeNode Node = new TreeNode();//第二个树节点
            Node.Name = "readmore";
            Node.Text = "oujun";
            Node.ImageIndex = 1;
            Node.ContextMenuStrip = null;
            Node.Tag = "yuewo";

            TreeNode sourceNode = new TreeNode();//第三个树节点
            sourceNode.Name = "D://readmore";
            sourceNode.Text = "D://oujun";
            sourceNode.ImageIndex = 1;
            sourceNode.ContextMenuStrip = null;
            sourceNode.Tag = "D://yuewo";

            sourceNode.Nodes.Add(targetNode);
            sourceNode.Nodes.Add(Node);
            Node.Nodes.Add(targetNode);

            //下面的三条语句对三种情况进行测试

            // Assert.AreEqual(sourceNode, targetNode.Parent);

            Assert.AreEqual(Node, targetNode.Parent);

        }

        /// <summary>
        ///GetNewListItem 的测试
        ///</summary>
        [TestMethod()]
        public void GetNewListItemTest()
        {
            int imgIndex = 1; // TODO: 初始化为适当的值
            TreeNode node = newTreeNode("liudehua.mp3", "tttt", 2, "C://Users/oujun/Desktop/练习/liudehua.mp3", null); // TODO: 初始化为适当的值
            ListViewItem expected = new ListViewItem(); // TODO: 初始化为适当的值
            expected = null;
            ListViewItem actual;
            actual = FormUtil.GetNewListItem(imgIndex, node);
            Assert.AreEqual(1, actual.ImageIndex);
        }


        /// <summary>
        ///CanDrag 的测试
        ///</summary>
        [TestMethod()]
        public void CanDragTest()
        {

            TreeNode draggedNode = newTreeNode("readmore", "we", 2, "yuewo", null);
            TreeNode targetNode = newTreeNode("readmore", "we", 1, "yuewo", null);
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = FormUtil.CanDrag(draggedNode, targetNode);
            Assert.AreEqual(expected, actual);

            TreeNode equalNode1 = newTreeNode("readmore", "we", 1, "yuewo", null);
            TreeNode equalNode2 = newTreeNode("readmore", "we", 1, "yuewo", null);
            Assert.AreEqual(expected, FormUtil.CanDrag(equalNode1, equalNode2));

        }

        private TreeNode newTreeNode(string name, string text, int image, string tag, ContextMenuStrip nodeCntMnu)
        {
            TreeNode targetNode = new TreeNode(); // TODO: 初始化为适当的值
            targetNode.Name = name;
            targetNode.Text = text;
            targetNode.ImageIndex = image;
            targetNode.ContextMenuStrip = nodeCntMnu;
            targetNode.Tag = tag;
            return targetNode;
        }

        /// <summary>
        ///AnalyseCurNode 的测试
        ///</summary>
        [TestMethod()]
        public void AnalyseCurNodeTest()
        {
            TreeNode Node1 = newTreeNode("readmore", "we", 2, "yuewo", null);
            TreeNode Node2 = newTreeNode("more", "we", 1, "yuewo", null);
            TreeNode curNode = newTreeNode("readmore", "we", 1, "yuewo", null); // TODO: 初始化为适当的值
            curNode.Nodes.Add(Node1);
            curNode.Nodes.Add(Node2);
            List<TreeNode> leafNodes = new List<TreeNode>(); // TODO: 初始化为适当的值
            leafNodes.Clear();
            leafNodes.Add(Node2);
            List<TreeNode> parentNodes = new List<TreeNode>(); // TODO: 初始化为适当的值
            parentNodes.Clear();
            parentNodes.Add(Node2);
            FormUtil.AnalyseCurNode(curNode, ref leafNodes, ref parentNodes);
            Assert.AreEqual(3, leafNodes.Count);
            Assert.AreEqual(1, parentNodes.Count);

            TreeNode TagNull = newTreeNode("readmore", "ours", 2, null, null);
            // testNull = null;
            TreeNode parent = newTreeNode("readmore", "ours", 2, "yuewo", null);
            parent.Nodes.Add(TagNull);
            parentNodes.Clear();
            FormUtil.AnalyseCurNode(parent, ref leafNodes, ref parentNodes);
            Assert.AreEqual(1, parentNodes.Count);
        }

        /// <summary>
        ///CreateBookNode 的测试
        ///</summary>
        [TestMethod()]
        public void CreateBookNodeTest()
        {
            TreeNode node = null; // TODO: 初始化为适当的值
            XmlNode expected = null; // TODO: 初始化为适当的值
            XmlNode actual;
            actual = FormUtil.CreateBookNode(node);
            Assert.AreEqual(expected, actual);
          //  Assert.Inconclusive("验证此测试方法的正确性。");
        }

    }

}