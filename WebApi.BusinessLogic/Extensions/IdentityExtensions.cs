using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebApi.BusinessLogic.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetErrors(this IEnumerable<IdentityError> identityErrors)
        {
            var stringBuilder = new StringBuilder();

            foreach (IdentityError item in identityErrors)
            {
                stringBuilder.AppendLine(item.Description);
            }

            string errors = stringBuilder.ToString();

            return errors;
        }
    }
}
