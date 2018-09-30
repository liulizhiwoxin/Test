using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using LitJson;
using Vincent;
using BuysingooShop.Web.UI;
using BuysingooShop.BLL;

namespace BuysingooShop.Web.tools
{
    /// <summary>
    /// register_ajax 的摘要说明
    /// </summary>
    public class register_ajax : IHttpHandler, IRequiresSessionState
    {
        Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
        Model.userconfig userConfig = new BLL.userconfig().loadConfig();
        public void ProcessRequest(HttpContext context)
        {
            string action = _Request.GetString("action", "");
            switch (action)
            {
                case "validate_username": //验证用户名
                    validate_username(context);
                    break;
                case "validate_usermobile": //验证手机号
                    validate_usermobile(context);
                    break;
                case "validate_code": //图片验证码
                    validate_code(context);
                    break;
                case "user_register": //用户注册
                    user_register(context);
                    break;
                case "user_invite_code": //申请邀请码
                    user_invite_code(context);
                    break;
                case "user_get_smscode"://获取验证码
                    user_get_smscode(context);
                    break;
                case "user_code": //判断验证码
                    user_code(context);
                    break;
                case "user_register_smscode": //发送注册短信
                    user_register_smscode(context);
                    break;
                case "user_changepassword"://发送短信修改密码
                    user_changepassword(context);
                    break;
                case "user_verify_email": //发送注册验证邮件
                    user_verify_email(context);
                    break;
                case "weixin_user_login": //微信用户登录
                    weixin_user_login(context);
                    break;
                case "user_login": //用户登录
                    user_login(context);
                    break;
                case "user_login_out": //安全退出
                    user_login_out(context);
                    break;
                case "user_check_login": //检查用户是否登录
                    user_check_login(context);
                    break;
                case "user_oauth_bind": //绑定第三方登录账户
                    user_oauth_bind(context);
                    break;
                case "user_oauth_register": //注册第三方登录账户
                    user_oauth_register(context);
                    break;
                case "user_username": //检测用户名是否存在
                    user_username(context);
                    break;
                case "user_mobile"://检测手机号是否存在
                    user_mobile(context);
                    break;
                case "user_salt"://检测推荐码是否存在
                    user_salt(context);
                    break;
            }
        }

        #region 验证用户名是否可用OK===========================
        private void validate_username(HttpContext context)
        {
            string username = Vincent._DTcms.DTRequest.GetString("param");
            //如果为Null，退出
            if (string.IsNullOrEmpty(username))
            {
                context.Response.Write("{ \"info\":\"用户名不可为空\", \"status\":\"n\" }");
                return;
            }
            //过滤注册用户名字符
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == username.ToLower())
                {
                    context.Response.Write("{ \"info\":\"该用户名不可用\", \"status\":\"n\" }");
                    return;
                }
            }
            BLL.users bll = new BLL.users();
            //查询数据库
            if (!bll.Exists(username.Trim()))
            {
                context.Response.Write("{ \"info\":\"该用户名可用\", \"status\":\"y\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该用户名已被注册\", \"status\":\"n\" }");
            return;
        }
        #endregion

