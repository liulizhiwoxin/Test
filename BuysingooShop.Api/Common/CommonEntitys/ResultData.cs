using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuysingooShop.Api.Common.CommonEntitys
{
    public class ResultData<T>
    {
        /// <summary>
        /// 200为正确返回，300为未登录，400为错误返回
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误说明， code！=200时有返回，
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 正确返回时的返回数据
        /// </summary>
        public T Result { get; set; }
    }
}