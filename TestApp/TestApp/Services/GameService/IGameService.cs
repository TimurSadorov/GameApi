using TestApp.Dto;
using TestApp.Entities;

namespace TestApp.Services.GameService;

public interface IGameService
{ 
    Task<GameDto?> GetGameByIdAsync(int id);
    List<GameDto> GetGames(string? genreName);
    Task<NewGameDto> AddGameAsync(GameDto gameDto);
    Task<GameDto?> UpdateGameAsync(int id, GameDto newGameInfo);
    Task<bool> DeleteGameAsync(int id);
}