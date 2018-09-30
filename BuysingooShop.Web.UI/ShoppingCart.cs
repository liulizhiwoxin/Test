using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Vincent;
using LitJson;
using System.Web;

namespace BuysingooShop.Web.UI
{
    /// <summary>
    /// 购物车帮助类
    /// </summary>
    public partial class ShopCart
    {
        #region 基本增删改方法====================================
        /// <summary>
        /// 获得购物车列表
        /// </summary>
        public static IList<Model.cart_items> GetList(int group_id)
        {
            IDictionary<string, string> dic = GetCart();
            if (dic != null)
            {
                IList<Model.cart_items> iList = new List<Model.cart_items>();
                Model.cart_total totalmodel = new Model.cart_total();

                foreach (var item in dic)
                {
                    BLL.article bll = new BLL.article();
                    Model.article model = bll.GetModel(Convert.ToInt32(item.Key));

                    if (model == null || !model.fields.ContainsKey("sell_price"))
                    {
                        continue;
                    }
                    string[] strArray = item.Value.Split('|');
                    Model.cart_items modelt = new Model.cart_items();
                    modelt.id = model.id;
                    modelt.title = model.title;
                    //modelt.img_url = model.img_url;
                    //获取产品图片
                    DataTable dt = new BLL.article_albums().GetList(1, " article_id=" + int.Parse((item.Key).ToString()) + " and category_id=1", " id").Tables[0];
                    var img = "";
                    if (dt.Rows.Count > 0)
                    {
                        img = dt.Rows[0]["original_path"].ToString();
                        modelt.img_url = img;
                    }
                    else
                    {
                        modelt.img_url = model.img_url;
                    }


                    modelt.quantity = strArray[0];
                    modelt.type = strArray[1];
                    modelt.weight = strArray[2];
                    modelt.price = decimal.Parse(strArray[3]);
                    if (strArray[1] == "智能家居")
                    {
                        modelt.total_price = decimal.Parse(modelt.quantity) * modelt.price;
                    }
                    else
                    {
                        modelt.total_price = decimal.Parse(modelt.quantity) * modelt.price * decimal.Parse(modelt.weight);
                    }

                    totalmodel.payable_amount += modelt.total_price;
                    totalmodel.total_point += modelt.point;

                    if (model.fields.ContainsKey("point"))
                    {
                        modelt.point = Vincent._DTcms.Utils.StrToInt(model.fields["point"], 0);
                    }
                    modelt.user_price = Vincent._DTcms.Utils.StrToDecimal(model.fields["sell_price"], 0);
                    if (model.fields.ContainsKey("stock_quantity"))
                    {
                        modelt.stock_quantity = Vincent._DTcms.Utils.StrToInt(model.fields["stock_quantity"], 0);
                    }
                    //会员价格
                    if (model.group_price != null)
                    {
                        Model.user_group_price gmodel = model.group_price.Find(p => p.group_id == group_id);
                        if (gmodel != null)
                        {
                            modelt.user_price = gmodel.price;
                        }
                    }
                    iList.Add(modelt);
                }
                return iList;
            }
            return null;
        }
        /// <summary>
        /// 获得购物车列表
        /// </summary>
        public static IList<Model.cart_items> GetList2(string str)
        {
            IDictionary<string, string> dic = GetCart();
            if (dic != null)
            {
                IList<Model.cart_items> iList = new List<Model.cart_items>();
                Model.cart_total totalmodel = new Model.cart_total();

                foreach (var item in dic)
                {
                    if (str.Contains(item.Key))
                    {
                        BLL.article bll = new BLL.article();
                        Model.article model = bll.GetModel(Convert.ToInt32(item.Key));
                        if (model == null || !model.fields.ContainsKey("sell_price"))
                        {
                            continue;
                        }
                        string[] strArray = item.Value.Split('|');
                        Model.cart_items modelt = new Model.cart_items();
                        modelt.id = model.id;
                        modelt.title = model.title;
                        //modelt.img_url = model.img_url;
                        //获取产品图片
                        DataTable dt = new BLL.article_albums().GetList(1, " article_id=" + int.Parse((item.Key).ToString()) + " and category_id=1", " id").Tables[0];
                        var img = "";
                        if (dt.Rows.Count > 0)
                        {
                            img = dt.Rows[0]["original_path"].ToString();
                            modelt.img_url = img;
                        }
                        else
                        {
                            modelt.img_url = model.img_url;
                        }

                        modelt.quantity = strArray[0];
                        modelt.type = strArray[1];
                        modelt.weight = strArray[2];
                        modelt.price = decimal.Parse(strArray[3]);
                        if (strArray[1] == "智能家居")
                        {
                            modelt.total_price = decimal.Parse(modelt.quantity) * modelt.price;
                        }
                        else
                        {
                            modelt.total_price = decimal.Parse(modelt.quantity) * modelt.price * decimal.Parse(modelt.weight);
                        }

                        totalmodel.payable_amount += modelt.total_price;
                        totalmodel.total_point += modelt.point;

                        if (model.fields.ContainsKey("point"))
                        {
                            modelt.point = Vincent._DTcms.Utils.StrToInt(model.fields["point"], 0);
                        }
                        modelt.user_price = Vincent._DTcms.Utils.StrToDecimal(model.fields["sell_price"], 0);
                        if (model.fields.ContainsKey("stock_quantity"))
                        {
                            modelt.stock_quantity = Vincent._DTcms.Utils.StrToInt(model.fields["stock_quantity"], 0);
                        }
                        ////会员价格
                        //if (model.group_price != null)
                        //{
                        //    Model.user_group_price gmodel = model.group_price.Find(p => p.group_id == group_id);
                        //    if (gmodel != null)
                        //    {
                        //        modelt.user_price = gmodel.price;
                        //    }
                        //}
                        iList.Add(modelt);
                    }
                }
                return iList;
            }
            return null;
        }

