using System.Collections.Generic;
using Store.Core.DataLayer.Mapping.HumanResources;
using Store.Core.DataLayer.Mapping.Production;
using Store.Core.DataLayer.Mapping.Sales;

namespace Store.Core.DataLayer.Mapping
{
    public class StoreEntityMapper : EntityMapper
    {
        public StoreEntityMapper()
        {
            Mappings = new List<IEntityMap>()
            {
                new ChangeLogMap(),
                new EventLogMap(),
                new ProductMap(),
                new ProductCategoryMap(),
                new ProductInventoryMap(),
                new CustomerMap(),
                new EmployeeMap(),
                new ShipperMap(),
                new OrderStatusMap(),
                new OrderMap(),
                new OrderDetailMap(),
                new OrderSummaryMap()
            };
        }
    }
}
