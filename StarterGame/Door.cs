using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //How to lock a door. Not used in code.
    public interface Locking
    {
        bool IsLocked { get; }
        bool IsUnlocked { get; }
        void Lock();
        void Unlock();
        bool MayOpen { get; }
        bool MayClose { get; }
    }

    //A regular lock that is extending off of attributes from "Locking" interface.
    public class RegularLock : Locking
    {
        private bool _isLocked;
        public bool IsLocked { get { return _isLocked; } }
        public bool IsUnlocked { get { return !_isLocked; } }

        //Locking a door that is unlocked.
        public void Lock()
        {
            _isLocked = true;
        }
        //Unlocking a door when it is locked.
        public  void Unlock()
        {
            _isLocked = false;
        }
        public bool MayOpen { get { return _isLocked ? false : true; } }
        public bool MayClose { get { return _isLocked ? true : false; } }
        public RegularLock()
        {
            _isLocked = false;
        }

    }
    //A door that extends off of the "Locking" interface.
    public class Door : Locking
    {
        private Room _room1;
        private Room _room2;
        private bool _closed;
        //private bool _locked;
        public bool IsClosed
        {
            get
            {
                return _closed;
            }
        }

        public bool IsOpen
        {
            get
            {
                return !_closed;
            }
        }

        private Locking _lock;

        
        //Constructor to go from room to room. 
        public Door(Room room1, Room room2)
        {
            _room1 = room1;
            _room2 = room2;
            _closed = false;
            _lock = null;
        }

        public Room OtherRoom(Room thisRoom)
        {
            if(thisRoom == _room1)
            {
                return _room2;
            }
            else
            {
                return _room1;
            }
            //Termary - one-line code
            //return thisRoom == room1 ? _room2 : _room1;
        }
        
        //Closing a door that is open.
        public void Close()
        {
            if(_lock != null)
            {
                _closed = _lock.MayClose;
            }
            else
            {
                _closed = true;
            }
        }

        //Opening a door that is closed.
        public void Open()
        {
            if(_lock != null)
            {
                _closed = !_lock.MayOpen;
            }
            else
            {
                _closed = false;
            }
        }
        public bool IsLocked { get { return _lock != null ? _lock.IsLocked : false; } }
        public bool IsUnlocked { get { return _lock != null ? _lock.IsUnlocked : true; } }
        public void Lock()
        {
            if(_lock != null)
            {
                _lock.Lock();
            }
        }
        public void Unlock()
        {
            if(_lock != null)
            {
                _lock.Unlock();
            }
        }
        public bool MayOpen { get { return _lock != null ? _lock.MayOpen : true; } }
        public bool MayClose { get { return _lock != null ? _lock.MayClose : true; } }

        //Putting a lock on a door.
        public void InstallLock(Locking newLock)
        {
            _lock = newLock;
        }
        /*public void Unlock()
        {
            _locked = false;
        }*/
        //
        public static Door connectRooms(Room fromRoom, Room toRoom, string fromTo, string toFrom) 
        {
            Door door = new Door(fromRoom, toRoom);
            fromRoom.SetExit(toFrom, door);
            toRoom.SetExit(fromTo, door);
            return door;
        }
    }
}
