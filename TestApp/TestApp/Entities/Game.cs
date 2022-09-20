using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace TestApp.Entities;

public class Game: BaseEntity
{
    [Required]
    public string Name { get; set; }
    public List<Genre> Genres { get; set; }
    public DeveloperStudio? DeveloperStudio { get; set; }
}