﻿using HomeService.Domain.Core.Dtos.Orders;
using HomeService.Domain.Core.Entities.Orders;
using HomeService.Infrastructure.EfCore.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HomeService.Infrastructure.EfCore.Repository.Orders;

public class OrderEfRepository(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<bool> Create(CreateOrderDto order, CancellationToken cancellationToken)
    {
        try
        {
            var item = new Order
            {
                TimeToDone = order.TimeToDone,
                CustomerId = order.CustomerId,
                CreateAt = DateTime.Now,
                Description = order.Description,
                ImagePath = order.ImagePath,
                Price = order.Price,
                IsActive = true,
                Status = Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertOffer,
                SubServiceId = order.SubServiceId,
            };
            await _dbContext.Orders.AddAsync(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return true;

        }
        catch
        {
            return false;
        }
    }
    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
        if (item is null)
            return false;
        item.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<List<GetOrderDto>> GetForExpert(int cityId, int subserviceId, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Orders.AsNoTracking()
            .Where(o => o.IsActive && o.Customer.CityId == cityId &&
            o.SubServiceId == subserviceId &&
            (o.Status == Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertOffer ||
            o.Status == Domain.Core.Enums.Orders.OrderStatusEnum.WaitingForExpertSelection))
            .Select(o => new GetOrderDto
            {
                Id = o.Id,
                CreateAt = DateTime.UtcNow,
                CustomerId = o.CustomerId,
                CustomerLname = o.Customer.Lname,
                Description = o.Description,
                ImagePath = o.ImagePath,
                Price = o.Price,
                TimeToDone = o.TimeToDone
            }).ToListAsync(cancellationToken);
        return item;


    }
   
}
