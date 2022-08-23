using Dapper;

namespace DataModel.DBEntity
{
    public class resourceassignment
    {
        [Key]
        public int id { get; set; }

        public int taskId { get; set; }

        public int resourceId { get; set; }
    }
}
