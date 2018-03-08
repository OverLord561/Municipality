using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Municipality.ViewModels.Enums
{
    public enum IncidentStatusesEnum : byte
    {
        New = 1,

        [Description("In Progress")]
        InProgress,

        Closed,

        Done,
        
    }


    public static class OrderStatusesExtensions
    {
        public static string GetDescription(this IncidentStatusesEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

}
