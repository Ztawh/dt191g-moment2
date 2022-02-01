using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace dt191g_moment2_MVC.Models;

public class GamesModel
{
    public List<string>? allGames { get; set; }

    public int? id { get; set; }
    
    [Required]
    public string? title { get; set; }
    [Required]
    public string? type { get; set; }
    [Required]
    public int? price { get; set; }
    
}