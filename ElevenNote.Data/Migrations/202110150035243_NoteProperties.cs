namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Note", "Content", c => c.String(nullable: false));
        }
    }
}
