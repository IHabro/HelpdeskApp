using DataLayer.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public enum NotificationType
    {
        Email,
        SMS
    }

    [Table("Actions")]
    [PrimaryKey(nameof(Id))]
    public class EscalationAction
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public required string Name { get; set; }

        [Required]
        public required NotificationType NotificationType { get; set; }

        public List<EscalationLevel> Levels { get; set; } = new List<EscalationLevel>();
        public List<HelpdeskUser> Users { get; set; } = new List<HelpdeskUser>();
        public List<ActionToProjectToRole> ActionOnProjectAndRole { get; set; } = new List<ActionToProjectToRole>();

        public EscalationAction() { }

        public override string ToString()
        {
            return $"Action [{Id}, {Name}, {NotificationType}]";
        }
    }
}
