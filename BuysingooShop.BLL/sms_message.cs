using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 手机短信
    /// </summary>
    public partial class sms_message
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        public sms_message()
        { }

        /// <summary>
        /// 检查账户信息是否正确
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            if (string.IsNullOrEmpty(siteConfig.smsapiurl) || string.IsNullOrEmpty(siteConfig.smsusername) || string.IsNullOrEmpty(siteConfig.smspassword))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="mobiles">手机号码，以英文“,”逗号分隔开</param>
        /// <param name="content">短信内容</param>
        /// <param name="pass">短信通道1验证码通道2广告通道</param>
        /// <param name="msg">返回提示信息</param>
        /// <returns>bool</returns>
        public bool Send(string mobiles, string content, int pass, out string msg)
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                msg = "短信配置参数有误，请完善后再提交！";
                return false;
            }
            //检查手机号码，如果超过2000则分批发送
            int sucCount = 0; //成功提交数量
            string errorMsg = string.Empty; //错误消息
            string[] oldMobileArr = mobiles.Split(',');
            int batch = oldMobileArr.Length / 2000 + 1; //2000条为一批，求出分多少批

            for (int i = 0; i < batch; i++)
            {
                StringBuilder sb = new StringBuilder();
                int sendCount = 0; //发送数量
                int maxLenght = (i + 1) * 2000; //循环最大的数

                //检测号码，忽略不合格的，重新组合
                for (int j = 0; j < oldMobileArr.Length && j < maxLenght; j++)
                {
                    int arrNum = j + (i * 2000);
                    string pattern = @"^1\d{10}$";
                    string mobile = oldMobileArr[arrNum].Trim();
                    Regex r = new Regex(pattern, RegexOptions.IgnoreCase); //正则表达式实例，不区分大小写
                    Match m = r.Match(mobile); //搜索匹配项
                    if (m != null)
                    {
                        sendCount++;
                        sb.Append(mobile + ",");
                    }
                }

                ////发送短信
                //if (sb.ToString().Length > 0)
                //{
                //    try
                //    {
                //        string result = Vincent._DTcms.Utils.HttpPost(siteConfig.smsapiurl,
                //            "cmd=tx&pass=" + pass + "&uid=" + siteConfig.smsusername + "&pwd=" + siteConfig.smspassword + "&mobile=" + Vincent._DTcms.Utils.DelLastComma(sb.ToString()) + "&encode=utf8&content=" + Vincent._DTcms.Utils.UrlEncode(content));
                //        string[] strArr = result.Split(new string[] { "||" }, StringSplitOptions.None);
                //        if (strArr[0] != "100")
                //        {
                //            errorMsg = "提交失败，错误提示：" + strArr[1];
                //            continue;
                //        }
                //        sucCount += sendCount; //成功数量
                //    }
                //    catch
                //    {
                //        //没有动作
                //    }
                //}


                //发送短信
                var message_name = _Utility.GetConfigAppSetting("message_name");
                var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
                foreach (var item in oldMobileArr)
                {
                    //_Message client = _Message.getInstance(message_name, message_pwd);
                    content = "【简单生活】 " + content;

                    var MessageNum = _MobileMessage.SendMessage(content, mobiles);
                    //string resultMT = client.MT(content, item, "", "");
                    /*
                    >0	成功,系统生成的任务编号,long类型
                     0	失败
                    -1	用户名或者密码不正确
                    -2	必填选项为空
                    -3	短信内容0个字节
                    -4	0个有效号码
                    -5	余额不够
                    -6	含有一级敏感词
                    -7	含有二级敏感词，人工审核
                    -8	提交频率太快，退避重发
                    -9	数据格式错误
                    -10	用户被禁用
                    -11	短信内容过长
                     * */

                    if (_Convert.ToInt( MessageNum,0) > 0)
                    {
                        sucCount += sendCount; //成功数量
                    }


                }
            }

            //返回状态
            if (sucCount > 0)
            {
                msg = "成功提交" + sucCount + "条，失败" + (oldMobileArr.Length - sucCount) + "条";
                return true;
            }
            msg = errorMsg;
            return false;
        }

        /// <summary>
        /// 查询账户剩余短信数量
        /// </summary>
        public int GetAccountQuantity(out string code)
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                code = "115";
                return 0;
            }
            try
            {
                string result = Vincent._DTcms.Utils.HttpPost(siteConfig.smsapiurl, "cmd=mm&uid=" + siteConfig.smsusername + "&pwd=" + siteConfig.smspassword);
                string[] strArr = result.Split(new string[] { "||" }, StringSplitOptions.None);
                if (strArr[0] != "100")
                {
                    code = strArr[0];
                    return 0;
                }
                code = strArr[0];
                return Vincent._DTcms.Utils.StrToInt(strArr[1], 0);
            }
            catch
            {
                code = "115";
                return 0;
            }
        }

        /// <summary>
        /// 查询已发送数量
        /// </summary>
        public int GetSendQuantity(out string code)
        {
            //检查是否设置好短信账号
            if (!Exists())
            {
                code = "115";
                return 0;
            }
            try
            {
                string result = Vincent._DTcms.Utils.HttpPost(siteConfig.smsapiurl, "cmd=se&uid=" + siteConfig.smsusername + "&pwd=" + siteConfig.smspassword);
                string[] strArr = result.Split(new string[] { "||" }, StringSplitOptions.None);
                if (strArr[0] != "100")
                {
                    code = strArr[0];
                    return 0;
                }
                code = strArr[0];
                return Vincent._DTcms.Utils.StrToInt(strArr[1], 0);
            }
            catch
            {
                code = "115";
                return 0;
            }
        }

    }
}
