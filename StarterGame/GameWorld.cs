using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    class GameWorld
    {
        //"Static" makes a variable belong to a class.
        private static GameWorld _instance = null;
        public static GameWorld Instance
        {
            get
            {
                //Lazy initalizer
                if(_instance == null)
                {
                    _instance = new GameWorld();
                }
                return _instance;
            }

        }
        //Entrance to the game world. The entrance is always the same.
        private Room _entrance;
        public Room Entrance
        {
            get
            {
                return _entrance;
            }
        }
        //Exit from the game world. Not used officially, exit is the win room.
        private Room _exit;
        public Room Exit
        {
            get
            {
                return _exit;
            }
        }

        //A dictionary of World Events to see if a player exited a room.
        private Dictionary<Room, WorldEvent> worldEvents;
        //Constructor for the game world.
        private GameWorld()
        {
            worldEvents = new Dictionary<Room, WorldEvent>();
            CreateWorld();
            //All notifications that are present in the game world.
            NotificationCenter.Instance.AddObserver("PlayerWillExitRoom", PlayerWillExitRoom);
            NotificationCenter.Instance.AddObserver("PlayerDidExitRoom", PlayerDidExitRoom);
        }
        //Sending a notification that the player will exit a room.
        public void PlayerWillExitRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            if(player.CurrentRoom == _exit)
            {
                player.OutputMessage("Player left the exit room");
            }
            player.OutputMessage("Player will exit " + player.CurrentRoom.Tag + "\n");
        }
        //Sending a notification that the player exited a room.
        public void PlayerDidExitRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            WorldEvent we = null;
            worldEvents.TryGetValue(player.CurrentRoom, out we);
            if(we != null)
            {
                we.Execute();
            }
         
        }
        //By making the CreateWorld() private, the only way to CreateWorld is GameWorld
        //Constructs the rooms
        private void CreateWorld()
        {
            //All the rooms in the world.
            Room cornelia = new Room("Cornelia"); //Starting room
            Room talap = new Room("Talap");
            Room baron = new Room("Baron");
            Room crescent = new Room("Crescent");
            Room vector = new Room("Vector");
            Room midgar = new Room("Midgar");
            Room timber = new Room("Timber");
            Room dali = new Room("Dali");
            Room luca = new Room("Luca");
            Room bevelle = new Room("Bevelle");
            Room norg = new Room("Norg");
            Room archades = new Room("Archades");
            Room eden = new Room("Eden");
            Room oerba = new Room("Oerba");
            Room bresha = new Room("Bresha");
            Room moogle = new Room("Moogle Town, kupo.");
            Room kugane = new Room("Kugane");
            Room melmond = new Room("Melmond");
            Room altair = new Room("Altair");
            Room mythril = new Room("Mythril");
            Room quelb = new Room("Quelb");
            Room wutai = new Room("Wutai");
            Room WinRoom = new Room("Room of winners."); //The room where you can win.

            //Connecting all the rooms their respective tethers. 
            Door door = Door.connectRooms(altair, crescent, "north", "south");
            door = Door.connectRooms(altair, norg, "south", "north");
            door = Door.connectRooms(altair, wutai, "east", "west");

            door = Door.connectRooms(cornelia, talap, "north", "south");
            door = Door.connectRooms(cornelia, kugane, "south", "north");
            door = Door.connectRooms(cornelia, crescent, "west", "east");
            door = Door.connectRooms(cornelia, midgar, "east", "west");
            door = Door.connectRooms(crescent, vector, "west", "east");

            door = Door.connectRooms(luca, dali, "north", "south");
            door = Door.connectRooms(luca, norg, "west", "east");
            door = Door.connectRooms(luca, melmond, "south", "north");

            door = Door.connectRooms(moogle, dali, "west", "east");
            door = Door.connectRooms(moogle, quelb, "north", "south");
            door = Door.connectRooms(midgar, mythril, "south", "north");
            door = Door.connectRooms(mythril, wutai, "west", "east");

            door = Door.connectRooms(norg, oerba, "west", "east");
            door = Door.connectRooms(norg, WinRoom, "south", "north");
            door = Door.connectRooms(norg, wutai, "north", "south");

            door = Door.connectRooms(oerba, archades, "north", "south");
            door = Door.connectRooms(oerba, bresha, "south", "north");
            door = Door.connectRooms(oerba, eden, "west", "east");

            door = Door.connectRooms(talap, baron, "east", "west");
            door = Door.connectRooms(talap, bevelle, "west", "east");
            door = Door.connectRooms(timber, dali, "south", "north");
            door = Door.connectRooms(timber, quelb, "east", "west");
            door = Door.connectRooms(timber, midgar, "west", "east");

            
            //Words on the note -- Decorator Pattern
            //Adding all of the real notes into specific rooms.
            IItem item0 = new Item("Note1", 5.0f);
            IItem decorator0 = new Item("Contents of note: S - 1");
            item0.AddDecorator(decorator0);
            kugane.Drop(item0);

            IItem item1 = new Item("Note2", 5.0f);
            IItem decorator1 = new Item("Contents of note: n - 2");
            item1.AddDecorator(decorator1);
            baron.Drop(item1);

            IItem item2 = new Item("Note3", 5.0f);
            IItem decorator2 = new Item("Contents of note: e - 5");
            item2.AddDecorator(decorator2);
            timber.Drop(item2);

            IItem item3 = new Item("Note4", 5.0f);
            IItem decorator3 = new Item("Contents of note: a - 7");
            item3.AddDecorator(decorator3);
            moogle.Drop(item3);

            IItem item4 = new Item("Note5", 5.0f);
            IItem decorator4 = new Item("Contents of note: k - 4");
            item4.AddDecorator(decorator4);
            eden.Drop(item4);

            IItem item5 = new Item("Note6", 5.0f);
            IItem decorator5 = new Item("Contents of note: t - 8");
            item5.AddDecorator(decorator5);
            vector.Drop(item5);

            IItem item6 = new Item("Note7", 5.0f);
            IItem decorator6 = new Item("Contents of note: r - 10");
            item6.AddDecorator(decorator6);
            bevelle.Drop(item6);

            IItem item7 = new Item("Note8", 5.0f);
            IItem decorator7 = new Item("Contents of note: a - 3");
            item7.AddDecorator(decorator7);
            altair.Drop(item7);

            IItem item8 = new Item("Note9", 5.0f);
            IItem decorator8 = new Item("Contents of note: E - 6");
            item8.AddDecorator(decorator8);
            quelb.Drop(item8);

            IItem item9 = new Item("Note10", 5.0f);
            IItem decorator9 = new Item("Contents of note: e - 9");
            item9.AddDecorator(decorator9);
            bresha.Drop(item9);

            //Fake notes that are in the world.
            IItem item10 = new Item("FNote1", 5.0f);
            IItem decorator10 = new Item("Contents of note: J - 1");
            item10.AddDecorator(decorator10);
            melmond.Drop(item10);

            IItem item11 = new Item("FNote2", 5.0f);
            IItem decorator11 = new Item("Contents of note: l - 5");
            item11.AddDecorator(decorator11);
            mythril.Drop(item11);

            IItem item12 = new Item("FNote3", 5.0f);
            IItem decorator12 = new Item("Contents of note: a - 2");
            item12.AddDecorator(decorator12);
            wutai.Drop(item12);

            IItem item13 = new Item("FNote4", 5.0f);
            IItem decorator13 = new Item("Contents of note: e - 4");
            item13.AddDecorator(decorator13);
            WinRoom.Drop(item13);

            IItem item14 = new Item("FNote5", 5.0f);
            IItem decorator14 = new Item("Contents of note: r - 3");
            item14.AddDecorator(decorator14);
            archades.Drop(item14);
            //Set Delegates
            
            //A trap room where you have to say the right password to proceed, cannot move anywhere until you get it right.
            TrapRoom tr = new TrapRoom();
            tr.Hint = "Creator of this program!";
            tr.Password = "Jarel";
            tr.ContainingRoom = timber;
            timber.Delegate = tr;
            
            //Norg is the echo room. It says the same word that you say three times.
            RoomDelegate rd = new EchoRoom();
            rd.ContainingRoom = norg;
            norg.Delegate = rd;
            IItem itemN = new Item("Scissors", 60.0f);
            norg.Drop(itemN);

           
            //cornelia will always be where the player will start.
            _entrance = cornelia;
            _exit = WinRoom;
            WorldChange we = new WorldChange(wutai, norg, "south", "north");
            worldEvents[talap] = we;
           
            //The only room where you can win! Guess the right password and you win, only three tries though.
            WinningRoom WR = new WinningRoom();
            WR.Password = "SnakeEater";
            WR.ContainingRoom = WinRoom;
            WinRoom.Delegate = WR;

        }

        //How to send notifications of where they left and where they are going to.
        private interface WorldEvent
        {
            void Execute();
        }
        
        //Where we connect all of the rooms together.
        private class WorldChange : WorldEvent
        {
            //Constructor for the WorldChange.
            public Room RoomA { get; set; }
            public Room RoomB { get; set; }
            public string AtoB { get; set; }
            public string BtoA { get; set; }
            
            public WorldChange(Room roomA, Room roomB, string aToB, string bToA)
            {
                RoomA = roomA;
                RoomB = roomB;
                AtoB = aToB;
                BtoA = bToA;
            }
            //When you move from room to room, this will keep track of where the player is.

            //How we connecting the two rooms and the exits between the rooms. 
            public void Execute()
            {
                Door.connectRooms(RoomA, RoomB, AtoB, BtoA);
            }
        }
    }
}
