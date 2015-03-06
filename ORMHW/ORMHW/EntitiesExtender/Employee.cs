namespace EntitiesExtender
{
    using Softuni.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public partial class Employee
    {
        private EntitySet<Territory> territories;

        public Employee()
        {
            this.Territories = new EntitySet<Territory>();
        }

        public virtual EntitySet<Territory> Territories
        {
            get { return this.territories; }
            set { this.territories = value; }
        }
    }
}
