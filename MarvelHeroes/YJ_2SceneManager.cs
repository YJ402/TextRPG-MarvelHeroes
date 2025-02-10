using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public enum SceneNum
    {
        State = 1,
        Inventory,
        SaveLoad,
        Exit,

        Town = 10,
        Dungeon
    }

    public class SceneManager
    {

        static SceneManager _instance;
        private SceneManager() { }
        public static SceneManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SceneManager();
            }

            return _instance;
        }

        //static DungeonScene dungeonScene = new DungeonScene();
        //static DungeonScene dungeonScene = new DungeonScene();
        //static DungeonScene dungeonScene = new DungeonScene();
        //static DungeonScene dungeonScene = new DungeonScene();

        static TownScene townScene = new TownScene();
        static DungeonScene dungeonScene = new DungeonScene();
        public Scene currentScene;

        static public void ChangeCurrentScene(SceneNum sceneNum) // 매개변수 이넘타입으로 할까?
        {
            switch ((int)sceneNum)
            {
                //case 1: _instance.currentScene = ExitUI; break;
                //case 2: _instance.currentScene = StatusUI; break;
                //case 3: _instance.currentScene = dungeonScene; break;
                //case 4: _instance.currentScene = dungeonScene; break;

                case 10: _instance.currentScene = townScene; break;
                case 11: _instance.currentScene = dungeonScene; break;

            }
        }
    }
}
