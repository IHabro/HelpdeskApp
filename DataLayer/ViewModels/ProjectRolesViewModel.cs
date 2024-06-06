using DataLayer.Models;

namespace DataLayer.ViewModels
{
    public class ProjectRolesViewModel
    {
        public IEnumerable<ProjectRole> Included { get; set; }
        public IEnumerable<ProjectRole> Excluded { get; set; }

        public ProjectRolesViewModel() { }
    }
}
