using JsonToCSharp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Util
{
    internal static class ExtendFunc
    {

        /// <summary>
        /// 扩展方法，根据下标设置属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyIndex"></param>
        /// <param name="propertyVallue"></param>
        public static void SetPropertyValue1<T>(this T obj, int propertyIndex, object propertyVallue) where T : class
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            // 转换值类型以匹配属性类型
            //object convertedValue = Convert.ChangeType(propertyVallue, properties[propertyIndex].PropertyType);
            properties[propertyIndex].SetValue(obj, ConvertValue(propertyVallue, properties[propertyIndex].PropertyType), null);
        }

        public static object ConvertValue(object value, Type targetType)
        {
            if (value == null)
            {
                return targetType.IsValueType &&
                       !(targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    ? Activator.CreateInstance(targetType)
                    : null;
            }

            // 如果已经是目标类型，直接返回
            if (targetType.IsInstanceOfType(value))
            {
                return value;
            }

            // 处理枚举类型 - 根据 Description 返回对应的枚举值
            if (targetType.IsEnum)
            {
                // 尝试通过 Description 查找枚举值
                if (value is string stringValue && !string.IsNullOrEmpty(stringValue))
                {
                    object enumValue = GetEnumValueFromDescription(targetType, stringValue);
                    if (enumValue != null)
                    {
                        return enumValue;
                    }

                    // 如果通过 Description 找不到，尝试按名称解析枚举
                    try
                    {
                        return Enum.Parse(targetType, stringValue, true);
                    }
                    catch
                    {
                        // 如果按名称解析失败，尝试按数值解析
                        if (int.TryParse(stringValue, out var intValue))
                        {
                            return Enum.ToObject(targetType, intValue);
                        }

                        throw new InvalidCastException($"无法将值 '{value}' 转换为枚举类型 {targetType.Name}");
                    }
                }
                // 如果值是数值类型，直接转换为枚举
                else if (value is int || value is short || value is byte || value is long)
                {
                    return Enum.ToObject(targetType, value);
                }
                else
                {
                    throw new InvalidCastException($"无法将值 '{value}' 转换为枚举类型 {targetType.Name}");
                }
            }

            // 处理可空枚举类型
            if (targetType.IsGenericType &&
                targetType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                Nullable.GetUnderlyingType(targetType).IsEnum)
            {
                Type underlyingType = Nullable.GetUnderlyingType(targetType);

                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }

                return ConvertValue(value, underlyingType);
            }

            // 处理 Guid 类型
            if (targetType == typeof(Guid) || targetType == typeof(Guid?))
            {
                if (value is string stringValue && !string.IsNullOrEmpty(stringValue))
                {
                    return Guid.Parse(stringValue);
                }
                throw new InvalidCastException($"无法将值 '{value}' 转换为 Guid 类型");
            }

            // 处理可空类型
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type underlyingType = Nullable.GetUnderlyingType(targetType);
                return string.IsNullOrEmpty(value.ToString()) ? null : ConvertValue(value, underlyingType);
            }

     


            // 默认使用 Convert.ChangeType
            return Convert.ChangeType(value, targetType);
        }

        // 获取枚举值的 Description
        private static string GetEnumDescription(object value)
        {
            if (value == null) return null;

            Type type = value.GetType();
            if (!type.IsEnum) return null;

            string name = Enum.GetName(type, value);
            if (name == null) return null;

            FieldInfo field = type.GetField(name);
            if (field == null) return null;

            DescriptionAttribute attr = field.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description;
        }

        // 通过 Description 查找枚举值
        private static object GetEnumValueFromDescription(Type enumType, string description)
        {
            if (!enumType.IsEnum) return null;

            foreach (var field in enumType.GetFields())
            {
                if (field.FieldType != enumType) continue;

                DescriptionAttribute attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr != null && attr.Description == description)
                {
                    return field.GetValue(null);
                }
            }

            return null;
        }

    }
}
