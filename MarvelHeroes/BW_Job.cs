using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public enum JobType
    {
        IronMan,
        SpiderMan,
        DoctorStrange,
        Hulk
    }
    
    // 직업별 기본 능력치 저장

    public class Job : Player
    {
        private static Dictionary<JobType, (int atk, int def, int hp, int mp)> jobStats =   //Dictionary<TKey, TValue>는 키(Key)와 값(Value)을 쌍으로 저장하는 자료구조
            new Dictionary<JobType, (int, int, int, int)>   //직업(JobType)에 따른 공격력, 방어력, 체력, 마나 값을 저장하는 딕셔너리
            {
                { JobType.IronMan, (atk: 50, def: 30, hp: 100, mp: 200) },   //즉, 어떤 직업(JobType)을 키로 사용하여, 해당 직업의 기본 능력치를 값으로 저장하는 구조
                { JobType.SpiderMan, (atk: 60, def: 25, hp: 120, mp: 150) },   //JobType을 키(key)로 사용하고,
                { JobType.DoctorStrange, (atk: 40, def: 20, hp: 80, mp: 300) }, //(int atk, int def, int hp, int mp) 튜플을 값(value)으로 저장
                { JobType.Hulk, (atk: 80, def: 50, hp: 200, mp: 50) }           // Dictionary를 사용한 이유?                                                                               // 만약 직업이 10개, 20개로 늘어나더라도 쉽게 관리할 수 있음!
            };

        //BW_Job 생성자는 캐릭터를 만들 때 사용됨.


        public Job(JobType job, int level, string name, int gold)
            : base(level, name, job.ToString(), jobStats[job].atk, 0, jobStats[job].def, 0, gold, jobStats[job].hp, jobStats[job].mp, jobStats[job].hp, 0, 0, false)
        {

            
            Console.WriteLine($"[{job}] 직업이 선택되었습니다!");  //캐릭터가 생성될 때, 어떤 직업을 선택했는지 출력해줌.
        }                                                          //jobStats[job] → 직업에 맞는 능력치를 자동으로 설정

        // 모든 직업 목록 출력
        public static void PrintJobList()    //사용 가능한 직업을 콘솔에 출력하는 함수
        {
            foreach (var job in Enum.GetValues(typeof(JobType)))   //JobType의 모든 값을 가져와서 반복문을 돌림.
            {
                Console.WriteLine($"- {job}");  //직업 리스트를 한 줄씩 출력.
            }
        }

        public void Skill(int index)
        {
            switch (Job)
            {
                case "IronMan":
                    if (index  == 1)
                    {
                        Console.WriteLine("아이언맨이 레이저 커터를 발사했다!");
                    }
                    else if(index == 2)
                    {
                        Console.WriteLine("아이언맨이 리펄서건를 사용했다! 회피율이 증가한다.");
                    }
                    else
                    {
                        Console.WriteLine("입력이 잘못되었습니다");
                    }
                    
                    break;
            }

            switch (Job)
            {
                case "SpiderMan":
                    if (index == 1)
                    {
                        Console.WriteLine("나노슈트 발동 공격력, 방어력 등을 올립니다");
                    }
                    else if (index == 2)
                    {
                        Console.WriteLine("거미줄 발사  거미줄로 상대방을 공격.");
                    }
                    else
                    {
                        Console.WriteLine("입력이 잘못되었습니다");
                    }

                    break;
            
            }
            
            switch (Job)
            {
                case "DoctorStrange":
                    if (index == 1)
                    {
                        Console.WriteLine("발탁의 화살 = 지면에 폭발을 일으켜 상대방에게 대미지를 부여");
                    }
                    else if (index == 2)
                    {
                        Console.WriteLine("호고스의 백마법 = 상대방의 공격을 무효화 합니다.");
                    }
                    else
                    {
                        Console.WriteLine("입력이 잘못되었습니다");
                    }

                    break;
            }

            switch (Job)
            {
                case "Hulk":
                    if (index == 1)
                    {
                        Console.WriteLine("비브라늄 펀치 = 비브라늄 너클을 사용하여 강력한 펀치를 날린다");
                    }
                    else if (index == 2)
                    {
                        Console.WriteLine("헐크의 샤우팅 = 분노와 함께 강력한 소리를 내뿜어 상대방이 1턴간 스턴된다");
                    }
                    else
                    {
                        Console.WriteLine("입력이 잘못되었습니다");
                    }

                    break;
            }
        }





    }
}
