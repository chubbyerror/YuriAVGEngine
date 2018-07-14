using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YuriUpdater
{
    class UpdateController
    {
        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns>是否需要更新</returns>
        private bool CheckUpdate()
        {
            try
            {
                FileStream fs = new FileStream(LocationContext.ParseURItoURL(LocationContext.VersionFilePath), FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                if (fs == null)
                {
                    return true;
                }
                List<string> lineitems = new List<string>();
                while (sr.EndOfStream != true)
                {
                    lineitems.Add(sr.ReadLine());
                }
                sr.Close();
                fs.Close();
                // assert only version text in first line
                string version = lineitems[0];
                NetUtil.FetchString(LocationContext.VersionFetchingURL, out string lastestVersion);
                return false;
            }
            catch
            {
                return true;
            }
        }
    }
}
