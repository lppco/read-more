using System;
using System.Windows.Forms;

namespace read_more
{
    public partial class frmAboutAuthor : Form
    {
        private static frmAboutAuthor instance = null;
        
        public frmAboutAuthor()
        {
            InitializeComponent();
        }

        public static frmAboutAuthor GetInstance()
        {
            if(instance==null)
            {
                instance=new frmAboutAuthor();
            }
            return instance;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
