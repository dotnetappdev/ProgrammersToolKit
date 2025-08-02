using System.Collections.Generic;
using ProgrammersToolKit.Core;

namespace ProgrammersToolKit.Models
{
    public class ApiTestGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ApiTestDefinition> Tests { get; set; }
    }
}
