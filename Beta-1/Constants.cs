namespace read_more
{
    /// <summary>
    /// 定义程序中用到的常量
    /// </summary>
    public static class Constants
    {
        public const string LIBRARYEXT = ".library";
        public const string FOLDEREXT = ".folder";
        public const string FOLDERSTR = "文件夹";
        public const string FILESTR = "文件";
        public const string FILENAME = "\\books.xml";
        public const string FILEFILTER =
            "all files(*.*)|*.*|pdf(*.pdf)|*.pdf|chm(*.chm)|*.chm|rar(*.rar)|*.rar|word(*.doc)|*.doc";

        public const string REPEATEDNODE = "新添加节点名称冲突，重复节点没有添加";
        public const string CHOOSELISTITEM = "请选择列表项后在进行右键操作";
        public const string ERRORTIP = "错误提示";
        public const string BEGINEXPORTEBOOK = "正在输出eBook信息列表...";
        public const string ENDEXPORTEBOOK = "输出完成";
        public const string BEGINUPDATE = "正在更新相关文件信息...";
        public const string ENDUPDATE = "更新完成";
        public const string ADDFOLDER = "正在添加文件夹，请稍等...";
        public const string ADDFINISH = "添加完成";
        public const string OPERATEFAIL = "操作失败，此文件类型没有关联到相应的程序";
        public const string FILEOPENFAIL = "您要打开的文件（目录）已被删除，是否删除此节点";
        public const string RENNODEFAIL = "重命名错误，您可能正在打开将要被重命名的文件夹\n请将其关闭，程序将再次尝试重命名";
        public const string DRAGFILEFAIL = "根节点下不能添加文件，叶子节点下不能添加任何文件及文件夹";
        public const string DRAGDROPTIP = "您可以拖拽树节点，列表项和文件夹到树中的任意节点";
        public const string UPDATEBOOKINFO = "请选择相应的eBook，然后在保存信息";
        public const string ERRPRICE = "请输入正确的价钱格式";
        public const string FILETYPETIP = "请按提示输入要查找的文件类型";
        public const string MUSTITEMTIP = "请正确填写必填查找选项";
        public const string SEARCHRESULT = "共查找到{0}条记录";
        public const string SEARCHNORESULT = "对不起，没有找到符合要求的记录";
        public const string STATRESULT = "共统计出{0}条记录";
        public const string STATNORESULT = "对不起，没有统计到符合要求的记录";
        public const string SELECTNODE = "请选择一个节点在添加文件或者文件夹";
        public const string HELPFILEDELETE = "帮助文档已经被您删除，不能打开";
        public const string UNKNOWERROR = "未知错误（您可能删除了程序的数据文件），请关闭后在重新打开软件";
        public const string APPREPEATED = "程序已经在运行";

        public const int FILENOTFOUNDCODE = 2;
        public const int NOTASSOCIATEDEXECODE = 1155;

        public const int EBOOKTAB = 0;
        public const int EBOOKSEARCHTAB = 1;
        public const int EBOOKSTATTAB = 2;
        public const int SIZEOFLEFT = 230;

        public const string PATTERN= @"^(\*\.\w+\s*)+";

        public const string TABINDENT = "      ";
    }
}