        /// <summary>
        /// 获得收藏夹列表
        /// </summary>
        public static List<Model.collect_items> GetCollectsList()
        {
            List<Model.collect_items> list = new List<Model.collect_items>();
            IDictionary<string, string> dic = GetCollect();
            //string collects = Vincent._DTcms.Utils.GetCookie("cookie_favorite_cart");
            //dic = JsonMapper.ToObject<Dictionary<string, string>>(collects);

            if (dic!=null)
            {
                foreach (var item in dic)
                {
                    if (item.Key != null)
                    {
                        string[] array = item.Value.Split('|');
                        Model.collect_items modelt = new Model.collect_items();
                        modelt.id = int.Parse(item.Key);
                        modelt.type = array[0];
                        modelt.title = array[1];
                        modelt.price = array[2];
                        modelt.url_img = array[3];
                        list.Add(modelt);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        public static bool AddCart(string Key, string Quantity)
        {
            IDictionary<string, string> dic = GetCart();
            if (dic != null)
            {
                if (dic.ContainsKey(Key))
                {
                    dic[Key] = Quantity;
                    AddCookies(JsonMapper.ToJson(dic));
                    return true;
                }
            }
            else
            {
                dic = new Dictionary<string, string>();
            }
            //不存在的则新增
            dic.Add(Key, Quantity);
            AddCookies(JsonMapper.ToJson(dic));
            return true;
        }

        /// <summary>
        /// 添加到收藏夹
        /// </summary>
        /// <param name="Key">商品ID</param>
        /// <param name="goodType">商品类型</param>
        /// <returns></returns>
        public static bool AddfavoriteCart(string Key,string goodInfo)
        {
            IDictionary<string, string> dic = GetCollect();
            if (dic != null)
            {
                if (dic.ContainsKey(Key))
                {
                    dic[Key] = goodInfo;
                    AddCollectCookies(JsonMapper.ToJson(dic));
                    return true;
                }
            }
            else
            {
                dic = new Dictionary<string, string>();
            }
            //不存在的则新增
            dic.Add(Key, goodInfo);
            AddCollectCookies(JsonMapper.ToJson(dic));
            return true;
            //IDictionary<string, string> dic = new Dictionary<string, string>();
            //string collectCart = Vincent._DTcms.Utils.GetCookie("cookie_favorite_cart");
            
            //if (dic.Count>0)
            //{
            //    dic = JsonMapper.ToObject<Dictionary<string, string>>(collectCart);
            //    if (!collectCart.Contains(Key))
            //    {
            //        dic[Key] = goodInfo;
            //        AddCookies(JsonMapper.ToJson(dic));
            //        Vincent._DTcms.Utils.WriteCookie("cookie_favorite_cart", Key);
            //        return true;
            //    }
            //    return false;
            //}
            //else
            //{
            //    dic.Add(Key, goodInfo);
            //    Vincent._DTcms.Utils.WriteCookie("cookie_favorite_cart", JsonMapper.ToJson(dic));
            //    return true;
            //}

        }
        /// <summary>
        /// 更新购物车数量
        /// </summary>
        public static bool UpdateCart(string Key, string Quantity)
        {
            
            if (Quantity == "")
            {
                //Clear(Key);
                return true;
            }
            IDictionary<string, string> dic = GetCart();
            if (dic != null && dic.ContainsKey(Key))
            {
                dic[Key] = Quantity;
                AddCookies(JsonMapper.ToJson(dic));
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除购物车
        /// </summary>
        /// <param name="Key">主键 0为清理所有的购物车信息</param>
        public static void ClearCart(string Key)
        {
            if (Key == "0")//为0的时候清理全部购物车cookies
            {
                Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_SHOPPING_CART, "", -43200);
            }
            else
            {
                IDictionary<string, string> dic = GetCart();
                if (dic != null)
                {
                    dic.Remove(Key);
                    AddCookies(JsonMapper.ToJson(dic));
                }
            }
        }
        #endregion

        /// <summary>
        /// 移除收藏夹
        /// </summary>
        /// <param name="Key">主键 0为清理所有的购物车信息</param>
        public static void ClearCollect(string Key)
        {
            if (Key == "0")//为0的时候清理全部购物车cookies
            {
                Vincent._DTcms.Utils.WriteCookie("cookie_favorite_cart", "", -43200);
            }
            else
            {
                IDictionary<string, string> dic = GetCollect();
                if (dic != null)
                {
                    dic.Remove(Key);
                    AddCollectCookies(JsonMapper.ToJson(dic));
                }
            }
        }

        #region 扩展方法==========================================
        public static Model.cart_total GetTotal(int group_id)
        {
            Model.cart_total model = new Model.cart_total();
            IList<Model.cart_items> iList = GetList(group_id);
            if (iList != null)
            {
                foreach (Model.cart_items modelt in iList)
                {
                    //model.total_num++;
                    //model.total_quantity += modelt.quantity;
                    //model.payable_amount += modelt.price * modelt.quantity;
                    //model.real_amount += modelt.user_price * modelt.quantity;
                    //model.total_point += modelt.point * modelt.quantity;
                }
            }
            return model;
        }
        #endregion

        #region 私有方法==========================================
        /// <summary>
        /// 获取cookies值
        /// </summary>
        private static IDictionary<string, string> GetCart()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(GetCookies()))
            {
                return JsonMapper.ToObject<Dictionary<string, string>>(GetCookies());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取收藏cookies值
        /// </summary>
        /// <returns></returns>
        private static IDictionary<string, string> GetCollect()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(GetCollectCookies()))
            {
                return JsonMapper.ToObject<Dictionary<string, string>>(GetCollectCookies());
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加对象到cookies
        /// </summary>
        /// <param name="strValue"></param>
        private static void AddCookies(string strValue)
        {
            Vincent._DTcms.Utils.WriteCookie(Vincent._DTcms.DTKeys.COOKIE_SHOPPING_CART, strValue, 43200); //存储一个月
        }

        /// <summary>
        /// 添加对象到收藏cookies
        /// </summary>
        /// <param name="strValue"></param>
        private static void AddCollectCookies(string strValue)
        {
            Vincent._DTcms.Utils.WriteCookie("cookie_favorite_cart", strValue, 43200); //存储一个月
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCookies()
        {
            return Vincent._DTcms.Utils.GetCookie(Vincent._DTcms.DTKeys.COOKIE_SHOPPING_CART);
        }

        /// <summary>
        /// 获取收藏cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCollectCookies()
        {
            return Vincent._DTcms.Utils.GetCookie("cookie_favorite_cart");
        }



        #endregion
    }

}
