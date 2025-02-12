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
        private static GameManager instance;
        //프로퍼티를 활용한 싱글톤
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        //게임 전역에 필요한 매니저 선언
        public SceneManager SM{ get; private set; }
        public ItemManager IM { get; private set; }
        public QuestManager QM { get; private set; }
        public Inventory inventory;
        public Player player;
        //매니저 할당
        private GameManager() { }
        public void Initialize()
        {
            IM = new ItemManager();
            inventory = new Inventory();
            SM = new SceneManager();
            QM = new QuestManager();
        }

        //게임 시작 메서드
        public void GameStart()
        {
            Initialize();
            //1.캐릭터 생성씬(1회용)
            // 플레이어 할당
            player = new Player(1, "", 100, false, 0, 10);
            SM.currentScene = new CreateCharacterScene(); // 일회용이라서 여기서 생성.
            while (true)
            {
                //이름 짓기
                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                GameView.ViewSceneInput2_2(SM.currentScene);
                while (true)
                {
                    string temp1 = Console.ReadLine();
                    if (temp1.Count() < 10 && temp1.Count() > 0)
                    {
                        Console.WriteLine("플레이어의 이름에 temp 입력 메서드"); // 이름 바꾸는 메서드 트리거
                        player.Name = temp1;
                        break;
                    }
                    Console.WriteLine("1~10자 사이의 문자를 입력해주세요.");
                }

                //직업 선택
                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                int temp2 = GameView.ViewGetSceneSelect2(SM.currentScene, "원하시는 직업을 선택해주세요.");
                SM.currentScene.sceneSelections[temp2].Execute();

                //씬 변경 후 종료
                SM.currentScene = SM.townScene;
                break;
            }

            // 플레이어 할당
            //player = new Player(1, "", 100, false, JobType.IronMan, 0, 10);

            //2.게임 루프
            bool isRunning = true;
            while (isRunning)
            {
                GameManager.Instance.inventory.AddItem(new EquipItem("아이언맨 기본 갑옷", ItemType.Amor, JobType.IronMan, 5, "아이언맨의 기본방어구", 1000));
                GameView.ViewSceneNameAndDesc1(SM.currentScene);
                int temp = GameView.ViewGetSceneSelect2(SM.currentScene);
                SM.currentScene.sceneSelections[temp].Execute();
            }
        }
    }
}
