using System;
using System.Collections.Generic;
using System.Text;

namespace War_Ships
{
    class Ship
    {
        byte deck; // 1 - 4
        public byte Deck
        {
            get => deck;
            set => deck = value;
        }
        bool location = true; // t - гор. f - верт.
        public bool LocationIsHorizontal
        {
            get => location;
            set => location = value;
        }
        public Ship(byte deck) => this.deck = deck;
        public bool CheckForContact(bool[,] Forbiddenfield, int x, int y)
        {
            if (location) // Горизонтально
            {
                while (y + deck > 10)
                    y--;
                while (x >= 10)
                    x--;
                for (int i = y; i < y + deck; i++)
                    if (Forbiddenfield[x, i] == false)
                        return false;
            }
            else // Вертикально
            {
                while (x + deck > 10)
                    x--;
                while (y >= 10)
                    y--;
                for (int i = x; i < x + deck; i++)
                    if (Forbiddenfield[i, y] == false)
                        return false;
            }
            return true;
        }
    }
}