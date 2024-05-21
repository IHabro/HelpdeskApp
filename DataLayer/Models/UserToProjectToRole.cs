using DataLayer.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models
{
    [PrimaryKey(nameof(User_Fk), nameof(Role_Fk), nameof(Project_Fk))]
    public class UserToProjectToRole
    {
        public string User_Fk { get; set; }
        public HelpdeskUser User { get; set; } = null!;

        public int Role_Fk { get; set; }
        public ProjectRole Role { get; set; } = null!;

        public int Project_Fk { get; set; }
        public Project Project { get; set; } = null!;

        public DateTime GrantedOn { get; set; }

        public override string ToString()
        {
            return $"UserToProjectToRole [FirstName: {User.FirtName}, RoleName: {Role.Name}, Project: {Project.Name}, Granted: {GrantedOn}]";
        }
    }
}
