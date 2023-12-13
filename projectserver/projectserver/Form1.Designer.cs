namespace projectserver
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBoxServer = new RichTextBox();
            textBoxPort = new TextBox();
            label1 = new Label();
            buttonListen = new Button();
            richTextBoxConnected = new RichTextBox();
            richTextBoxIF = new RichTextBox();
            richTextBoxSPS = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBoxServer
            // 
            richTextBoxServer.Location = new Point(68, 90);
            richTextBoxServer.Name = "richTextBoxServer";
            richTextBoxServer.Size = new Size(392, 248);
            richTextBoxServer.TabIndex = 0;
            richTextBoxServer.Text = "";
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(107, 35);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(190, 27);
            textBoxPort.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(63, 42);
            label1.Name = "label1";
            label1.Size = new Size(38, 20);
            label1.TabIndex = 2;
            label1.Text = "Port:";
            // 
            // buttonListen
            // 
            buttonListen.Location = new Point(326, 35);
            buttonListen.Name = "buttonListen";
            buttonListen.Size = new Size(134, 27);
            buttonListen.TabIndex = 3;
            buttonListen.Text = "Listen";
            buttonListen.UseVisualStyleBackColor = true;
            buttonListen.Click += buttonListen_Click;
            // 
            // richTextBoxConnected
            // 
            richTextBoxConnected.Location = new Point(68, 344);
            richTextBoxConnected.Name = "richTextBoxConnected";
            richTextBoxConnected.Size = new Size(392, 106);
            richTextBoxConnected.TabIndex = 4;
            richTextBoxConnected.Text = "";
            // 
            // richTextBoxIF
            // 
            richTextBoxIF.Location = new Point(68, 456);
            richTextBoxIF.Name = "richTextBoxIF";
            richTextBoxIF.Size = new Size(392, 106);
            richTextBoxIF.TabIndex = 5;
            richTextBoxIF.Text = "";
            // 
            // richTextBoxSPS
            // 
            richTextBoxSPS.Location = new Point(68, 568);
            richTextBoxSPS.Name = "richTextBoxSPS";
            richTextBoxSPS.Size = new Size(392, 106);
            richTextBoxSPS.TabIndex = 6;
            richTextBoxSPS.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 703);
            Controls.Add(richTextBoxSPS);
            Controls.Add(richTextBoxIF);
            Controls.Add(richTextBoxConnected);
            Controls.Add(buttonListen);
            Controls.Add(label1);
            Controls.Add(textBoxPort);
            Controls.Add(richTextBoxServer);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBoxServer;
        private TextBox textBoxPort;
        private Label label1;
        private Button buttonListen;
        private RichTextBox richTextBoxConnected;
        private RichTextBox richTextBoxIF;
        private RichTextBox richTextBoxSPS;
    }
}