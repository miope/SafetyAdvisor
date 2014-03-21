namespace SafetyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEvaluationItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvaluationItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EvaluationItems", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvaluationItems", "Parent_Id", "dbo.EvaluationItems");
            DropIndex("dbo.EvaluationItems", new[] { "Parent_Id" });
            DropTable("dbo.EvaluationItems");
        }
    }
}
