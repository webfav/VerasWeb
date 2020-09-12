using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VerasWeb.Models
{
    public class Covid19Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]       
        public string Id { get; set; }

        [Required(ErrorMessage = "Indtast dato")]
        [DataType(DataType.Date)]
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Indtast CPR-nummer")]
        //[RegularExpression(@"^\d{6}\[0-9][A-Za-z]{4}$", ErrorMessage = "Dansk CPR-nr skal have 10 cifre med fødselsdato som de første 6 tal.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Dansk CPR-nr skal have 10 cifre.")]
        [JsonProperty(PropertyName = "cpr")]
        public string CPR { get; set; }

        [Required(ErrorMessage = "Indtast navn")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        
        [Display(Name = "Er smittet med Covid19?")]
        [JsonProperty(PropertyName = "isInfected")]
        public bool Smittet { get; set; }

    }
}
