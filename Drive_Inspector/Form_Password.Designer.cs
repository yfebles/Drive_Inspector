namespace Drive_Inspector
{
    partial class Form_Password
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
            this.accept_bttn = new System.Windows.Forms.Button();
            this.tbox_pass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // accept_bttn
            // 
            this.accept_bttn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.accept_bttn.Location = new System.Drawing.Point(73, 47);
            this.accept_bttn.Name = "accept_bttn";
            this.accept_bttn.Size = new System.Drawing.Size(105, 26);
            this.accept_bttn.TabIndex = 0;
            this.accept_bttn.Text = "Aceptar";
            this.accept_bttn.UseVisualStyleBackColor = true;
            this.accept_bttn.Click += new System.EventHandler(this.accept_bttn_Click);
            // 
            // tbox_pass
            // 
            this.tbox_pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbox_pass.Location = new System.Drawing.Point(12, 12);
            this.tbox_pass.Name = "tbox_pass";
            this.tbox_pass.PasswordChar = '*';
            this.tbox_pass.Size = new System.Drawing.Size(235, 26);
            this.tbox_pass.TabIndex = 0;
            this.tbox_pass.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbox_pass_KeyPress);
            // 
            // Form_Password
            // 
            this.AcceptButton = this.accept_bttn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(84)))), ((int)(((byte)(84)))));
            this.ClientSize = new System.Drawing.Size(259, 93);
            this.Controls.Add(this.tbox_pass);
            this.Controls.Add(this.accept_bttn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Password";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escriba su contraseña";
            this.Load += new System.EventHandler(this.Form_Password_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept_bttn;
        private System.Windows.Forms.TextBox tbox_pass;
    }
}