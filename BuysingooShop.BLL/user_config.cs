using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using Vincent;

namespace BuysingooShop.BLL
{
    public partial class userconfig
    {
        private readonly DAL.userconfig dal = new DAL.userconfig();

        /// <summary>
        ///  读取用户配置文件
        /// </summary>
        public Model.userconfig loadConfig()
        {
            Model.userconfig model = _Cache.Get<Model.userconfig>(Vincent._DTcms.DTKeys.CACHE_USER_CONFIG);
            if (model == null)
            {
                _Cache.Insert(Vincent._DTcms.DTKeys.CACHE_USER_CONFIG, dal.loadConfig(Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_USER_XML_CONFING)),
                    Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_USER_XML_CONFING));
                model = _Cache.Get<Model.userconfig>(Vincent._DTcms.DTKeys.CACHE_USER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存用户配置文件
        /// </summary>
        public Model.userconfig saveConifg(Model.userconfig model)
        {
            return dal.saveConifg(model, Vincent._DTcms.Utils.GetXmlMapPath(Vincent._DTcms.DTKeys.FILE_USER_XML_CONFING));
        }

    }
}
