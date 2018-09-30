using BuysingooShop.Web.AdminService;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using Vincent;
using Vincent._DTcms;


namespace BuysingooShop.Web.Service
{
    /// <summary>
    /// SubmitAjax 的摘要说明
    /// </summary>
    public class SubmitAjax : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
       
        public AdminServiceSoapClient SoapClient = new AdminServiceSoapClient();

        public void ProcessRequest(HttpContext context)
        {
            string param = _Request.GetString("param", "");
            switch (param)
            {
                case "regist":
                    regist(context);        //注册
                    break;
                case "login":
                    login(context);        //登录
                    break;
                case "GetMobileMsgcode":
                    GetMobileMsgcode(context);        //获取手机短信验证码
                    break;
                case "message":
                    message(context);        //添加留言
                    break;
                case "contact":
                    contact(context);        //获取钻石会员套餐
                    break;
                case "GetUserInfo":
                    GetUserInfo(context);        //获取用户信息
                    break;


            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public void GetUserInfo(HttpContext context)
        {
            string outmsg = "";
            int user_id = _Request.GetInt("user_id");

            BLL.users bll = new BLL.users();
            Model.users user = bll.GetModel(user_id);

            if (user.user_name.Length != 11)
            {
                outmsg = null;

                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
            }
            else
            {
                outmsg = ObjectToJSON(user);
                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
            }


        }
        /// 注册
        /// </summary>
        public void regist(HttpContext context)
        {
            string outmsg = "{\"status\":1,\"msg\":\"注册成功！\"}";
            string mobile = _Request.GetString("mobile");
            string usersname = _Request.GetString("usersname");
            string password = _Request.GetString("password");
            string usersaera = _Request.GetString("usersaera");
            string usernick = _Request.GetString("usernick");//y用户昵称



            string userPwd1 = Vincent._MD5Encrypt.GetMD5(password.Trim());
            Model.users model = new Model.users();
            BLL.users bll = new BLL.users();
            model.user_name = mobile;
            model.mobile = mobile;
            model.isEmail = 0;
            model.isMobile = 0;
            model.Parentid = 0;
            model.Leftor_right = 0;
            model.MarketId = 0;
            model.OrganizeId = 0;
            model.PreId = 0;
            model.nick_name = usernick;//用户昵称
            //model.password = Vincent._DESEncrypt.Encrypt(password.Trim());
            //model.password = Vincent._DESEncrypt.Encrypt(password.Trim());
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            if (!bll.ExistsMobile(mobile))
            {
                int id = bll.AddPc(model);
               if (id>0)
               {
                   DataTable dt = bll.GetList(0, "id  =" + id + " ", "id asc").Tables[0];
                   if (dt != null && dt.Rows.Count > 0)
                   {
                       outmsg = CreateJsonParameters(dt);
                       
                   }
                //// 保存session值    
                context.Session[DTKeys.SESSION_USER_INFO] = model;
                context.Session.Timeout = 45;
                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
               }
               else
               {
                   context.Response.Clear();
                   context.Response.Write("{\"status\":0,\"msg\":\"注册未成功！\"}");
                   context.Response.End();
               }
            }
            else
            {
                context.Response.Clear();
                context.Response.Write("{\"status\":0,\"msg\":\"注册未成功！\"}");
                context.Response.End();
            } 
           
          

            
        }
        /// <summary>
        /// 获取手机短信验证码
        /// </summary>
        public void GetMobileMsgcode(HttpContext context)
        {
            string mobile = _Request.GetString("mobile", "");
           

           
            BLL.users bll = new BLL.users();
            string outresult = "";
            if (string.IsNullOrEmpty(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号不能为空\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }
            if (bll.ExistsMobile(mobile))
            {
                outresult = "{\"status\":\"n\",\"info\":\"手机号已被注册\"}";
                context.Response.Clear();
                context.Response.Write(outresult);
                context.Response.End();
            }
      
            Random rd = new Random();
            int msgcode = rd.Next(100000, 999999);

            var messageNum = Vincent._MobileMessage.SendMessageCode(msgcode.ToString(), mobile);

            if (messageNum >= 0)
            {
                //写Session，设置验证码有效期，比如10分钟
                _Session.SetSession(DTKeys.SESSION_CODE, msgcode.ToString());
                _Cookie.SetCookie(DTKeys.SESSION_SMS_CODE, msgcode.ToString(), 600);

                outresult = "{\"status\":\"y\",\"info\":" + msgcode + "}";
            }
            else
            {
                outresult = "{\"status\":\"n\",\"info\":\"短信发送失败\"}";
            }

            context.Response.Clear();
            context.Response.Write(outresult);
            context.Response.End();
        }

      
        /// 登录
        /// </summary>
        public void login(HttpContext context)
        {
            string outmsg = "{\"status\":1,\"msg\":\"会员登录成功！\"}";
            string mobile = _Request.GetString("mobile");
            string password = _Request.GetString("password");
            string imgCode = _Request.GetString("imgCode");
            BLL.users bll = new BLL.users();

            string salt = bll.GetSalt(mobile);
            //把明文进行加密重新赋值
            password = _DESEncrypt.Encrypt(password, salt);
            Model.users model = bll.GetModel(mobile, password.Trim());
         
            if (model == null)
            {
                context.Response.Clear();
                context.Response.Write("{\"status\":0,\"msg\":\"错误提示：用户名或密码错误，请重试！\"}");
                context.Response.End();
            }
            //检查用户输入信息是否为空
            if (imgCode == "")
            {
                context.Response.Write("{\"status\":0, \"msg\":\"请填写验证码！\"}");
                return;
            }
           
            if (imgCode.ToLower() != (Vincent._Cookie.GetCookie(DTKeys.SESSION_CODE).ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"验证码错误，请重新填写！\"}");
                return;
            }
            //// 保存session值    
            context.Session[DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            string userid = Convert.ToString(model.id);
          
            string json = ObjectToJSON(model);
            if (!string.IsNullOrEmpty(userid))
            {
                outmsg = "{\"status\":2,\"msg\":" + userid + "}";
                _Log.SaveMessage("登录成功：" + mobile + "/" + password + "/" + mobile);
            }
            else
            {
                _Log.SaveMessage("登录失败：" + mobile + "/" + password + "/" + mobile);
            }

            //注：如果以上都处理成功，返回json，处理失败，返回""
            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();

        }

        /// 添加留言
        /// </summary>
        public void message(HttpContext context)
        {
            string outmsg = "{\"status\":1,\"msg\":\"留言成功！\"}";
            string messagename = _Request.GetString("messagename");
            string messagmodile = _Request.GetString("messagmodile");
            string messageemail = _Request.GetString("messageemail");
            string messageliu = _Request.GetString("messageliu");
            Model.article_comment model = new Model.article_comment();
            BLL.article_comment bll = new BLL.article_comment();
            model.user_name = messagename;
            model.channel_id = 1;
            model.user_ip = messagmodile;
            model.content = messageemail;
            model.reply_content = messageliu;
            model.add_time = DateTime.Now;
           int id=  bll.Add(model);
           if (id>0)
           {
                context.Response.Clear();
                context.Response.Write(outmsg);
                context.Response.End();
           }
           else
           {
               context.Response.Clear();
               context.Response.Write("{\"status\":0,\"msg\":\"留言失败请稍候再试！\"}");
               context.Response.End();
           }
           
        }

        /// 获取主题内容
        /// </summary>
        public void contact(HttpContext context)
        {
            string outmsg = "{\"status\":0,\"msg\":\"没有分类！\"}";

            int id = _Request.GetInt("id");
            BLL.article bll = new BLL.article();
            DataTable dt = bll.GetListqian(0, "category_id  =" + id + " ", "sort_id asc,id asc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                outmsg = CreateJsonParameters(dt);
            }

            context.Response.Clear();
            context.Response.Write(outmsg);
            context.Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region Model与JSON相互转化========================================

        /// <summary>
        /// 将dataTable转换成json字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string CreateJsonParameters(DataTable dt)
        {
            StringBuilder JsonString = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("{");
                JsonString.Append("\"T_blog\":[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    /**/
                    /**/
                    /**/
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]}");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        // using System.Runtime.Serialization.Json;
        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        /// <summary>
        /// Model与JSON相互转化
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string ObjectToJSON(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                if (jsonObject == null)
                {
                    return null;
                }
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        #endregion
    }
}