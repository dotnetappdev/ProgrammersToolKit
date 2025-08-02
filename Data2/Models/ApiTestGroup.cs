using System.Collections.Generic;

namespace ProgrammersToolKit.Core
{
    public class ApiTestGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApiTestDefinition> Tests { get; set; }
    }
}
