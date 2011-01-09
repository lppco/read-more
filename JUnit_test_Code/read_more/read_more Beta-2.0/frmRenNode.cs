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
            setLanguage();
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
                if (frmMain.isChinese() == false)
                    this.errRepeatedName.SetError(this.txtRenNodeName, "节点名称不能为空");
                else
                    this.errRepeatedName.SetError(this.txtRenNodeName, "Please input node name!");
            }
            else if (parentNode != null && parentNode.Nodes.ContainsKey(this.txtRenNodeName.Text))
            {
                if (frmMain.isChinese() == false)
                    this.errRepeatedName.SetError(this.txtRenNodeName, "节点名称重复");
                else
                    this.errRepeatedName.SetError(this.txtRenNodeName, "The name is already existed!");

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

        /// <summary>
        /// 设置语言
        /// </summary>
        private void setLanguage()
        {
            bool choice = frmMain.isChinese();

            if (choice == false)
            {
                this.label1.Text = "原节点名称";
                this.label2.Text = "重命名节点名称";
                this.btnOK.Text = "确定";
                this.btnCancel.Text = "取消";
                this.Text = "重命名节点";
            }
            else
            {
                this.label1.Text = "Primary name";
                this.label2.Text = "New name";
                this.btnOK.Text = "OK";
                this.btnCancel.Text = "Cancel";
                this.Text = "Rename node";
            }//end else

        }
    }
}
