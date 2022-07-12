﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSE.Pedidos.Infra.Data;

namespace NSE.Pedidos.Infra.Migrations
{
    [DbContext(typeof(PedidosContext))]
    [Migration("20220526000647_voucher")]
    partial class voucher
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.MinhaSequencia", "'MinhaSequencia', '', '1000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PedidoStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("VoucherUtilizado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdutoImagem")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItems");
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Vouchers.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUtilizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Percentual")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("TipoDesconto")
                        .HasColumnType("int");

                    b.Property<bool>("Utilizado")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.Pedido", b =>
                {
                    b.HasOne("NSE.Pedidos.Domain.Vouchers.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherId");

                    b.OwnsOne("NSE.Pedidos.Domain.Pedidos.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("PedidoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .HasColumnName("Bairro")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cep")
                                .HasColumnName("Cep")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cidade")
                                .HasColumnName("Cidade")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complemento")
                                .HasColumnName("Complemento")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Estado")
                                .HasColumnName("Estado")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Logradouro")
                                .HasColumnName("Logradouro")
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Numero")
                                .HasColumnName("Numero")
                                .HasColumnType("varchar(100)");

                            b1.HasKey("PedidoId");

                            b1.ToTable("Pedidos");

                            b1.WithOwner()
                                .HasForeignKey("PedidoId");
                        });
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.PedidoItem", b =>
                {
                    b.HasOne("NSE.Pedidos.Domain.Pedidos.Pedido", "Pedido")
                        .WithMany("PedidoItems")
                        .HasForeignKey("PedidoId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
