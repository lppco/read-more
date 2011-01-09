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
            setLanguage();
        }

        public static frmAboutAuthor GetInstance()
        {
            instance = new frmAboutAuthor();
            return instance;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         /// <summary>
        /// 设置语言
        /// </summary>
        private void setLanguage()
        {
            bool choice = frmMain.isChinese();

            if (choice == false)
            {
                this.label1.Text = "制作团队";              
                this.label2.Text = "小组信息";
                this.label3.Text = "说明";
                this.label4.Text = "悦·我";
                this.label5.Text = "这是我们软件工程的一个项目，\r\n虽然这是我们第一次尝试，\r\n项目质量可能有所偏颇，\r\n但是我们会努力做到最好！";
                this.label6.Text = "连燚，彭姣，欧君\r\n陈虹霈，彭玉媛\r\n08embeded，ss，sysu";
                this.btnOK.Text = "确定";            
                this.Text = "关于作者";
            }
            else
            {
                this.label1.Text = "Team";
                this.label2.Text = "Members";
                this.label3.Text = "Introduce";
                this.label4.Text = "悦·我";
                this.label5.Text = "This is a course project of SE,\r\nthough this is our first time\r\n"
                                +" to make a software, we will try\r\n our best!";
                this.label6.Text = "连燚，彭姣，欧君\r\n陈虹霈，彭玉媛\r\n08embeded，ss，sysu";
                this.btnOK.Text = "OK";
                this.Text = "About Author";
            }//end else

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
