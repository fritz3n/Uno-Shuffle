using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle
{
    class Card
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
