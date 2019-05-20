using System;
using System.Collections.Generic;
using WebApi.DataAccess.Entities;

namespace WebApi.DataAccess.Exceptions
{
    public class BulkOperationException: ApplicationException
    {
        public BulkOperationException(string message, IEnumerable<IBaseEntity> entitiesWithErrors) : base(message)
        {
            Entities = entitiesWithErrors;
        }

        public IEnumerable<IBaseEntity> Entities { get; private set; }
    }
}
