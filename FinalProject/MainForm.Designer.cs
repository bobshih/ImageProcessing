namespace FinalProject
{
    partial class MainForm
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
            this._cTableLayoutAll = new System.Windows.Forms.TableLayoutPanel();
            this._cButtonStartGame = new System.Windows.Forms.Button();
            this._cImageBoxTracing = new FinalProject.SellectableAndTracableImageBox();
            this._cTableLayoutAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._cImageBoxTracing)).BeginInit();
            this.SuspendLayout();
            // 
            // _cTableLayoutAll
            // 
            this._cTableLayoutAll.ColumnCount = 1;
            this._cTableLayoutAll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._cTableLayoutAll.Controls.Add(this._cButtonStartGame, 0, 1);
            this._cTableLayoutAll.Controls.Add(this._cImageBoxTracing, 0, 0);
            this._cTableLayoutAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cTableLayoutAll.Location = new System.Drawing.Point(0, 0);
            this._cTableLayoutAll.Name = "_cTableLayoutAll";
            this._cTableLayoutAll.RowCount = 2;
            this._cTableLayoutAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._cTableLayoutAll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this._cTableLayoutAll.Size = new System.Drawing.Size(476, 321);
            this._cTableLayoutAll.TabIndex = 0;
            // 
            // _cButtonStartGame
            // 
            this._cButtonStartGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cButtonStartGame.Location = new System.Drawing.Point(3, 252);
            this._cButtonStartGame.Name = "_cButtonStartGame";
            this._cButtonStartGame.Size = new System.Drawing.Size(470, 66);
            this._cButtonStartGame.TabIndex = 0;
            this._cButtonStartGame.Text = "Start Game";
            this._cButtonStartGame.UseVisualStyleBackColor = true;
            this._cButtonStartGame.Click += new System.EventHandler(this.ClickGameButton);
            // 
            // _cImageBoxTracing
            // 
            this._cImageBoxTracing.Dock = System.Windows.Forms.DockStyle.Fill;
            this._cImageBoxTracing.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this._cImageBoxTracing.Location = new System.Drawing.Point(3, 3);
            this._cImageBoxTracing.Name = "_cImageBoxTracing";
            this._cImageBoxTracing.Size = new System.Drawing.Size(470, 243);
            this._cImageBoxTracing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._cImageBoxTracing.TabIndex = 2;
            this._cImageBoxTracing.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 321);
            this.Controls.Add(this._cTableLayoutAll);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this._cTableLayoutAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._cImageBoxTracing)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _cTableLayoutAll;
        private System.Windows.Forms.Button _cButtonStartGame;
        private SellectableAndTracableImageBox _cImageBoxTracing;
    }
}