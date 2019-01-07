namespace FinalProject
{
    partial class Game
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this._cLabelLevelUGet = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _cLabelLevelUGet
            // 
            this._cLabelLevelUGet.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._cLabelLevelUGet.AutoSize = true;
            this._cLabelLevelUGet.BackColor = System.Drawing.Color.Transparent;
            this._cLabelLevelUGet.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this._cLabelLevelUGet.ForeColor = System.Drawing.Color.White;
            this._cLabelLevelUGet.Location = new System.Drawing.Point(159, 232);
            this._cLabelLevelUGet.Name = "_cLabelLevelUGet";
            this._cLabelLevelUGet.Size = new System.Drawing.Size(0, 48);
            this._cLabelLevelUGet.TabIndex = 0;
            this._cLabelLevelUGet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 289);
            this.Controls.Add(this._cLabelLevelUGet);
            this.Name = "Game";
            this.Text = "Ufo ~Hi~ Attack";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ClientSizeChanged += new System.EventHandler(this.ChangeClientSize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _cLabelLevelUGet;
    }
}

