using EntityFrameworkCore.TemporalTables.Attributes;

namespace EntityFrameworkCore.TemporalTables.TestApp.Entities
{
    [TemporalTables]
    public class Role
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
