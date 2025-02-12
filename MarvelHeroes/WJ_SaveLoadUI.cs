
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace MarvelHeroes
{
    internal class SaveLoadUI
    {
        public void SaveLoadScene()
        {
            while (true)
            {
                Console.Clear();
                GameView.PrintText("게임 저장/불러오기", 0, ConsoleColor.Magenta);
                Console.WriteLine();
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
                    else if (choice == 1) SaveTextRPG(GameManager.Instance.player, GameManager.Instance.inventory);
                    else if (choice == 2)
                    {
                       GameData gameData = LoadTextRPG();
                       GameManager.Instance.player = gameData.SavaPlayer;
                    }                  
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요");
                }
            }
        }

        public void SaveTextRPG(Player player, Inventory inventory, string filename = "MarbleSaveTextRPG.json")
        {
            GameData data = new GameData() { SavaPlayer = player };

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filename, json);
            Console.WriteLine("게임이 저장되었습니다.");
        }

        public GameData LoadTextRPG(string filename = "MarbleSaveTextRPG.json")
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("저장된 데이터가 없습니다.");
                return new GameData { SavaPlayer = new Player()};
            }
            else
            {
                string json = File.ReadAllText(filename);
                GameData data = JsonSerializer.Deserialize<GameData>(json);

                Console.WriteLine("저장된 데이터를 불러왔습니다.");
                return data;
            }

        }

    }

    public class GameData
    {
        // 기본값 설정
        public Player SavaPlayer { get; set; } = new Player();

        public GameData() { }
    }
}
