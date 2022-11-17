using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    //An interface for room delegation, used for delegate pattern.
    public interface RoomDelegate
    {
        Door GetExit(string exitName);
        //Containing room is the where the effects of the room take place.
        Room ContainingRoom { set; get; }
        //Dictionary of exits.
        Dictionary<string, Door> Exits { get; set; }
        string Description();
    }

    //The traproom which has a password with unlimited tries but you cannot move anywhere.
    public class TrapRoom : RoomDelegate
    {
        private Door _trickDoor;
        private string _password;
        private string _hint;
        public string Password { set { _password = value; } }
        public string Hint { set { _hint = value; } }
        public Dictionary<string, Door> Exits { get; set; }
        //Constructor for Traproom
        public TrapRoom()
        {
            Hint = "";
            Password = "";
            NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
        }

        //Getting exit for the trap room when escaped.
        public Door GetExit(string exitName)
        {
            return _trickDoor;
        }
        private Room _containRoom;
        //Constructor for the containing room.
        public Room ContainingRoom
        {
            set
            {
                _containRoom = value;
                _trickDoor = new Door(_containRoom, _containRoom);
            }
            get
            {
                return _containRoom;
            }
        }
        //A notification that the player did say a word.
        public void PlayerDidSayWord(Notification notificaion)
        {
            Player player = (Player)notificaion.Object;
            if (player.CurrentRoom == ContainingRoom)
            {

                Dictionary<string, Object> userInfo = notificaion.UserInfo;
               
                string word = (string)userInfo["word"];
                if (word.Equals(_password))
                {
                    player.OutputMessage("You said the correct password.");
                    ContainingRoom.Delegate = null;
                }
                else
                {
                    player.OutputMessage("That is not the right password!");
                    player.InfoMessage("Hint: " + _hint);

                }
            }
        }

        //Description of trap room.
        public string Description()
        {
            return "You are now trapped";
        }
    }

    //A class for the winning room. It is a room delegate.
    public class WinningRoom : RoomDelegate
    {
        private int tries;
        private Door _winDoor;
        private string _password;
        public string Password { set { _password = value; } }
        public Dictionary<string, Door> Exits { get; set; }
        //Constructor for the winning room.
        public WinningRoom()
        {
            Password = "";
            NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
        }
        //Getting all of the exits for the winning room.
        public Door GetExit(string exitName)
        {
            Exits.TryGetValue(exitName, out _winDoor);
            return _winDoor;
        }
        private Room _containRoom;
        //Constructor for the containg room with the winning room effect.
        public Room ContainingRoom
        {
            set
            {
                _containRoom = value;
            }
            get
            {
                return _containRoom;
            }
        }
        //Notification to send if the player said a word in the winning room.
        public void PlayerDidSayWord(Notification notificaion)
        {
            Player player = (Player)notificaion.Object;
            if (player.CurrentRoom == ContainingRoom)
            {
                Dictionary<string, Object> userInfo = notificaion.UserInfo;
                string word = (string)userInfo["word"];
                if (word.Equals(_password))
                {
                    //If the player enters the correct word, he will win the game and leave.
                    player.GoodJobMessage("You have won the game! Good Job!");
                    Environment.Exit(0);
                    ContainingRoom.Delegate = null;
                }
                else
                {
                    //adding tries to the player's count, if the player incorrectly three times, it will exit the game forcefully.
                    tries += 1;
                    if(tries == 1)
                    {
                        player.WarningMessage("First try is extinguished, please try again and make sure you have all of the correct notes.");
                        player.InfoMessage("Tries: " + tries);
                    }
                    if(tries == 2)
                    {
                        player.ErrorMessage("Second try is evaporated, please try again. Remember to associate the letter with the number.");
                        player.ErrorMessage("Next try will be your last chance");
                        player.InfoMessage("Tries: " + tries);

                    }
                    if (tries == 3)
                    {
                        
                        player.GameOverMessage("The way to get out of the room has been sealed and you are stuck for eternity! :(");
                        Environment.Exit(0);
                    }
                }
            }
        }
        //Description for the win room.
        public string Description()
        {
            return "You are now in the room where you can win.";
        }
    }
   
    //A class where if you say a word, it will be repeated three times.
    public class EchoRoom : RoomDelegate{
        //Dictionary of exits.
        public Dictionary<string, Door> Exits { get; set; }
        private Room _containRoom;
        //A containing room for where echos will take place.
        public Room ContainingRoom
        {
            set
            {
                _containRoom = value;
            }
            get
            {
                return _containRoom;
            }
        }
        //A notification to be present if the user says a word in this room.
        public EchoRoom()
        {
            NotificationCenter.Instance.AddObserver("PlayerDidSayWord", PlayerDidSayWord);
        }
        //Gets all the exits for the echo room.
        public Door GetExit(string exitName)
        {
            Door door = null;
            Exits.TryGetValue(exitName, out door);
            return door;
        }

        //A description of the echo room.
        public string Description()
        {
            return "You are now in the echo room.";
        }

        //A notification that is sent when a word is sent by the player.
        public void PlayerDidSayWord(Notification notificaion)
        {
            Player player = (Player)notificaion.Object;
            if (player.CurrentRoom == ContainingRoom)
            {
                Dictionary<string, Object> userInfo = notificaion.UserInfo;
                string word = (string)userInfo["word"];
                player.OutputMessage(word + "..." + word + "..." + word + "...");
        }
    }              
}
    //The base class for room.
    public class Room{
        //"exits" contains all the exits in the rooms.
        private Dictionary<string, Door> exits;
        private string _tag;
        public bool playerWasHere = false;
        public string Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        private RoomDelegate _delagate;
        public RoomDelegate Delegate
        {
            set
            {
                _delagate = value;
                if(_delagate != null)
                {
                    _delagate.Exits = exits;
                }
            }
        }
        //private IItem _item;
        private ItemContainer _items;

        public Room() : this("No Tag")
        {

        }

        //Designated constructor for room.
        public Room(string tag)
        {
            exits = new Dictionary<string, Door>();
            this.Tag = tag;
            _delagate = null;
            _items = new ItemContainer("Floor", 0f);
        }

        public void SetExit(string exitName, Door door)
        {
            //Set the exit and put it into the dictionary.
            exits[exitName] = door;
        }
        //Getting all exits for a room from a door.
        public Door GetExit(string exitName)
        {


            if (_delagate == null)
            {
                Door door = null;
                exits.TryGetValue(exitName, out door);
                return door;
            }
            else
            {
                return _delagate.GetExit(exitName);
            }
        }

        //Getting all exits for a room in a word.
        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Door>.KeyCollection keys = exits.Keys;
            //take all the keys and concatenate it.
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }
        //Puts the item back into the room.
        public void Drop(IItem item)
        {
            _items.AddItem(item);
        }
        //Removing an item form a room.
        public IItem Remove(String itemName)
        {
            IItem itemToReturn = _items.RemoveItem(itemName);
            return itemToReturn;
        }


        //Get a description of the room you are in and get the exits to get out of.
        public string Description()
        {
            return (_delagate == null ? "" : _delagate.Description() + "\n") + "You are in " + this.Tag + ".\n *** " + this.GetExits() + "\n" + _items.LongDescription;
        }
       
    }
}