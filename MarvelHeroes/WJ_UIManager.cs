﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarvelHeroes.UIManager;

namespace MarvelHeroes
{

    public class UIManager
    {
        UIScene uiScene = new UIScene();
        public class UIScene : Scene
        {
            public UIScene()
            {
                Name = "UI";
                Description = "캐릭터 정보, 인벤토리, 저장, 종료를 할 수 있습니다";

                ISelections state = new ToUIBranch(SceneNum.State);
                ISelections inventory = new ToUIBranch(SceneNum.Inventory);
                ISelections saveLoad = new ToUIBranch(SceneNum.SaveLoad);
                ISelections exit = new ToUIBranch(SceneNum.Exit);

                sceneSelections = new Dictionary<int, ISelections>() { { 1, state }, { 2, inventory }, { 3, saveLoad }, { 4, exit } };
            }
        }

        public class ToUIBranch : ISelections
        {
            SceneNum UIBranch;

            public ToUIBranch(SceneNum i)
            {
                UIBranch = i;
            }

            public void Execute()
            {
                switch (UIBranch)
                {
                    case SceneNum.Inventory:
                        //InventoryUI inventoryUI = new InventoryUI();
                        //inventoryUI.InventoryScene();
                        break;
                    case SceneNum.SaveLoad:
                        SaveLoadUI saveLoadUI = new SaveLoadUI();
                        saveLoadUI.SaveLoadScene();
                        break;
                    case SceneNum.State:
                        StatusUI statusUI = new StatusUI(); // 상태창 class
                        statusUI.UIStateScene();
                        break;
                    case SceneNum.Exit:
                        ExitUI exitUI = new ExitUI(); // 상태창 class
                        exitUI.ExitScene();
                        break;
                }
            }
            public string GetSelectionDesc()
            {
                string sceneKorName = "";

                switch (UIBranch)
                {
                    case SceneNum.Inventory:
                        sceneKorName = "인벤토리";
                        break;
                    case SceneNum.SaveLoad:
                        sceneKorName = "저장/불러오기";
                        break;
                    case SceneNum.State:
                        sceneKorName = "캐릭터 상태";
                        break;
                    case SceneNum.Exit:
                        sceneKorName = "종료";
                        break;
                    defualt:
                        sceneKorName = "없는 장소";
                        break;
                }

                return $"{sceneKorName}(으)로 이동하기";
            }
        }

        //public class ToInven : ISelections
        //{
        //    public void Execute()
        //    {
        //       // InventoryUI inventoryUI = new InventoryUI(); // 인벤토리 class
        //       // inventoryUI.UIStateScene();
        //    }

        //    public string GetSelectionDesc()
        //    {
        //        return $"인벤토리(으)로 이동하기";
        //    }
        //}

        //public class ToSaveLoad : ISelections
        //{
        //    public void Execute()
        //    {
        //        SaveLoadUI saveLoadUI = new SaveLoadUI(); // 상태창 class
        //        saveLoadUI.SaveLoadScene();
        //    }

        //    public string GetSelectionDesc()
        //    {
        //        return $"저장 / 불러오기(으)로 이동하기";
        //    }
        //}

        //public class ToExit : ISelections
        //{
        //    public void Execute()
        //    {
        //        ExitUI exitUI = new ExitUI(); // 게임종료 class // 상태창 class
        //        exitUI.ExitScene();
        //    }

        //    public string GetSelectionDesc()
        //    {
        //        return $"게임 종료(으)로 이동하기";
        //    }
        //}

        public void  UIMainScene()
        {
            //int sceneTmep = temp; // UI 전 씬에서 씬의 정보를 저장함
            //Console.Clear();
            //Console.WriteLine("UI");
            //Console.WriteLine();
            //Console.WriteLine("캐릭터 정보, 인벤토리, 저장, 종료를 할 수 있습니다");
            //Console.WriteLine();
            //Console.WriteLine("1. 캐릭터 정보");
            //Console.WriteLine("2. 인벤토리");
            //Console.WriteLine("3. 게임 저장");
            //Console.WriteLine("4. 게임 종료");
            //Console.WriteLine();
            //Console.WriteLine("0. 나가기");
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("원하시는 행동을 입력해주세요");
            //Console.Write(">>");
            //while (true)
            //{
            //    if (int.TryParse(Console.ReadLine(), out int choice))
            //    {
            //        switch (choice)
            //        {
            //            case 1:
            //                // 캐릭터정보
            //                statusUI.UIStateScene(); // 씬의 정보를 잃지 않기 위해서 상태창 갈때도 보내주고 그대로 반환받음
            //                break;
            //            case 2:
            //                // 인벤토리
            //                break;
            //            case 3:
            //                // 게임저장
            //                break;
            //            case 4:
            //                // 게임종료
            //                break;
            //        }
            //        if (choice == 0) { break; } // 0일때 while문 break
            //    }
            //    else
            //    {
            //        Console.WriteLine("입력을 다시시도해 주세요"); // 오류처리
            //    }
            //}
            // return sceneTmep; // 저장한 씬 정보를 반환
            while (true)
            {
                Console.Clear();
                GameView.ViewSceneNameAndDesc1(uiScene);
                int temp = GameView.ViewGetSceneSelect2(uiScene);
                uiScene.sceneSelections[temp].Execute();
            }
        }
    }


}
