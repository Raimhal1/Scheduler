using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySchedulerWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; private set; }

        private List<DayEvent> events = new List<DayEvent>();

        public List<DayEvent> Events
        {
            get
            {
                return events;
            }
        }


        public User()
        {
            Id = 0;
            UserName = "";
        }

        public User(int id, string userName) : this()
        {
            this.Id = id;
            UserName = userName;
        }

        public void addUserToEvent(DayEvent Event)
        {
            Event.members.Add(this);
            Events.Add(Event);
        }

        public void removeUserToEvent(DayEvent Event)
        {
            Event.members.Remove(this);
        }

    }
}
