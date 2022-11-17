using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
   public class Item : IItem
    {
        //All of the variables for the Item class.
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private float _weight;
        public string LongName { get { return Name + (_decorator != null ?", " + _decorator.LongName:""); } }
        public virtual float Weight { get { return _weight + (_decorator != null ? _decorator.Weight : 0f) ; } set { _weight = value; } }
        private IItem _decorator;

        //All constructors for item in case an item doesn't have a name and/or weight.
        public Item() : this("No Name") { }
        public Item(string name) : this(name, 0.0f) { }
        //Designated Constructor
        public Item(string name, float weight)
        {
            Name = name;
            Weight = weight;
            _decorator = null;
        }

        //returning weight of an item.
        public float getWeight()
        {
            return _weight;
        }

        //Description of an item. 
        public string Description
        {
            get
            {
                return "Name: "+  Name + ", "+ "Weight: " + _weight;
            }
            
        }

        //The longer description of a regular description.
        public virtual string LongDescription
        {
            get { return "Name: " + LongName + ", " + "Weight: " + Weight; }
        }

        //Adding decorators to items, decorators are addons to items but will increase weight if it has any.
        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }

        //Adding an item to a room which is in the world.
        public virtual void AddItem(IItem item)
        {

        }

        //Removing an item from a room and adding it into our inventory. 
        public virtual IItem RemoveItem(string name)
        {
            return null;
        }
        
        //A method to clone an item.
        public virtual IItem Clone()
        {
            Item clone = (Item)this.MemberwiseClone();
            clone._name = "" +_name;
            clone._weight = _weight; //This should be by value, not by reference
            clone._decorator = _decorator != null ? _decorator.Clone():null;
            return clone;
        }
    }

    public class ItemContainer : Item
    {
        //Dictionary of items in our ItemContainer class to keep track of items.
        private Dictionary<string, IItem> items;
        override
        public float Weight
        {
            get
            {
                float tempWeight = base.Weight;
                foreach (IItem item in items.Values)
                {
                    tempWeight += item.Weight;
                }
                return tempWeight;
            }
        }
        //Designated Constructor
        public ItemContainer(string name, float weight) : base(name, weight)
        {
            items = new Dictionary<string, IItem>();
        }

        //Adding an item to a room which is in the world.
        override
        public void AddItem(IItem item)
        {
            items[item.Name] = item;
        }
        //Removing an item from a room and adding it into our inventory. 

        override
        public IItem RemoveItem(string name)
        {
            IItem item = null;
            items.TryGetValue(name, out item);
            items.Remove(name);
            return item;
        }
        //Showing the items names and weights that are correspondent to each other.
        public string ShowItems()
        {
            foreach(IItem item in items.Values)
            {
                Console.WriteLine("\n" + item.LongName + " " + "| "+ item.Weight);
               // Console.WriteLine("\n" + item.LongDescription);
            }
            return " ";

        }

        //Long description of an item. 
        override
        public string LongDescription
        {
            get
            {
                string description = base.LongDescription + "\n";
                foreach(IItem item in items.Values)
                {
                    description += "\t - " + item.LongDescription + "\n";
                }
                return description;
            }
        }
    }
    //Interface for an enemy.
    public interface IEnemy
    {
        void TakeDamage(float damage);
    }
    
    public enum AMMO_TYPE { BULLET, ARROW, ROCK, LIGHTING, CHEESE}
    //Interface for ammo. It has power, type, quantity and an reload method.
    public interface IAmmo
    {
        float Power { get; }
        AMMO_TYPE Type { get; }
        int Quantity { get; }
        int Extract();
    }

    //A weapon class that extends off of Item.
    //Weapon class has its own methods like "Load" and "Fire".
    public abstract class Weapon : Item
    {
        public Weapon() : base("No Name") { }
        public Weapon(string name) : this(name, 1.0f) { }
        public Weapon(string name, float weight) : base(name, weight) { }
        public abstract void Load();
        public abstract void Prepare();
        public abstract void Fire(IEnemy enemy);
        public abstract void LoadAmmo(IAmmo ammo);

        //Template Method Design Pattern
        public void Use(IEnemy enemy)
        {
            Load();
            Prepare();
            Fire(enemy);
        }
    }
    //Pistol is a class which extends off of weapon.
    public class Pistol : Weapon
    {
        private IAmmo _ammo;
        private int _loadedAmmo;
        
        public Pistol () : base() { }

        public Pistol(string name, float weight) : base(name, weight)
        {
            _ammo = null;
            _loadedAmmo = 0;
        }
        override
        public void Load()
        {
            _loadedAmmo = _ammo != null ? _ammo.Extract() : 0;
        }
        override
        public void Prepare()
        {

        }
        override
        public void Fire(IEnemy enemy)
        {
            enemy.TakeDamage(_ammo != null ? _ammo.Power : 0 * _loadedAmmo);
            _loadedAmmo = 0;
        }
        override
        public void LoadAmmo(IAmmo ammo)
        {
            if(ammo.Type == AMMO_TYPE.BULLET)
            {
                _ammo = ammo;
            }
        }
    }
    //Prototype design pattern
    public class ItemSpawner
    {
        private IItem _prototype;

        public ItemSpawner(IItem prototype)
        {
            _prototype = prototype;
        }

        public IItem Spawn()
        {
            return _prototype.Clone();
        }
    }
}
