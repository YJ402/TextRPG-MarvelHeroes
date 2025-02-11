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
    internal class BattleManager
    {

        static BattleManager Battleinstance;

        private BattleManager() { }

        // 배틀메니저 싱글톤 패턴 구현
        public static BattleManager Getinstance()
        {
            if (Battleinstance == null)
            {
                Battleinstance = new BattleManager();
            }

            return Battleinstance;
        }

        Random random = new Random();
        Player player = new Player(1,"ygm", 2000, 50, 50, false, JobType.IronMan);

        public void BattleStart(int currentFloor)
        {
            bool isClear = false;

            List<Monster> floormonsters = RandomMonster(Monster.GenerateRandomMonsters(5, currentFloor));
            //List<Monster> bossMonster = new List<Monster>
            //{
            //    new Monster(99,"타노스", 1000, 999, 999, 10, 99, 99, false)
            //};
            
            int notBattleHp = player.Hp;

            while (!isClear)
            {
                int selectNumber = StartPlayBattlePage(player, floormonsters);

                switch (selectNumber)
                {
                    case 1:
                        isClear = AttackPlayerPage(player, floormonsters, currentFloor, notBattleHp);
                        break;
                    case 2:
                        isClear = SkillPlayerPage(player, floormonsters, currentFloor, notBattleHp);
                        break;
                    case 3:
                        PotionPlayerPage(player, floormonsters, currentFloor);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }


            }

        }
        // 도전할 층 선택 
        //public int SelectBattlePage()
        //{
        //    int nextFloorNumber = Floor.nextFloorNumber;

        //    foreach (var floor in floors)
        //    {
        //        if (floor.nextFloor)
        //        {
        //            nextFloorNumber = floor.numberFloor;
        //        }
        //    }

        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("던전\n");
        //        Console.WriteLine("현재 도전할 층은 {0} 층입니다.", nextFloorNumber);
        //        Console.WriteLine("        #.UI\n");

        //        foreach (var floor in floors)
        //        {
        //            if (nextFloorNumber == floor.numberFloor)
        //            {
        //                floor.StatusFloor = "도전";
        //            }

        //            if (floor.numberFloor == 4)
        //            {
        //                Console.WriteLine("{0}.{1}층 {2}", floor.numberFloor, floor.numberFloor, floor.StatusFloor);

        //            }
        //            else Console.WriteLine("{0}.{1}층 {2}", floor.numberFloor, floor.numberFloor, floor.StatusFloor);
        //        }

        //        int input = GetInput(1, 4);

        //        if (input == 0) return 0;
        //        else if (input >= 1 && input <= 4)
        //        {
        //            if (input == 1) return 1;
        //            else if (input == 2)
        //            {
        //                if (floors[input - 1].StatusFloor == " ") return 2;
        //                else Console.WriteLine("입장 할 수 없습니다.");
        //                Console.ReadKey();
        //            }
        //            else if (input == 3)
        //            {
        //                if (floors[input - 1].StatusFloor == " ") return 3;
        //                else Console.WriteLine("입장 할 수 없습니다.");
        //                Console.ReadKey();
        //            }
        //            else if (input == 4)
        //            {
        //                if (floors[input - 1].StatusFloor == " ") return 4;
        //                else Console.WriteLine("입장 할 수 없습니다.");
        //                Console.ReadKey();
        //            }
        //        }
        //        else Console.WriteLine("잘못된 입력입니다.");

        //    }

        //}

        // 도전 할지 다시 묻는 선택 페이지
        //public bool FloorBattleExit(int floorinput, List<Monster> floorMonsters, Player player)
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("던전\n");
        //        Console.WriteLine("{0}층에 진입하려 합니다.", floorinput);
        //        Console.WriteLine("도전에 진입하면 UI에 접근하실 수 없습니다\n");
        //        Console.WriteLine("도전하시겠습니까?");
        //        Console.WriteLine("        #.UI\n");
        //        //Console.WriteLine("1. 예");
        //        //Console.WriteLine("2. 아니오\n");
        //        Console.WriteLine("원하시는 행동을 입력해주세요.");

        //        bool choice = GameView.SceneSelectYN();

        //        if (choice == true) return true;
        //        else if (player.Hp <= 0) Console.WriteLine("Hp가 0이라 진입 할 수 없습니다.");
        //        else return false;
        //    }

        //}

        // 공격, 스킬, 포션 사용 선택 창
        public int StartPlayBattlePage(Player player, List<Monster> floormonsters)
        {

            while (true)

            {
                Console.Clear();
                Console.WriteLine("Battle!! -  Player Turn\n");

                // 그룹화된 몬스터 출력 

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

        // 랜덤으로 층별 몬스터 반환 메서드
        public List<Monster> RandomMonster(List<Monster> monsters)
        {
            int takeNumber = random.Next(1, 5);
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

        // 공격할 몬스터 선택하는 페이지
        public bool AttackPlayerPage(Player player, List<Monster> floormonsters, int floorinput, int notBattleHp)
        {
            int beforPlayerHp = player.Hp;
            while (true)
            {
                Console.Clear();

                FloorSelectMontersView(floormonsters, BattlStatusPage.BattleAttack);

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp, player.MaxMp);

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.");

                int selectMonster = GetInput(0, floormonsters.Count);

                if (selectMonster == 0) return false;
                else if (selectMonster >= 1 && selectMonster <= floormonsters.Count)
                {
                    if (!floormonsters[selectMonster - 1].isDead)
                    {
                       floormonsters[selectMonster - 1] = PlayerAttack(player, floormonsters[selectMonster - 1], selectMonster);
                       player = MonasterAttack(player, floormonsters);  
                       return BattleResult(player, floormonsters, notBattleHp, floorinput);

                    }
                    else Console.WriteLine("잘못된 입력입니다.");
                }
                else Console.WriteLine("잘못된 입력입니다.");
            }

        }

        // 플레이어가 공격하는 메서드
        public Monster PlayerAttack(Player player, Monster floormonster, int selectMonster)
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
                        floormonster.TakeDamge(finalDamage);
                        //floormonster.Hp -= finalDamage;


                        Console.WriteLine("Battle\n");
                        Console.WriteLine("{0} 의 공격", player.Name);
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n", floormonster.Level, floormonster.MonsterName, floormonster.Atk);
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

        // 몬스터가 공격하는 메서드  -> 
        public Player MonasterAttack(Player player, List<Monster> floormonsters)
        {
            // 전투 중 플레이어 데미지 받기 전 hp
            int playerBeforHp = player.Hp;

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
                        player.Hp -= hitdamage;

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {0} {1}의 공격!", floormonsters[i].Level, floormonsters[i].MonsterName);
                        Console.WriteLine("{0} 을(를) 맞췄습니다.) [데미지 : {1}] - 치명타 데미지\n", player.Name, hitdamage);
                        Console.WriteLine("Lv. {0} {1}", player.Level, player.Name);

                        player.IsDead(player, playerBeforHp);

                    }
                    // 치명타 안 터지면 출력
                    else
                    {
                        player.Hp -= finalDamage;

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

        // 플레이어 스킬 선택하는 메서드
        public bool SkillPlayerPage(Player player, List<Monster> floormonsters, int floorinput, int notBattleHp)
        {
            List<Skill> skills = player.JobSkills(player);
            int beforBattlehp = player.hp;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Skill\n");
                FloorSelectMontersView(floormonsters);

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp, player.MaxMp);

                Skill.SkillListView(skills);

                Console.WriteLine("0. 취소\n");

                int input = GetInput(0, 2);

                if (input == 0) return false;
                else if (input >= 1 && input <= skills.Count)
                {
                    switch (player.PlayerJob)
                    {
                        case "아이언맨":
                            if (input == 1) floormonsters = IronManSkillPage(player, floormonsters, skills, input);
                            else if (input == 2) player.IronManAddDex(skills[input - 1].Adddex, 0);
                            player = MonasterAttack(player, floormonsters);
                            player.IronManAddDex(skills[input - 1].Adddex, 1);
                            break;
                        case "스파이더맨":
                            if (input == 1) player.NanoSuit(skills[input - 1].skillAtk, skills[input - 1].Adddef, 0);
                            else if (input == 2) floormonsters = SpiderManSkillPage(player, floormonsters, skills, input);
                            player = MonasterAttack(player, floormonsters);
                            player.NanoSuit(skills[input - 1].skillAtk, skills[input - 1].Adddef, 1);
                            break;
                        case "닥터스트레인지":
                            if (input == 1) floormonsters = DoctorStrangeSkillPage(player, floormonsters, skills, input);
                            else if (input == 2)
                            {
                                floormonsters = DoctorStrangeSkillPage(player, floormonsters, skills, input);
                                break;
                            }
                            player = MonasterAttack(player, floormonsters);
                            break;
                        case "헐크":
                            if (input == 1) floormonsters = HulkSkillPage(player, floormonsters, skills, input);
                            else if (input == 2) floormonsters = HulkSkillPage(player, floormonsters, skills, input);
                            player = MonasterAttack(player, floormonsters);
                            break;
                    }
                
                    return BattleResult(player, floormonsters, notBattleHp, floorinput);

                }
                else Console.WriteLine("잘못된 입력입니다.");

                
            }

 
        }

        // 아이언맨 스킬
        public List<Monster> IronManSkillPage(Player player, List<Monster> floormonster, List<Skill> skill, int input)
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
        public List<Monster> SpiderManSkillPage(Player player, List<Monster> floormonster, List<Skill> skill, int input)
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

                floormonster[i].IsDeadBattle (floormonster[i], monsterBefor_hp);
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
        public List<Monster> DoctorStrangeSkillPage(Player player, List<Monster> floormonster, List<Skill> skill, int input)
        {
            int monsterBefor_hp;
            player.Mp -= skill[input - 1].UseMp;


            Console.WriteLine("Battle\n");
            Console.WriteLine("{0} 의 공격\n", player.Name);

            for (int i = 0; i < floormonster.Count; i++)
            {
                int skillDamage = 0;

                monsterBefor_hp = floormonster[i].Hp;

                if (input == 1)
                {
                    skillDamage = random.Next(skill[input - 1].skillAtk, skill[input - 1].skillAtk + 40);
                    floormonster[i].Hp -= skillDamage;
                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[i].Level, floormonster[i].MonsterName, skillDamage);
                    Console.WriteLine("Lv. {0} {1}", floormonster[i].Level, floormonster[i].MonsterName);

                }
                else if (input == 2)
                {
                    if (floormonster.Count >= 2)
                    {
                        // 랜덤하게 2마리 몬스터 선택 후 HP 감소 (원본 리스트 직접 수정)
                        List<Monster> selectedMonsters = floormonster.OrderBy(m => random.Next()).Take(2).ToList();
                        for (int j = 0; j <= selectedMonsters.Count; j++)
                        {
                            selectedMonsters[j].Hp -= skill[input - 1].skillAtk;
                            Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", selectedMonsters[i].Level, selectedMonsters[i].MonsterName, skill[input - 1].skillAtk);
                            Console.WriteLine("Lv. {0} {1}", selectedMonsters[i].Level, selectedMonsters[i].MonsterName);

                        }
                    }
                    else if (floormonster.Count == 1)
                    {
                        // 몬스터가 1마리만 있으면 그 몬스터에게만 데미지 적용
                        floormonster[0].Hp -= skill[input - 1].skillAtk;
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 스킬 공격!!\n", floormonster[0].Level, floormonster[0].MonsterName, skill[input - 1].skillAtk);
                        Console.WriteLine("Lv. {0} {1}", floormonster[0].Level, floormonster[0].MonsterName);
                    }
                }

                floormonster[i].IsDead(floormonster[i], monsterBefor_hp);
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
        public List<Monster> HulkSkillPage(Player player, List<Monster> floormonster, List<Skill> skill, int input)
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

        // 플레이어 포션 사용하는 메서드
        public void PotionPlayerPage(Player player, List<Monster> floormonsters, int floorinput)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!\n");
                FloorSelectMontersView(floormonsters);
                Console.WriteLine();
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("Lv. {0} {1} {2}", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Hp, player.MaxMp);
                Console.WriteLine("1. HP 포션(수량:{0})");
                Console.WriteLine("2. MP 포션(수량:{0})");

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");


                int selectPotion = GetInput(0, 2);

                switch (selectPotion)
                {
                    case 0:
                        return;
                    case 1:
                        if (player.Hp == 100) Console.WriteLine("HP가 MAX입니다.");
                        else UsePotion(player,selectPotion);
                        return;
                    case 2:
                        if (player.Mp == 100) Console.WriteLine("MP가 MAX입니다.");
                        else UsePotion(player, selectPotion);
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }

            }


        }

        public void UsePotion(Player player, int selectPotion)
        {
            int beforeHp = player.Hp;
            int beforeMp = player.Mp;

            if (selectPotion == 1)
            {
                player.Hp += 30;

                Console.WriteLine("Battle - HP 포션 사용");
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv 이름 직업");
                Console.WriteLine($"{beforeHp} -> {player.Hp}");

            }
            else if (selectPotion == 2)
            {
                player.Mp += 30;

                Console.WriteLine("Battle - HP 포션 사용");
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv 이름 직업");
                Console.WriteLine($"{beforeMp} -> {player.Mp}");
            }
            Console.WriteLine("아무키나 입력하세요.");
            Console.ReadKey();

        }

        // 클리어 메서드
        public void ClearBattlePage(Player player, int beforeBattleHp, int clearfloor, List<Monster> floorMonsters)
        {

            while (true)
            {
                //DungeionScene.challengeFloor ++;
                Console.Clear();
                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.\n", floorMonsters.Count);
                Console.WriteLine("Lv. {0} {1} {2}", player.Level, player.Name, player.PlayerJob);
                Console.WriteLine("HP {0} -> {1}\n", beforeBattleHp, player.Hp);
                Console.WriteLine("0. 다음");

                int input = GetInput(0, 0);

                if (input == 0) return;
                else Console.WriteLine("잘못된 입력입니다.");

            }
        }
        // 패배 매서드
        public void DefeatBattlePage(Player player, int beforeBattleHp)
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

        //배틀 결과 확인
        public bool BattleResult(Player player, List<Monster> floormonsters, int beforBattlehp, int floorinput)
        {
            if (floormonsters.All(m => m.Hp <= 0))
            {
                ClearBattlePage(player, beforBattlehp, floorinput, floormonsters);
                return true;
            }
            else if (player.Hp <= 0)
            {
                DefeatBattlePage(player, beforBattlehp);
                return true;
            }
            else return false;

            
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


    // 층에 대한 내용 층수, 층의 상태, 클리어 여부, 다음층인지 확인
    //public class Floor
    //{
    //    public int numberFloor { get; set; }
    //    public string floorName { get; set; }
    //    public string StatusFloor { get; set; }
    //    public bool clearFloor { get; set; }
    //    public bool nextFloor { get; set; }

    //    public static int nextFloorNumber = 1;

    //    public Floor(int _NumberFloor, string _StatusFloor, bool _clearFloor, bool _nextFloor)
    //    {
    //        numberFloor = _NumberFloor;
    //        StatusFloor = _StatusFloor;
    //        clearFloor = _clearFloor;
    //        nextFloor = _nextFloor;
    //    }


    //}

    //class Monster : Unit
    //{
    //    public string Name { get; private set; }
    //    int floor { get; set; }
    //    public bool isAtk { get; private set; }

    //    private static Random rand = new Random();

    //public Monster(int _Level, string _Name, int _Hp, int _Atk,int _Def ,int _floor, int _Critical, int _Dex, bool _isDaed, bool _isAtk)
    //    : base(_Level, _Atk, _Def, _Hp, _Critical, _Dex, false)
    //{
    //    Level = _Level;
    //    Name = _Name;
    //    Hp = _Hp;
    //    Atk = _Atk;
    //    Hp = _Hp;
    //    Atk = _Atk;
    //    floor = _floor;
    //    Critical = _Critical;
    //    Dexterity = _Dex;
    //    isAtk = true;          
    //}

    // 몬스터 데미지 받는 메서드
    //public override TestMonster TakeDamge(int damge)
    //{
    //    int newHp;
    //    if (Hp <= 0)
    //    {
    //        newHp = 0;
    //        isDead = true;
    //    }
    //    else newHp = Hp - damge;

    //    return new TestMonster(Level,Name, newHp, Atk, Def, floor, Critical, Dexterity, isDead, isAtk);
    //}

    // 몬스터 스테이지 감소 메서드
    //public Monster TakeStatus(int minus, BattlStatusPage monsterstatus)
    //{
    //    int newStatus = 0;

    //    switch(monsterstatus)
    //    {
    //        case BattlStatusPage.MinusDex:
    //            if (Dexterity <= 0) newStatus = 0;
    //            else newStatus = Dexterity - minus;
    //            return new Monster(Level, Name, Hp, Atk, Def, floor, Critical, newStatus, isDead, isAtk);
    //        case BattlStatusPage.MinusDef:
    //            if (Def <= 0) newStatus = 0;
    //            else newStatus = Def - minus;
    //            return new Monster(Level, Name, Hp, Atk, newStatus, floor, Critical, Dexterity, isDead, isAtk);
    //        case BattlStatusPage.MinusAtk:
    //            if (Dexterity <= 0) newStatus = 0;
    //            else newStatus = Dexterity - minus;
    //            return new Monster(Level, Name, Hp, newStatus, Def, floor, Critical, Dexterity, isDead, isAtk);
    //        default:
    //            return new Monster(Level, Name, Hp, newStatus, Def, floor, Critical, Dexterity, isDead, isAtk);
    //    }
    //}

    // 몬스터 생존 확인 메서드
    //public override TestMonster IsDead(Unit monster, int beforeHp)
    //{
    //    if (monster.Hp <= 0)
    //    {
    //        int newHp = 0;
    //        Console.WriteLine("HP {0} -> Dead\n", beforeHp);
    //        return new TestMonster(Level, Name, newHp, Atk, Def, floor, Critical, Dexterity, isDead, isAtk);
    //    }
    //    else
    //    {
    //        Console.WriteLine("HP {0} -> {1}\n", beforeHp, monster.Hp);
    //        return new TestMonster(Level, Name, Hp, Atk, Def, floor, Critical, Dexterity, isDead, isAtk);
    //    }
    //}

    //public TestMonster IsStun(TestMonster monster)
    //{
    //    return new TestMonster(Level, Name, Hp, Atk, Def, floor, Critical, Dexterity, isDead, false);
    //}

    //몬스터 랜덤 생성 메서드
    //public static List<TestMonster> GenerateRandomMonsters(int count, int floorLevel)
    //{
    //    List<TestMonster> monsters = new List<TestMonster>();
    //    string[] names = { "Goblin", "Orc", "Slime", "Skeleton", "Wolf", "Zombie", "Troll" };

    //    for (int i = 0; i < count; i++)
    //    {
    //        string name = names[rand.Next(names.Length)];
    //        int level = floorLevel + rand.Next(3); // 층에 따라 레벨 반영
    //        int hp = 50 + level * 10 + rand.Next(20); // 레벨 기반 체력 설정
    //        int def = 10 + level * 10 + rand.Next(20);
    //        int atk = 5 + level * 2 + rand.Next(5); // 공격력
    //        int critical = rand.Next(5, 21); // 크리티컬 확률 (5~20%)
    //        int dexterity = rand.Next(5, 21); // 민첩성 (5~20%)

    //        monsters.Add(new TestMonster(level, name, hp, atk, def, floorLevel, critical, dexterity, false, false));
    //    }

    //    return monsters;
    //}



    //}

}


