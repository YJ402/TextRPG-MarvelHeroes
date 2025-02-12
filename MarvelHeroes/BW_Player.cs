using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//함수나,클래스명은 PascalCase 메서드, 프로퍼티 이름 등에 사용됩니다. 단어의 첫 글자는 대문자로 시작하며, 이후 단어의 첫 글자도 대문자로 표기합니다
//변수명은 camelCase 변수, 매개변수, 로컬 변수 이름 등에 사용됩니다 단어의 첫 글자는 소문자로 시작하며, 이후 단어의 첫 글자는 대문자로 표기합니다


//-캐릭터의 정보를 표시합니다.
//-7개의 속성을 가지고 있습니다.
// 레벨 / 이름 / 직업 / 공격력 / 방어력 / 체력 / Gold
//- 처음 기본값은 **이름을 제외하고는 아래와 동일하게** 만들어주세요
//- 이후 장착한 아이템에 따라 수치가 변경 될 수 있습니다.

namespace MarvelHeroes
{

    //JobType job → 선택한 직업
    //int level → 캐릭터 레벨
    //string name → 캐릭터 이름
    //int gold → 보유한 골드(돈)
    //int atk → 공격력
    //int equipAtk → 장비 공격력
    //int def → 기본 방어력
    //int hp → 체력
    //int mp → 마력
    //int maxHp → 최대체력
    //int CH → 크리티컬 
    //int Dxi → 민첩성


    public class Player : Unit
    {
        public string Name { get; set; }
        public JobType PlayerJob { get; set; }
        public int EquipAtk { get; set; }
        public int EquipDef { get; set; }
        public int Gold { get; set; }
        public int mp;
        public int Mp
        {
            get => mp;

            set
            {
                if (value <= 0)
                {
                    mp = 0;
                    
                }
                else if(value > MaxMp)
                {
                    mp = MaxMp;
                }
                else mp = value;
            }
        }
        public int Hp
        {
            get => hp;

            set
            {
                if (value <= 0)
                {
                    hp = 0;
                    isDead = true;
                }
                else if (value > MaxHp)
                {
                    hp = MaxHp;
                }
                else hp = value;
            }
        }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }

        public int xp;
        public int maxXp { get; set; }

        public int Xp
        {
            get { return xp; }
            set
            {
                if (xp != value)
                {
                    if (maxXp > 0 && value >= maxXp)
                    {
                        xp = value;
                        LevelUp();
                    }
                    else { xp = value; }
                }
            }
        }

        [JsonConstructor]
        public Player(string name, JobType playerJob, int equipAtk, int equipDef, int gold, int mp, int maxMp, int hp, int maxHp, int xp, int _maxXp, int level, int atk, int def, int critical, int dexterity, bool isDead = false) : base(level, atk, def, hp, critical, dexterity, isDead)
        {
            Name = name;
            PlayerJob = playerJob;
            EquipAtk = equipAtk;
            EquipDef = equipDef;
            Gold = gold;
            

            MaxMp = maxMp;
            MaxHp = maxHp;
            maxXp = _maxXp;

            Mp = mp;
            Hp = hp;
            Xp = xp;
        }

        public Player(int _Level, string name, int gold, bool _isDead, int xp, int _maxXp)
            : base (0, 0, 0, 0, 0, 0, false)

        {
            // 이런식으로 되있는게 속성
            Level = _Level;
            Name = name;
            //PlayerJob = Job.jobStats[jobtypeName].name;
            //Atk = Job.jobStats[jobtypeName].atk;
            //Def = Job.jobStats[jobtypeName].def;
            //Hp = Job.jobStats[jobtypeName].hp;
            //Mp = Job.jobStats[jobtypeName].mp;
            //Critical = Job.jobStats[jobtypeName].critical;
            //Dexterity = Job.jobStats[jobtypeName].dexerity;
            Gold = gold;
            //MaxHp = Hp;
            //MaxMp = Job.jobStats[jobtypeName].mp;
            maxXp = _maxXp;
            Xp = 0;

         
        }

        public void LevelUp()
        {
            do
            {
                Level += 1;
                Atk += 1;
                Def += 1;
                xp = xp - maxXp;
                maxXp = 20 + (4 * (Level - 1) * (Level - 1)) - (10 * (Level - 1));
            } while (xp >= maxXp);
        }


 
        //public override Player TakeHpHeal(int heal)
        //{
        //    int newHp;

        //    if(Hp >= 100) newHp = 100;
        //    else newHp = Hp + heal;

        //    return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, newHp, Mp, MaxHp, Critical, Dexterity, isDead);
        //}
        //public Player TakeMana(int useMp)
        //{
        //    int newMp;

        //    if (Mp <= 0)
        //    {
        //        newMp = 0;
        //    }
        //    else newMp = Mp - useMp;

        //    return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, newMp, MaxHp, Critical, Dexterity, isDead);
        //}
        //public Player TakeMpHeal(int heal)
        //{
        //    int newMp;

        //    if (Hp >= 100) newMp = 100;
        //    else newMp = Hp + heal;

        //    return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, newMp, MaxHp, Critical, Dexterity, isDead);
        //}


        // 아이언맨 리펄서건 스킬
        public void IronManAddDex(int Adddex, int number)
        {
            int newDex = 0;

            Mp -= 10;

            switch(number)
            {
                case 0:
                    newDex = Dexterity + Adddex;

                    break;
                case 1:
                    newDex = Dexterity - Adddex;
                    break;                  
            }
        }
        // 스파이더맨 나노슈트 스킬
        public void NanoSuit(int addatk, int adddef, int number)
        {


            switch (number)
            {
                case 0:
                    Mp -= 25;
                    Atk += addatk;
                    Def += adddef;
                    break;
                case 1:
                    Atk -= addatk;
                    Def -= adddef;
                    break;
            }
        }

        // 직업 스킬 리스트 가져오기
        public List<Skill> JobSkills(Player player)
        {
            List<Skill> skills = new List<Skill>();

            switch (player.PlayerJob)
            {
                case JobType.IronMan:
                    IronMan ironMan = new IronMan();
                    skills = ironMan.ironManSkills;
                    return skills;
                case JobType.SpiderMan:
                    SpiderMan spiderMan = new SpiderMan();
                    skills = spiderMan.spiderManSkills;
                    return skills;
                case JobType.DoctorStrange:
                    DoctorStrange doctorStrange = new DoctorStrange();
                    skills = doctorStrange.DoctorStrangeSKills;
                    return skills;
                case JobType.Hulk:
                    Hulk hulk = new Hulk();
                    skills = hulk.HulkSKills;
                    return skills;
                default:
                    Console.WriteLine("스킬이 없습니다.");
                    Console.ReadKey();
                    return skills;
            }

        }

    }
}
