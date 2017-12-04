namespace Editor_Graficacion_3D_OpenGL
{
    partial class Giros
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
            this.RB_Y = new System.Windows.Forms.RadioButton();
            this.RB_YN = new System.Windows.Forms.RadioButton();
            this.RB_XN = new System.Windows.Forms.RadioButton();
            this.RB_X = new System.Windows.Forms.RadioButton();
            this.RB_ZN = new System.Windows.Forms.RadioButton();
            this.RB_Z = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BT_Cerrar = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // RB_Y
            // 
            this.RB_Y.AutoSize = true;
            this.RB_Y.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.RB_Y.Location = new System.Drawing.Point(132, 12);
            this.RB_Y.Name = "RB_Y";
            this.RB_Y.Size = new System.Drawing.Size(18, 30);
            this.RB_Y.TabIndex = 0;
            this.RB_Y.TabStop = true;
            this.RB_Y.Text = "Y";
            this.RB_Y.UseVisualStyleBackColor = true;
            this.RB_Y.CheckedChanged += new System.EventHandler(this.RB_Y_CheckedChanged);
            // 
            // RB_YN
            // 
            this.RB_YN.AutoSize = true;
            this.RB_YN.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.RB_YN.Location = new System.Drawing.Point(129, 220);
            this.RB_YN.Name = "RB_YN";
            this.RB_YN.Size = new System.Drawing.Size(21, 30);
            this.RB_YN.TabIndex = 1;
            this.RB_YN.TabStop = true;
            this.RB_YN.Text = "-Y";
            this.RB_YN.UseVisualStyleBackColor = true;
            this.RB_YN.CheckedChanged += new System.EventHandler(this.RB_YN_CheckedChanged);
            // 
            // RB_XN
            // 
            this.RB_XN.AutoSize = true;
            this.RB_XN.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RB_XN.Location = new System.Drawing.Point(12, 126);
            this.RB_XN.Name = "RB_XN";
            this.RB_XN.Size = new System.Drawing.Size(35, 17);
            this.RB_XN.TabIndex = 2;
            this.RB_XN.TabStop = true;
            this.RB_XN.Text = "-X";
            this.RB_XN.UseVisualStyleBackColor = true;
            this.RB_XN.CheckedChanged += new System.EventHandler(this.RB_XN_CheckedChanged);
            // 
            // RB_X
            // 
            this.RB_X.AutoSize = true;
            this.RB_X.Location = new System.Drawing.Point(240, 126);
            this.RB_X.Name = "RB_X";
            this.RB_X.Size = new System.Drawing.Size(32, 17);
            this.RB_X.TabIndex = 3;
            this.RB_X.TabStop = true;
            this.RB_X.Text = "X";
            this.RB_X.UseVisualStyleBackColor = true;
            this.RB_X.CheckedChanged += new System.EventHandler(this.RB_X_CheckedChanged);
            // 
            // RB_ZN
            // 
            this.RB_ZN.AutoSize = true;
            this.RB_ZN.Location = new System.Drawing.Point(208, 56);
            this.RB_ZN.Name = "RB_ZN";
            this.RB_ZN.Size = new System.Drawing.Size(35, 17);
            this.RB_ZN.TabIndex = 4;
            this.RB_ZN.TabStop = true;
            this.RB_ZN.Text = "-Z";
            this.RB_ZN.UseVisualStyleBackColor = true;
            this.RB_ZN.CheckedChanged += new System.EventHandler(this.RB_ZN_CheckedChanged);
            // 
            // RB_Z
            // 
            this.RB_Z.AutoSize = true;
            this.RB_Z.Location = new System.Drawing.Point(64, 195);
            this.RB_Z.Name = "RB_Z";
            this.RB_Z.Size = new System.Drawing.Size(32, 17);
            this.RB_Z.TabIndex = 5;
            this.RB_Z.TabStop = true;
            this.RB_Z.Text = "Z";
            this.RB_Z.UseVisualStyleBackColor = true;
            this.RB_Z.CheckedChanged += new System.EventHandler(this.RB_Z_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::Editor_Graficacion_3D_OpenGL.Properties.Resources.xyz2;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 260);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // BT_Cerrar
            // 
            this.BT_Cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_Cerrar.Location = new System.Drawing.Point(197, 227);
            this.BT_Cerrar.Name = "BT_Cerrar";
            this.BT_Cerrar.Size = new System.Drawing.Size(75, 23);
            this.BT_Cerrar.TabIndex = 7;
            this.BT_Cerrar.Text = "Cerrar";
            this.BT_Cerrar.UseVisualStyleBackColor = true;
            this.BT_Cerrar.Click += new System.EventHandler(this.BT_Cerrar_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(22, 56);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Velocidad";
            // 
            // Giros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 260);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.BT_Cerrar);
            this.Controls.Add(this.RB_Z);
            this.Controls.Add(this.RB_ZN);
            this.Controls.Add(this.RB_X);
            this.Controls.Add(this.RB_XN);
            this.Controls.Add(this.RB_YN);
            this.Controls.Add(this.RB_Y);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Giros";
            this.Text = "Rotar";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RB_Y;
        private System.Windows.Forms.RadioButton RB_YN;
        private System.Windows.Forms.RadioButton RB_XN;
        private System.Windows.Forms.RadioButton RB_X;
        private System.Windows.Forms.RadioButton RB_ZN;
        private System.Windows.Forms.RadioButton RB_Z;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BT_Cerrar;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
    }
}