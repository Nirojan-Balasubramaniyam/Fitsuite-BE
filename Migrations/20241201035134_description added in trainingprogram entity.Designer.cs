﻿// <auto-generated />
using System;
using GYMFeeManagement_System_BE.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GYMFeeManagement_System_BE.Migrations
{
    [DbContext(typeof(GymDbContext))]
    [Migration("20241201035134_description added in trainingprogram entity")]
    partial class descriptionaddedintrainingprogramentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.HasIndex("BranchId")
                        .IsUnique()
                        .HasFilter("[BranchId] IS NOT NULL");

                    b.HasIndex("MemberId")
                        .IsUnique()
                        .HasFilter("[MemberId] IS NOT NULL");

                    b.HasIndex("RequestId")
                        .IsUnique()
                        .HasFilter("[RequestId] IS NOT NULL");

                    b.HasIndex("StaffId")
                        .IsUnique()
                        .HasFilter("[StaffId] IS NOT NULL");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Alert", b =>
                {
                    b.Property<int>("AlertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlertId"));

                    b.Property<DateTime?>("AccessedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Action")
                        .HasColumnType("bit");

                    b.Property<string>("AlertType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<int?>("ProgramId")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AlertId");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProgramId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Branch", b =>
                {
                    b.Property<int>("BranchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchId"));

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BranchId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.ContactUsMessage", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Read")
                        .HasColumnType("bit");

                    b.Property<DateTime>("SubmittedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("MessageId");

                    b.ToTable("ContactUsMessages");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.EnrollProgram", b =>
                {
                    b.Property<int>("EnrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollId"));

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int");

                    b.HasKey("EnrollId");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProgramId");

                    b.ToTable("EnrollPrograms");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.HasIndex("BranchId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("MemberId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.ProgramType", b =>
                {
                    b.Property<int>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeId"));

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeId");

                    b.ToTable("ProgramTypes");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<decimal?>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("NIC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProgramId")
                        .HasColumnType("int");

                    b.Property<string>("ReceiptNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestId");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProgramId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewId");

                    b.HasIndex("MemberId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Staff", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffId"));

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("StaffId");

                    b.HasIndex("BranchId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.TrainingProgram", b =>
                {
                    b.Property<int>("ProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgramId"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("ProgramId");

                    b.HasIndex("TypeId");

                    b.ToTable("TrainingPrograms");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.WorkoutEnrollment", b =>
                {
                    b.Property<int>("WorkoutEnrollId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkoutEnrollId"));

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutPlanId")
                        .HasColumnType("int");

                    b.HasKey("WorkoutEnrollId");

                    b.HasIndex("MemberId");

                    b.HasIndex("StaffId");

                    b.HasIndex("WorkoutPlanId");

                    b.ToTable("WorkoutEnrollments");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.WorkoutPlan", b =>
                {
                    b.Property<int>("WorkoutPlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkoutPlanId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepsCount")
                        .HasColumnType("int");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("WorkoutPlanId");

                    b.HasIndex("StaffId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Address", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Branch", "Branch")
                        .WithOne("Address")
                        .HasForeignKey("GYMFeeManagement_System_BE.Entities.Address", "BranchId");

                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "Member")
                        .WithOne("Address")
                        .HasForeignKey("GYMFeeManagement_System_BE.Entities.Address", "MemberId");

                    b.HasOne("GYMFeeManagement_System_BE.Entities.Request", "Request")
                        .WithOne("Address")
                        .HasForeignKey("GYMFeeManagement_System_BE.Entities.Address", "RequestId");

                    b.HasOne("GYMFeeManagement_System_BE.Entities.Staff", "Staff")
                        .WithOne("Address")
                        .HasForeignKey("GYMFeeManagement_System_BE.Entities.Address", "StaffId");

                    b.Navigation("Branch");

                    b.Navigation("Member");

                    b.Navigation("Request");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Alert", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "member")
                        .WithMany("Alerts")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GYMFeeManagement_System_BE.Entities.TrainingProgram", "TrainingProgram")
                        .WithMany("Alerts")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("TrainingProgram");

                    b.Navigation("member");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.EnrollProgram", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "member")
                        .WithMany("EnrollPrograms")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GYMFeeManagement_System_BE.Entities.TrainingProgram", "TrainingProgram")
                        .WithMany("EnrollPrograms")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TrainingProgram");

                    b.Navigation("member");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Member", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Branch", "Branch")
                        .WithMany("Members")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GYMFeeManagement_System_BE.Entities.Staff", "Staff")
                        .WithMany("Members")
                        .HasForeignKey("TrainerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Branch");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Payment", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "member")
                        .WithMany("Payments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("member");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Request", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "member")
                        .WithMany("Requests")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("GYMFeeManagement_System_BE.Entities.TrainingProgram", "trainingProgram")
                        .WithMany("Requests")
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("member");

                    b.Navigation("trainingProgram");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Review", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "Member")
                        .WithMany("Reviews")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Staff", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Branch", "Branch")
                        .WithMany("Staffs")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.TrainingProgram", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.ProgramType", "ProgramType")
                        .WithMany("TrainingPrograms")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ProgramType");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.WorkoutEnrollment", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Member", "Member")
                        .WithMany("WorkoutEnrollments")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("GYMFeeManagement_System_BE.Entities.Staff", null)
                        .WithMany("WorkoutEnrollments")
                        .HasForeignKey("StaffId");

                    b.HasOne("GYMFeeManagement_System_BE.Entities.WorkoutPlan", "WorkoutPlan")
                        .WithMany("WorkoutEnrollments")
                        .HasForeignKey("WorkoutPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("WorkoutPlan");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.WorkoutPlan", b =>
                {
                    b.HasOne("GYMFeeManagement_System_BE.Entities.Staff", "Staff")
                        .WithMany("WorkoutPlans")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Branch", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Members");

                    b.Navigation("Staffs");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Member", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Alerts");

                    b.Navigation("EnrollPrograms");

                    b.Navigation("Payments");

                    b.Navigation("Requests");

                    b.Navigation("Reviews");

                    b.Navigation("WorkoutEnrollments");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.ProgramType", b =>
                {
                    b.Navigation("TrainingPrograms");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Request", b =>
                {
                    b.Navigation("Address");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.Staff", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Members");

                    b.Navigation("WorkoutEnrollments");

                    b.Navigation("WorkoutPlans");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.TrainingProgram", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("EnrollPrograms");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("GYMFeeManagement_System_BE.Entities.WorkoutPlan", b =>
                {
                    b.Navigation("WorkoutEnrollments");
                });
#pragma warning restore 612, 618
        }
    }
}