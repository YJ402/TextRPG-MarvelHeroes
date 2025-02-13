using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    //아이템 - 장착유무, 이름, 공력력||방어력||회복량, 설명, 가격

    public class ItemManager
    {
        public List<Item> equipItems;
        public List<Item> usingItems;
        public List<Item> Alltems;
        public Inventory inventory;
        //DH_ItemManager 호출시 실행
        public ItemManager()
        {
            inventory = new Inventory();
            GameManager.Instance.inventory = inventory;
            //public EquipItem(string name, ItemType itemtype, JobType jobType, int value, string descrip, int cost)
            equipItems = new List<Item>
            {
                new EquipItem("아이언맨 기본 무기", ItemType.Weapon, JobType.IronMan, 5,"아이언맨의 기본무기", 500),
                new EquipItem("스파이더맨 기본 무기", ItemType.Weapon, JobType.SpiderMan, 5,"스파이더맨의 기본무기", 500),
                new EquipItem("닥터스트레인지 기본 무기", ItemType.Weapon, JobType.DoctorStrange, 5,"닥터스트레인지의 기본무기", 500),
                new EquipItem("헐크 기본 무기", ItemType.Weapon, JobType.Hulk, 5,"헐크의 기본무기", 500),
                new EquipItem("아이언맨 기본 갑옷", ItemType.Amor, JobType.IronMan, 5,"아이언맨의 기본방어구", 500),
                new EquipItem("스파이더맨 기본 갑옷", ItemType.Amor, JobType.SpiderMan, 5,"스파이더맨의 기본방어구", 500),
                new EquipItem("닥터스트레인지 기본 갑옷", ItemType.Amor, JobType.DoctorStrange, 5,"닥터스트레인지의 기본방어구", 500),
                new EquipItem("헐크 기본 갑옷", ItemType.Amor, JobType.Hulk, 5,"헐크의 기본방어구", 500),

                new EquipItem("Mk.7 건틀렛",ItemType.Weapon, JobType.IronMan, 10,"아이언맨의 Mk.7의 건틀렛", 1000),
                new EquipItem("웹 슈터", ItemType.Weapon, JobType.SpiderMan, 10,"스파이더맨의 거미줄 히히 발싸 장비", 1000),
                new EquipItem("와툼의 지팡이", ItemType.Weapon, JobType.DoctorStrange, 10, "옥타 에센스라는 신들 중 하나인 와툼의 지팡이", 1000),
                new EquipItem("로키", ItemType.Weapon, JobType.Hulk, 10, "헐크한테 다리가 잡혀 무기로 사용되는 불쌍한 로키", 1000),
                new EquipItem( "Mk.7 아머", ItemType.Amor, JobType.IronMan, 10, "아이언맨의 Mk.7의 아머", 1000),
                new EquipItem("스타크 슈트", ItemType.Amor, JobType.SpiderMan, 10, "아이언맨이 준 스파이더맨의 슈트", 1000),
                new EquipItem("클록 오브 리비테이션", ItemType.Amor, JobType.DoctorStrange, 10, "닥터스트레인지의 스스로 날아다니는 망토", 1000),
                new EquipItem("질긴 청바지", ItemType.Amor, JobType.Hulk, 10, "동대문에서 산 질긴 청바지", 1000),

                new EquipItem("나노 건틀렛",ItemType.Weapon, JobType.IronMan, 30,"나노테크로 완성된 아이언맨의 건틀렛", 3000),
                new EquipItem("웹 슈터 ver2", ItemType.Weapon, JobType.SpiderMan, 30, "스파이더맨의 거미줄 히히 발싸 슈웅 장비", 3000),
                new EquipItem("비샨티의 서",ItemType.Weapon, JobType.DoctorStrange, 30,"비샨티의 주신인 우쉬투르가 직접 정리한 백마법서", 3000),
                new EquipItem("헐크버스터 건틀렛", ItemType.Weapon, JobType.Hulk, 30, "아이언맨에게 강탈한 헐크버스터 건틀렛", 3000),
                new EquipItem("Mk.85 나노 아머", ItemType.Amor, JobType.IronMan, 30, "나노테크로 완성된 Mk.85 아이언맨 아머", 3000),
                new EquipItem("아이언 스파이더 슈트", ItemType.Amor, JobType.SpiderMan, 30, "스파이더맨 나노테크 아머", 3000),
                new EquipItem("아가모토의 눈", ItemType.Amor, JobType.DoctorStrange, 30, "닥터스트레인지의 타임스톤이 들어가 시간조절이 가능한 목걸이", 3000),
                new EquipItem("헐크버스터 아머", ItemType.Amor, JobType.Hulk, 30, "아이언맨에게 강탈한 헐크버스터 아머", 3000)
            };

            //public UsingItem(string name, ItemType type, int value, string descrip, int cost)
            usingItems = new List<Item>
            {
                new UsingItem("소형 힐링 포션", ItemType.Healing, 20, "소량의 체력을 회복시켜줍니다.", 10, 1),
                new UsingItem("대형 힐링 포션", ItemType.Healing, 50, "대량의 체력을 회복시켜줍니다.", 30, 1),
                new UsingItem("소형 마나 포션", ItemType.Regeneration, 20, "소량의 마나를 재생시켜줍니다.", 20, 1),
                new UsingItem("대형 마나 포션", ItemType.Regeneration, 50, "대량의 마나를 재생시켜줍니다.", 40, 1)
            };

            Alltems = new List<Item>();

            foreach (Item item in equipItems)
            {
                Alltems.Add(item);
            }

            foreach (Item item in usingItems)
            {
                Alltems.Add(item);
            }
        }

    }
    //직업종류의 상수
    //public enum ITemJobType
    //{
    //    IronMan,
    //    Spiderman,
    //    Dr,
    //    Hulk
    //}

    //아이템 종류의 상수
    public enum ItemType
    {
        Weapon,
        Amor,
        Healing,
        Regeneration
    }

    //아이템 클래스
    public abstract class Item
    {
        public string Name { get; set; }
        public ItemType ItemType { get; set; }
        public int Value { get; set; }
        public string Descrip { get; set; }
        public int Cost { get; set; }
        public bool IsEquip { get; set; }

        public Item(string name, ItemType itemtype, int value, string descrip, int cost)
        {
            Name = name;
            ItemType = itemtype;
            Value = value;
            Descrip = descrip;
            Cost = cost;
            IsEquip = false;
        }

        public abstract void Use(Player Player);

    }

    //장착아이템 클래스
    public class EquipItem : Item
    {
        public JobType ItemJobType { get; set; }
        public bool IsPurchase { get; set; }



        //public Child(int X) : base(X) { } //base키워드를 이용하여 상속
        public EquipItem(string name, ItemType itemtype, JobType itemJobType, int value, string descrip, int cost) : base(name, itemtype, value, descrip, cost)
        {
            this.ItemJobType = itemJobType;
            this.IsPurchase = false;
            this.IsEquip = false;
        }

        public override void Use(Player player)
        {
            //플레이어의 직업의 종류를 불러옴
            //조건문if 사용해서 플래이어의 직업과 무기의 직업을 비교하는 조건
            //착용 불가 메세지 후 return
            if (player.PlayerJob != ItemJobType)
            {
                Console.WriteLine("사용이 불가능한 장비입니다!");
                Console.ReadKey();
                return;
            }

            if (IsEquip) //장착 중인 아이템 선택시 해제
            {

                IsEquip = false;

                if (ItemType == ItemType.Weapon)
                    player.EquipAtk -= Value;
                else if (ItemType == ItemType.Amor)
                    player.EquipDef -= Value;
            }

            else //아이템 착용
            {
                foreach (Item item in GameManager.Instance.inventory.items)
                {
                    EquipItem nowEquie = item as EquipItem; // 이거 힐링 아이템은 nowEquie이 안 들어가서 null 발생 함!!

                    if (nowEquie == null) continue; // Healing 같은 아이템이면 건너뛰기

                    if (nowEquie.IsEquip && nowEquie.ItemType == ItemType)
                    {
                        Console.WriteLine("같은 종류의 아이템이 장착되어 있습니다.");
                        Console.ReadKey();
                        return;
                    }
                }

                IsEquip = true;

                if (ItemType == ItemType.Weapon) //타입이 무기일 경우 공격력 증가 
                    player.EquipAtk += Value;
                else if (ItemType == ItemType.Amor) //타입이 갑옷일 경우 방어력 증가
                    player.EquipDef += Value;

                foreach (var q in GameManager.Instance.QM.acceptQuest)
                {

                    if (q is not EquipQuest) continue; 

                    if ((q as EquipQuest).RequiredType == ItemType)
                    {
                        q.IsCompleted(GameManager.Instance.player, null, this);
                        Console.ReadKey();
                    }
                }
            }
        }
    }

    //소모아이템 클래스
    public class UsingItem : Item
    {
        //quantity가 0이하면 삭제하는 추가 로직
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                if(quantity <= 0)
                GameManager.Instance.inventory.RemoveItem(this);
            }
        }

        public UsingItem(string name, ItemType itemtype, int value, string descrip, int cost, int quantity) : base(name, itemtype, value, descrip, cost)
        {
            Quantity = quantity;
        }

        public override void Use(Player player)
        {
            //플레이어의 체력을 불러옴
            //조건문if 사용해서 포션 수량을 비교하는 조건
            //사용 불가 메세지 후 return
            if (this.Quantity <= 0)
            {
                Console.WriteLine($"{Name}이(가) 없습니다.");
                return;
            }

            //아이템타입이 힐링일경우 회복
            if (ItemType == ItemType.Healing)
            {
                player.Hp += this.Value;

                Quantity--;
                Console.WriteLine($"{player.Name}이(가) {Name}을 사용하였습니다.");
                Console.WriteLine($"체력을 {Value}만큼 회복합니다.(남은 개수 : {Quantity})");
                Console.ReadKey();
            }
            else if (ItemType == ItemType.Regeneration)
            {
                player.Mp += this.Value;

                Quantity--;
                Console.WriteLine($"{player.Name}이(가) {Name}을 사용하였습니다.");
                Console.WriteLine($"마나가 {Value}만큼 재생합니다.(남은 개수 : {Quantity})");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"{Name}은(는) 사용할 수 없는 아이템입니다.");
            }
        }
    }
}
