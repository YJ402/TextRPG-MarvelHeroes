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
        public int InventoryScene(int temp)
        {
            int sceneTemp = temp;
            Console.Clear();
            GameView.PrintText("인벤토리", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("캐릭터의 소지 아이템이 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            foreach (int i in GameManager.inventory) // 게임매니저에서 선언한 인벤토리 리스트를 가져옴
            {
                DisplayInven(i,0);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else if (choice == 1) { UIEquipScene(sceneTemp); }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
            return sceneTemp;
        }

        public string DisplayInven(int i, int j)
        {
            string str = GameManager.inventory[i].isEquip ? "[E]" : "";
            if (j == 0)
            {
                str = $"- {i + 1}" + str + $"{GameManager.inventory[i].name}\t| {DisplayType(i)}\t | {GameManager.inventory[i].Descrip}\t| {gameManager.inventory[i].Cost}";
            }
            else
            {
                str = $"-  " + str + $"{GameManager.inventory[i].name}\t| {DisplayType(i)}\t | {GameManager.inventory[i].Descrip}\t| {gameManager.inventory[i].Cost}";
            }
            return str;
        }

        public string DisplayType(int i)
        {
            string str = GameManager.inventory[i].type == ItemType.Weapon ? $"공격력 : {GameManager.inventory[i].Value}" : $"방어력 : {gameManager.inventory[i].Value}";
            return str;
        }

        public int UIEquipScene(int temp)
        {
            int sceneTemp = temp;
            Console.Clear();
            GameView.PrintText("인벤토리 - 장착관리 ", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("캐릭터의 소지 아이템을 장착, 장착해제 할 수 있습니다..");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            foreach (int i in GameManager.inventory) // 게임매니저에서 선언한 인벤토리 리스트를 가져옴
            {
                DisplayInven(i,1);
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else { Equip(choice); }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
            return sceneTemp;
        }

        public void Equip(int choice)
        {
            DH_ItemManager select = GameManager.inventory[choice - 1]; 

            for (int i = 0; i < GameManager.inventory.Count; i++)
            {
                if ((GameManager.inventory[choice - 1].isEquip = false) && (GameManager.inventory[choice - 1].Type == select.Type))
                {
                }
            }
        }

    }

}

