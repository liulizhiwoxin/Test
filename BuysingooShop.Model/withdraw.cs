using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    [Serializable]
    public partial class withdraw
    {
        public withdraw() { }

        private int _id;
        private int _user_id;
        private string _card_no = "";
        private decimal _amount = 0M;
        private int _banktype=0;
        private string _remark = "";
        private int _status=0;
        private DateTime _addtime = DateTime.Now;
        private string _reason = "";
        private string _img_url = "";

        private string _openId = "";

        public string OpenId
        {
            get { return _openId; }
            set { _openId = value; }
        }

        private string _mobile = "";

        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int user_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string card_no
        {
            get { return _card_no; }
            set { _card_no = value; }
        }
        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// 银行类型
        /// </summary>
        public int banktype
        {
            get { return _banktype; }
            set { _banktype = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 提现状态
        /// </summary>
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime addtime
        {
            get { return _addtime; }
            set { _addtime = value; }
        }
        /// <summary>
        /// 驳回原因
        /// </summary>
        public string reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        /// <summary>
        /// 转账凭据
        /// </summary>
        public string img_url
        {
            get { return _img_url; }
            set { _img_url = value; }
        }

    }
}
