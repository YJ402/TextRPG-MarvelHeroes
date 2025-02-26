﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class GameView
    {
        //예시 PrintText("[탐험 결과]", 200, ConsoleColor.DarkBlue);
        public static void PrintText(string text, int ms = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Thread.Sleep(ms);
        }


        public static void ViewSceneNameAndDesc1(Scene scene, ConsoleColor color = ConsoleColor.Green)
        {
            Console.Clear();
            if (scene is DungeonScene)
            { color = ConsoleColor.DarkRed; }
            PrintText($"<{scene.Name}>", 0, color);
            Console.WriteLine(scene.Description);
            Console.WriteLine();
        }

        // 선택지 메서드 2개: ViewGetSceneSelect(씬 선택지를 출력하고 입력 값을 반환함. 예외처리도 함//지금은 toDos랑 toGos로 선택지를 출력하는데 그냥 단일화할까.), SceneSelectYN(예 아니오 선택지를 출력하고 입력값에 따른 참 거짓을 반환함. 예외처리도 함) /// 
        public static int ViewGetSceneSelect2(Scene currentScene, string str = "원하시는 행동을 입력해주세요.")
        {
            //ViewSceneUI(currentScene);

            ViewSceneSelect2_1(currentScene);

            ViewSceneInput2_2(currentScene, str);

            return GetSceneInput2_3(currentScene);
        }


        public static void ViewSceneSelect2_1(Scene currentScene)
        {
            if (currentScene.sceneSelections != null)
            {
                int temp = currentScene.sceneSelections.Count;

                if (currentScene.sceneSelections.ContainsKey(0))
                {
                    //UI 버튼이 있는 씬일때.
                    Console.WriteLine("\t\t\t\t\t\t0.UI");

                    for (int i = 1; i < temp; i++)
                    {
                        Console.WriteLine("{0}. {1}", i, currentScene.sceneSelections[i].GetSelectionDesc());
                    }
                }
                else
                {
                    //UI 버튼이 없는 씬일때.
                    for (int i = 1; i < temp + 1; i++)
                    {
                        Console.WriteLine("{0}. {1}", i, currentScene.sceneSelections[i].GetSelectionDesc());
                    }
                }
                Console.WriteLine();
            }
        }

        public static void ViewSceneInput2_2(Scene currentScene, string str = "원하시는 행동을 입력해주세요.")
        {
            //씬 선택지를 출력하기
            if (currentScene.Description2 != null)
                Console.WriteLine(currentScene.Description2);
            else
                Console.WriteLine(str);
            Console.Write(">> ");
        }

        public static int GetSceneInput2_3(Scene currentScene)
        {
            while (true)
            {
                //입력값 유효성 검사(숫자인지, 선택지 갯수 내의 숫자인지)
                int input;
                int temp = currentScene.sceneSelections.Count();

                if (currentScene.sceneSelections.ContainsKey(0))
                {
                    //UI 버튼이 있는 씬일때.
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        if (input < temp && input > -1)
                            return input;
                    }
                }
                else
                {
                    //UI 버튼이 없는 씬일때.
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        if (input < 1 + temp && input > 0)
                            return input;
                    }
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

        public static string DisplayInven(int i, int j, List<Item> inven)
        {
            string str = inven[i].IsEquip ? "[E]" : "";
            if (j == 0)
            {
                str = $"- {string.Format("{0:D2}", i + 1)}. " + str + $"{inven[i].Name}\t| {DisplayType(i, inven)}\t | {inven[i].Descrip}\t| {inven[i].Cost}";
            }
            else
            {
                str = $"-  " + str + $"{inven[i].Name}\t| {DisplayType(i, inven)}\t | {inven[i].Descrip}\t| {inven[i].Cost}";
            }
            return str;
        }

        public static string DisplayType(int i, List<Item> inven)
        {
            string str = "";
            if (inven[i].ItemType == ItemType.Weapon) { str = $"공격력 : {inven[i].Value}"; }
            else if (inven[i].ItemType == ItemType.Amor) { str = $"방어력 : {inven[i].Value}"; }
            else if (inven[i].ItemType == ItemType.Healing) { str = $"HP회복량 : {inven[i].Value}"; }
            else if (inven[i].ItemType == ItemType.Regeneration) { str = $"MP회복량 : {inven[i].Value}"; }
            return str;
        }
    }
}
