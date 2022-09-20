using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace TestApp.Entities;

public class DeveloperStudio: BaseEntity
{
    [Required]
    public string Name { get; set; }
    public List<Game> Games { get; set; }
}