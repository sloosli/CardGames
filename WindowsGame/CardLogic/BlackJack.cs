using System;
using System.Collections.Generic;

namespace WindowsGame.CardLogic
{
    class BlackJackHand
    {
        private List<Card> cards;

        public BlackJackHand()
        {
            cards = new List<Card>();
        }

        public void Add(Card card)
        {
            cards.Add(card);
        }

        public int Count => cards.Count;

        public void Clear()
        {
            cards.Clear();
        }

        public int GetSum
        {
            get
            {
                int sum = 0;
                foreach (var item in cards)
                {
                    sum += item.Value;
                }
                return sum;
            }
        }

        public bool DownGradeAce()
        {
            foreach (var item in cards)
            {
                if (item.DownGradeAce())
                    return true;
            }
            return false;
        }
    }

    enum BlackJackResult
    {
        PlayerOver21,
        DealerOver21,
        PlayerHigher,
        DealerHigher,
        PlayerBlackJack,
        DealerBlackJack,
        Drawn
    }

    public class BlackJack
    {
        #region Events

        public delegate void NotifyHandler(string result);
        public delegate void GetCardHandler(Card card);

        public event NotifyHandler EndOfGameResultEvent;
        private void OnEndOfGameResult(string result)
        {
            EndOfGameResultEvent?.Invoke(result);
        }

        public event GetCardHandler DealerGetCardEvent;
        public event GetCardHandler PlayerGetCardEvent;
        private void OnDealerGetCard(Card card)
        {
            DealerGetCardEvent?.Invoke(card);
        }
        private void OnPlayerGetCard(Card card)
        {
            PlayerGetCardEvent?.Invoke(card);
        }

        #endregion

        private CardsDeck mainDeck;

        private BlackJackHand playerHand;
        private BlackJackHand dealerHand;

        public int PlayerSum => playerHand.GetSum;
        public int DealerSum => dealerHand.GetSum;

        public bool GameOn { get; private set; }

        public BlackJack()
        {
            mainDeck = new CardsDeck();
            playerHand = new BlackJackHand();
            dealerHand = new BlackJackHand();
        }

        public BlackJack(int amountOfDecks)
        {
            mainDeck = new CardsDeck(amountOfDecks);
            playerHand = new BlackJackHand();
            dealerHand = new BlackJackHand();
        }

        public void Start()
        {
            if (GameOn)
                return;

            GameOn = true;

            PlayerGetCard();
            DealerGetCard();
            PlayerGetCard();

            if (PlayerSum == 21) // BlackJack 
                PlayerStop();
        }

        //TODO: Эту гадость надо переписать что бы было более читаемо
        public void PlayerStop() 
        {
            if (!GameOn)
                return;

            GameOn = false;

            if (PlayerSum > 21) // Игрок перебрал
            {
                SetWin(BlackJackResult.PlayerOver21);
                return;
            }

            DealerGetCard(); // Дилер берет вторую карту

            if (PlayerSum == 21 && playerHand.Count == 2) // У игрока блэкджек
            {
                if (DealerSum == 21) // BJ на BJ - ничья
                    SetWin(BlackJackResult.Drawn);
                else                 // Игрок победил
                    SetWin(BlackJackResult.PlayerBlackJack);
                return;
            }
            if (DealerSum == 21) // У дилера блэкджек, у игрока уже нет
            {
                SetWin(BlackJackResult.DealerBlackJack);
                return;
            }

            while (DealerSum < 17) // Дилер добирает карты
            {
                DealerGetCard();
                if (DealerSum > 21 && !dealerHand.DownGradeAce()) // Дилер перебрал
                {
                    SetWin(BlackJackResult.DealerOver21);
                    return;
                }
            }
            if (DealerSum == PlayerSum) // Ничья
            {
                SetWin(BlackJackResult.Drawn);
                return;
            }
            if (DealerSum > PlayerSum) 
            {
                SetWin(BlackJackResult.DealerHigher);
                return;
            }
            if (DealerSum < PlayerSum)
            {
                SetWin(BlackJackResult.PlayerHigher);
                return;
            }
        } 

        public void PlayerGetCard()
        {
            if (!GameOn)
                return;

            Card card = mainDeck.GetCard();

            playerHand.Add(card);
            OnPlayerGetCard(card);

            if (PlayerSum >= 21 && !playerHand.DownGradeAce())
                PlayerStop();
        }

        private void DealerGetCard()
        {
            Card card = mainDeck.GetCard();

            dealerHand.Add(card);
            OnDealerGetCard(card);
        }
        
        private void SetWin(BlackJackResult result)
        {
            string text;
            switch (result)
            {
                case BlackJackResult.PlayerOver21:
                    {
                        text = "У вас перебор(" +
                               Convert.ToString(PlayerSum) +
                               "), вы проиграли";
                        break;
                    }
                case BlackJackResult.DealerBlackJack:
                    {
                        text = "У диллера блекджек, вы проиграли";
                        break;
                    }
                case BlackJackResult.DealerHigher:
                    {
                        text = "У диллера больше очков, вы проиграли";
                        break;
                    }
                case BlackJackResult.Drawn:
                    {
                        text = "Ничья";
                        break;
                    }
                case BlackJackResult.PlayerHigher:
                    {
                        text = "У вас больше очков, вы победили!!!";
                        break;
                    }
                case BlackJackResult.PlayerBlackJack:
                    {
                        text = "У вас блекджек, вы победили!!!";
                        break;
                    }
                case BlackJackResult.DealerOver21:
                    {
                        text = "У диллера перебор(" +
                               Convert.ToString(PlayerSum) +
                               "), вы победили!!!";
                        break;
                    }
                default:
                    {
                        text = "ERROR";
                        break;
                    }
            }

            dealerHand.Clear();
            playerHand.Clear();
            mainDeck.CheckDeks();
            OnEndOfGameResult(text);
        }
    }
}
