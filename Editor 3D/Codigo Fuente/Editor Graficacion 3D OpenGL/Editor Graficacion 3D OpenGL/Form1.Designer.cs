namespace Editor_Graficacion_3D_OpenGL
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.parametrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ocultarMostrarInformacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.girarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.TB_poligonos = new System.Windows.Forms.TextBox();
            this.TB_vertices = new System.Windows.Forms.TextBox();
            this.TB_original = new System.Windows.Forms.TextBox();
            this.OpenGL = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.girarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1049, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametrosToolStripMenuItem,
            this.ocultarMostrarInformacionToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(69, 20);
            this.toolStripMenuItem1.Text = "Opciones";
            // 
            // parametrosToolStripMenuItem
            // 
            this.parametrosToolStripMenuItem.Name = "parametrosToolStripMenuItem";
            this.parametrosToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.parametrosToolStripMenuItem.Text = "Parametros";
            this.parametrosToolStripMenuItem.Click += new System.EventHandler(this.parametrosToolStripMenuItem_Click);
            // 
            // ocultarMostrarInformacionToolStripMenuItem
            // 
            this.ocultarMostrarInformacionToolStripMenuItem.Name = "ocultarMostrarInformacionToolStripMenuItem";
            this.ocultarMostrarInformacionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.ocultarMostrarInformacionToolStripMenuItem.Text = "Ocultar/Mostrar Informacion";
            this.ocultarMostrarInformacionToolStripMenuItem.Click += new System.EventHandler(this.ocultarMostrarInformacionToolStripMenuItem_Click);
            // 
            // girarToolStripMenuItem
            // 
            this.girarToolStripMenuItem.Name = "girarToolStripMenuItem";
            this.girarToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.girarToolStripMenuItem.Text = "Rotar";
            this.girarToolStripMenuItem.Click += new System.EventHandler(this.girarToolStripMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TB_poligonos);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TB_vertices);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TB_original);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1049, 109);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 453);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(1049, 109);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // TB_poligonos
            // 
            this.TB_poligonos.Location = new System.Drawing.Point(756, 3);
            this.TB_poligonos.Multiline = true;
            this.TB_poligonos.Name = "TB_poligonos";
            this.TB_poligonos.ReadOnly = true;
            this.TB_poligonos.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_poligonos.Size = new System.Drawing.Size(375, 104);
            this.TB_poligonos.TabIndex = 2;
            // 
            // TB_vertices
            // 
            this.TB_vertices.Location = new System.Drawing.Point(384, 3);
            this.TB_vertices.Multiline = true;
            this.TB_vertices.Name = "TB_vertices";
            this.TB_vertices.ReadOnly = true;
            this.TB_vertices.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_vertices.Size = new System.Drawing.Size(375, 104);
            this.TB_vertices.TabIndex = 1;
            // 
            // TB_original
            // 
            this.TB_original.Location = new System.Drawing.Point(3, 3);
            this.TB_original.Multiline = true;
            this.TB_original.Name = "TB_original";
            this.TB_original.ReadOnly = true;
            this.TB_original.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_original.Size = new System.Drawing.Size(375, 104);
            this.TB_original.TabIndex = 0;
            // 
            // OpenGL
            // 
            this.OpenGL.AccumBits = ((byte)(0));
            this.OpenGL.AutoCheckErrors = false;
            this.OpenGL.AutoFinish = true;
            this.OpenGL.AutoMakeCurrent = true;
            this.OpenGL.AutoSwapBuffers = true;
            this.OpenGL.BackColor = System.Drawing.Color.Black;
            this.OpenGL.ColorBits = ((byte)(32));
            this.OpenGL.DepthBits = ((byte)(32));
            this.OpenGL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGL.Location = new System.Drawing.Point(0, 0);
            this.OpenGL.Name = "OpenGL";
            this.OpenGL.Size = new System.Drawing.Size(1049, 562);
            this.OpenGL.StencilBits = ((byte)(0));
            this.OpenGL.TabIndex = 3;
            this.OpenGL.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGL_Paint);
            this.OpenGL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGL_MouseDown);
            this.OpenGL.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpenGL_MouseMove);
            this.OpenGL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenGL_MouseUp);
            this.OpenGL.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.OpenGL_MouseWheel);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 562);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.OpenGL);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "Editor 3D";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TextBox TB_original;
        private System.Windows.Forms.TextBox TB_poligonos;
        private System.Windows.Forms.TextBox TB_vertices;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private Tao.Platform.Windows.SimpleOpenGlControl OpenGL;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem girarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ocultarMostrarInformacionToolStripMenuItem;
    }
}

