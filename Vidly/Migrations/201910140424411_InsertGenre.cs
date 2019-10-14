namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GENRES (TYPE) VALUES ('Comedy')");
            Sql("INSERT INTO GENRES (TYPE) VALUES ('Action')");
            Sql("INSERT INTO GENRES (TYPE) VALUES ('Drama')");
            Sql("INSERT INTO GENRES (TYPE) VALUES ('Family')");
            Sql("INSERT INTO GENRES (TYPE) VALUES ('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
