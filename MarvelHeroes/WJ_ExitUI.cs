
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    internal class ExitUI
    {
        public int ExitScene(int temp)
        {
            int sceneTemp = temp;
            Console.Clear();
            GameView.PrintText("게임종료", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("게임이 종료됩니다.");
            Console.WriteLine();
            Console.WriteLine("종료 하시겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 예");
            Console.WriteLine("0. 아니오");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else if (choice == 1) { Environment.Exit(0); }
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
