using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BuysingooShop.Api.Common.CommonHelpers
{
    public class HttpHelper
    {
        /// <summary>
        /// Http请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="Method">Get/Post</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static string Request(string url, Encoding encoding = null, string Method = "GET", string parameters = null)
        {
            try
            {
                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (encoding == null)
                {
                    encoding = Encoding.GetEncoding("UTF-8");
                }
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
                Request.Method = Method;
                Request.Timeout = 10000;
                Request.ContentType = "application/x-www-form-urlencoded";
                Request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.104 Safari/537.36 Core/1.53.4620.400 QQBrowser/9.7.13014.400";
                //Request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident / 7.0; rv: 11.0) like Gecko";
                Request.Proxy = null;
                if (Method.ToUpper() == "POST" && parameters != null)
                {
                    //StringBuilder buffer = new StringBuilder();
                    //int i = 0;
                    //foreach (string key in parameters.Keys)
                    //{
                    //    if (i > 0)
                    //    {
                    //        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    //    }
                    //    else
                    //    {
                    //        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    //    }
                    //    i++;
                    //}
                    byte[] data = encoding.GetBytes(parameters);
                    using (Stream stream = Request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream resStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(resStream, encoding);
                    string content = streamReader.ReadToEnd();
                    streamReader.Close();
                    resStream.Close();
                    return content;
                }
                else
                {

                    throw new Exception($"请求失败!,url:{url}\r\nres:{JsonHelper.ObjToJsonString(response)}");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"请求失败!,url:{url}\r\nres:{e.Message}");
            }
        }


        #region 网络上的Http请求
        /// <summary>
        /// 异步请求post（键值对形式,可等待的）
        /// </summary>
        /// <param name="uri">网络基址("http://localhost:59315")</param>
        /// <param name="url">网络的地址("/api/UMeng")</param>
        /// <param name="formData">键值对List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();formData.Add(new KeyValuePair<string, string>("userid", "29122"));formData.Add(new KeyValuePair<string, string>("umengids", "29122"));</param>
        /// <param name="charset">编码格式</param>
        /// <param name="mediaType">头媒体类型</param>
        /// <returns></returns>
        public async Task<string> HttpPostAsync(string uri, string url, List<KeyValuePair<string, string>> formData = null,
            string charset = "UTF-8", string mediaType = "application/x-www-form-urlencoded")
        {

            string tokenUri = url;
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            HttpContent content = new FormUrlEncodedContent(formData);
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            content.Headers.ContentType.CharSet = charset;
            for (int i = 0; i < formData.Count; i++)
            {
                content.Headers.Add(formData[i].Key, formData[i].Value);
            }

            HttpResponseMessage resp = await client.PostAsync(tokenUri, content);
            resp.EnsureSuccessStatusCode();
            string token = await resp.Content.ReadAsStringAsync();
            return token;
        }

        /// <summary>
        /// 同步请求post（键值对形式）
        /// </summary>
        /// <param name="uri">网络基址("http://localhost:59315")</param>
        /// <param name="url">网络的地址("/api/UMeng")</param>
        /// <param name="formData">键值对List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();formData.Add(new KeyValuePair<string, string>("userid", "29122"));formData.Add(new KeyValuePair<string, string>("umengids", "29122"));</param>
        /// <param name="charset">编码格式</param>
        /// <param name="mediaType">头媒体类型</param>
        /// <returns></returns>
        public string HttpPost(string uri, string url, List<KeyValuePair<string, string>> formData = null,
            string charset = "UTF-8", string mediaType = "application/x-www-form-urlencoded")
        {
            string tokenUri = url;
            var client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            HttpContent content = new FormUrlEncodedContent(formData);
            content.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
            content.Headers.ContentType.CharSet = charset;
            for (int i = 0; i < formData.Count; i++)
            {
                content.Headers.Add(formData[i].Key, formData[i].Value);
            }

            var res = client.PostAsync(tokenUri, content);
            res.Wait();
            HttpResponseMessage resp = res.Result;

            var res2 = resp.Content.ReadAsStringAsync();
            res2.Wait();

            string token = res2.Result;
            return token;
        }

        /// <summary>
        /// 接下来是通过流的方式进行POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Post(string url, string data, Encoding encoding, int type)
        {
            try
            {
                HttpWebRequest req = WebRequest.CreateHttp(new Uri(url));
                if (type == 1)
                {
                    req.ContentType = "application/json;charset=utf-8";
                }
                else if (type == 2)
                {
                    req.ContentType = "application/xml;charset=utf-8";
                }
                else
                {
                    req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                }

                req.Method = "POST";
                //req.Accept = "text/xml,text/javascript";
                req.ContinueTimeout = 60000;

                byte[] postData = encoding.GetBytes(data);
                Stream reqStream = req.GetRequestStreamAsync().Result;
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Dispose();

                var rsp = (HttpWebResponse)req.GetResponseAsync().Result;
                var result = GetResponseAsString(rsp, encoding);
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                return reader.ReadToEnd();
            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Dispose();
                if (stream != null) stream.Dispose();
                if (rsp != null) rsp.Dispose();
            }
        }

        /// <summary>
        /// 异步请求get(UTF-8)
        /// </summary>
        /// <param name="url">链接地址</param>    
        /// <param name="formData">写在header中的内容</param>
        /// <returns></returns>
        public async Task<string> HttpGetAsync(string url, List<KeyValuePair<string, string>> formData = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(formData);
            if (formData != null)
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                content.Headers.ContentType.CharSet = "UTF-8";
                for (int i = 0; i < formData.Count; i++)
                {
                    content.Headers.Add(formData[i].Key, formData[i].Value);
                }
            }
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            for (int i = 0; i < formData.Count; i++)
            {
                request.Headers.Add(formData[i].Key, formData[i].Value);
            }
            var resp = await httpClient.SendAsync(request);
            resp.EnsureSuccessStatusCode();
            string token = await resp.Content.ReadAsStringAsync();

            return token;
        }

        /// <summary>
        /// 同步get请求
        /// </summary>
        /// <param name="url">链接地址</param>    
        /// <param name="formData">写在header中的键值对</param>
        /// <returns></returns>
        public string HttpGet(string url, List<KeyValuePair<string, string>> formData = null)
        {
            HttpClient httpClient = new HttpClient();
            HttpContent content = new FormUrlEncodedContent(formData);
            if (formData != null)
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                content.Headers.ContentType.CharSet = "UTF-8";
                for (int i = 0; i < formData.Count; i++)
                {
                    content.Headers.Add(formData[i].Key, formData[i].Value);
                }
            }
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get,
            };
            for (int i = 0; i < formData.Count; i++)
            {
                request.Headers.Add(formData[i].Key, formData[i].Value);
            }
            var res = httpClient.SendAsync(request);
            res.Wait();
            var resp = res.Result;
            Task<string> temp = resp.Content.ReadAsStringAsync();
            temp.Wait();
            return temp.Result;
        }
        #endregion
    }
}