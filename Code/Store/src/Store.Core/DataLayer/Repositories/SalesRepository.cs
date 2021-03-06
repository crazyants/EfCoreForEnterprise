using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Core.DataLayer.Contracts;
using Store.Core.DataLayer.DataContracts;
using Store.Core.EntityLayer.HumanResources;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Repositories
{
    public class SalesRepository : Repository, ISalesRepository
    {
        public SalesRepository(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IQueryable<Customer> GetCustomers(Int32 pageSize, Int32 pageNumber)
        {
            return Paging<Customer>(pageSize, pageNumber);
        }

        public async Task<Customer> GetCustomerAsync(Customer entity)
        {
            return await DbContext
                .Set<Customer>()
                .FirstOrDefaultAsync(item => item.CustomerID == entity.CustomerID);
        }

        public async Task<Int32> AddCustomerAsync(Customer entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<Int32> UpdateCustomerAsync(Customer changes)
        {
            var entity = await GetCustomerAsync(changes);

            if (entity != null)
            {
                entity.CompanyName = changes.CompanyName;
                entity.ContactName = changes.ContactName;
            }

            return await CommitChangesAsync();
        }

        public async Task<Int32> DeleteCustomerAsync(Customer entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public IQueryable<OrderInfo> GetOrders(Int32 pageSize, Int32 pageNumber, Int32? customerID = null, Int32? employeeID = null, Int32? shipperID = null)
        {
            var query = from order in DbContext.Set<Order>()
                        join orderStatus in DbContext.Set<OrderStatus>() on order.OrderStatusID equals orderStatus.OrderStatusID
                        join customer in DbContext.Set<Customer>() on order.CustomerID equals customer.CustomerID
                        join employee in DbContext.Set<Employee>() on order.EmployeeID equals employee.EmployeeID
                        join shipper in DbContext.Set<Shipper>() on order.ShipperID equals shipper.ShipperID
                        select new OrderInfo
                        {
                            OrderID = order.OrderID,
                            OrderStatusID = order.OrderStatusID,
                            OrderStatusDescription = orderStatus.Description,
                            OrderDate = order.OrderDate,
                            CustomerID = order.CustomerID,
                            CustomerName = customer.CompanyName,
                            EmployeeID = order.EmployeeID,
                            EmployeeName = employee.FirstName + " " + employee.MiddleName + " " + employee.LastName,
                            ShipperID = order.ShipperID,
                            ShipperName = shipper.CompanyName,
                            Total = order.Total,
                            Comments = order.Comments,
                            CreationUser = order.CreationUser,
                            CreationDateTime = order.CreationDateTime,
                            LastUpdateUser = order.LastUpdateUser,
                            LastUpdateDateTime = order.LastUpdateDateTime
                        };

            if (customerID.HasValue)
            {
                query = query.Where(item => item.CustomerID == customerID);
            }

            if (employeeID.HasValue)
            {
                query = query.Where(item => item.EmployeeID == employeeID);
            }

            if (shipperID.HasValue)
            {
                query = query.Where(item => item.ShipperID == shipperID);
            }

            return Paging(query, pageSize, pageNumber);
        }

        public async Task<Order> GetOrderAsync(Order entity)
        {
            return await DbContext
                .Set<Order>()
                .Include(p => p.OrderDetails)
                .FirstOrDefaultAsync(item => item.OrderID == entity.OrderID);
        }

        public Task<Int32> AddOrderAsync(Order entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public async Task<Int32> UpdateOrderAsync(Order changes)
        {
            var entity = await GetOrderAsync(changes);

            if (entity != null)
            {
                entity.OrderDate = changes.OrderDate;
                entity.CustomerID = changes.CustomerID;
                entity.EmployeeID = changes.EmployeeID;
                entity.ShipperID = changes.ShipperID;
                entity.Total = changes.Total;
                entity.Comments = changes.Comments;

                Update(entity);
            }

            return await CommitChangesAsync();
        }

        public async Task<Int32> DeleteOrderAsync(Order entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public async Task<OrderDetail> GetOrderDetailAsync(OrderDetail entity)
        {
            return await DbContext
                .Set<OrderDetail>()
                .FirstOrDefaultAsync(item => item.OrderID == entity.OrderID && item.ProductID == entity.ProductID);
        }

        public Task<Int32> AddOrderDetailAsync(OrderDetail entity)
        {
            Add(entity);

            return CommitChangesAsync();
        }

        public async Task<Int32> UpdateOrderDetailAsync(OrderDetail changes)
        {
            var entity = await GetOrderDetailAsync(changes);

            if (entity != null)
            {
                entity.ProductID = changes.ProductID;
                entity.ProductName = changes.ProductName;
                entity.UnitPrice = changes.UnitPrice;
                entity.Quantity = changes.Quantity;
                entity.Total = changes.Total;
            }

            return await CommitChangesAsync();
        }

        public async Task<Int32> DeleteOrderDetailAsync(OrderDetail entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }

        public IQueryable<Shipper> GetShippers(Int32 pageSize, Int32 pageNumber)
        {
            return Paging<Shipper>(pageSize, pageNumber);
        }

        public async Task<Shipper> GetShipperAsync(Shipper entity)
        {
            return await DbContext
                .Set<Shipper>()
                .FirstOrDefaultAsync(item => item.ShipperID == entity.ShipperID);
        }

        public async Task<Int32> AddShipperAsync(Shipper entity)
        {
            Add(entity);

            return await CommitChangesAsync();
        }

        public async Task<Int32> UpdateShipperAsync(Shipper changes)
        {
            var entity = await GetShipperAsync(changes);

            if (entity != null)
            {
                entity.CompanyName = changes.CompanyName;
                entity.ContactName = changes.ContactName;
            }

            return await CommitChangesAsync();
        }

        public async Task<Int32> DeleteShipperAsync(Shipper entity)
        {
            Remove(entity);

            return await CommitChangesAsync();
        }
    }
}
