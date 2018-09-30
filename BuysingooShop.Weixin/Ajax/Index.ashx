<%@ WebHandler Language="C#" Class="Index" %>

using System;
using System.Web;

public class Index : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string param = Vincent._Request.GetString("param", "");
        switch (param)
        {
            case "GetAddressByLngb":
                GetAddressByLngb();             //根据经纬度查询出地名
                break;
            case "Getgeoconv":
                Getgeoconv();//转换坐标
                break;

            case "WXJSSDK_config":
                WXJSSDK_config();               //获取微信JS-SDK配置参数
                break;


        }

    }

    /// <summary>
    /// 根据经纬度查询出地名
    /// </summary>
    public void GetAddressByLngb()
    {
        //例http://api.map.baidu.com/geocoder/v2/?ak=E4805d16520de693a3fe707cdc962045&callback=&location=39.983424,116.322987&output=json&pois=0
        string json = "";
        //lat<纬度>,lng<经度> 
        var lng = Vincent._Request.GetString("lng");   //经度
        var lat = Vincent._Request.GetString("lat");   //纬度
        var ak = Vincent._WebConfig.GetAppSettingsString("ak");
        var Url = Vincent._WebConfig.GetAppSettingsString("GetAddressByLngb_Url");
        var postDataStr = "ak=" + ak + "&callback=&location=" + lat + "," + lng + "&output=json&pois=0";

        json = Vincent._WebHttp.HttpGet(Url, postDataStr);

        //记录日志
        Vincent._Log.SaveMessage("经纬度查询出地名：" + (Url + postDataStr) + "\r\n" + json);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(json);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 转换坐标
    /// </summary>
    public void Getgeoconv()
    {
        //例http://api.map.baidu.com/geocoder/v2/?ak=E4805d16520de693a3fe707cdc962045&callback=&location=39.983424,116.322987&output=json&pois=0
        string json = "";
        //lat<纬度>,lng<经度> 
        var lng = Vincent._Request.GetString("lng");   //经度
        var lat = Vincent._Request.GetString("lat");   //纬度
        var ak = Vincent._WebConfig.GetAppSettingsString("ak");
        var Url = "http://api.map.baidu.com/geoconv/v1/?";
        //var postDataStr = "ak=" + ak + "&callback=&location=" + lat + "," + lng + "&output=json&pois=0";
        var postDataStr = "coords=" + lng + "," + lat + "&from=1&to=5&ak=52rS70vDROKs1uGdwgGLQmy8";

        json = Vincent._WebHttp.HttpGet(Url, postDataStr);

        //记录日志
        Vincent._Log.SaveMessage("经纬度查询出地名：" + (Url + postDataStr) + "\r\n" + json);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(postDataStr);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 获取微信JS-SDK配置参数
    /// </summary>
    /// <param name="context"></param>
    public void WXJSSDK_config()
    {
        Vincent._Log.SaveMessage("Index.ashx->WXJSSDK_config()启动");

        var appid = Vincent._WebConfig.GetAppSettingsString("sAppID");
        var url = Vincent._Request.GetString("url");
        //var timestamp = Vincent._Request.GetString("timestamp");
        //var nonceStr = Vincent._Request.GetString("nonceStr");

        Vincent._Weixin.WeixinUtility wxUtility = new Vincent._Weixin.WeixinUtility("sAppID", "sAppSecret");


        //获取access_token
        string access_token = wxUtility.IsExistAccess_Token();

        string timestamp = "";  // Vincent._Weixin.WeixinUtility.GenerateTimeStamp();
        string nonceStr = "";   // Vincent._Weixin.WeixinUtility.GenerateNonceStr();
        string jsapi_ticket = wxUtility.IsExistJsapiTicket(out timestamp, out nonceStr);

        //Vincent._Log4Net.Info("signature=" + signature);

        string string1 = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url;

        Vincent._Log.SaveMessage("string1=" + string1);

        System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        byte[] str1 = System.Text.Encoding.UTF8.GetBytes(string1);
        byte[] str2 = sha1.ComputeHash(str1);
        sha1.Clear();
        (sha1 as IDisposable).Dispose();
        string signature = System.BitConverter.ToString(str2).Replace("-", ""); //转换成为字符串的显示

        Vincent._Log.SaveMessage("signature=" + signature);

        var json = "{\"appId\":\"" + appid + "\",\"timestamp\":\"" + timestamp + "\",\"nonceStr\":\"" + nonceStr + "\",\"signature\":\"" + signature.ToLower() + "\",\"success\":\"1\"}";

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(json);
        HttpContext.Current.Response.End();

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}