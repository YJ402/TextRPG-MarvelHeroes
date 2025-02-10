using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class Monster : Unit
    {
        public string MonsterName { get; set; }
        int Floor { get; set; }
        public bool IsAtk { get; set; }

        private static Random rand = new Random();

        public Monster(int _Level, string _Name, int _Hp, int _Atk, int _Def, int _floor, int _Critical, int _Dex, bool _isDaed, bool _isAtk = true)
           :base(_Level, _Atk, _Def, _Hp, _Critical, _Dex, false)
        {
            Level = _Level;
            MonsterName = _Name;
            Hp = _Hp;
            Atk = _Atk;
            Hp = _Hp;
            Atk = _Atk;
            Floor = _floor;
            Critical = _Critical;
            Dexterity = _Dex;
        }

        public Monster IsDeadBattle(Monster monster, int beforeHp)
        {
            if (monster.Hp <= 0)
            {
                monster.isDead = true;
                monster.IsAtk = false;
                Console.WriteLine("HP {0} -> Dead\n", beforeHp);
            }
            else
            {
                Console.WriteLine("HP {0} -> {1}\n", beforeHp, monster.Hp);
            }

            return monster;
        }

        public Monster IsDeadview(Monster monster)
        {

                if (monster.Hp <= 0)
                {
                    monster.isDead = true;
                    Console.WriteLine("Lv. {0}. {1} Dead", monster.Level, monster.MonsterName);
                }
                else
                {
                    Console.WriteLine("Lv. {0}. {1} HP {2} ", monster.Level, monster.MonsterName, monster.Hp);
                }
            
            return monster;
        }

        public Monster IsDeadview2(Monster monster, int i)
        {

            if (monster.Hp <= 0)
            {
                monster.isDead = true;
                Console.WriteLine("{0} Lv. {1}. {2} Dead", i, monster.Level, monster.MonsterName);
            }
            else
            {
                Console.WriteLine("{0} Lv. {1}. {2} HP {3} ", i, monster.Level, monster.MonsterName, monster.Hp);
            }

            return monster;
        }

        public Monster IsStun(Monster monster)
        {
            monster.IsAtk = false;
            return monster;
        }

        public static List<Monster> GenerateRandomMonsters(int count, int floorLevel)
        {
            List<Monster> monsters = new List<Monster>();
            string[] names = { "Goblin", "Orc", "Slime", "Skeleton", "Wolf", "Zombie", "Troll" };

            for (int i = 0; i < count; i++)
            {
                string name = names[rand.Next(names.Length)];
                int level = floorLevel + rand.Next(3); // 층에 따라 레벨 반영
                int hp = 50 + level * 10 + rand.Next(20); // 레벨 기반 체력 설정
                int def = 10 + level * 10 + rand.Next(20);
                int atk = 5 + level * 2 + rand.Next(5); // 공격력
                int critical = rand.Next(5, 21); // 크리티컬 확률 (5~20%)
                int dexterity = rand.Next(5, 21); // 민첩성 (5~20%)

                monsters.Add(new Monster(level, name, hp, atk, def, floorLevel, critical, dexterity, false, true));
            }

            return monsters;
        }

        public void TakeStatus(int minus, BattlStatusPage monsterstatus)
        {
            int newStatus = 0;

            switch (monsterstatus)
            {
                case BattlStatusPage.MinusDex:
                    if (Dexterity <= 0) newStatus = 0;
                    else newStatus = Dexterity - minus;
                    break;
                case BattlStatusPage.MinusDef:
                    if (Def <= 0) newStatus = 0;
                    else newStatus = Def - minus;
                    break;
                case BattlStatusPage.MinusAtk:
                    if (Dexterity <= 0) newStatus = 0;
                    else newStatus = Dexterity - minus;
                    break;
                    
            }
        }

    }
}
