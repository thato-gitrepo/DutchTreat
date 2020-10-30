using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            //Map between Order & ViewModel
            CreateMap<Order, OrderViewModel>()
                //Map order id from the source id
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                //Reverse mapping to the opposite direction
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
