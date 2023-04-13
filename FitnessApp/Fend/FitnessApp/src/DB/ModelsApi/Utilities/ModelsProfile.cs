using AutoMapper;
using ModelsApi.Models.DTOs;
using ModelsApi.Models.Entities;

namespace ModelsApi.Utilities
{
    // Read: Getting Started with AutoMapper in ASP.NET Core
    // at https://code-maze.com/automapper-net-core/
    //
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            // CreateMap<FromType, ToType>();
            CreateMap<ModelDetails, EfModel>();
            CreateMap<EfModel, ModelDetails>();
            CreateMap<Job, EfJob>();
            CreateMap<EfJob, Job>();
            CreateMap<EfModel, Model>();
            CreateMap<NewJob, EfJob>();
            CreateMap<NewExpense, EfExpense>();
        }
    }
}
