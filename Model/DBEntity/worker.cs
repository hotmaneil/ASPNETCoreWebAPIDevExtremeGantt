using Dapper;

namespace DataModel.DBEntity
{
    public class worker
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }

        [Editable(false)]
        public string text { get; set; }
    }
}
