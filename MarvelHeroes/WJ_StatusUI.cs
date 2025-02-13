using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class StatusUI
    {
        Player player = GameManager.Instance.player;
        public void UIStateScene() // 상태정보창
        {
            while (true)
            {
            Console.Clear();
            GameView.PrintText("<상태 보기>", 0, ConsoleColor.Magenta);
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            ShowStatus();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                }
                else
                {
                    GameView.PrintText("입력을 다시시도해 주세요", 800);
                }
            }
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Lv : {player.Level} ({player.Xp} / {player.maxXp})"); // 플레이어 레벨
            Console.WriteLine($"이름 : {player.Name} ({player.PlayerJob})"); // 플레이어 이름 및 직업
            string attack = player.EquipAtk == 0 ? $"공격력 : {player.Atk}" : $"공격력 : {player.Atk + player.EquipAtk} (+{player.EquipAtk})"; // 공격력 보여주기
            Console.WriteLine(attack); // 플레이어 공격력
            string deffence = player.EquipDef == 0 ? $"방어력 : {player.Def}" : $"방어력 : {player.Def + player.EquipDef} (+{player.EquipDef})"; // 방어력 보여주기
            Console.WriteLine(deffence); // 플레이어 공격력
            Console.WriteLine($"크리티컬 확률 : {player.Critical}%");
            Console.WriteLine($"회피율 : {player.Dexterity}%");
            Console.WriteLine($"체력 : ({player.Hp} / {player.MaxHp})");
            Console.WriteLine($"마력 : ({player.Mp} / {player.MaxMp})");
            Console.WriteLine($"Gold : {player.Gold} G");
        }
    }
}
