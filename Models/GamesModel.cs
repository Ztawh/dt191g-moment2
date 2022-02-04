using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace dt191g_moment2_MVC.Models;

public class GamesModel
{
    public int? id { get; set; }
    
    [Display(Name = "Titel")]
    [Required(ErrorMessage = "Ange en titel")]
    public string? title { get; set; }
    
    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Ange en genre")]
    public string? type { get; set; }
    
    [Display(Name = "Pris")]
    [Required(ErrorMessage = "Ange ett pris")]
    public int? price { get; set; }

    [Display(Name = "Barnvänligt")]
    public bool kidFriendly { get; set; }

}