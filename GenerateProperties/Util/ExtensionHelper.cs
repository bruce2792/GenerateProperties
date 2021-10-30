using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateProperties.Util
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// 字符串转换decimal
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            decimal d;
            decimal.TryParse(str, out d);
            return d;
        }

        public static DateTime? ToDateTime(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return null;
            DateTime d;
            DateTime.TryParse(str, out d);
            return d;
        }

        public static short ToSmallInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            short s;
            short.TryParse(str, out s);
            return s;
        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            int i;
            int.TryParse(str, out i);
            return i;
        }

        ///// <summary>
        ///// 给泛型赋值
        ///// </summary>
        ///// <param name="pi"></param>
        ///// <param name="assemblyObj"></param>
        ///// <param name="value"></param>
        //public static void _SetValue(this PropertyInfo pi, object assemblyObj, string value)
        //{
        //    if (pi.PropertyType.Equals(typeof(String)))//判断属性的类型是不是String
        //    {
        //        String _name = pi.Name;
        //        pi.SetValue(assemblyObj, value, null);//给泛型的属性赋值
        //    }
        //}



    }
}
