using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class Inventory
    {
        //인벤토리는 GameManager에 객체로 생성해두었습니다. "GameManager.Instance.inventory"로 접근할 수 있습니다.
        public List<Item> items = new List<Item>();
        //private List<Item> equippedItems = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        //public void AddEquippedItems(Item item)
        //{
        //    equippedItems.Add(item);
        //}
        //public void RemoveEquippedItems(Item item)
        //{
        //    equippedItems.Remove(item);
        //}
    }
}

