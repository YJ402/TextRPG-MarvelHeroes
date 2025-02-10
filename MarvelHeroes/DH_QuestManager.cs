using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    internal class DH_QuestManager
    {
        List<Quest> questlist;
        List<Quest> acceptQuest;

        public DH_QuestManager()
        {
            questlist = new List<Quest>
            {
                new Quest ("이장의 부탁1","이장이 당신에게 근처 치타우리의 처리를 부탁했습니다.", 0, 101),
                new Quest ("이장의 부탁2","이장이 당신에게 근처 치타우리의 처리를 부탁했습니다.", 0, 102),
                new Quest ("이장의 부탁3","이장이 당신에게 근처 치타우리의 처리를 부탁했습니다.", 0, 103)
            };

            acceptQuest = new List<Quest>();
        }

        public void AcceptQuest(Quest quest)
        {
            //Contain()함수는 
            if (!acceptQuest.Contains(quest);
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

        public abstract void Questclear();
    }

    public class ItemQuest : Quest
    {
        public ItemQuest(string name, string descrip, int demand, int questId) : base(name, descrip, demand, questId)
        {

        }

        public override void Questclear()
        {

        }
    }

    public class HuntQuest : Quest
    {
        public HuntQuest(string name, string descrip, int demand, int questId) : base(name, descrip, demand, questId)
        {

        }

        public override void Questclear()
        {

        }
    }

    public class LevelQuest : Quest
    {
        public LevelQuest(string name, string descrip, int demand, int questId) : base(name, descrip, demand, questId)
        {

        }

        public override void Questclear()
        {

        }
    }

}
