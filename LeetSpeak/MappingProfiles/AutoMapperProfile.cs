using AutoMapper;
using LeetSpeak.Models;
using LeetSpeak.Data;

namespace LeetSpeak.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<TranslationViewModel, Translation>();
        }
    }
}
