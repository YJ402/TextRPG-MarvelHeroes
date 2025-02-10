using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class Inventory
    {
        List<Item> items = new List<Item>();
        List<Item> Equippeditems = new List<Item>();

        public void ClickItem(Player player, Item item)
        {
            //아이템이 장착템이면 장착템에 넣기
            if(item is EquipItem)
                Equippeditems.Add(item);
            item.Use(player);
        }
        //public void Equip(Player player, Item item)
        //{
        //    //직업이 알맞은지 검사 && 장착중인 아이템인지 검사
        //            //true: 벗기
        //            //false: 장착하기
        //                //해당부위에 다른 아이템이 장착중인지 검사
        //                    //그 아이템을 벗기기
        //                //장착하기
        //}

        //public void Equip_Equip(Player player, Item item)
        //{
        //    Equippeditems.Add(item);
        //    item.IsEquip = true;
        //}

        //public void Equip_Unequip(Player player, Item item)
        //{


        //}

        //public override void Use(Player player)
        //{
        //    //플레이어의 직업의 종류를 불러옴
        //    //조건문if 사용해서 플래이어의 직업과 무기의 직업을 비교하는 조건
        //    //착용 불가 메세지 후 return
        //    if (player.PlayerJob != this.ItemJobType.ToString())
        //    {
        //        Console.WriteLine("사용이 불가능한 장비입니다!");
        //        return;
        //    }

        //    if (this.IsEquip) //장착 중인 아이템 선택시 해제
        //    {
        //        this.IsEquip = false;

        //        if (this.ItemType == ItemType.Weapon)
        //            player.EquipAtk -= this.Value;
        //        else if (this.ItemType == ItemType.Amor)
        //            player.EquipDef -= this.Value;
        //    }
        //    else //아이템 착용
        //    {
        //        this.IsEquip = true;

        //        if (this.ItemType == ItemType.Weapon) //타입이 무기일 경우 공격력 증가 
        //            player.EquipAtk += this.Value;
        //        else if (this.ItemType == ItemType.Amor) //타입이 갑옷일 경우 방어력 증가
        //            player.EquipDef += this.Value;
        //    }
        //}

    }
}

