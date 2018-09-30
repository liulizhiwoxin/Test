using System;
using System.Collections.Generic;

namespace BuysingooShop.Model
{
    /// <summary>
    /// ������
    /// </summary>
    [Serializable]
    public partial class orders
    {
        public orders()
        { }
        #region Model
        private int _id;
        private string _idstring = "";
        private string _order_no = "";
        private string _trade_no = "";
        private int _user_id = 0;
        private string _user_name = "";
        private int _payment_id = 0;
        private decimal _payment_fee = 0M;
        private int _payment_status = 1;
        private DateTime? _payment_time;
        private int _express_id = 0;
        private string _express_no = "";
        private decimal _express_fee = 0M;
        private int _express_status = 1;
        private string _express_statusstring = "";
        private DateTime? _express_time;
        private string _accept_name = "";
        private string _post_code = "";
        private string _telphone = "";
        private string _mobile = "";
        private string _area = "";
        private string _address = "";
        private string _message = "";
        private string _remark = "";
        private decimal _payable_amount = 0M;
        private decimal _real_amount = 0M;
        private decimal _order_amount = 0M;
        private string _order_amountstring = "";
        private int _point = 0;
        private int _status = 1;
        private string _statusstring = "";
        private DateTime _add_time = DateTime.Now;
        private string _timestring = "";
        private DateTime? _confirm_time;
        private DateTime? _complete_time;
        private DateTime? _winery_time;
        private string _str_code = "";
        private int _brand_id = 0;
        private int _is_bill = 0;
        private int _bill_type = 0;
        private string _down_order = "";
        private string _invoice_rise = "";
        private string _store_name = "";
        private string _store_address = "";
        private int _store_id = 0;
        private string _school_info = "";
        /// <summary>
        /// ����ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ѧУ��Ϣ
        /// </summary>
        public string school_info
        {
            set { _school_info = value; }
            get { return _school_info; }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public string idstring
        {
            set { _idstring = value; }
            get { return _idstring; }
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
        /// ���׺ŵ���֧���õ�
        /// </summary>
        public string trade_no
        {
            set { _trade_no = value; }
            get { return _trade_no; }
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
        /// ֧����ʽ 0 ����֧�� 1��������
        /// </summary>
        public int payment_id
        {
            set { _payment_id = value; }
            get { return _payment_id; }
        }
        /// <summary>
        /// ֧��������
        /// </summary>
        public decimal payment_fee
        {
            set { _payment_fee = value; }
            get { return _payment_fee; }
        }
        /// <summary>
        /// ֧��״̬1δ֧��2��֧��
        /// </summary>
        public int payment_status
        {
            set { _payment_status = value; }
            get { return _payment_status; }
        }
        /// <summary>
        /// ֧��ʱ��
        /// </summary>
        public DateTime? payment_time
        {
            set { _payment_time = value; }
            get { return _payment_time; }
        }
        /// <summary>
        /// ���ID
        /// </summary>
        public int express_id
        {
            set { _express_id = value; }
            get { return _express_id; }
        }
        /// <summary>
        /// ��ݵ���
        /// </summary>
        public string express_no
        {
            set { _express_no = value; }
            get { return _express_no; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public decimal express_fee
        {
            set { _express_fee = value; }
            get { return _express_fee; }
        }
        /// <summary>
        /// ����״̬1δ����2�ѷ���
        /// </summary>
        public int express_status
        {
            set { _express_status = value; }
            get { return _express_status; }
        }

        /// <summary>
        /// ����״̬1δ����2�ѷ���(����)
        /// </summary>
        public string express_statusstring
        {
            set { _express_statusstring = value; }
            get { return _express_statusstring; }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? express_time
        {
            set { _express_time = value; }
            get { return _express_time; }
        }
        /// <summary>
        /// �ջ�������
        /// </summary>
        public string accept_name
        {
            set { _accept_name = value; }
            get { return _accept_name; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string post_code
        {
            set { _post_code = value; }
            get { return _post_code; }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public string telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// �ֻ�
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// �ջ���ַ��ID
        /// </summary>
        public string area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// �ջ���ַ
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// ������ע
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// Ӧ����Ʒ�ܽ��
        /// </summary>
        public decimal payable_amount
        {
            set { _payable_amount = value; }
            get { return _payable_amount; }
        }
        /// <summary>
        /// ʵ����Ʒ�ܽ��
        /// </summary>
        public decimal real_amount
        {
            set { _real_amount = value; }
            get { return _real_amount; }
        }

        /// <summary>
        /// �����ܽ��
        /// </summary>
        public decimal order_amount
        {
            set { _order_amount = value; }
            get { return _order_amount; }
        }

        /// <summary>
        /// �����ܽ��(����)
        /// </summary>
        public string order_amountstring
        {
            set { _order_amountstring = value; }
            get { return _order_amountstring; }
        }
        /// <summary>
        /// ����,��������|��������
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// ����״̬1���ɶ���,2��ƽ̨DIYȷ��,3���Ƴ�����ȷ��,4�Ƴ��������,90��ɶ���,91������,99ȡ������,100���϶���
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }

        public string statusstring
        {
            set { _statusstring = value; }
            get { return _statusstring; }
        }
        /// <summary>
        /// �µ�ʱ��
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }

        /// <summary>
        /// �µ�ʱ��(����)
        /// </summary>
        public string timestring
        {
            set { _timestring = value; }
            get { return _timestring; }
        }
        /// <summary>
        /// ȷ��ʱ��
        /// </summary>
        public DateTime? confirm_time
        {
            set { _confirm_time = value; }
            get { return _confirm_time; }
        }
        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }
        /// <summary>
        /// �Ƴ��������ʱ��
        /// </summary>
        public DateTime? winery_time
        {
            set { _winery_time = value; }
            get { return _winery_time; }
        }
        /// <summary>
        /// �ۿ۴���
        /// </summary>
        public string str_code
        {
            set { _str_code = value; }
            get { return _str_code; }
        }
        /// <summary>
        /// Ʒ��ID
        /// </summary>
        public int brand_id
        {
            set { _brand_id = value; }
            get { return _brand_id; }
        }

        /// <summary>
        /// �Ƿ񿪾߷�Ʊ 0�޷�Ʊ 1�з�Ʊ
        /// </summary>
        public int is_bill
        {
            set { _is_bill = value; }
            get { return _is_bill; }
        }
        /// <summary>
        /// ��Ʊ���� 0�޷�Ʊ 1ֽ�ʷ�Ʊ 2���ӷ�Ʊ
        /// </summary>
        public int bill_type
        {
            set { _bill_type = value; }
            get { return _bill_type; }
        }
        /// <summary>
        /// �µ�����
        /// </summary>
        public string down_order
        {
            set { _down_order = value; }
            get { return _down_order; }
        }

        /// <summary>
        /// ��Ʊ̧ͷ
        /// </summary>
        public string invoice_rise
        {
            set { _invoice_rise = value; }
            get { return _invoice_rise; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string store_name
        {
            set { _store_name = value; }
            get { return _store_name; }
        }
        /// <summary>
        /// ���̵�ַ
        /// </summary>
        public string store_address
        {
            set { _store_address = value; }
            get { return _store_address; }
        }
        /// <summary>
        /// ����id
        /// </summary>
        public int store_id
        {
            set { _store_id = value; }
            get { return _store_id; }
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