using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class GameManager
    {
        //싱글톤 패턴
        static GameManager instance;
        private GameManager() { }
        public static GameManager GetInstance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }

        //필요한 객체 생성
        Quest quest = new Quest();

        //메인에 실행할 게임 시작 메서드
        public void GameStart()
        {
            SceneManager SM = SceneManager.GetInstance();
            SM.currentScene = new CreateCharacterScene();

            while (true)
            {
                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                GameView.ViewSceneInput2_2(SM.currentScene);

                TownScene townScene = new TownScene();
                DungeonScene dungeonScene = new DungeonScene(); 

                while (true)
                {
                    string temp1 = Console.ReadLine();
                    if (temp1.Count() < 10 && temp1.Count() > 0)
                    {
                        Console.WriteLine("플레이어의 이름에 temp 입력 메서드"); // 이름 바꾸는 메서드 트리거
                        break;
                    }
                    Console.WriteLine("1~10자 사이의 문자를 입력해주세요.");
                }

                
                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                int temp2 = GameView.ViewGetSceneSelect2(SM.currentScene);
                SM.currentScene.sceneSelections[temp2].Execute();

                SM.currentScene = townScene;

                break;
            }

            bool isRunning = true;
            while (isRunning)
            {
                //나중에 퀘스트 클래스가 생기면 필요할수도?
                //ISelections talkToChief = new TalkToChief(quest);
                //ISelections investigate = new Investigate(quest);

                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                int temp = GameView.ViewGetSceneSelect2(SM.currentScene);
                SM.currentScene.sceneSelections[temp].Execute();
            }
        }
    }
}
