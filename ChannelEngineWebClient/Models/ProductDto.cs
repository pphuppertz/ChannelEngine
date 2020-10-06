using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChannelEngineWebClient.Models
{
    public class ProductDto
    {
        public string Description { get; set; }
        public string MerchantProductNumber { get; set; }
        public string  Gti { get; set; }
        public int QuantityInStock { get; set; }
    }
}