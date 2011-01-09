using read_more;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestProjectReadMore
{
    /// <summary>
    ///这是 CommonUtilTest 的测试类，旨在
    ///包含所有 CommonUtilTest 单元测试
    ///</summary>
    [TestClass()]
    public class CommonUtilTest
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
        ///GetRelativeAppDir 的测试
        ///</summary>
        [TestMethod()]
        public void GetRelativeAppDirTest()
        {
            string relativePath = "read"; // TODO: 初始化为适当的值
            string expected = System.Windows.Forms.Application.StartupPath + "\\" + "read"; // TODO: 初始化为适当的值
            string actual;
            actual = CommonUtil.GetRelativeAppDir(relativePath);
            Assert.AreEqual(expected, actual);
            // Assert.Inconclusive("验证此测试方法的正确性。");
        }
        /// <summary>
        ///GetFileName 的测试
        ///</summary>
        [TestMethod()]
        public void GetFileNameTest()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\" + "read.pdf"; // TODO: 初始化为适当的值
            string expected = "read.pdf"; // TODO: 初始化为适当的值
            string actual;
            actual = CommonUtil.GetFileName(path);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///GetFolderName 的测试
        ///</summary>
        [TestMethod()]
        public void GetFolderNameTest()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\" + "read"; // TODO: 初始化为适当的值
            string expected = "read"; // TODO: 初始化为适当的值
            string actual;
            actual = CommonUtil.GetFolderName(path);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CreateDir 的测试
        ///</summary>
        [TestMethod()]
        public void CreateDirTest()
        {
            string path = "D://read"; // TODO: 初始化为适当的值
            CommonUtil.CreateDir(path);
            bool afterCreate = Directory.Exists(path);
            Assert.IsTrue(afterCreate);
        }

        /// <summary>
        ///DelDir 的测试
        ///</summary>
        [TestMethod()]
        public void DelDirTest()
        {
            string path = "D://read"; // TODO: 初始化为适当的值
            CommonUtil.DelDir(path);
            bool afterDelete = Directory.Exists(path);
            Assert.IsFalse(afterDelete);
        }

        /// <summary>
        ///GetParentPath 的测试
        ///</summary>
        [TestMethod()]
        public void GetParentPathTest()
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\" + "read"; // TODO: 初始化为适当的值
            string expected = System.Windows.Forms.Application.StartupPath; // TODO: 初始化为适当的值
            string actual;
            actual = CommonUtil.GetParentPath(path);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///CopyFilesExt 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("read_more.exe")]
        public void CopyFilesExtTest()
        {
            string SourceDir = "D://Test/myoj"; // 源文件夹
            string DestDir = "D://Test/myoj1"; // 目标文件夹
            createFileAndDirectory();//创建文件与目录

            string[] getFile = new string[2];//将存储文件名称
            getFile = Directory.GetFiles(SourceDir);//获得源文件夹的文件

            CommonUtil_Accessor.CopyFilesExt(SourceDir, DestDir);

            string[] expectedFile = new string[2];//将存储文件名称
            expectedFile = Directory.GetFiles(DestDir);//获得目标文件夹的文件

            string expected = null;
            string actual = null;
            for (int i = 0; i < expectedFile.Length; i++)
            {
                expected = expectedFile[i].Substring(DestDir.Length + 1, expectedFile[i].Length - DestDir.Length - 1);
                actual = getFile[i].Substring(SourceDir.Length + 1, getFile[i].Length - SourceDir.Length - 1);
                Assert.AreEqual(expected, actual);
            }
        }

        public void createFileAndDirectory()
        {
            string SourceDir = "D://Test/myoj"; // 源文件夹
            string SourceDirectory1 = "D://Test/myoj/read1";// 源文件夹下的第一个子目录
            string SourceDirectory2 = "D://Test/myoj/read2";// 源文件夹下的第二个子目录
            string fileCreate1 = "D://Test/myoj/readMore.txt";//创建的第一个文件名
            string fileCreate2 = "D://Test/myoj/readMore.pdf";//创建的第二个文件名
            string DestDir = "D://Test/myoj1"; // 目标文件夹

            if (!Directory.Exists(SourceDir))
            {
                Directory.CreateDirectory(SourceDir);//创建源文件
            }
            if (!Directory.Exists(DestDir))
            {
                Directory.CreateDirectory(DestDir);//创建目标文件
            }
            if (!Directory.Exists(SourceDirectory1))
            {
                Directory.CreateDirectory(SourceDirectory1);//创建源文件下的第一个子目录
            }
            if (!Directory.Exists(SourceDirectory2))
            {
                Directory.CreateDirectory(SourceDirectory2);//创建源文件下的第二个子目录
            }
            if (!File.Exists(fileCreate1))
            {
                File.Create(fileCreate1);//在源文件夹创建第一个文件
            }
            if (!File.Exists(fileCreate2))
            {
                File.Create(fileCreate2);//在源文件夹创建第二个文件
            }

        }

        /// <summary>
        ///CopyDirExt 的测试
        ///</summary>
        [TestMethod()]
        public void CopyDirExtTest()
        {
            string SourceDir = "D://Test/myoj"; // 源文件夹
            string DestDir = "D://Test/myoj1"; // 目标文件夹
            //int lengthSoure=SourceDir.Length;
            //int lengthDest=DestDir.Length;
            createFileAndDirectory();

            string[] getDirectory = new string[2];//将存储子目录名称
            string[] getFile = new string[2];//将存储文件名称
            getDirectory = Directory.GetDirectories(SourceDir);//获得源文件夹的子目录
            getFile = Directory.GetFiles(SourceDir);//获得源文件夹的文件

            CommonUtil_Accessor.CopyDirExt(SourceDir, DestDir);

            string[] expectedDirectory = new string[2];//将存储子目录名称
            string[] expectedFile = new string[2];//将存储文件名称
            expectedDirectory = Directory.GetDirectories(DestDir);//获得目标文件夹的子目录
            expectedFile = Directory.GetFiles(DestDir);//获得目标文件夹的文件

            string expected = null;
            string actual = null;

            for (int i = 0; i < expectedFile.Length; i++)
            {
                expected = expectedFile[i].Substring(DestDir.Length + 1, expectedFile[i].Length - DestDir.Length - 1);
                actual = getFile[i].Substring(SourceDir.Length + 1, getFile[i].Length - SourceDir.Length - 1);
                Assert.AreEqual(expected, actual);
            }

            for (int i = 0; i < expectedDirectory.Length; i++)
            {
                expected = expectedDirectory[i].Substring(DestDir.Length + 1, expectedDirectory[i].Length - DestDir.Length - 1);
                actual = getDirectory[i].Substring(SourceDir.Length + 1, getDirectory[i].Length - SourceDir.Length - 1);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
