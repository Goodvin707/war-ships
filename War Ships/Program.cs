using System;

namespace War_Ships
{
    class Program
    {
        static void SinglePlayer()
        {
            Console.Clear();
            Random r = new Random();
            Player One = new Player(false);
            One.PositioningPhaseByKeys();
            Player Two = new Player(false) { Name = "Бот" };
            Two.GeneratePosition();
            Console.Clear();

            bool playerOne = r.Next(0, 101) >= 50;
            while (true)
            {
                if (playerOne)
                {
                    One.AttackPhaseOnBot(Two.Field);
                    playerOne = false;
                }
                else
                {
                    Two.BotAttackPhase(One.Field);
                    playerOne = true;
                }
                if (One.Score == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\n☺ Вы выиграли ☻");
                    break;
                }
                if (Two.Score == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n\nВы проиграли :(");
                    break;
                }
            }
        }
        static void Online()
        {

        }
        static void HotSeat()
        {
            Console.Clear();
            Random r = new Random();
            Player One = new Player(false) { Name = "Игрок 1" };
            // One.SetName();
            One.PositioningPhaseByKeys();
            Player Two = new Player(false) { Name = "Игрок 2" };
            // Two.SetName();
            Two.PositioningPhaseByKeys();
            Console.Clear();

            bool playerOne = r.Next(0, 101) >= 50;
            while (true)
            {
                if (playerOne)
                {
                    One.AttackPhase(Two.Field, playerOne);
                    playerOne = false;
                }
                else
                {
                    Two.AttackPhase(One.Field, playerOne);
                    playerOne = true;
                }
                if (One.Score == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\n☺ Выиграл игрок {One.Name} ☻");
                    break;
                }
                if (Two.Score == 20)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\n☺ Выиграл игрок {Two.Name} ☻");
                    break;
                }
            }
        }
        static void Settings()
        {

        }
        static void Main()
        {
            /*
             * 1. Сделать закрашивание области вокруг убитого корабля
             */

            Console.SetWindowSize(40, 40);

            while(true)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("╔╗╔╗╔╗╔══╗╔═══╗───╔══╗╔╗╔╗╔══╗╔═══╗╔══╗\n"
                              + "║║║║║║║╔╗║║╔═╗║───║╔═╝║║║║╚╗╔╝║╔═╗║║╔═╝\n"
                              + "║║║║║║║╚╝║║╚═╝║───║╚═╗║╚╝║─║║─║╚═╝║║╚═╗\n"
                              + "║║║║║║║╔╗║║╔╗╔╝───╚═╗║║╔╗║─║║─║╔══╝╚═╗║\n"
                              + "║╚╝╚╝║║║║║║║║║────╔═╝║║║║║╔╝╚╗║║───╔═╝║\n"
                              + "╚═╝╚═╝╚╝╚╝╚╝╚╝────╚══╝╚╝╚╝╚══╝╚╝───╚══╝");
                Console.WriteLine();

                Console.WriteLine("1. Одиночная игра");
                Console.WriteLine("2. Хот-сит");
                Console.WriteLine("3. Настройки");
                Console.WriteLine("4. Выход");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        SinglePlayer();
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        HotSeat();
                        Console.WriteLine("Нажмите любую клавишу для продолжения");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        Settings();
                        break;
                    case ConsoleKey.D4:
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                        break;
                }
            }
        }
    }
}