using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Player
    {
        public int Level { get; set; } 
        public string Name { get; set; }
        public string Job { get; }
        public int Atk { get; }
        public int EquipAtk { get; }
        public int Def { get; }
        public int EquipDef { get; }
        public int Gold { get; set; }
        public int Hp { get; }
        public int Mp { get; }
        public int MaxHp { get; }
        public int Critical {  get; }

        public int Dexterity { get; }

        public Player(int level, string name, string job, int atk, int equipAtk, int def, int equipDef, int gold, int hp, int mp, int maxHp, int CH, int Dxi)
        {
            // 이런식으로 되있는게 속성

            Level = 1;    
            Name = name;
            Job = job;
            Atk = atk;
            EquipAtk = 0;
            Def = def;
            EquipDef = 0;
            Gold = 1500;
            Hp = hp;
            Mp = mp;
            MaxHp = maxHp;
            Critical = CH;
            Dexterity = Dxi;
        }

        // 체력 감소
        public Player TakeDamge(int damge)
        {
            int newHp = Hp - damge;

            return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, newHp, Mp, MaxHp, Critical, Dexterity);
        }
        // 마나 감소
        public Player TakeMana(int useMp)
        {
            int newMp = Mp - useMp;

            return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, newMp, MaxHp, Critical, Dexterity);
        }
        // 체력 회복
        public Player TakeHpHeal(int heal)
        {
            int newHp = Hp + heal;
            return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, newHp, Mp, MaxHp, Critical, Dexterity);
        }
        // 마나 회복
        public Player TakeMpHeal(int heal)
        {
            int newMp = Mp + heal;
            return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, newMp, MaxHp, Critical, Dexterity);
        }
        // 플레이어 생존 확인
        public Player IsDead(Player player, int beforeHp)
        {
            if (player.Hp <= 0)
            {
                int newHp = 0;
                Console.WriteLine("HP {0} -> Dead\n", beforeHp);
                return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, newHp, Mp, MaxHp, Critical, Dexterity);
            }
            else
            {
                Console.WriteLine("HP {0} -> {1}\n", beforeHp, player.Hp);
                return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, Mp, MaxHp, Critical, Dexterity);
            }

        }

        // 아이언맨 리펄서건 스킬
        public Player IronManAddDex(int Adddex, int number)
        {
            int newDex = 0;

            switch(number)
            {
                case 0:
                    newDex = Dexterity + Adddex;
                    break;
                case 1:
                    newDex = Dexterity - Adddex;
                    break;
                    
            }

            return new Player(Level, Name, Job, Atk, EquipAtk, Def, EquipDef, Gold, Hp, Mp, MaxHp, Critical, newDex);
        }
        // 스파이더맨 나노슈트 스킬
        public Player NanoSuit(int addatk, int adddef, int number)
        {
            int newAtk = 0;
            int newDef = 0;
           
            switch (number)
            {
                case 0:
                    newAtk = Atk + addatk;
                    newDef = Def + adddef;
                    break;
                case 1:
                    newAtk = Atk - addatk;
                    newDef = Def - adddef;
                    break;

            }

            return new Player(Level, Name, Job, newAtk, EquipAtk, newDef, EquipDef, Gold, Hp, Mp, MaxHp, Critical, newDex);

        }


        // 직업 스킬 리스트 가져오기
        public List<Skill> JobSkills(Player player)
        {
            List<Skill> skills = new List<Skill>();

            switch (player.Job)
            {
                case "아이어맨":
                    IronMan ironMan = new IronMan();
                    skills = ironMan.IronManSKills;
                    break;
                case "스파이더맨":
                    break;
                case "닥터스트레인지":
                    break;
                case "헐크":
                    break;
            }
            return skills;
        }

    }
}
