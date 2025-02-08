using System;
using System.Collections.Generic;
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
            if(Battleinstance == null)
            {
                Battleinstance = new BattleManager();
            }

            return Battleinstance;
        }

        Player player = new Player(1, "cha", "아이언맨", 10, 0, 5, 0, 1000, 100,100, 100, 1, 3);
        List<TestMonster> monsters1 = TestMonster.GenerateRandomMonsters(5, 1);
        List<TestMonster> monsters2 = TestMonster.GenerateRandomMonsters(5, 2);
        List<TestMonster> monsters3 = TestMonster.GenerateRandomMonsters(5, 3);
        List<Job> job = new List<Job>();
        Random random = new Random();
        Floor[] floors = new Floor[]
        {
            new Floor(1, "도전", false, true),
            new Floor(2, "잠김", false, false),
            new Floor(3, "잠김", false, false),
            new Floor(4, "잠김", false, false){ floorName = "보스방" }
        };

        // 도전할 층 선택
        public void SelectBattlePage()
        {
            int nextFloorNumber = Floor.nextFloorNumber;
         

            foreach(var floor in floors)
            {
               if(floor.nextFloor)
                {
                    nextFloorNumber = floor.numberFloor;
                }
            }

            while (true)
            {
                Console.WriteLine("던전\n");
                Console.WriteLine("현재 도전할 층은 {0} 층입니다.", nextFloorNumber);
                Console.WriteLine("        #.UI\n");
                
                foreach(var floor in floors)
                {
                    if(nextFloorNumber == floor.numberFloor)
                    {
                        floor.StatusFloor = "도전";
                    }

                    if(floor.numberFloor == 4)
                    {
                        Console.WriteLine("{0}.{1}층 {2}", floor.numberFloor, floor.numberFloor, floor.StatusFloor);

                    }
                    else Console.WriteLine("{0}.{1}층 {2}", floor.numberFloor, floor.numberFloor, floor.StatusFloor);
                }

                int input = GetInput(1, 4);

                if (input == 0) return;
                else if (input >= 1 && input <= 4)
                {
                    if(input == 1)
                    {
                       FloorBattleExit(input, monsters1);
                    }
                    else if(input == 2)
                    {
                        if (floors[input-1].StatusFloor == " ") FloorBattleExit(input, monsters2);
                        else Console.WriteLine("입장 할 수 없습니다.");
                    }
                   else if (input == 3)
                    {
                        if (floors[input - 1].StatusFloor == " ") FloorBattleExit(input, monsters3);
                        else Console.WriteLine("입장 할 수 없습니다.");
                    }
                    else if (input == 4)
                    {
                        if (floors[input - 1].StatusFloor == " ") FloorBattleExit(input, monsters3);
                        else Console.WriteLine("입장 할 수 없습니다.");
                    }
                }
                else Console.WriteLine("잘못된 입력입니다.");
            }
        }

        // 도전 할지 다시 묻는 선택 페이지
        public void FloorBattleExit(int floorinput, List<TestMonster> floorMonsters)
        {
            while (true)
            {
                Console.WriteLine("던전\n");
                Console.WriteLine("{0}층에 진입하려 합니다.", floorinput);
                Console.WriteLine("도전에 진입하면 UI에 접근하실 수 없습니다\n");
                Console.WriteLine("도전하시겠습니까?");
                Console.WriteLine("        #.UI\n");
                Console.WriteLine("1. 예");
                Console.WriteLine("2. 아니오\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                bool choice = GameView.SceneSelectYN();

                if (choice == true) StartPlayBattlePage(player, floorMonsters, floorinput);
                else if (player.Hp <= 0) Console.WriteLine("Hp가 0이라 진입 할 수 없습니다.");
                else return;
            }

        }

        // 공격, 스킬, 포션 사용 선택 창
        public void StartPlayBattlePage(Player player, List<TestMonster> monsters, int floorinput)
        {
            
            List<TestMonster> floormonsters = RandomMonster(monsters);
            int beforBattlehp = player.Hp;

            while (true)

            { 
                if(floormonsters.All(m => m.Hp <=0))
                {
                    ClearBattlePage(player, beforBattlehp, floorinput);
                    Console.WriteLine("던전 진입 화면으로 이동합니다.");
                    return;
                }
                else if(player.Hp <= 0)
                {
                    //player.Hp = 0; // player클래스에서 재정의 할지 아니면 set 설정 추가할지 ?
                    DefeatBattlePage(player, beforBattlehp);
                    Console.WriteLine("던전 진입 화면으로 이동합니다.");
                    return;
                }

                Console.WriteLine("Battle!! -  Player Turn\n");

                // 그룹화된 몬스터 출력 
                FloorSelectMontersView(floormonsters);


                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})",player.Level, player.Name, player.Job);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp,player.MaxHp);
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 포션사용\n");
                Console.WriteLine("0. 도망가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = GetInput(0,3);

                switch (input)
                {
                    case 0: 
                        return;
                    case 1:
                        AttackPlayerPage(player, floormonsters, floorinput);
                        break;
                    case 2:
                        SkillPlayerPage(player, job, floormonsters, floorinput);
                        break;
                    case 3:
                        PotionPlayerPage(player, floormonsters, floorinput);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;

                }
                Console.ReadKey();
            }

        }

        // 랜덤으로 층별 몬스터 반환 메서드
        public List<TestMonster> RandomMonster(List<TestMonster> monsters)
        {

            int takeNumber = random.Next(1, 5);
            // 몬스터 클래스에서 층에 해당하는 몬스터 그룹화

            // 몬스터 3마리 랜덤 선택
             List<TestMonster> randomFloorMonsters = monsters.OrderBy(m => random.Next()).Take(takeNumber).ToList();

            // 선택한 3마리 몬스터 반환
            return randomFloorMonsters;

        }

        //층으로 선택된 몬스터 3~4마리 출력하는 메서드
        public void FloorSelectMontersView(List<TestMonster> monsters, BattlStatusPage battlStatusPage = BattlStatusPage.BattleBase)
        {
            if(battlStatusPage == BattlStatusPage.BattleBase)
            {
                foreach (var monster in monsters)
                {
                    if (monster.isDead)
                    {
                        monster.Hp = 0;
                        Console.WriteLine("Lv. {0}. {1} Dead", monster.Level, monster.Name);
                    }

                    else Console.WriteLine("Lv. {0}. {1} HP {2} ", monster.Level, monster.Name, monster.Hp);
                }
            }
            else if(battlStatusPage == BattlStatusPage.BattleAttack)
           { 
                for(int i = 1; i <= monsters.Count; i++)
                {
                    if (monsters[i].Hp <= 0)
                    {
                        monsters[i].Hp = 0;
                        Console.WriteLine(i + "Lv. {0}. {1} Dead", monsters[i].Level, monsters[i].Name);
                    }

                    else Console.WriteLine(i + "Lv. {0}. {1} HP {2}", monsters[i].Level, monsters[i].Name, monsters[i].Hp);
                }

            }

        }

        // 공격할 몬스터 선택하는 페이지
        public void AttackPlayerPage(Player player, List<TestMonster> floormonsters, int floorinput)
        {
            while (true)
            {
                FloorSelectMontersView(floormonsters, BattlStatusPage.BattleAttack);

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("{0} {1} ({2})", player.Level, player.Name, player.Job);
                Console.WriteLine("{0}/{1}", player.Hp, player.MaxHp);
                Console.WriteLine("{0}/{1}\n", player.Mp, player.MaxHp);

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.");

                int selectMonster = GetInput(1, floormonsters.Count);

                if (selectMonster == 0) return;
                else if (selectMonster >= 1 && selectMonster <= floormonsters.Count)
                {
                    if (!floormonsters[selectMonster - 1].isDead)
                    {
                        floormonsters[selectMonster - 1] = PlayerAttack(player, floormonsters[selectMonster - 1], selectMonster);
                        player = MonasterAttack(player, floormonsters, selectMonster);
                    }
                    else Console.WriteLine("잘못된 입력입니다.");
                }
                else Console.WriteLine("잘못된 입력입니다.");

            }


        }
        
        // 플레이어가 공격하는 메서드
        public TestMonster PlayerAttack(Player player, TestMonster floormonster, int selectMonster)
        {
            int attackPencent = random.Next(0, 10); // 맞을 확률
            int hitNumber = random.Next(0, 10); // 치명타 확률
            int attackError =(int)Math.Round(player.Atk * 0.1);
            int finalDamage = random.Next(player.Atk-attackError, player.Atk + attackError);
            // 현재 몬스터 hp 이전
            int monsterBefor_hp = floormonster.Hp;

            while (true)
            {
                // 몬스터가 데미지를 받는지 확인
                if (attackPencent < player.Dexterity)
                {
                    // 몬스터에게 치명타가 터지는지 확인
                    if (hitNumber < player.Critical)
                    {
                        int hitDamage = (int)Math.Round(finalDamage*1.6); // 1.6으로 수정 할 것
                        floormonster.Hp -= hitDamage;

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("{0} 의 공격", player.Name);
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 치명타 공격!!\n",floormonster.Level, floormonster.Name, hitDamage);
                        Console.WriteLine("Lv. {0} {1}", floormonster.Level, floormonster.Name);

                        if (floormonster.Hp <= 0)
                        {
                            floormonster.Hp = 0;
                            floormonster.isDead = true;
                            Console.WriteLine("HP {0} -> Dead\n", monsterBefor_hp);
                        }
                        else
                        {
                            Console.WriteLine("HP {0} -> {1}\n",monsterBefor_hp, floormonster.Hp);
                        }
                    }
                    // 치명타 안 터지면 출력
                    else
                    {
                        floormonster.Hp -= finalDamage;

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("{0} 의 공격", player.Name);
                        Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]\n", floormonster.Level, floormonster.Name, floormonster.Atk);
                        Console.WriteLine("Lv. {0} {1}", floormonster.Level, floormonster.Name);

                        if (floormonster.Hp <= 0)
                        {
                            floormonster.Hp = 0;
                            floormonster.isDead = true;
                            Console.WriteLine("HP {0} -> Dead\n", monsterBefor_hp);
                        }
                        else
                        {
                            Console.WriteLine("HP {0} -> {1}\n", monsterBefor_hp, floormonster.Hp);
                        }

                    }
                }
                // 공격 실패 시 출력
                else
                {
                    Console.WriteLine("Battle\n");
                    Console.WriteLine("Lv. {0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n", floormonster.Level, floormonster.Name);

                }

                Console.WriteLine("0. 다음\n");

                int input = GetInput(0, 0);

                if (input == 0) return floormonster;
                else Console.WriteLine("잘못된 입력입니다.");
            }

        }

        // 몬스터가 공격하는 메서드  -> 
       public Player MonasterAttack(Player player, List<TestMonster> floormonsters, int selectMonster)
        {
            // 전투 중 플레이어 데미지 받기 전 hp
            int playerBeforHp = player.Hp;

            for (int i = 0;  i < floormonsters.Count; i++)
            {
                int attackPecent = random.Next(0, 9);
                int hitNumber = random.Next(0, 9);
                int attackError = (int)Math.Round(floormonsters[i].Atk * 0.1);
                int finalDamage = random.Next(floormonsters[i].Atk - attackError, floormonsters[i].Atk + attackError);

                // 몬스터의 공격이 성공 했는지 확인
                if (attackPecent < floormonsters[i].Critical)
                {
                    // 몬스터에게 치명타가 터지는지 확인
                    if (hitNumber < 1)
                    {
                        int hitdamage = (int)Math.Round(finalDamage * 1.2);
                        player = player.TakeDamge(hitdamage);

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {몬스터 레벨} {몬스터 이름}의 공격!");
                        Console.WriteLine("{플레이어 이름} 을(를 맞췄습니다.) [데미지 : {몬스터 데미지}] - 치명타 데미지\n");
                        Console.WriteLine("Lv. {플레이어 레벨} {플레이어 이름}");

                        player = player.IsDead(player);
                    }
                    // 치명타 안 터지면 출력
                    else
                    {
                        player = player.TakeDamge(finalDamage);

                        Console.WriteLine("Battle\n");
                        Console.WriteLine("Lv. {몬스터 레벨} {몬스터 이름}의 공격!");
                        Console.WriteLine("{플레이어 이름} 을(를 맞췄습니다.) [데미지 : {몬스터 데미지}]\n");
                        Console.WriteLine("Lv. {플레이어 레벨} {플레이어 이름}");


                        player = player.IsDead(player);


                    }
                }
                // 공격 실패 시 출력
                else
                {
                    Console.WriteLine("Battle\n");
                    Console.WriteLine("Lv. {0} {1} 가 공격했지만 아무일도 일어나지 않았습니다.\n");

                }

                Console.WriteLine("아무키나 누르세요.");
                Console.ReadKey();
            }
            return player;
        }

        // 플레이어 스킬 선택하는 메서드
        public void SkillPlayerPage(Player player,List<Job> job, List<TestMonster> floormonsters, int floorinput)
        {
            while (true)
            {
                FloorSelectMontersView(floormonsters);

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("Lv + Name + (job)");
                Console.WriteLine("hp/maxhp");
                Console.WriteLine("mp/maxmp\n");

                for (int i = 0; i < job.Count; i++)
                {
                    Console.WriteLine("{0} {스킬 이름} - MP {마나 소모량}", i);
                    Console.WriteLine("{스킬 설명}");
                }

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = 0;

                if (input == 0) return;
                else if (input >= 1 && input <= job.Count) Console.WriteLine("스킬 메서드를 실행한다.");
                else Console.WriteLine("잘못된 입력입니다.");

            }
            {
                Console.WriteLine("Battle!!\n");
                FloorSelectMontersView(floormonsters);
                Console.WriteLine();
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("Lv + Name + (job)");
                Console.WriteLine("hp/maxhp");
                Console.WriteLine("mp/maxmp\n");

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                


                int selectSkill = 1;

                if (selectSkill == 0) return;
                else if (selectSkill >= 1 && selectSkill <= 2)
                {
                    if (player.mp > 0)
                    {
                        
                    }
                    else Console.WriteLine("마나가 부족합니다.");
                }
                else Console.WriteLine("잘못된 입력입니다.");



            }


        }

        // 플레이어 포션 사용하는 메서드
        public void PotionPlayerPage(Player player, List<TestMonster> floormonsters, int floorinput)
        {
            Console.WriteLine("Battle!!\n");
            FloorSelectMontersView(floormonsters);
            Console.WriteLine();
            Console.WriteLine("[내정보]\n");
            Console.WriteLine("Lv + Name + (job)");
            Console.WriteLine("hp/maxhp");
            Console.WriteLine("mp/maxmp\n");
            Console.WriteLine("HP 포션(수량:{0})");
            Console.WriteLine("MP 포션(수량:{0})");

            Console.WriteLine("0. 취소\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");


            int selectPotion = 1;

                switch(selectPotion)
                {
                    case 0:
                        return;
                    case 1:
                        if (player.hp == 100) Console.WriteLine("HP가 MAX입니다.");
                        else UsePotion(selectPotion);
                        break;
                    case 2:
                        if (player.mp == 100) Console.WriteLine("MP가 MAC입니다.");
                        else UsePotion(selectPotion);
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;

                }

        }

        public void UsePotion(int selectPotion)
        {
            int beforeHp = player.hp;
            int beforeMp = player.mp;

            if(selectPotion == 1)
            {
                player.hp += 30;
           
                if(player.hp >= 100)
                {
                    player.hp = 100;
                }

                Console.WriteLine("Battle - HP 포션 사용");
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv 이름 직업");
                Console.WriteLine($"{beforeHp} -> {회복된 체력}");
                
            }
            else if(selectPotion == 2)
            {
                player.mp += 30;

                if (player.mp >= 100)
                {
                    player.mp = 100;
                }

                Console.WriteLine("Battle - HP 포션 사용");
                Console.WriteLine("[내정보]");
                Console.WriteLine("Lv 이름 직업");
                Console.WriteLine($"{beforeMp} -> {회복된 마나}");
            }
            Console.WriteLine("아무키나 입력하세요.");
            Console.ReadKey();

        }

        // 클리어 메서드
        public void ClearBattlePage(Player player, int beforeBattleHp, int clearfloor)
        {

            while (true)
            {
                foreach (var floor in floors)
                {
                    if (clearfloor == floor.numberFloor)
                    {
                        floor.clearFloor = true;
                        floor.nextFloor = false;
                        floor.StatusFloor = " ";                      
                    }
                }

                if(clearfloor >= 3)
                {
                   floors[3].nextFloor = true;
                }
                else
                {
                    floors[clearfloor + 1].nextFloor = true;
                }
            
                

                Console.WriteLine("Battle!! - Result\n");
                Console.WriteLine("Victory\n");
                Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.\n");
                Console.WriteLine("Lv. {플레이어 레벨} {플레이어 이름}");
                Console.WriteLine("HP {0} -> {현재 hp}\n", beforeBattleHp);
                Console.WriteLine("0. 다음");

                int input = 0;

                if (input == 0) return;
                else Console.WriteLine("잘못된 입력입니다.");

            }
        }

        // 패배 매서드
        public void DefeatBattlePage(Player player, int beforeBattleHp)
        {
            Console.WriteLine("Battle!! - Result\n");
            Console.WriteLine("You Lose\n");
            Console.WriteLine("Lv. {플레이어 레벨} {플레이어 이름}");
            Console.WriteLine("HP {0} -> {현재 hp}\n", beforeBattleHp);
            Console.WriteLine("0. 다음");

            int input = 0;

            if (input == 0) return;
            else Console.WriteLine("잘못된 입력입니다.");

        }

        public  int GetInput(int min, int max)
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

    }

    // 몬스터 출력 하는 씬 선택
    public enum BattlStatusPage
    {
        BattleBase,
        BattleAttack
    }


    // 층에 대한 내용 층수, 층의 상태, 클리어 여부, 다음층인지 확인
    public class Floor
    {
        public int numberFloor { get; set; }
        public string floorName { get; set; }
        public string StatusFloor { get; set; }
        public bool clearFloor { get; set; }
        public bool nextFloor { get; set; }

        public static int nextFloorNumber = 1;

        public Floor(int _NumberFloor, string _StatusFloor, bool _clearFloor, bool _nextFloor)
        {
            numberFloor = _NumberFloor;
            StatusFloor = _StatusFloor;
            clearFloor = _clearFloor;
            nextFloor = _nextFloor;
        }


    }

    class TestMonster
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; }
        int floor { get; set; }
        public int Critical { get; }
        public int Dexterity { get; }
        public bool isDead { get; set; }

        private static Random rand = new Random();

        public TestMonster(int _Level, string _Name, int _Hp, int _Atk, int _floor, int _Critical, int _Dex, bool _Dead)
        {
            Level = _Level;
            Name = _Name;
            Hp = _Hp;
            Atk = _Atk;
            Hp = _Hp;
            Atk = _Atk;
            floor = _floor;
            Critical = _Critical;
            Dexterity = _Dex;
            isDead = _Dead;
        }

        public static List<TestMonster> GenerateRandomMonsters(int count, int floorLevel)
        {
            List<TestMonster> monsters = new List<TestMonster>();
            string[] names = { "Goblin", "Orc", "Slime", "Skeleton", "Wolf", "Zombie", "Troll" };

            for (int i = 0; i < count; i++)
            {
                string name = names[rand.Next(names.Length)];
                int level = floorLevel + rand.Next(3); // 층에 따라 레벨 반영
                int hp = 50 + level * 10 + rand.Next(20); // 레벨 기반 체력 설정
                int atk = 5 + level * 2 + rand.Next(5); // 공격력
                int critical = rand.Next(5, 21); // 크리티컬 확률 (5~20%)
                int dexterity = rand.Next(5, 21); // 민첩성 (5~20%)
                bool isDead = false;

                monsters.Add(new TestMonster(level, name, hp, atk, floorLevel, critical, dexterity, isDead));
            }

            return monsters;
        }

    }

}


