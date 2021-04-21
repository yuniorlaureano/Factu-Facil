using FactuFacil.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactuFacil.Service
{
    public static class FactuFacilContextExtensions
    {
        public static T ExcludeProperties<T>(this FactuFacilContext context, T entity, bool isModified, params string[] properies)
        {
            var entry = context.Entry(entity);
            foreach (var p in properies)
            {
                entry.Property(p).IsModified = isModified;
            }
            return entity;
        }

        public static List<T> ExcludeProperties<T>(this FactuFacilContext context, List<T> entities, bool isModified, params string[] properies)
        {
            foreach (var entity in entities)
            {
                var entry = context.Entry(entity);
                foreach (var p in properies)
                {
                    entry.Property(p).IsModified = isModified;
                }
            }
            return entities;
        }
    }
}
