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
            //GameManager.Instance.QM.QuestStart("Chief");
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
                //GameManager.Instance.QM.QuestStart("investigation");
            }
        }

        public string GetSelectionDesc()
        {
            return "마을을 조사한다."; // 선택지 설명
        }
    }

    public class ToUI : ISelections
    {
        SceneNum currentScene;
        public ToUI(SceneNum cucScene)
        {
            currentScene = cucScene;
        }
        public void Execute()
        {
            Console.WriteLine("UI 트리거");// UI 트리거
            UIManager Um = new UIManager();
            Um.UIMainScene();
        }

        public string GetSelectionDesc()
        {
            return "UI"; // 선택지 설명
        }
    }

    public class ToWhere : ISelections
    {
        SceneNum sceneNum;

        public ToWhere(SceneNum i)
        {
            sceneNum = i;
        }
        public void Execute()
        {
            GameManager.Instance.SM.ChangeCurrentScene(sceneNum);
        }

        public string GetSelectionDesc()
        {
            string sceneKorName = "";

            switch (sceneNum)
            {
                case SceneNum.Town:
                    sceneKorName = "마을";
                    break;
                case SceneNum.Dungeon:
                    sceneKorName = "던전";
                    break;
                defualt:
                    sceneKorName = "없는 장소";
                    break;
            }

            return $"{sceneKorName}(으)로 이동하기";
        }
    }

    public class ToFloor : ISelections
    {
        int farmingFloor;
        int tryFloor;

        public ToFloor(int trying, int high)
        {
            farmingFloor = trying;
            tryFloor = high;
        }
        public void Execute()
        {
            if (GameView.SceneSelectYN("정말로 도전하시겠습니까?"))
            {
                Console.WriteLine($"{farmingFloor}층 공략하기");
                //배틀매니저의 전투 시작 메서드 호출(매개변수:목적 층 );
                BattleManager BM = new BattleManager();
                BM.BattleStart(farmingFloor);
            }
        }

        public string GetSelectionDesc()
        {
            if (farmingFloor == tryFloor)
                return $"{tryFloor}층 [도전]";
            else return $"{farmingFloor}층 [완료]";
        }
    }

    //public class ToTown : ISelections
    //{

    //    public void Execute()
    //    {
    //        SceneManager.ChangeCurrentScene(SceneNum.Town);
    //    }

    //    public string GetSelectionDesc()
    //    {
    //        return "마을로 이동하기";
    //    }
    //}

    //public class ToDungeon : ISelections
    //{

    //    public void Execute()
    //    {
    //        SceneManager.ChangeCurrentScene(SceneNum.Dungeon);
    //    }

    //    public string GetSelectionDesc()
    //    {
    //        return "던전으로 이동하기";
    //    }
    //}

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
