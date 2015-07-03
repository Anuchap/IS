namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCommunityToSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "Community", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sites", "Community");
        }
    }
}
