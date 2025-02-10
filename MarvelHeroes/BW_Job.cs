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
        IronMan = 0,
        SpiderMan = 1,
        DoctorStrange = 2,
        Hulk =3
    }
    
    // 직업별 기본 능력치 저장

    public class Job
    {
        public static Dictionary<JobType, (string name, int atk, int def, int hp, int mp)> jobStats =   //Dictionary<TKey, TValue>는 키(Key)와 값(Value)을 쌍으로 저장하는 자료구조
            new Dictionary<JobType, (string, int, int, int, int)>   //직업(JobType)에 따른 공격력, 방어력, 체력, 마나 값을 저장하는 딕셔너리
            {
                { JobType.IronMan, (name: "아이언맨", atk: 50, def: 30, hp: 100, mp: 200) },   //즉, 어떤 직업(JobType)을 키로 사용하여, 해당 직업의 기본 능력치를 값으로 저장하는 구조
                { JobType.SpiderMan, (name:"스파이더맨", atk: 60, def: 25, hp: 120, mp: 150) },   //JobType을 키(key)로 사용하고,
                { JobType.DoctorStrange, (name:"닥터스트레인지", atk: 40, def: 20, hp: 80, mp: 300) }, //(int atk, int def, int hp, int mp) 튜플을 값(value)으로 저장
                { JobType.Hulk, (name: "헐크", atk: 80, def: 50, hp: 200, mp: 50) }           // Dictionary를 사용한 이유?                                                                               // 만약 직업이 10개, 20개로 늘어나더라도 쉽게 관리할 수 있음!
            };

        //BW_Job 생성자는 캐릭터를 만들 때 사용됨.


        //public Job(JobType job, int level, string name, int gold)
        //    : base(level, name, job.ToString(), jobStats[job].atk, 0, jobStats[job].def, 0, gold, jobStats[job].hp, jobStats[job].mp, jobStats[job].hp, 15, 90, false)
        //{

            
        //    Console.WriteLine($"[{job}] 직업이 선택되었습니다!");  //캐릭터가 생성될 때, 어떤 직업을 선택했는지 출력해줌.
        //}                                                          //jobStats[job] → 직업에 맞는 능력치를 자동으로 설정

        // 모든 직업 목록 출력
        public static void PrintJobList()    //사용 가능한 직업을 콘솔에 출력하는 함수
        {
            foreach (var job in Enum.GetValues(typeof(JobType)))   //JobType의 모든 값을 가져와서 반복문을 돌림.
            {
                Console.WriteLine($"- {job}");  //직업 리스트를 한 줄씩 출력.
            }
        }


    }
}
