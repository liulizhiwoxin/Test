using System;
using System.Collections.Generic;

namespace BuysingooShop.Model
{
    /// <summary>
    /// �˿��
    /// </summary>
    [Serializable]
    public partial class refund
    {
        public refund()
        { }

        #region Model
        private int _id;
        private string _order_no = "";
        private int _user_id = 0;
        private string _user_name = "";
        private decimal _refund_fee = 0M;
        private decimal _express_money = 0M;
        private decimal _refund_money = 0M;
        private DateTime? _apply_time = null;
        private DateTime? _affirm_time = null;
        private DateTime? _complete_time = null;
        private string _express_no = "";
        private int _refund_type = 0;
        private int _refund_status = 1;
        private string _refund_reason = "";
        private string _un_refund_reason = "";
        private string _refund_no = "";
        private string _express_code = "";
        private string _number = "";

        /// <summary>
        /// ����ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// �û�ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// �û���
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// ���׽��
        /// </summary>
        public decimal express_money
        {
            set { _express_money = value; }
            get { return _express_money; }
        }
        /// <summary>
        /// �˿���
        /// </summary>
        public decimal refund_money
        {
            set { _refund_money = value; }
            get { return _refund_money; }
        }
        /// <summary>
        /// �˿�������
        /// </summary>
        public decimal refund_fee
        {
            set { _refund_fee = value; }
            get { return _refund_fee; }
        }
        /// <summary>
        /// �����˿�ʱ��
        /// </summary>
        public DateTime? apply_time
        {
            set { _apply_time = value; }
            get { return _apply_time; }
        }
        /// <summary>
        /// ȷ���˿�ʱ��
        /// </summary>
        public DateTime? affirm_time
        {
            set { _affirm_time = value; }
            get { return _affirm_time; }
        }
        /// <summary>
        /// �˿����ʱ��
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }
        /// <summary>
        /// �˿��ݵ���
        /// </summary>
        public string express_no
        {
            set { _express_no = value; }
            get { return _express_no; }
        }
        /// <summary>
        /// �˻���ʽ(1�����  2������ȡ��)
        /// </summary>
        public int refund_type
        {
            set { _refund_type = value; }
            get { return _refund_type; }
        }
        /// <summary>
        /// �˿�ԭ��
        /// </summary>
        public string refund_reason
        {
            set { _refund_reason = value; }
            get { return _refund_reason; }
        }
        /// <summary>
        /// �˿��ԭ��
        /// </summary>
        public string un_refund_reason
        {
            set { _un_refund_reason = value; }
            get { return _un_refund_reason; }
        }
        /// <summary>
        /// �˿�״̬ 1�����˿,2ȷ���˿,3����˿,4ȡ���˿,5�����˿
        /// </summary>
        public int refund_status
        {
            set { _refund_status = value; }
            get { return _refund_status; }
        }
        /// <summary>
        /// �˿��
        /// </summary>
        public string refund_no
        {
            set { _refund_no = value; }
            get { return _refund_no; }
        }
        /// <summary>
        /// ��ݴ���
        /// </summary>
        public string express_code
        {
            set { _express_code = value; }
            get { return _express_code; }
        }
        /// <summary>
        /// �˻�������
        /// </summary>
        public string number
        {
            set { _number = value; }
            get { return _number; }
        }
        #endregion Model

        private List<order_goods> _order_goods;
        /// <summary>
        /// ��Ʒ�б�
        /// </summary>
        public List<order_goods> order_goods
        {
            set { _order_goods = value; }
            get { return _order_goods; }
        }
    }
}