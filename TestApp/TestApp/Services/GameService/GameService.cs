using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApp.Dto;
using TestApp.Entities;
using TestApp.Repositories;
using TestApp.Specifications;

namespace TestApp.Services.GameService;

public class GameService : IGameService
{
    private readonly IRepository<Game> _gameRepository;
    private readonly IRepository<Genre> _genreRepository;
    private readonly IRepository<DeveloperStudio> _developerStudioRepository;
    private readonly IMapper _mapper;

    public GameService(IRepository<Game> gameRepository, IMapper mapper, IRepository<Genre> genreRepository,
        IRepository<DeveloperStudio> developerStudioRepository)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _genreRepository = genreRepository;
        _developerStudioRepository = developerStudioRepository;
    }

    public async Task<GameDto?> GetGameByIdAsync(int id)
    {
        var game = await GetFullInfoAboutGameAsync(id);

        return game is not null
            ? _mapper.Map<GameDto>(game)
            : null;
    }

    public List<GameDto> GetGames(string? genreName)
    {
        var gamesQuery = IncludeGenreAndStudio(_gameRepository.Query);
        if (!string.IsNullOrEmpty(genreName))
        {
            gamesQuery = gamesQuery.Where(new GameByGenre(genreName));
        }

        var games = gamesQuery.ToList();
        return _mapper.Map<List<GameDto>>(games);
    }

    public async Task<NewGameDto> AddGameAsync(GameDto gameDto)
    {
        var game = _mapper.Map<Game>(gameDto);
        await MapGenresAndStudioToNewGameAsync(game, gameDto);

        var newGame = await _gameRepository.AddAsync(game);
        await _gameRepository.SaveChangesAsync();

        return _mapper.Map<NewGameDto>(newGame);
    }

    public async Task<GameDto?> UpdateGameAsync(int id, GameDto newGameInfo)
    {
        var game = await GetFullInfoAboutGameAsync(id);
        if (game is null)
        {
            return null;
        }

        game.Name = newGameInfo.Name;
        await MapGenresAndStudioToNewGameAsync(game, newGameInfo);
        
        _gameRepository.Update(game);
        await _gameRepository.SaveChangesAsync();

        return _mapper.Map<GameDto>(game);
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        var game = await _gameRepository.Query
            .FirstOrDefaultAsync(new EntityByIdSpec<Game>(id));
        if (game is null)
        {
            return false;
        }

        _gameRepository.Delete(game);
        await _gameRepository.SaveChangesAsync();
        return true;
    }
    
    private async Task<Game?> GetFullInfoAboutGameAsync(int id)
    {
        return await IncludeGenreAndStudio(_gameRepository.Query)
            .FirstOrDefaultAsync(new EntityByIdSpec<Game>(id));
    }

    private IQueryable<Game> IncludeGenreAndStudio(IQueryable<Game> query)
    {
        return query
            .Include(game => game.Genres)
            .Include(game => game.DeveloperStudio);
    }

    private async Task MapGenresAndStudioToNewGameAsync(Game newGame, GameDto gameDto)
    {
        var genres = _genreRepository.Query
            .Where(new GenreByNames(gameDto.GenresName))
            .ToList();
        newGame.Genres = genres;

        var studio = await _developerStudioRepository.Query
                .FirstOrDefaultAsync(new DeveloperStudioByName(gameDto.DeveloperStudioName));
        newGame.DeveloperStudio = studio;
    }
}