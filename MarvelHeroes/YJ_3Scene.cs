using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelHeroes
{
    public class Quest
    {
        //임시용 클래스. 나중에 삭제
    }


    public abstract class Scene
    {
        public Dictionary<int, ISelections> sceneSelections;
        public Quest quest;

        public string Name { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
    }

    public class CreateCharacterScene : Scene
    {
        public CreateCharacterScene()
        {
            Name = "캐릭터 생성창";
            Description = "스파르타 던전에 오신 여러분 환영합니다.\r\n";
            Description2 = "원하시는 이름을 설정해주세요.";

            ISelections action1 = new Select_1_Class();
            ISelections action2 = new Select_2_Class();
            ISelections action3 = new Select_3_Class();
            ISelections action4 = new Select_4_Class();

            sceneSelections = new Dictionary<int, ISelections>() { { 1, action1 }, { 2, action2 }, { 3, action3 }, { 4, action4 } };
        }
    }

    public class TownScene : Scene//, ISelections
    {
        public TownScene()
        {
            Name = "마을";
            Description = "마을에선 다른 지역으로 이동할 수 있고 퀘스트를 수주할 수 있습니다.";

            ISelections UI = new ToUI(SceneNum.Town);
            ISelections action1 = new TalkToChief(quest);
            ISelections action2 = new Investigate(quest);
            ISelections action3 = new ToWhere(SceneNum.Dungeon);

            sceneSelections = new Dictionary<int, ISelections>() { { 0, UI }, { 1, action1 }, { 2, action2 }, { 3, action3 } };
        }
    }

    public class DungeonScene : Scene//, ISelections
    {
        int challengingFloor = 10;

        public DungeonScene()
        {
            int i = 1;

            Name = "던전";
            Description = "던전에선 더 높은 층으로 올라갈 수 있습니다.";
            Description2 = "도전하고 싶은 층을 입력해주세요.";

            ISelections UI = new ToUI(SceneNum.Dungeon);
            ISelections action1 = new ToWhere(SceneNum.Town);
            //ISelections action2 = new ToFloor();

            //sceneSelections = new Dictionary<int, ISelections>();

            sceneSelections = new Dictionary<int, ISelections>() { { 0, UI } };

            for (; i < challengingFloor; i++)
            {
                sceneSelections.Add(i, CloneToFloor(i, challengingFloor));
            }

            sceneSelections.Add(i, CloneToFloor(i++, challengingFloor));
            sceneSelections.Add( i, action1 );
        }

        public ToFloor CloneToFloor(int x, int y)
        {
            ToFloor clone = new ToFloor(x, y);
            return clone;
        }
    }

    //public class BattleScene : Scene
    //{
    //    int Floor;

    //    public BattleScene()
    //    {
    //        Name = "Battle";
    //        Description = "~~~~??아브라카다브라";
    //        Description2 = "FloorScene.디스크립션2";
    //    }
    //}
}

