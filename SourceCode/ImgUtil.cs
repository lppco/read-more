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
            switch (ext)
            {
                case ".avi":
                    index = IconIndex.AVI;
                    break;
                case ".bmp":
                    index = IconIndex.BMP;
                    break;
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
                case ".h":
                    index = IconIndex.H;
                    break;
                case ".html":
                case ".htm":
                case ".css":
                case ".js":
                case ".jsp":
                case ".asp":
                case ".php":
                case ".aspx":
                    index = IconIndex.HTML;
                    break;
                case ".ini":
                case ".dll":
                    index = IconIndex.INI;
                    break;
                case ".iso":
                    index = IconIndex.ISO;
                    break;
                case ".java":
                case ".class":
                    index = IconIndex.JAVA;
                    break;
                case ".jpg":
                case ".jepg":
                    index = IconIndex.JPG;
                    break;
                case ".library":
                    index = IconIndex.LIBRARY;
                    break;
                case ".mp3":
                    index = IconIndex.MP3;
                    break;
                case ".mpeg":
                case ".mpg":
                    index = IconIndex.MPEG;
                    break;
                case ".pdf":
                    index = IconIndex.PDF;
                    break;
                case ".png":
                    index = IconIndex.PNG;
                    break;
                case ".ppt":
                case ".pptx":
                    index = IconIndex.PPT;
                    break;
                case ".rar":
                    index = IconIndex.RAR;
                    break;
                case ".rmvb":
                case ".rm":
                    index = IconIndex.RMVB;
                    break;
                case ".swf":
                case ".flv":
                case ".fla":
                    index = IconIndex.SWF;
                    break;
                case ".wav":
                    index = IconIndex.WAV;
                    break;
                case ".wmv":
                    index = IconIndex.WMV;
                    break;
                case ".xls":
                case ".xlsx":
                    index = IconIndex.XLS;
                    break;
                case ".zip":
                    index = IconIndex.ZIP;
                    break;
                /*case "new":
                    index = IconIndex.NEW;
                    break;*/
                default:
                    index = IconIndex.NEW;
                    break;
            }
            return index;
        }
    }
}
