using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterGame
{
    //One of the only commands that require only itself.
    //when typed in, it will tell all of the items that are present in your inventory.
   class InventoryCommand : Command
    {
        public InventoryCommand() : base()
        {
            this.Name = "inventory";
        }

        override
        public bool Execute(Player player)
        {
            if (this.HasSecondWord() != true)
            {
                player.ShowInventory();

            }
            else
            {
                Console.WriteLine("\n'inventory' is a one word command. It does not need " + this.SecondWord);
            }
            return false;
        }
    }
}
