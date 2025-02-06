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
            PrintText(scene, 0, ConsoleColor.Green);
        }

        public static void ViewSceneInfo(Scene scene)
        {

        }

        public static void ViewSceneSelect(Scene scene)
        {

        }

        public static bool ViewYesOrNo(Scene scene)
        {

        }
    }
}
