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

        public void AddItem(Item item, int i = 1)
        {
            if (item is UsingItem)
            {
                UsingItem _item = item as UsingItem;

                _item.Quantity += i;
                if (!items.Contains(item))
                {
                    items.Add(item);
                }
            }
            else { items.Add(item); }
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }
}

