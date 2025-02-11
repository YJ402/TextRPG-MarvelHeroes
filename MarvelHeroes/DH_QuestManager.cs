using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    internal class QuestManager
    {
        List<Quest> questlist_Chief; //모든퀘스트 목록
        List<Quest> questlist_Invagation; //모든퀘스트 목록
        List<Quest> acceptQuest; //플레이어가 받은 퀘스트


        public QuestManager()
        {
            questlist_Chief = new List<Quest>
            {
            new HuntQuest("시장의 토벌 의뢰", "시장이 당신에게 근처 치타우리족의 처리를 부탁했습니다.", 3, 1101, "Goblin"),
            new ItemQuest("영웅의 품격", "사장의 신뢰를 위해 방어구를 입은 멋진 모습을 보여줍시다. ", 1, 1201,ItemType.Amor),
            new LevelQuest("시장의 신뢰", "시장은 강한 자를 원합니다. 시장의 신뢰를 위해 레벨 10을 달성하세요.", 10, 1301)
            };

            questlist_Invagation = new List<Quest>
            {
            new HuntQuest("미망인의 복수", "미망인의 남편을 죽인 치타우리를 사냥해옵시다.", 3, 2101, "Goblin"),
            new ItemQuest("전투준비", "무기를 장착하여 전투에 대비합시다.", 1, 2201, ItemType.Weapon),
            new LevelQuest("부족한 힘", "적에 비해 당신은 약합니다. 레벨 15을 달성합시다.", 15, 2301)
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

        public void CheckCompleteQuest(List<Monster> killMonster, Player player, Item item)
        {
            List<Quest> completedQuests = new List<Quest>();
            foreach (Quest quest in acceptQuest)
            {
                if (quest.IsCompleted(killMonster, player, item))
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

        public abstract bool IsCompleted(List<Monster> killMonster, Player player, Item item); // 퀘스트 완료 체크

        public abstract void Questclear();
    }

    public class ItemQuest : Quest
    {

        public ItemType RequiredType { get; set; } // 장착해야 할 아이템 이름

        public ItemQuest(string name, string descrip, int demand, int questId, ItemType requiredType)
            : base(name, descrip, demand, questId)
        {
            RequiredType = requiredType;
        }
        public override bool IsCompleted(List<Monster> killMonster, Player player, Item item)
        {
            // ⭐ 무기 장착 퀘스트일 경우, 플레이어가 무기를 장착했는지 확인
            if (RequiredType == ItemType.Weapon && item.Use.IsEquip == true)
                return true;

            // ⭐ 방어구 장착 퀘스트일 경우, 플레이어가 방어구를 장착했는지 확인
            if (RequiredType == ItemType.Amor && item.EquippedArmor == true)
                return true;

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
        public override bool IsCompleted(List<Monster> killMonster, Player player, Item item)
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
        public override bool IsCompleted(List<Monster> killMonster, Player player, Item item)
        {
            return player.Level >= Demand;
        }

        public override void Questclear()
        {
            Console.WriteLine($"[레벨업 퀘스트 완료] {Name}");
        }
    }

}
