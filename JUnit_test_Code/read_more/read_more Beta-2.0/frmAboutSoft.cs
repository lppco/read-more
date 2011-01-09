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
            setLanguage();
        }

        public static frmAboutSoft GetInstance()
        {
            instance=new frmAboutSoft();
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
                this.label1.Text = "软件名称";
                this.label2.Text = "版本";
                this.label3.Text = "开发平台";
                this.label4.Text = "介绍";
                this.label5.Text = "读善其身";
                this.label6.Text = "Beta";
                this.label7.Text = "VS2008,WIN XP,WIN7";
                this.label8.Text = "提供方便快捷的方式管理\r\n电子书，支持复杂的查找\r\n"
                                  +"方式，使得用户可以快速\r\n找出想要的书籍。\r\n由于小组成员水平有限，\r\n"
                                  +"软件发生错误在所难免，\r\n请大家谅解！";
                this.btnOK.Text = "确定";
                this.Text = "软件介绍";
            }
            else
            {
                this.label1.Text = "Name";
                this.label2.Text = "Version";
                this.label3.Text = "Develop";
                this.label4.Text = "Introduce";
                this.label5.Text = "读善其身";
                this.label6.Text = "Beta";
                this.label7.Text = "VS2008,WIN XP,WIN7";
                this.label8.Text = "Easy to Manage the book\r\n in PC，support complexed \r\n"
                                  + "search, then user can  \r\nfind the book in a tiny \r\ntime.Limited to our level, \r\n"
                                  + "there may be some bugs. \r\nSo what! Try it!";
                this.btnOK.Text = "OK";
                this.Text = "About software";
            }//end else

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
