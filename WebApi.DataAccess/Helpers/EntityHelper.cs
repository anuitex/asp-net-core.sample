using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApi.DataAccess.Extensions
{
    public static class EntityHelper
    {
        public static string GetEntityTableName<IBaseEntity>()
        {
            if (typeof(IBaseEntity).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() is TableAttribute tableAttribute)
            {
                return tableAttribute.Name;
            }
            else
            {
                throw new TypeLoadException($"Attribute {typeof(TableAttribute).Name} should be specified for the {typeof(IBaseEntity).Name} entity.");
            }
        }
    }
}
