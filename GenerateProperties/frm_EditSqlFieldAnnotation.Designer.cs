namespace GenerateProperties
{
    partial class frm_EditSqlFieldAnnotation
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
            this.table1 = new AntdUI.Table();
            this.SuspendLayout();
            // 
            // table1
            // 
            this.table1.Gap = 12;
            this.table1.Location = new System.Drawing.Point(12, 12);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(1696, 955);
            this.table1.TabIndex = 0;
            this.table1.Text = "table1";
            // 
            // frm_EditSqlFieldAnnotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2022, 1008);
            this.Controls.Add(this.table1);
            this.Name = "frm_EditSqlFieldAnnotation";
            this.Text = "frm_EditSqlFieldAnnotation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_EditSqlFieldAnnotation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table table1;
    }
}