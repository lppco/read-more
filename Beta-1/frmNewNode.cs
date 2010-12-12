using System;
using System.Windows.Forms;

namespace read_more
{
    public partial class frmNewNode : Form
    {
        /// <summary>
        /// 新节点的名字
        /// </summary>
        private string newNodeName;
        /// <summary>
        /// 新建节点的父节点，检查新建节点的名称是否与已知节点的名称有重复
        /// </summary>
        private readonly TreeNode curNode;
        
        public frmNewNode(TreeNode node)
        {
            InitializeComponent();
            this.curNode = node;
        }

        public string NewNodeName
        {
            get { return newNodeName; }
            set { newNodeName = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(this.txtNewNodeName.Text=="")
            {
                this.errNewNodeName.SetError(this.txtNewNodeName,"节点名称不能为空");
            }
            else if(curNode.Nodes.ContainsKey(this.txtNewNodeName.Text))
            {
                this.errNewNodeName.SetError(this.txtNewNodeName, "节点名称重复");
            }
            else
            {
                NewNodeName = this.txtNewNodeName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
