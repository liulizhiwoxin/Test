using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Vincent;

namespace BuysingooShop.BLL
{
    public partial class orderconfig
    {
        private readonly DAL.orderconfig dal = new DAL.orderconfig();
        /// <summary>
        ///  读取用户配置文件
        /// </summary>
        public Model.orderconfig loadConfig()
        {
            Model.orderconfig model = _Cache.Get<Model.orderconfig>(Vincent._DTcms.DTKeys.CACHE_ORDER_CONFIG);
            if (model == null)
            {
                _Cache.Insert(Vincent._DTcms.DTKeys.CACHE_ORDER_CONFIG, dal.loadConfig(Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_ORDER_XML_CONFING)),
                    Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_ORDER_XML_CONFING));
                model = _Cache.Get<Model.orderconfig>(Vincent._DTcms.DTKeys.CACHE_ORDER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存用户配置文件
        /// </summary>
        public Model.orderconfig saveConifg(Model.orderconfig model)
        {
            return dal.saveConifg(model, Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_ORDER_XML_CONFING));
        }
    }
}
