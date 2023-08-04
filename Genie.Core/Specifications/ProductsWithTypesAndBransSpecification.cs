using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Genie.Core.Entities;

namespace Genie.Core.Specifications
{
    public class ProductsWithTypesAndBransSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBransSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBransSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}