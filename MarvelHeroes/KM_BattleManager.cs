using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    internal class BattleManager
    {
        BW_Player player = new BW_Player();
        List<BW_Monster> monsters = new List<BW_Monster>();


        
        public void SelectBattlePage()
        {
            while (true)
            {
                Console.WriteLine("던전\n");
                Console.WriteLine("현재 도전할 층은 {0} 층입니다.");
                Console.WriteLine("        #.UI\n");
                Console.WriteLine("1. 1층");
                Console.WriteLine("2. 2층");
                Console.WriteLine("3. 3층(도전)\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = 0;

                switch (input)
                {
                    case 1:
                        FloorBattleExit(input);
                        break;
                    case 2:
                        FloorBattleExit(input);
                        break;
                    case 3:
                        FloorBattleExit(input);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }

            }
        }


        public void FloorBattleExit(int input)
        {
            
            while (true)
            {
                Console.WriteLine("던전\n");
                Console.WriteLine("{0}층에 진입하려 합니다.", input);
                Console.WriteLine("도전에 진입하면 UI에 접근하실 수 없습니다\n");
                Console.WriteLine("도전하시겠습니까?");
                Console.WriteLine("        #.UI\n");
                //bool choice = GameView.ViewYesOrNo();
                Console.WriteLine("1. 예");
                Console.WriteLine("2. 아니오\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                switch (choice)
                {
                    case true:
                        StartPlayBattle(player, monsters, input);
                        break;
                    case false:
                        return;
                }
            }

        }

        public void StartPlayBattle(BW_Player player, List<BW_Monster> monsters, int select)
        {
            
            List<BW_Monster> floormonsters = RandomMonster(monsters, select);

            while (true)
            {


                Console.WriteLine("Battle!! -  Player Turn\n");

                // 그룹화된 몬스터 출력 
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine(i + "Lv + name + hp");
                }

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("Lv + Name + (job)");
                Console.WriteLine("hp/maxhp");
                Console.WriteLine("mp/maxmp\n");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬");
                Console.WriteLine("3. 포션사용\n");
                Console.WriteLine("0. 도망가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");

                int input = 0;

                switch (input)
                {
                    case 1:
                        AttackPlayerPage(player, floormonsters, input);
                        break;
                    case 2:
                        SkillPlayerPage(player, floormonsters, input);
                        break;
                    case 3:
                        PotionPlayerPage(player, floormonsters, input);
                        break;
                    case 0:
                        return;

                }
            }

        }

        // 랜덤으로 층별 몬스터 반환 메서드
        public List<BW_Monster> RandomMonster(List<BW_Monster> monsters, int select)
        {
            // select에 해당하는 층에 몬스터들
            Random random = new Random();

            // 몬스터 클래스에서 층에 해당하는 몬스터 그룹화
            List<BW_Monster> floorMonster = monsters.Where(m => m.floor == select).ToList();

            // 몬스터 3마리 랜덤 선택
             List<BW_Monster> randomMonsters = monsters.OrderBy(m => random.Next()).Take(3).ToList();

            // 선택한 3마리 몬스터 반환
            return randomMonsters;

        }



        public void AttackPlayerPage(BW_Player player, List<BW_Monster> monsters, int select)
        {
            while (true)
            {
                Console.WriteLine("Battle\n");
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine(i + "Lv + name + hp");
                }

                Console.WriteLine("");
                Console.WriteLine("[내정보]\n");
                Console.WriteLine("Lv + Name + (job)");
                Console.WriteLine("hp/maxhp");
                Console.WriteLine("mp/maxmp\n");

                Console.WriteLine("0. 취소\n");
                Console.WriteLine("대상을 선택해주세요.");

                int input = 1;

                switch (input)
                {
                    case 1:
                        PlayerAttack(player, monsters, input);
                        MonasterAttack(player, monsters, input);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 0:
                        return;

                }
            }


        }

        public void PlayerAttack(BW_Player player, List<BW_Monster> monster, int input)
        {
            Random random = new Random();

            int attackNumber = random.Next(0, 9);

            if(attackNumber > 3)
            {
                // 몬스터 체력 감소\
                // monster[input].hp -= player.attack
            }

            Console.WriteLine("아무키나 눌러주세요.");
            Console.ReadKey();
        }

       public void MonasterAttack(BW_Player player, List<BW_Monster> monster, int input)
        {

        }




        public void SkillPlayerPage(BW_Player player, List<BW_Monster> monsters, int select)
        {
            //스킬 클래스가 필요할 것으로 생각 됨
        }

        public void PotionPlayerPage(BW_Player player, List<BW_Monster> monsters, int select)
        {


        }


    }




    public class FloorBattle
    {
        public int currentFloor {  get; set; }
        public int lasttFloor { get; set; }

        public FloorBattle(int _currentFloor, int _lastFloor)
        {
            currentFloor = _currentFloor;
            lasttFloor = _lastFloor;

        }


    }



}


