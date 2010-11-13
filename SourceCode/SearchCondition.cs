using System;
using System.Collections;
using System.Windows.Forms;

namespace read_more
{
    /// <summary>
    /// 查找条件的实体类
    /// </summary>
    public class SearchCondition
    {
        private string keyWord;
        private IList searchRange;
        private DateTime pressDateFrom;
        private DateTime pressDateTo;
        private bool isFilterDate;
        private string fileType;
        private bool isFilterFileType;
        private string priceFrom;
        private string priceTo;
        private bool isFilterPrice;
        private TreeNode path;
        private ArrayList exts;
        private System.Collections.Generic.IComparer<NodeBookMap> comparer;

        public SearchCondition()
        {

        }

        public SearchCondition(string keyWord, ICollection ranges)
            : this()
        {
   
        }

        public string KeyWord
        {
            get { return keyWord; }
            set { keyWord = value; }
        }

        public IList SearchRange
        {
            get { return searchRange; }
        }

        public void AddSearchRange(ICollection ranges)
        {
   
        }

        public DateTime PressDateFrom
        {
            get { return pressDateFrom; }
            set { pressDateFrom = value; }
        }

        public DateTime PressDateTo
        {
            get { return pressDateTo; }
            set { pressDateTo = value; }
        }

        public bool IsFilterDate
        {
            get { return isFilterDate; }
            set { isFilterDate = value; }
        }

        public string FileType
        {
            get { return fileType; }
            set
            {
                fileType = value;
                exts = new ArrayList();
                exts.AddRange(fileType.Split(new char[] { ' ', '\t', '\n' }));
            }
        }

        public string PriceFrom
        {
            get { return priceFrom; }
            set { priceFrom = value; }
        }

        public string PriceTo
        {
            get { return priceTo; }
            set { priceTo = value; }
        }

        public TreeNode Path
        {
            get { return path; }
            set { path = value; }
        }

        public bool IsFilterFileType
        {
            get { return isFilterFileType; }
            set { isFilterFileType = value; }
        }

        public bool IsFilterPrice
        {
            get { return isFilterPrice; }
            set { isFilterPrice = value; }
        }

        public ArrayList Exts
        {
            get { return exts; }
        }

        public System.Collections.Generic.IComparer<NodeBookMap> SortComparer
        {
            get { return comparer; }
            set { comparer = value; }
        }
    }
}
