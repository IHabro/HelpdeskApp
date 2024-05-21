using DataLayer.DbContexts;
using DataLayer.Models;
using DataLayer.Repositories;

namespace FunctionTest.DatabaseTables
{
    public class JoinTables
    {
        public static void FillActionToProjectToRole(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill ActionToProjectToRole Join Table--------------------");

            try
            {
                ProjectRepository projectRepository = new ProjectRepository(context);
                RoleRepository roleRepository = new RoleRepository(context);
                ActionRepository actionRepository = new ActionRepository(context);

                var projectList = projectRepository.GetAll();
                var roleList = roleRepository.GetAll();
                var actionList = actionRepository.GetAll();

                if (projectList.Any() && roleList.Any() && actionList.Any())
                //if (projectList.Any() && roleList.Any() && actionList.Any() && false)
                {
                    Console.WriteLine("Adding SMS action to Admin role on all projects");

                    var role = roleList.Where(r => r.Name == "Admin").First();
                    var action = actionList.Where(a => a.Name.Contains("SMS")).First();

                    foreach (var project in projectList)
                    {
                        project.ActionOnRoleInProject.Add(new ActionToProjectToRole
                        {
                            Action_Fk = action.Id,
                            Project_Fk = project.Id,
                            Role_Fk = role.Id,
                            Action = action,
                            Project = project,
                            Role = role
                        });
                    }

                    Console.WriteLine("Adding E-mail action to user role on all projects");

                    role = roleList.Where(r => r.Name == "User").First();
                    action = actionList.Where(a => a.Name.Contains("mail")).First();

                    foreach (var project in projectList)
                    {
                        project.ActionOnRoleInProject.Add(new ActionToProjectToRole
                        {
                            Action_Fk = action.Id,
                            Project_Fk = project.Id,
                            Role_Fk = role.Id,
                            Action = action,
                            Project = project,
                            Role = role
                        });
                    }
                }

                actionRepository.SaveChanges();
                projectRepository.SaveChanges();
                roleRepository.SaveChanges();

                Console.WriteLine("\nData for all Projects");
                projectList = projectRepository.GetAll();

                foreach (var project in projectList)
                {
                    Console.WriteLine(project.ToString());
                    foreach (var item in project.ActionOnRoleInProject)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }

                Console.WriteLine("\nData for all Users");
                actionList = actionRepository.GetAll();

                foreach (var user in actionList)
                {
                    Console.WriteLine(user.ToString());
                    foreach (var item in user.ActionOnProjectAndRole)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }

                Console.WriteLine("\nData for all Roles");
                roleList = roleRepository.GetAll();

                foreach (var role in roleList)
                {
                    Console.WriteLine(role.ToString());
                    foreach (var item in role.ActionForProjectAndRole)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n--------------------Fill ActionToProjectToRole Join Table--------------------");
        }

        public static void FillUserToProjectToRole(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill UserToProjectToRole Join Table--------------------");

            try
            {
                ProjectRepository projectRepository = new ProjectRepository(context);
                RoleRepository roleRepository = new RoleRepository(context);
                UserRepository userRepository = new UserRepository(context);

                var projectList = projectRepository.GetAll();
                var roleList = roleRepository.GetAll();
                var userList = userRepository.GetAll();

                if (projectList.Any() && roleList.Any() && userList.Any())
                {
                    Console.WriteLine($"Adding Vojtech Habrnal to every Project as an Admin");
                    Console.WriteLine($"Adding TestUser to every Project as an User");

                    var user = userList.Where(u => u.FirtName == "Vojtech").First();
                    var role = roleList.Where(r => r.Name == "Admin").First();

                    foreach (var project in projectList)
                    {
                        project.UsersAndRoles.Add(new UserToProjectToRole
                        {
                            Project = project,
                            User = user,
                            Role = role,
                            Project_Fk = project.Id,
                            User_Fk = user.Id,
                            Role_Fk = role.Id
                        });
                    }

                    user = userList.Where(u => u.FirtName.Contains("Test")).First();
                    role = roleList.Where(r => r.Name == "User").First();

                    foreach (var project in projectList)
                    {
                        project.UsersAndRoles.Add(new UserToProjectToRole
                        {
                            Project = project,
                            User = user,
                            Role = role,
                            Project_Fk = project.Id,
                            User_Fk = user.Id,
                            Role_Fk = role.Id
                        });
                    }
                }

                userRepository.SaveChanges();
                projectRepository.SaveChanges();
                roleRepository.SaveChanges();

                Console.WriteLine("\nData for all Projects");
                projectList = projectRepository.GetAll();

                foreach (var project in projectList)
                {
                    Console.WriteLine(project.ToString());
                    foreach (var item in project.UsersAndRoles)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }

                Console.WriteLine("\nData for all Users");
                userList = userRepository.GetAll();

                foreach (var user in userList)
                {
                    Console.WriteLine(user.ToString());
                    foreach (var item in user.RolesInProjects)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }

                Console.WriteLine("\nData for all Roles");
                roleList = roleRepository.GetAll();

                foreach (var role in roleList)
                {
                    Console.WriteLine(role.ToString());
                    foreach (var item in role.UsersInProjects)
                    {
                        Console.WriteLine($"\t{item}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n--------------------Fill UserToProjectToRole Join Table--------------------");
        }

        public static void FillRoleToProjects(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill RoleToProjects Join Table--------------------");

            try
            {
                ProjectRepository projectRepository = new ProjectRepository(context);
                RoleRepository roleRepository = new RoleRepository(context);

                var projectList = projectRepository.GetAll();
                var roleList = roleRepository.GetAll();

                if (projectList.Any() && roleList.Any())
                {
                    Console.WriteLine($"Adding roles 3 basic roles to each project");

                    foreach (var project in projectList)
                    {
                        foreach (var role in roleList)
                        {
                            project.Roles.Add(role);
                        }
                    }
                }

                projectRepository.SaveChanges();
                roleRepository.SaveChanges();

                Console.WriteLine("\nData of all Project:");
                projectList = projectRepository.GetAll();

                foreach (var project in projectList)
                {
                    Console.WriteLine(project.ToString());

                    foreach (var role in project.Roles)
                    {
                        Console.WriteLine($"\t{role}");
                    }
                }

                Console.WriteLine("\nData of all Roles:");
                roleList = roleRepository.GetAll();

                foreach (var role in roleList)
                {
                    Console.WriteLine(role.ToString());

                    foreach (var project in role.Projects)
                    {
                        Console.WriteLine($"\t{project}");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n--------------------Fill RoleToProjects Join Table--------------------");
        }

        public static void FillLevelToAction(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill LevelToAction Join Table--------------------");

            try
            {
                ActionRepository actionRepository = new ActionRepository(context);
                LevelRepository levelRepository = new LevelRepository(context);

                var actionList = actionRepository.GetAll();
                var levelList = levelRepository.GetAll();

                if (actionList.Any() && levelList.Any())
                {
                    Console.WriteLine("Adding both action types (email, SMS) to each level");

                    foreach (var level in levelList)
                    {
                        foreach (var action in actionList)
                        {
                            level.Actions.Add(action);
                        }
                    }
                }

                actionRepository.SaveChanges();
                levelRepository.SaveChanges();

                Console.WriteLine("\nData of all Actions:");
                actionList = actionRepository.GetAll();

                foreach (var action in actionList)
                {
                    Console.WriteLine(action.ToString());

                    foreach (var level in action.Levels)
                    {
                        Console.WriteLine($"\t{level}");
                    }
                }

                Console.WriteLine("\nData of all Levels:");
                levelList = levelRepository.GetAll();

                foreach (var level in levelList)
                {
                    Console.WriteLine(level.ToString());

                    foreach (var action in level.Actions)
                    {
                        Console.WriteLine($"\t{action}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n--------------------Fill LevelToAction Join Table--------------------");
        }

        public static void FillUserToAction(IdentityDbContext context)
        {
            Console.WriteLine("\n--------------------Fill UserToAction Join Table--------------------");

            try
            {
                UserRepository userRepository = new UserRepository(context);
                ActionRepository actionRepository = new ActionRepository(context);

                var userList = userRepository.GetAll();
                var actionList = actionRepository.GetAll();

                if (actionList.Any() && userList.Any())
                {
                    Console.WriteLine("Adding email to Vojtech Habrnal and SMS to TestUser");

                    for (int i = 0; i < userList.Count(); i++)
                    {
                        userList.ElementAt(i).Actions.Add(actionList.ElementAt(i));
                    }
                }

                userRepository.SaveChanges();
                actionRepository.SaveChanges();

                Console.WriteLine("\nData of all Actions:");
                actionList = actionRepository.GetAll();

                foreach (var action in actionList)
                {
                    Console.WriteLine(action.ToString());

                    foreach (var user in action.Users)
                    {
                        Console.WriteLine($"\t{user}");
                    }
                }

                Console.WriteLine("\nData of all Levels:");
                userList = userRepository.GetAll();

                foreach (var user in userList)
                {
                    Console.WriteLine(user.ToString());

                    foreach (var action in user.Actions)
                    {
                        Console.WriteLine($"\t{action}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n--------------------Fill UserToAction Join Table--------------------");

        }
    }
}
