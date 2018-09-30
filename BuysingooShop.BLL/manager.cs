using System;
using System.Data;
using System.Collections.Generic;
using Vincent;

namespace BuysingooShop.BLL
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    public partial class manager
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.manager dal;
        public manager()
        {
            dal = new DAL.manager(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 查询用户名是否存在
        /// </summary>
        public bool Exists(string user_name)
        {
            return dal.Exists(user_name);
        }

        /// <summary>
        /// 查询折扣码是否存在
        /// </summary>
        public bool ExistsCode(string str_code)
        {
            return dal.ExistsCode(str_code);
        }

        /// <summary>
        /// 根据用户名取得Salt
        /// </summary>
        public string GetSalt(string user_name)
        {
            return dal.GetSalt(user_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager model)
        {
            return dal.Update(model);
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
        public Model.manager GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModel(string  username)
        {
            return dal.GetModel(username);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.manager GetModelCode(string strwhere)
        {
            return dal.GetModelCode(strwhere);
        }
        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password)
        {
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt)
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
            return dal.GetModel(user_name, password);
        }

        /// <summary>
        /// 根据用户名密码返回一个实体
        /// </summary>
        public Model.manager GetModel(string user_name, string password, bool is_encrypt, bool isSuperPwd)
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

                return dal.GetModel(user_name, password);
            }
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

        #endregion  Method
    }
}