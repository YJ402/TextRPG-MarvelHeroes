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
            Console.WriteLine($"Lv : {GameManager.Instance.player.Level} ({GameManager.Instance.player.Xp} / {GameManager.Instance.player.maxXp})"); // 플레이어 레벨
            Console.WriteLine($"이름 : {GameManager.Instance.player.Level} ({GameManager.Instance.player.PlayerJob})"); // 플레이어 이름 및 직업
            string attack = GameManager.Instance.player.EquipAtk == 0 ? $"공격력 : {GameManager.Instance.player.Atk}" : $"공격력 : {GameManager.Instance.player.Atk + GameManager.Instance.player.EquipAtk} (+{GameManager.Instance.player.EquipAtk})"; // 공격력 보여주기
            Console.WriteLine(attack); // 플레이어 공격력
            string deffence = GameManager.Instance.player.EquipDef == 0 ? $"방어력 : {GameManager.Instance.player.Def}" : $"방어력 : {GameManager.Instance.player.Def + GameManager.Instance.player.EquipDef} (+{GameManager.Instance.player.EquipDef})"; // 방어력 보여주기
            Console.WriteLine(deffence); // 플레이어 공격력
            Console.WriteLine($"크리티컬 확률 : {GameManager.Instance.player.Critical}%");
            Console.WriteLine($"회피율 : {GameManager.Instance.player.Dexterity}%");
            Console.WriteLine($"체력 : ({GameManager.Instance.player.Hp} / {GameManager.Instance.player.MaxHp})");
            Console.WriteLine($"마력 : ({GameManager.Instance.player.Mp} / {GameManager.Instance.player.MaxMp})");
            Console.WriteLine($"Gold : {GameManager.Instance.player.Gold} G");
        }
    }
}
