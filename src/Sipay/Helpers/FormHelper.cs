using System.Linq;

namespace Sipay.Helpers
{
    internal class FormHelper
    {
        public static string ToKeyValueURL(object obj)
        {
            var keyvalues = obj.GetType().GetProperties()
                .ToList()
                .Select(p => $"{p.Name} = {p.GetValue(obj)}")
                .ToArray();

            return string.Join("&", keyvalues);
        }
    }
}