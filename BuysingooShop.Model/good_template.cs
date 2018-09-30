using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuysingooShop.Model
{
    #region 模板实体类============================================
    /// <summary>
    /// article:实体类
    /// </summary>
    [Serializable]
    public partial class good_template
    {
        //无参构造函数
        public good_template() { }

        private int _id;
        private int _goodId=0;
        private int _categoryId=0;
        private string _name;
        private string _remark;
        private int _sort_id = 99;
        private DateTime _addTime = DateTime.Now;
        private int _isLock = 1;
        private int _isDefault = 0;
        private string _img_url;
        private int _isAd = 0;
        private int _sort_ad = 0;
        private string _img_url1;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 商品ID
        /// </summary>
        public int goodId
        {
            set { _goodId = value; }
            get { return _goodId; }
        }
        /// <summary>
        /// 主题分类ID
        /// </summary>
        public int categoryId
        {
            set { _categoryId = value; }
            get { return _categoryId; }
        }
        /// <summary>
        /// 模板名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
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
        public DateTime addTime
        {
            set { _addTime = value; }
            get { return _addTime; }
        }
        /// <summary>
        /// 1正常  2禁用
        /// </summary>
        public int isLock
        {
            set { _isLock = value; }
            get { return _isLock; }
        }
        /// <summary>
        /// 0非默认 1默认   一类主题下，只允许默认一个模板
        /// </summary>
        public int isDefault
        {
            set { _isDefault = value; }
            get { return _isDefault; }
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
        /// 是否为广告位
        /// </summary>
        public int isAd
        {
            set { _isAd = value; }
            get { return _isAd; }
        }
        /// <summary>
        /// 广告位位置
        /// </summary>
        public int sort_ad
        {
            set { _sort_ad = value; }
            get { return _sort_ad; }
        }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string img_url1
        {
            set { _img_url1 = value; }
            get { return _img_url1; }
        }
        /// <summary>
        /// 模板图片
        /// </summary>
        private List<good_template_pic> _pics;
        public List<good_template_pic> pics
        {
            set { _pics = value; }
            get { return _pics; }
        }
    }
    #endregion Model
}
