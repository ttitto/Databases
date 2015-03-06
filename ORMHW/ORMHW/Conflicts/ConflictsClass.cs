namespace Conflicts
{
    using Softuni.Models;
    using System;

    public class ConflictsClass
    {
        static void Main(string[] args)
        {
            using (SoftUniEntities softuniDbContextFirst = new SoftUniEntities())
            {
                using (SoftUniEntities softuniDbContextSecond = new SoftUniEntities())
                {
                    softuniDbContextSecond.Addresses.Find(3).AddressText = "Vasil Levski Str. 15";
                    softuniDbContextFirst.Addresses.Find(3).AddressText = "BulairStr 25";
                    softuniDbContextSecond.SaveChanges();
                    softuniDbContextFirst.SaveChanges();
                }
            }

            using (SoftUniEntities softuniDbContext = new SoftUniEntities())
            {
                string addressText = softuniDbContext.Addresses.Find(3).AddressText;
                Console.WriteLine(addressText);
            }
        }
    }
}