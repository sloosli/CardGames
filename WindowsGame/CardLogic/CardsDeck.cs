using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace WindowsGame.CardLogic
{
    class CardsDeck
    {
        private const int amountOfCards = 52;

        private readonly int amountOfDecks;
        private Queue<Card> playedDeck;
        private Queue<Card> usedDeck;
        private List<Card> inGameDesk;

        public int RemainCards => playedDeck.Count;

        public int Length => playedDeck.Count;

        public CardsDeck(int amountOfDecks = 3)
        {
            if (amountOfDecks > 8)
            {
                amountOfDecks = 8;
            }
            this.amountOfDecks = amountOfDecks;

            playedDeck = new Queue<Card>();
            usedDeck = new Queue<Card>();
            inGameDesk = new List<Card>();

            for (int i = 0; i < amountOfCards * this.amountOfDecks; i++)
            {
                Card card = new Card(i % amountOfCards);
                playedDeck.Enqueue(card);
            }

            Shuffle();
            Shuffle();
            Shuffle();
        }

        private void Shuffle()
        {
            Card[] newDeck = new Card[amountOfCards * amountOfDecks];
            int i = 0;
            Random rnd = new Random();

            while(usedDeck.Count != 0)
            {
                newDeck[i] = usedDeck.Dequeue();
                i++;
            }

            while(playedDeck.Count != 0)
            {
                newDeck[i] = playedDeck.Dequeue();
                i++;
            }

            for (int j = 0; j < newDeck.Length; j++)
            {
                i = rnd.Next(0, newDeck.Length - 1);
                Card temp = newDeck[0];
                newDeck[0] = newDeck[i];
                newDeck[i] = temp;
            }

            playedDeck = new Queue<Card>(newDeck);
        }

        public void CheckDeks()
        {
            if (playedDeck.Count < usedDeck.Count / 2)
            {
                Shuffle();
            }
        }

        public Card GetCard()
        {
            if (playedDeck.Count == 0)
                return null;
            Card card = playedDeck.Dequeue();
            inGameDesk.Add(card);
            return card;
        }

        public void ReturnCard(Card card)
        {
            if (inGameDesk.Contains(card))
            {
                inGameDesk.Remove(card);
                usedDeck.Enqueue(card);
            }
        }
    }
}
