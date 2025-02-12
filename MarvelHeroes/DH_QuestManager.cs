using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class QuestManager
    {
        public static List<Quest> questlist_Chief; //모든퀘스트 목록
        public static List<Quest> questlist_Investigation; //모든퀘스트 목록
        public List<Quest> acceptQuest; //플레이어가 받은 퀘스트



        public QuestManager()
        {
            questlist_Chief = new List<Quest>
            {
            new HuntQuest("시장의 토벌 의뢰", "시장이 당신에게 근처 쉐도우 가디언의 처리를 부탁했습니다.\n쉐도우 가디언 3마리를 처리합시다.", 3, 1101, "쉐도우 가디언"),
            new EquipQuest("영웅의 품격", "사장의 신뢰를 위해 방어구를 입은 멋진 모습을 보여줍시다. ", 1, 1201,ItemType.Amor),
            new LevelQuest("시장의 신뢰", "시장은 강한 자를 원합니다. 시장의 신뢰를 위해 레벨 10을 달성하세요.", 10, 1301)
            };

            questlist_Investigation = new List<Quest>
            {
            new HuntQuest("미망인의 복수", "미망인의 남편을 죽인 메탈 울프 3마리를 사냥해옵시다.", 3, 2101, "메탈 울프"),
            new EquipQuest("전투준비", "무기를 장착하여 전투에 대비합시다.", 1, 2201, ItemType.Weapon),
            new LevelQuest("부족한 힘", "적에 비해 당신은 약합니다. 레벨 15을 달성합시다.", 15, 2301)
            };

            acceptQuest = new List<Quest>(); //리스트 초기화
        }

        public void QuestStart(string questTrigger)
        {
            List<Quest> list = new List<Quest>();

            if (questTrigger == "Chief")
            {
                list = questlist_Chief;
            }
            else if (questTrigger == "Investigation")
            {
                list = questlist_Investigation;
            }
            Console.WriteLine("메인 퀘스트");
            Console.WriteLine("[퀘스트 목록]");

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i].Name} | {list[i].Descrip}");
            }

            Console.WriteLine("");
            Console.WriteLine("어떤 퀘스트를 수락하시겠습니까?");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);
                if (isNumber == false || list.Count <= sellecNumber)
                {

                    Console.WriteLine("잘못된 입력입니다");
                    continue;
                }
                if (sellecNumber == 0)
                {
                    GameManager.Instance.SM.ChangeCurrentScene(SceneNum.Town);
                    break;

                }

                AcceptQuest(list[sellecNumber - 1]);
                break;

            }
        }

        public void AcceptQuest(Quest quest)
        {
            //public bool Contains (T item);
            //Contain()함수는 item은 List<T>에서 찾을 객체(또는 값) 이며,
            //item이 클래스의 객체 또는 배열과 같은 참조 타입에 대해서 값은 null이 될 수 있습니다.
            //item이 존재하면 Contain() 함수는 True를 반환하고 그렇지 않으면 False를 반환합니다.
            if (!acceptQuest.Contains(quest)) //이미 받은 퀘스트가 아닐때
            {
                acceptQuest.Add(quest);//퀘스트 추가
                Console.WriteLine($"[퀘스트 수락] {quest.Name} 퀘스트를 받았습니다.");

            }
            else
            {
                Console.WriteLine("이미 받은 퀘스트입니다.");
            }
        }

        public void CheckCompleteQuest(Monster Monster, Player player = null, EquipItem item = null)
        {
            List<Quest> completedQuests = new List<Quest>();
            foreach (Quest quest in acceptQuest)
            {
                if (quest.IsCompleted(Monster, player, item))
                {
                    completedQuests.Add(quest);
                    acceptQuest.Remove(quest);
                }
            }

            // 완료된 퀘스트 처리
            foreach (Quest completed in completedQuests)
            {
                ClearQuest(completed);
            }
        }

        public void ClearQuest(Quest quest)
        {
            if (acceptQuest.Contains(quest)) // 받은 퀘스트인지 확인
            {
                acceptQuest.Remove(quest); // 완료된 퀘스트 제거
                quest.Questclear();
                Console.WriteLine($"[퀘스트 완료] {quest.Name} 퀘스트를 완료했습니다!");
            }
            else
            {
                Console.WriteLine("받지 않은 퀘스트입니다.");
            }
        }

        public void GiveReward(Quest quest, Player player)
        {
            List<Item> itemlist = GameManager.Instance.IM.Alltems;
            while (true)
            {
                int random = new Random().Next(0,itemlist.Count());
                if (!GameManager.Instance.inventory.items.Contains(itemlist[random]))
                {
                    //아이템 주는 로직 작성
                    GameManager.Instance.inventory.AddItem(itemlist[random]);
                    Console.WriteLine($"보상획득! 아이템 획득 {itemlist[random].Name}");
                    break;
                }

            }

            int RewardGold = new Random().Next(100, 200);
            player.Gold += RewardGold;
            Console.WriteLine($"보상획득! 골드 + {RewardGold}");
        }

    }

    public abstract class Quest
    {
        public string Name { get; set; }
        public string Descrip { get; set; }
        public int Demand { get; set; }
        public int QuestId { get; set; }

        
        public Quest(string name, string descrip, int demand, int questId) 
        {
            Name = name;
            Descrip = descrip;
            Demand = demand;
            QuestId = questId;

        }

        public abstract bool IsCompleted(Monster Monster, Player player, EquipItem item); // 퀘스트 완료 체크

        public abstract void Questclear();
    }

    public class EquipQuest : Quest
    {

        public ItemType RequiredType { get; set; } // 장착해야 할 아이템 타입

        public EquipQuest(string name, string descrip, int demand, int questId, ItemType requiredType)
            : base(name, descrip, demand, questId)
        {
            RequiredType = requiredType;
        }
        public override bool IsCompleted(Monster Monster, Player player, EquipItem item)
        {
            if (item == null) return false;
            // 무기 장착 퀘스트 확인
            if (RequiredType == ItemType.Weapon && item.IsEquip != false)
            {
                return true;
            }

            // 방어구 장착 퀘스트 확인
            if (RequiredType == ItemType.Amor && item.IsEquip != false)
            {

                return true;
            }

            return false; // 아직 퀘스트 완료 조건을 충족하지 않음
        }

        public override void Questclear()
        {
            Console.WriteLine($"[아이템 퀘스트 완료] {Name}");
        }
    }

    public class HuntQuest : Quest
    {
        private string targetMonster;
        public HuntQuest(string name, string descrip, int demand, int questId, string monster)
            : base(name, descrip, demand, questId)
        {
            targetMonster = monster; 
        }
        public override bool IsCompleted(Monster Monster, Player player, EquipItem item)
        {

                if (Monster.MonsterName == targetMonster)
                    Demand--;
            
                return Demand <= 0;
        }

        public override void Questclear()
        {
            Console.WriteLine($"[사냥 퀘스트 완료] {Name}");
        }
    }

    public class LevelQuest : Quest
    {
        public LevelQuest(string name, string descrip, int demand, int questId)
            : base(name, descrip, demand, questId)
        {

        }
        public override bool IsCompleted(Monster Monster, Player player, EquipItem item)
        {
            return player.Level >= Demand;
        }

        public override void Questclear()
        {
            Console.WriteLine($"[레벨업 퀘스트 완료] {Name}");
        }
    }

}
