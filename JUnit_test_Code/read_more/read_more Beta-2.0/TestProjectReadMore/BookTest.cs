using read_more;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;

namespace TestProjectReadMore
{


    /// <summary>
    ///这是 BookTest 的测试类，旨在
    ///包含所有 BookTest 单元测试
    ///</summary>
    [TestClass()]
    public class BookTest
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

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
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
        ///Book 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void BookConstructorTest()
        {
            XmlNode bookNode = null; // TODO: 初始化为适当的值
            Book target = new Book(bookNode);
            Book expected = null;
            Assert.AreNotEqual(expected, target);
        }

        /// <summary>
        ///Book 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void BookConstructorTest1()
        {
            Book target = new Book();
            Book expected = null;
            Assert.AreNotEqual(expected, target);
        }

        /// <summary>
        ///Author 的测试
        ///</summary>
        [TestMethod()]
        public void AuthorTest()
        {
            Book target = new Book(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.Author = expected;
            actual = target.Author;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Date 的测试
        ///</summary>
        [TestMethod()]
        public void DateTest()
        {
            Book target = new Book(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.Date = expected;
            actual = target.Date;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///Description 的测试
        ///</summary>
        [TestMethod()]
        public void DescriptionTest()
        {
            Book target = new Book(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.Description = expected;
            actual = target.Description;
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///Name 的测试
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Book target = new Book(); // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);

        }
    }
}
