using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace BuysingooShop.Api.Common.CommonHelpers
{
    public class JsonHelper
    {
        /// <summary>
        /// 字符串转换为对象（UTF-8）
        /// </summary>
        /// <typeparam name="T">当前的泛型</typeparam>
        /// <param name="strJson">数组字符串</param>
        /// <returns></returns>
        public static T ParseFormJson<T>(string strJson) where T : class
        {
            //创建指定泛型类型参数指定的类型的实例
            //使用无参数的构造函数
            //T obj = Activator.CreateInstance<T>();
            using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(strJson)))
            {
                DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(typeof(T));
                return (T)JsonSerializer.ReadObject(memoryStream);
            }
        }

        /// <summary>
        /// 对象转换为 JSON
        /// </summary>
        /// <typeparam name="ObjType">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string ObjToJsonString<ObjType>(ObjType obj) where ObjType : class
        {
            try
            {
                return JsonMapper.ToJson(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// JSON 转换为对象
        /// </summary>
        /// <typeparam name="ObjType">对象类型</typeparam>
        /// <param name="StrJson">JSON格式的字符串</param>
        /// <returns></returns>
        public static ObjType JsonStringToObj<ObjType>(string StrJson) where ObjType : class
        {
            try
            {
                return JsonMapper.ToObject<ObjType>(StrJson); ;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}