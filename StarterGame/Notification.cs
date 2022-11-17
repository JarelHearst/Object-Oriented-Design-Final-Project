using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    public class Notification
    {
        //name of notification
        public String Name { get; set; }
        //cam be any object
        public Object Object { get; set; }
        //infortmation for the user
        public Player Player { get; set; }
        public Dictionary<String, Object> UserInfo { get; set; }
        //Constructors for a notification. 
        public Notification() : this("NotificationName")
        {
        }

        public Notification(String name) : this(name, null)
        {
        }

        public Notification(String name, Object obj) : this(name, obj, null)
        {
        }
        //Designated Constructor.
        public Notification(String name, Object obj, Dictionary<String, Object> userInfo)
        {
            this.Name = name;
            this.Object = obj;
            this.UserInfo = userInfo;
        }
    }
}
