using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    public class systemlog
    {
        public systemlog(){}

        #region 私有属性

        /// <summary>
        /// ID
        /// </summary>
        private int _ID;

        /// <summary>
        /// UserID
        /// </summary>
        private string _UserID;

        /// <summary>
        /// DateTime
        /// </summary>
        private DateTime _DateTime;

        /// <summary>
        /// IPAddress
        /// </summary>
        private string _IPAddress;

        /// <summary>
        /// Description
        /// </summary>
        private string _Description;

        /// <summary>
        /// TypeId
        /// </summary>
        private int _TypeId;

        /// <summary>
        /// GroupId
        /// </summary>
        private int _GroupId;

        /// <summary>
        /// Value
        /// </summary>
        private string _Value;

        /// <summary>
        /// 用户手机号
        /// </summary>
        private string _UserName;


        #endregion

        #region 公共属性

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get { return _ID; } set { _ID = value; } }

        /// <summary>
        /// UserID
        /// </summary>
        public string UserID { get { return _UserID; } set { _UserID = value; } }

        /// <summary>
        /// DateTime
        /// </summary>
        public DateTime DateTime { get { return _DateTime; } set { _DateTime = value; } }

        /// <summary>
        /// IPAddress
        /// </summary>
        public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get { return _Description; } set { _Description = value; } }

        /// <summary>
        /// TypeId
        /// </summary>
        public int TypeId { get { return _TypeId; } set { _TypeId = value; } }

        /// <summary>
        /// GroupId
        /// </summary>
        public int GroupId { get { return _GroupId; } set { _GroupId = value; } }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get { return _Value; } set { _Value = value; } }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserName { get { return _UserName; } set { _UserName = value; } }

        #endregion
    }
}
