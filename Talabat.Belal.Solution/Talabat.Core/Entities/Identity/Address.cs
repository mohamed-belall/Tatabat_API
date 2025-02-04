namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }

        // relationship 1:1
        // optional from address and mandatory from AppUser
        // take primary key of optional and put it as a foreign key @ mandatory 
        public string AppUserId { get; set; } // foreign key

    }
}