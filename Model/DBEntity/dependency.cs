using Dapper;

namespace DataModel.DBEntity
{
    public class dependency
    {
        [Key]
        public int id { get; set; }

        public int predecessorId { get; set; }

        public int successorId { get; set; }

        public int type { get; set; }
    }
}
