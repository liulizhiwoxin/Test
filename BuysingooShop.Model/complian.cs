using System;
using System.Collections.Generic;

namespace BuysingooShop.Model
{
    /// <summary>
    /// Ͷ�߱�
    /// </summary>
    [Serializable]
    public partial class complian
    {
        public complian()
        { }

        #region Model
        private int _id;
        private int _user_id = 0;
        private string _user_name = "";
        private string _complian_title;
        private DateTime? _complian_time = null;
        private string _complian_content = "";
        private string _mobile_phone = "";
        private int _is_status = 1;
        private int _is_anonymous = 1;
        private int _parent_id = 0;
        private int _com_type = 0;
        private int _good_id = 0;
        private string _target;
        private string _order_no;
        private string _reply_content;

        /// <summary>
        /// ����ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        /// �û�����
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string complian_title
        {
            set { _complian_title = value; }
            get { return _complian_title; }
        }
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime? complian_time
        {
            set { _complian_time = value; }
            get { return _complian_time; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string complian_content
        {
            set { _complian_content = value; }
            get { return _complian_content; }
        }
        /// <summary>
        /// ��ϵ��ʽ
        /// </summary>
        public string mobile_phone
        {
            set { _mobile_phone = value; }
            get { return _mobile_phone; }
        }
        /// <summary>
        /// ״̬ 1δ����,2������,3�����
        /// </summary>
        public int is_status
        {
            set { _is_status = value; }
            get { return _is_status; }
        }
        /// <summary>
        /// �Ƿ����� 0����,1��ʾ(Ĭ��Ϊ1)
        /// </summary>
        public int is_anonymous
        {
            set { _is_anonymous = value; }
            get { return _is_anonymous; }
        }
        /// <summary>
        /// ��ID (Ĭ��Ϊ0)
        /// </summary>
        public int parent_id
        {
            set { _parent_id = value; }
            get { return _parent_id; }
        }
        /// <summary>
        /// ��� 0��ѯ,1Ͷ��,2����(Ĭ��Ϊ0)
        /// </summary>
        public int com_type
        {
            set { _com_type = value; }
            get { return _com_type; }
        }
        /// <summary>
        /// ��Ʒid
        /// </summary>
        public int good_id
        {
            set { _good_id = value; }
            get { return _good_id; }
        }
        /// <summary>
        /// Ͷ�߶���
        /// </summary>
        public string target
        {
            set { _target = value; }
            get { return _target; }
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
        /// �ظ�
        /// </summary>
        public string reply_content
        {
            set { _reply_content = value; }
            get { return _reply_content; }
        }
        #endregion Model


    }
}