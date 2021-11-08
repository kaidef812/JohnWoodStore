using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohnWoodStore.Model.Database;

namespace JohnWoodStore.Model
{
    class ProductInDataGrid
    {
        private int _id { get; set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }
        public string CategoryName { get; private set; }

        public ProductInDataGrid(Product product)
        {
            _id = product.Id;
            Name = product.Name;
            Price = Math.Round(product.Price, 2);
            Description = product.Description;
            var category = FindCategory(product.CategoryId);
            CategoryName = category.Name;
        }

        public int GetId()
        {
            return _id;
        }

        private Category FindCategory(int categoryId)
        {
            using(StoreContext db = new StoreContext())
            {
                var allCategories = db.Categories.ToList();
                return allCategories.Find(x => x.Id == categoryId);
            }
        }
    }
}
