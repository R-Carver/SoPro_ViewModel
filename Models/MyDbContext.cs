using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Collections;

namespace WebApplication1.Models
{
    public class MyDbContext : IdentityDbContext<ContractUser>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contract>()
                        .HasRequired(c => c.ContractOwner)
                        .WithMany(o => o.OwnerContracts)
                        .HasForeignKey(c => c.OwnerId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                        .HasOptional(c => c.Dispatcher)
                        .WithMany(d => d.DispatcherContracts)
                        .HasForeignKey(c => c.DispatcherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                        .HasOptional(c => c.Coordinator)
                        .WithMany(d => d.CoordinatorContracts)
                        .HasForeignKey(c => c.CoordinatorId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                        .HasOptional(c => c.Department)
                        .WithMany(o => o.Contracts)
                        .HasForeignKey(c => c.DepartmentId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                        .HasOptional(c => c.UDepartment)
                        .WithMany(o => o.UContracts)
                        .HasForeignKey(c => c.UDepartmentId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                        .HasOptional(c => c.Dispatcher)
                        .WithMany(o => o.DispatcherDepartments)
                        .HasForeignKey(c => c.DispatcherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                            .HasOptional(c => c.StvDispatcher)
                            .WithMany(o => o.StvDispatcherDepartments)
                            .HasForeignKey(c => c.StvDispatcherId)
                            .WillCascadeOnDelete(false);

        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Mandant> Mandants { get; set; }
        public DbSet<MU_Relation> MU_Relations { get; set; }
        public DbSet<DU_Relation> DU_Relations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ContractFile> ContractFiles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ContractsKind> ContractKinds { get; set; }
        public DbSet<ContractsType> ContractTypes { get; set; }
        public DbSet<ContractsStatus> ContractStatuss { get; set; }

    }
}