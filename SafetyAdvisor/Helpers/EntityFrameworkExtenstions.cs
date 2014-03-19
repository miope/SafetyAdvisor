using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace SafetyAdvisor.Helpers
{
    public static class EntityFrameworkExtenstions
    {
        public static void SetFieldsAsModified<TEntity>(this DbEntityEntry<TEntity> dbEntry, params string[] fields) where TEntity : class
        {
            fields.ToList().ForEach(f => dbEntry.Property(f).IsModified = true);
        }
    }
}