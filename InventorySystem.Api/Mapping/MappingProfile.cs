using AutoMapper;
using InventorySystem.Api.Dtos;
using InventorySystem.Api.Dtos.Account;
using InventorySystem.Api.Dtos.ProductDetails;
using InventorySystem.Api.Dtos.User;
using InventorySystem.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace InventorySystem.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            CreateMap<User, RegisterDto>();
            CreateMap<User, LoginDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<User, CreateUserDto>();
            CreateMap<User, AccountUpdateDto>();
            CreateMap<User, OwnerDto>();
            CreateMap<ProductDetails, GetProductDetailsDto>();
            CreateMap<GetProductDetailsDto, ProductDetails>();


            // Dto to Domain
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<GetUserDto, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<AccountUpdateDto, User>();
            CreateMap<CreateProductDetailsDto, ProductDetails>();
        }
    }
}
