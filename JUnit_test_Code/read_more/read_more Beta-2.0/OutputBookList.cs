using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace read_more
{
    /// <summary>
    /// 回调主窗体方法的委托，隐藏进度条提示的窗体
    /// </summary>
    public delegate void UpdateMainUICallback();

    /// <summary>
    /// 输出根目录下book列表信息
    /// </summary>
    public class OutputBookList
    {
        private string filePath;
        private IList outputRange;
        private SearchCondition condition;
        private UpdateMainUICallback updateUI;

        public OutputBookList(string path, SearchCondition conditon, UpdateMainUICallback callback)
        {
            this.filePath = path;
            this.condition = conditon;
            this.updateUI = callback;
        }

        public void AddOutputRange(ICollection ranges)
        {
            outputRange = new ArrayList();
            foreach (int o in ranges)
            {
                outputRange.Add(o);
            }
        }

        public void BeginOutputList()
        {
            SearchBook search = new SearchBook();
            List<NodeBookMap> result = search.Search(this.condition);
           
            using (StreamWriter sw = new StreamWriter(this.filePath, false))
            {
                foreach (NodeBookMap map in result)
                {
                    sw.WriteLine("书名: " + map.NodeBook.Name);
                    foreach (int index in this.outputRange)
                    {
                        switch (index)
                        {
                            case OutputBookIndex.AUTHOR:
                                sw.WriteLine(Constants.TABINDENT + "作者: " + map.NodeBook.Author);
                                break;
                            case OutputBookIndex.DATE:
                                sw.WriteLine(Constants.TABINDENT + "日期: " + map.NodeBook.Date);
                                break;
                            case OutputBookIndex.DESCRIPTION:
                                sw.WriteLine(Constants.TABINDENT + "描述: " + map.NodeBook.Description);
                                break;
                            case OutputBookIndex.REALPATH:
                                sw.WriteLine(Constants.TABINDENT + "实际路径: " + map.Node.Tag);
                                break;
                        }
                    }
                }
                sw.Flush();
                sw.Close();
            }
            if (updateUI != null)
            {
                updateUI();
            }
        }
    }
}
