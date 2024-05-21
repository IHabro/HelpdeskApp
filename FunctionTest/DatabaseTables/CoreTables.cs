using DataLayer.Areas.Identity.Data;
using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;

namespace FunctionTest.DatabaseTables
{
    public class CoreTables
    {
        public static void FillIncidents(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill Incident Table--------------------");

            try
            {
                // Incident UnitOfWork ???
                GeneralRepository<HelpdeskUser> userRepository = new GeneralRepository<HelpdeskUser>(context);
                GeneralRepository<Project> projectRepository = new GeneralRepository<Project>(context);
                GeneralRepository<EscalationLevel> levelRepository = new GeneralRepository<EscalationLevel>(context);
                GeneralRepository<Incident> incidentRepository = new GeneralRepository<Incident>(context);

                var userList = userRepository.GetAll();
                var projectList = projectRepository.GetAll();
                var levelList = levelRepository.GetAll();

                var incidentList = incidentRepository.GetAll();

                if (incidentList.Any() == false)
                {
                    incidentRepository.Insert(new Incident
                    {
                        DateOfOcurence = DateTime.Now,
                        CodeName = "Initial Test Incident",
                        Description = "Test incident for Admin Project under user Vojtech Habrnal",
                        TargettedSystem = "None",
                        User_Fk = userList.Where(e => e.FirtName == "Vojtech").First().Id,
                        User = userList.Where(e => e.FirtName == "Vojtech").First(),
                        Level_Fk = (from level in levelList where level.Name == "Low" select level.Id).First(),
                        Level = (from level in levelList where level.Name == "Low" select level).First(),
                        Project_Fk = projectList.Where(e => e.Name == "Admin").First().Id,
                        Project = projectList.Where(e => e.Name == "Admin").First(),
                    });

                    incidentRepository.Insert(new Incident
                    {
                        DateOfOcurence = DateTime.Now,
                        CodeName = "Initial Test Incident",
                        Description = "Test incident for Admin Project under user Vojtech Habrnal",
                        TargettedSystem = "None",
                        User_Fk = userList.Where(e => e.FirtName == "Vojtech").First().Id,
                        User = userList.Where(e => e.FirtName == "Vojtech").First(),
                        Level_Fk = 2,
                        Level = levelList.Where(e => e.Name == "Medium").First(),
                        Project_Fk = 3,
                        Project = projectList.Where(e => e.Name == "Test").First(),
                    });
                }

                context.SaveChanges();

                Console.WriteLine("Incidents for each User in DB:");
                userList = userRepository.GetAll();
                foreach (var user in userList)
                {
                    Console.WriteLine(user.ToString());

                    if (user.Incidents.Any() == false)
                        continue;

                    foreach (var incident in user.Incidents)
                    {
                        Console.WriteLine($"\t{incident}");
                    }
                }

                Console.WriteLine("\n\nIncidents for each Project in DB:");
                projectList = projectRepository.GetAll();
                foreach (var project in projectList)
                {
                    Console.WriteLine(project.ToString());

                    if (project.Incidents.Any() == false)
                        continue;

                    foreach (var incident in project.Incidents)
                    {
                        Console.WriteLine($"\t{incident}");
                    }
                }

                Console.WriteLine("\n\nIncidents for each Level in DB:");
                levelList = levelRepository.GetAll();
                foreach (var level in levelList)
                {
                    Console.WriteLine(level.ToString());

                    if (level.Incidents.Any() == false)
                        continue;

                    foreach (var incident in level.Incidents)
                    {
                        Console.WriteLine($"\t{incident}");
                    }
                }

                Console.WriteLine("\n\nData of all incidents:");
                incidentList = incidentRepository.GetAll();
                foreach (var incident in incidentList)
                {
                    Console.WriteLine(incident.ToString());

                    if (incident.Project == null || incident.User == null || incident.Level == null)
                    {
                        Console.WriteLine("\tError in this incident");
                        continue;
                    }

                    Console.WriteLine($"\t{incident.User}");
                    Console.WriteLine($"\t{incident.Project}");
                    Console.WriteLine($"\t{incident.Level}");
                }

                context.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("--------------------Fill Incident Table--------------------");
        }

        public static void FillCoreTables(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill Core Tables--------------------");

            try
            {
                // User
                GeneralRepository<HelpdeskUser> userRepository = new GeneralRepository<HelpdeskUser>(context);
                var userList = userRepository.GetAll();

                if (userList.Any() == false)
                {
                    Console.WriteLine($"Adding 2 Users into the DB");

                    userRepository.Insert(new HelpdeskUser
                    {
                        FirtName = "Vojtech",
                        LastName = "Habrnal",
                        UserName = "Habro",
                        PasswordHash = "",
                        Email = "vo.habrnal@seznam.cz",
                        PhoneNumber = "732800320",
                        PhoneNumberConfirmed = true
                    });

                    userRepository.Insert(new HelpdeskUser
                    {
                        FirtName = "Test First",
                        LastName = "Test Last",
                        UserName = "Test",
                        Email = "test@test.cz",
                        PhoneNumber = "123456789",
                        PhoneNumberConfirmed = false
                    });
                }

                userRepository.SaveChanges();


                // EscalationAction
                GeneralRepository<EscalationAction> actionRepository = new GeneralRepository<EscalationAction>(context);
                var actionList = actionRepository.GetAll();

                if (actionList.Any() == false)
                {
                    Console.WriteLine($"Adding 2 EscalationActions into the DB");

                    actionRepository.Insert(new EscalationAction
                    {
                        Name = "E-mail notification",
                        NotificationType = NotificationType.Email
                    });

                    actionRepository.Insert(new EscalationAction
                    {
                        Name = "SMS notification",
                        NotificationType = NotificationType.SMS
                    });
                }

                actionRepository.SaveChanges();


                // EscalationLevel
                GeneralRepository<EscalationLevel> levelRepository = new GeneralRepository<EscalationLevel>(context);
                var levelList = levelRepository.GetAll();

                if (levelList.Any() == false)
                {
                    Console.WriteLine($"Adding 3 EscalationLevels into the DB");

                    levelRepository.Insert(new EscalationLevel
                    {
                        Name = "Low",
                        Description = "Incidents cclassified as Low priority",
                    });

                    levelRepository.Insert(new EscalationLevel
                    {
                        Name = "Medium",
                        Description = "Incidents cclassified as Medium priority",
                    });

                    levelRepository.Insert(new EscalationLevel
                    {
                        Name = "High",
                        Description = "Incidents cclassified as High priority",
                    });

                }

                levelRepository.SaveChanges();


                // Project
                GeneralRepository<Project> projectRepository = new GeneralRepository<Project>(context);
                var projectList = projectRepository.GetAll();

                if (projectList.Any() == false)
                {
                    Console.WriteLine($"Adding 3 Projects into the DB");

                    projectRepository.Insert(new Project
                    {
                        Name = "Admin"
                    });

                    projectRepository.Insert(new Project
                    {
                        Name = "Test"
                    });

                    projectRepository.Insert(new Project
                    {
                        Name = "Customer A"
                    });
                }

                projectRepository.SaveChanges();

                // ProjectRole
                GeneralRepository<ProjectRole> roleRepository = new GeneralRepository<ProjectRole>(context);
                var roleList = roleRepository.GetAll();

                if (!roleList.Any())
                {
                    Console.WriteLine($"Adding 3 Users into the DB");

                    roleRepository.Insert(new ProjectRole
                    {
                        Name = "Admin",
                        Description = "User with full access to the Project resources"
                    });

                    roleRepository.Insert(new ProjectRole
                    {
                        Name = "Manager",
                        Description = "User with partial access to the Project resources"
                    });

                    roleRepository.Insert(new ProjectRole
                    {
                        Name = "User",
                        Description = "User with some access to the Project resources"
                    });
                }

                roleRepository.SaveChanges();

                Console.WriteLine("\nUsers in DB:");
                foreach (var user in userRepository.GetAll())
                {
                    Console.WriteLine(user.ToString());
                }

                Console.WriteLine("\nActions in DB:");
                foreach (var action in actionRepository.GetAll())
                {
                    Console.WriteLine(action.ToString());
                }

                Console.WriteLine("\nLevels in DB:");
                foreach (var level in levelRepository.GetAll())
                {
                    Console.WriteLine(level.ToString());
                }

                Console.WriteLine("\nProjects in DB:");
                foreach (var project in projectRepository.GetAll())
                {
                    Console.WriteLine(project.ToString());
                }

                Console.WriteLine("\nRoles in DB:");
                foreach (var role in roleRepository.GetAll())
                {
                    Console.WriteLine(role.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("--------------------Fill Core Tables--------------------");
        }
    }
}
