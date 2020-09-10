using System;
using System.Collections.Generic;

namespace CardLogic
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

        public int GetSum()
        {
            int sum = 0;
            foreach (var item in cards)
            {
                sum += item.Value;
            }
            return sum;
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
        public void OnEndOfGameResult(string result)
        {
            EndOfGameResultEvent?.Invoke(result);
        }

        public event GetCardHandler DealerGetCardEvent;
        public event GetCardHandler PlayerGetCardEvent;
        public void OnDealerGetCard(Card card)
        {
            var resObj = DealerGetCardEvent?.BeginInvoke(card, new AsyncCallback(AsyncResultHandler), "Дилер взял карту " + card);
            DealerGetCardEvent?.EndInvoke(resObj);
        }
        public void OnPlayerGetCard(Card card)
        {
            var resObj = PlayerGetCardEvent?.BeginInvoke(card, new AsyncCallback(AsyncResultHandler), "Игрок взял карту " + card);
            PlayerGetCardEvent?.EndInvoke(resObj);
        }

        public void AsyncResultHandler(IAsyncResult resObj) { }

        #endregion

        private CardsDeck mainDeck;

        private BlackJackHand playerDeck;
        private BlackJackHand dealerDeck;
        private Card dealerHidden;

        public int PlayerSum => playerDeck.GetSum();
        public int DealerSum => dealerDeck.GetSum();

        public bool GameOn { get; private set; }

        public BlackJack()
        {
            mainDeck = new CardsDeck();
            playerDeck = new BlackJackHand();
            dealerDeck = new BlackJackHand();
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

            if (PlayerSum == 21 && playerDeck.Count == 2) // У игрока блэкджек
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
            }
            if (DealerSum > 21) // Дилер перебрал
            {
                SetWin(BlackJackResult.DealerOver21);
                return;
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
            Card card = mainDeck.GetCard();

            playerDeck.Add(card);
            OnPlayerGetCard(card);

            if (PlayerSum >= 21 && !playerDeck.DownGradeAce())
                PlayerStop();
        }

        private void DealerGetCard()
        {
            Card card = mainDeck.GetCard();

            playerDeck.Add(card);
            OnPlayerGetCard(card);
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
            OnEndOfGameResult(text);
        }
    }
}
