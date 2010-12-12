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
            outputItems = new ArrayList();
        }

        public IList OutputItems
        {
            get { return outputItems; }
        }

        public static frmSetExport GetInstance()
        {
            if (instance == null)
            {
                instance = new frmSetExport();
            }
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

        private void chklSetExport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
