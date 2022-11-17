using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    //All the actions that the player can make are here.
    public class Player
    {
        //Stack called path.
        private Stack<Room> Path = new Stack<Room>();
        private Room _currentRoom = null;
        private float initCap = 0; //How much the player has at the start
        private float maxCap = 50; //How much the player can hold at a time.
        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }

        //The player's inventory
        private ItemContainer _inventory;

        //Designated Constructor for player.
        public Player(Room room)
        {
            _currentRoom = room;
            _inventory = new ItemContainer("Inventory", 0f);
        }
        //A way to give the player an item. 
        public void Give(IItem item)
        {
            _inventory.AddItem(item);
            
        }

        //A way to take an item away from the player's inventory.
        public IItem Take(String itemName)
        {
            return _inventory.RemoveItem(itemName);
        }

        //A method to drop an item in a room. 
        public void Drop(string itemName)
        {
            IItem item = Take(itemName);
            if(item != null)
            {
                CurrentRoom.Drop(item);
            }
            else
            {
                OutputMessage("The item " + itemName + " is not in your inventory");
            }
        }
        //Showing all of the items in an inventory. 
        public void ShowInventory()
        {
            _inventory.ShowItems();
        }

        //A method that allows the player to grab an item.
        public void Grab(string itemName)
        {
            IItem item = CurrentRoom.Remove(itemName);
            if(item != null)
            {
                //If the weight of the item that they want to pick up and the player's weight since the-
                //start of the game is bigger than the maximum capacity(50), force the player to drop the item.
                if (item.Weight + initCap > maxCap)
                {
                    Console.WriteLine("This item cannot be picked up. " + itemName + " would exceed your maximum inventory of " + maxCap);
                    CurrentRoom.Drop(item);
                }
                else
                {
                    //Add the item into the inventory and update the initial weight each time it is successfully grabbed.
                    initCap += item.Weight;
                    Console.WriteLine("You have picked up a " + itemName);
                    Give(item);
                    Console.WriteLine("You now have " + initCap + " out of " + maxCap + " of space left in your inventory");
                }
            }
            //If an item is not in an room.
            else
            {
                OutputMessage("The item " + itemName + " is not in the room");
            }
        }

        //this is the player's movement across the world.
        public void WaltTo(string direction)
        {
            Door door = this._currentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsOpen)
                {
                    Room nextRoom = door.OtherRoom(CurrentRoom);
                    Notification notification = new Notification("PlayerWillExitRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.Path.Push(this._currentRoom);
                    this._currentRoom = nextRoom;
                    notification = new Notification("PlayerDidExitRoom", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    this.OutputMessage("\n" + this._currentRoom.Description());
                    this.toRoom(nextRoom);
                }
                else 
                {
                    this.OutputMessage("\nThe door on " + direction + " is closed.");
                }
                
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);
            }
        }

        //The method for the "BackCommand" class.
        public bool BackTo()
        {
            if (this.Path.Count == 0)
            {
                return false;
            }
            //Pop out the last room.
            Room lastRoom = this.Path.Pop();
            this.OutputMessage("Going back");
            this._currentRoom = lastRoom;

            /*Notification notification = new Notification("PlayerWillExitRoom", this);
            NotificationCenter.Instance.PostNotification(notification);
            notification = new Notification("PlayerDidExitRoom", this);
            NotificationCenter.Instance.PostNotification(notification);*/
            this.OutputMessage("\n" + this._currentRoom.Description());
            this.toRoom(lastRoom);
            return true;
        }

        //Notification for if you successfully entered a new room with back command
        public void toRoom(Room room)
        {
            this._currentRoom = room;

            NotificationCenter.Instance.PostNotification("PlayerEnteredRoom", this);
            if (!this._currentRoom.playerWasHere)
            {
                NotificationCenter.Instance.PostNotification("PlayerEnteredNewRoom", this);
            }
            this._currentRoom.playerWasHere = true;
        }

        //Telling the player to open the door when given the chance.
        public void Open(string direction)
        {
            Door door = this._currentRoom.GetExit(direction);
            if(door != null)
            {
                if (door.IsClosed)
                {
                    door.Open();
                    if (door.IsOpen)
                    {
                        this.OutputMessage("\nThe door on " + direction + " is now open");
                    }
                    else
                    {
                        this.OutputMessage("\nThe door on " + direction + " is still closed");
                    }
                }
                else
                {
                    this.OutputMessage("\nThe door on " + direction + " is already opened");

                }
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);

            }
        }

        //A method for the player to say a word.
        public void Say(String word)
        {
            OutputMessage("\n" + word + "\n");
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["word"] = word;
            Notification notification = new Notification("PlayerDidSayWord", this, userInfo);
            NotificationCenter.Instance.PostNotification(notification);
        }

        //A method of how the player can inspect an item.
        public void Inspect(string itemName)
        {
            IItem item = this._currentRoom.Remove(itemName);
            if (item != null)
            {
                InfoMessage(item.LongDescription);
                //Putting back the item
                CurrentRoom.Drop(item);
            }
            else
            {
                this.OutputMessage("\nThere is no such item in the room");

            }
        }
        //A method of how the player can unlock a door.
        public void Unlock(string direction)
        {
            Door door = this._currentRoom.GetExit(direction);
            if (door != null)
            {
                if (door.IsLocked)
                {
                    door.Unlock();
                    if (door.IsUnlocked)
                    {
                        this.OutputMessage("\nThe door on " + direction + " is now unlocked");
                    }
                    else
                    {
                        this.OutputMessage("\nThe door on " + direction + " is still locked");
                    }
                }
                else
                {
                    this.OutputMessage("\nThe door on " + direction + " is already unlocked");

                }
            }
            else
            {
                this.OutputMessage("\nThere is no door on " + direction);

            }
        }
        //How we give a player a message about the game's events.
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        //A message given with color for a specofic event. 
        public void ColoredMessage(string message, ConsoleColor color) {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }
        //A message that is given in red, meaning grave error or mistake.
        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }

        //A message given in yellow, meaning warning ahead.
        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Yellow);
        }

        //A message given in blue, meaning helpful information is given.
        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Blue);
        }

        //A message given in dark blue, meaning dangerous information is given.
        public void BitterInfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkBlue);
        }
        //A message given in green, meaning good things are happening. 
        public void GoodJobMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Green);
        }

        //A message in purple, you have lost if you see this color in a message.
        public void GameOverMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkMagenta);
        }
    }

}
