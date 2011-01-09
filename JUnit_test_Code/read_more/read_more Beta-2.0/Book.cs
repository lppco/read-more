using System.Xml;

namespace read_more
{
    /// <summary>
    /// Book的实体类
    /// </summary>
    public class Book
    {
        private string name;
        private string author;
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
            if(bookNode!=null)
            {
                Name = bookNode.ChildNodes[BookInfoIndex.NAME].InnerText;
                Author = bookNode.ChildNodes[BookInfoIndex.AUTHOR] == null
                    ? "" : bookNode.ChildNodes[BookInfoIndex.AUTHOR].InnerText;
                Date = bookNode.ChildNodes[BookInfoIndex.DATE] == null
                    ? "" : bookNode.ChildNodes[BookInfoIndex.DATE].InnerText;
                Description = bookNode.ChildNodes[BookInfoIndex.DESCRIPTION] == null
                    ? "" : bookNode.ChildNodes[BookInfoIndex.DESCRIPTION].InnerText;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
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
