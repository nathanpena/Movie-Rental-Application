namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, MembershipTypeId) VALUES ('Nathan Pena', 1, 1)");
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, MembershipTypeId) VALUES ('Elizabeth Pena', 1, 2)");
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, MembershipTypeId) VALUES ('Sophia Pena', 1, 3)");
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, MembershipTypeId) VALUES ('Maribel Pena', 1, 1)");
            Sql("INSERT INTO Customers (Name, IsSubscribedToNewsletter, MembershipTypeId) VALUES ('Gabriel Pena', 1, 1)");
        }
        
        public override void Down()
        {
        }
    }
}
