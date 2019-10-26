﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDiabeticSystem.Web.Data;

namespace MyDiabeticSystem.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20191012224950_CompleteDBIII")]
    partial class CompleteDBIII
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Check", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Carbohydrates");

                    b.Property<DateTime>("Date");

                    b.Property<double>("Glucometry");

                    b.Property<DateTime>("Hour");

                    b.Property<int?>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Checks");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Parameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime?>("EndTime");

                    b.Property<int?>("PatientId");

                    b.Property<DateTime?>("StartTime");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanEdit");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(20);

                    b.Property<DateTime>("DateBirth");

                    b.Property<int?>("DoctorId");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Ratio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("PatientId");

                    b.Property<DateTime>("StartTime");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Ratios");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Record", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Justification");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<int?>("PatientId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Sensibility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<int?>("PatientId");

                    b.Property<DateTime>("StartTime");

                    b.Property<double>("Value");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Sensibilities");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Check", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Parameter", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Patient", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Ratio", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Record", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("MyDiabeticSystem.Web.Data.Entities.Sensibility", b =>
                {
                    b.HasOne("MyDiabeticSystem.Web.Data.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });
#pragma warning restore 612, 618
        }
    }
}