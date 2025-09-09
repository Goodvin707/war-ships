using System;

namespace War_Ships
{
    class Player
    {
        string name;
        Ship[] ships;
        string[,] field;
        bool[,] Forbiddenfield;
        int curX = 0, curY = 0;
        int score = 0;
        public int Score { get => score; }
        public string[,] Field { get => field; }
        public string Name { get => name; set => name = value; }
        public Player(bool fillByDefault)
        {
            // Создание кораблей
            ships = new Ship[10];
            ships[0] = new Ship(4);
            ships[1] = ships[2] = new Ship(3);
            ships[3] = ships[4]= ships[5] = new Ship(2);
            ships[6] = ships[7] = ships[8] = ships[9] = new Ship(1);
            // Создание игрового поля
            field = new string[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    field[i, j] = "~";
            // Создание вспомогательной матрицы
            Forbiddenfield = new bool[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    Forbiddenfield[i, j] = true;

            if (fillByDefault)
            {
                for (int shipIt = 0; shipIt < ships.Length; shipIt++)
                {
                    ships[shipIt].LocationIsHorizontal = true;
                    field[0, 0] = "[]";
                    field[0, 1] = "[]";
                    field[0, 2] = "[]";
                    field[0, 3] = "[]";

                    field[0, 5] = "[]";
                    field[0, 6] = "[]";
                    field[0, 7] = "[]";

                    field[2, 0] = "[]";
                    field[2, 1] = "[]";
                    field[2, 2] = "[]";

                    field[2, 4] = "[]";
                    field[2, 5] = "[]";

                    field[2, 7] = "[]";
                    field[2, 8] = "[]";

                    field[4, 0] = "[]";
                    field[4, 1] = "[]";

                    field[4, 3] = "[]";

                    field[4, 5] = "[]";

                    field[4, 7] = "[]";

                    field[4, 9] = "[]";
                }
            }
        }
        public void Display()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (field[i, j])
                    {
                        case "[]":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(field[i, j]);
                            break;
                        case "~":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "X":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "#":
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "*":
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(field[i, j] + " ");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void DisplayAndHideShips(string[,] field)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (field[i, j])
                    {
                        case "[]":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("~ ");
                            break;
                        case "~":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "X":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "#":
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(field[i, j] + " ");
                            break;
                        case "*":
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(field[i, j] + " ");
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
        public void GeneratePosition()
        {
            Random r = new Random();

            for (int shipIt = 0; shipIt < ships.Length; shipIt++)
            {
            toShipPositioning:
                switch (r.Next(1, 3))
                {
                    case 1:
                        ships[shipIt].LocationIsHorizontal = true;
                        break;
                    case 2:
                        ships[shipIt].LocationIsHorizontal = false;
                        break;
                }
                int x = r.Next(1, 11);
                int y = r.Next(1, 11);
                if (!ships[shipIt].CheckForContact(Forbiddenfield, --x, --y))
                    goto toShipPositioning;
                if (ships[shipIt].Deck == 1) // если однопалубный
                {
                    field[x, y] = "[]";
                    Forbiddenfield[x, y] = false;

                    try
                    { Forbiddenfield[x + 1, y] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x + 1, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x + 1, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                }
                else // если 2-4 палубный
                {
                    if (ships[shipIt].LocationIsHorizontal)
                    {
                        while (y + ships[shipIt].Deck > 10)
                            y--;
                        while (x >= 10)
                            x--;
                        for (int j = y; j < y + ships[shipIt].Deck; j++)
                        {
                            field[x, j] = "[]";
                            Forbiddenfield[x, j] = false;

                            try
                            {
                                Forbiddenfield[x + 1, j - 1] = false;
                                Forbiddenfield[x, j - 1] = false;
                                Forbiddenfield[x - 1, j - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x + 1, j - 1] = false;
                                Forbiddenfield[x + 1, j] = false;
                                Forbiddenfield[x + 1, j + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x + 1, j + 1] = false;
                                Forbiddenfield[x, j + 1] = false;
                                Forbiddenfield[x - 1, j + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x - 1, j + 1] = false;
                                Forbiddenfield[x - 1, j] = false;
                                Forbiddenfield[x - 1, j - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                    else
                    {
                        while (x + ships[shipIt].Deck > 10)
                            x--;
                        while (y >= 10)
                            y--;
                        for (int i = x; i < x + ships[shipIt].Deck; i++)
                        {
                            field[i, y] = "[]";
                            Forbiddenfield[i, y] = false;
                            try
                            { Forbiddenfield[i + 1, y - 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i, y - 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i - 1, y - 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i + 1, y - 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i + 1, y] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i + 1, y + 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i + 1, y + 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i, y + 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i - 1, y + 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i - 1, y + 1] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i - 1, y] = false; }
                            catch (IndexOutOfRangeException) { }
                            try
                            { Forbiddenfield[i - 1, y - 1] = false; }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                }
            }
        }
        public void PositioningPhaseByKeys()
        {
            string[] currentCells = new string[4];
            for (int shipIt = 0; shipIt < ships.Length; shipIt++)
            {
                bool neededCellChosen = false;
                while (!neededCellChosen)
                {
                    for (int i = 0; i < ships[shipIt].Deck; i++)
                    {
                        if (ships[shipIt].LocationIsHorizontal)
                        {
                            currentCells[i] = field[curX, curY + i];
                            field[curX, curY + i] = "[]";
                        }
                        else
                        {
                            currentCells[i] = field[curX + i, curY];
                            field[curX + i, curY] = "[]";
                        }
                    }

                    Display();
                    Console.WriteLine("Управление\n  " + (char)30 + "\n" + (char)17 + "   " + (char)16 + "\n  " + (char)31);
                    Console.WriteLine("R - поворот");
                    Console.WriteLine("Enter");
                again:
                    for (int i = 0; i < ships[shipIt].Deck; i++)
                    {
                        if (ships[shipIt].LocationIsHorizontal)
                            field[curX, curY + i] = currentCells[i];
                        else
                            field[curX + i, curY] = currentCells[i];
                    }
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (ships[shipIt].LocationIsHorizontal)
                                curX = curX <= 0 ? 9 : --curX;
                            else
                                curX = curX <= 0 ? 9 - ships[shipIt].Deck + 1 : --curX;
                            break;
                        case ConsoleKey.DownArrow:
                            if (ships[shipIt].LocationIsHorizontal)
                                curX = curX >= 9 ? 0 : ++curX;
                            else
                                curX = curX + ships[shipIt].Deck - 1 >= 9 ? 0 : ++curX;
                            break;
                        case ConsoleKey.LeftArrow:
                            if (ships[shipIt].LocationIsHorizontal)
                                curY = curY <= 0 ? 9 - ships[shipIt].Deck + 1 : --curY;
                            else
                                curY = curY <= 0 ? 9 : --curY;
                            break;
                        case ConsoleKey.RightArrow:
                            if (ships[shipIt].LocationIsHorizontal)
                                curY = curY + ships[shipIt].Deck - 1 >= 9 ? 0 : ++curY;
                            else
                                curY = curY >= 9 ? 0 : ++curY;
                            break;
                        case ConsoleKey.R:
                            ships[shipIt].LocationIsHorizontal = !ships[shipIt].LocationIsHorizontal;
                            break;
                        case ConsoleKey.Enter:
                            if (!ships[shipIt].CheckForContact(Forbiddenfield, curX, curY))
                                goto again;
                            for (int i = 0; i < ships[shipIt].Deck; i++)
                            {
                                if (ships[shipIt].LocationIsHorizontal)
                                {
                                    field[curX, curY + i] = "[]";
                                    Forbiddenfield[curX, curY + i] = false;
                                    try
                                    {
                                        Forbiddenfield[curX + 1, curY + i - 1] = false;
                                        Forbiddenfield[curX, curY + i - 1] = false;
                                        Forbiddenfield[curX - 1, curY + i - 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX + 1, curY + i - 1] = false;
                                        Forbiddenfield[curX + 1, curY + i] = false;
                                        Forbiddenfield[curX + 1, curY + i + 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX + 1, curY + i + 1] = false;
                                        Forbiddenfield[curX, curY + i + 1] = false;
                                        Forbiddenfield[curX - 1, curY + i + 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX - 1, curY + i + 1] = false;
                                        Forbiddenfield[curX - 1, curY + i] = false;
                                        Forbiddenfield[curX - 1, curY + i - 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                }
                                else
                                {
                                    field[curX + i, curY] = "[]";
                                    Forbiddenfield[curX + i, curY] = false;
                                    try
                                    {
                                        Forbiddenfield[curX + i + 1, curY - 1] = false;
                                        Forbiddenfield[curX + i, curY - 1] = false;
                                        Forbiddenfield[curX + i - 1, curY - 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX + i + 1, curY - 1] = false;
                                        Forbiddenfield[curX + i + 1, curY] = false;
                                        Forbiddenfield[curX + i + 1, curY + 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX + i + 1, curY + 1] = false;
                                        Forbiddenfield[curX + i, curY + 1] = false;
                                        Forbiddenfield[curX + i - 1, curY + 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                    try
                                    {
                                        Forbiddenfield[curX + i - 1, curY + 1] = false;
                                        Forbiddenfield[curX + i - 1, curY] = false;
                                        Forbiddenfield[curX + i - 1, curY - 1] = false;
                                    }
                                    catch (IndexOutOfRangeException) { }
                                }
                            }
                            neededCellChosen = true;
                            break;
                        default:
                            goto again;
                    }
                    Console.Clear();
                }
            }
        }
        public void PositioningPhaseByInputCoordinates()
        {
        again:
            Console.WriteLine("Сгенерировать корабли? (Y - да)");
            bool needGen = Console.ReadKey().Key == ConsoleKey.Y;
            Random r = new Random();

            for (int shipIt = 0; shipIt < ships.Length; shipIt++)
            {
            toShipPositioning:
                Console.Clear();
                Display();
                System.Threading.Thread.Sleep(200);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(needGen? "" : "Расположение\n1. Горизонтально\n2. Вертикально");
                switch (needGen ? r.Next(1, 3) : int.Parse(Console.ReadLine()))
                {
                    case 1:
                        ships[shipIt].LocationIsHorizontal = true;
                        break;
                    case 2:
                        ships[shipIt].LocationIsHorizontal = false;
                        break;
                }
                Console.WriteLine(needGen? "" : "Куда ставим?");
                int x = needGen ? r.Next(1, 11) : int.Parse(Console.ReadLine());
                int y = needGen ? r.Next(1, 11) : int.Parse(Console.ReadLine());
                if (!ships[shipIt].CheckForContact(Forbiddenfield, --x, --y))
                    goto toShipPositioning;
                if (ships[shipIt].Deck == 1) // если однопалубный
                {
                    field[x, y] = "[]";
                    Forbiddenfield[x, y] = false;

                    try
                    { Forbiddenfield[x + 1, y] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x + 1, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x + 1, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y + 1] = false; }
                    catch (IndexOutOfRangeException) { }
                    try
                    { Forbiddenfield[x - 1, y - 1] = false; }
                    catch (IndexOutOfRangeException) { }
                }
                else // если 2-4 палубный
                {
                    if (ships[shipIt].LocationIsHorizontal)
                    {
                        while (y + ships[shipIt].Deck > 10)
                            y--;
                        while (x >= 10)
                            x--;
                        for (int j = y; j < y + ships[shipIt].Deck; j++)
                        {
                            field[x, j] = "[]";
                            Forbiddenfield[x, j] = false;

                            try
                            {
                                Forbiddenfield[x + 1, j - 1] = false;
                                Forbiddenfield[x, j - 1] = false;
                                Forbiddenfield[x - 1, j - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x + 1, j - 1] = false;
                                Forbiddenfield[x + 1, j] = false;
                                Forbiddenfield[x + 1, j + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x + 1, j + 1] = false;
                                Forbiddenfield[x, j + 1] = false;
                                Forbiddenfield[x - 1, j + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[x - 1, j + 1] = false;
                                Forbiddenfield[x - 1, j] = false;
                                Forbiddenfield[x - 1, j - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                    else
                    {
                        while (x + ships[shipIt].Deck > 10)
                            x--;
                        while (y >= 10)
                            y--;
                        for (int i = x; i < x + ships[shipIt].Deck; i++)
                        {
                            field[i, y] = "[]";
                            Forbiddenfield[i, y] = false;

                            try
                            {
                                Forbiddenfield[i + 1, y - 1] = false;
                                Forbiddenfield[i, y - 1] = false;
                                Forbiddenfield[i - 1, y - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[i + 1, y - 1] = false;
                                Forbiddenfield[i + 1, y] = false;
                                Forbiddenfield[i + 1, y + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[i + 1, y + 1] = false;
                                Forbiddenfield[i, y + 1] = false;
                                Forbiddenfield[i - 1, y + 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                            try
                            {
                                Forbiddenfield[i - 1, y + 1] = false;
                                Forbiddenfield[i - 1, y] = false;
                                Forbiddenfield[i - 1, y - 1] = false;
                            }
                            catch (IndexOutOfRangeException) { }
                        }
                    }
                }
            }
            Console.Clear();
            Display();
            Console.WriteLine("Норм? (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.N)
            {
                // Очистка игрового поля
                field = new string[10, 10];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                        field[i, j] = "~";
                // Очистка вспомогательной матрицы
                Forbiddenfield = new bool[10, 10];
                for (int i = 0; i < 10; i++)
                    for (int j = 0; j < 10; j++)
                        Forbiddenfield[i, j] = true;
                Console.Clear();
                goto again;
            }
            Console.Clear();
        }
        public void AttackPhase(string[,] enemyField, bool playerOne)
        {
            string currentCell = enemyField[curX, curY];
            enemyField[curX, curY] = "X";

            bool neededCellChosen = false;
            while (!neededCellChosen)
            {
                Console.Clear();
                if (playerOne)
                {
                    DisplayAndHideShips(field);
                    Console.WriteLine("\n-------------------\n");
                    DisplayAndHideShips(enemyField);
                }
                else
                {
                    DisplayAndHideShips(enemyField);
                    Console.WriteLine("\n-------------------\n");
                    DisplayAndHideShips(field);
                }

                Console.WriteLine("Управление\n  " + (char)30 + "\n" + (char)17 + "   " + (char)16 + "\n  " + (char)31);
                Console.WriteLine("Enter");
            again:
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX <= 0 ? curX = 9 : --curX, curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.DownArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX >= 9 ? curX = 0 : ++curX, curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.LeftArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX, curY <= 0 ? curY = 9 : --curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.RightArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX, curY >= 9 ? curY = 0 : ++curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.Enter:
                        enemyField[curX, curY] = currentCell;
                        switch (enemyField[curX, curY])
                        {
                            case "[]":
                                enemyField[curX, curY] = "#";
                                currentCell = enemyField[curX, curY];
                                enemyField[curX, curY] = "X";
                                score++;
                                if (score == 20)
                                    break;
                                continue;
                            case "~":
                                enemyField[curX, curY] = "*";
                                break;
                            case "#":
                                goto again;
                            case "*":
                                goto again;
                        }
                        neededCellChosen = true;
                        break;
                    default:
                        goto again;
                }
            }
        }
        public void AttackPhaseOnBot(string[,] enemyField)
        {
            string currentCell = enemyField[curX, curY];
            enemyField[curX, curY] = "X";

            bool neededCellChosen = false;
            while (!neededCellChosen)
            {
                Console.Clear();
                Display();
                Console.WriteLine("\n-------------------\n");
                DisplayAndHideShips(enemyField);

                Console.WriteLine("Управление\n  " + (char)30 + "\n" + (char)17 + "   " + (char)16 + "\n  " + (char)31);
                Console.WriteLine("Enter");
            again:
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX <= 0 ? curX = 9 : --curX, curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.DownArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX >= 9 ? curX = 0 : ++curX, curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.LeftArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX, curY <= 0 ? curY = 9 : --curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.RightArrow:
                        enemyField[curX, curY] = currentCell;
                        currentCell = enemyField[curX, curY >= 9 ? curY = 0 : ++curY];
                        enemyField[curX, curY] = "X";
                        break;
                    case ConsoleKey.Enter:
                        enemyField[curX, curY] = currentCell;
                        switch (enemyField[curX, curY])
                        {
                            case "[]":
                                enemyField[curX, curY] = "#";
                                currentCell = enemyField[curX, curY];
                                enemyField[curX, curY] = "X";
                                score++;
                                if (score == 20)
                                    break;
                                continue;
                            case "~":
                                enemyField[curX, curY] = "*";
                                break;
                            case "#":
                                goto again;
                            case "*":
                                goto again;
                        }
                        neededCellChosen = true;
                        break;
                    default:
                        goto again;
                }
            }
        }
        public void BotAttackPhase(string[,] enemyField)
        {
        again:
            int x = new Random().Next(0, 10);
            int y = new Random().Next(0, 10);
            switch (enemyField[x, y])
            {
                case "[]":
                    enemyField[x, y] = "#";
                    score++;
                    if (score == 20)
                        break;
                    goto again;
                case "~":
                    enemyField[x, y] = "*";
                    break;
                case "#":
                    goto again;
                case "*":
                    goto again;
            }
        }
        public void SetName()
        {
            Console.Write($"{Name}, введите свой ник: ");
            string s = Console.ReadLine();
            Name = s == "" ? Name : s;
        }
    }
}