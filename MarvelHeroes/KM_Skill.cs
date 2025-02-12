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
        public int SkillLevel { get; set; }
        public SkillNames SkillEnum { get; set; }
        public string SkillName { get; set; }
        public string Description { get; set; }
        public int skillAtk { get; set; }
        public int UseMp { get; set; }
        public int Adddef { get; set; }
        public int Adddex { get; set; }

        public int playerAtk;


        public Skill(int _SkillLevel = 1,SkillNames _skillEnum = SkillNames.None ,string _SKillName = "", string _Description = "", int _skillAtk = 0, int _UseMp = 0, int _Adddef = 0,int _Adddex = 0)
        {
            SkillLevel = _SkillLevel;
            SkillEnum = _skillEnum;
            SkillName = _SKillName;
            Description = _Description;
            skillAtk = _skillAtk;
            UseMp = _UseMp;
            Adddef = _Adddef;
            Adddex = _Adddex;
            playerAtk = GameManager.Instance.player.Atk;
            
        }

        public void UseSkill(Skill skill)
        {
            

        }

        

        public static void SkillListView(List<Skill> skills)
        {
            for (int i = 1; i <= skills.Count; i++)
            {
                Console.WriteLine(" {0}. {1} - MP {2}", i, skills[i-1].SkillName, skills[i-1].UseMp);
                Console.WriteLine("   {0}\n", skills[i - 1].Description);
            }
        }

    }
    public class IronMan : Skill
    {
        public List<Skill> ironManSkills;

        public IronMan()
        {    
           ironManSkills = new List<Skill>()
            {
            new Skill(_SkillLevel: 1, SkillNames.LaserCutter, "레이저 커터", "공격력계수 0.7 만큼 모든 적을 공격합니다", _skillAtk: (int)(playerAtk*0.7) , _UseMp: 50),
            new Skill(_SkillLevel: 1, SkillNames.RepulsorGun, "리펄서건", "회피율이 50%로 증가 합니다.", _Adddex: 5, _UseMp: 10)
            };
        }

        public List<Skill> IronManSkills
        {
            get { return ironManSkills; }
        }


    }

    public class SpiderMan : Skill
    {
        public List<Skill> spiderManSkills;

        public SpiderMan()
        {
            List<Skill> spiderManSkills = new List<Skill>()
            {
            new Skill(_SkillLevel: 1, SkillNames.NanoSuit,"나노슈트 발동!", " 공격력, 방어력을 20씩 올립니다.", _skillAtk:20, _Adddef: 20,_UseMp: 25),
            new Skill(_SkillLevel: 1, SkillNames.SpiderWeb,"거미줄 발사!","거미줄 발사해 공격력 계수 0.7의 데미지와 명중률을 50% 다운 시킵니다.",_skillAtk: (int)(playerAtk*0.7), _Adddex: 50, _UseMp:25)
            };

        }

        public List<Skill> SpiderManSKills
        {
            get { return spiderManSkills; }
        }
    }

    public class DoctorStrange : Skill
    {
        public List<Skill> doctorStrangeSkills;

        public DoctorStrange()
        {
            doctorStrangeSkills = new List<Skill>()
            {
            new Skill(_SkillLevel: 1, SkillNames.SummoningArrow,"발탁의 화살", " 지면에 폭발을 일으켜 랜덤한 데미지를 줍니다.(10~50)",_skillAtk:10, _UseMp: 50),
            new Skill(_SkillLevel: 1, SkillNames.HogosWhiteMagic,"호고스의 백마법","상대방의 공격페이지 무효화하고 랜덤으로 2마리에게 20의 데미지를 줍니다.",_skillAtk: 20, _UseMp:20)
            };
        }

        public List<Skill> DoctorStrangeSKills
        {
            get { return doctorStrangeSkills; }
        }
    }



    public class Hulk : Skill
    {
        public List<Skill> hulkSkills;

        public Hulk()
        {
             hulkSkills = new List<Skill>()
            {
            new Skill(_SkillLevel: 1, SkillNames.VibraniumPunch,"비뷰라늄 펀치", "비브라늄 너클을 사용하여 100의 데미를 가합니다.",_skillAtk:100, _UseMp: 10),
            new Skill(_SkillLevel: 1, SkillNames.HulkShouting,"헐크의 샤우팅","분노와 함께 강력한 소리를 내뿜어 영구적으로 공격력을 낮춥니다.",_skillAtk: 50, _UseMp:50)
            };

        }
        public List<Skill> HulkSKills
        {
            get { return hulkSkills; }
        }
    }

}
