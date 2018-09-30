using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    #region 模板图片实体类============================================
    /// <summary>
    /// article:实体类
    /// </summary>
    [Serializable]
    public partial class good_template_pic
    {
        //无参构造函数
        public good_template_pic() { }

        private int _id;
        private int _templateId=0;
        private int _typeId=1;
        private string _picUrl;
        private string _smallPicUrl;
        private DateTime _addTime = DateTime.Now;
        private int _isLock = 0;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 模板表ID
        /// </summary>
        public int templateId
        {
            set { _templateId = value; }
            get { return _templateId; }
        }
        /// <summary>
        /// 1盒子正面 2盒子背面 3盒子侧面 4瓶子正面 5瓶子背面 6瓶子侧面
        /// </summary>
        public int typeId
        {
            set { _typeId = value; }
            get { return _typeId; }
        }
        /// <summary>
        /// 图片URL
        /// </summary>
        public string picUrl
        {
            set { _picUrl = value; }
            get { return _picUrl; }
        }
        /// <summary>
        /// 缩略图URL
        /// </summary>
        public string smallPicUrl
        {
            set { _smallPicUrl = value; }
            get { return _smallPicUrl; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime addTime
        {
            set { _addTime = value; }
            get { return _addTime; }
        }
        /// <summary>
        /// 0正常  1禁用
        /// </summary>
        public int isLock
        {
            set { _isLock = value; }
            get { return _isLock; }
        }
    }
    #endregion Model
}
