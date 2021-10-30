using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateProperties.Model
{
    public class TableField
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 是否为NULL
        /// </summary>
        public int FieldIsNullable { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public short Length { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 字段数据类型
        /// </summary>
        public string FieldNotes { get; set; }

    }
}
