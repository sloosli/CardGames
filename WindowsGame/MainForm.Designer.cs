namespace WindowsGame
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDouble = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonGetCard = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.formRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cardCounterLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cardCounterLabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonDouble);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.buttonGetCard);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(657, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(127, 500);
            this.panel1.TabIndex = 0;
            // 
            // buttonDouble
            // 
            this.buttonDouble.Location = new System.Drawing.Point(26, 259);
            this.buttonDouble.Name = "buttonDouble";
            this.buttonDouble.Size = new System.Drawing.Size(75, 23);
            this.buttonDouble.TabIndex = 3;
            this.buttonDouble.Text = "Дабл";
            this.buttonDouble.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(26, 230);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // buttonGetCard
            // 
            this.buttonGetCard.Location = new System.Drawing.Point(26, 201);
            this.buttonGetCard.Name = "buttonGetCard";
            this.buttonGetCard.Size = new System.Drawing.Size(75, 23);
            this.buttonGetCard.TabIndex = 1;
            this.buttonGetCard.Text = "Карту";
            this.buttonGetCard.UseVisualStyleBackColor = true;
            this.buttonGetCard.Click += new System.EventHandler(this.ButtonGetCard_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(26, 172);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // formRefreshTimer
            // 
            this.formRefreshTimer.Enabled = true;
            this.formRefreshTimer.Interval = 20;
            this.formRefreshTimer.Tick += new System.EventHandler(this.formRefreshTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Карт осталось:";
            // 
            // cardCounterLabel
            // 
            this.cardCounterLabel.AutoSize = true;
            this.cardCounterLabel.Location = new System.Drawing.Point(89, 26);
            this.cardCounterLabel.Name = "cardCounterLabel";
            this.cardCounterLabel.Size = new System.Drawing.Size(0, 13);
            this.cardCounterLabel.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 500);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDouble;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonGetCard;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Timer formRefreshTimer;
        private System.Windows.Forms.Label cardCounterLabel;
        private System.Windows.Forms.Label label1;
    }
}

