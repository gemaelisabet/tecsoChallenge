namespace Challenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumnos",
                c => new
                    {
                        AlumnoID = c.Int(nullable: false, identity: true),
                        Apellido = c.String(),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.AlumnoID);
            
            CreateTable(
                "dbo.Inscripciones",
                c => new
                    {
                        InscripcionID = c.Int(nullable: false, identity: true),
                        CursoID = c.Int(nullable: false),
                        AlumnoID = c.Int(nullable: false),
                        EstadoCursada = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InscripcionID)
                .ForeignKey("dbo.Alumnos", t => t.AlumnoID, cascadeDelete: true)
                .ForeignKey("dbo.Cursos", t => t.CursoID, cascadeDelete: true)
                .Index(t => t.CursoID)
                .Index(t => t.AlumnoID);
            
            CreateTable(
                "dbo.Cursos",
                c => new
                    {
                        CursoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.CursoID);
            
            CreateTable(
                "dbo.TecsoLogs",
                c => new
                    {
                        TecsoLogID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        TipoLog = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TecsoLogID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inscripciones", "CursoID", "dbo.Cursos");
            DropForeignKey("dbo.Inscripciones", "AlumnoID", "dbo.Alumnos");
            DropIndex("dbo.Inscripciones", new[] { "AlumnoID" });
            DropIndex("dbo.Inscripciones", new[] { "CursoID" });
            DropTable("dbo.TecsoLogs");
            DropTable("dbo.Cursos");
            DropTable("dbo.Inscripciones");
            DropTable("dbo.Alumnos");
        }
    }
}
