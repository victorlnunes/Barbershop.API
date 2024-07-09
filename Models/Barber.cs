namespace Barbershop.API.Models
{
    public class Barber
    {
        public Barber(string firstName, string lastName, string email, string phone, string bio)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Bio = bio;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Bio { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public virtual IList<Appointment> Appointments { get; private set; }
    }
}
