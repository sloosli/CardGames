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
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxCardRemain = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlayerHand = new System.Windows.Forms.TextBox();
            this.textBoxDealerHand = new System.Windows.Forms.TextBox();
            this.formRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonDouble);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.buttonGetCard);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(600, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 500);
            this.panel1.TabIndex = 0;
            // 
            // buttonDouble
            // 
            this.buttonDouble.Location = new System.Drawing.Point(72, 323);
            this.buttonDouble.Name = "buttonDouble";
            this.buttonDouble.Size = new System.Drawing.Size(75, 23);
            this.buttonDouble.TabIndex = 3;
            this.buttonDouble.Text = "Дабл";
            this.buttonDouble.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(72, 294);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Стоп";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.ButtonStop_Click);
            // 
            // buttonGetCard
            // 
            this.buttonGetCard.Location = new System.Drawing.Point(72, 265);
            this.buttonGetCard.Name = "buttonGetCard";
            this.buttonGetCard.Size = new System.Drawing.Size(75, 23);
            this.buttonGetCard.TabIndex = 1;
            this.buttonGetCard.Text = "Карту";
            this.buttonGetCard.UseVisualStyleBackColor = true;
            this.buttonGetCard.Click += new System.EventHandler(this.ButtonGetCard_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(72, 236);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxCardRemain);
            this.panel2.Controls.Add(this.textBoxResult);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBoxPlayerHand);
            this.panel2.Controls.Add(this.textBoxDealerHand);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 500);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // textBoxCardRemain
            // 
            this.textBoxCardRemain.Location = new System.Drawing.Point(411, 49);
            this.textBoxCardRemain.Name = "textBoxCardRemain";
            this.textBoxCardRemain.ReadOnly = true;
            this.textBoxCardRemain.Size = new System.Drawing.Size(100, 20);
            this.textBoxCardRemain.TabIndex = 6;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(349, 172);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(234, 116);
            this.textBoxResult.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Результат";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ваша рука";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Рука дилера";
            // 
            // textBoxPlayerHand
            // 
            this.textBoxPlayerHand.Location = new System.Drawing.Point(67, 265);
            this.textBoxPlayerHand.Multiline = true;
            this.textBoxPlayerHand.Name = "textBoxPlayerHand";
            this.textBoxPlayerHand.ReadOnly = true;
            this.textBoxPlayerHand.Size = new System.Drawing.Size(234, 127);
            this.textBoxPlayerHand.TabIndex = 1;
            // 
            // textBoxDealerHand
            // 
            this.textBoxDealerHand.Location = new System.Drawing.Point(67, 72);
            this.textBoxDealerHand.Multiline = true;
            this.textBoxDealerHand.Name = "textBoxDealerHand";
            this.textBoxDealerHand.ReadOnly = true;
            this.textBoxDealerHand.Size = new System.Drawing.Size(234, 116);
            this.textBoxDealerHand.TabIndex = 0;
            // 
            // formRefreshTimer
            // 
            this.formRefreshTimer.Enabled = true;
            this.formRefreshTimer.Interval = 20;
            this.formRefreshTimer.Tick += new System.EventHandler(this.formRefreshTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 500);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDouble;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonGetCard;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlayerHand;
        private System.Windows.Forms.TextBox textBoxDealerHand;
        private System.Windows.Forms.TextBox textBoxCardRemain;
        private System.Windows.Forms.Timer formRefreshTimer;
    }
}

