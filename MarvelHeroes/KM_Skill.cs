using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public enum SkillNames
    {
        None,
        LaserCutter,
        RepulsorGun,
        NanoSuit,
        SpiderWeb,
        SummoningArrow,
        HogosWhiteMagic,
        VibraniumPunch,
        HulkShouting

    }
    public class Skill
    {
        public int Level { get; protected set; }
        public SkillNames SkillEnum { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int skillAtk { get; protected set; }
        public int UseMp { get; protected set; }
        public int Adddef { get; protected set; }
        public int Adddex { get; protected set; }


        public Skill(int _Level = 1,SkillNames skillName = SkillNames.None ,string _Name = "", string _Description = "", int _skillAtk = 0, int _UseMp = 0, int _Adddef = 0,int _Adddex = 0)
        {
            Level = _Level;
            SkillEnum = skillName;
            Name = _Name;
            Description = _Description;
            skillAtk = _skillAtk;
            UseMp = _UseMp;
            Adddef = _Adddef;
            Adddex = _Adddex;
        }

        public void UseSkill(Skill skill)
        {
            

        }

        

        public static void SkillListView(List<Skill> skills)
        {
            for (int i = 1; i <= skills.Count; i++)
            {
                Console.WriteLine("{0} {1} - MP {2}\n", i, skills[i].Name, skills[i].UseMp);
                Console.WriteLine("{0}", skills[i].Description);
            }

        }

    }
    public class IronMan : Skill
    {
        public List<Skill> ironManSkills  = new List<Skill>()
        {
            new Skill(_Level: 1, SkillNames.LaserCutter,"레이저 커터", "공격력 50으로 모든 적을 공격합니다", _skillAtk: 50, _UseMp: 50),
            new Skill(_Level: 1, SkillNames.RepulsorGun,"리펄서건","회피율이 50%로 증가 합니다.",_Adddex: 5, _UseMp:10)
        };
        public List<Skill> IronManSKills
        {
            get { return ironManSkills; }
        }
    }

    public class SpiderMan : Skill
    {
        public List<Skill> spiderManSkills = new List<Skill>()
        {
            new Skill(_Level: 1, SkillNames.NanoSuit,"나노슈트 발동!", " 공격력, 방어력을 20씩 올립니다.", _skillAtk:20, _Adddef: 20,_UseMp: 50),
            new Skill(_Level: 1, SkillNames.SpiderWeb,"거미줄 발사!","거미줄 발사해 10의 데미지와 명중률을 50% 다운 시킵니다.",_skillAtk: 10, _Adddex: 5, _UseMp:50)
        };
        public List<Skill> SpiderManSKills
        {
            get { return spiderManSkills; }
        }
    }

}
