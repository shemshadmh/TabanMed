﻿using System.Linq.Expressions;
using System.Reflection;
using Application.Interfaces.Application;
using Common;
using Domain.Common;
using Domain.Entities.Destination;
using Domain.Entities.Destination.Translation;
using Domain.Entities.Hotels;
using Domain.Entities.Hotels.Translation;
using Domain.Entities.Identity;
using Domain.Entities.Localization;
using Domain.Entities.MedicalCenters;
using Domain.Entities.MedicalCenters.Translation;
using Domain.Entities.Permission;
using Domain.Entities.TourServices;
using Domain.Entities.TourServices.Translation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, string, UserClaim,
        UserRole, UserLogin, RoleClaim, UserToken>, IApplicationDbContext
    {
        private readonly ICurrentServices _currentServices;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(ICurrentServices currentServices, IDateTime dateTime,
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _currentServices = currentServices;
            _dateTime = dateTime;
        }

        #region Permission

        public DbSet<Permission> Permission => Set<Permission>();

        #endregion

        #region Destination

        public DbSet<City> Cities => Set<City>();
        public DbSet<CityTranslation> CityTranslations => Set<CityTranslation>();
        public DbSet<Country> Countries => Set<Country>();
        public DbSet<CountryTranslation> CountriesTranslation => Set<CountryTranslation>();

        #endregion

        #region Hotel

        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<HotelTranslation> HotelTranslations => Set<HotelTranslation>();
        public DbSet<HotelFacility> HotelFacilities => Set<HotelFacility>();
        public DbSet<HotelSelectedFacility> HotelSelectedFacilities => Set<HotelSelectedFacility>();
        public DbSet<HotelImage> HotelImages => Set<HotelImage>();
        public DbSet<HotelFacilityTranslation> HotelFacilityTranslations => Set<HotelFacilityTranslation>();

        #endregion

        #region MedicalCenter
        public DbSet<MedicalCenter> MedicalCenters => Set<MedicalCenter>();
        public DbSet<MedicalCenterTranslation> MedicalCenterTranslations=> Set<MedicalCenterTranslation>();
        public DbSet<MedicalCenterMedicalService> MedicalCenterMedicalServices=> Set<MedicalCenterMedicalService>();
        public DbSet<MedicalService> MedicalServices=> Set<MedicalService>();
        public DbSet<MedicalServiceTranslation> MedicalServiceTranslations=> Set<MedicalServiceTranslation>();


        #endregion

        #region Localization

        public DbSet<Language> Languages => Set<Language>();

        #endregion

        #region TourService

        public DbSet<TourService> TourServices => Set<TourService>();
        public DbSet<TourServiceTranslation> TourServiceTranslations => Set<TourServiceTranslation>();


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if(tableName!.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName[6..]);
                }
            }

            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if(typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(nameof(AuditableEntity.Created))
                        .HasColumnType(
                            ModelConstants.Shared.SmallDatetimeColumnType) 
                        .IsRequired();

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<string?>(nameof(AuditableEntity.CreatedBy))
                        .HasColumnType(ModelConstants.Shared.VarCharColumnType) 
                        .HasMaxLength(50);
                }

                if(typeof(EditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(nameof(EditableEntity.LastModified))
                        .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType); 

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<string?>(nameof(EditableEntity.LastModifiedBy))
                        .HasColumnType(ModelConstants.Shared.VarCharColumnType) 
                        .HasMaxLength(50);
                }

                if(typeof(DeletableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(nameof(DeletableEntity.DeletedOn))
                        .HasColumnType(ModelConstants.Shared.SmallDatetimeColumnType); 

                    modelBuilder.Entity(entityType.ClrType)
                        .Property<string?>(nameof(DeletableEntity.DeletedBy))
                        .HasColumnType(ModelConstants.Shared.VarCharColumnType) 
                        .HasMaxLength(50);

                    // modify expression to handle soft delete query filter
                    var parameter = Expression.Parameter(entityType.ClrType);
                    Expression<Func<DeletableEntity, bool>> filterExpr = entity => !entity.IsDeleted;
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter,
                        filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    entityType.SetQueryFilter(lambdaExpression);
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentServices.Username ?? "Seed";
                        entry.Entity.Created = _dateTime.UtcNow;
                        break;
                }
            }

            foreach(var entry in ChangeTracker.Entries<EditableEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentServices.Username;
                        entry.Entity.LastModified = _dateTime.UtcNow;
                        break;
                }
            }

            foreach(var entry in ChangeTracker.Entries<DeletableEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedBy = _currentServices.Username;
                        entry.Entity.DeletedOn = _dateTime.UtcNow;
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }

            foreach(var entry in ChangeTracker.Entries<ApplicationUser>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentServices.Username ?? "seed";
                        entry.Entity.Created = _dateTime.UtcNow;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentServices.Username;
                        entry.Entity.LastModified = _dateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.DeletedBy = _currentServices.Username;
                        entry.Entity.DeletedOn = _dateTime.UtcNow;
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
