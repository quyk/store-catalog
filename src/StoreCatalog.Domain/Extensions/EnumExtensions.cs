using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace StoreCatalog.Domain.Extensions
{
    /// <summary>
    /// Usefull enum extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the string value of the <see cref="DescriptionAttribute"/>
        /// </summary>
        /// <param name="value">A <see cref="Enum"/> type</param>
        /// <returns>The description value</returns>
        public static string GetDescription(this Enum value)
        {
            Type genericEnumType = value.GetType();

            MemberInfo[] memberInfo = genericEnumType.GetMember(value.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }

            return value.ToString();
        }
    }
}
