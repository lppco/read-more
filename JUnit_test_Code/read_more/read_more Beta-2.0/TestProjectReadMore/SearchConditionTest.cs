using read_more;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TestProjectReadMore
{


    /// <summary>
    ///这是 SearchConditionTest 的测试类，旨在
    ///包含所有 SearchConditionTest 单元测试
    ///</summary>
    [TestClass()]
    public class SearchConditionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///SortComparer 的测试
        ///</summary>
        [TestMethod()]
        public void SortComparerTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            IComparer<NodeBookMap> expected = null; // TODO: 初始化为适当的值
            IComparer<NodeBookMap> actual;
            target.SortComparer = expected;
            actual = target.SortComparer;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///SearchRange 的测试
        ///</summary>
        [TestMethod()]
        public void SearchRangeTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            IList expected = null;
            IList actual;
            actual = target.SearchRange;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Path 的测试
        ///</summary>
        [TestMethod()]
        public void PathTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            TreeNode expected = null; // TODO: 初始化为适当的值
            TreeNode actual;
            target.Path = expected;
            actual = target.Path;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///KeyWord 的测试
        ///</summary>
        [TestMethod()]
        public void KeyWordTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.KeyWord = expected;
            actual = target.KeyWord;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///IsFilterFileType 的测试
        ///</summary>
        [TestMethod()]
        public void IsFilterFileTypeTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.IsFilterFileType = expected;
            actual = target.IsFilterFileType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///IsFilterDate 的测试
        ///</summary>
        [TestMethod()]
        public void IsFilterDateTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            target.IsFilterDate = expected;
            actual = target.IsFilterDate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///FileType 的测试
        ///</summary>
        [TestMethod()]
        public void FileTypeTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.FileType = expected;
            actual = target.FileType;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Exts 的测试
        ///</summary>
        [TestMethod()]
        public void ExtsTest()
        {
            SearchCondition target = new SearchCondition(); // TODO: 初始化为适当的值
            ArrayList expected = null;
            ArrayList actual;
            actual = target.Exts;
            Assert.AreEqual(expected, actual);
        }

      
    }
}

