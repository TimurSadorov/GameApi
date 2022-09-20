using Microsoft.AspNetCore.Mvc;
using TestApp.Dto;
using TestApp.Entities;
using TestApp.Services.GameService;

namespace TestApp.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [ActionName(nameof(GetGameByIdAsync))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<GameDto>> GetGameByIdAsync(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game is null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpGet]
    public ActionResult<List<GameDto>> GetGames(string? genreName)
    {
        var games = _gameService.GetGames(genreName);

        return Ok(games);
    }

    [HttpPost]
    public async Task<ActionResult<Game>> AddGameAsync([FromBody] GameDto game)
    {
        var newGame = await _gameService.AddGameAsync(game);
        return CreatedAtAction(nameof(GetGameByIdAsync), new {id = newGame.Id}, newGame);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<GameDto>> UpdateGameAsync([FromBody] GameDto gameDto, int id)
    {
        var updatedGame = await _gameService.UpdateGameAsync(id, gameDto);
        if (updatedGame is null)
        {
            return NotFound();
        }

        return Ok(updatedGame);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteGameAsync(int id)
    {
        var isDeleted = await _gameService.DeleteGameAsync(id);

        if (isDeleted)
        {
            return NoContent();
        }

        return NotFound();
    }
}