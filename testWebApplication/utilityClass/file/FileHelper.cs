using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using utilityClass.cmd;

namespace utilityClass.file
{
    public class FileHelper
    {
        #region 命令行文件帮助类
        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="oldFile">原文件地址</param>
        /// <param name="newFilePath">新文件路径/新文件名称</param>
        /// <param name="overwrite">是否覆盖</param>
        public static void fileCopyCmd(string oldFile, string newFilePath, bool overwrite)
        {
            if (string.IsNullOrEmpty(oldFile) || string.IsNullOrEmpty(newFilePath))
            {
                return;
            }
            oldFile = oldFile.Replace("/", "\\").Replace("\\\\", "\\");
            newFilePath = newFilePath.Replace("/", "\\").Replace("\\\\", "\\");
            string route = newFilePath.Substring(0, newFilePath.LastIndexOf("\\"));
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            string sCmd = @"copy " + (overwrite ? "/y " : "") + oldFile + " " + newFilePath;
            ExcuteCmd.Execute(sCmd);
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="oldFile">原文件地址</param>
        /// <param name="newFilePath">新文件路径/新文件名称</param>
        public static void fileCopyCmd(string oldFile, string newFilePath)
        {
            fileCopyCmd(oldFile, newFilePath, false);
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="oldFile">原文件地址</param>
        /// <param name="newFile">新文件名称</param>
        public static void fileReNameCmd(string oldFile, string newFile)
        {
            if (string.IsNullOrEmpty(oldFile) || string.IsNullOrEmpty(newFile))
            {
                return;
            }
            oldFile = oldFile.Replace("/", "\\");
            newFile = newFile.Replace("/", "\\");
            string sCmd = @"ren /y " + oldFile + " " + newFile;
            ExcuteCmd.Execute(sCmd);
        }
        #endregion
    }
}