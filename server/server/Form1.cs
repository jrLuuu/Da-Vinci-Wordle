using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace server
{
    public partial class Form1 : Form
    {
        TcpListener Server;
        Socket Client;
        Thread Thread_Server;
        Thread Thread_Client;
        Hashtable HT = new Hashtable();

        bool secret = false, wordle = false;
        int ans = 0, mini = 0, maxi = 100; 
        int num_used_counts, RndBuffer = 0; // random value for question numbers
        string user_ans = "";
        // records the question numbers that has already been used
        int[] num_used = new int[30] { -1, -1, -1, -1, -1,
                                       -1, -1, -1, -1, -1,
                                       -1, -1, -1, -1, -1,
                                       -1, -1, -1, -1, -1,
                                       -1, -1, -1, -1, -1,
                                       -1, -1, -1, -1, -1};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trigger_button_Click_1(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread_Server = new Thread(ServerSub);
            Thread_Server.IsBackground = true; // set up the background
            Thread_Server.Start(); // trigger the listening mode
            trigger_button.Enabled = false;
        }

        private void ServerSub() // accept connection request from client
        {
            string IP = ip_textBox.Text;
            int Port = int.Parse(port_textBox.Text);
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);
            Server = new TcpListener(EP);
            Server.Start(100);
            while (true)
            {
                Client = Server.AcceptSocket();
                Thread_Client = new Thread(Listen);
                Thread_Client.IsBackground = true;
                Thread_Client.Start();
            }
        }

        private void Listen() // listen the contents that client just sent
        {
            Socket socket = Client;
            Thread thread = Thread_Client;
            while (true)
            {
                try
                {
                    byte[] BT = new byte[1023]; // array for receiving
                    int inLen = socket.Receive(BT); // receive messages
                    string Msg = Encoding.Default.GetString(BT, 0, inLen);
                    string Cmd = Msg.Substring(0, 1);
                    string Str = Msg.Substring(1);
                    switch (Cmd) // status
                    {
                        case "0": // connected
                            HT.Add(Str, socket);
                            players_listBox.Items.Add(Str);
                            break;
                        case "8": // game selection
                            if (Str[0] == '1') 
                            {
                                mode_textBox.Text = "DA-VINCI CODE";
                                secret = true;
                                wordle = false;
                            }
                            else if (Str[0] == '2')
                            {
                                mode_textBox.Text = "WORDLE";
                                secret = false;
                                wordle = true;
                            }
                            break;
                        case "9": // disconnected
                            HT.Remove(Str);
                            players_listBox.Items.Remove(Str);
                            thread.Abort();
                            break;
                        case "1": // check the answer
                            string user_ans = "", temp = Str.Substring(0, 3);
                            string user_name = Str.Substring(1);
                            if (secret)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (temp[i] >= '0' && temp[i] <= '9')
                                    {
                                        user_ans += temp[i];
                                    }
                                }
                            }
                            if (wordle)
                            {
                                user_ans = Str.Substring(0, 5);
                            }
                            CheckAns(RndBuffer, user_ans, user_name);
                            break;
                    }
                }
                catch (Exception)
                {
                    // ignore when error occurs
                }
            }
        }

        private void SendClear(string Str) // send messages to online players
        {
            string new_str = "3" + Str;
            byte[] BT = Encoding.Default.GetBytes(new_str);
            foreach (Socket s in HT.Values)
            {
                s.Send(BT, 0, BT.Length, SocketFlags.None);
            }
        }
        
        private void SendAns(string Str) // send messages to online players
        {
            string new_str = "4" + Str;
            byte[] BT = Encoding.Default.GetBytes(new_str);
            foreach (Socket s in HT.Values)
            {
                s.Send(BT, 0, BT.Length, SocketFlags.None);
            }
        }

        private void SendAll(string Str) // send messages to online players
        {
            string new_str = "5" + Str;
            byte[] BT = Encoding.Default.GetBytes(new_str);
            foreach (Socket s in HT.Values)
            {
                s.Send(BT, 0, BT.Length, SocketFlags.None);
            }
        }

        private void send_button_Click_1(object sender, EventArgs e)
        {
            ans = 0;
            mini = 0;
            maxi = 100;
            SendAll("Welcome");
            SendClear("Clear");
            if (secret)
            {
                Random crandom = new Random(); // trigger random questions
                ans = crandom.Next(0, 101); // range between 0~100
                question_textBox.Text = ans.ToString();
                SendAns(ans.ToString());
            }
            if (wordle)
            {
                question_textBox.Text = user_ans;
                string SQ = Quiz();
                SendAns(SQ);
                SendClear("Clear");
            }
        }

        private string Quiz() // questions
        {
            string[] Que = new string[] { "which", "there", "their", "about", "would",
                                          "these", "other", "words", "could", "write",
                                          "first", "water", "after", "where", "right",
                                          "think", "three", "years", "place", "sound",
                                          "great", "again", "still", "every", "small",
                                          "found", "those", "never", "under", "might" };

            Random crandom = new Random(); // trigger random questions
            RndBuffer = crandom.Next(0, 30); // range between 0~29
            for (int i = 0; i < num_used_counts; i++)
            {
                if (RndBuffer == num_used[i])
                {
                    RndBuffer = crandom.Next(0, 30); // range between 0~29
                }
                else
                {
                    break;
                }
            }

            question_textBox.Text = Que[RndBuffer];
            num_used[num_used_counts] = RndBuffer;
            num_used_counts++;
            return Que[RndBuffer];
        }

        private void CheckAns(int question_num, string user_ans, string user_name)
        {
            if (secret)
            {
                if (user_ans == ans.ToString()) // correct
                {
                    int grade = 1;
                    string grade_str;
                    grade_str = grade.ToString();
                    byte[] BT = Encoding.Default.GetBytes("6" + grade_str);
                    foreach (Socket s in HT.Values)
                    {
                        s.Send(BT, 0, BT.Length, SocketFlags.None);
                    }
                }
                else // incorrect
                {
                    byte[] BT = Encoding.Default.GetBytes("6" + "0");
                    foreach (Socket s in HT.Values)
                    {
                        s.Send(BT, 0, BT.Length, SocketFlags.None);
                    }
                }

                int int_user_ams;
                int_user_ams = int.Parse(user_ans);
                if (int_user_ams < ans && int_user_ams > mini)
                {
                    mini = int_user_ams;
                    string SQ = int_user_ams + "~" + maxi;
                    SendAll(SQ); // broadcast
                }
                else if (int_user_ams > ans && int_user_ams < maxi)
                {
                    maxi = int_user_ams;
                    string SQ = mini + "~" + maxi;
                    SendAll(SQ); // broadcast
                }
            }
            if (wordle)
            {
                string[] question_ans = new string[30] { "which", "there", "their", "about", "would",
                                                         "these", "other", "words", "could", "write",
                                                         "first", "water", "after", "where", "right",
                                                         "think", "three", "years", "place", "sound",
                                                         "great", "again", "still", "every", "small",
                                                         "found", "those", "never", "under", "might" };

                if (user_ans == question_ans[question_num]) // correct
                {
                    int grade = 1;
                    string grade_str;
                    grade_str = grade.ToString();
                    byte[] BT = Encoding.Default.GetBytes("6" + grade_str);
                    foreach (Socket s in HT.Values)
                    {
                        s.Send(BT, 0, BT.Length, SocketFlags.None);
                    }
                }
                else // incorrect
                {
                    byte[] BT = Encoding.Default.GetBytes("6" + "0");
                    foreach (Socket s in HT.Values)
                    {
                        s.Send(BT, 0, BT.Length, SocketFlags.None);
                    }
                }
            }
        }
        
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}