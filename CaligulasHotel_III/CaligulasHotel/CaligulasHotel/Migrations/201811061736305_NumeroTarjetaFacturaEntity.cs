namespace CaligulasHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NumeroTarjetaFacturaEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Factura", "NumeroTarjeta", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Factura", "NumeroTarjeta");
        }
    }
}
