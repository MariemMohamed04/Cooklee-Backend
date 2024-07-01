using Cooklee.Data.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShipmentDetails, ShipmentDetails => ShipmentDetails.WithOwner());
            builder.Property(O => O.Status).HasConversion
             (
                OrderStatus => OrderStatus.ToString(),
                OrderStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OrderStatus)
             );
            builder.Property(O => O.SubTotal).HasColumnType("float");
        }
    }
}
