using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;

namespace YuriUpdater
{
    public class NetUtil
    {
        /// <summary>
        /// 从指定的URL下载文件
        /// </summary>
        /// <remarks>这个方法是同步的，如果需要异步请利用信号分发机制</remarks>
        /// <param name="url">要下载的文件的URL</param>
        /// <param name="saveFileRelativePath">保存文件相对程序根目录的路径</param>
        /// <param name="timeout">请求超时的毫秒数</param>
        /// <param name="bufferSize">缓冲区字节数</param>
        /// <returns>操作是否成功</returns>
        public static bool Download(string url, string saveFileRelativePath, int timeout = 5000, int bufferSize = 2048)
        {
            try
            {
                using (FileStream fs = new FileStream(LocationContext.ParseURItoURL(saveFileRelativePath), FileMode.Create, FileAccess.Write))
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    var response = (HttpWebResponse)request.GetResponse();
                    request.Timeout = timeout;
                    Stream responseStream = response.GetResponseStream();
                    var bufferBytes = new byte[Math.Max(128, bufferSize)];
                    int bytesRead;
                    do
                    {
                        bytesRead = responseStream.Read(bufferBytes, 0, bufferBytes.Length);
                        fs.Write(bufferBytes, 0, bytesRead);
                    } while (bytesRead > 0);
                    fs.Flush();
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 访问指定的URL并下载页面中的内容为字符串
        /// </summary>
        /// <param name="url">要访问的URL</param>
        /// <param name="result">[out] 获得的字符串</param>
        /// <returns>操作是否成功</returns>
        public static bool FetchString(string url, out string result)
        {
            try
            {
                var wb = new WebClient();
                result = wb.DownloadString(url);
                return true;
            }
            catch (Exception ex)
            {
                result = null;
                return false;
            }
        }
    }
}
