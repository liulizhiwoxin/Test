using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BuysingooShop.Api.Common.CommonTools
{
    public class AppConfig
    {
        private static string FilePath { get; set; }
        public static JsonData Configs = null;

        /// <summary>
        /// 配置初始化
        /// </summary>
        /// <param name="filePath"></param>
        public static void Init(string filePath)
        {
            try
            {
                FilePath = filePath;
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    try
                    {
                        Configs = JsonMapper.ToObject(sr);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static void Update()
        {
            Init(FilePath);
        }
    }
}