        #region 验证手机号=====================================
        private void validate_usermobile(HttpContext context)
        {
            string usermobile = Vincent._DTcms.DTRequest.GetString("param");
            //如果为Null，退出
            if (string.IsNullOrEmpty(usermobile))
            {
                context.Response.Write("{ \"info\":\"手机号不可为空\", \"status\":\"n\" }");
                return;
            }
            BLL.users bll = new BLL.users();
            //查询数据库
            if (!bll.Exists(usermobile.Trim()))
            {
                context.Response.Write("{ \"info\":\"该手机号可用\", \"status\":\"y\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该手机号已被注册\", \"status\":\"n\" }");
            return;
        }
        #endregion

        #region 图片验证码=====================================
        private void validate_code(HttpContext context)
        {
            string strcode = Vincent._DTcms.DTRequest.GetString("param");
            //如果为Null，退出
            if (string.IsNullOrEmpty(strcode))
            {
                context.Response.Write("{ \"info\":\"验证码不可为空\", \"status\":\"n\" }");
                return;
            }

            if (context.Session[Vincent._DTcms.DTKeys.SESSION_CODE].ToString().ToUpper() == strcode.ToUpper())
            {
                context.Response.Write("{ \"info\":\"该验证码可用\", \"status\":\"y\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"验证码不正确\", \"status\":\"n\" }");
            return;
        }
        #endregion

        #region 用户注册OK=====================================
        private void user_register(HttpContext context)
        {
            string code = Vincent._DTcms.DTRequest.GetFormString("txtCode").Trim();
            string salt = Vincent._DTcms.DTRequest.GetFormString("txtSalt").Trim();
            string username = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtName").Trim());
            string password = Vincent._DTcms.DTRequest.GetFormString("txtPwd").Trim();
            //string mobile = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtMobile").Trim());
            string userip = Vincent._DTcms.DTRequest.GetIP();
            string saltstring = "";
            #region 检查各项并提示

            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            //if (bll.ExistsMobile(username))
            //{
            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.Write("{\"status\":0, \"msg\":\"该手机号已经注册！\"}");
            //    HttpContext.Current.Response.End();
            //    return;
            //}
            if (salt != "")
            {
                saltstring = bll.GetSalt(salt);
            }
            if (bll.Exists(username.Trim()))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户名已被注册！\"}");
                return;
            }
            //检查是否开启会员功能
            if (siteConfig.memberstatus == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，会员功能已关闭，无法注册！\"}");
                return;
            }
            if (userConfig.regstatus == 0)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，系统暂不允许注册新用户！\"}");
                return;
            }
            if (code.ToLower() != (_Cookie.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE).ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你的手机验证码不正确！\"}");
                return;
            }
            #endregion

            //保存注册信息
            model.group_id = 1;//普通用户注册
            model.user_name = username;
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.mobile = username;
            model.reg_ip = userip;
            model.parentSalt = saltstring;
            model.reg_time = DateTime.Now;
            model.isMobile = 1;
            model.strcode = Vincent._DTcms.Utils.GetCheckCode(20);//生成随机码
            //设置对应的状态
            switch (userConfig.regverify)
            {
                case 0:
                    model.status = 0; //正常
                    break;
                case 3:
                    model.status = 2; //人工审核
                    break;
                default:
                    model.status = 1; //待验证
                    break;
            }
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"系统故障，请联系网站管理员！\"}");
                return;
            }
            model = bll.GetModel(newId);

            if (model != null)
            {
                context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
                context.Session.Timeout = 45;

                //防止Session提前过期
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);

                //写入登录日志
                new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
                context.Response.Write("{\"status\":1, \"msg\":\"注册成功，欢迎成为本站会员！\"}");
            }
            else
            {
                context.Response.Write("{\"status\":0, \"msg\":\"注册失败！\"}");
            }


        }
        #endregion

        //判断验证码是否正确
        private void user_code(HttpContext context)
        {
            string code = Vincent._DTcms.DTRequest.GetFormString("txtCode").Trim();

            if (code.ToLower() != (_Cookie.GetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE).ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，你的手机验证码不正确！\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"验证码正确！\"}");
        }

        #region 邀请注册处理方法OK=============================
        private string verify_invite_reg(string user_name, string invite_code)
        {
            if (string.IsNullOrEmpty(invite_code))
            {
                return "{\"status\":0, \"msg\":\"邀请码不能为空！\"}";
            }
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel = codeBll.GetModel(invite_code);
            if (codeModel == null)
            {
                return "{\"status\":0, \"msg\":\"邀请码不正确或已过期！\"}";
            }
            if (userConfig.invitecodecount > 0)
            {
                if (codeModel.count >= userConfig.invitecodecount)
                {
                    codeModel.status = 1;
                    return "{\"status\":0, \"msg\":\"该邀请码已经被使用！\"}";
                }
            }
            //检查是否给邀请人增加积分
            if (userConfig.pointinvitenum > 0)
            {
                new BLL.user_point_log().Add(codeModel.user_id, codeModel.user_name, userConfig.pointinvitenum, "邀请用户【" + user_name + "】注册获得积分", true);
            }
            //更改邀请码状态
            codeModel.count += 1;
            codeBll.Update(codeModel);
            return "success";
        }
        #endregion

        #region Email验证发送邮件OK============================
        private string verify_email(Model.users userModel)
        {

            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            //检查是否重复提交
            codeModel = codeBll.GetModel(userModel.user_name, Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString(), "d");
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                codeModel.user_id = userModel.id;
                codeModel.user_name = userModel.user_name;
                codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString();
                codeModel.str_code = userModel.strcode;
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.regemailexpired);
                codeModel.add_time = DateTime.Now;
                new BLL.user_code().Add(codeModel);
            }
            //获得邮件内容
            Model.mail_template mailModel = new BLL.mail_template().GetModel("regverify");
            if (mailModel == null)
            {
                return "{\"status\":0, \"msg\":\"邮件发送失败，邮件模板内容不存在！\"}";
            }
            //替换模板内容
            string titletxt = mailModel.maill_title;
            string bodytxt = mailModel.content;
            titletxt = titletxt.Replace("{webname}", siteConfig.webname);
            titletxt = titletxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
            bodytxt = bodytxt.Replace("{webtel}", siteConfig.webtel);
            bodytxt = bodytxt.Replace("{weburl}", siteConfig.weburl);
            bodytxt = bodytxt.Replace("{username}", userModel.user_name);
            bodytxt = bodytxt.Replace("{valid}", userConfig.regemailexpired.ToString());
            bodytxt = bodytxt.Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower()
                + "/checkemail.aspx?action=checkmail&userid=" + userModel.id + "&strcode=" + userModel.strcode);
            //发送邮件
            try
            {
                _Email.SendMail(siteConfig.emailsmtp,
                    siteConfig.emailusername,
                    _DESEncrypt.Decrypt(siteConfig.emailpassword),
                    siteConfig.emailnickname,
                    siteConfig.emailfrom,
                    userModel.email,
                    titletxt, bodytxt);
            }
            catch (Exception)
            {
                //return "{\"status\":0, \"msg\":\""+ex.Message+"\"}";
                return "{\"status\":0, \"msg\":\"邮件发送失败，请联系本站管理员！\"}";
            }
            return "success";
        }
        #endregion

        #region 手机验证发送短信OK=============================
        private string verify_mobile(Model.users userModel)
        {
            //生成随机码
            string strcode = Vincent._DTcms.Utils.Number(4);
            BLL.user_code codeBll = new BLL.user_code();
            Model.user_code codeModel;
            //检查是否重复提交
            codeModel = codeBll.GetModel(userModel.user_name, Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString(), "n");
            if (codeModel == null)
            {
                codeModel = new Model.user_code();
                codeModel.user_id = userModel.id;
                codeModel.user_name = userModel.user_name;
                codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.RegVerify.ToString();
                codeModel.str_code = strcode;
                codeModel.eff_time = DateTime.Now.AddMinutes(userConfig.regsmsexpired);
                codeModel.add_time = DateTime.Now;
                new BLL.user_code().Add(codeModel);
            }
            //获得短信模板内容
            Model.sms_template smsModel = new BLL.sms_template().GetModel("usercode");
            if (smsModel == null)
            {
                return "{\"status\":0, \"msg\":\"发送失败，短信模板内容不存在！\"}";
            }
            //替换模板内容
            string msgContent = smsModel.content;
            msgContent = msgContent.Replace("{webname}", siteConfig.webname);
            msgContent = msgContent.Replace("{webtel}", siteConfig.webtel);
            msgContent = msgContent.Replace("{weburl}", siteConfig.weburl);
            msgContent = msgContent.Replace("{username}", userModel.user_name);
            msgContent = msgContent.Replace("{code}", codeModel.str_code);
            msgContent = msgContent.Replace("{valid}", userConfig.regsmsexpired.ToString());
            //发送短信
            string tipMsg = string.Empty;
            bool sendStatus = new BLL.sms_message().Send(userModel.mobile, msgContent, 2, out tipMsg);
            if (!sendStatus)
            {
                return "{\"status\": 0, \"msg\": \"短信发送失败，" + tipMsg + "\"}";
            }
            return "success";
        }
        #endregion

        #region 申请邀请码OK===================================
        private void user_invite_code(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，用户尚未登录或已超时！\"}");
                return;
            }
            //检查是否开启邀请注册
            if (userConfig.regstatus != 2)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，系统不允许通过邀请注册！\"}");
                return;
            }
            BLL.user_code codeBll = new BLL.user_code();
            //检查申请是否超过限制
            if (userConfig.invitecodenum > 0)
            {
                int result = codeBll.GetCount("user_name='" + model.user_name + "' and type='" + Vincent._DTcms.DTEnums.CodeEnum.Register.ToString() + "' and datediff(d,add_time,getdate())=0");
                if (result >= userConfig.invitecodenum)
                {
                    context.Response.Write("{\"status\":0, \"msg\":\"对不起，您申请邀请码的数量已超过每天限制！\"}");
                    return;
                }
            }
            //删除过期的邀请码
            codeBll.Delete("type='" + Vincent._DTcms.DTEnums.CodeEnum.Register.ToString() + "' and status=1 or datediff(d,eff_time,getdate())>0");
            //随机取得邀请码
            string str_code = Vincent._DTcms.Utils.GetCheckCode(8);
            Model.user_code codeModel = new Model.user_code();
            codeModel.user_id = model.id;
            codeModel.user_name = model.user_name;
            codeModel.type = Vincent._DTcms.DTEnums.CodeEnum.Register.ToString();
            codeModel.str_code = str_code;
            if (userConfig.invitecodeexpired > 0)
            {
                codeModel.eff_time = DateTime.Now.AddDays(userConfig.invitecodeexpired);
            }
            codeBll.Add(codeModel);
            context.Response.Write("{\"status\":1, \"msg\":\"恭喜您，申请邀请码已成功！\"}");
            return;
        }
        #endregion

        #region 发送注册验证码短信OK===========================
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        private void user_register_smscode(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"获取失败，请重新获取！\"}";
            var mobile = _Request.GetString("mobile", "");
            var smscoderand = _Request.GetString("smsCodeRand", "");
            users bll = new users();
            if (bll.ExistsMobile(mobile))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("{\"status\":0, \"msg\":\"该手机号已经注册！\"}");
                HttpContext.Current.Response.End();
                return;
            }
            if (smscoderand.Length != 6) //如果JS生成的随机码不符，则用C#生成随机码
            {
                Random ro = new Random();
                var no = ro.Next(100000, 999999); //随机一个数

                smscoderand = no.ToString();
            }

            //写短信数据，发SMS
            var message_name = _Utility.GetConfigAppSetting("message_name");
            var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
            var message_content = _Utility.GetConfigAppSetting("message_content");
            message_content = message_content.Replace("num", smscoderand);
      
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

            var MessageNum = Vincent._MobileMessage.SendMessageCode(message_content, mobile);
                  
            Model.userconfig userConfig = new BLL.userconfig().loadConfig();
            if (MessageNum > 0)
            {
                //写Session，设置验证码有效期，比如10分钟
                //_Session.SetSession(DTKeys.SESSION_CODE, smscoderand);
                userConfig.regstatus = 2;
                _Cookie.SetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE, smscoderand, 600);
                outmsg = "{\"status\":1, \"msg\":\"获取成功！\"}";
            }
            else
            {
                //记录日志
                //_Log.SaveMessage("手机：" + mobile + "，原因:" + AppCode.Utility.NumToMsg(MessageNum)); 
            }

            //注：如果以上都处理成功，返回"Y"，处理失败，返回"N"
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(outmsg);
            HttpContext.Current.Response.End();
        }
        #endregion

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="context"></param>
        private void user_get_smscode(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"获取失败，请重新获取！\"}";
            var mobile = _Request.GetString("mobile", "");
            HttpContext.Current.Session["mobile"] = mobile;
            var smscoderand = _Request.GetString("smsCodeRand", "");
            if (smscoderand.Length != 6) //如果JS生成的随机码不符，则用C#生成随机码
            {
                Random ro = new Random();
                var no = ro.Next(100000, 999999); //随机一个数

                smscoderand = no.ToString();
            }

            //写短信数据，发SMS
            var message_name = _Utility.GetConfigAppSetting("message_name");
            var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
            var message_content = _Utility.GetConfigAppSetting("message_content");
            message_content = message_content.Replace("num", smscoderand);

            var MessageNum = Vincent._MobileMessage.SendMessageCode(message_content, mobile);
                  
            Model.userconfig userConfig = new BLL.userconfig().loadConfig();
            if (MessageNum > 0)
            {
                //写Session，设置验证码有效期，比如10分钟
                //_Session.SetSession(DTKeys.SESSION_CODE, smscoderand);
                userConfig.regstatus = 2;
                _Cookie.SetCookie(Vincent._DTcms.DTKeys.SESSION_SMS_CODE, smscoderand, 600);
                outmsg = "{\"status\":1, \"msg\":\"获取成功！\"}";
            }
            else
            {
                //记录日志
                //_Log.SaveMessage("手机：" + mobile + "，原因:" + AppCode.Utility.NumToMsg(MessageNum)); 
            }

            //注：如果以上都处理成功，返回"Y"，处理失败，返回"N"
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(outmsg);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 发送短信修改密码
        /// </summary>
        private void user_changepassword(HttpContext context)
        {
            string outmsg = "{\"status\":0, \"msg\":\"获取失败，请重新获取！\"}";
            var mobile = _Request.GetString("mobile", "");
            users bll = new users();
            if (!bll.ExistsMobile(mobile))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write("{\"status\":0, \"msg\":\"手机号不存在！\"}");
                HttpContext.Current.Response.End();
                return;
            }

            //生成密码
            Random ro = new Random();
            var no = ro.Next(100000, 999999); //随机一个数
            string password = no.ToString();

            Model.users model = bll.GetModelMobile(mobile);

            //写短信数据，发SMS
            var message_name = _Utility.GetConfigAppSetting("message_name");
            var message_pwd = _Utility.GetConfigAppSetting("message_pwd");
            var message_content = _Utility.GetConfigAppSetting("message_changepassword");
            message_content = message_content.Replace("num", password);
            message_content = message_content.Replace("username", model.user_name);

          
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

            var MessageNum = Vincent._MobileMessage.SendMessageCode(message_content, mobile);
                  
            Model.userconfig userConfig = new BLL.userconfig().loadConfig();
            if (MessageNum > 0)
            {
                outmsg = "{\"status\":1, \"msg\":\"修改成功,稍后请注意查收您的短信！\"}";
            }
            else
            {
                //记录日志
                //_Log.SaveMessage("手机：" + mobile + "，原因:" + AppCode.Utility.NumToMsg(MessageNum)); 
            }

            //注：如果以上都处理成功，返回"Y"，处理失败，返回"N"
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Write(outmsg);
            HttpContext.Current.Response.End();
        }

        #region 发送注册验证邮件===============================
        private void user_verify_email(HttpContext context)
        {
            string username = Vincent._DTcms.DTRequest.GetFormString("username");
            //检查是否过快
            string cookie = Vincent._DTcms.Utils.GetCookie("user_reg_email");
            if (cookie == username)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"发送邮件间隔为20分钟，您刚才已经提交过啦，休息一下再来吧！\"}");
                return;
            }
            Model.users model = new BLL.users().GetModel(username);
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户不存在或已删除！\"}");
                return;
            }
            if (model.status != 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户无法进行邮箱验证！\"}");
                return;
            }
            string result = verify_email(model);
            if (result != "success")
            {
                context.Response.Write(result);
                return;
            }
            context.Response.Write("{\"status\":1, \"msg\":\"邮件已经发送成功啦！\"}");
            Vincent._DTcms.Utils.WriteCookie("user_reg_email", username, 20); //20分钟内无重复发送
            return;
        }
        #endregion

        #region 用户登录OK=====================================
        private void weixin_user_login(HttpContext context)
        {
            var urlReferrer = HttpContext.Current.Request.UrlReferrer;

            var cart = Vincent._DTcms.DTRequest.GetQueryInt("cart", 0);
            var id = Vincent._DTcms.DTRequest.GetQueryInt("id", 0);
            var state = Vincent._DTcms.DTRequest.GetString("state");

            _Log.SaveMessage(string.Format("UrlReferrer: {0}, Id: {1}, State: {2}", urlReferrer, id, state));

            BuysingooShop.BLL.users bll = new BuysingooShop.BLL.users();
            Model.users model = null;
            try
            {
                model = bll.GetModel(id);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
                return;
            }

            if (model == null)
            {
                context.Response.Write("微信授权登陆失败，请重试！");
                return;
            }

            //检查用户是否通过验证
            if (model.status == 1) //待验证
            {
                context.Response.Write("{\"status\":0, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username="
                    + Vincent._DTcms.Utils.UrlEncode(model.user_name) + "\", \"msg\":\"你的账号未激活,请激活后再使用！\"}");
                return;
            }
            else if (model.status == 2) //待审核
            {
                context.Response.Write("{\"status\":0, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username="
                    + Vincent._DTcms.Utils.UrlEncode(model.user_name) + "\", \"msg\":\"你的账号未审核！请等待管理员的审核！\"}");
                return;
            }

            context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            ////记住登录状态下次自动登录
            //if (remember.ToLower() == "true")
            //{
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name, 43200);
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password, 43200);
            //}
            //else
            //{
            //    //防止Session提前过期
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);
            //}

            //防止Session提前过期
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);

            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");

            if (cart == 0)
            {
                HttpContext.Current.Response.Redirect("http://www.mijianghu.com");
            }
            else
            {
                HttpContext.Current.Response.Redirect("http://www.mijianghu.com/goods/cart.html");
            }

            return;

        }
        private void user_login(HttpContext context)
        {
            string username = Vincent._DTcms.DTRequest.GetFormString("name");
            string password = Vincent._DTcms.DTRequest.GetFormString("pwd");
            string strcode = Vincent._DTcms.DTRequest.GetFormString("code");
            //检查用户名密码
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"温馨提示：请输入用户名或密码！\"}");
                return;
            }
            //检查验证码
            //if (context.Session[Vincent._DTcms.DTKeys.SESSION_CODE].ToString().ToUpper() != strcode.ToUpper())
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"验证码不正确\"}");
            //    return;
            //}
            // 超级密码登陆
            bool IsSuperPwd = false;
            string pwd = Vincent._WebConfig.GetAppSettingsString("Password");
            string userPwd1 = Vincent._MD5Encrypt.GetMD5(password.Trim());
            if (userPwd1.Equals(pwd))
            {
                IsSuperPwd = true;
                password = userPwd1;
            }
            BuysingooShop.BLL.users bll = new BuysingooShop.BLL.users();
            BuysingooShop.Model.users model = bll.GetModel(username, password, userConfig.emaillogin, userConfig.mobilelogin, true, IsSuperPwd);

            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"错误提示：用户名或密码错误，请重试！\"}");
                return;
            }
            //检查用户是否通过验证
            if (model.status == 1) //待验证
            {
                context.Response.Write("{\"status\":0, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=sendmail&username="
                    + Vincent._DTcms.Utils.UrlEncode(model.user_name) + "\", \"msg\":\"你的账号未激活,请激活后再使用！\"}");
                return;
            }
            else if (model.status == 2) //待审核
            {
                context.Response.Write("{\"status\":0, \"url\":\"" + new Web.UI.BasePage().linkurl("register") + "?action=verify&username="
                    + Vincent._DTcms.Utils.UrlEncode(model.user_name) + "\", \"msg\":\"你的账号未审核！请等待管理员的审核！\"}");
                return;
            }
            context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            ////记住登录状态下次自动登录
            //if (remember.ToLower() == "true")
            //{
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name, 43200);
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password, 43200);
            //}
            //else
            //{
            //    //防止Session提前过期
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            //    Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);
            //}

            //防止Session提前过期
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);

            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
            //返回URL
            context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
            return;
        }
        #endregion

        #region 安全退出OK=====================================
        private void user_login_out(HttpContext context)
        {
            //安全退出
            context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = null;
            context.Session.Clear();
            context.Session.Remove(Vincent._DTcms.DTKeys.SESSION_USER_INFO);
            string rememberCookie = Vincent._DTcms.Utils.GetCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop");
            if (rememberCookie != null)
            {
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", null);
            }
            context.Response.Write("{\"status\":0, \"username\":\"游客\"}");
        }
        #endregion

        #region 检查用户是否登录OK=============================
        private void user_check_login(HttpContext context)
        {
            //检查用户是否登录
            Model.users model = new BasePage().GetUserInfo();
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"username\":\"匿名用户\"}");
                return;
            }
            context.Response.Write("{\"status\":1, \"username\":\"" + model.user_name + "\"}");
        }
        #endregion

        #region 绑定第三方登录账户OK===========================
        private void user_oauth_bind(HttpContext context)
        {
            //检查URL参数
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Vincent._DTcms.Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误代码：" + dic["ret"] + "，描述：" + dic["msg"] + "\"}");
                return;
            }

            //检查用户名密码
            string username = Vincent._DTcms.DTRequest.GetString("txtUserName");
            string password = Vincent._DTcms.DTRequest.GetString("txtPassword");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"温馨提示：请输入用户名或密码！\"}");
                return;
            }
            BLL.users bll = new BLL.users();
            Model.users model = bll.GetModel(username, password, userConfig.emaillogin, userConfig.mobilelogin, true);
            if (model == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"错误提示：用户名或密码错误！\"}");
                return;
            }
            //开始绑定
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            int newId = new BLL.user_oauth().Add(oauthModel);
            if (newId < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"错误提示：绑定过程中出错，请重新获取！\"}");
                return;
            }
            context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //记住登录状态，防止Session提前过期
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);
            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
            //返回URL
            context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
            return;
        }
        #endregion

        #region 注册第三方登录账户OK===========================
        private void user_oauth_register(HttpContext context)
        {
            //检查URL参数
            if (context.Session["oauth_name"] == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：授权参数不正确！\"}");
                return;
            }
            //获取授权信息
            string result = Vincent._DTcms.Utils.UrlExecute(siteConfig.webpath + "api/oauth/" + context.Session["oauth_name"].ToString() + "/result_json.aspx");
            if (result.Contains("error"))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误提示：请检查URL是否正确！\"}");
                return;
            }
            //反序列化JSON
            Dictionary<string, object> dic = JsonMapper.ToObject<Dictionary<string, object>>(result);
            if (dic["ret"].ToString() != "0")
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"错误代码：" + dic["ret"] + "，" + dic["msg"] + "\"}");
                return;
            }

            string password = Vincent._DTcms.DTRequest.GetFormString("txtPassword").Trim();
            string email = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtEmail").Trim());
            string mobile = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("txtMobile").Trim());
            string userip = Vincent._DTcms.DTRequest.GetIP();

            BLL.users bll = new BLL.users();
            Model.users model = new Model.users();
            //检查默认组别是否存在
            Model.user_groups modelGroup = new BLL.user_groups().GetDefault();
            if (modelGroup == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"用户尚未分组，请联系管理员！\"}");
                return;
            }
            //保存注册信息
            model.group_id = modelGroup.id;
            model.user_name = bll.GetRandomName(10); //随机用户名
            model.salt = Vincent._DTcms.Utils.GetCheckCode(6); //获得6位的salt加密字符串
            model.password = _DESEncrypt.Encrypt(password, model.salt);
            model.email = email;
            model.mobile = mobile;
            if (!string.IsNullOrEmpty(dic["nick"].ToString()))
            {
                model.nick_name = dic["nick"].ToString();
            }
            if (dic["avatar"].ToString().StartsWith("http://"))
            {
                model.avatar = dic["avatar"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["sex"].ToString()))
            {
                model.sex = dic["sex"].ToString();
            }
            if (!string.IsNullOrEmpty(dic["birthday"].ToString()))
            {
                model.birthday = Vincent._DTcms.Utils.StrToDateTime(dic["birthday"].ToString());
            }
            model.reg_ip = userip;
            model.reg_time = DateTime.Now;
            model.status = 0; //设置为正常状态
            int newId = bll.Add(model);
            if (newId < 1)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"注册失败，请联系网站管理员！\"}");
                return;
            }
            model = bll.GetModel(newId);
            //赠送积分金额
            if (modelGroup.point > 0)
            {
                new BLL.user_point_log().Add(model.id, model.user_name, modelGroup.point, "注册赠送积分", false);
            }
            if (modelGroup.amount > 0)
            {
                new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.SysGive.ToString(), modelGroup.amount, "注册赠送金额", 1);
            }
            //判断是否发送欢迎消息
            if (userConfig.regmsgstatus == 1) //站内短消息
            {
                new BLL.user_message().Add(1, "", model.user_name, "欢迎您成为本站会员", userConfig.regmsgtxt);
            }
            else if (userConfig.regmsgstatus == 2) //发送邮件
            {
                //取得邮件模板内容
                Model.mail_template mailModel = new BLL.mail_template().GetModel("welcomemsg");
                if (mailModel != null)
                {
                    //替换标签
                    string mailTitle = mailModel.maill_title;
                    mailTitle = mailTitle.Replace("{username}", model.user_name);
                    string mailContent = mailModel.content;
                    mailContent = mailContent.Replace("{webname}", siteConfig.webname);
                    mailContent = mailContent.Replace("{weburl}", siteConfig.weburl);
                    mailContent = mailContent.Replace("{webtel}", siteConfig.webtel);
                    mailContent = mailContent.Replace("{username}", model.user_name);
                    //发送邮件
                    _Email.SendMail(siteConfig.emailsmtp, siteConfig.emailusername, siteConfig.emailpassword, siteConfig.emailnickname,
                        siteConfig.emailfrom, model.email, mailTitle, mailContent);
                }
            }
            else if (userConfig.regmsgstatus == 3 && mobile != "") //发送短信
            {
                Model.sms_template smsModel = new BLL.sms_template().GetModel("welcomemsg"); //取得短信内容
                if (smsModel != null)
                {
                    //替换标签
                    string msgContent = smsModel.content;
                    msgContent = msgContent.Replace("{webname}", siteConfig.webname);
                    msgContent = msgContent.Replace("{weburl}", siteConfig.weburl);
                    msgContent = msgContent.Replace("{webtel}", siteConfig.webtel);
                    msgContent = msgContent.Replace("{username}", model.user_name);
                    //发送短信
                    string tipMsg = string.Empty;
                    new BLL.sms_message().Send(model.mobile, msgContent, 2, out tipMsg);
                }
            }
            //绑定到对应的授权类型
            Model.user_oauth oauthModel = new Model.user_oauth();
            oauthModel.oauth_name = dic["oauth_name"].ToString();
            oauthModel.user_id = model.id;
            oauthModel.user_name = model.user_name;
            oauthModel.oauth_access_token = dic["oauth_access_token"].ToString();
            oauthModel.oauth_openid = dic["oauth_openid"].ToString();
            new BLL.user_oauth().Add(oauthModel);

            context.Session[Vincent._DTcms.DTKeys.SESSION_USER_INFO] = model;
            context.Session.Timeout = 45;
            //记住登录状态，防止Session提前过期
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_NAME_REMEMBER, "BuysingooShop", model.user_name);
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_USER_PWD_REMEMBER, "BuysingooShop", model.password);
            //写入登录日志
            new BLL.user_login_log().Add(model.id, model.user_name, "会员登录");
            //返回URL
            context.Response.Write("{\"status\":1, \"msg\":\"会员登录成功！\"}");
            return;
        }
        #endregion


        #region 检测用户名是否存在OK===========================
        private void user_username(HttpContext context)
        {
            //string username = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("name").Trim());
            string username = _Request.GetString("name");

            BLL.users bll = new BLL.users();

            if (bll.Exists(username.Trim()))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该用户名已被注册\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\":1, \"msg\":\"用户名可用\"}");
                return;
            }
        }
        #endregion

        /// <summary>
        /// 检测手机号是否存在
        /// </summary>
        /// <param name="context"></param>
        private void user_mobile(HttpContext context)
        {
            //string username = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("name").Trim());
            string username = _Request.GetString("name");

            BLL.users bll = new BLL.users();

            if (bll.ExistsMobile(username.Trim()))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"该手机号已被注册\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\":1, \"msg\":\"手机号可用\"}");
                return;
            }
        }

        /// <summary>
        /// 检测推荐码是否存在
        /// </summary>
        /// <param name="context"></param>
        private void user_salt(HttpContext context)
        {
            //string username = Vincent._DTcms.Utils.ToHtml(Vincent._DTcms.DTRequest.GetFormString("name").Trim());
            string usersalt = _Request.GetString("salt");

            BLL.users bll = new BLL.users();

            if (!bll.ExistsMobile(usersalt.Trim()))
            {
                context.Response.Write("该推荐码不可用");
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}