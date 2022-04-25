﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sena.DAL;

namespace Sena.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sena.Models.Entities.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NombreCliente");

                    b.Property<int>("TipoDocumentoId")
                        .HasColumnType("int");

                    b.HasKey("ClienteId");

                    b.HasIndex("TipoDocumentoId");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            ClienteId = 1,
                            Documento = "123456789",
                            Email = "generado@generado.com",
                            Estado = true,
                            Nombres = "Cliente generado",
                            TipoDocumentoId = 1
                        },
                        new
                        {
                            ClienteId = 2,
                            Documento = "987654321",
                            Email = "generado2@generado.com",
                            Estado = true,
                            Nombres = "Cliente generado 2",
                            TipoDocumentoId = 2
                        },
                        new
                        {
                            ClienteId = 3,
                            Documento = "88990022",
                            Email = "generado3@generado.com",
                            Estado = true,
                            Nombres = "Cliente generado 3",
                            TipoDocumentoId = 3
                        });
                });

            modelBuilder.Entity("Sena.Models.Entities.TipoDocumento", b =>
                {
                    b.Property<int>("TipoDocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Tipo de documento");

                    b.HasKey("TipoDocumentoId");

                    b.ToTable("TiposDocumento");

                    b.HasData(
                        new
                        {
                            TipoDocumentoId = 1,
                            Nombre = "TI"
                        },
                        new
                        {
                            TipoDocumentoId = 2,
                            Nombre = "CC"
                        },
                        new
                        {
                            TipoDocumentoId = 3,
                            Nombre = "CE"
                        },
                        new
                        {
                            TipoDocumentoId = 4,
                            Nombre = "PASAPORTE"
                        },
                        new
                        {
                            TipoDocumentoId = 5,
                            Nombre = "CONTRASEÑA"
                        });
                });

            modelBuilder.Entity("Sena.Models.Entities.Cliente", b =>
                {
                    b.HasOne("Sena.Models.Entities.TipoDocumento", "TipoDocumento")
                        .WithMany("Clientes")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDocumento");
                });

            modelBuilder.Entity("Sena.Models.Entities.TipoDocumento", b =>
                {
                    b.Navigation("Clientes");
                });
#pragma warning restore 612, 618
        }
    }
}
