using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Common;

namespace DAL
{
    class PoolContext : ObjectContext
    {
        ObjectSet<Item> m_Items;
        ObjectSet<Order> m_Orders;
        ObjectSet<ItemsToOrder> m_ItemsToOrder;

        public PoolContext()
            : base("name=PoolClubDBEntities", "PoolClubDBEntities")
        {
            m_Items = CreateObjectSet<Item>();
            m_Orders = CreateObjectSet<Order>();
            m_ItemsToOrder = CreateObjectSet<ItemsToOrder>();
        }

        public ObjectSet<Item> Items
        {
            get
            {
                return m_Items;
            }
        }

        public ObjectSet<Order> Orders
        {
            get
            {
                return m_Orders;
            }
        }

        
        public ObjectSet<ItemsToOrder> ItemsToOrders
        {
            get
            {
                return m_ItemsToOrder;
            }
        }
    }
}
