namespace Week7_Lab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreasedNoteLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pins", "Note", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pins", "Note", c => c.String(maxLength: 250));
        }
    }
}
