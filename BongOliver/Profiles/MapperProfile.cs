using AutoMapper;
using BongOliver.DTOs.Booking;
using BongOliver.DTOs.Hair;
using BongOliver.DTOs.Notification;
using BongOliver.DTOs.Order;
using BongOliver.DTOs.Product;
using BongOliver.DTOs.Rate;
using BongOliver.DTOs.Service;
using BongOliver.DTOs.User;
using BongOliver.Models;

namespace BongOliver.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();

            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
            CreateMap<Booking, BookingDetailDTO>();
            CreateMap<BookingDetailDTO, Booking>();

            CreateMap<Booking, UpdateBookingDTO>();
            CreateMap<UpdateBookingDTO, Booking>();

            CreateMap<Service, ServiceDTO>();
            CreateMap<ServiceDTO, Service>();

            CreateMap<Service, CreateServiceDTO>();
            CreateMap<CreateServiceDTO, Service>();

            CreateMap<ServiceType, ServiceTypeDTO>();
            CreateMap<ServiceTypeDTO, ServiceType>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();

            CreateMap<Order, OrderDetailDTO>();
            CreateMap<OrderDetailDTO, Order>();

            CreateMap<Rate, RateDTO>();
            CreateMap<RateDTO, Rate>();

            CreateMap<Rate, CreateRateDTO>();
            CreateMap<CreateRateDTO, Rate>();

            CreateMap<Notification, NotificationDTO>();
            CreateMap<NotificationDTO, Notification>();

            CreateMap<HairStyle, HairStyleDTO>();
            CreateMap<HairStyleDTO, HairStyle>();
        }
    }
}
