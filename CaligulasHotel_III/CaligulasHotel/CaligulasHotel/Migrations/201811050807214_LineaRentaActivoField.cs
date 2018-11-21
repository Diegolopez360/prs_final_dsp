namespace CaligulasHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LineaRentaActivoField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LineaRentaVehiculo", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LineaRentaVehiculo", "Activo");
        }
    }
}
