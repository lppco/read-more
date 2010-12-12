using System;
using System.Windows.Forms;

namespace read_more
{
    public partial class frmAboutSoft : Form
    {
        private static frmAboutSoft instance = null;
        
        private frmAboutSoft()
        {
            InitializeComponent();
        }

        public static frmAboutSoft GetInstance()
        {
            if(instance==null)
            {
                instance=new frmAboutSoft();
            }
            return instance;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
