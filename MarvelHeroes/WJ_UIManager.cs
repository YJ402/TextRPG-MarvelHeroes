using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class WJ_UIManager
    {
        WJ_StatusUI statusUI = new WJ_StatusUI(); // 상태창 class
        WJ_InventoryUI inventoryUI = new WJ_InventoryUI(); // 인벤토리 class
        WJ_SaveLoadUI saveLoadUI = new WJ_SaveLoadUI(); // save/Load class
        WJ_ExitUI exitUI = new WJ_ExitUI(); // 게임종료 class

        public int UIMainScene(int temp)
        {
            int sceneTemp = temp; // UI 전 씬에서 씬의 정보를 저장함
            Console.Clear();
            Console.WriteLine("UI");
            Console.WriteLine();
            Console.WriteLine("캐릭터 정보, 인벤토리, 저장, 종료를 할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("1. 캐릭터 정보");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 게임 저장");
            Console.WriteLine("4. 게임 종료");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            // 캐릭터정보
                            sceneTemp = statusUI.UIStateScene(sceneTemp); // 씬의 정보를 잃지 않기 위해서 상태창 갈때도 보내주고 그대로 반환받음
                            break;
                        case 2:
                            // 인벤토리
                            sceneTemp = inventoryUI.InventoryScene(sceneTemp);
                            break;
                        case 3:
                            // 게임저장
                            break;
                        case 4:
                            // 게임종료
                            break;
                    }
                    if (choice == 0) { break; } // 0일때 while문 break
                }
                else
                {
                    Console.WriteLine("입력을 다시시도해 주세요"); // 오류처리
                }
            }
            return sceneTemp; // 저장한 씬 정보를 반환
        }
    }


}
