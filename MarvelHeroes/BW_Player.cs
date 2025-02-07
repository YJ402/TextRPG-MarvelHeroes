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
    }
}
