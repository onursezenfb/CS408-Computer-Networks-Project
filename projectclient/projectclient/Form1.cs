using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace projectclient
{
    public partial class Form1 : Form
    {
        // Keep the channel messages of IF100 and SPS101 channels separetely
        Dictionary<string, List<string>> channelMessages = new Dictionary<string, List<string>>()
        {
            { "IF100", new List<string>() },
            { "SPS101", new List<string>() }
        };

        string username = "";
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        bool isSubscribedToIF100 = false;
        bool isSubscribedToSPS101 = false;
        string onChannel = "";

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            channelMessages["IF100"].Clear();
            channelMessages["SPS101"].Clear();
            channelMessages["IF100"].Add("Welcome to IF100 Channel"); // Write a welcome message in each channel to describe which channel the user is at
            channelMessages["SPS101"].Add("Welcome to SPS101 Channel");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (textBoxIP.Text != "")
            {
                string IP = textBoxIP.Text;
                username = textBoxUsername.Text;

                int portNum;
                if (Int32.TryParse(textBoxPort.Text, out portNum) && username != "")
                {
                    try
                    {
                        clientSocket.Connect(IP, portNum);

                        // Send your username to the server               
                        Byte[] buffer = Encoding.Default.GetBytes(username);
                        clientSocket.Send(buffer);

                        // Receive message from server to check if successful connection (unique username)
                        Byte[] buffer2 = new Byte[64];
                        int receivedBytesCount = clientSocket.Receive(buffer2);
                        string incomingMessage = Encoding.Default.GetString(buffer2, 0, receivedBytesCount);

                        // Trim the incoming message to remove any whitespace or null characters
                        incomingMessage = incomingMessage.Trim();

                        if (incomingMessage == "OK")
                        {
                            buttonConnect.Enabled = false;
                            buttonDisconnect.Enabled = true;
                            subscriptionIF.Enabled = true;
                            subscriptionSPS.Enabled = true;
                            connected = true;

                            logsGeneral.AppendText("Connected to the server!\n");

                            Thread receiveThread = new Thread(ReceiveData);
                            receiveThread.Start();
                        }
                        else
                        {
                            logsGeneral.AppendText("Not a unique username! Unsuccessful connection to the server!\n");
                        }
                    }
                    catch
                    {
                        logsGeneral.AppendText("Could not connect to the server!\n");
                    }
                }
                else if (username == "")
                {
                    logsGeneral.AppendText("Check the username\n");
                }
                else
                {
                    logsGeneral.AppendText("Check the port\n");
                }
            }
            else
            {
                logsGeneral.AppendText("Check the IP\n");
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        // Method to send data to the server
        private void SendData(string message)
        {
            message = message.Trim();
            Byte[] buffer = Encoding.Default.GetBytes(message);
            clientSocket.Send(buffer);
        }

        private void subscriptionIF_Click(object sender, EventArgs e)
        {
            if (!isSubscribedToIF100)
            {
                // Change button color to green
                subscriptionIF.BackColor = Color.Green;
                channelIF.Enabled = true;
                isSubscribedToIF100 = true;
                // Send subscription message to server
                Byte[] buffer = Encoding.Default.GetBytes("subscribe-IF100");
                clientSocket.Send(buffer);
                logsGeneral.AppendText("You succesfully subscribed to IF100 Channel\n");
            }
            else
            {
                if (onChannel == "IF100")
                {
                    buttonSend.Enabled = false;
                    textBoxMessage.Enabled = false;
                }
                // Change button color to red
                subscriptionIF.BackColor = Color.Red;
                channelIF.Enabled = false;
                isSubscribedToIF100 = false;
                // Send unsubscription message to server
                Byte[] buffer = Encoding.Default.GetBytes("unsubscribe-IF100");
                clientSocket.Send(buffer);
                logsGeneral.AppendText("You succesfully unsubscribed from IF100 Channel\n");
                channelIF.BackColor = Color.LightGray;
            }
        }

        private void subscriptionSPS_Click(object sender, EventArgs e)
        {
            if (!isSubscribedToSPS101)
            {
                // Change button color to green
                subscriptionSPS.BackColor = Color.Green;
                channelSPS.Enabled = true;
                isSubscribedToSPS101 = true;
                // Send subscription message to server
                Byte[] buffer = Encoding.Default.GetBytes("subscribe-SPS101");
                clientSocket.Send(buffer);
                logsGeneral.AppendText("You succesfully subscribed to SPS101 Channel\n");
            }
            else
            {
                if (onChannel == "SPS101")
                {
                    buttonSend.Enabled = false;
                    textBoxMessage.Enabled = false;
                }
                // Change button color to red
                subscriptionSPS.BackColor = Color.Red;
                channelSPS.Enabled = false;
                isSubscribedToSPS101 = false;
                // Send unsubscription message to server
                Byte[] buffer = Encoding.Default.GetBytes("unsubscribe-SPS101");
                clientSocket.Send(buffer);
                logsGeneral.AppendText("You succesfully unsubscribed from SPS101 Channel\n");
                channelSPS.BackColor = Color.LightGray;
            }
        }

        // Method to update message display every time a new message is received from the server
        private void UpdateMessageDisplay(string channel)
        {
            // Invoke this on the UI thread if this is being called from another thread
            this.Invoke(new Action(() =>
            {
                if (channel == "IF100" && isSubscribedToIF100)
                {
                    // Display IF100 messages
                    logsIF.Clear();
                    logsIF.AppendText(string.Join(Environment.NewLine, channelMessages["IF100"]));
                }
                if (channel == "SPS101" && isSubscribedToSPS101)
                {
                    // Display SPS101 messages
                    logsSPS.Clear();
                    logsSPS.AppendText(string.Join(Environment.NewLine, channelMessages["SPS101"]));
                }
            }));
        }

        // Method to receive data from the server
        private void ReceiveData()
        {
            // This method should be running in a background thread or task
            // since it will be continuously receiving data from the server
            while (connected) // Assume 'connected' is a flag indicating the client is connected
            {
                try
                {
                    // Code to receive data from the server
                    // This could be from a TCP socket, for example
                    // Received messages will be in the format "channel:message"
                    Byte[] buffer = new Byte[1024];
                    int receivedBytesCount = clientSocket.Receive(buffer);
                    string receivedData = Encoding.Default.GetString(buffer, 0, receivedBytesCount);
                    string[] splitData = receivedData.Split(new[] { '-' }, 2);
                    string channel = splitData[0];
                    string message = splitData[1];

                    // Add the message to the appropriate channel's message list if subscribed
                    if (channelMessages.ContainsKey(channel))
                    {
                        //logs.AppendText("Contains the key!\n");
                        channelMessages[channel].Add(message);
                        UpdateMessageDisplay(channel);
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        ResetButtons();
                        logsGeneral.AppendText("The server has disconnected\n");
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void ResetButtons()
        {
            connected = false;
            buttonConnect.Enabled = true;
            textBoxMessage.Enabled = false;
            logsIF.Clear();
            logsSPS.Clear();
            logsGeneral.Clear();
            buttonDisconnect.Enabled = false;
            subscriptionIF.Enabled = false;
            subscriptionSPS.Enabled = false;
            subscriptionIF.BackColor = Color.IndianRed;
            subscriptionSPS.BackColor = Color.IndianRed;
            channelIF.BackColor = Color.LightGray;
            channelSPS.BackColor = Color.LightGray;
            channelIF.Enabled = false;
            channelSPS.Enabled = false;
            buttonSend.Enabled = false;
            textBoxMessage.Clear();
            textBoxUsername.Clear();
            textBoxPort.Clear();
            textBoxIP.Clear();
        }

        private void SetCurrentChannel(string channelName)
        {
            onChannel = channelName;
        }

        private void channelIF_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string channelName = button.Text;
            channelIF.BackColor = Color.Green;
            channelSPS.BackColor = Color.LightGray;
            buttonSend.Enabled = true;
            textBoxMessage.Enabled = true;
            SetCurrentChannel(channelName);
            UpdateMessageDisplay(channelName);
        }

        private void channelSPS_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string channelName = button.Text;
            channelIF.BackColor = Color.LightGray;
            channelSPS.BackColor = Color.Green;
            buttonSend.Enabled = true;
            textBoxMessage.Enabled = true;
            SetCurrentChannel(channelName);
            UpdateMessageDisplay(channelName);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text == "")
            {
                logsGeneral.AppendText("You can't send an empty message!\n");
            }
            else
            {
                string message = (onChannel + "-" + username + ": " + textBoxMessage.Text).Trim();
                SendData(message);
                textBoxMessage.Clear();
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            ResetButtons();
            string message = "disconnected-" + username;
            channelMessages["IF100"].Clear();
            channelMessages["SPS101"].Clear();
            channelMessages["IF100"].Add("Welcome to IF100 Channel");
            channelMessages["SPS101"].Add("Welcome to SPS101 Channel");
            SendData(message);
            logsGeneral.AppendText("You have disconnected...");
        }
    }
}