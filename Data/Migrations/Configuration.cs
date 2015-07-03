using Domain.Entities;

namespace Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.Context context)
        {
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

            context.Groups.AddOrUpdate(p => p.Id,
                new Group
                {
                    Id = 1,
                    Name = "Central"
                },
                new Group
                {
                    Id = 2,
                    Name = "East"
                },
                new Group
                {
                    Id = 3,
                    Name = "North"
                },
                new Group
                {
                    Id = 4,
                    Name = "Northeast"
                },
                new Group
                {
                    Id = 5,
                    Name = "South"
                },
                new Group
                {
                    Id = 6,
                    Name = "West"
                },
                new Group
                {
                    Id = 7,
                    Name = "Affiliates"
                });

            context.PatternTimes.AddOrUpdate(p => p.Id,
                new PatternTime
                {
                    Id = 1,
                    Key = 1,
                    Days = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 10,
                    TimeClose = 18
                },
                new PatternTime
                {
                    Id = 2,
                    Key = 2,
                    Days = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 9,
                    TimeClose = 17
                },
                new PatternTime
                {
                    Id = 3,
                    Key = 3,
                    Days = "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 10,
                    TimeClose = 18
                },
                new PatternTime
                {
                    Id = 4,
                    Key = 4,
                    Days = "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 10,
                    TimeClose = 19
                },
                new PatternTime
                {
                    Id = 5,
                    Key = 5,
                    Days = "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 10,
                    TimeClose = 20
                },
                new PatternTime
                {
                    Id = 6,
                    Key = 6,
                    Days = "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 11,
                    TimeClose = 20
                },
                new PatternTime
                {
                    Id = 7,
                    Key = 7,
                    Days = "Sunday,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday",
                    TimeOpen = 11,
                    TimeClose = 21
                },
                new PatternTime
                {
                    Id = 8,
                    Key = 8,
                    Days = "Monday,Tuesday,Wednesday,Thursday,Friday",
                    TimeOpen = 11,
                    TimeClose = 19
                },
                new PatternTime
                {
                    Id = 9,
                    Key = 8,
                    Days = "Sunday,Saturday",
                    TimeOpen = 10,
                    TimeClose = 18
                },
                new PatternTime
                {
                    Id = 10,
                    Key = 9,
                    Days = "Monday,Tuesday,Wednesday,Thursday,Friday",
                    TimeOpen = 11,
                    TimeClose = 19
                },
                new PatternTime
                {
                    Id = 11,
                    Key = 9,
                    Days = "Saturday",
                    TimeOpen = 10,
                    TimeClose = 18
                }
                );
        }
    }
}