using System;
using System.Collections.Generic;
using System.Text;
using ApiWithbasicAuthentication.Domain.Model;
using AutoMapper;

namespace ApiWithbasicAuthentication.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDetail>(); // means you want to map from User to UserDTO


            CreateMap<Student, StudentDetail>()
       .ForMember(dest =>
           dest.StudentName,
           opt => opt.MapFrom(src => src.StudentName))
       .ForMember(dest =>
           dest.StudentID,
           opt => opt.MapFrom(src => src.StudentID))
        .ForMember(dest =>
           dest.Weight,
           opt => opt.MapFrom(src => src.Weight))
         .ForMember(dest =>
           dest.Height,
           opt => opt.MapFrom(src => src.Height))
          .ForMember(dest =>
           dest.DateOfBirth,
           opt => opt.MapFrom(src => src.DateOfBirth))
           .ForMember(dest =>
           dest.GradeId,
           opt => opt.MapFrom(src => src.GradeID))
            .ForMember(dest =>
           dest.GradeName,
           opt => opt.MapFrom(src => src.Grade.GradeName))
             .ForMember(dest =>
           dest.Section,
           opt => opt.MapFrom(src => src.Grade.Section));

            //  Mapper.DynamicMap(Student, StudentDetail);
        }
    }

}
