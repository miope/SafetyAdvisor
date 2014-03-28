namespace SafetyAdvisor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReactiveFieldOnEvaluationItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvaluationItems", "IsReactive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EvaluationItems", "IsReactive");
        }
    }
}
