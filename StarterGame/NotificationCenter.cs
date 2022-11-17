using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class NotificationCenter
    {
        private Dictionary<String, EventContainer> observers;
        private static NotificationCenter _instance;
        public static NotificationCenter Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new NotificationCenter();
                }
                return _instance;
            }
        }
        public NotificationCenter()
        {
            observers = new Dictionary<String, EventContainer>();
        }

        private class EventContainer
        {
            private event Action<Notification> Observer;
            public EventContainer()
            {
            }
            //Observer pattern, used when adding or removing a notification from the game world.
            
            //Adding a notification.
            public void AddObserver(Action<Notification> observer)
            {
                Observer += observer;
            }
            //Removing a notification.
            public void RemoveObserver(Action<Notification> observer)
            {
                Observer -= observer;
            }
            //Sends a notification to the user.
            public void SendNotification(Notification notification)
            {
                //Call each one of the observers (Event)
                Observer(notification);
            }
            //If nothing is in the notifcation, don't send anything.
            public bool IsEmpty()
            {
                return Observer == null;
            }
        }
        //Sends notification name and the action
        public void AddObserver(String notificationName, Action<Notification> observer)
        {
            if(!observers.ContainsKey(notificationName))
            {
                observers[notificationName] = new EventContainer();
            }
            observers[notificationName].AddObserver(observer);
        }

        //Removes notification from the game.
        public void RemoveObserver(String notificationName, Action<Notification> observer)
        {
            if(observers.ContainsKey(notificationName))
            {
                observers[notificationName].RemoveObserver(observer);
                if(observers[notificationName].IsEmpty())
                {
                    observers.Remove(notificationName);
                }
            }
        }
        //generates the notification
        public void PostNotification(Notification notification)
        {
            if(observers.ContainsKey(notification.Name))
            {
                observers[notification.Name].SendNotification(notification);
            }
        }
        //generates the notification with a word and a player.
        public void PostNotification(string notificationS, Player player)
        {
            Notification notification = new Notification(notificationS);
            notification.Player = player;
            if (observers.ContainsKey(notification.Name))
            {
                observers[notification.Name].SendNotification(notification);
            }
        }
    }
}
