using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{

    //아이템 - 장착유무, 이름, 공력력||방어력||회복량, 설명, 가격

    internal class DH_ItemManager
    {
        List<EquipItem> equipItems;
        List<UsingItem> usingItems;

        //DH_ItemManager 호출시 실행
        public DH_ItemManager()
        {
            //public EquipItem(string name, ItemType itemtype, JobType jobType, int value, string descrip, int cost)
            equipItems = new List<EquipItem>
            {
                new EquipItem("아이언맨 기본 무기", ItemType.Weapon, JobType.IronMan, 5,"아이언맨의 기본무기", 500),
                new EquipItem("스파이더맨 기본 무기", ItemType.Weapon, JobType.Spiderman, 5,"스파이더맨의 기본무기", 500),
                new EquipItem("닥터스트레인지 기본 무기", ItemType.Weapon, JobType.Dr, 5,"닥터스트레인지의 기본무기", 500),
                new EquipItem("헐크 기본 무기", ItemType.Weapon, JobType.Hulk, 5,"헐크의 기본무기", 500)
            };

            //public UsingItem(string name, ItemType type, int value, string descrip, int cost)
            usingItems = new List<UsingItem>
            {
                new UsingItem("소형 힐링 포션", ItemType.Healing, 20,"소량의 체력을 회복시켜줍니다.", 10),
                new UsingItem("대형 힐링 포션", ItemType.Healing, 50,"대량의 체력을 회복시켜줍니다.", 30)
            };
        }
    }

    public enum JobType
    {
        IronMan,
        Spiderman,
        Dr,
        Hulk
    }

    public enum ItemType
    {
        Weapon,
        Amor,
        Healing
    }

    public interface Item
    {
        public string Name { get; }
        public int Value { get; }
        public string Descrip { get; }
        public int Cost { get; }
    }

    public class EquipItem : Item
    {
        public string Name { get; }
        public JobType JobType { get; }
        public ItemType ItemType { get; }
        public int Value { get; }
        public string Descrip { get; }
        public int Cost { get; }
        public bool IsPurchase { get; set; }
        public bool IsEquip { get; set; }

        public EquipItem(string name, ItemType itemtype, JobType jobType, int value, string descrip, int cost)
        {
            Name = name;
            ItemType = itemtype;
            JobType = jobType;
            Value = value;
            Descrip = descrip;
            Cost = cost;
            IsPurchase = false;
            IsEquip = false;
        }
    }

    public class UsingItem
    {
        public string Name { get; }
        public ItemType Type { get; }
        public int Value { get; }
        public string Descrip { get; }
        public int Cost { get; }

        public UsingItem(string name, ItemType type, int value, string descrip, int cost)
        {
            Name = name;
            Type = type;
            Value = value;
            Descrip = descrip;
            Cost = cost;
        }
    }


}
