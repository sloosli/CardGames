using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        TextBox[] playersBoxes;
        CardAnimation animation;

        public MainForm()
        {
            InitializeComponent();
            StopAction();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            game = new BlackJack();

            game.PlayerGetCardEvent += PlayerGetCardHandler;
            game.EndOfGameResultEvent += ResultHandler;

            this.playersBoxes = new TextBox[] { textBoxDealerHand, textBoxPlayerHand };
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Debug.WriteLine("Вызов методa MainForm_Paint");
            if (animation != null && animation.IsPlayed)
            {
                g.DrawImageUnscaled(animation.card.Image, animation.Position);
                animation.Move();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameAccess.Abort();
        }

        private void PlayerGetCardHandler(GetCardEventArgs args)
        {
            // Данный метод вызывается в потоке gameAccess, так что что бы обратиться к полям формы
            // необходимо вызывать метод SetNewCard асинхронно в родительском потоке поля
            // Но также нам необходимо заблокировать текущий поток, что бы игра не продолжалась,
            // пока не закончится обработка текущего события

            var result = this.BeginInvoke(new GetCardHandler(SetNewCard), args);
            this.EndInvoke(result);
            while (animation != null && animation.IsPlayed) ;
        }

        private void SetNewCard(GetCardEventArgs args)
        {
            playersBoxes[args.PlayerId].Text += args.NewCard.ToString() + "  ";
            textBoxCardRemain.Text = args.CardInDeck.ToString();
            animation = new CardAnimation(args.NewCard, 400, 400);
        
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

        private void formRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }

    class CardAnimation
    {
        private const double AnimationTime = 500;
        private const int xStart = 0,
                          yStart = 0;

        public readonly Card card;
        private double[] position;
        private readonly double[] destination;
        private readonly double[] coordSpeed;
        private DateTime prevMoveTime;


        public bool IsPlayed { get; private set; }
        public Point Position => new Point((int)position[0], (int)position[1]);

        public CardAnimation(Card card, int x, int y)
        {
            this.card = card;
            this.destination = new double[] { x, y };
            this.position = new double[] { xStart, yStart };
            this.coordSpeed = new double[] { 
                (x - xStart) / AnimationTime,
                (y - yStart) / AnimationTime};
            IsPlayed = true;
            prevMoveTime = DateTime.Now;
        }

        public void Move()
        {
            if (!IsPlayed)
                return;
            DateTime curMoveTime = DateTime.Now;
            TimeSpan elapsedTime = curMoveTime - prevMoveTime;
            Debug.WriteLine(elapsedTime);
            prevMoveTime = curMoveTime;
            this.position[0] += (coordSpeed[0] * elapsedTime.TotalMilliseconds);
            this.position[1] += (coordSpeed[1] * elapsedTime.TotalMilliseconds);
            if ((destination[0] - position[0] > 0) != (coordSpeed[0] > 0))
                position[0] = destination[0];
            if ((destination[1] - position[1] > 0) != (coordSpeed[1] > 0))
                position[1] = destination[1];
            if (position[0] == destination[0] && position[1] == destination[1])
                IsPlayed = false;
        }
    }
}
