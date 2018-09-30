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
    public partial class brand
    {
        //无参构造函数
        public brand() { }

        #region 基本属性
        private int _id;
        private string _brandName;
        private string _brandImgUrl;
        private string _remark;
        private int _isLock = 1;
        private int _managerId = 0;
        private int _sort_id = 99;
        private DateTime _add_time = DateTime.Now;
        private DateTime? _update_time;

        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string brandName
        {
            set { _brandName = value; }
            get { return _brandName; }
        }
        /// <summary>
        /// 品牌Logo
        /// </summary>
        public string brandImgUrl
        {
            set { _brandImgUrl = value; }
            get { return _brandImgUrl; }
        }
        /// <summary>
        /// 品牌简介
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        /// 供应商用户ID
        /// </summary>
        public int managerId
        {
            set { _managerId = value; }
            get { return _managerId; }
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
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? update_time
        {
            set { _update_time = value; }
            get { return _update_time; }
        } 
        #endregion

        #region 扩展属性
        private List<brand_attach> _brand_attach;
        /// <summary>
        /// 品牌相册
        /// </summary>
        public List<brand_attach> brand_attach
        {
            set { _brand_attach = value; }
            get { return _brand_attach; }
        } 
        #endregion
    }
    #endregion Model
}
