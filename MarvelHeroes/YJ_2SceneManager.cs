using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    enum SceneNum
    {
        Town = 1,
        Dungeon = 2
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

        static TownScene townScene = new TownScene();
        static DungeonScene dungeonScene = new DungeonScene();
        public Scene currentScene;

        static public void ChangeCurrentScene(string sceneName)
        {
            SceneNum temp = (SceneNum)Enum.Parse(typeof(SceneNum), sceneName);

            switch ((int)temp)
            {
                case 1: _instance.currentScene = townScene; break;
                case 2: _instance.currentScene = dungeonScene; break;
            }
        }
    }
}
