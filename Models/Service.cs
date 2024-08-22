namespace Barbershop.API.Models
{
    public class Service
    {
        public Service(string name)
        {
            Name = name;
            Appointments = new List<Appointment>();
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public virtual IList<Appointment> Appointments { get; private set; }
    }
}
