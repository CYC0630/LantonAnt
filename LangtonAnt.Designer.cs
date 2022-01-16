
namespace Ant
{
    partial class LangtonAnt
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
            this.OpenGLControl = new Tao.Platform.Windows.OpenGLControl();
            this.TimerTick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // OpenGLControl
            // 
            this.OpenGLControl.AccumBits = ((byte)(0));
            this.OpenGLControl.AutoCheckErrors = false;
            this.OpenGLControl.AutoFinish = false;
            this.OpenGLControl.AutoMakeCurrent = true;
            this.OpenGLControl.AutoSwapBuffers = true;
            this.OpenGLControl.BackColor = System.Drawing.Color.Black;
            this.OpenGLControl.ColorBits = ((byte)(32));
            this.OpenGLControl.DepthBits = ((byte)(16));
            this.OpenGLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OpenGLControl.Location = new System.Drawing.Point(0, 0);
            this.OpenGLControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpenGLControl.Name = "OpenGLControl";
            this.OpenGLControl.Size = new System.Drawing.Size(941, 492);
            this.OpenGLControl.StencilBits = ((byte)(0));
            this.OpenGLControl.TabIndex = 0;
            this.OpenGLControl.Load += new System.EventHandler(this.OpenGLControl_Load);
            this.OpenGLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.OpenGLControl_Paint);
            this.OpenGLControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpenGLControl_KeyDown);
            this.OpenGLControl.Resize += new System.EventHandler(this.OpenGLControl_Resize);
            // 
            // TimerTick
            // 
            this.TimerTick.Enabled = true;
            this.TimerTick.Interval = 50;
            this.TimerTick.Tick += new System.EventHandler(this.TimerTick_Tick);
            // 
            // LangtonAnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 492);
            this.Controls.Add(this.OpenGLControl);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LangtonAnt";
            this.Text = "蘭頓螞蟻";
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.OpenGLControl OpenGLControl;
        private System.Windows.Forms.Timer TimerTick;
    }
}

