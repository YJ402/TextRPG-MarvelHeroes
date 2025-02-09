using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarvelHeroes
{
    public class Unit
    {
        public int Level { get; protected set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public int Hp { get; protected set; }
        public int Critical { get; protected set; }
        public int Dexterity { get; protected set; }
        public bool isDead { get; protected set; }

        public Unit(int _Level, int _Atk, int _Def, int _Hp, int _Critical, int _Dexterity, bool _isdDead = false)
        {
            Level = _Level;
            Atk = _Atk;
            Def = _Def;
            Hp = _Hp;
            Critical = _Critical;
            Dexterity = _Dexterity;
            
        }

        // 체력 감소
        public virtual Unit TakeDamge(int damge)
        {
            int newHp = Hp - damge;

            return new Unit(Level,Atk,Def,newHp, Critical, Dexterity);
        }

        // 체력 회복
        public virtual Unit TakeHpHeal(int heal)
        {
            int newHp = Hp + heal;
            return new Unit(Level,Atk, Def, newHp, Critical, Dexterity);
        }

        public virtual Unit TakeDex(int damge)
        {
            int newDex = Dexterity - damge;

            return new Unit(Level, Atk, Def, Hp, Critical, newDex);
        }

        // 플레이어 생존 확인
        public virtual Unit IsDead(Unit unit, int beforeHp)
        {
            if (unit.Hp <= 0)
            {
                int newHp = 0;
                Console.WriteLine("HP {0} -> Dead\n", beforeHp);
                return new Unit(Level,Atk, Def, newHp, Critical, Dexterity);
            }
            else
            {
                Console.WriteLine("HP {0} -> {1}\n", beforeHp, unit.Hp);
                return new Unit(Level,Atk, Def, Hp, Critical, Dexterity);
            }

        }
    }
}
