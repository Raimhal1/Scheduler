using System;
using System.Collections.Generic;

namespace MyScheduler
{
    public class User
    {
        public int id { get; private set; }
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
            id = 0;
            UserName = "";
        }

        public User(int id, string userName) : this()
        {
            this.id = id;
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
