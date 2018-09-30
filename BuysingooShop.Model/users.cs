using System;
namespace BuysingooShop.Model
{
    #region 用户信息实体类
    /// <summary>
    /// users实体类
    /// </summary>
    [Serializable]
    public partial class users
    {
        public users(){ }

        private int _id;
        private int _group_id = 0;
        private string _user_name;
        private string _password;
        private string _salt;
        private string _email = "";
        private string _nick_name = "";
        private string _avatar = "";
        private string _sex = "保密";
        private DateTime? _birthday;
        private string _telphone = "";
        private string _mobile = "";
        private string _qq = "";
        private string _address = "";
        private string _safe_question = "";
        private string _safe_answer = "";
        private decimal _amount = 0M;
        private int _point = 0;
        private int _exp = 0;
        private int _status = 0;
        private DateTime _reg_time = DateTime.Now;
        private DateTime _pay_time = DateTime.Now;

        private string _reg_time_str = "";
        private string _pay_time_str = "";

        private string _reg_ip;
        private string _strcode;
        private string _real_name;
        private string _postcode;
        private string _parentSalt;
        private int _isemail;
        private int _ismobile;
        private decimal _frozen_amount = 0M;

        private int _parentid;
        private int _leftor_right;
        private int _marketId;

        private int _organizeId;
        private int _preId;

        private decimal _amount_total = 0M;
        private decimal _frozen_amount_total = 0M;

        private string _city;
        private string _provinces;
        private string _Area;
        private int _point_total;

        private int _isBuwei;

        public int IsBuwei
        {
            get { return _isBuwei; }
            set { _isBuwei = value; }
        }

        /// <summary>
        /// 累积积分
        /// </summary>
        public int point_total
        {
            get { return _point_total; }
            set { _point_total = value; }
        }
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }
      
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        
        public string Provinces
        {
            get { return _provinces; }
            set { _provinces = value; }
        }

        public int PreId
        {
            get { return _preId; }
            set { _preId = value; }
        }
        
        /// <summary>
        /// 组织Id 
        /// </summary>
        public int OrganizeId
        {
            get { return _organizeId; }
            set { _organizeId = value; }
        }

        /// <summary>
        /// 上级会员ID
        /// </summary>
        public int Parentid
        {
            get { return _parentid; }
            set { _parentid = value; }
        }

        /// <summary>
        /// 0不区分  1左边 2右边
        /// </summary>
        public int Leftor_right
        {
            get { return _leftor_right; }
            set { _leftor_right = value; }
        }

        /// <summary>
        /// marketId市场ID，一个市场一个ID
        /// </summary>
        public int MarketId
        {
            get { return _marketId; }
            set { _marketId = value; }
        }



        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户组别
        /// </summary>
        public int group_id
        {
            set { _group_id = value; }
            get { return _group_id; }
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
        /// 真实姓名
        /// </summary>
        public string real_name
        {
            set { _real_name = value; }
            get { return _real_name; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 用户注册密钥Key
        /// </summary>
        public string salt
        {
            set { _salt = value; }
            get { return _salt; }
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
        /// 用户昵称
        /// </summary>
        public string nick_name
        {
            set { _nick_name = value; }
            get { return _nick_name; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar
        {
            set { _avatar = value; }
            get { return _avatar; }
        }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string qq
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 联系地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 安全问题
        /// </summary>
        public string safe_question
        {
            set { _safe_question = value; }
            get { return _safe_question; }
        }
        /// <summary>
        /// 问题答案
        /// </summary>
        public string safe_answer
        {
            set { _safe_answer = value; }
            get { return _safe_answer; }
        }
        /// <summary>
        /// 预存款
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 用户积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 经验值
        /// </summary>
        public int exp
        {
            set { _exp = value; }
            get { return _exp; }
        }
        /// <summary>
        /// 用户状态,0正常,1待验证,2待审核,3锁定
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime reg_time
        {
            set { _reg_time = value; }
            get { return _reg_time; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime pay_time
        {
            set { _pay_time = value; }
            get { return _pay_time; }
        }


        public string reg_time_str
        {
            set { _reg_time_str = value; }
            get { return _reg_time_str; }
        }

        public string pay_time_str
        {
            set { _pay_time_str = value; }
            get { return _pay_time_str; }
        }
     
        /// <summary>
        /// 注册IP
        /// </summary>
        public string reg_ip
        {
            set { _reg_ip = value; }
            get { return _reg_ip; }
        }
        /// <summary>
        /// 注册验证码
        /// </summary>
        public string strcode
        {
            set { _strcode = value; }
            get { return _strcode; }
        }
        /// <summary>
        /// 推荐人密钥
        /// </summary>
        public string parentSalt
        {
            set { _parentSalt = value; }
            get { return _parentSalt; }
        }
        /// <summary>
        /// 邮箱是否验证
        /// </summary>
        public int isEmail
        {
            set { _isemail = value; }
            get { return _isemail; }
        }
        /// <summary>
        /// 手机是否验证
        /// </summary>
        public int isMobile
        {
            set { _ismobile = value; }
            get { return _ismobile; }
        }
        /// <summary>
        /// 冻结金额
        /// </summary>
        public decimal frozen_amount
        {
            set { _frozen_amount = value; }
            get { return _frozen_amount; }
        }

        /// <summary>
        /// 预存款
        /// </summary>
        public decimal amount_total
        {
            set { _amount_total = value; }
            get { return _amount_total; }
        }

        /// <summary>
        /// 预存款
        /// </summary>
        public decimal frozen_amount_total
        {
            set { _frozen_amount_total = value; }
            get { return _frozen_amount_total; }
        }
    }
    
        #endregion Model
}