namespace read_more 
{
    /// <summary>
    /// eBook信息在book.xml中的索引
    /// </summary>
    public static class BookInfoIndex
    {
        public const int PATH = 0;
        public const int NAME = 1;
        public const int REALPATH = 2;
        public const int ISBN = 3;
        public const int AUTHOR = 4;
        public const int PRICE = 5;
        public const int PRESS = 6;
        public const int DATE = 7;
        public const int DESCRIPTION = 8;
    }

    /// <summary>
    /// 必填查找选项中查找信息范围的索引
    /// </summary>
    public static class SearchRangeIndex
    {
        public const int NAME = 0;
        public const int ISBN = 1;
        public const int AUTHOR = 2;
        public const int PRESS = 3;
        public const int DESCRIPTION = 4;
    }

    /// <summary>
    /// 输出book信息时的电子书信息索引
    /// </summary>
    public static class OutputBookIndex
    {
        public const int ISBN = 0;
        public const int AUTHOR = 1;
        public const int DATE = 2;
        public const int PRICE = 3;
        public const int PRESS = 4;
        public const int DESCRIPTION = 5;
        public const int REALPATH = 6;

    }
}
