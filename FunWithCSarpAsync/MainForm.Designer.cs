namespace FunWithCSarpAsync
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
            this.btnCallMethod = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnShowMessage = new System.Windows.Forms.Button();
            this.btnMultiAwaits = new System.Windows.Forms.Button();
            this.btnSum = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCallMethod
            // 
            this.btnCallMethod.Location = new System.Drawing.Point(81, 12);
            this.btnCallMethod.Name = "btnCallMethod";
            this.btnCallMethod.Size = new System.Drawing.Size(114, 46);
            this.btnCallMethod.TabIndex = 0;
            this.btnCallMethod.Text = "Вызвать метод";
            this.btnCallMethod.UseVisualStyleBackColor = true;
            this.btnCallMethod.Click += new System.EventHandler(this.btnCallMethod_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(29, 108);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(216, 20);
            this.txtInput.TabIndex = 1;
            // 
            // btnShowMessage
            // 
            this.btnShowMessage.Location = new System.Drawing.Point(81, 169);
            this.btnShowMessage.Name = "btnShowMessage";
            this.btnShowMessage.Size = new System.Drawing.Size(114, 46);
            this.btnShowMessage.TabIndex = 2;
            this.btnShowMessage.Text = "Показать сообщение";
            this.btnShowMessage.UseVisualStyleBackColor = true;
            this.btnShowMessage.Click += new System.EventHandler(this.btnShowMessage_Click);
            // 
            // btnMultiAwaits
            // 
            this.btnMultiAwaits.Location = new System.Drawing.Point(81, 263);
            this.btnMultiAwaits.Name = "btnMultiAwaits";
            this.btnMultiAwaits.Size = new System.Drawing.Size(114, 46);
            this.btnMultiAwaits.TabIndex = 3;
            this.btnMultiAwaits.Text = "Много";
            this.btnMultiAwaits.UseVisualStyleBackColor = true;
            this.btnMultiAwaits.Click += new System.EventHandler(this.btnMultiAwaits_Click);
            // 
            // btnSum
            // 
            this.btnSum.Location = new System.Drawing.Point(81, 328);
            this.btnSum.Name = "btnSum";
            this.btnSum.Size = new System.Drawing.Size(114, 46);
            this.btnSum.TabIndex = 4;
            this.btnSum.Text = "Суммировать";
            this.btnSum.UseVisualStyleBackColor = true;
            this.btnSum.Click += new System.EventHandler(this.btnSum_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 398);
            this.Controls.Add(this.btnSum);
            this.Controls.Add(this.btnMultiAwaits);
            this.Controls.Add(this.btnShowMessage);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnCallMethod);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCallMethod;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnShowMessage;
        private System.Windows.Forms.Button btnMultiAwaits;
        private System.Windows.Forms.Button btnSum;
    }
}

