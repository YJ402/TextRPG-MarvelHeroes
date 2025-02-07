using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public interface ISelections
    {
        public void Execute();
        public string GetSelectionDesc();
    }

    public class TalkToChief : ISelections
    {
        private Quest quest;
        public TalkToChief(Quest q) 
        {
            quest = q;
        }

        public void Execute()
        {
            Console.WriteLine("이장으로 퀘스트 트리거");// 퀘스트 트리거
        }

        public string GetSelectionDesc()
        {
            return "마을의 이장과 대화한다."; // 선택지 설명
        }
    }

    public class Investigate : ISelections
    {
        private Quest quest;


        public Investigate(Quest q)
        {
            quest = q;
        }

        public void Execute()
        {
            int random = new Random().Next(1, 100);
            if (random > 70)
            {
                Console.WriteLine("조사로 퀘스트 트리거");// 퀘스트 트리거
            }
        }

        public string GetSelectionDesc()
        {
            return "마을을 조사한다."; // 선택지 설명
        }
    }

    public class ToTown : ISelections
    {

        public void Execute()
        {
            SceneManager.ChangeCurrentScene("Town");
        }

        public string GetSelectionDesc()
        {
            return "마을로 이동하기";
        }
    }

    public class ToDungeon : ISelections
    {

        public void Execute()
        {
            SceneManager.ChangeCurrentScene("Dungeon");
        }

        public string GetSelectionDesc()
        {
            return "던전으로 이동하기";
        }
    }

    public class Select_1_Class : ISelections
    {

        public void Execute()
        {
            Console.WriteLine("1_Class로 전직하기 트리거");//1_Class로 전직하기 트리거 ;
        }

        public string GetSelectionDesc()
        {
            return "1_Class";
        }
    }

    public class Select_2_Class : ISelections
    {
        public void Execute()
        {
            Console.WriteLine("2_Class로 전직하기 트리거");//2_Class로 전직하기 트리거 ;
        }

        public string GetSelectionDesc()
        {
            return "2_Class";
        }
    }

    public class Select_3_Class : ISelections
    {
        public void Execute()
        {
            Console.WriteLine("3_Class로 전직하기 트리거");//3_Class로 전직하기 트리거 ;
        }

        public string GetSelectionDesc()
        {
            return "3_Class";
        }
    }

    public class Select_4_Class : ISelections
    {
        public void Execute()
        {
            Console.WriteLine("4_Class로 전직하기 트리거");//3_Class로 전직하기 트리거 ;
        }

        public string GetSelectionDesc()
        {
            return "3_Class";
        }
    }
}
