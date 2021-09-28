using System;

namespace MyScheduler
{
    public class User
    {
        public string UserName { get; private set; }

        public User()
        {
            UserName = "";
        }

        public User(string userName) : this()
        {
            UserName = userName;
        }

        public void addUserToEvent(DayEvent Event)
        {
            Event.members.Add(this);
        }

        public void removeUserToEvent(DayEvent Event)
        {
            Event.members.Remove(this);
        }

    }
}
