using System;
using System.Collections;
using System.Windows.Forms;

namespace read_more
{
    public partial class frmSetExport : Form
    {
       /// <summary>
       /// 得到已经选择了的输出信息的索引范围
       /// </summary>
        private IList outputItems;
        /// <summary>
        /// 单件模式
        /// </summary>
        public static frmSetExport instance = null;

        private frmSetExport()
        {
            InitializeComponent();
            setLanguage();
            outputItems = new ArrayList();
        }

        public IList OutputItems
        {
            get { return outputItems; }
        }

        public static frmSetExport GetInstance()
        {
            instance = new frmSetExport();
            return instance;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach(int index in this.chklSetExport.CheckedIndices)
            {
                outputItems.Add(index);
            }
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        private void setLanguage()
        {
            bool choice = frmMain.isChinese();
            this.chklSetExport.Items.Clear();
            if (choice == false)
            {
                this.lblSetExport.Text = "设置需要输出信息";
                this.chklSetExport.Items.AddRange(new object[] {
                 "作者",
                 "日期",
                 "描述",
                 "实际路径"});
                this.btnOK.Text = "确认";
                this.btnCancel.Text = "取消";
                this.Text = "设置输出信息";
            }
            else
            {
                this.lblSetExport.Text = "Set the info to display";
                this.chklSetExport.Items.AddRange(new object[] {
                 "Author",
                 "Date",
                 "Discription",
                 "Real path"});
                this.btnOK.Text = "OK";
                this.btnCancel.Text = "Cancel";
                this.Text = "Set the info to display";
            }//end else

        }
        private void chklSetExport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
