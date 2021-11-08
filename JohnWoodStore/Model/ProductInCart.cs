using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.Model.Database;

namespace JohnWoodStore.Model
{
    class ProductInCart
    {
        private int _id { get; set; }
        public string Name { get; private set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public ProductInCart(Product product, int count)
        {
            _id = product.Id;
            Name = product.Name;
            var category = FindCategory(product.CategoryId);
            CategoryName = category.Name;
            Price = product.Price * count;
            Count = count;
        }

        public int GetId()
        {
            return _id;
        }

        private Category FindCategory(int categoryId)
        {
            using (StoreContext db = new StoreContext())
            {
                var allCategories = db.Categories.ToList();
                return allCategories.Find(x => x.Id == categoryId);
            }
        }
    }
}
