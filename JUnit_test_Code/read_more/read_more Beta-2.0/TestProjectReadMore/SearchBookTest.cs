using read_more;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestProjectReadMore
{


    /// <summary>
    ///这是 SearchBookTest 的测试类，旨在
    ///包含所有 SearchBookTest 单元测试
    ///</summary>
    [TestClass()]
    public class SearchBookTest
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
        ///CheckPressDate 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("read_more.exe")]
        public void CheckPressDateTest()
        {
            DateTime pressDate = new DateTime(); // TODO: 初始化为适当的值
            DateTime dateFrom = new DateTime(); // TODO: 初始化为适当的值
            DateTime dateTo = new DateTime(); // TODO: 初始化为适当的值
            bool expected = true; // TODO: 初始化为适当的值
            bool actual;
            actual = SearchBook_Accessor.CheckPressDate(pressDate, dateFrom, dateTo);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CheckFileType 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("read_more.exe")]
        public void CheckFileTypeTest()
        {
            IList fileTypes = new List<SearchBook>(); // TODO: 初始化为适当的值
            string path = string.Empty; // TODO: 初始化为适当的值
            bool expected = false; // TODO: 初始化为适当的值
            bool actual;
            actual = SearchBook_Accessor.CheckFileType(fileTypes, path);
            Assert.AreEqual(expected, actual);
        }

    }
}
