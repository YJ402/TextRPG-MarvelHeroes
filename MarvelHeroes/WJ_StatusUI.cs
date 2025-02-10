using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class WJ_StatusUI
    {
        public void UIStateScene(SceneNum temp) // 상태정보창
        {
            //int sceneTemp = tmep;
            Console.Clear();
            GameView.PrintText("상태 보기", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine(""); // 플레이어 레벨
            Console.WriteLine(""); // 플레이어 이름 및 직업
            //string attack = EquipAttack == 0 ? $"공격력 : {atk}" : $"공격력 : {atk} (+{EquipAttack})"; // 공격력 보여주기
            //Console.WriteLine(attack); // 플레이어 공격력
            //string deffence = EquipDefence == 0 ? $"방어력 : {def}" : $"공격력 : {def} (+{EquipDefence})"; // 방어력 보여주기
            //Console.WriteLine(deffence); // 플레이어 공격력
            //Console.WriteLine($"체력 : ({health} / {maxHealth})");
            //Console.WriteLine($"Gold : {gold} G");
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
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
            //return sceneTemp;
        }
    }
}
