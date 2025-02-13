using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarvelHeroes.UIManager;

namespace MarvelHeroes
{
    public enum UINum
    {
        State,
        Inventory,
        SaveLoad,
        Exit,
        OutUI
    }
    public class UIManager
    {
        UIScene uiScene = new UIScene();
        public class UIScene : Scene
        {
            public UIScene()
            {
                Name = "UI";
                Description = "캐릭터 정보, 인벤토리, 저장, 종료를 할 수 있습니다";

                ISelections state = new ToUIBranch(UINum.State);
                ISelections inventory = new ToUIBranch(UINum.Inventory);
                ISelections saveLoad = new ToUIBranch(UINum.SaveLoad);
                ISelections exit = new ToUIBranch(UINum.Exit);
                ISelections outUI = new ToUIBranch(UINum.OutUI);


                sceneSelections = new Dictionary<int, ISelections>() { { 1, state }, { 2, inventory }, { 3, saveLoad }, { 4, exit }, { 5, outUI } };
            }
        }
        public class ToUIBranch : ISelections
        {
            UINum UIBranch;

            public ToUIBranch(UINum i)
            {
                UIBranch = i;
            }

            public void Execute()
            {
                switch (UIBranch)
                {
                    case UINum.Inventory:
                        InventoryUI inventoryUI = new InventoryUI(); // 인벤토리ui class
                        inventoryUI.InventoryScene();
                        break;
                    case UINum.SaveLoad:
                        SaveLoadUI saveLoadUI = new SaveLoadUI(); // 저장 불러오기ui class
                        saveLoadUI.SaveLoadScene();
                        break;
                    case UINum.State:
                        StatusUI statusUI = new StatusUI(); // 상태창ui class
                        statusUI.UIStateScene();
                        break;
                    case UINum.Exit:
                        ExitUI exitUI = new ExitUI(); // 게임종료 class
                        exitUI.ExitScene();
                        break;
                    case UINum.OutUI: // ui종료
                        break;
                }
            }
            public string GetSelectionDesc()
            {
                string sceneKorName = "";

                switch (UIBranch)
                {
                    case UINum.Inventory:
                        sceneKorName = "인벤토리";
                        break;
                    case UINum.SaveLoad:
                        sceneKorName = "저장/불러오기";
                        break;
                    case UINum.State:
                        sceneKorName = "캐릭터 상태";
                        break;
                    case UINum.Exit:
                        sceneKorName = "게임 종료";
                        break;
                    case UINum.OutUI:
                        sceneKorName = "UI 종료";
                        break;
                        defualt:
                        sceneKorName = "없는 장소";
                        break;
                }

                return $"{sceneKorName}(으)로 이동하기";
            }
        }


        public void UIMainScene()
        {
            while (true)
            {
                Console.Clear();
                GameView.ViewSceneNameAndDesc1(uiScene);
                int temp = GameView.ViewGetSceneSelect2(uiScene);
                uiScene.sceneSelections[temp].Execute();
                if (temp == 5) // 5일때 while문 break
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }


}
