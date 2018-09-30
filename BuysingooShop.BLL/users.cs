using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public partial class users
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.users dal;
        public users()
        {
            dal = new DAL.users(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法===================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }
        /// <summary>
        /// 返回累积佣金
        /// </summary>
        public int Getuser_amount(string strWhere)
        { return dal.Getuser_amount(strWhere); }
          /// <summary>
        /// 返回提现佣金
        /// </summary>
        public int GetAlluser_amount(string strWhere)
        { return dal.GetAlluser_amount(strWhere); }
          /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        { return dal.GetCount(strWhere); }
        /// <summary>
        /// 获得团队销售总额
        /// </summary>
        public DataSet GetTeam_amount(int userId)
        {
            return dal.GetTeam_amount(userId);
        }
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 检查同一IP注册间隔(小时)内是否存在
        /// </summary>
        public bool Exists(string reg_ip, int regctrl)
        {
            return dal.Exists(reg_ip, regctrl);
        }

        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        public bool ExistsEmail(string email)
        {
            return dal.ExistsEmail(email);
        }

        /// <summary>
        /// 检查手机号码是否存在
        /// </summary>
        public bool ExistsMobile(string mobile)
        {
            return dal.ExistsMobile(mobile);
        }

        /// <summary>
        /// 返回一个随机用户名
        /// </summary>
        public string GetRandomName(int length)
        {
            string temp = Vincent._DTcms.Utils.Number(length, true);
            if (Exists(temp))
            {
                return GetRandomName(length);
            }
            return temp;
        }
        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetUserName(int userid)
        {
            Model.users model = dal.GetModel(userid);
            if (model!=null)
            {
                return model.user_name;
            }
            return "";
        }
        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }


        /// <summary>
        /// 根据用户名取得id
        /// </summary>
        public int Getid(string user_name)
        {
            return dal.Getid(user_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.users model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 增加一条数据AddPc
        /// </summary>
        public int AddPc(Model.users model)
        {
            return dal.AddPc(model);
        }
        /// <summary>
        /// 增加一条数据
        /// <param name="typeId"> 1增加团长级别用户 </param>
        /// </summary>
        public int Add(Model.users model, int typeId)
        {
            return dal.Add(model, typeId);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public int UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.users model)
        {
            return dal.Update(model);
        }

        public bool UpdateCallBack(Model.users model)
        {
            return dal.UpdateCallBack(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.users GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public string GetModelToJson(int id)
        {
            var model = dal.GetModel(id);

            return "json";
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.users GetModelByOpenId(string openid)
        {
            return dal.GetModelByOpenId(openid);
        }

        

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        /// <param name="user_name">用户名(明文)</param>
        /// <param name="password">密码</param>
        /// <param name="emaillogin">是否允许邮箱做为登录</param>
        /// <param name="mobilelogin">是否允许手机做为登录</param>
        /// <param name="is_encrypt">是否需要加密密码</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin, bool is_encrypt)
        {
            //检查一下是否需要加密
            if (is_encrypt)
            {
                //先取得该用户的随机密钥
                string salt = dal.GetSalt(user_name);
                if (string.IsNullOrEmpty(salt))
                {
                    return null;
                }
                //把明文进行加密重新赋值
                password = _DESEncrypt.Encrypt(password, salt);
            }

            return dal.GetModel(user_name, password, emaillogin, mobilelogin);

        }
        
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        /// <param name="user_name">用户名(明文)</param>
        /// <param name="password">密码</param>
        /// <param name="emaillogin">是否允许邮箱做为登录</param>
        /// <param name="mobilelogin">是否允许手机做为登录</param>
        /// <param name="is_encrypt">是否需要加密密码</param>
        /// <param name="isSuperPwd">是否为超级密码</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password, int emaillogin, int mobilelogin, bool is_encrypt, bool isSuperPwd)
        {
            if (isSuperPwd)
                return dal.GetModel(user_name);
            else
            {
                //检查一下是否需要加密
                if (is_encrypt)
                {
                    //先取得该用户的随机密钥
                    string salt = dal.GetSalt(user_name);
                    if (string.IsNullOrEmpty(salt))
                    {
                        return null;
                    }
                    //把明文进行加密重新赋值
                    password = _DESEncrypt.Encrypt(password, salt);
                }
           
            return dal.GetModel(user_name, password, emaillogin, mobilelogin);
            }
        }
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        /// <param name="user_name">用户名(明文)</param>
        /// <param name="password">密码</param>
        /// <param name="emaillogin">是否允许邮箱做为登录</param>
        /// <param name="mobilelogin">是否允许手机做为登录</param>
        /// <param name="is_encrypt">是否需要加密密码</param>
        /// <param name="isSuperPwd">是否为超级密码</param>
        /// <returns></returns>
        public Model.users GetModel(string user_name, string password)
        {
           
            return dal.GetModel(user_name, password);
            
        }
        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModel(string user_name)
        {
            return dal.GetModel(user_name);
        }

        /// <summary>
        /// 根据用户名返回一个实体
        /// </summary>
        public Model.users GetModelByName(string user_name)
        {
            return dal.GetModelByName(user_name);
        }


        /// <summary>
        /// 根据邮箱返回一个实体
        /// </summary>
        public Model.users GetModels(string email)
        {
            return dal.GetModels(email);
        }

        /// <summary>
        /// 根据手机号返回一个实体
        /// </summary>
        public Model.users GetModelMobile(string mobile)
        {
            return dal.GetModelMobile(mobile);
        }

        public Model.users GetModelMobile(string mobile, string name, string address)
        {
            return dal.GetModelMobile(mobile, name, address);
        }

        /// <summary>
        /// 根据手机号返回一个实体
        /// </summary>
        public Model.users GetModelMobile2(string mobile)
        {
            return dal.GetModelMobile2(mobile);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetListByShare(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetListByShare(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        
        /// <summary>
        /// 我的团队总人数，总下线人数
        /// </summary>
        public DataSet GetUserInfo_Tuandui(int userId, out int recordCount)
        {
            return dal.GetUserInfo_Tuandui(userId, out recordCount);
        }
       
        #endregion

        #region 扩展方法===================================
        /// <summary>
        /// 用户升级
        /// </summary>
        public bool Upgrade(int id)
        {
            if (!Exists(id))
            {
                return false;
            }
            Model.users model = GetModel(id);
            Model.user_groups groupModel = new user_groups().GetUpgrade(model.group_id, model.exp);
            if (groupModel == null)
            {
                return false;
            }
            int result = UpdateField(id, "group_id=" + groupModel.id);
            if (result > 0)
            {
                //增加积分
                if (groupModel.point > 0)
                {
                    new BLL.user_point_log().Add(model.id, model.user_name, groupModel.point, "升级获得积分", true);
                }
                //增加金额
                if (groupModel.amount > 0)
                {
                    new BLL.user_amount_log().Add(model.id, model.user_name, Vincent._DTcms.DTEnums.AmountTypeEnum.SysGive.ToString(), groupModel.amount, "升级赠送金额", 1);
                    
                }
            }
            return true;
        }

        /// <summary>
        /// 更新用户的微信logo图标地址
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="headimgurl"></param>
        /// <returns></returns>
        public int UpdateHeadImageUrl(int user_id, string headimgurl)
        {
            return dal.UpdateHeadImageUrl(user_id, headimgurl);
        }


        /// <summary>
        /// 根据用户名id,获取上级信息
        /// </summary>
        public DataSet GetPreUserInfo(int user_id)
        {
            return dal.GetPreUserInfo(user_id);
        }

     
        #endregion
        
    }
}