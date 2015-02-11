using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Paladyne.Angularjs.BL.Models;
using Paladyne.Angularjs.DAL.Entities;

namespace Paladyne.Angularjs.BL
{
    public static class AutoMapperConfig
    {
        public class NullStringConverter : ITypeConverter<string, string>
        {
            public string Convert(ResolutionContext context)
            {
                return context.SourceValue as string ?? string.Empty;
            }
        }

        public static void RegisterMappers()
        {
            Mapper.CreateMap<User, CreateUser>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id));
            Mapper.CreateMap<CreateUser, User>()
                .ForMember(x => x.UserModules, x => x.MapFrom(y => y.Modules));

            Mapper.CreateMap<CreateUser.UserModule, UserModule>();

            Mapper.CreateMap<UpdateUserModule, UserModule>();

            Mapper.CreateMap<UpdateUserData, User>();
            Mapper.CreateMap<UpdateUserData.UserModule, UserModule>();
        }
    }
}
