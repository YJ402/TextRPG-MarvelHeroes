namespace MarvelHeroes
{
    internal class Program
    {
        static void Main()
        {
            GameManager instance = GameManager.GetInstance();
            instance.GameStart();
        }
    }
}
