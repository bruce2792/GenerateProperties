using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties
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


        /// <summary>
        /// 使用 lambda 表达式设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="target"></param>
        /// <param name="memberLambda"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLambda, TValue value)
        {
            if (memberLambda.Body is MemberExpression memberSelectorExpression)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value);
                }
            }
        }

        /// <summary>
        /// 使用 lambda 表达式设置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="target"></param>
        /// <param name="memberLambda"></param>
        /// <param name="default">没有获取到值时的默认值</param>
        public static TValue GetPropertyValue<T, TValue>(this T target, Expression<Func<T, TValue>> memberLambda, TValue @default)
        {
            if (memberLambda.Body is MemberExpression memberSelectorExpression)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    return (TValue)property.GetValue(target);
                }
            }
            return @default;
        }


        //public static unsafe string ToUpperFirst(this string str)
        //{
        //    if (str == null) return null;
        //    string ret = string.Copy(str);
        //    fixed (char* ptr = ret)
        //        *ptr = char.ToUpper(*ptr);
        //    return ret;
        //}


    }
}
