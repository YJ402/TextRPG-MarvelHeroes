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
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int hp;
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
                else hp = value;
            }
        }
        public int Critical { get; set; }
        public int Dexterity { get; set; }
        public bool isDead { get; set; }


        public Unit() { }

        public Unit(int _Level, int _Atk, int _Def, int _Hp, int _Critical, int _Dexterity, bool _isdDead = false)
        {
            Level = _Level;
            Atk = _Atk;
            Def = _Def;
            Hp = _Hp;
            Critical = _Critical;
            Dexterity = _Dexterity;
            isDead = _isdDead;

        }

        // 체력 감소
        public virtual void TakeDamge(int damage)
        {
            if (Hp <= 0)
            {
                isDead = true;
                Hp = 0;
            }       
            else 
            {
                Hp -= damage;
            }

        }

        //// 체력 회복
        //public virtual Unit TakeHpHeal(int heal)
        //{
        //    int newHp = Hp + heal;
        //    return new Unit(Level,Atk, Def, newHp, Critical, Dexterity);
        //}

        //public virtual Unit TakeDex(int damge)
        //{
        //    int newDex = Dexterity - damge;

        //    return new Unit(Level, Atk, Def, Hp, Critical, newDex);
        //}

        //// 플레이어 생존 확인
        public virtual void IsDead(Unit unit, int beforeHp)
        {
            if (unit.Hp <= 0)
            {
                Console.WriteLine("HP {0} -> Dead\n", unit.Hp);
            }
            else
            {
                Console.WriteLine("HP {0} -> {1}\n", beforeHp, unit.Hp);
            }

        }
    }
}
