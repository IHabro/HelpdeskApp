using DataLayer.Areas.Identity.Data;
using DataLayer.Models;

namespace DataLayer.ViewModels
{
    public class ActionUsersViewModel
    {
        public EscalationAction Action { get; set; }
        public IEnumerable<HelpdeskUser> UsersToNotify { get; set; }
        public IEnumerable<HelpdeskUser> NotificationCandidates { get; set; }

        public ActionUsersViewModel() { }
    }
}
