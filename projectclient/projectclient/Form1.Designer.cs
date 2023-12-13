namespace projectclient
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
            textBoxIP = new TextBox();
            textBoxPort = new TextBox();
            label1 = new Label();
            label2 = new Label();
            logsIF = new RichTextBox();
            label3 = new Label();
            subscriptionIF = new Button();
            subscriptionSPS = new Button();
            label4 = new Label();
            channelIF = new Button();
            channelSPS = new Button();
            label5 = new Label();
            textBoxUsername = new TextBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            label6 = new Label();
            textBoxMessage = new TextBox();
            buttonSend = new Button();
            logsGeneral = new RichTextBox();
            logsSPS = new RichTextBox();
            SuspendLayout();
            // 
            // textBoxIP
            // 
            textBoxIP.Location = new Point(121, 77);
            textBoxIP.Name = "textBoxIP";
            textBoxIP.Size = new Size(156, 27);
            textBoxIP.TabIndex = 0;
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(121, 127);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(156, 27);
            textBoxPort.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 80);
            label1.Name = "label1";
            label1.Size = new Size(24, 20);
            label1.TabIndex = 2;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 134);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 3;
            label2.Text = "Port:";
            // 
            // logsIF
            // 
            logsIF.Location = new Point(794, 77);
            logsIF.Name = "logsIF";
            logsIF.Size = new Size(362, 289);
            logsIF.TabIndex = 4;
            logsIF.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 239);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 5;
            label3.Text = "Subscription:";
            // 
            // subscriptionIF
            // 
            subscriptionIF.BackColor = Color.IndianRed;
            subscriptionIF.Enabled = false;
            subscriptionIF.Location = new Point(166, 231);
            subscriptionIF.Name = "subscriptionIF";
            subscriptionIF.Size = new Size(79, 28);
            subscriptionIF.TabIndex = 6;
            subscriptionIF.Text = "IF100";
            subscriptionIF.UseVisualStyleBackColor = false;
            subscriptionIF.Click += subscriptionIF_Click;
            // 
            // subscriptionSPS
            // 
            subscriptionSPS.BackColor = Color.IndianRed;
            subscriptionSPS.Enabled = false;
            subscriptionSPS.Location = new Point(282, 231);
            subscriptionSPS.Name = "subscriptionSPS";
            subscriptionSPS.Size = new Size(79, 28);
            subscriptionSPS.TabIndex = 7;
            subscriptionSPS.Text = "SPS101";
            subscriptionSPS.UseVisualStyleBackColor = false;
            subscriptionSPS.Click += subscriptionSPS_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 306);
            label4.Name = "label4";
            label4.Size = new Size(65, 20);
            label4.TabIndex = 8;
            label4.Text = "Channel:";
            // 
            // channelIF
            // 
            channelIF.BackColor = Color.LightGray;
            channelIF.Enabled = false;
            channelIF.Location = new Point(166, 298);
            channelIF.Name = "channelIF";
            channelIF.Size = new Size(79, 28);
            channelIF.TabIndex = 9;
            channelIF.Text = "IF100";
            channelIF.UseVisualStyleBackColor = false;
            channelIF.Click += channelIF_Click;
            // 
            // channelSPS
            // 
            channelSPS.BackColor = Color.LightGray;
            channelSPS.Enabled = false;
            channelSPS.Location = new Point(282, 298);
            channelSPS.Name = "channelSPS";
            channelSPS.Size = new Size(79, 28);
            channelSPS.TabIndex = 10;
            channelSPS.Text = "SPS101";
            channelSPS.UseVisualStyleBackColor = false;
            channelSPS.Click += channelSPS_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(36, 178);
            label5.Name = "label5";
            label5.Size = new Size(78, 20);
            label5.TabIndex = 11;
            label5.Text = "Username:";
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(121, 175);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(156, 27);
            textBoxUsername.TabIndex = 12;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(294, 77);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(90, 54);
            buttonConnect.TabIndex = 13;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Enabled = false;
            buttonDisconnect.Location = new Point(294, 148);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(90, 54);
            buttonDisconnect.TabIndex = 14;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(36, 355);
            label6.Name = "label6";
            label6.Size = new Size(70, 20);
            label6.TabIndex = 15;
            label6.Text = "Message:";
            // 
            // textBoxMessage
            // 
            textBoxMessage.Enabled = false;
            textBoxMessage.Location = new Point(121, 352);
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.Size = new Size(263, 27);
            textBoxMessage.TabIndex = 16;
            // 
            // buttonSend
            // 
            buttonSend.Enabled = false;
            buttonSend.Location = new Point(121, 394);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(263, 30);
            buttonSend.TabIndex = 17;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // logsGeneral
            // 
            logsGeneral.Location = new Point(407, 80);
            logsGeneral.Name = "logsGeneral";
            logsGeneral.Size = new Size(362, 289);
            logsGeneral.TabIndex = 18;
            logsGeneral.Text = "";
            // 
            // logsSPS
            // 
            logsSPS.Location = new Point(1181, 77);
            logsSPS.Name = "logsSPS";
            logsSPS.Size = new Size(362, 289);
            logsSPS.TabIndex = 19;
            logsSPS.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1598, 450);
            Controls.Add(logsSPS);
            Controls.Add(logsGeneral);
            Controls.Add(buttonSend);
            Controls.Add(textBoxMessage);
            Controls.Add(label6);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(textBoxUsername);
            Controls.Add(label5);
            Controls.Add(channelSPS);
            Controls.Add(channelIF);
            Controls.Add(label4);
            Controls.Add(subscriptionSPS);
            Controls.Add(subscriptionIF);
            Controls.Add(label3);
            Controls.Add(logsIF);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxPort);
            Controls.Add(textBoxIP);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxIP;
        private TextBox textBoxPort;
        private Label label1;
        private Label label2;
        private RichTextBox logsIF;
        private Label label3;
        private Button subscriptionIF;
        private Button subscriptionSPS;
        private Label label4;
        private Button channelIF;
        private Button channelSPS;
        private Label label5;
        private TextBox textBoxUsername;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Label label6;
        private TextBox textBoxMessage;
        private Button buttonSend;
        private RichTextBox logsGeneral;
        private RichTextBox logsSPS;
    }
}