using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    #region 商品品牌实体类============================================
    /// <summary>
    /// article:实体类
    /// </summary>
    [Serializable]
    public partial class source_material
    {
        //无参构造函数
        public source_material() { }

        private int _id;
        private int _user_id;
        private string _title;
        private string _subtitle;
        private string _img_url;
        private string _remark;
        private int _type = 0;
        private int _sort_id = 99;
        private DateTime _add_time = DateTime.Now;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 副标题
        /// </summary>
        public string subtitle
        {
            set { _subtitle = value; }
            get { return _subtitle; }
        }
        /// <summary>
        /// 图片
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
        /// 类型0背景1瓶子2文字3相片4装饰
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
    }
    #endregion Model
}
