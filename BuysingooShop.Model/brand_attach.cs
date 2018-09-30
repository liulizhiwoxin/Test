using System;
namespace BuysingooShop.Model
{
	/// <summary>
	/// brand_attach:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ����ID
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// Ʒ��ID
		/// </summary>
		public int brand_id
		{
			set{ _brand_id=value;}
			get{return _brand_id;}
		}
		/// <summary>
		/// ����ID
		/// </summary>
		public int theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		/// <summary>
		/// ͼƬ�ߴ�
		/// </summary>
		public string size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// ԭͼƬ·��
		/// </summary>
		public string img_url
		{
			set{ _img_url=value;}
			get{return _img_url;}
		}
		/// <summary>
		/// ͼƬ˵��
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// ����ͼ·��
        /// </summary>
        public string small_imgurl
        {
            set { _small_imgurl = value; }
            get { return _small_imgurl; }
        }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
		#endregion Model

	}
}

