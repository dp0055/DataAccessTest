using System.Collections.Generic;

namespace DataAccessTest.Models
{
    public class OutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Film { get; set; }
        public List<string> Cars { get; set; }
    }
}
