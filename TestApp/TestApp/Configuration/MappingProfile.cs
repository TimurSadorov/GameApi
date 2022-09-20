using AutoMapper;
using TestApp.Dto;
using TestApp.Entities;

namespace TestApp.Configuration;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMapForBaseGameDto<GameDto>().ReverseMap();
        CreateMapForBaseGameDto<NewGameDto>();
    }

    private IMappingExpression<Game, TGameDto> CreateMapForBaseGameDto<TGameDto>()
        where TGameDto: GameDto
    {
        return CreateMap<Game, TGameDto>()
            .ForMember(dto => dto.GenresName,
                memberOptions => 
                    memberOptions.MapFrom(game => game.Genres.Select(genre => genre.Name)));
    }
}