using System.IO;

namespace read_more
{
    /// <summary>
    /// 程序的工具类，提供一些常用函数
    /// </summary>
    public static class CommonUtil
    {
        /// <summary>
        /// 得到相对于程序启动目录的路径
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public static string GetRelativeAppDir(string relativePath)
        {
            return System.Windows.Forms.Application.StartupPath + "\\" + relativePath;
        }

        /// <summary>
        /// 从一个文件夹路径中得到文件夹的名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFolderName(string path)
        {
            return path.Substring(path.LastIndexOf("\\") + 1);
        }

        /// <summary>
        /// 从一个文件路径中得到文件的名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileName(string path)
        {
            return GetFolderName(path);
        }

        /// <summary>
        /// 根据路径创建一个文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 根据路径删除一个文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void DelDir(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// 得到一个路径的父路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentPath(string path)
        {
            int index = path.LastIndexOf(@"\");
            return path.Substring(0, index);   
        }

        /// <summary>
        /// 把源文件内的文件拷贝到目标文件夹内
        /// </summary>
        /// <param name="SourceDir"></param>
        /// <param name="DestDir"></param>
        /// <returns></returns>
        private static bool CopyFilesExt(string SourceDir, string DestDir)
        {
            string[] FileNames = Directory.GetFiles(SourceDir);
            for (int i = 0; i < FileNames.Length; i++)
                File.Copy(FileNames[i],
                    DestDir + FileNames[i].Substring(SourceDir.Length), true);
            return true;
        }

        /// <summary>
        /// 将源文件夹拷贝到目标文件夹
        /// </summary>
        /// <param name="SourceDir"></param>
        /// <param name="DestDir"></param>
        /// <returns></returns>
        public static bool CopyDirExt(string SourceDir, string DestDir)
        {
            DirectoryInfo diSource = new DirectoryInfo(SourceDir);
            DirectoryInfo diDest = new DirectoryInfo(DestDir);
            if (diSource.Exists)
            {
                if (!diDest.Exists)
                    diDest.Create();
                if (CopyFilesExt(SourceDir, DestDir))
                {
                    string[] SubDirs = Directory.GetDirectories(SourceDir);

                    bool bResult = true;
                    for (int i = 0; i < SubDirs.Length; i++)
                        if (!CopyDirExt(SubDirs[i] + @"\", DestDir + SubDirs[i].Substring(SourceDir.Length) + @"\"))
                            bResult = false;
                    return bResult;
                }
            }
            return false;
        }
    }
}
