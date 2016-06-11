namespace WebApplication1.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.MyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.Models.MyDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                manager.Create(new IdentityRole { Name = "Administrator" });
            }
            if (!context.Roles.Any(r => r.Name == "Dispatcher"))
            {
                manager.Create(new IdentityRole { Name = "Dispatcher" });
            }
            if (!context.Roles.Any(r => r.Name == "Coordinator"))
            {
                manager.Create(new IdentityRole { Name = "Coordinator" });
            }

            var hasher = new PasswordHasher();
            string password = "123456";
            string hashedPassword = hasher.HashPassword(password);

            ContractUser admin, coord1, coord2, dis1, dis2;

            context.Users.AddOrUpdate(
                p => p.Email,
                /*Passwort: "123456" */
                admin = new ContractUser { Email = "admin@admin.de", UserName = "Admin" },
                coord1 = new ContractUser { Email = "c1@coordinator.de", UserName = "Carl Maier" },
                coord2 = new ContractUser { Email = "c2@coordinator.de", UserName = "Carina Krumbold" },

                dis1 = new ContractUser { Email = "d1@dispatcher.de", UserName = "Daniel Aminati" },
                dis2 = new ContractUser { Email = "d2@dispatcher.de", UserName = "Dixi Aiman" }

            );
            context.SaveChanges();

            var userManager = new UserManager<ContractUser>(new UserStore<ContractUser>(context));
            /*userManager.Create(admin, password);
            userManager.Create(coord1, password);
            userManager.Create(coord2, password);
            userManager.Create(dis1, password);
            userManager.Create(dis2, password);*/

            userManager.AddToRole(admin.Id, "Administrator");
            userManager.AddToRole(coord1.Id, "Coordinator");
            userManager.AddToRole(coord2.Id, "Coordinator");
            userManager.AddToRole(dis1.Id, "Dispatcher");
            userManager.AddToRole(dis2.Id, "Dispatcher");

            context.SaveChanges();

            context.Mandants.AddOrUpdate(
                    p => p.MandantName,
                    new Mandant { MandantName = "Augsburg", BuchungsKreis = 11 },
                    new Mandant { MandantName = "Günzburg", BuchungsKreis = 21 },
                    new Mandant { MandantName = "Donauwörth", BuchungsKreis = 24 },
                    new Mandant { MandantName = "Kaufbeuren", BuchungsKreis = 31 },
                    new Mandant { MandantName = "Kempten", BuchungsKreis = 33 },
                    new Mandant { MandantName = "Memmingen", BuchungsKreis = 35 },
                    new Mandant { MandantName = "Lindau", BuchungsKreis = 37 },
                    new Mandant { MandantName = "Obergünzburg", BuchungsKreis = 39 },
                    new Mandant { MandantName = "Zentralverwaltung", BuchungsKreis = 51 },
                    new Mandant { MandantName = "DLZ", BuchungsKreis = 61 },
                    new Mandant { MandantName = "W+F Zusmarshausen", BuchungsKreis = 81 },
                    new Mandant { MandantName = "W+F Günzburg", BuchungsKreis = 82 },
                    new Mandant { MandantName = "W+F Kaufbeuren", BuchungsKreis = 83 }
            );
            context.SaveChanges();

            context.MU_Relations.AddOrUpdate(
                    p => p.MandantID,
                    new MU_Relation { UserID = context.Users.Single(i => i.UserName == "Carl Maier").Id, MandantID = context.Mandants.Single(i => i.MandantName == "DLZ").Id },
                    new MU_Relation { UserID = context.Users.Single(i => i.UserName == "Carina Krumbold").Id, MandantID = context.Mandants.Single(i => i.MandantName == "Augsburg").Id }
            );
            context.SaveChanges();

            context.Departments.AddOrUpdate(
                    p => p.DepartmentName,
                    new Department { DepartmentName = "Apotheke", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.MandantName == "DLZ") },
                    new Department { DepartmentName = "Bertriebstechnik", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.MandantName == "DLZ") },
                    new Department { DepartmentName = "Bertriebstechnik Energieversorgung", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.MandantName == "DLZ") },
                    new Department { DepartmentName = "Bertriebstechnik Leitung", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.BuchungsKreis == 61) },
                    new Department { DepartmentName = "Bertriebstechnik Mechatronik", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.BuchungsKreis == 61) },
                    new Department { DepartmentName = "Betriebstechnik Sekretariat", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.BuchungsKreis == 61) },
                    new Department { DepartmentName = "DLZ Leitung", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.BuchungsKreis == 61) },
                    //new Department { DepartmentName = "EDV/IT", Dispatcher = null, StvDispatcher = null, Mandant = context.Mandants.Single(i => i.BuchungsKreis == 61) },

                    new Department { DepartmentName = "Haustechnik BKH Kempten", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.MandantName == "Kempten") },
                    new Department { DepartmentName = "Regionalleitung Mitte", Dispatcher = context.Users.Single(i => i.UserName == "Daniel Aminati"), StvDispatcher = context.Users.Single(i => i.UserName == "Dixi Aiman"), Mandant = context.Mandants.Single(i => i.MandantName == "Augsburg") }


                    );
            context.SaveChanges();

            context.ContractKinds.AddOrUpdate(
                   p => p.Description,
                   new ContractsKind { Description = "Kaufverträge" },
                   new ContractsKind { Description = "Mietverträge" },
                   new ContractsKind { Description = "Leasingverträge" },
                   new ContractsKind { Description = "Dienstleistungsvertrag" },
                   new ContractsKind { Description = "Kooperationsvereinbarungen" },
                   new ContractsKind { Description = "Konsiliarverträge" }
               );

            context.ContractStatuss.AddOrUpdate(
                   p => p.Description,
                   new ContractsStatus { Description = "incomplete" },
                   new ContractsStatus { Description = "activ" },
                   new ContractsStatus { Description = "repealed" },
                   new ContractsStatus { Description = "closed" },
                   new ContractsStatus { Description = "deleted" }
               );

            context.ContractTypes.AddOrUpdate(
                  p => p.Description,
                  new ContractsType { Description = "IT-Verträge" },
                  new ContractsType { Description = "Telekommunikation" },
                  new ContractsType { Description = "Liegenschaftsverwaltung" },
                  new ContractsType { Description = "Fahrzeuge" },
                  new ContractsType { Description = "Versicherungen" }
              );


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
