using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Vincent;

namespace BuysingooShop.BLL
{
    public partial class siteconfig
    {
        private readonly DAL.siteconfig dal = new DAL.siteconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.siteconfig loadConfig()
        {
            Model.siteconfig model = _Cache.Get<Model.siteconfig>(Vincent._DTcms.DTKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                _Cache.Insert(Vincent._DTcms.DTKeys.CACHE_SITE_CONFIG, dal.loadConfig(Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_SITE_XML_CONFING)),
                    Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_SITE_XML_CONFING));
                model = _Cache.Get<Model.siteconfig>(Vincent._DTcms.DTKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.siteconfig saveConifg(Model.siteconfig model)
        {
            return dal.saveConifg(model, Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_SITE_XML_CONFING));
        }

    }
}
