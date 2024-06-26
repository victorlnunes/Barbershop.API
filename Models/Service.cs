namespace Barbershop.API.Models
{
    public class Service
    {
        public Service(string name)
        {
            Name = name;
        }
        public int Id { get; private set; }
        public string Name { get; set; }
    }
}
