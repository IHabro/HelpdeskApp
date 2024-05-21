using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    [Table("Projects")]
    [PrimaryKey(nameof(Id))]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public required string Name { get; set; }

        public List<Incident> Incidents { get; set; } = new List<Incident>();
        public List<ProjectRole> Roles { get; set; } = new List<ProjectRole>();
        public List<UserToProjectToRole> UsersAndRoles { get; set; } = new List<UserToProjectToRole>();
        public List<ActionToProjectToRole> ActionOnRoleInProject { get; set; } = new List<ActionToProjectToRole>();

        public Project() { }

        public override string ToString()
        {
            return $"Project [{Id}, {Name}]";
        }
    }
}
