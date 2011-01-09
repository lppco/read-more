using System.Collections.Generic;
using System.IO;

namespace read_more
{
    /// <summary>
    /// 跟文件类型排序
    /// </summary>
    public class SortType : IComparer<NodeBookMap>
    {
        public int Compare(NodeBookMap map1, NodeBookMap map2)
        {
            if (map1 == null)
            {
                if (map2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (map2 == null)
                {
                    return 1;
                }
                else
                {
                    string ext1 = Path.GetExtension(map1.Node.Tag.ToString());
                    string ext2 = Path.GetExtension(map2.Node.Tag.ToString());
                    int retval = ext1.CompareTo(ext2);
                    return retval;
                }
            }

        }
    }

}
