namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using StudentSystem.Data;
    using StudentSystem.Models;
    using System.Collections.Generic;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            base.Seed(context);
        }
    }
}
