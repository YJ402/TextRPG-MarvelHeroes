﻿using System;
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
                new EquipItem("아이언맨 기본 갑옷", ItemType.Amor, JobType.IronMan, 5,"아이언맨의 기본방어구", 1000),
                new EquipItem("스파이더맨 기본 갑옷", ItemType.Amor, JobType.SpiderMan, 5,"스파이더맨의 기본방어구", 1000),
                new EquipItem("닥터스트레인지 기본 갑옷", ItemType.Amor, JobType.DoctorStrange, 5,"닥터스트레인지의 기본방어구", 1000),
                new EquipItem("헐크 기본 갑옷", ItemType.Amor, JobType.Hulk, 5,"헐크의 기본방어구", 1000)
            };

            //public UsingItem(string name, ItemType type, int value, string descrip, int cost)
            usingItems = new List<Item>
            {
                new UsingItem("소형 힐링 포션", ItemType.Healing, 20, "소량의 체력을 회복시켜줍니다.", 10, 0),
                new UsingItem("대형 힐링 포션", ItemType.Healing, 50, "대량의 체력을 회복시켜줍니다.", 30, 0),
                new UsingItem("소형 마나 포션", ItemType.Regeneration, 20, "소량의 마나를 재생시켜줍니다.", 20, 0),
                new UsingItem("대형 마나 포션", ItemType.Regeneration, 50, "대량의 마나를 재생시켜줍니다.", 40, 0)
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
            if (player.PlayerJob != this.ItemJobType)
            {
                Console.WriteLine("사용이 불가능한 장비입니다!");
                return;
            }

            if (this.IsEquip) //장착 중인 아이템 선택시 해제
            {
                this.IsEquip = false;

                if (this.ItemType == ItemType.Weapon)
                    player.EquipAtk -= this.Value;
                else if (this.ItemType == ItemType.Amor)
                    player.EquipDef -= this.Value;
            }
            else //아이템 착용
            {
                this.IsEquip = true;

                if (this.ItemType == ItemType.Weapon) //타입이 무기일 경우 공격력 증가 
                    player.EquipAtk += this.Value;
                else if (this.ItemType == ItemType.Amor) //타입이 갑옷일 경우 방어력 증가
                    player.EquipDef += this.Value;

                foreach(var q in GameManager.Instance.QM.acceptQuest)
                {
                    if ((q as EquipQuest).RequiredType == ItemType)
                    { 
                        GameManager.Instance.QM.CheckCompleteQuest(GameManager.Instance.player, null, this);
                        Console.ReadKey();
                        break;
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
                if (quantity <= 0) // && GameManager.Instance.inventory.items.Contains(this)
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
            }
            else if(ItemType == ItemType.Regeneration)
            {
                player.Mp += this.Value;

                Quantity--;
                Console.WriteLine($"{player.Name}이(가) {Name}을 사용하였습니다.");
                Console.WriteLine($"마나가 {Value}만큼 재생합니다.(남은 개수 : {Quantity})");
            }
            else
            {
                Console.WriteLine($"{Name}은(는) 사용할 수 없는 아이템입니다.");
            }
        }
    }
}
