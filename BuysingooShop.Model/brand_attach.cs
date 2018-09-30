using System;
namespace BuysingooShop.Model
{
	/// <summary>
	/// brand_attach:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class brand_attach
	{
		public brand_attach()
		{}
		#region Model
		private int _id;
		private int _brand_id;
		private int _theme_id;
		private string _size;
		private string _img_url;
		private string _remark;
        private string _small_imgurl;
        private DateTime _add_time = DateTime.Now;

		/// <summary>
		/// 主键ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// 品牌ID
		/// </summary>
		public int brand_id
		{
			set{ _brand_id=value;}
			get{return _brand_id;}
		}
		/// <summary>
		/// 主题ID
		/// </summary>
		public int theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		/// <summary>
		/// 图片尺寸
		/// </summary>
		public string size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 原图片路径
		/// </summary>
		public string img_url
		{
			set{ _img_url=value;}
			get{return _img_url;}
		}
		/// <summary>
		/// 图片说明
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// 略缩图路径
        /// </summary>
        public string small_imgurl
        {
            set { _small_imgurl = value; }
            get { return _small_imgurl; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
		#endregion Model

	}
}

