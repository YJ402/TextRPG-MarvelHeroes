using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public enum SceneNum
    {
        Town = 1,
        Dungeon = 2,
        Shop = 3
    }

    public class SceneManager
    {
        public TownScene townScene { get; private set; }
        public DungeonScene dungeonScene { get; private set; }
        public ShopScene shopScene { get; private set; }
        public Scene currentScene;

        public SceneManager()
        {
            townScene = new TownScene();
            dungeonScene = new DungeonScene();
            shopScene = new ShopScene();
        }

        public void ChangeCurrentScene(SceneNum sceneNum) // 매개변수 이넘타입으로 할까?
        {
            switch ((int)sceneNum)
            {
                case 1: currentScene = townScene; break;
                case 2: currentScene = dungeonScene; break;
                case 3: currentScene = shopScene; break;
            }
        }
    }
}
