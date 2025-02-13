
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
            string playerData = JsonConvert.SerializeObject(GameManager.Instance.player); //플레이어
            File.WriteAllText(path + "\\playerData.json", playerData);

            string inventoryData = JsonConvert.SerializeObject(GameManager.Instance.inventory.items, new JsonSerializerSettings{ TypeNameHandling = TypeNameHandling.All }); // 인벤토리
            File.WriteAllText(path + "\\UserInventoryData.json", inventoryData); 

            string acceptQuestData = JsonConvert.SerializeObject(GameManager.Instance.QM.acceptQuest, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }); // 퀘스트
            File.WriteAllText(path + "\\acceptQuestData.json", acceptQuestData);

        }

        public static void LoadData()
        {
            // 플레이어 데이터가 없을 때 -> 세이브 데이터가 없을 때
            if (!File.Exists(path + "\\playerData.json"))
            {
                SaveData();
                return;
            }
            else // 데이터가 있을때 불러오기
            {
                string playerLData = File.ReadAllText(path + "\\playerData.json");
                Player playerLoadData = JsonConvert.DeserializeObject<Player>(playerLData);
                GameManager.Instance.player = playerLoadData;

                string inventoryLData = File.ReadAllText(path + "\\UserInventoryData.json");
                List<Item> inventoryLoadData = JsonConvert.DeserializeObject<List<Item>>(inventoryLData ,new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                if (GameManager.Instance.inventory.items == null)
                {
                    GameManager.Instance.inventory.items = new List<Item>(); // 리스트를 초기화
                }
                foreach (Item data in inventoryLoadData)
                {
                    GameManager.Instance.inventory.AddItem(data);
                }

                string acceptQuestLData = File.ReadAllText(path + "\\acceptQuestData.json");
                List<Quest> acceptQuestLoadData = JsonConvert.DeserializeObject<List<Quest>>(acceptQuestLData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                if (GameManager.Instance.QM.acceptQuest == null)
                {
                    GameManager.Instance.QM.acceptQuest = new List<Quest>(); // 리스트를 초기화
                }
                foreach (Quest data in acceptQuestLoadData)
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
                    else if (choice == 1) 
                    { 
                        SaveData();
                        GameView.PrintText("저장 중입니다...", 1000,ConsoleColor.Cyan);
                    }
                    else if (choice == 2) 
                    { 
                        LoadData();
                        GameView.PrintText("불러오는 중입니다...", 1000, ConsoleColor.Cyan);
                    }
                }
                else
                {
                    GameView.PrintText("입력을 다시시도해 주세요", 800);
                }
            }
        }
    }
}
