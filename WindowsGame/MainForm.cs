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
        readonly LinkedList<CardAnimation> animations = new LinkedList<CardAnimation>();

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint, true);
            UpdateStyles();

            StopAction();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            game = new BlackJack();

            game.PlayerGetCardEvent += PlayerGetCardHandler;
            game.EndOfGameResultEvent += ResultHandler;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var animation in animations)
            {
                g.DrawImageUnscaled(animation.card.Image, animation.Position);
                if(animation.IsPlayed)
                    animation.Move();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gameAccess?.Abort();
        }

        private void PlayerGetCardHandler(GetCardEventArgs args)
        {
            // Данный метод вызывается в потоке gameAccess, так что что бы обратиться к полям формы
            // необходимо вызывать метод SetNewCard асинхронно в родительском потоке поля
            // Но также нам необходимо заблокировать текущий поток, что бы игра не продолжалась,
            // пока не закончится обработка текущего события

            var result = this.BeginInvoke(new GetCardHandler(SetNewCard), args);
            this.EndInvoke(result);
            while (animations.Last() != null && animations.Last().IsPlayed) ;
        }

        private void SetNewCard(GetCardEventArgs args)
        {
            cardCounterLabel.Text = args.CardInDeck.ToString();
            int destX = args.PlayerCards.Length * 40,
                destY = args.PlayerId == 0 ? 40 : this.Size.Height - 230;
            animations.AddLast(new CardAnimation(args.NewCard, destX, destY));        
        }

        private void ResultHandler(string result)
        {
            this.Invoke(new MethodInvoker(StopAction));
            MessageBox.Show(result, "Результат", MessageBoxButtons.OK);
            animations.Clear();
        }

        private void StartAction()
        {
            buttonStart.Enabled = false;
            buttonGetCard.Enabled = true;
            buttonStop.Enabled = true;
            buttonDouble.Enabled = true;
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
        private const int xStart = 600,
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
