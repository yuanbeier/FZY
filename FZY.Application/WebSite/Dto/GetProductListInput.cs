using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FZY.Page;

namespace FZY.WebSite.Dto
{
    /// <summary>
    /// /
    /// </summary>
    public class GetProductListInput:BasePageInput
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 产品类别Id
        /// </summary>
        public int? CategoryId { set; get; }
    }
}
