using System.ComponentModel;

namespace Sipay.Models.Enums
{
    public enum SipayTransactionType
    {
        [Description("Auth")] Auth,
        [Description("Pre-Authorization")] PreAuth
    }

    public static class SipayTransactionTypeExtensions
    {
        public static string ToDescriptionString(this SipayTransactionType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}