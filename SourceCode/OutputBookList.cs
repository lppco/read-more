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

        }

        public void AddOutputRange(ICollection ranges)
        {

        }

        public void BeginOutputList()
        {
     
        }
    }
}
