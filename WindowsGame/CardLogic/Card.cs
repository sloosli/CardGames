using System;
using System.Resources;
using System.Drawing;
using WindowsGame.Properties;

namespace WindowsGame.CardLogic
{
    public class Card : IDisposable
    {
        private static string[] suits = { "Треф", "Бубен", "Черв", "Пики" };
        private static string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король", "Туз" };

        public readonly int Index;
        public readonly Bitmap Image;
        public int Value { get; private set; }


        public Card(int index)
        {
            Index = index;

            int value = index % 13;
            if (value <= 8)      value += 2;
            else if (value < 12) value = 10;
            else                 value = 11;
            Value = value;

            ResourceManager rm = Resources.ResourceManager;
            Image = (Bitmap)rm.GetObject("_" + index);
        }

        public bool DownGradeAce()
        {
            if (Value != 11)
                return false;

            Value = 1;
            return true;
        }

        public void Dispose()
        {
            Image.Dispose();
        }

        public override string ToString()
        {
            return ranks[Index % 13] + " " + suits[Index / 13];
        }

    }
}
