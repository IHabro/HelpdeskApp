using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Roles")]
    [PrimaryKey(nameof(Id))]
    public class ProjectRole
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public required string Name { get; set; }

        public required string Description { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();

        public List<UserToProjectToRole> UsersInProjects { get; set; } = new List<UserToProjectToRole>();
        public List<ActionToProjectToRole> ActionForProjectAndRole { get; set; } = new List<ActionToProjectToRole>();

        public ProjectRole() { }

        public override string ToString()
        {
            return $"Role [{Id}, {Name}, {Description}]";
        }
    }
}
