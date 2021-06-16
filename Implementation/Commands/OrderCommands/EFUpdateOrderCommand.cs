using Application.Commands.OrderCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.OrderCommands
{
    public class EFUpdateOrderCommand : IUpdateOrderCommand
    {
        private readonly ProjekatASPContext _context;

        public EFUpdateOrderCommand(ProjekatASPContext context)
        {
            _context = context;
        }

        public int Id => 14;

        public string Name => "Updating Order using Entity Framework";

        public void Execute(ChangeOrderStatusDto request)
        {
            var order = _context.Orders.Include(o => o.OrderLines).ThenInclude(ol => ol.Product).FirstOrDefault(x => x.Id == request.OrderId);

            if (order == null)
            {
                throw new EntityNotFoundException(request.OrderId, typeof(Order));
            }

            if (order.OrderStatus == OrderStatus.Completed)
            {
                throw new ConflictException("Can not change status of delivered order.");
            }

            if (order.OrderStatus == OrderStatus.Canceled)
            {
                throw new ConflictException("Can not change status of canceled order.");
            }

            if (order.OrderStatus == OrderStatus.Hold || order.OrderStatus == OrderStatus.Shipped)
            {
                if(request.Status == OrderStatus.Canceled || request.Status == OrderStatus.Shipped)
                {
                    order.OrderStatus = request.Status;

                    if(request.Status == OrderStatus.Canceled)
                    {
                        foreach(var orderLine in order.OrderLines)
                        {
                            orderLine.Product.Quantity += orderLine.Quantity;
                        }
                    }
                }
                if(order.OrderStatus == OrderStatus.Hold && request.Status == OrderStatus.Completed)
                {
                    throw new ConflictException("Order can not be transitioned from hold to completed directly.");
                }
                if (order.OrderStatus == OrderStatus.Shipped && request.Status == OrderStatus.Completed)
                {
                    order.OrderStatus = request.Status;
                }
            }

            _context.SaveChanges();

        }
    }
}
