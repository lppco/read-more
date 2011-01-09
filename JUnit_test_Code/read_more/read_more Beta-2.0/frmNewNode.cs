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
            setLanguage();
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
                if (frmMain.isChinese()==false)
                    this.errNewNodeName.SetError(this.txtNewNodeName,"节点名称不能为空");
                else
                    this.errNewNodeName.SetError(this.txtNewNodeName, "Please input node name!");
            }
            else if(curNode.Nodes.ContainsKey(this.txtNewNodeName.Text))
            {
                if (frmMain.isChinese() == false)
                    this.errNewNodeName.SetError(this.txtNewNodeName, "节点名称重复");
                else
                    this.errNewNodeName.SetError(this.txtNewNodeName, "This name already exist!");
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

        /// <summary>
        /// 设置语言
        /// </summary>
        private void setLanguage()
        {
            bool choice = frmMain.isChinese();

            if (choice == false)
            {
                this.label1.Text = "新节点名称";
                this.btnOK.Text = "确定";
                this.btnCancel.Text = "取消";
                this.Text = "新建节点";
            }
            else
            {
                this.label1.Text = "Node name";
                this.btnOK.Text = "OK";
                this.btnCancel.Text = "Cancel";
                this.Text = "New a node";
            }//end else

        }
    }
}
