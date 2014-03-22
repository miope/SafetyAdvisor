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
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EvaluationItems", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvaluationItems", "ParentId", "dbo.EvaluationItems");
            DropIndex("dbo.EvaluationItems", new[] { "ParentId" });
            DropTable("dbo.EvaluationItems");
        }
    }
}
