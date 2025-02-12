using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class InventoryUI
    {
        Inventory inventory = GameManager.Instance.inventory;
        //List<EquipItem> inventory = new List<EquipItem>();
        public void InventoryScene()
        {
            while (true)
            {
                Console.Clear();
                GameView.PrintText("<인벤토리>", 0, ConsoleColor.Magenta);
                Console.WriteLine("캐릭터의 소지 아이템이 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.items.Count; i++) // 게임매니저에서 선언한 인벤토리 리스트를 가져옴
                {
                    if (inventory.items[i].IsEquip)
                    {
                        GameView.PrintText(GameView.DisplayInven(i, 1, inventory.items),0,ConsoleColor.Yellow);
                    }
                    else { Console.WriteLine(GameView.DisplayInven(i, 1, inventory.items)); }
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else if (choice == 1) { UIEquipScene(); }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
        }

        //public string DisplayInven(int i, int j)
        //{
        //    string str = GameManager.Instance.inventory.items[i].IsEquip ? "[E]" : "";
        //    if (j == 0)
        //    {
        //        str = $"- {i + 1}. " + str + $"{GameManager.Instance.inventory.items[i].Name}\t| {DisplayType(i)}\t | {GameManager.Instance.inventory.items[i].Descrip}\t| {GameManager.Instance.inventory.items[i].Cost}";
        //    }
        //    else
        //    {
        //        str = $"-  " + str + $"{GameManager.Instance.inventory.items[i].Name}\t| {DisplayType(i)}\t | {GameManager.Instance.inventory.items[i].Descrip}\t| {GameManager.Instance.inventory.items[i].Cost}";
        //    }
        //    return str;
        //}

        //public string DisplayType(int i)
        //{
        //    string str = GameManager.Instance.inventory.items[i].ItemType == ItemType.Weapon ? $"공격력 : {GameManager.Instance.inventory.items[i].Value}" : $"방어력 : {GameManager.Instance.inventory.items[i].Value}";
        //    return str;
        //}

        public void UIEquipScene()
        {
            while (true)
            {
                Console.Clear();
                GameView.PrintText("<인벤토리 - 장착/사용 관리> ", 0, ConsoleColor.Magenta);
                Console.WriteLine("캐릭터의 소지 아이템을 장착, 장착해제, 사용 할 수 있습니다..");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < inventory.items.Count; i++) // 게임매니저에서 선언한 인벤토리 리스트를 가져옴
                {
                    if (inventory.items[i].IsEquip)
                    {
                        GameView.PrintText(GameView.DisplayInven(i, 0, inventory.items), 0, ConsoleColor.Yellow);
                    }
                    else { Console.WriteLine(GameView.DisplayInven(i, 0, inventory.items)); }
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    for (int i = 0; i < inventory.items.Count; i++)
                    {
                        if (choice == i+1)
                        {
                            inventory.items[i].Use(GameManager.Instance.player);
                        } // 장착 메서드
                    }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
        }

    }

}

