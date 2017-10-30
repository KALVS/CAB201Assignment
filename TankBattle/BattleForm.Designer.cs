namespace TankBattle
{
    partial class BattleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.PowerIndicatorLabel = new System.Windows.Forms.Label();
            this.FireButton = new System.Windows.Forms.Button();
            this.PowerBar = new System.Windows.Forms.TrackBar();
            this.AngleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.weaponComboBox = new System.Windows.Forms.ComboBox();
            this.Power = new System.Windows.Forms.Label();
            this.PowerLabel = new System.Windows.Forms.Label();
            this.AngleLeaveMeAlone = new System.Windows.Forms.Label();
            this.WeaponLeaveMeAlone = new System.Windows.Forms.Label();
            this.Wind = new System.Windows.Forms.Label();
            this.PlayerLabel = new System.Windows.Forms.Label();
            this.WindLeaveMeAlone = new System.Windows.Forms.Label();
            this.weaponEffectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponEffectBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.PowerIndicatorLabel);
            this.controlPanel.Controls.Add(this.FireButton);
            this.controlPanel.Controls.Add(this.PowerBar);
            this.controlPanel.Controls.Add(this.AngleNumericUpDown);
            this.controlPanel.Controls.Add(this.weaponComboBox);
            this.controlPanel.Controls.Add(this.Power);
            this.controlPanel.Controls.Add(this.PowerLabel);
            this.controlPanel.Controls.Add(this.AngleLeaveMeAlone);
            this.controlPanel.Controls.Add(this.WeaponLeaveMeAlone);
            this.controlPanel.Controls.Add(this.Wind);
            this.controlPanel.Controls.Add(this.PlayerLabel);
            this.controlPanel.Controls.Add(this.WindLeaveMeAlone);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 32);
            this.controlPanel.TabIndex = 1;
            this.controlPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.controlPanel_Paint);
            // 
            // PowerIndicatorLabel
            // 
            this.PowerIndicatorLabel.AutoSize = true;
            this.PowerIndicatorLabel.Location = new System.Drawing.Point(641, 11);
            this.PowerIndicatorLabel.Name = "PowerIndicatorLabel";
            this.PowerIndicatorLabel.Size = new System.Drawing.Size(0, 13);
            this.PowerIndicatorLabel.TabIndex = 9;
            // 
            // FireButton
            // 
            this.FireButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FireButton.Location = new System.Drawing.Point(713, 6);
            this.FireButton.Name = "FireButton";
            this.FireButton.Size = new System.Drawing.Size(75, 23);
            this.FireButton.TabIndex = 8;
            this.FireButton.Text = "Fire!";
            this.FireButton.UseVisualStyleBackColor = true;
            this.FireButton.Click += new System.EventHandler(this.FireButton_Click);
            // 
            // PowerBar
            // 
            this.PowerBar.LargeChange = 10;
            this.PowerBar.Location = new System.Drawing.Point(513, 3);
            this.PowerBar.Maximum = 100;
            this.PowerBar.Minimum = 5;
            this.PowerBar.Name = "PowerBar";
            this.PowerBar.Size = new System.Drawing.Size(104, 45);
            this.PowerBar.TabIndex = 7;
            this.PowerBar.Value = 5;
            this.PowerBar.Scroll += new System.EventHandler(this.PowerBar_Scroll);
            // 
            // AngleNumericUpDown
            // 
            this.AngleNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.AngleNumericUpDown.Location = new System.Drawing.Point(410, 6);
            this.AngleNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.AngleNumericUpDown.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.AngleNumericUpDown.Name = "AngleNumericUpDown";
            this.AngleNumericUpDown.Size = new System.Drawing.Size(45, 20);
            this.AngleNumericUpDown.TabIndex = 6;
            this.AngleNumericUpDown.ValueChanged += new System.EventHandler(this.AngleNumericUpDown_ValueChanged);
            // 
            // weaponComboBox
            // 
            this.weaponComboBox.FormattingEnabled = true;
            this.weaponComboBox.Location = new System.Drawing.Point(234, 6);
            this.weaponComboBox.Name = "weaponComboBox";
            this.weaponComboBox.Size = new System.Drawing.Size(121, 21);
            this.weaponComboBox.TabIndex = 5;
            this.weaponComboBox.SelectedIndexChanged += new System.EventHandler(this.weaponComboBox_SelectedIndexChanged);
            // 
            // Power
            // 
            this.Power.AutoSize = true;
            this.Power.Location = new System.Drawing.Point(623, 11);
            this.Power.Name = "Power";
            this.Power.Size = new System.Drawing.Size(0, 13);
            this.Power.TabIndex = 4;
            // 
            // PowerLabel
            // 
            this.PowerLabel.AutoSize = true;
            this.PowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerLabel.Location = new System.Drawing.Point(461, 9);
            this.PowerLabel.Name = "PowerLabel";
            this.PowerLabel.Size = new System.Drawing.Size(46, 13);
            this.PowerLabel.TabIndex = 3;
            this.PowerLabel.Text = "Power:";
            // 
            // AngleLeaveMeAlone
            // 
            this.AngleLeaveMeAlone.AutoSize = true;
            this.AngleLeaveMeAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AngleLeaveMeAlone.Location = new System.Drawing.Point(361, 8);
            this.AngleLeaveMeAlone.Name = "AngleLeaveMeAlone";
            this.AngleLeaveMeAlone.Size = new System.Drawing.Size(43, 13);
            this.AngleLeaveMeAlone.TabIndex = 2;
            this.AngleLeaveMeAlone.Text = "Angle:";
            // 
            // WeaponLeaveMeAlone
            // 
            this.WeaponLeaveMeAlone.AutoSize = true;
            this.WeaponLeaveMeAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WeaponLeaveMeAlone.Location = new System.Drawing.Point(168, 9);
            this.WeaponLeaveMeAlone.Name = "WeaponLeaveMeAlone";
            this.WeaponLeaveMeAlone.Size = new System.Drawing.Size(70, 16);
            this.WeaponLeaveMeAlone.TabIndex = 1;
            this.WeaponLeaveMeAlone.Text = "Weapon:";
            // 
            // Wind
            // 
            this.Wind.AutoSize = true;
            this.Wind.Location = new System.Drawing.Point(104, 13);
            this.Wind.Name = "Wind";
            this.Wind.Size = new System.Drawing.Size(27, 13);
            this.Wind.TabIndex = 0;
            this.Wind.Text = "0 W";
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.AutoSize = true;
            this.PlayerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerLabel.Location = new System.Drawing.Point(12, 9);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(66, 16);
            this.PlayerLabel.TabIndex = 0;
            this.PlayerLabel.Text = "Player X";
            // 
            // WindLeaveMeAlone
            // 
            this.WindLeaveMeAlone.AutoSize = true;
            this.WindLeaveMeAlone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WindLeaveMeAlone.Location = new System.Drawing.Point(104, 0);
            this.WindLeaveMeAlone.Name = "WindLeaveMeAlone";
            this.WindLeaveMeAlone.Size = new System.Drawing.Size(36, 13);
            this.WindLeaveMeAlone.TabIndex = 0;
            this.WindLeaveMeAlone.Text = "Wind";
            this.WindLeaveMeAlone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // weaponEffectBindingSource
            // 
            this.weaponEffectBindingSource.DataSource = typeof(TankBattle.WeaponEffect);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "BattleForm";
            this.Text = "Tank Battle";
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PowerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.weaponEffectBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label PlayerLabel;
        private System.Windows.Forms.Label WindLeaveMeAlone;
        private System.Windows.Forms.Label Power;
        private System.Windows.Forms.Label PowerLabel;
        private System.Windows.Forms.Label AngleLeaveMeAlone;
        private System.Windows.Forms.Label WeaponLeaveMeAlone;
        private System.Windows.Forms.Label Wind;
        private System.Windows.Forms.ComboBox weaponComboBox;
        private System.Windows.Forms.BindingSource weaponEffectBindingSource;
        private System.Windows.Forms.NumericUpDown AngleNumericUpDown;
        private System.Windows.Forms.TrackBar PowerBar;
        private System.Windows.Forms.Button FireButton;
        private System.Windows.Forms.Label PowerIndicatorLabel;
        private System.Windows.Forms.Timer timer1;
    }
}

