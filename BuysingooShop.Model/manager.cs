using System;
using System.Collections.Generic;
using System.Text;

namespace BuysingooShop.Model
{
    /// <summary>
    /// 管理员信息表:实体类
    /// </summary>
    [Serializable]
    public partial class manager
    {
        public manager()
        { }
        #region Model
        private int _id;
        private int _role_id;
        private int _role_type = 2;
        private string _role_name;
        private string _user_name;
        private string _password;
        private string _salt;
        private string _real_name = "";
        private string _telephone = "";
        private string _email = "";
        private int _is_lock = 0;
     
        private DateTime _add_time = DateTime.Now;
        private int _winery_id = 0;
        private string _str_code = "";
        private string _str_code_rage = "";
        private int _brand_id = 0;
        private int _age = 0;
        private int _workAge = 0;
        private string _style = "";
        private string _img_url = "";
        private string _remark = "";
        private string _qq = "";

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 管理员类型1超管2系管
        /// </summary>
        public int role_type
        {
            set { _role_type = value; }
            get { return _role_type; }
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public string role_name
        {
            set { _role_name = value; }
            get { return _role_name; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 6位随机字符串,加密用到
        /// </summary>
        public string salt
        {
            set { _salt = value; }
            get { return _salt; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string real_name
        {
            set { _real_name = value; }
            get { return _real_name; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telephone
        {
            set { _telephone = value; }
            get { return _telephone; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 酒厂ID
        /// </summary>
        public int winery_id
        {
            set { _winery_id = value; }
            get { return _winery_id; }
        }
        /// <summary>
        /// 折扣代码
        /// </summary>
        public string str_code
        {
            set { _str_code = value; }
            get { return _str_code; }
        }
        /// <summary>
        /// 折扣率
        /// </summary>
        public string str_code_rage
        {
            set { _str_code_rage = value; }
            get { return _str_code_rage; }
        }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public int brand_id
        {
            set { _brand_id = value; }
            get { return _brand_id; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 从业年龄
        /// </summary>
        public int workAge
        {
            set { _workAge = value; }
            get { return _workAge; }
        }
        /// <summary>
        /// 擅长风格
        /// </summary>
        public string style 
        {
            set { _style = value; }
            get { return _style; }
        }
        /// <summary>
        /// 设计师照片
        /// </summary>
        public string img_url 
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark 
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ 
        {
            set { _qq = value; }
            get { return _qq; }
        }

        #endregion Model
    }
}
