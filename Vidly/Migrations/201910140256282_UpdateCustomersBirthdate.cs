namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomersBirthdate : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE CUSTOMERS SET BIRTHDATE ='19830304'  WHERE ID=2");
            Sql("UPDATE CUSTOMERS SET BIRTHDATE ='19830618'  WHERE ID=4");
            Sql("UPDATE CUSTOMERS SET BIRTHDATE ='20070224'  WHERE ID=5");
        }
        
        public override void Down()
        {
        }
    }
}
