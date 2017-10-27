namespace TARGITD3FODataReader
{
    partial class TableContent
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
            this.d3FOConnectionManagerEditorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.d3FOConnectionManagerEditorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // d3FOConnectionManagerEditorBindingSource
            // 
            this.d3FOConnectionManagerEditorBindingSource.DataSource = typeof(TARGITD3FOConnection.D3FOConnectionManagerEditor);
            // 
            // TableContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 400);
            this.Name = "TableContent";
            this.Text = "TableContent";
            ((System.ComponentModel.ISupportInitialize)(this.d3FOConnectionManagerEditorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource d3FOConnectionManagerEditorBindingSource;
    }
}