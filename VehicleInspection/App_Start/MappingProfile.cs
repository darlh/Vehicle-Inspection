using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleInspection.Models;
using VehicleInspection.DTOs;

namespace VehicleInspection.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Inspection, InspectionDto>();
            Mapper.CreateMap<InspectionDto, Inspection>();

            Mapper.CreateMap<Vehicle, VehicleDto>();
        }
    }
}