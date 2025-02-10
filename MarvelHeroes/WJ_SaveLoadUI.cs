
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    internal class WJ_SaveLoadUI
    {
        public int InventoryScene(int temp)
        {
            int sceneTemp = temp;
            Console.Clear();
            GameView.PrintText("게임 저장/불러오기", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("현재 진행 상황이 저장/불러오기 됩니다.");
            Console.WriteLine();
            Console.WriteLine("게임을 저장/불러오기 하기겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("0. 아니오");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else if (choice == 1) { ; }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
            return sceneTemp;
        }
    }
}
