﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using WorkShop.EF;

#nullable disable

namespace WorkShop.EF.Migrations
{
    [DbContext(typeof(EscolaContext))]
    [Migration("20241106114011_v5")]
    partial class v5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WorkShop.EF.Models.Certificado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Conclusao")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("MatriculaId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("MatriculaId")
                        .IsUnique();

                    b.ToTable("Certificado");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Estudante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("Id");

                    b.HasIndex("Nome", "Sobrenome")
                        .IsUnique();

                    b.ToTable("Estudantes");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("EstudanteId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("EstudanteId");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Certificado", b =>
                {
                    b.HasOne("WorkShop.EF.Models.Matricula", "Matricula")
                        .WithOne("Certificado")
                        .HasForeignKey("WorkShop.EF.Models.Certificado", "MatriculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Matricula");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Matricula", b =>
                {
                    b.HasOne("WorkShop.EF.Models.Curso", "Curso")
                        .WithMany("Matriculas")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkShop.EF.Models.Estudante", "Estudante")
                        .WithMany("Matriculas")
                        .HasForeignKey("EstudanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudante");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Curso", b =>
                {
                    b.Navigation("Matriculas");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Estudante", b =>
                {
                    b.Navigation("Matriculas");
                });

            modelBuilder.Entity("WorkShop.EF.Models.Matricula", b =>
                {
                    b.Navigation("Certificado");
                });
#pragma warning restore 612, 618
        }
    }
}
