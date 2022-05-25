using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Util
{
    public class JsonHelper
    {
        //递归遍历json节点，把需要定义的类存入classes
        public static void GetClasses(JToken jToken, Dictionary<string, string> classes)
        {
            if (jToken is JValue)
            {
                return;
            }
            var childToken = jToken.First;
            while (childToken != null)
            {
                if (childToken.Type == JTokenType.Property)
                {
                    var p = (JProperty)childToken;
                    var valueType = p.Value.Type;

                    if (valueType == JTokenType.Object)
                    {
                        var className = p.Name.First().ToString().ToUpper() + p.Name.Substring(1);
                      
                        classes.Add(className, GetClassDefinion(p.Value));
                        GetClasses(p.Value, classes);
                    }
                    else if (valueType == JTokenType.Array)
                    {
                        foreach (var item in (JArray)p.Value)
                        {
                            if (item.Type == JTokenType.Object)
                            {
                                var className = p.Name.First().ToString().ToUpper() + p.Name.Substring(1);
                                if (!classes.ContainsKey(className))
                                {
                                    classes.Add(className, GetClassDefinion(item));
                                }

                                GetClasses(item, classes);
                            }
                        }
                    }
                }

                childToken = childToken.Next;
            }
        }


        //获取类中的所有的属性
        public static string GetClassDefinion(JToken jToken)
        {
            if (!jToken.HasValues) { return string.Empty; }
            StringBuilder sb = new StringBuilder();
            var subValueToken = jToken.First;
            while (subValueToken != null)
            {
                if (subValueToken.Type == JTokenType.Property)
                {
                    var p = (JProperty)subValueToken;
                    var fieldName = p.Name.ToLower() == "id" ? "ID" : p.Name.First().ToString().ToUpper() + p.Name.Substring(1);
                    var valueType = p.Value.Type;
                    if (valueType == JTokenType.Object)
                    {
                        sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public {fieldName} {fieldName} {{get;set;}}\r\n\r\n");

                    }
                    else if (valueType == JTokenType.Array)
                    {
                        var arr = (JArray)p.Value;
                        //a.First
                        // properties += $"        /// <summary>\r\n        /// {item.FieldNotes}\r\n        /// </summary>\r\n        public string {item.FieldName} {{get;set;}}\r\n\r\n";

                        switch (arr.First.Type)
                        {
                            case JTokenType.Object:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public List<{fieldName}> {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.Integer:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public List<int> {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.Float:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public List<float> {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.String:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public List<string> {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.Boolean:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public List<bool> {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (valueType)
                        {
                            case JTokenType.Integer:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public int {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.Float:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public float {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.String:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public string {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            case JTokenType.Boolean:
                                sb.Append($"        /// <summary>\r\n        /// \r\n        /// </summary>\r\n        public bool {fieldName} {{get;set;}}\r\n\r\n");
                                break;
                            default:
                                break;
                        }
                    }
                }

                subValueToken = subValueToken.Next;
            }

            return sb.ToString();
        }
    }
}
