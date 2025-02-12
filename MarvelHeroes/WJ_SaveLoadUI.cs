
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarvelHeroes
{
    internal class SaveLoadUI
    {
        public static string path = AppDomain.CurrentDomain.BaseDirectory;

        public static void SaveData()
        {
            string playerData = JsonConvert.SerializeObject(GameManager.Instance.player);
            File.WriteAllText(path + "\\playerData.json", playerData);

            string inventoryData = JsonConvert.SerializeObject(GameManager.Instance.inventory.items);
            File.WriteAllText(path + "\\UserInventoryData.json", inventoryData);

            string acceptQuestData = JsonConvert.SerializeObject(GameManager.Instance.QM.acceptQuest);
            File.WriteAllText(path + "\\acceptQuestData.json", acceptQuestData);
        }

        public static void LoadData()
        {
            // 유저 데이터가 없을 때 -> 세이브 데이터가 없을 때
            if (!File.Exists(path + "\\playerData.json"))
            {
                SaveData();
                return;
            }
            else
            {
                string playerLData = File.ReadAllText(path + "\\playerData.json");
                Player playerLoadData = JsonConvert.DeserializeObject<Player>(playerLData);
                GameManager.Instance.player = playerLoadData;

                string inventoryLData = File.ReadAllText(path + "\\UserInventoryData.json");
                EquipItem[] inventoryLoadData = JsonConvert.DeserializeObject<EquipItem[]>(inventoryLData);
                if (GameManager.Instance.inventory.items == null)
                {
                    GameManager.Instance.inventory.items = new List<Item>(); // 리스트를 초기화
                }
                foreach (EquipItem data in inventoryLoadData)
                {
                    GameManager.Instance.inventory.AddItem(data);
                }

                string acceptQuestLData = File.ReadAllText(path + "\\acceptQuestData.json");
                HuntQuest[] acceptQuestLoadData = JsonConvert.DeserializeObject<HuntQuest[]>(acceptQuestLData);
                if (GameManager.Instance.QM.acceptQuest == null)
                {
                    GameManager.Instance.QM.acceptQuest = new List<Quest>(); // 리스트를 초기화
                }
                foreach (HuntQuest data in acceptQuestLoadData)
                {
                    GameManager.Instance.QM.acceptQuest.Add(data);
                }
            }
        }


        public void SaveLoadScene()
        {
            while (true)
            {
                Console.Clear();
                GameView.PrintText("<게임 저장/불러오기>", 0, ConsoleColor.Magenta);
                Console.WriteLine("현재 진행 상황이 저장/불러오기 됩니다.");
                Console.WriteLine();
                Console.WriteLine("게임을 저장/불러오기 하기겠습니까?");
                Console.WriteLine();
                Console.WriteLine("1. 저장");
                Console.WriteLine("2. 불러오기");
                Console.WriteLine("0. 아니오");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice == 0) { break; }
                    else if (choice == 1) { SaveData(); }
                    else if (choice == 2) { LoadData(); }
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
        }
    }
}
