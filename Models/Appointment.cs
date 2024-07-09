namespace Barbershop.API.Models
{
    public class Appointment
    {
        public Appointment(DateTime schedule, Barber barber, Client client)
        {
            Schedule = schedule;
            Barber = barber;
            Client = client;
            CreatedAt = DateTime.Now;
        }
        public int Id { get; private set; }
        public DateTime Schedule { get; private set; }
        public virtual Barber Barber { get; private set; }
        public virtual Client Client { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

    }
}
