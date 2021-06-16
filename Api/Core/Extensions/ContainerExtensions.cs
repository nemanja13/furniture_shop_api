using Api.Core.FakeActors;
using Api.Core.Jwt;
using Application;
using Application.Commands.CategoryCommands;
using Application.Commands.ColorCommands;
using Application.Commands.CommentCommands;
using Application.Commands.MaterialCommands;
using Application.Commands.OrderCommands;
using Application.Commands.ProductCommands;
using Application.Commands.RatingCommands;
using Application.Commands.UserCommands;
using Application.Logger;
using Application.Queries.CategoryQueries;
using Application.Queries.ColorQueries;
using Application.Queries.CommentQueries;
using Application.Queries.LogQueries;
using Application.Queries.MaterialQueries;
using Application.Queries.OrderQueries;
using Application.Queries.ProductQueries;
using Application.Queries.RatingQueries;
using Application.Queries.UserQueries;
using Implementation.Commands.CategoryCommands;
using Implementation.Commands.ColorCommands;
using Implementation.Commands.CommentCommands;
using Implementation.Commands.MaterialCommands;
using Implementation.Commands.OrderCommands;
using Implementation.Commands.ProductCommands;
using Implementation.Commands.RatingCommands;
using Implementation.Commands.UserCommands;
using Implementation.Logging;
using Implementation.Queries.CategoryQueries;
using Implementation.Queries.ColorQueries;
using Implementation.Queries.CommentQueries;
using Implementation.Queries.LogQueries;
using Implementation.Queries.MaterialQueries;
using Implementation.Queries.OrderQueries;
using Implementation.Queries.ProductQueries;
using Implementation.Queries.RatingQueries;
using Implementation.Queries.UserQueries;
using Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Extensions
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<IGetCategoriesQuery, EFGetCategoriesQuery>();
            services.AddTransient<IGetCategoryQuery, EFGetCategoryQuery>();
            services.AddTransient<ICreateCategoryCommand, EFCreateCategoryCommand>();
            services.AddTransient<IDeleteCategoryCommand, EFDeleteCategoryCommand>();
            services.AddTransient<IUpdateCategoryCommand, EFUpdateCategoryCommand>();

            services.AddTransient<IGetColorsQuery, EFGetColorsQuery>();
            services.AddTransient<IGetColorQuery, EFGetColorQuery>();
            services.AddTransient<ICreateColorCommand, EFCreateColorCommand>();
            services.AddTransient<IDeleteColorCommand, EFDeleteColorCommand>();
            services.AddTransient<IUpdateColorCommand, EFUpdateColorCommand>();

            services.AddTransient<IGetMaterialsQuery, EFGetMaterialsQuery>();
            services.AddTransient<IGetMaterialQuery, EFGetMaterialQuery>();
            services.AddTransient<ICreateMaterialCommand, EFCreateMaterialCommand>();
            services.AddTransient<IDeleteMaterialCommand, EFDeleteMaterialCommand>();
            services.AddTransient<IUpdateMaterialCommand, EFUpdateMaterialCommand>();

            services.AddTransient<IGetProductsQuery, EFGetProductsQuery>();
            services.AddTransient<IGetProductQuery, EFGetProductQuery>();
            services.AddTransient<ICreateProductCommand, EFCreateProductCommand>();
            services.AddTransient<IDeleteProductCommand, EFDeleteProductCommand>();
            services.AddTransient<IUpdateProductCommand, EFUpdateProductCommand>();

            services.AddTransient<IGetUsersQuery, EFGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EFGetUserQuery>();
            services.AddTransient<ICreateUserCommand, EFCreateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EFDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EFUpdateUserCommand>();
            services.AddTransient<IUpdateUserAdminCommand, EFUpdateUserAdminCommand>();

            services.AddTransient<IGetCommentQuery, EFGetCommentQuery>();
            services.AddTransient<ICreateCommentCommand, EFCreateCommentCommand>();
            services.AddTransient<IDeleteCommentCommand, EFDeleteCommentCommand>();
            services.AddTransient<IUpdateCommentCommand, EFUpdateCommentCommand>();

            services.AddTransient<IGetRatingQuery, EFGetRatingQuery>();
            services.AddTransient<ICreateRatingCommand, EFCreateRatingCommand>();
            services.AddTransient<IUpdateRatingCommand, EFUpdateRatingCommand>();

            services.AddTransient<IGetOrdersQuery, EFGetOrdersQuery>();
            services.AddTransient<IGetOrderQuery, EFGetOrderQuery>();
            services.AddTransient<ICreateOrderCommand, EFCreateOrderCommand>();
            services.AddTransient<IUpdateOrderCommand, EFUpdateOrderCommand>();

            services.AddTransient<IGetLogsQuery, EFGetLogsQuery>();
            services.AddTransient<IUseCaseLogger, DatabaseUseCaseLogger>();

            services.AddTransient<UseCaseExecutor>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<CreateCategoryValidator>();
            services.AddTransient<UpdateCategoryValidator>();

            services.AddTransient<CreateColorValidator>();
            services.AddTransient<UpdateColorValidator>();

            services.AddTransient<CreateMaterialValidator>();
            services.AddTransient<UpdateMaterialValidator>();

            services.AddTransient<CreateProductValidator>();
            services.AddTransient<UpdateProductValidator>();

            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();

            services.AddTransient<CreateCommentValidator>();
            services.AddTransient<UpdateCommentValidator>();

            services.AddTransient<CreateRatingValidator>();
            services.AddTransient<UpdateRatingValidator>();

            services.AddTransient<CreateOrderValidator>();

            services.AddTransient<UserLoginRequestValidator>();
            services.AddTransient<UpdateUserAdminValidator>();
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new UnauthorizedActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;

            });
        }

        public static void AddJwt(this IServiceCollection services)
        {
            services.AddTransient<JwtManager>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "asp_api",
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMyVerySecretKey")),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
