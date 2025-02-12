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
            GameManager.Instance.QM.QuestStart("Chief");
        }

        public string GetSelectionDesc()
        {
            return "닉퓨리와 대화하기"; // 선택지 설명
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
            GameManager.Instance.player.Gold -= 10;
            int random = new Random().Next(1, 100);
            if (random > 70)
            {
                GameManager.Instance.QM.QuestStart("investigation");
            }
            else { GameView.PrintText("뉴욕은 안전한 것 같다!", 1000, ConsoleColor.Magenta);  }
        }

        public string GetSelectionDesc()
        {
            return "뉴욕 길거리 치안 유지 활동하기(10G)"; // 선택지 설명
        }
    }

    public class Trade_Buy : ISelections
    {
        List<Item> playerInventory;
        List<Item> equipItemList;
        List<Item> usingItemList;

        List<Item> shopInventory;

        public Trade_Buy()
        {
            playerInventory = GameManager.Instance.inventory.items;
            equipItemList = GameManager.Instance.IM.equipItems;
            usingItemList = GameManager.Instance.IM.usingItems;
        }

        public void Execute()
        {

            //샵 인벤토리 업데이트 (인벤토리에 없는 것만 + 
            shopInventory = new List<Item>();

            foreach (Item item in equipItemList)
            {
                if (!playerInventory.Contains(item))
                {
                    shopInventory.Add(item);
                }
            }

            foreach (Item item in usingItemList)
            {
                shopInventory.Add(item);
            }

            while (true)
            {
                //초기화
                int num = 0;


                //선택지 보여주고
                Console.Clear();
                GameView.ViewSceneNameAndDesc1(GameManager.Instance.SM.currentScene);
                Console.WriteLine("\n[구매 가능 아이템 목록]");
                for (; num < shopInventory.Count(); num++)
                {
                        Console.WriteLine(GameView.DisplayInven(num, 0, shopInventory));
                }
                Console.WriteLine("\n0. 나가기");

                //입력 받고
                int input;
                Console.Write("\n구매를 원하는 아이템 번호를 입력해주세요.\n>>");
                while (true)
                {
                    //입력값 유효성 검사
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        if (input == 0) { break; }
                        else if (input-1 < shopInventory.Count() && input > 0)
                        {
                            if (shopInventory[input - 1].Cost > GameManager.Instance.player.Gold)
                            {
                                Console.WriteLine("골드가 부족합니다.");
                                Console.Write(">> ");
                                continue;
                            }
                            break;
                        }
                    }
                    Console.WriteLine("잘못된 입력입니다");
                    Console.Write(">> ");
                }

                //선택 아이템 플레이어 인벤으로 이동, 돈 지불
                if (input == 0)
                    break;
                GameManager.Instance.inventory.AddItem(shopInventory[input - 1]);
                GameManager.Instance.player.Gold -= shopInventory[input - 1].Cost;
                shopInventory.Remove(shopInventory[input - 1]);
            }
        }

        public string GetSelectionDesc()
        {
            return "아이템 구입"; // 선택지 설명
        }
    }

    public class Trade_Sell : ISelections
    {
        List<Item> playerInventory;

        public Trade_Sell()
        {
            playerInventory = GameManager.Instance.inventory.items;
        }

        public void Execute()
        {

            while (true)
            {
                int num = 0;

                //선택지 보여주고
                Console.Clear();
                GameView.ViewSceneNameAndDesc1(GameManager.Instance.SM.currentScene);
                Console.WriteLine("\n[판매 가능 아이템 목록]");
                for (; num < playerInventory.Count(); num++)
                {
                    Console.WriteLine(GameView.DisplayInven(num, 0, playerInventory));
                }
                Console.WriteLine("\n0. 나가기");
                //입력 받고
                int input;
                Console.Write("\n판매를 원하는 아이템 번호를 입력해주세요.\n>>");
                while (true)
                {
                    //입력값 유효성 검사
                    if (int.TryParse(Console.ReadLine(), out input))
                    {
                        if (input == 0) { break; }
                        else if (input-1 < playerInventory.Count() && input > 0)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("잘못된 입력입니다");
                    Console.Write(">> ");
                }

                //선택 아이템 플레이어 인벤으로 이동, 돈 지불
                if (input == 0)
                    break;
                Item targetItem = playerInventory[input - 1];
                if (targetItem is UsingItem)
                {
                    (targetItem as UsingItem).Quantity--;
                }
                else if(!targetItem.IsEquip)
                {
                    targetItem.Use(GameManager.Instance.player);
                    GameManager.Instance.inventory.RemoveItem(targetItem);
                }
                else 
                { 
                    GameManager.Instance.inventory.RemoveItem(targetItem); 
                }
                GameManager.Instance.player.Gold += (int)(targetItem.Cost*0.6);
            }
        }

        public string GetSelectionDesc()
        {
            return "아이템 판매"; // 선택지 설명
        }
    }

    public class ToUI : ISelections
    {
        //SceneNum currentScene;
        //public ToUI(SceneNum cucScene)
        //{
        //    currentScene = cucScene;
        //}
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
                    sceneKorName = "뉴욕";
                    break;
                case SceneNum.Dungeon:
                    sceneKorName = "인피니티 타워";
                    break;
                case SceneNum.Shop:
                    sceneKorName = "쉴드 무기고";
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


    public class SelectClass : ISelections
    {
        JobType selectedJob;
        Job job;

        public SelectClass(JobType _selectedJob)
        {
            selectedJob = _selectedJob;
            job = new Job();
        }

        public void Execute()
        {
            Console.WriteLine($"{selectedJob}전직하기 트리거");//1_Class로 전직하기 트리거 ;
            SetJobToPlayer();
        }

        public string GetSelectionDesc()
        {
            return $"{job.jobStats[selectedJob].name}";
        }

        public void SetJobToPlayer()
        {
            Player player = GameManager.Instance.player;

            player.Atk = job.jobStats[selectedJob].atk;
            player.PlayerJob = selectedJob;
            player.Def = job.jobStats[selectedJob].def;
            player.MaxHp = job.jobStats[selectedJob].hp;
            player.MaxMp = job.jobStats[selectedJob].mp;
            player.Hp = job.jobStats[selectedJob].hp;
            player.Mp = job.jobStats[selectedJob].mp;
            player.Critical = job.jobStats[selectedJob].critical;
            player.Dexterity = job.jobStats[selectedJob].dexerity;
        }
    }
}
