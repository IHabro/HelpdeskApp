using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HelpdeskUser class
public class HelpdeskUser : IdentityUser
{
    [Required]
    [Column(TypeName = "nvarchar(127)")]
    public required string FirtName { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(127)")]
    public required string LastName { get; set; }

    public List<Incident> Incidents { get; set; } = new List<Incident>();
    public List<EscalationAction> Actions { get; set; } = new List<EscalationAction>();

    // One part, RolesInProjects is a join entity, and Projeects and Roles should be for navigation to remove join request later
    public List<UserToProjectToRole> RolesInProjects { get; set; } = new List<UserToProjectToRole>();

    public HelpdeskUser() { }

    public override string ToString()
    {
        return $"User [{Id}, {FirtName}, {LastName}]";
    }
}
