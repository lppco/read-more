using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace read_more
{
    /// <summary>
    /// 提供查找方法接口
    /// </summary>
    public class SearchBook
    {
        /// <summary>
        /// 错误日志
        /// </summary>
        private TraceLog errLog = TraceLog.GetInstance();

        /// <summary>
        /// 符合要求的书籍列表信息
        /// </summary>
        List<NodeBookMap> resultBook = new List<NodeBookMap>();

        /// <summary>
        /// 根据查找条件进行查找符合条件的书籍
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<NodeBookMap> Search(SearchCondition condition)
        {

        }

        #region 过滤必填查找选项
        /// <summary>
        /// 根据关键字和书名进行过滤
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="ranges"></param>
        /// <param name="node"></param>
        private void SearchBookName(string keyWord, IList ranges, TreeNode node)
        {
        }

        /// <summary>
        /// 根据ISBN信息进行过滤
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="ranges"></param>
        private void SearchBookISBN(string keyWord, IList ranges)
        {
        }

        private void SearchBookAuthor(string keyWord, IList ranges)
        {
           
        }

        private void SearchBookPress(string keyWord, IList ranges)
        {
           
        }

        private void SearchBookDescription(string keyWord, IList ranges)
        {
           
        }
        #endregion

        #region 过滤选添查找选项
        /// <summary>
        /// 根据出版日期进行过滤
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        private void SearchPressDate(DateTime dateFrom, DateTime dateTo)
        {
          
        }

        private static bool CheckPressDate(DateTime pressDate, DateTime dateFrom, DateTime dateTo)
        {
           
        }

        private void SearchFileType(IList exts)
        {
            
        }

        private static bool CheckFileType(IList fileTypes, string path)
        {
           
        }

        private void SearchPrice(string priceFrom, string priceTo)
        {
           
        }

        private static bool CheckPrice(string pLow, string pHigh, string price)
        {
           
        }
        #endregion

    }
}
