﻿namespace Barbershop.API.Models
{
    public class Client
    {
        public Client(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            CreatedAt = DateTime.Now;
            Appointments = new List<Appointment>();
        }
        public void UpdateName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public void UpdateEmail(string email)
        {
            Email = email;
        }
        public void UpdatePhone(string phone)
        {
            Phone = phone;
        }
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set;}
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public IList<Appointment> Appointments { get; private set; } 
    }
}
