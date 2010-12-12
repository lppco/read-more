namespace read_more
{
    /// <summary>
    /// 处理图像的帮助类
    /// </summary>
    public class ImgUtil
    {
        /// <summary>
        /// 根据文件的扩展名返回相应的icon图标在imagelist中的位置
        /// </summary>s
        /// <param name="ext">文件的扩展名</param>
        /// <returns>icon索引</returns>
        public static int GetIconIndex(string ext)
        {
            int index;
            switch(ext)
            {
                case ".c":
                    index = IconIndex.C;
                    break;
                case ".cpp":
                    index = IconIndex.CPP;
                    break;
                case ".doc":
                case ".docx":
                    index = IconIndex.DOC;
                    break;
                case ".folder":
                    index = IconIndex.FOLDER;
                    break;
                case ".html":
                case ".htm":
                    index = IconIndex.HTML;
                    break;
                case ".pdf":
                    index = IconIndex.PDF;
                    break;
                case ".ppt":
                case ".pptx":
                    index = IconIndex.PPT;
                    break;
                case ".txt":
                    index = IconIndex.TXT;
                    break;
                case ".xls":
                case ".xlsx":
                    index = IconIndex.XLS;
                    break;
                case ".xml":
                    index = IconIndex.XML;
                    break;
                default:
                    index = IconIndex.NEW;
                    break;
            }
            return index;
        }
    }
}
