using System;
using System.Windows.Forms;

namespace read_more
{
    public partial class frmRenNode : Form
    {
        /// <summary>
        /// 重命名节点的名称
        /// </summary>
        private string renNodeName;
        /// <summary>
        /// 重命名节点的父节点，检查重命名节点的名称是否与已知节点的名称有重复
        /// </summary>
        private readonly TreeNode parentNode;

        public frmRenNode(string curNodeName,TreeNode node)
        {
            InitializeComponent();
            parentNode = node;
            this.txtOldNodeName.Text = curNodeName;
        }

        public string RenNodeName
        {
            get { return renNodeName; }
            set { renNodeName = value; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtRenNodeName.Text=="")
            {
                this.errRepeatedName.SetError(this.txtRenNodeName, "节点名称不能为空");
            }
            else if (parentNode != null && parentNode.Nodes.ContainsKey(this.txtRenNodeName.Text))
            {
                this.errRepeatedName.SetError(this.txtRenNodeName, "节点名称重复");
            }
            else
            {
                RenNodeName = this.txtRenNodeName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
