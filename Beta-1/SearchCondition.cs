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
        private bool isFilterDate;
        private string fileType;
        private bool isFilterFileType;
        private TreeNode path;
        private ArrayList exts;
        private System.Collections.Generic.IComparer<NodeBookMap> comparer;

        public SearchCondition()
        {
            IsFilterDate = false;
            IsFilterFileType = false;
        }

        public SearchCondition(string keyWord,ICollection ranges):this()
        {
            this.KeyWord = keyWord;
            AddSearchRange(ranges);
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
            searchRange = new ArrayList();
            foreach (int o in ranges)
            {
                searchRange.Add(o);
            }
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
                exts=new ArrayList();
                exts.AddRange(fileType.Split(new char[]{' ','\t','\n'}));
            }
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
