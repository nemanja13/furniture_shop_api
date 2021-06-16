using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakerController : ControllerBase
    {
        private readonly ProjekatASPContext _context;
        private readonly string password = "f1dc735ee3581693489eaf286088b916"; // sifra123
        private readonly List<int> useCases = new List<int> { 7, 8, 9, 13, 18, 19, 20, 22, 27, 32, 33, 35 };
        private readonly List<int> adminUseCases = Enumerable.Range(1, 40).ToList();

        public FakerController(ProjekatASPContext context)
        {
            _context = context;
        }

        // POST: api/Faker
        [HttpPost]
        public IActionResult Post()
        {
            var categoryNames = new List<string> { "Sofa", "Bed", "Chair", "Desk", "Lamp", "Shelve", "Wardrobe" };
            var materialNames = new List<string> { "Wood", "Leather", "Cloth", "Metal", "Plastic", "Steel" };
            var colorNames = new List<string> { "Red", "Green", "Blue", "Black", "White", "Gray", "Orange", "Yellow", "Brown" };
            var productImages = new List<string[]>
            {
                new string[] 
                { 
                    "sofa.png",
                    "sofa2.png"
                },
                new string[]
                {
                    "bed.png",
                    "bed2.png"
                },
                new string[]
                {
                    "chair.png",
                    "chair2.png"
                },
                new string[]
                {
                    "desk.png",
                    "desk2.png"
                },
                new string[]
                {
                    "lamp.png",
                    "lamp2.png"
                },
                new string[]
                {
                    "shelve.png",
                    "shelve2.png",
                    "shelve3.png"
                },
                new string[]
                {
                    "wardrobe.png",
                    "wardrobe2.png",
                    "wardrobe3.png"
                }
            }; 
            
            var categories = categoryNames.Select(c => new Category { Name = c });
            var materials = materialNames.Select(c => new Material { Name = c });
            var colors = colorNames.Select(c => new Color { Name = c });

            _context.Categories.AddRange(categories);
            _context.Materials.AddRange(materials);
            _context.Colors.AddRange(colors);

            _context.SaveChanges();

            var categoryIds = _context.Categories.Select(x => x.Id).ToList();
            var materialIds = _context.Materials.Select(x => x.Id).ToList();
            var colorIds = _context.Colors.Select(x => x.Id).ToList();

            var usersFaker = new Faker<User>();

            usersFaker.RuleFor(x => x.FirstName, f => f.Name.FirstName());
            usersFaker.RuleFor(x => x.LastName, f => f.Name.LastName());
            usersFaker.RuleFor(x => x.Email, f => f.Internet.Email());
            usersFaker.RuleFor(x => x.Phone, f => f.Phone.PhoneNumber());
            usersFaker.RuleFor(x => x.Password, f => password); 

            var users = usersFaker.Generate(10);

            var admins = new List<User>
            {
                new User
                {
                    FirstName = "Pera",
                    LastName = "Peric",
                    Email = "pera@gmail.com",
                    Phone = "064 44 33 223",
                    Password = password
                },
                new User
                {
                    FirstName = "Mika",
                    LastName = "Mikic",
                    Email = "mika@gmail.com",
                    Phone = "064 66 55 223",
                    Password = password
                }
            };

            _context.Users.AddRange(admins);
            _context.Users.AddRange(users);

            foreach(var user in users)
            {
                foreach(var useCaseId in useCases)
                {
                    _context.UserUseCases.Add(new UserUseCase
                    {
                        User = user,
                        UseCaseId = useCaseId
                    });
                }
            }

            foreach(var admin in admins)
            {
                adminUseCases.ForEach(useCaseId =>
                {
                    _context.UserUseCases.Add(new UserUseCase
                    {
                        User = admin,
                        UseCaseId = useCaseId
                    });
                });
            }

            var priceFaker = new Faker<Price>();

            priceFaker.RuleFor(x => x.SalePrice, f => f.Random.Decimal(10, 1000));

            var materialProductFaker = new Faker<MaterialProduct>();

            materialProductFaker.RuleFor(x => x.MaterialId, f => f.PickRandom(materialIds));

            var colorProductFaker = new Faker<ColorProduct>();

            colorProductFaker.RuleFor(x => x.ColorId, f => f.PickRandom(colorIds));


            var productsFaker = new Faker<Product>();

            productsFaker.RuleFor(x => x.Name, f => f.Commerce.ProductName());
            productsFaker.RuleFor(x => x.FullName, f => f.Commerce.ProductDescription());
            productsFaker.RuleFor(x => x.Description, f => f.Lorem.Text());
            productsFaker.RuleFor(x => x.Quantity, f => f.Random.Int(0, 1000));
            productsFaker.RuleFor(x => x.CategoryId, f => f.PickRandom(categoryIds));
            productsFaker.RuleFor(x => x.ProductMaterials, f => materialProductFaker.Generate(1));
            productsFaker.RuleFor(x => x.ProductColors, f => colorProductFaker.Generate(1));
            productsFaker.RuleFor(x => x.CategoryId, f => f.PickRandom(categoryIds));
            productsFaker.RuleFor(x => x.Image, (f, x) => f.PickRandom(productImages.ElementAt(Array.IndexOf(categoryNames.ToArray(), _context.Categories.Find(x.CategoryId).Name))));
            productsFaker.RuleFor(x => x.Dimensions, f => $"{f.Random.Int(20, 200)} x {f.Random.Int(20, 200)} x {f.Random.Int(20, 200)}");
            productsFaker.RuleFor(x => x.Prices, f => priceFaker.Generate(f.Random.Int(1,2)));

            var products = productsFaker.Generate(50);

            _context.Products.AddRange(products);


            var ratingFaker = new Faker<Rating>();

            ratingFaker.RuleFor(x => x.User, f => f.PickRandom(users));
            ratingFaker.RuleFor(x => x.Product, f => f.PickRandom(products));
            ratingFaker.RuleFor(x => x.Mark, f => f.Random.Int(1,5));

            var ratings = ratingFaker.Generate(120);

            _context.Ratings.AddRange(ratings);

            var commentFaker = new Faker<Comment>();

            commentFaker.RuleFor(x => x.CommentText, f => f.Lorem.Text());
            commentFaker.RuleFor(x => x.Product, f => f.PickRandom(products));
            commentFaker.RuleFor(x => x.User, f => f.PickRandom(users));

            var comments = commentFaker.Generate(30);

            _context.Comments.AddRange(comments);

            var orderLineFaker = new Faker<OrderLine>();

            orderLineFaker.RuleFor(x => x.Product, f => f.PickRandom(products));
            orderLineFaker.RuleFor(x => x.Quantity, f => f.Random.Int(10, 100));
            orderLineFaker.RuleFor(x => x.Name, (f, x) => x.Product.Name);
            orderLineFaker.RuleFor(x => x.Price, (f, x) => x.Product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.SalePrice).First());

            var orderFaker = new Faker<Order>();

            orderFaker.RuleFor(x => x.User, f => f.PickRandom(users));
            orderFaker.RuleFor(x => x.OrderDate, f => f.Date.Past(3, DateTime.Now));
            orderFaker.RuleFor(x => x.Address, f => f.Address.FullAddress());
            orderFaker.RuleFor(x => x.OrderStatus, f => f.PickRandom(Enum.GetValues(typeof(OrderStatus)).GetValue(f.Random.Int(0, 3))));
            orderFaker.RuleFor(x => x.PaymentMethod, f => f.PickRandom(Enum.GetValues(typeof(PaymentMethod)).GetValue(f.Random.Int(0, 2))));
            orderFaker.RuleFor(x => x.OrderLines, f => orderLineFaker.Generate(f.Random.Int(1, 5)));

            var orders = orderFaker.Generate(20);

            _context.Orders.AddRange(orders);

            _context.SaveChanges();


            return Ok();
        }
    }
}
