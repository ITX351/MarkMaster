using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;

namespace MarkMaster.Utils
{
    public class Tools
    {
        public static string GetAbsolutePath(string relativePath)
        {
            // 获取工程目录的基目录
            //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var baseDirectory = Directory.GetCurrentDirectory();
            var absolutePath = Path.Combine(baseDirectory, relativePath);

            // 确保目录存在
            var directoryPath = Path.GetDirectoryName(absolutePath);
            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return absolutePath;
        }
    }
}