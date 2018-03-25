namespace App.Game.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.emprestimo", "Data_devolvido", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.emprestimo", "Data_devolvido", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
