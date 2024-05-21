using Microsoft.EntityFrameworkCore;

namespace DataLayer.Models
{
    [PrimaryKey(nameof(Action_Fk), nameof(Role_Fk), nameof(Project_Fk))]
    public class ActionToProjectToRole
    {
        public int Action_Fk { get; set; }
        public EscalationAction Action { get; set; } = null!;

        public int Role_Fk { get; set; }
        public ProjectRole Role { get; set; } = null!;

        public int Project_Fk { get; set; }
        public Project Project { get; set; } = null!;

        public override string ToString()
        {
            return $"UserToProjectToRole [FirstName: {Action.Name}, RoleName: {Role.Name}, Project: {Project.Name}]";
        }
    }
}
