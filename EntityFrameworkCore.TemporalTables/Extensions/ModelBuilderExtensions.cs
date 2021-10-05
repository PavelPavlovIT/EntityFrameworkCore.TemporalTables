using EntityFrameworkCore.TemporalTables.Attributes;
using EntityFrameworkCore.TemporalTables.Cache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EntityFrameworkCore.TemporalTables.Extensions
{
    public static class ModelBuilderExtensions
    {

        private static bool GetTemporalTablesAttribute(Type type)
        {
            TemporalTablesAttribute CustomAttribute = (TemporalTablesAttribute)Attribute.GetCustomAttribute(type, typeof(TemporalTablesAttribute));

            if (CustomAttribute != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Configure all tables to be temporal by default.
        /// You may skip this method and register them manually by using <see cref="EntityTypeBuilderExtensions.UseTemporalTable" /> method.
        /// </summary>
        public static void UseTemporalTables(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .ToList();

            foreach (var entityType in entityTypes)
            {
                if (GetTemporalTablesAttribute(entityType.ClrType))
                    TemporalEntitiesCache.Add(entityType);
            }
        }

        /// <summary>
        /// Configure none of the tables to be temporal by default.
        /// You can still register them manually by using <see cref="EntityTypeBuilderExtensions.UseTemporalTable" /> method.
        /// </summary>
        public static void PreventTemporalTables(this ModelBuilder modelBuilder)
        {
            var entityTypes = modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .ToList();

            foreach (var entityType in entityTypes)
            {
                TemporalEntitiesCache.Remove(entityType);
            }
        }
    }
}
