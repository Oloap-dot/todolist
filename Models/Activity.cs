using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace todolist.Models
{
    [Index(nameof(Titolo), IsUnique = true)] //to take the titolo and make it unique
    [Index(nameof(Descrizione), IsUnique = true)]
    public class Activity
    {
        public Int16 Id {get; set;}

        [Required(ErrorMessage = "Necessario inserire il titolo dell'attività")]
        [StringLength(maximumLength:200, MinimumLength = 2)]
        public string Titolo {get; set;} = string.Empty;

        [Required(ErrorMessage = "Necessario inserire la descrizione dell'attività")]
        [StringLength(maximumLength: 1000, MinimumLength = 2)]
        public string Descrizione {get; set;} = string.Empty;

        [Required(ErrorMessage = "Necessario inserire l'assegnatario")]
        [StringLength(maximumLength:100, MinimumLength = 2)]
        public string Assegnatario {get; set;} = string.Empty;

        [StringLength(maximumLength:50)]
        public string StatoString {get; set;} = string.Empty;
        public ActivityState Stato {get; set;}
        public DateTime? DataInizio { get; set;}
        public DateTime? DataFine {get; set;}

    }
}