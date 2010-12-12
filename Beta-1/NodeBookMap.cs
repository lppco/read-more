using System.Windows.Forms;

namespace read_more
{
    /// <summary>
    /// 树节点和book实例的映射累
    /// </summary>
    public class NodeBookMap
    {
        private TreeNode node;
        private Book nodeBook;

        
        public NodeBookMap()
        {
        }

        public NodeBookMap(TreeNode node,Book book)
        {
            this.Node = node;
            this.NodeBook = book;
        }


        public TreeNode Node
        {
            get { return node; }
            set { node = value; }
        }

        public Book NodeBook
        {
            get { return nodeBook; }
            set { nodeBook = value; }
        }
    }
}
