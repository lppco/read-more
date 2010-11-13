using System.Xml;

namespace read_more
{
    /// <summary>
    /// Book的实体类
    /// </summary>
    public class Book
    {
        private string name;
        private string isbn;
        private string author;
        private string price;
        private string press;
        private string date;
        private string description;

        public Book()
        {
        }

        /// <summary>
        /// 通过book.xml文件中的一个xmlnode节点实例化Book类
        /// </summary>
        /// <param name="bookNode"></param>
        public Book(XmlNode bookNode)
        {
            
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ISBN
        {
            get { return isbn; }
            set { isbn = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Press
        {
            get { return press; }
            set { press = value; }
        }

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
