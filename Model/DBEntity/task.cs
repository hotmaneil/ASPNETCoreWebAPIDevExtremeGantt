using System;
using Dapper;

namespace DataModel.DBEntity
{
    public class task
    {
        [Key]
        public int id { get; set; }

        /// <summary>
        /// 父Id
        /// 0為自己是工作主項
        /// 1以上是工作細項
        /// </summary>
        public int parentId { get; set; }

        /// <summary>
        /// 工作任務名稱
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime start { get; set; }

        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime end { get; set; }

        /// <summary>
        /// 進度百分比
        /// </summary>
        public int progress { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        public int count { get; set; }
    }
}
