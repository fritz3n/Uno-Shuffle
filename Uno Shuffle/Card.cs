using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle
{
    public class Card
    {
        public Suit Suit { get; private set; }
        public Color Color { get; set; }

        public Card(Suit suit, Color color)
        {
            this.Suit = suit;
            this.Color = color;

            if ((suit == Suit.Plus4 || suit == Suit.Pick) && color != Color.Nigro)
                throw new ArgumentException("Plus4 or Pick suits need to be Color Nigro");

            if (!(suit != Suit.Plus4 || suit != Suit.Pick) && color == Color.Nigro)
                throw new ArgumentException("Only Plus4 or Pick suits can be Color Nigro");
        }

        public bool CanBePlaceOn(Card secondCard)
        {
            if((this.Color == Color.Nigro || secondCard.Color == this.Color) && this.Suit != Suit.Pick && secondCard.Suit != Suit.Pick)
            {
                return true;
            }

            if (this.Color == secondCard.Color)
            {
                return true;
            }

            if (this.Suit == secondCard.Suit)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return Color + ": " + Suit;
        }

        public void WriteToConsole()
        {
            //Set the colors to the cards color
            switch (this.Color)
            {
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Color.Nigro:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                default:
                    throw new NotImplementedException(this.Color.ToString());
            }
            Console.WriteLine(Suit.ToString());
            //Reset the colors
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public enum Suit
    {
        N0,
        N1,
        N2,
        N3,
        N4,
        N5,
        N6,
        N7,
        N8,
        N9,
        Skip,
        Reverse,
        Plus2,
        Plus4,
        Pick,
    }

    public enum Color
    {
        Nigro,
        Red,
        Green,
        Blue,
        Yellow
    }
}
