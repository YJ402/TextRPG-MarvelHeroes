using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class StatusUI
    {
        public void UIStateScene() // 상태정보창
        {
            while (true)
            {
            Console.Clear();
            GameView.PrintText("상태 보기", 0, ConsoleColor.Magenta);
            Console.WriteLine();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            ShowStatus();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine(""); // 플레이어 레벨
            Console.WriteLine(""); // 플레이어 이름 및 직업
            //string attack = GameManager.player.EquipAttack == 0 ? $"공격력 : {GameManager.player.Atk}" : $"공격력 : {GameManager.player.Atk} (+{GameManager.player.EquipAttack})"; // 공격력 보여주기
            //Console.WriteLine(attack); // 플레이어 공격력
            //string deffence = GameManager.player.EquipDefence == 0 ? $"방어력 : {GameManager.player.Def}" : $"공격력 : {GameManager.player.Def} (+{GameManager.player.EquipDefence})"; // 방어력 보여주기
            //Console.WriteLine(deffence); // 플레이어 공격력
            //Console.WriteLine($"체력 : ({GameManager.player.Hp} / {GameManager.player.MaxHp})");
            //Console.WriteLine($"마력 : ({GameManager.player.Mp})");
            //Console.WriteLine($"Gold : {GameManager.player.Gold} G");
        }
    }
}
