using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace projectserver
{
    public partial class Form1 : Form
    {
        List<string> connectedUsernames = new List<string>(); // keep the connected usernames as a list
        Dictionary<string, Socket> IF100_subscribers = new Dictionary<string, Socket>(); // keep the subscribers as a dict (key: username, value: client socket)
        Dictionary<string, Socket> SPS101_subscribers = new Dictionary<string, Socket>();

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBoxPort.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3); // 3 represents max length of the pending connections queue

                listening = true;
                buttonListen.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                richTextBoxServer.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                richTextBoxServer.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);

                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        richTextBoxServer.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket clientSocket)
        {
            bool isFirstMessage = true;
            string username = "";

            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int received = clientSocket.Receive(buffer);
                    if (received == 0) // if client no longer sends anything to the server, it means that the client socket is disconnected
                    {
                        clientSockets.Remove(clientSocket);
                        connectedUsernames.Remove(username);
                        UpdateDisplayConnected();
                        richTextBoxServer.AppendText(username + " has disconnected.\n");
                        break;
                    }
                    string text = Encoding.ASCII.GetString(buffer, 0, received);

                    if (isFirstMessage) // the first message from the client includes the username
                    {
                        username = text;
                        if (connectedUsernames.Contains(username)) // check if username is unique
                        {
                            string rejectMessage = "Client connection is unsuccessful. Username " + username + " already in use.";
                            richTextBoxServer.AppendText(rejectMessage+"\n");
                            byte[] rejectData = Encoding.Default.GetBytes(rejectMessage);
                            clientSocket.Send(rejectData);
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                            break;
                        }
                        else
                        {
                            richTextBoxServer.AppendText(username + " has connected.\n");
                            connectedUsernames.Add(username);
                            UpdateDisplayConnected();
                            isFirstMessage = false;
                            string acceptMessage = "OK";
                            byte[] acceptData = Encoding.Default.GetBytes(acceptMessage);
                            clientSocket.Send(acceptData);
                        }
                    }
                    else
                    {
                        string[] splitData = text.Split(new[] { '-' }, 2);
                        string function = splitData[0];
                        string message = splitData[1];

                        //richTextBoxServer.AppendText("Function: " + function + "\n");
                        //richTextBoxServer.AppendText("Message: " + message + "\n");
                        if (function == "subscribe")
                        {
                            if (message == "IF100")
                            {
                                IF100_subscribers.Add(username, clientSocket);
                                richTextBoxServer.AppendText(username + " has subscribed to IF100 Channel!\n");
                                UpdateDisplayIF();
                            }
                            else
                            {
                                SPS101_subscribers.Add(username, clientSocket);
                                richTextBoxServer.AppendText(username + " has subscribed to SPS101 Channel!\n");
                                UpdateDisplaySPS();
                            }
                        }
                        else if (function == "unsubscribe")
                        {
                            if (message == "IF100")
                            {
                                IF100_subscribers.Remove(username);
                                richTextBoxServer.AppendText(username + " has unsubscribed from IF100 Channel!\n");
                                UpdateDisplayIF();
                            }
                            else
                            {
                                SPS101_subscribers.Remove(username);
                                richTextBoxServer.AppendText(username + " has unsubscribed from SPS101 Channel!\n");
                                UpdateDisplaySPS();
                            }
                        }
                        else if (function == "disconnected")
                        {
                            // We remove the username from subscribers upon disconnection
                            // We also disconnect the client socket and then remove it from the subscribers dictionary
                            if (IF100_subscribers.ContainsKey(message))
                            {
                                IF100_subscribers.Remove(message);
                                UpdateDisplayIF();
                                richTextBoxServer.AppendText("User " + message + " has been removed from IF100 channel.\n");
                            }
                            if (SPS101_subscribers.ContainsKey(message))
                            {
                                SPS101_subscribers.Remove(message);
                                UpdateDisplaySPS();
                                richTextBoxServer.AppendText("User " + message + " has been removed from SPS101 channel.\n");
                            }
                            connectedUsernames.Remove(message);
                            UpdateDisplayConnected();
                            clientSockets.Remove(clientSocket);
                            clientSocket.Shutdown(SocketShutdown.Both);
                            clientSocket.Close();
                        }
                        else
                        {
                            string[] splitMessage = message.Split(new[] { ':' }, 2);
                            string usernameMessage = splitMessage[0];
                            if (function == "IF100")
                            {
                                richTextBoxServer.AppendText("Message received for IF100 channel from " + usernameMessage + "\n");
                                string fullMessage = "IF100-" + message;
                                //richTextBoxServer.AppendText("Sending this: " + fullMessage + "\n");
                                byte[] dataToSend = Encoding.Default.GetBytes(fullMessage);
                                foreach (string nameuser in IF100_subscribers.Keys)
                                {
                                    Socket user = IF100_subscribers[nameuser];
                                    richTextBoxServer.AppendText("Trying to send a message by " + nameuser + " to IF100 channel...\n");
                                    if (IsSocketConnected(user))
                                    {
                                        try
                                        {
                                            user.Send(dataToSend);
                                            //richTextBoxServer.AppendText("sent");
                                        }
                                        catch (SocketException)
                                        {
                                            // Handle the disconnection case
                                            richTextBoxServer.AppendText("Subscriber " + nameuser + " has disconnected unexpectedly while sending the message.\n");
                                            IF100_subscribers.Remove(nameuser); // Remove the disconnected subscriber
                                            UpdateDisplayIF();
                                            connectedUsernames.Remove(nameuser);
                                            UpdateDisplayConnected();
                                            clientSockets.Remove(clientSocket);
                                            clientSocket.Shutdown(SocketShutdown.Both);
                                            clientSocket.Close();
                                        }
                                    }
                                    richTextBoxServer.AppendText("Message by " + nameuser + " is sent to IF100 channel!\n\n");
                                }
                            }

                            else
                            {
                                richTextBoxServer.AppendText("Message received for SPS101 channel from " + usernameMessage + "\n");
                                string fullMessage = "SPS101-" + message;
                                //richTextBoxServer.AppendText("Sending this: " + fullMessage + "\n");
                                byte[] dataToSend = Encoding.Default.GetBytes(fullMessage);
                                foreach (string nameuser in SPS101_subscribers.Keys)
                                {
                                    Socket user = SPS101_subscribers[nameuser];
                                    richTextBoxServer.AppendText("Trying to send a message by " + nameuser + " to SPS101 channel...\n");
                                    if (IsSocketConnected(user))
                                    {
                                        try
                                        {
                                            user.Send(dataToSend);
                                            //richTextBoxServer.AppendText("sent");
                                        }
                                        catch (SocketException)
                                        {
                                            // Handle the disconnection case
                                            richTextBoxServer.AppendText("Subscriber " + nameuser + " has disconnected unexpectedly while sending the message.\n");
                                            SPS101_subscribers.Remove(nameuser); // Remove the disconnected subscriber
                                            UpdateDisplaySPS();
                                            connectedUsernames.Remove(nameuser);
                                            UpdateDisplayConnected();
                                            clientSockets.Remove(clientSocket);
                                            clientSocket.Shutdown(SocketShutdown.Both);
                                            clientSocket.Close();
                                        }
                                    }
                                    richTextBoxServer.AppendText("Message by " + nameuser + " is sent to SPS101 channel!\n\n");
                                }
                            }
                        }
                    }
                }
                catch
                {
                    if (!string.IsNullOrEmpty(username))
                    {
                        connectedUsernames.Remove(username);
                        UpdateDisplayConnected();
                        if (IF100_subscribers.Keys.Contains(username))
                        {
                            IF100_subscribers.Remove(username);
                            UpdateDisplayIF();
                        }
                        if (SPS101_subscribers.Keys.Contains(username))
                        {
                            SPS101_subscribers.Remove(username);
                            UpdateDisplaySPS();
                        }
                    }
                    clientSockets.Remove(clientSocket);
                    richTextBoxServer.AppendText(username + " has disconnected.\n");
                    break;
                }
            }
        }

        // Handle form closing to prevent unexpected behavior
        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            foreach (Socket clientSocket in clientSockets)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            serverSocket.Close();
            connectedUsernames.Clear();
            Environment.Exit(0);
        }

        // Helper method to check if a socket is still connected
        private bool IsSocketConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }

        // Subscribers display is updated each time a user subscribes or unsubscribes
        private void UpdateDisplayIF()
        {
            richTextBoxIF.Clear();
            richTextBoxIF.AppendText("IF100 Subscribers:\n");
            foreach (string username in IF100_subscribers.Keys)
            {
                richTextBoxIF.AppendText(username + "\n");
            }
        }

        private void UpdateDisplaySPS()
        {
            richTextBoxSPS.Clear();
            richTextBoxSPS.AppendText("SPS101 Subscribers:\n");
            foreach (string username in SPS101_subscribers.Keys)
            {
                richTextBoxSPS.AppendText(username + "\n");
            }
        }

        // Connected display is updated each time a user connects or disconnects
        private void UpdateDisplayConnected()
        {
            richTextBoxConnected.Clear();
            richTextBoxConnected.AppendText("Connected Users:\n");
            foreach (string username in connectedUsernames)
            {
                richTextBoxConnected.AppendText(username + "\n");
            }
        }
    }
}