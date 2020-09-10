using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsGame.CardLogic;

namespace WindowsGame
{
    public partial class MainForm : Form
    {
        BlackJack game;
        Thread gameAccess;

        public MainForm()
        {
            InitializeComponent();
            StopAction();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            game = new BlackJack();

            game.DealerGetCardEvent += DealerGetCardHandler;
            game.PlayerGetCardEvent += PlayerGetCardHandler;
            game.EndOfGameResultEvent += ResultHandler;
        }

        private void DealerGetCardHandler(Card card)
        {
            textBoxDealerHand.Invoke((MethodInvoker)(() =>
            textBoxDealerHand.Text += card.ToString() + "  "));
        }

        private void PlayerGetCardHandler(Card card)
        {
            textBoxPlayerHand.Invoke((MethodInvoker)(() =>
            textBoxPlayerHand.Text += card.ToString() + "  "));
        }

        private void ResultHandler(string result)
        {
            textBoxResult.Invoke((MethodInvoker)(() =>
            textBoxResult.Text = result));
            this.Invoke((MethodInvoker)(() => StopAction()));
        }

        private void StartAction()
        {
            buttonStart.Enabled = false;
            buttonGetCard.Enabled = true;
            buttonStop.Enabled = true;
            buttonDouble.Enabled = true;

            textBoxDealerHand.Text = "";
            textBoxPlayerHand.Text = "";
            textBoxResult.Text = "";
        }

        private void StopAction()
        {
            buttonStart.Enabled = true;
            buttonGetCard.Enabled = false;
            buttonStop.Enabled = false;
            buttonDouble.Enabled = false;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (gameAccess != null && gameAccess.IsAlive)
                return;

            StartAction();

            gameAccess = new Thread(new ThreadStart(game.Start));
            gameAccess.Name = "[Доступ и игре] Start";
            gameAccess.Start();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            if (gameAccess != null && gameAccess.IsAlive)
                return;

            StopAction();

            gameAccess = new Thread(new ThreadStart(game.PlayerStop));
            gameAccess.Name = "[Доступ и игре] PlayerStop";
            gameAccess.Start();
        }

        private void ButtonGetCard_Click(object sender, EventArgs e)
        {
            if (gameAccess != null && gameAccess.IsAlive)
                return;

            gameAccess = new Thread(new ThreadStart(game.PlayerGetCard));
            gameAccess.Name = "[Доступ и игре] PlayerGetCard";
            gameAccess.Start();
        }
    }
}
