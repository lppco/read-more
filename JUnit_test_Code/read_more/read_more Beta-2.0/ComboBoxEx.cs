using System.Drawing;
using System.Windows.Forms;

namespace read_more
{
    /// <summary>
    /// 查找路径时的下拉列表，由于列表项前面要加一个图标，所以重载了ComboBox类
    /// </summary>
    public class ComboBoxEx : ComboBox
    {
        private ImageList imageList;//需要显示的图标链表
        public ImageList ImageList
        {
            get { return imageList; }
            set { imageList = value; }
        }

        public ComboBoxEx()
        {
            DrawMode = DrawMode.OwnerDrawVariable;
        }

        protected override void OnDrawItem(DrawItemEventArgs ea)//重载显示方式
        {
            ea.DrawBackground();
            ea.DrawFocusRectangle();

            ComboBoxExItem item;
            Size imageSize = imageList.ImageSize;
            Rectangle bounds = ea.Bounds;

            try
            {
                item = (ComboBoxExItem)Items[ea.Index];

                if (item.ImageIndex != -1)
                {
                    if (item.ImageIndex != ImgUtil.GetIconIndex(Constants.LIBRARYEXT))
                    {
                        imageList.Draw(ea.Graphics, bounds.Left + imageSize.Width, bounds.Top, item.ImageIndex);
                        ea.Graphics.DrawString(item.Text, ea.Font, new
                          SolidBrush(ea.ForeColor), bounds.Left + 2*imageSize.Width, bounds.Top);
                    }
                    else
                    {//根据imagelist中的图标代码显示相应图标
                        imageList.Draw(ea.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                        ea.Graphics.DrawString(item.Text, ea.Font, new
                          SolidBrush(ea.ForeColor), bounds.Left + imageSize.Width, bounds.Top);
                    }

                }
                else
                {
                    ea.Graphics.DrawString(item.Text, ea.Font, new
                      SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
                }
            }
            catch
            {
                if (ea.Index != -1)
                {
                    ea.Graphics.DrawString(Items[ea.Index].ToString(), ea.Font, new
                      SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
                }
                else
                {
                    ea.Graphics.DrawString(Text, ea.Font, new
                      SolidBrush(ea.ForeColor), bounds.Left, bounds.Top);
                }
            }

            base.OnDrawItem(ea);
        }
    }

    /// <summary>
    /// 查找路径的下拉列表项
    /// </summary>
    class ComboBoxExItem
    {
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private int imageIndex;
        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        public ComboBoxExItem()
            : this("")
        {
        }

        public ComboBoxExItem(string text)
            : this(text, -1)
        {
        }

        public ComboBoxExItem(string text, int imageIndex)
        {
            this.text = text;
            this.imageIndex = imageIndex;
        }

        public override string ToString()
        {
            return text;
        }

    }
}
