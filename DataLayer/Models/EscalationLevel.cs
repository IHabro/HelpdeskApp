using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Levels")]
    [PrimaryKey(nameof(Id))]
    public class EscalationLevel
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Incident> Incidents { get; set; } = new List<Incident>();
        public List<EscalationAction> Actions { get; set; } = new List<EscalationAction>();


        public EscalationLevel() { }

        public override string ToString()
        {
            return $"Level [{Id}, {Name}, {Description}]";
        }
    }
}
