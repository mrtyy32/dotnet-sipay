using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

		public static IEnumerable<KeyValuePair<string, string>> FormKeyValues(object obj)
		{
			var formValues = new List<KeyValuePair<string, string>>();

			var objectValues = obj.GetType().GetProperties()
				.ToList();

			foreach (var item in objectValues)
			{
				if (item.Name != "BasketItems")
				{
					var jsonName = item.GetCustomAttributes<JsonPropertyAttribute>().FirstOrDefault().PropertyName ?? "";
					var formItem = new KeyValuePair<string, string>(jsonName, $"{item.GetValue(obj)}");
					formValues.Add(formItem);
				}
			}

			return formValues;
		}
	}
}