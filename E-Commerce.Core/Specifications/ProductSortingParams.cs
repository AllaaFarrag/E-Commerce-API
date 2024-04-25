using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductSortingParams
    {
        NameAsc , NameDesc , PriceAsc , PriceDesc
    }
}
