using JohnWoodStore.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohnWoodStore.Model
{
    class SelectedProduct
    {
        private int _id { get; set; }
        public string Name { get; private set; }
        public string CategoryName { get; set; }
        private decimal _price { get; set; }
        public int Count { get; set; }

        public SelectedProduct(Product product)
        {
            _id = product.Id;
            Name = product.Name;
            var category = FindCategory(product.CategoryId);
            CategoryName = category.Name;
            _price = product.Price;
            Count = 1;
        }

        private Category FindCategory(int categoryId)
        {
            using (StoreContext db = new StoreContext())
            {
                var allCategories = db.Categories.ToList();
                return allCategories.Find(x => x.Id == categoryId);
            }
        }

        public int GetId()
        {
            return _id;
        }

        public decimal GetPrice()
        {
            return _price;
        }
    }
}
