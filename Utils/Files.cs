using System;
using System.IO;
using System.Text;

namespace Framework.FSO
{
    public class Files
    {
        /// <summary>
        /// 判断是否存在指定文件
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public Boolean IsExitFile(string Path)
        {
            if (Directory.Exists(Path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否存在指定文件 没有则创建
        /// </summary>
        /// <param name="Path">文件路径</param>
        public void IsCreateFile(string Path)
        {
            if (!Directory.Exists(Path))
            {
                FileInfo CreateFile = new FileInfo(Path);         //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                }
            }
        }

        

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="FolderPathName"></param>
        public void CreateFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    string CreatePath = System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString();
                    if (!Directory.Exists(CreatePath))
                    {
                        Directory.CreateDirectory(CreatePath);
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 清空指定文件夹
        /// </summary>
        /// <param name="FolderPathName"></param>
        public void DeleteChildFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    string CreatePath = System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString();
                    if (Directory.Exists(CreatePath))
                    {
                        Directory.Delete(CreatePath, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="FilePathName"></param>
        public void DeleteFile(string FilePathName)
        {
            try
            {
                FileInfo DeleFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString());
                DeleFile.Delete();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 创建指定文件
        /// </summary>
        /// <param name="FilePathName"></param>
        public void CreateFile(string FilePathName)
        {
            try
            {
                //创建文件夹 
                string[] strPath = FilePathName.Split('/');
                CreateFolder(FilePathName.Replace("/" + strPath[strPath.Length - 1].ToString(), "")); //创建文件夹 
                FileInfo CreateFile = new FileInfo(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString());         //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 删除整个文件夹及其字文件夹和文件
        /// </summary>
        /// <param name="FolderPathName"></param>
 
        public void DeleParentFolder(string FolderPathName)
        {

            try
            {
                DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(FolderPathName).ToString());
                if (DelFolder.Exists)
                {
                    DelFolder.Delete();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 在文件里追加内容
        /// </summary>
        /// <param name="FilePathName"></param>
        /// <param name="WriteWord"></param>
        public void ReWriteReadinnerText(string FilePathName, string WriteWord)
        {
            try
            {
                //建立文件夹和文件 
                //CreateFolder(FilePathName); 
                CreateFile(FilePathName);
                //得到原来文件的内容 
                FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.ReadWrite);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string OldString = FileReadWord.ReadToEnd().ToString();
                OldString = OldString + WriteWord;
                //把新的内容重新写入 
                StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);
                FileWrite.Write(WriteWord);
                //关闭 
                FileWrite.Close();
                FileReadWord.Close();
                FileRead.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 读取文件的内容
        /// </summary>
        /// <param name="FilePathName"></param>
        /// <returns></returns>
        public string ReaderFileData(string FilePathName)
        {
            try
            {

                FileStream FileRead = new FileStream(System.Web.HttpContext.Current.Server.MapPath(FilePathName).ToString(), FileMode.Open, FileAccess.Read);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string TxtString = FileReadWord.ReadToEnd().ToString();
                //关闭 
                FileReadWord.Close();
                FileRead.Close();
                return TxtString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix;
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="orignFile"></param>
        /// <param name="NewFile"></param>
        public void FileCopy(string orignFile, string NewFile)
        {
            File.Copy(System.Web.HttpContext.Current.Server.MapPath(orignFile), System.Web.HttpContext.Current.Server.MapPath(NewFile), true);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path"></param>
        public void FileDel(string Path)
        {
            File.Delete(System.Web.HttpContext.Current.Server.MapPath(Path));
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="orignFile"></param>
        /// <param name="NewFile"></param>
        public void FileMove(string orignFile, string NewFile)
        {
            File.Move(System.Web.HttpContext.Current.Server.MapPath(orignFile), System.Web.HttpContext.Current.Server.MapPath(NewFile));
        }

        /// <summary>
        /// 将指定文件夹下面的所有内容copy到目标文件夹下面  如果目标文件夹为只读属性就会报错。
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="aimPath"></param>
        public void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                string _srcPath = System.Web.HttpContext.Current.Server.MapPath(srcPath);
                string _aimPath = System.Web.HttpContext.Current.Server.MapPath(aimPath);
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (_aimPath[_aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    _aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(_aimPath))
                    Directory.CreateDirectory(_aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(_srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file, _aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, _aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="path"></param>
        public static void downFile(string path)
        {
            try
            {
                path = System.Web.HttpContext.Current.Server.MapPath(path);
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Charset = "UTF-8";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //  添加头信息，为"文件下载/另存为"对话框指定默认文件名  
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpContext.Current.Server.UrlEncode(file.Name));
                //  添加头信息，指定文件大小，让浏览器能够显示下载进度  
                System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
                //  指定返回的是一个不能被客户端读取的流，必须被下载  
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                //  把文件流发送到客户端  
                System.Web.HttpContext.Current.Response.WriteFile(file.FullName);
                //  停止页面的执行  
                System.Web.HttpContext.Current.Response.End();
            }
            catch (Exception e)
            {
                System.Web.HttpContext.Current.Response.Write(e.Message);
                System.Web.HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 根据内容和文件路径地址生成文件
        /// </summary>
        /// <param name="strContent">返回的内容</param>
        /// <param name="strFilePath">文件路径</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public string CreateFile(string strContent, string strFilePath, Encoding code)
        {
            //用来写文件
            StreamWriter sw = null;
            //写入文件
            try
            {
                sw = new StreamWriter(strFilePath, false, code);
                sw.Write(strContent);
                sw.Flush();
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            finally
            {
                sw.Close();
            }
            return "";
        }
    }
}
