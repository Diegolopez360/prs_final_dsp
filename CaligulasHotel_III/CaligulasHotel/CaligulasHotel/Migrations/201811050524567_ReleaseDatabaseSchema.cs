namespace CaligulasHotel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReleaseDatabaseSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articulo",
                c => new
                    {
                        ArticuloId = c.String(nullable: false, maxLength: 128),
                        SKU = c.String(maxLength: 50),
                        Nombre = c.String(maxLength: 100),
                        Descripcion = c.String(maxLength: 255),
                        ImageUrl = c.String(),
                        PrecioUnitario = c.Double(nullable: false),
                        PrecioCompra = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        UnidadesCompradas = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        Categoria_CategoriaId = c.String(maxLength: 128),
                        Marca_MarcaId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArticuloId)
                .ForeignKey("dbo.Categoria", t => t.Categoria_CategoriaId)
                .ForeignKey("dbo.Marca", t => t.Marca_MarcaId)
                .Index(t => t.Categoria_CategoriaId)
                .Index(t => t.Marca_MarcaId);
            
            CreateTable(
                "dbo.Categoria",
                c => new
                    {
                        CategoriaId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoriaId);
            
            CreateTable(
                "dbo.Marca",
                c => new
                    {
                        MarcaId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MarcaId);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        FacturaId = c.String(nullable: false, maxLength: 128),
                        TotalPagar = c.Double(nullable: false),
                        Descuento = c.Double(nullable: false),
                        NumeroArticulos = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.FacturaId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LineaFactura",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        NumeroArticulos = c.Int(nullable: false),
                        Articulo_ArticuloId = c.String(maxLength: 128),
                        Factura_FacturaId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articulo", t => t.Articulo_ArticuloId)
                .ForeignKey("dbo.Factura", t => t.Factura_FacturaId)
                .Index(t => t.Articulo_ArticuloId)
                .Index(t => t.Factura_FacturaId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        FullName = c.String(maxLength: 100),
                        Address = c.String(maxLength: 255),
                        BirthDate = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CardNumber = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Habitacion",
                c => new
                    {
                        HabitacionId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        NumeroPersonas = c.Int(nullable: false),
                        Descripcion = c.String(),
                        ImageUrl = c.String(),
                        PrecioNoche = c.Double(nullable: false),
                        NumeroHabitaciones = c.Int(nullable: false),
                        HabitacionesDisponibles = c.Int(nullable: false),
                        ServiciosHabitacion = c.String(),
                        Disponible = c.Boolean(nullable: false),
                        TipoHabitacion_TipoHabitacionId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HabitacionId)
                .ForeignKey("dbo.TipoHabitacion", t => t.TipoHabitacion_TipoHabitacionId)
                .Index(t => t.TipoHabitacion_TipoHabitacionId);
            
            CreateTable(
                "dbo.TipoHabitacion",
                c => new
                    {
                        TipoHabitacionId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.TipoHabitacionId);
            
            CreateTable(
                "dbo.LineaHabitacionUsuario",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FechaContrato = c.DateTime(nullable: false),
                        FechaVencimiento = c.DateTime(nullable: false),
                        NumeroNoches = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        TotalPago = c.Double(nullable: false),
                        Habitacion_HabitacionId = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Habitacion", t => t.Habitacion_HabitacionId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Habitacion_HabitacionId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LineaRentaVehiculo",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FechaRenta = c.DateTime(nullable: false),
                        FechaExpiracion = c.DateTime(nullable: false),
                        TotalPago = c.Double(nullable: false),
                        NumeroHoras = c.Int(nullable: false),
                        RentaVehiculo_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RentaVehiculo", t => t.RentaVehiculo_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.RentaVehiculo_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RentaVehiculo",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CostoHora = c.Double(nullable: false),
                        MaxHoras = c.Int(nullable: false),
                        MinHoras = c.Int(nullable: false),
                        MinMinutos = c.Int(nullable: false),
                        MaxMinutos = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        Vehiculo_VehiculoId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehiculo", t => t.Vehiculo_VehiculoId)
                .Index(t => t.Vehiculo_VehiculoId);
            
            CreateTable(
                "dbo.Vehiculo",
                c => new
                    {
                        VehiculoId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        Matricula = c.String(),
                        ImageUrl = c.String(),
                        NumeroPersonas = c.Int(nullable: false),
                        FechaFabricacion = c.DateTime(nullable: false),
                        TipoVehiculo_TipoVehiculoId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VehiculoId)
                .ForeignKey("dbo.TipoVehiculo", t => t.TipoVehiculo_TipoVehiculoId)
                .Index(t => t.TipoVehiculo_TipoVehiculoId);
            
            CreateTable(
                "dbo.TipoVehiculo",
                c => new
                    {
                        TipoVehiculoId = c.String(nullable: false, maxLength: 128),
                        Nombre = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TipoVehiculoId);
            
            CreateTable(
                "dbo.LineaShoppingCart",
                c => new
                    {
                        LineaId = c.String(nullable: false, maxLength: 128),
                        NumeroArticulos = c.Int(nullable: false),
                        TotalPagar = c.Double(nullable: false),
                        PrecioUnitario = c.Double(nullable: false),
                        Cancelado = c.Boolean(nullable: false),
                        FechaAgregado = c.DateTime(nullable: false),
                        FechaCancelado = c.DateTime(),
                        Articulo_ArticuloId = c.String(maxLength: 128),
                        ShoppingCart_CartId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LineaId)
                .ForeignKey("dbo.Articulo", t => t.Articulo_ArticuloId)
                .ForeignKey("dbo.ShoppingCart", t => t.ShoppingCart_CartId)
                .Index(t => t.Articulo_ArticuloId)
                .Index(t => t.ShoppingCart_CartId);
            
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        CartId = c.String(nullable: false, maxLength: 128),
                        FechaCreacion = c.DateTime(nullable: false),
                        MaxItems = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LineaShoppingCart", "ShoppingCart_CartId", "dbo.ShoppingCart");
            DropForeignKey("dbo.ShoppingCart", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LineaShoppingCart", "Articulo_ArticuloId", "dbo.Articulo");
            DropForeignKey("dbo.LineaRentaVehiculo", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LineaRentaVehiculo", "RentaVehiculo_Id", "dbo.RentaVehiculo");
            DropForeignKey("dbo.RentaVehiculo", "Vehiculo_VehiculoId", "dbo.Vehiculo");
            DropForeignKey("dbo.Vehiculo", "TipoVehiculo_TipoVehiculoId", "dbo.TipoVehiculo");
            DropForeignKey("dbo.LineaHabitacionUsuario", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LineaHabitacionUsuario", "Habitacion_HabitacionId", "dbo.Habitacion");
            DropForeignKey("dbo.Habitacion", "TipoHabitacion_TipoHabitacionId", "dbo.TipoHabitacion");
            DropForeignKey("dbo.Factura", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LineaFactura", "Factura_FacturaId", "dbo.Factura");
            DropForeignKey("dbo.LineaFactura", "Articulo_ArticuloId", "dbo.Articulo");
            DropForeignKey("dbo.Articulo", "Marca_MarcaId", "dbo.Marca");
            DropForeignKey("dbo.Articulo", "Categoria_CategoriaId", "dbo.Categoria");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ShoppingCart", new[] { "User_Id" });
            DropIndex("dbo.LineaShoppingCart", new[] { "ShoppingCart_CartId" });
            DropIndex("dbo.LineaShoppingCart", new[] { "Articulo_ArticuloId" });
            DropIndex("dbo.Vehiculo", new[] { "TipoVehiculo_TipoVehiculoId" });
            DropIndex("dbo.RentaVehiculo", new[] { "Vehiculo_VehiculoId" });
            DropIndex("dbo.LineaRentaVehiculo", new[] { "User_Id" });
            DropIndex("dbo.LineaRentaVehiculo", new[] { "RentaVehiculo_Id" });
            DropIndex("dbo.LineaHabitacionUsuario", new[] { "User_Id" });
            DropIndex("dbo.LineaHabitacionUsuario", new[] { "Habitacion_HabitacionId" });
            DropIndex("dbo.Habitacion", new[] { "TipoHabitacion_TipoHabitacionId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.LineaFactura", new[] { "Factura_FacturaId" });
            DropIndex("dbo.LineaFactura", new[] { "Articulo_ArticuloId" });
            DropIndex("dbo.Factura", new[] { "User_Id" });
            DropIndex("dbo.Articulo", new[] { "Marca_MarcaId" });
            DropIndex("dbo.Articulo", new[] { "Categoria_CategoriaId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ShoppingCart");
            DropTable("dbo.LineaShoppingCart");
            DropTable("dbo.TipoVehiculo");
            DropTable("dbo.Vehiculo");
            DropTable("dbo.RentaVehiculo");
            DropTable("dbo.LineaRentaVehiculo");
            DropTable("dbo.LineaHabitacionUsuario");
            DropTable("dbo.TipoHabitacion");
            DropTable("dbo.Habitacion");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.LineaFactura");
            DropTable("dbo.Factura");
            DropTable("dbo.Marca");
            DropTable("dbo.Categoria");
            DropTable("dbo.Articulo");
        }
    }
}
