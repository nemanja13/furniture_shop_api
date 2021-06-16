using Application;
using Application.Commands.OrderCommands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Commands.OrderCommands
{
    public class EFCreateOrderCommand : ICreateOrderCommand
    {
        private readonly ProjekatASPContext _context;
        private readonly CreateOrderValidator _validator;
        private readonly IApplicationActor _actor;
        private readonly IMapper _mapper;

        public EFCreateOrderCommand(ProjekatASPContext context, CreateOrderValidator validator, IMapper mapper, IApplicationActor actor)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _actor = actor;
        }

        public int Id => 13;

        public string Name => "Creating New Order using EntityFramework";

        public void Execute(OrderDto request)
        {
            request.UserId = _actor.Id;
            _validator.ValidateAndThrow(request);

            var order = _mapper.Map<Order>(request);

            foreach (var item in request.OrderLines)
            {
                var product = _context.Products.Include(p => p.Prices).FirstOrDefault(p => p.Id == item.ProductId);

                if (product == null)
                    throw new EntityNotFoundException(item.ProductId.Value, typeof(Product));

                product.Quantity -= item.Quantity;
                _context.OrderLines.Add(new OrderLine
                {
                    Name = product.Name,
                    Quantity = item.Quantity,
                    Price = product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.SalePrice).First(),
                    Product = product,
                    Order = order
                });
            }

         
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
