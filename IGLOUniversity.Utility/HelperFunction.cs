using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Utility
{
    public class HelperFunction
    {
        public static void MappingModel<TDest, Tsource>(TDest destination, Tsource source)
            where TDest : class
            where Tsource : class
        {
            var destinationProperties = destination.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                PropertyInfo? property = destinationProperties.FirstOrDefault(a => a.Name == sourceProperty.Name);
                if (property != null)
                {
                    property.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }

        public static string GetFullName(string firstName, string? middleName, string? lastName)
        {
            return $"{firstName + " "}{middleName + " "}{lastName}";
        }
    }
}
