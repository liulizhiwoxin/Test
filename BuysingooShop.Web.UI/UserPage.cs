using System;
using System.Web;

namespace BuysingooShop.Web.UI
{
    public partial class UserPage : BasePage
    {
        protected Model.users userModel;
        protected Model.user_groups groupModel;

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(UserPage_Init); //加入IInit事件
        }

        /// <summary>
        /// OnInit事件,检查用户是否登录
        /// </summary>
        void UserPage_Init(object sender, EventArgs e)
        {
            if (!IsUserLogin())
            {
                //跳转URL
                HttpContext.Current.Response.Redirect(linkurl("login"));
                return;
            }
            //获得登录用户信息
            userModel = GetUserInfo();
            groupModel = new BLL.user_groups().GetModel(userModel.group_id);
            if (groupModel == null)
            {
                groupModel = new Model.user_groups();
            }
            InitPage();
        }

        /// <summary>
        /// 构建一个虚方法，供子类重写
        /// </summary>
        protected virtual void InitPage()
        {
            //无任何代码
        }
        //#region JS提示============================================
        ///// <summary>
        ///// 添加编辑删除提示
        ///// </summary>
        ///// <param name="msgtitle">提示文字</param>
        ///// <param name="url">返回地址</param>
        ///// <param name="msgcss">CSS样式</param>
        //protected void JscriptMsg(string msgtitle, string url, string msgcss)
        //{
        //    string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\")";
        //    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        //}
        ///// <summary>
        ///// 带回传函数的添加编辑删除提示
        ///// </summary>
        ///// <param name="msgtitle">提示文字</param>
        ///// <param name="url">返回地址</param>
        ///// <param name="msgcss">CSS样式</param>
        ///// <param name="callback">JS回调函数</param>
        //protected void JscriptMsg(string msgtitle, string url, string msgcss, string callback)
        //{
        //    string msbox = "parent.jsprint(\"" + msgtitle + "\", \"" + url + "\", \"" + msgcss + "\", " + callback + ")";
        //    ClientScript.RegisterClientScriptBlock(Page.GetType(), "JsPrint", msbox, true);
        //}
        //#endregion
    }
}