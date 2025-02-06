using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class GameView
    {
        public static List<Action<Scene>> ViewNormalScene = new List<Action<Scene>>() { ViewSceneNameAndDesc, ViewItemInfo, ViewMonsterInfo, ViewSceneSelect };


        //예시 PrintText("[탐험 결과]", 200, ConsoleColor.DarkBlue);
        public static void PrintText(string text, int ms = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Thread.Sleep(ms);
        }


        public static void ViewSceneNameAndDesc(Scene scene)
        {
            Console.WriteLine(scene.Name);
            Console.WriteLine(scene.Description);
        }

        public static void ViewItemInfo(Scene scene, Inventory inventory) // inventory를 가져와도 되고, inventory 내에 소유 아이템을 정리한 리스트가 있다면 그걸 가져와도 되고
        {
            Console.WriteLine("\n [아이템 목록]");
            int i = 1;
            foreach (Item item in inventory.{ 리스트이름})
            {
                Console.WriteLine($"-[{i}]\t{item.Name}\t|\t{item.ValueName} +{item.Value}\t|\t{item.Description}\t \t|  가격: {item.Price}골드");
                i++;
            }
        }

        public static void ViewMonsterInfo(Scene scene, Monster monster) // monster를 가져와도 되고, monster내에 출몰 몬스터를 정리한 리스트가 있다면 그걸 가져와도 되고
        {
            Console.WriteLine("\n [아이템 목록]");
            foreach (Monster mon in MonsterList)
            {
                if(monster.HP < 0)
                {
                    Console.WriteLine($"Lv{mon.Lv} {mon.Name} Dead");
                    continue;
                }
                Console.WriteLine($"Lv{mon.Lv} {mon.Name} HP{mon.Health}");
            }
        }


        public static void ViewSceneSelect(Scene scene)
        {
            //씬 선택지를 출력하기
        }

        public static void ViewInput(string str = "원하시는 행동을 입력해주세요.")
        {
            //씬 선택지를 출력하기
            Console.WriteLine(str);
            Console.Write(">>");

        }

        public static bool ViewYesOrNo(Scene scene)
        {
            Console.WriteLine("1.예");
            Console.WriteLine("2.아니오");

            bool yes = false;
            int input;

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1)
                    {
                        yes = true;

                    }
                    break;
                }
            }
            return yes;
        }

    }
}
