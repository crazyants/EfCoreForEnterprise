using System;
using System.Linq;
using System.Threading.Tasks;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Contracts
{
    public interface IProductionRepository : IRepository
    {
        IQueryable<Product> GetProducts(Int32 pageSize = 0, Int32 pageNumber = 0, Int32? productCategoryID = null);

        Task<Product> GetProductAsync(Product entity);

        Product GetProductByName(String productName);

        void AddProduct(Product entity);

        Task<Int32> UpdateProductAsync(Product changes);

        void DeleteProduct(Product entity);

        IQueryable<ProductCategory> GetProductCategories();

        ProductCategory GetProductCategory(ProductCategory entity);

        void AddProductCategory(ProductCategory entity);

        void UpdateProductCategory(ProductCategory changes);

        void DeleteProductCategory(ProductCategory entity);

        IQueryable<ProductInventory> GetProductInventories();

        ProductInventory GetProductInventory(ProductInventory entity);

        Task<Int32> AddProductInventoryAsync(ProductInventory entity);

        Task<Int32> UpdateProductInventoryAsync(ProductInventory changes);

        void DeleteProductInventory(ProductInventory entity);
    }
}
