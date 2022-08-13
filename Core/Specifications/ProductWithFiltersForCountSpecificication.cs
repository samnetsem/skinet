using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecificication :BaseSpecifications<Product>
    {
        public ProductWithFiltersForCountSpecificication(ProductSpecParams productParms )
         : base(x=>
              (string.IsNullOrEmpty(productParms.Search) || x.Name.ToLower().Contains
                (productParms.Search)) &&
                (!productParms.BrandId.HasValue || x.ProductBrandId == productParms.BrandId) &&
                (!productParms.TypeId.HasValue || x.ProductTypeId ==productParms.TypeId)
               )  
        {
        }
    }
}