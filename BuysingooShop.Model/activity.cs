using System;

namespace BuysingooShop.Model
{
    /// <summary>
    /// activity:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class activity
    {
        public activity()
        { }

        #region Model
        private int _id;
        private string _fields;
        private string _value;
        private string _remark;
        private int _is_close = 0;
        private DateTime _start_time = DateTime.Now;
        private DateTime _end_time = DateTime.Now;
        private string _category_id = "";



        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string fields
        {
            set { _fields = value; }
            get { return _fields; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int is_close
        {
            set { _is_close = value; }
            get { return _is_close; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime start_time
        {
            set { _start_time = value; }
            get { return _start_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime end_time
        {
            set { _end_time = value; }
            get { return _end_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string category_id
        {
            set { _category_id = value; }
            get { return _category_id; }
        }

        #endregion Model

    }
}
