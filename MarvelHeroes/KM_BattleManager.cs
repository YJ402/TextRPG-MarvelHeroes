using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarvelHeroes
{
    public class BattleManager
    {

        Random random = new Random();

        Player player { get; set; }

        public BattleManager()
        {
            player = GameManager.Instance.player;
        }

        // 배틀 시작 씬
        public void BattleStart(int currentFloor)
        {
            bool isClear = false;

            List<Monster> floormonsters = RandomMonster(Monster.GenerateRandomMonsters(5, currentFloor));
            int notBattleHp = player.Hp;

            while (!isClear)
            {
                int selectNumber = StartPlayBattlePage(floormonsters);

                switch (selectNumber)
                {
                    case 1:
                        isClear = AttackPlayerPage(floormonsters, currentFloor, notBattleHp, selectNumber);
                        break;
                    case 2:
                        isClear = SkillPlayerPage(floormonsters, currentFloor, notBattleHp, selectNumber);
                        break;
                    case 3:
                        PotionPlayerPage(floormonsters, currentFloor, selectNumber);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }


            }

        }  

        // 플레이어 공격 선택 씬
        public bool AttackPlayerPage(List<Monster> floormonsters, int floorinput, int notBattleHp, int selectNumber)
        {
            int beforPlayerHp = player.Hp;
            while (true)
            {
                ViewPlayerAndMonster(floormonsters, selectNumber, BattlStatusPage.BattleAttack);

                int selectMonster = GetInput(0, floormonsters.Count);

                if (selectMonster == 0) return false;
                else if (selectMonster >= 1 && selectMonster <= floormonsters.Count)
                    return NormalAttackBattleScene(floormonsters, selectMonster, floorinput, notBattleHp);
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 일반공격한 플레이어와 몬스터 배틀!
        public bool NormalAttackBattleScene(List<Monster> floormonsters, int selectMonster, int floorinput, int notBattleHp)
        {
            if (!floormonsters[selectMonster - 1].isDead)
            {
                PlayerAttack(floormonsters[selectMonster - 1], selectMonster);
                MonasterAttack(floormonsters, floorinput);
                return BattleResult(floormonsters, notBattleHp, floorinput);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return false;
            }
        }

        // 플레이어 스킬 선택 씬
        public bool SkillPlayerPage(List<Monster> floormonsters, int floorinput, int notBattleHp, int selectNumber)
        {
            List<Skill> skills = player.JobSkills(player);
            int beforBattlehp = player.hp;

            while (true)
            {
                ViewPlayerAndMonster(floormonsters, selectNumber);

                Skill.SkillListView(skills);

                Console.WriteLine("0. 취소\n");

                int input = GetInput(0, 2);

                if (input == 0) return false;
                else if (input >= 1 && input <= skills.Count)
                    return SkillBattlePlayerAndMonster(floormonsters, skills, input, floorinput, notBattleHp);
                else Console.WriteLine("잘못된 입력입니다.");


            }


        }

        // 플레이어 스킬로 싸우는 배틀 씬
        public bool SkillBattlePlayerAndMonster(List<Monster> floormonsters, List<Skill> skills, int input, int floorinput, int notBattleHp)
        {
            switch (player.PlayerJob)
            {
                case JobType.IronMan:
                    if (input == 1) floormonsters = IronManSkillPage(floormonsters, skills, input);
                    else if (input == 2) player.IronManAddDex(skills[input - 1].Adddex, 0);
                    player = MonasterAttack(floormonsters, floorinput);
                    player.IronManAddDex(skills[input - 1].Adddex, 1);
                    break;
                case JobType.SpiderMan:
                    if (input == 1) player.NanoSuit(skills[input - 1].skillAtk, skills[input - 1].Adddef, 0);
                    else if (input == 2) floormonsters = SpiderManSkillPage(floormonsters, skills, input);
                    player = MonasterAttack(floormonsters, floorinput);
                    player.NanoSuit(skills[input - 1].skillAtk, skills[input - 1].Adddef, 1);
                    break;
                case JobType.DoctorStrange:
                    if (input == 1) floormonsters = DoctorStrangeSkillPage(floormonsters, skills, input);
                    else if (input == 2)
                    {
                        floormonsters = DoctorStrangeSkillPage(floormonsters, skills, input);
                        break;
                    }
                    player = MonasterAttack(floormonsters, floorinput);
                    break;
                case JobType.Hulk:
                    if (input == 1) floormonsters = HulkSkillPage(floormonsters, skills, input);
                    else if (input == 2) floormonsters = HulkSkillPage(floormonsters, skills, input);
                    player = MonasterAttack(floormonsters, floorinput);
                    break;
            }

            return BattleResult(floormonsters, notBattleHp, floorinput);



        }

        // 플레이어 포션 선택 씬
        public void PotionPlayerPage(List<Monster> floormonsters, int floorinput, int selectNumber)
        {
            while (true)
            {
                ViewPlayerAndMonster(floormonsters, selectNumber);
               
                List<UsingItem> usingItems = new List<UsingItem>();

                foreach (var potion in GameManager.Instance.inventory.items)
                {
                    if(potion is UsingItem)
                     usingItems.Add((UsingItem)potion);
                }
              
                for(int i = 1; i< usingItems.Count; i++)
                {
                    Console.WriteLine("{0}. {1} 수량: {2}", i, usingItems[i].Name, usingItems[i].Quantity);
                }

                int selectPotion = GetInput(0, 2);

                if (selectPotion == 0) return;
                else if (selectPotion == 1 || selectPotion == 2) usingItems[selectPotion - 1].Use(player);
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }

        //배틀 결과 확인
        public bool BattleResult(List<Monster> floormonsters, int beforBattlehp, int floorinput)
        {
            if (floormonsters.All(m => m.Hp <= 0))
            {
                ClearBattlePage(beforBattlehp, floorinput, floormonsters);
                return true;
            }
            else if (player.Hp <= 0)
            {
                DefeatBattlePage(beforBattlehp);
                return true;
            }
            else return false;


        }





        // 실제 전투와 관련된 메서드

        // 플레이어가 공격하는 씬
        public Monster PlayerAttack(Monster floormonster, int selectMonster)
        {
            int attackPencent = random.Next(0, 100); // 맞을 확률
            int hitNumber = random.Next(0, 100); // 치명타 확률
            int attackError = (int)Math.Round(player.Atk * 0.1);
            int finalDamage = random.Next(player.Atk - attackError, player.Atk + attackError);
            // 현재 몬스터 hp 이전
            int monsterBefor_hp = floormonster.Hp;

            while (true)
            {
                Console.Clear();
                // 몬스터가 데미지를 받는지 확인
                if (attackPencent > floormonster.Dexterity)
                {
                    // 몬스터에게 치명타가 터지는지 확인
                    if (hitNumber < player.Critical)
                    {
                        int hitDamage = (int)Math.Round(finalDamage * 1.6); // 1.6으로 수정 할 것
                        hitDamage -= (int)(floormonster.Def * 0.3);
                        if (hitDamage <= 0) hitDamage = 10;
                        floormonster.TakeDamge(hitDamage);
                        //floormonster.Hp -= hitDamage;

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("{0} 의 공격", player.Name);
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 치명타 공격!!\n", floormonster.Level, floormonster.MonsterName, hitDamage);
                        Console.WriteLine("Lv. {0} {1}", floormonster.Level, floormonster.MonsterName);

                        floormonster.IsDeadBattle(floormonster, monsterBefor_hp);

                    }
                    // 치명타 안 터지면 출력
                    else
                    {
                        finalDamage -= (int)(floormonster.Def * 0.3);
                        if (finalDamage <= 0) finalDamage = 5;
                        floormonster.TakeDamge(finalDamage);
                        //floormonster.Hp -= finalDamage;


                        Console.WriteLine("Battle\n");
                        Console.WriteLine("{0} 의 공격", player.Name);
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n", floormonster.Level, floormonster.MonsterName, finalDamage);
                        Console.WriteLine("Lv. {0} {1}", floormonster.Level, floormonster.MonsterName);

                        floormonster.IsDeadBattle(floormonster, monsterBefor_hp);

                    }
                }
                // 공격 실패 시 출력
                else
                {
                    Console.WriteLine("Battle\n");
                    Console.WriteLine("Lv. {0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n", floormonster.Level, floormonster.MonsterName);

                }

                Console.WriteLine("0. 다음\n");

                int input = GetInput(0, 0);

                if (input == 0) return floormonster;
                else Console.WriteLine("잘못된 입력입니다.");
            }

        }

        // 몬스터가 공격하는 씬
        public Player MonasterAttack(List<Monster> floormonsters, int floorinput)
        {
            // 전투 중 플레이어 데미지 받기 전 hp
            int playerBeforHp = player.Hp;

            if (floorinput < 10)
            {
                player = NormalMonasterAttack(floormonsters, playerBeforHp);

            }
            else
            {
                player = BossMonsterAttack(floormonsters, playerBeforHp);
            }


            return player;
        }

        //보스 몬스터 공격하는 씬
        public Player BossMonsterAttack(List<Monster> floormonsters, int playerBeforHp)
        {
            int skillrand = random.Next(0, 100);

            // 일반공격
            if (skillrand < 80)
            {
                for (int i = 0; i < floormonsters.Count; i++)
                {
                    if (floormonsters[i].isDead)
                    {
                        continue;
                    }

                    Console.Clear();
                    int attackPecent = random.Next(0, 100);
                    int hitNumber = random.Next(0, 100);
                    int attackError = (int)Math.Round(floormonsters[i].Atk * 0.1);
                    int finalDamage = random.Next(floormonsters[i].Atk - attackError, floormonsters[i].Atk + attackError);

                    // 몬스터의 공격이 성공 했는지 확인
                    if (attackPecent > player.Dexterity && floormonsters[i].IsAtk)
                    {
                        // 몬스터에게 치명타가 터지는지 확인
                        if (hitNumber < floormonsters[i].Critical)
                        {
                            int hitdamage = (int)Math.Round(finalDamage * 1.2);
                            hitdamage -= (int)(player.Def * 0.5);
                            if (hitdamage <= 0) hitdamage = 10;
                            player.TakeDamge(hitdamage);

                            Console.WriteLine("Battle\n");
                            Console.WriteLine("Lv. {0} {1}의 공격!", floormonsters[i].Level, floormonsters[i].MonsterName);
                            Console.WriteLine("{0} 을(를) 맞췄습니다.) [데미지 : {1}] - 치명타 데미지\n", player.Name, hitdamage);
                            Console.WriteLine("Lv. {0} {1}", player.Level, player.Name);

                            player.IsDead(player, playerBeforHp);

                        }
                        // 치명타 안 터지면 출력
                        else
                        {
                            finalDamage -= (int)(player.Def * 0.5);
                            if (finalDamage <= 0) finalDamage = 5;
                            player.TakeDamge(finalDamage);

                            Console.WriteLine("Battle\n");
                            Console.WriteLine("Lv. {0} {1}의 공격!", floormonsters[i].Level, floormonsters[i].MonsterName);
                            Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]\n", player.Name, finalDamage);
                            Console.WriteLine("Lv. {0} {1}", player.Level, player.Name);

                            player.IsDead(player, playerBeforHp);
                        }
                    }
                    // 공격 실패 시 출력
                    else
                    {
                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {0} {1} 가 공격했지만 아무일도 일어나지 않았습니다.\n", floormonsters[i].Level, floormonsters[i].MonsterName);

                    }

                    Console.WriteLine("아무키나 누르세요.");
                    Console.ReadKey();
                }
            }
            // 스킬 공격
            else
            {
                for (int i = 0; i < floormonsters.Count; i++)
                {
                    if (floormonsters[i].isDead)
                    {
                        continue;
                    }

                    Console.Clear();
                    switch (floormonsters[i].MonsterName)
                    {
                        case "타노스":
                            player = TanosSkillPage(floormonsters[i]);
                            player.IsDead(player, playerBeforHp);
                            break;
                        case "울트론":
                            player = UltronSkillPage(floormonsters[i]);
                            player.IsDead(player, playerBeforHp);
                            break;
                    }
                }
            }

            return player;

        }

        // 일반 몬스터 공격하는 씬
        public Player NormalMonasterAttack(List<Monster> floormonsters, int playerBeforHp)
        {
            for (int i = 0; i < floormonsters.Count; i++)
            {
                if (floormonsters[i].isDead)
                {
                    continue;
                }

                Console.Clear();
                int attackPecent = random.Next(0, 100);
                int hitNumber = random.Next(0, 100);
                int attackError = (int)Math.Round(floormonsters[i].Atk * 0.1);
                int finalDamage = random.Next(floormonsters[i].Atk - attackError, floormonsters[i].Atk + attackError);

                // 몬스터의 공격이 성공 했는지 확인
                if (attackPecent > player.Dexterity && floormonsters[i].IsAtk)
                {
                    // 몬스터에게 치명타가 터지는지 확인
                    if (hitNumber < floormonsters[i].Critical)
                    {
                        int hitdamage = (int)Math.Round(finalDamage * 1.2);
                        hitdamage -= (int)(player.Def * 0.5);
                        if (hitdamage <= 0) hitdamage = 10;
                        player.TakeDamge(hitdamage);

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {0} {1}의 공격!", floormonsters[i].Level, floormonsters[i].MonsterName);
                        Console.WriteLine("{0} 을(를) 맞췄습니다.) [데미지 : {1}] - 치명타 데미지\n", player.Name, hitdamage);
                        Console.WriteLine("Lv. {0} {1}", player.Level, player.Name);

                        player.IsDead(player, playerBeforHp);

                    }
                    // 치명타 안 터지면 출력
                    else
                    {
                        finalDamage -= (int)(player.Def * 0.5);
                        if (finalDamage <= 0) finalDamage = 5;
                        player.TakeDamge(finalDamage);

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {0} {1}의 공격!", floormonsters[i].Level, floormonsters[i].MonsterName);
                        Console.WriteLine("{0} 을(를) 맞췄습니다. [데미지 : {1}]\n", player.Name, finalDamage);
                        Console.WriteLine("Lv. {0} {1}", player.Level, player.Name);

                        player.IsDead(player, playerBeforHp);
                    }
                }
                // 공격 실패 시 출력
                else
                {
                    Console.WriteLine("Battle\n");
                    Console.WriteLine("Lv. {0} {1} 가 공격했지만 아무일도 일어나지 않았습니다.\n", floormonsters[i].Level, floormonsters[i].MonsterName);

                }

                Console.WriteLine("아무키나 누르세요.");
                Console.ReadKey();
            }

            return player;

        }
        // 스킬 메서드

        // 아이언맨 스킬
        public List<Monster> IronManSkillPage(List<Monster> floormonster, List<Skill> skill, int input)
        {
            int monsterBefor_hp;
            player.Mp -= skill[input - 1].UseMp;

            Console.Clear();
            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", player.Name);

            for (int i = 0; i < floormonster.Count; i++)
            {
                monsterBefor_hp = floormonster[i].Hp;
                floormonster[i].Hp -= skill[input - 1].skillAtk;


                Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[i].Level, floormonster[i].MonsterName, skill[input - 1].skillAtk);
                Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);

                floormonster[i].IsDeadBattle(floormonster[i], monsterBefor_hp);
            }

            Console.WriteLine("0. 다음\n");
            int select = GetInput(0, 0);

            if (select != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            return floormonster;
        }

        // 스파이더맨 스킬
        public List<Monster> SpiderManSkillPage(List<Monster> floormonster, List<Skill> skill, int input)
        {
            int monsterBefor_hp;
            player.Mp -= skill[input - 1].UseMp;

            Console.Clear();
            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", player.Name);

            for (int i = 0; i < floormonster.Count; i++)
            {
                monsterBefor_hp = floormonster[i].Hp;
                floormonster[i].Hp -= skill[input - 1].skillAtk;
                floormonster[i].TakeStatus(skill[input - 1].skillAtk, BattlStatusPage.MinusDex);

                Console.WriteLine("Battle\n");
                Console.WriteLine("{0} 의 공격", player.Name);
                Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[i].Level, floormonster[i].MonsterName, skill[input - 1].skillAtk);
                Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);

                floormonster[i].IsDeadBattle(floormonster[i], monsterBefor_hp);
            }

            Console.WriteLine("0. 다음\n");
            int select = GetInput(0, 0);

            if (select != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            return floormonster;
        }

        // 닥터스트레인지 스킬
        public List<Monster> DoctorStrangeSkillPage(List<Monster> floormonster, List<Skill> skill, int input)
        {
            int monsterBefor_hp;
            player.Mp -= skill[input - 1].UseMp;
            int skillDamage = 0;

            Console.Clear();
            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", player.Name);

            if(input == 1)
            {
                for (int i = 0; i < floormonster.Count; i++)
                {
                    monsterBefor_hp = floormonster[i].Hp;
                    skillDamage = random.Next(skill[input - 1].skillAtk, skill[input - 1].skillAtk + 40);
                    floormonster[i].Hp -= skillDamage;
                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[i].Level, floormonster[i].MonsterName, skillDamage);
                    Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);

                    floormonster[i].IsDead(floormonster[i], monsterBefor_hp);
                }
            }
            else if(input == 2)
            {
                // 랜덤하게 2마리 몬스터 선택 후 HP 감소 (원본 리스트 직접 수정)

                List<Monster> selectedMonsters = floormonster.OrderBy(m => random.Next()).Take(2).ToList();

                selectedMonsters.RemoveAll(monster => monster.Hp <= 0);

                if (floormonster.Count >= 2)
                {
                    for (int j = 0; j < selectedMonsters.Count; j++)
                    {
                        monsterBefor_hp = floormonster[j].Hp;
                        selectedMonsters[j].Hp -= skill[input - 1].skillAtk;
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", selectedMonsters[j].Level, selectedMonsters[j].MonsterName, skill[input - 1].skillAtk);
                        Console.WriteLine("Lv. {0} {1}", selectedMonsters[j].Level, selectedMonsters[j].MonsterName);

                        selectedMonsters[j].IsDead(selectedMonsters[j], monsterBefor_hp);
                    }
                }
                else if (floormonster.Count == 1)
                {
                    // 몬스터가 1마리만 있으면 그 몬스터에게만 데미지 적용
                    monsterBefor_hp = floormonster[0].Hp;
                    floormonster[0].Hp -= skill[input - 1].skillAtk;
                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[0].Level, floormonster[0].MonsterName, skill[input - 1].skillAtk);
                    Console.WriteLine("Lv. {0} {1}", floormonster[0].Level, floormonster[0].MonsterName);

                    floormonster[0].IsDead(floormonster[0], monsterBefor_hp);
                }
                else if(floormonster.Count == 0)Console.WriteLine("죽은 몬스터를 공격했습니다.!!");
            }
            Console.WriteLine("0. 다음\n");
            int select = GetInput(0, 0);
            if (select != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            return floormonster;
        }

        // 헐크 스킬
        public List<Monster> HulkSkillPage(List<Monster> floormonster, List<Skill> skill, int input)
        {
            int monsterBefor_hp;
            int monsterBefor_Atk;
            player.Mp -= skill[input - 1].UseMp;


            if (input == 1)
            {
                Console.Clear();
                Console.WriteLine("Battle\n");
                Console.WriteLine("{0} 의 공격\n", player.Name);

                for (int i = 0; i < floormonster.Count; i++)
                {
                    monsterBefor_hp = floormonster[i].Hp;
                    floormonster[i].Hp -= skill[input - 1].skillAtk;

                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[i].Level, floormonster[i].MonsterName, skill[input - 1].skillAtk);
                    Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);
                    floormonster[i].IsDeadBattle(floormonster[i], monsterBefor_hp);
                }
            }
            else if (input == 2)
            {
                Console.Clear();
                Console.WriteLine("Battle\n");
                Console.WriteLine("{0} 의 공격\n", player.Name);

                for (int i = 0; i < floormonster.Count; i++)
                {
                    monsterBefor_Atk = floormonster[i].Atk;
                    floormonster[i].Atk -= skill[input - 1].skillAtk;

                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [공격력 감소 : {2}] - 공격력 감소!!\n", floormonster[i].Level, floormonster[i].MonsterName, skill[input - 1].skillAtk);
                    Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);
                    floormonster[i].HulkShouting(floormonster[i], monsterBefor_Atk);
                }
            }



            Console.WriteLine("0. 다음\n");
            int select = GetInput(0, 0);

            if (select != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            return floormonster;

        }

        //타노스 스킬
        public Player TanosSkillPage(Monster bossMonster)
        {

            int skillrand = random.Next(0, 100);
            int skillNumber;

            if (skillrand < 80) skillNumber = 1;
            else if (skillrand < 95) skillNumber = 2;
            else skillNumber = 3;

            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", bossMonster.MonsterName);

            switch (skillNumber)
            {
                case 1:
                    Console.WriteLine(" 타노스가 핑거 스냅을 사용했다.!!");
                    Console.WriteLine(" {0}의 체력이 반으로 줄어들었다.!!\n", player.Name);
                    player.Hp /= 2;
                break;

                case 2:
                    Console.WriteLine(" 타노스가 타임 스톤을 사용했다.!!");
                    Console.WriteLine(" 타노스의 체력이 회복되었다.!!\n");
                    Console.WriteLine(" {0} -> {1}", bossMonster.Hp, bossMonster.Hp = 1000);
                break;

                case 3:
                    Console.WriteLine(" 타노스가 파워 스톤을 사용했다.!!\n");
                    Console.WriteLine(" {0}의 피해를 입었다.", bossMonster.Atk * 10);
                    player.Hp -= bossMonster.Atk * 10;
                break;

            }
            Console.WriteLine("아무키나 누르세요.");
            Console.ReadKey();
            return player;
        }

        //울트론 스킬
        public Player UltronSkillPage(Monster bossMonster)
        {
            int skillrand = random.Next(0, 100);
            int skillNumber;

            if (skillrand < 50) skillNumber = 1;
            else skillNumber = 2;


            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", bossMonster.MonsterName);

            switch (skillNumber)
            {
                case 1:
                    Console.WriteLine(" 울트론이 파워 슬램을 사용했다.!!");
                    Console.WriteLine(" {0} 데미지를 입었다.!!\n", bossMonster.Atk * 5);
                    Console.WriteLine(" HP: {0} -> {1}", player.Hp, player.Hp -= bossMonster.Atk * 5);
                break;

                case 2:
                    Console.WriteLine(" 울트론이 EMP 광선을 사용했다.!!");
                    Console.WriteLine(" {0}의 MP가 0이 되었다!!\n");
                    Console.WriteLine(" {0} -> {1}", player.Mp, player.Mp = 0);
                break;
            }
            Console.WriteLine("아무키나 누르세요.");
            Console.ReadKey();

            return player;
        }




        // 기타 UI 메서드


        // 배틀 시작하는 씬
        public int StartPlayBattlePage(List<Monster> floormonsters)
        {
            while (true)

            {
                ViewPlayerAndMonster(floormonsters);

                int input = GetInput(0, 3);

                switch (input)
                {
                    case 0:
                        return 0;
                    case 1:
                        return 1;
                    case 2:
                        if (player.Mp <= 0)
                        {
                            Console.WriteLine("MP가 부족합니다.");
                            break;
                        }
                        else return 2;
                    case 3:
                        return 3;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
                Console.ReadKey();
            }

        }

        public void ViewPlayerAndMonster(List<Monster> floormonsters, int selectNumber = 0, BattlStatusPage battlStatusPage =BattlStatusPage.BattleBase)
        {
       
            if(battlStatusPage == BattlStatusPage.BattleAttack)
            {
                Console.Clear();
                if (selectNumber == 1) Console.WriteLine("Battle!! - Attack\n");
                else if (selectNumber == 2) Console.WriteLine("Battle!! - Skill\n");

                FloorSelectMontersView(floormonsters, battlStatusPage);


                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp, player.MaxMp);
                Console.WriteLine("0. 취소\n");
                if (selectNumber == 1) Console.WriteLine("대상을 선택해주세요.");
                else if (selectNumber == 2) Console.WriteLine("스킬을 선택해주세요.");
                else if (selectNumber == 3) Console.WriteLine("사용할 아이템을 선택해주세요.");
            }
            else if(battlStatusPage == BattlStatusPage.BattleBase)
            {
                Console.Clear();
                Console.WriteLine("Battle!! -  Player Turn\n");

                FloorSelectMontersView(floormonsters);

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp, player.MaxMp);
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 포션사용\n");
                Console.WriteLine("0. 도망가기\n");
            }

        }
        // 클리어 메서드
        public void ClearBattlePage(int beforeBattleHp, int clearfloor, List<Monster> floorMonsters)
        {
            int Adddgold = random.Next(10, 10*clearfloor);
            int AddExp = 0;
            Item getItem = GetRandomItem();
            GameManager.Instance.inventory.AddItem(getItem);

            foreach (var monster in floorMonsters)
            {
                AddExp += monster.Level * 1;
            }

            while (true)
            {
                //던전씬 정보 업뎃용
                int challengingFloor = GameManager.Instance.SM.dungeonScene.challengingFloor++;
                challengingFloor  = (challengingFloor == clearfloor) ? challengingFloor++ : challengingFloor;
                GameManager.Instance.SM.dungeonScene.UpdateDungeonSelections();

                Console.Clear();
                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", floorMonsters.Count);

                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine("Lv. {0} {1} {2}", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("HP {0} -> {1}", beforeBattleHp, player.Hp);
                Console.WriteLine("exp {0} => {1}\n", player.Xp, player.Xp += AddExp);

                Console.WriteLine("[획득 아이템]");
                Console.WriteLine("Gold {0} -> {1}", player.Gold, player.Gold +=Adddgold);
                Console.WriteLine("{0} = 1", getItem.Name);

                Console.WriteLine("0. 다음");

                int input = GetInput(0, 0);

                if (input == 0) return;
                else Console.WriteLine("잘못된 입력입니다.");

            }
        }
        
        // 패배 매서드
        public void DefeatBattlePage(int beforeBattleHp)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("You Lose\n");
                Console.WriteLine("Lv. {0} {1} {2}", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("HP {0} -> {1}\n", beforeBattleHp, player.Hp);
                Console.WriteLine("0. 다음");

                int input = GetInput(0, 0);

                if (input == 0) return;
                else Console.WriteLine("잘못된 입력입니다.");

            }


        }
        
        //버튼 메서드
        public int GetInput(int min, int max)
        {
            while (true) //return이 되기 전까지 반복
            {
                Console.Write("원하시는 행동을 입력해주세요.");

                //int.TryParse는 int로 변환이 가능한지 bool값을 반환, 가능(true)할 경우 out int input으로 숫자도 반환
                if (int.TryParse(Console.ReadLine(), out int input) && (input >= min) && (input <= max))
                    return input;

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            }

        }

        // 랜덤으로 층별 몬스터 반환 메서드
        public List<Monster> RandomMonster(List<Monster> monsters)
        {
            int takeNumber = random.Next(1, monsters.Count);
            // 몬스터 클래스에서 층에 해당하는 몬스터 그룹화

            // 몬스터 3마리 랜덤 선택
            List<Monster> randomFloorMonsters = monsters.OrderBy(m => random.Next()).Take(takeNumber).ToList();

            // 선택한 3마리 몬스터 반환
            return randomFloorMonsters;

        }

        //층으로 선택된 몬스터 3~4마리 출력하는 메서드
        public void FloorSelectMontersView(List<Monster> monsters, BattlStatusPage battlStatusPage = BattlStatusPage.BattleBase)
        {
            if (battlStatusPage == BattlStatusPage.BattleBase)
            {
                if (monsters.Count > 1)
                {
                    foreach (var monster in monsters)
                    {
                        monster.IsDeadview(monster);
                    }
                }
                else monsters[0].IsDeadview(monsters[0]);

            }
            else if (battlStatusPage == BattlStatusPage.BattleAttack)
            {
                if (monsters.Count > 1)
                {
                    for (int i = 1; i <= monsters.Count; i++)
                    {
                        monsters[i - 1] = monsters[i - 1].IsDeadview2(monsters[i - 1], i);
                    }
                }
                else monsters[0].IsDeadview2(monsters[0], 1);
            }

        }

        public Item GetRandomItem()
        {
            List<Item> getRandomitems = GameManager.Instance.IM.Alltems;

            int r = random.Next(0,getRandomitems.Count);      

            Item getItem = GameManager.Instance.IM.Alltems[r];

            return getItem;
        }

    }


    // 배틀스테이지 필요한 enum
    public enum BattlStatusPage
    {
        BattleBase,
        BattleAttack,
        MinusDex,
        MinusAtk,
        MinusDef
    }

}


