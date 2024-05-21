using DataLayer.Areas.Identity.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Incidents")]
    [PrimaryKey(nameof(Id))]
    public class Incident
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string CodeName { get; set; }

        public string Description { get; set; }

        [Required]
        public string TargettedSystem { get; set; }

        [Required]
        public DateTime DateOfOcurence { get; set; }

        // FK + navigation reference (null! signifies required navigation property)
        // Microsoft.Identity User.Id is string...
        public string User_Fk { get; set; }
        public HelpdeskUser User { get; set; } = null!;

        // FK + navigation reference, both not null
        public int Project_Fk { get; set; }
        public Project Project { get; set; } = null!;

        // FK + navigation reference, both not null
        public int Level_Fk { get; set; }
        public EscalationLevel Level { get; set; } = null!;


        public Incident() { }

        public override string ToString()
        {
            return $"Incident [{Id}, {CodeName}, {Description}, {TargettedSystem}, {DateOfOcurence}]";
        }
    }
}
