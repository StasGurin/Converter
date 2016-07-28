using MongoDB.Bson;

namespace Visitors.Models
{
    public class DomainMask
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
    }
}