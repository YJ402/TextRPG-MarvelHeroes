using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public static class GameView
    {

        //예시 PrintText("[탐험 결과]", 200, ConsoleColor.DarkBlue);
        public static void PrintText(string text, int ms = 0, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
            Thread.Sleep(ms);
        }


        public static void ViewScene(Scene scene)
        {
            //매개변수로 받은 SCene에 대한 정보(씬 이름, 씬 설명)를 출력하기
        }

        public static void ViewItemInfo(Scene scene)
        {
            //매개변수로 받은 (info가 필요한 씬인 경우) Scene에 대한 정보(씬 이름, 씬 설명)를 출력하기
        }

        public static void ViewSceneSelect(Scene scene)
        {

        }

        public static bool ViewYesOrNo(Scene scene)
        {

        }

    }
}
