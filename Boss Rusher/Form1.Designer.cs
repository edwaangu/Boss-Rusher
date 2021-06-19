
namespace Boss_Rusher
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTick = new System.Windows.Forms.Timer(this.components);
            this.easyButton = new System.Windows.Forms.Button();
            this.normalButton = new System.Windows.Forms.Button();
            this.hardButton = new System.Windows.Forms.Button();
            this.titleBox = new System.Windows.Forms.PictureBox();
            this.dialogueLabel1 = new System.Windows.Forms.Label();
            this.dialogueLabel2 = new System.Windows.Forms.Label();
            this.rankBox = new System.Windows.Forms.PictureBox();
            this.menuButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.titleBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rankBox)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTick
            // 
            this.gameTick.Enabled = true;
            this.gameTick.Interval = 20;
            this.gameTick.Tick += new System.EventHandler(this.gameTick_Tick);
            // 
            // easyButton
            // 
            this.easyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.easyButton.FlatAppearance.BorderSize = 4;
            this.easyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.easyButton.Font = new System.Drawing.Font("Agenda Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyButton.Location = new System.Drawing.Point(475, 250);
            this.easyButton.Name = "easyButton";
            this.easyButton.Size = new System.Drawing.Size(250, 70);
            this.easyButton.TabIndex = 1;
            this.easyButton.Text = "Easy";
            this.easyButton.UseVisualStyleBackColor = false;
            this.easyButton.Click += new System.EventHandler(this.easyButton_Click);
            // 
            // normalButton
            // 
            this.normalButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.normalButton.FlatAppearance.BorderSize = 4;
            this.normalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalButton.Font = new System.Drawing.Font("Agenda Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.normalButton.Location = new System.Drawing.Point(475, 330);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(250, 70);
            this.normalButton.TabIndex = 2;
            this.normalButton.Text = "Normal";
            this.normalButton.UseVisualStyleBackColor = false;
            this.normalButton.Click += new System.EventHandler(this.normalButton_Click);
            // 
            // hardButton
            // 
            this.hardButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.hardButton.FlatAppearance.BorderSize = 4;
            this.hardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hardButton.Font = new System.Drawing.Font("Agenda Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hardButton.Location = new System.Drawing.Point(475, 410);
            this.hardButton.Name = "hardButton";
            this.hardButton.Size = new System.Drawing.Size(250, 70);
            this.hardButton.TabIndex = 3;
            this.hardButton.Text = "Hard";
            this.hardButton.UseVisualStyleBackColor = false;
            this.hardButton.Click += new System.EventHandler(this.hardButton_Click);
            // 
            // titleBox
            // 
            this.titleBox.BackColor = System.Drawing.Color.Transparent;
            this.titleBox.Image = global::Boss_Rusher.Properties.Resources.titleText;
            this.titleBox.Location = new System.Drawing.Point(400, -30);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(400, 260);
            this.titleBox.TabIndex = 0;
            this.titleBox.TabStop = false;
            // 
            // dialogueLabel1
            // 
            this.dialogueLabel1.BackColor = System.Drawing.Color.Transparent;
            this.dialogueLabel1.Font = new System.Drawing.Font("Ubuntu Light", 24F, System.Drawing.FontStyle.Bold);
            this.dialogueLabel1.Location = new System.Drawing.Point(150, 0);
            this.dialogueLabel1.Name = "dialogueLabel1";
            this.dialogueLabel1.Size = new System.Drawing.Size(900, 55);
            this.dialogueLabel1.TabIndex = 4;
            this.dialogueLabel1.Text = "Great work on beating the only boss!";
            this.dialogueLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dialogueLabel1.Visible = false;
            // 
            // dialogueLabel2
            // 
            this.dialogueLabel2.BackColor = System.Drawing.Color.Transparent;
            this.dialogueLabel2.Font = new System.Drawing.Font("Ubuntu Light", 20F, System.Drawing.FontStyle.Bold);
            this.dialogueLabel2.Location = new System.Drawing.Point(150, 45);
            this.dialogueLabel2.Name = "dialogueLabel2";
            this.dialogueLabel2.Size = new System.Drawing.Size(900, 35);
            this.dialogueLabel2.TabIndex = 5;
            this.dialogueLabel2.Text = "Your rank is...";
            this.dialogueLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dialogueLabel2.Visible = false;
            // 
            // rankBox
            // 
            this.rankBox.BackColor = System.Drawing.Color.Transparent;
            this.rankBox.Image = global::Boss_Rusher.Properties.Resources.ARank;
            this.rankBox.Location = new System.Drawing.Point(525, 150);
            this.rankBox.Name = "rankBox";
            this.rankBox.Size = new System.Drawing.Size(150, 150);
            this.rankBox.TabIndex = 6;
            this.rankBox.TabStop = false;
            this.rankBox.Visible = false;
            // 
            // menuButton
            // 
            this.menuButton.BackColor = System.Drawing.Color.Silver;
            this.menuButton.FlatAppearance.BorderSize = 4;
            this.menuButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.menuButton.Font = new System.Drawing.Font("Agenda Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuButton.Location = new System.Drawing.Point(475, 410);
            this.menuButton.Name = "menuButton";
            this.menuButton.Size = new System.Drawing.Size(250, 70);
            this.menuButton.TabIndex = 7;
            this.menuButton.Text = "Menu";
            this.menuButton.UseVisualStyleBackColor = false;
            this.menuButton.Visible = false;
            this.menuButton.Click += new System.EventHandler(this.menuButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Silver;
            this.exitButton.FlatAppearance.BorderSize = 4;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Agenda Bold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(475, 486);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(250, 70);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.menuButton);
            this.Controls.Add(this.rankBox);
            this.Controls.Add(this.dialogueLabel2);
            this.Controls.Add(this.dialogueLabel1);
            this.Controls.Add(this.hardButton);
            this.Controls.Add(this.normalButton);
            this.Controls.Add(this.easyButton);
            this.Controls.Add(this.titleBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.titleBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rankBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameTick;
        private System.Windows.Forms.PictureBox titleBox;
        private System.Windows.Forms.Button easyButton;
        private System.Windows.Forms.Button normalButton;
        private System.Windows.Forms.Button hardButton;
        private System.Windows.Forms.Label dialogueLabel1;
        private System.Windows.Forms.Label dialogueLabel2;
        private System.Windows.Forms.PictureBox rankBox;
        private System.Windows.Forms.Button menuButton;
        private System.Windows.Forms.Button exitButton;
    }
}

