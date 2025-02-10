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
        // public static List<Action<Scene>> ViewNormalScene = new List<Action<Scene>>() { ViewSceneNameAndDesc, ViewItemInfo, ViewMonsterInfo, ViewSceneSelect };


        //예시 PrintText("[탐험 결과]", 200, ConsoleColor.DarkBlue);
        public static void PrintText(string text, int ms = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Thread.Sleep(ms);
        }


        public static void ViewSceneNameAndDesc1(Scene scene)
        {
            Console.WriteLine(scene.Name);
            Console.WriteLine(scene.Description);
            Console.WriteLine();
        }

        //public static void ViewItemInfo(Scene scene, Inventory inventory) // inventory를 가져와도 되고, inventory 내에 소유 아이템을 정리한 리스트가 있다면 그걸 가져와도 되고
        //{
        //    Console.WriteLine("\n [아이템 목록]");
        //    int i = 1;
        //    foreach (Item item in inventory.{ 리스트이름})
        //    {
        //        Console.WriteLine($"-[{i}]\t{item.Name}\t|\t{item.ValueName} +{item.Value}\t|\t{item.Description}\t \t|  가격: {item.Price}골드");
        //        i++;
        //    }
        //}

        //public static void ViewMonsterInfo(Scene scene, List<Monster> monsterlist) // monster를 가져와도 되고, monster내에 출몰 몬스터를 정리한 리스트가 있다면 그걸 가져와도 되고
        //{
        //    Console.WriteLine("\n [아이템 목록]");
        //    foreach (Monster mon in MonsterList)
        //    {
        //        if(monster.HP < 0)
        //        {
        //            Console.WriteLine($"Lv{mon.Lv} {mon.Name} Dead");
        //            continue;
        //        }
        //        Console.WriteLine($"Lv{mon.Lv} {mon.Name} HP{mon.Health}");
        //    }
        //}


        // 선택지 메서드 2개: ViewGetSceneSelect(씬 선택지를 출력하고 입력 값을 반환함. 예외처리도 함//지금은 toDos랑 toGos로 선택지를 출력하는데 그냥 단일화할까.), SceneSelectYN(예 아니오 선택지를 출력하고 입력값에 따른 참 거짓을 반환함. 예외처리도 함) /// 
        public static int ViewGetSceneSelect2(Scene currentScene)
        {
            ViewSceneSelect2_1(currentScene);

            ViewSceneInput2_2(currentScene);

            return GetSceneInput2_3(currentScene);
        }

        public static void ViewSceneSelect2_1(Scene currentScene)
        {
            if (currentScene.sceneSelections != null)
            {
                for (int i = 1; i < currentScene.sceneSelections.Count + 1; i++)
                {
                    Console.WriteLine("{0}. {1}", i, currentScene.sceneSelections[i].GetSelectionDesc());
                }
            }
            //while(i < scene.toDos.Count)
            //{
            //    Console.WriteLine("{i+1}. {scene.toDos[i].Name}을/를 하기");
            //    i++;
            //}

            //while (i < scene.toGos.Count)
            //{
            //    Console.WriteLine("{i+1}. {scene.toGos[i]}(으)로 가기");
            //    i++;
            //}
            //Console.WriteLine();
        }

        public static void ViewSceneInput2_2(Scene currentScene)
        {
            //씬 선택지를 출력하기
            if (currentScene.Description2 != null)
                Console.WriteLine(currentScene.Description2);
            else
                Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        public static int GetSceneInput2_3(Scene currentScene)
        {
            while (true)
            {
                //입력값 유효성 검사(숫자인지, 선택지 갯수 내의 숫자인지)
                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input < 1 + currentScene.sceneSelections.Count() && input > 0)
                        return input;
                }
                Console.WriteLine("잘못된 입력입니다");
                Console.Write(">> ");
            }
        }

        public static bool SceneSelectYN(string warning = "")
        {
            Console.WriteLine(warning);
            Console.WriteLine("1.예\n2.아니오\n>> ");

            int input;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    if (input == 1)
                        return true;
                    else if (input == 2)
                        return false;
                }
                Console.WriteLine("잘못된 입력입니다.\n>> ");
            }
        }

        
    }
}
