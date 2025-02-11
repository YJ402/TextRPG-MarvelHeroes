using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class QuestManager
    {
        List<Quest> questlist; //모든퀘스트 목록
        List<Quest> acceptQuest; //플레이어가 받은 퀘스트

        public QuestManager()
        {
            questlist = new List<Quest>
            {
            new HuntQuest("이장의 부탁1", "이장이 당신에게 근처 고블린 처리를 부탁했습니다.", 3, 101, "Goblin"),
            new HuntQuest("이장의 부탁2", "이장이 당신에게 근처 고블린 처리를 부탁했습니다.", 5, 102, "Goblin"),
            new ItemQuest("마법 물약 수집", "마법 물약을 2개 모아오세요.", 2, 201, "마법 물약"),
            new LevelQuest("레벨업 도전", "레벨 10을 달성하세요.", 10, 301)
            };

            acceptQuest = new List<Quest>(); //리스트 초기화
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

        public void CheckCompleteQuest(List<Monster> killMonster, InventoryUI inventory, Player player)
        {
            List<Quest> completedQuests = new List<Quest>();
            foreach (Quest quest in acceptQuest)
            {
                if (quest.IsCompleted(killMonster, inventory, player))
                {
                    completedQuests.Add(quest);
                    //acceptQuest.Remove(quest);
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

        public abstract bool IsCompleted(List<Monster> killMonster, InventoryUI inventory, Player player); // 퀘스트 완료 체크

        public abstract void Questclear();
    }

    public class ItemQuest : Quest
    {
        public string RequiredItem { get; set; }
        public ItemQuest(string name, string descrip, int demand, int questId, string requiredItem)
            : base(name, descrip, demand, questId)
        {
            RequiredItem = requiredItem;
        }
        public override bool IsCompleted(List<Monster> killMonster, InventoryUI inventory, Player player)
        {
            foreach (Item i in InvetoryUI)
            {
                if (i.Name = RequiredItem)
                {
                    YJ_Inventory.destroy(i);
                    return true;
                }
            }
            return false;
        }

        public override void Questclear()
        {
            Console.WriteLine($"[아이템 퀘스트 완료] {Name}");
        }
    }

    public class HuntQuest : Quest
    {
        string targetMonster;
        public HuntQuest(string name, string descrip, int demand, int questId, string monster)
            : base(name, descrip, demand, questId)
        {
            targetMonster = monster; 
        }
        public override bool IsCompleted(List<Monster> killMonster, InventoryUI inventory, Player player)
        {
            foreach (Monster m in killMonster)
            {
                if (m.MonsterName == targetMonster)
                    Demand--;
            }
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
        public override bool IsCompleted(List<Monster> killMonster, InventoryUI inventory, Player player)
        {
            return player.Level >= Demand;
        }

        public override void Questclear()
        {
            Console.WriteLine($"[레벨업 퀘스트 완료] {Name}");
        }
    }

}
