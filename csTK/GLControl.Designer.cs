namespace csTK
{
    partial class GLControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.glCtrl = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // glCtrl
            // 
            this.glCtrl.BackColor = System.Drawing.Color.Black;
            this.glCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glCtrl.Location = new System.Drawing.Point(0, 0);
            this.glCtrl.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.glCtrl.Name = "glCtrl";
            this.glCtrl.Size = new System.Drawing.Size(784, 562);
            this.glCtrl.TabIndex = 0;
            this.glCtrl.VSync = false;
            // 
            // GLControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GLControl";
            this.Size = new System.Drawing.Size(192, 154);
            this.ResumeLayout(false);

        }

        #endregion
        private OpenTK.GLControl glCtrl;
    }
}
