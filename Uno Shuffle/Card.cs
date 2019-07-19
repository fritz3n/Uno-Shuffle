using System;
using System.Collections.Generic;
using System.Text;

namespace Uno_Shuffle
{
    struct Card
    {
        public Suit Suit { get; private set; }
        public Color Color { get; private set; }

        public Card(Suit suit, Color color)
        {
            this.Suit = suit;
            this.Color = color;

            if ((suit == Suit.Plus4 || suit == Suit.Pick) && color != Color.Nigro)
                throw new ArgumentException("Plus4 or Pick suits need to be Color Nigro");

            if ((suit != Suit.Plus4 || suit != Suit.Pick) && color == Color.Nigro)
                throw new ArgumentException("Only Plus4 or Pick suits can be Color Nigro");
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
