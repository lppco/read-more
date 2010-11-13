using System.Collections;
using System.Windows.Forms;

namespace read_more
{
    /// <summary>
    /// 点击列表表头时排序列表项
    /// </summary>
    class ListViewColumnSorter : IComparer
    {
        private int ColumnToSort;

        public int SortColumn
        {
            get { return ColumnToSort; }
            set { ColumnToSort = value; }
        }

        private SortOrder OrderOfSort;

        public SortOrder Order
        {
            get { return OrderOfSort; }
            set { OrderOfSort = value; }
        }
        private CaseInsensitiveComparer ObjectCompare;

        public ListViewColumnSorter()
        {
  
        }

        int IComparer.Compare(object x, object y)
        {
    
        }
    }
}
