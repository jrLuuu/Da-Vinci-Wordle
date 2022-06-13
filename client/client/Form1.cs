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
using System.Media;
using WMPLib;

namespace client
{
    public partial class Form1 : Form
    {
        Socket T;
        Thread Th;
        string User;
        int UserScore = 0;
        int count = 1; // row of wordle
        bool secret = false, wordle = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void logIn_button_Click(object sender, EventArgs e)
        {
            string IP = ip_textBox.Text; // server ip
            int Port = int.Parse(port_textBox.Text); // server port
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse(IP), Port);

            // build up TCP for bidirectional communication
            T = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            User = name_textBox.Text; // username

            try
            {
                T.Connect(EP); // connect to the endpoint of server
                Send("0" + User); // send username to server
                Th = new Thread(Listen);
                Th.IsBackground = true;
                Th.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect the server!", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            logIn_button.Enabled = false;
        }

        private void secret_button_Click(object sender, EventArgs e)
        {
            secret = true;
            wordle = false;
            Send("8" + "1" + User); // play DA-VINCI CODE
        }

        private void wordle_button_Click(object sender, EventArgs e)
        {
            secret = false;
            wordle = true;
            Send("8" + "2" + User); // play WORDLE
        }

        private void Send(string Str) // send messages to server
        {
            byte[] BT = Encoding.Default.GetBytes(Str);
            T.Send(BT, 0, BT.Length, SocketFlags.None);
        }

        private void Listen() // listen the contents that server just sent
        {
            EndPoint ServerEP = (EndPoint)T.RemoteEndPoint; // endpoint of server
            while (true)
            {
                try
                {
                    byte[] BT = new byte[1023]; // array for receiving
                    int inLen = 0; // numbers of receive bytes
                    inLen = T.ReceiveFrom(BT, ref ServerEP); // receive messages
                    string Msg = Encoding.Default.GetString(BT, 0, inLen);
                    string Cmd = Msg.Substring(0, 1);
                    string Str = Msg.Substring(1);
                    switch (Cmd) // status
                    {
                        case "3": // clear wordle textboxs
                            Msg = Encoding.Default.GetString(BT, 0, inLen);
                            this.Invoke(new Action(() => { Clear(); }));
                            break;
                        case "4": // show answer
                            Msg = Encoding.Default.GetString(BT, 0, inLen);
                            this.Invoke(new Action(() => { realAns_textBox.Text = Msg; }));
                            break;
                        case "5": // show question
                            Msg = Encoding.Default.GetString(BT, 0, inLen);
                            this.Invoke(new Action(() => { questoin_textBox.Text = Msg.Substring(1); }));
                            break;
                        case "6": // show score 
                            int get_score = int.Parse(Str);
                            UserScore = UserScore + get_score;
                            CheckCorrect(get_score);
                            this.Invoke(new Action(() => { score_textBox.Text = UserScore.ToString(); }));
                            break;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void Clear()
        {
            textBox1_1.Text = ""; textBox1_2.Text = ""; textBox1_3.Text = ""; textBox1_4.Text = ""; textBox1_5.Text = "";
            textBox2_1.Text = ""; textBox2_2.Text = ""; textBox2_3.Text = ""; textBox2_4.Text = ""; textBox2_5.Text = "";
            textBox3_1.Text = ""; textBox3_2.Text = ""; textBox3_3.Text = ""; textBox3_4.Text = ""; textBox3_5.Text = "";
            textBox4_1.Text = ""; textBox4_2.Text = ""; textBox4_3.Text = ""; textBox4_4.Text = ""; textBox4_5.Text = "";
            textBox5_1.Text = ""; textBox5_2.Text = ""; textBox5_3.Text = ""; textBox5_4.Text = ""; textBox5_5.Text = "";
            textBox6_1.Text = ""; textBox6_2.Text = ""; textBox6_3.Text = ""; textBox6_4.Text = ""; textBox6_5.Text = "";

            textBox1_1.BackColor = Color.White; textBox1_2.BackColor = Color.White;
            textBox1_3.BackColor = Color.White; textBox1_4.BackColor = Color.White;
            textBox1_5.BackColor = Color.White;
            textBox2_1.BackColor = Color.White; textBox2_2.BackColor = Color.White;
            textBox2_3.BackColor = Color.White; textBox2_4.BackColor = Color.White;
            textBox2_5.BackColor = Color.White;
            textBox3_1.BackColor = Color.White; textBox3_2.BackColor = Color.White;
            textBox3_3.BackColor = Color.White; textBox3_4.BackColor = Color.White;
            textBox3_5.BackColor = Color.White;
            textBox4_1.BackColor = Color.White; textBox4_2.BackColor = Color.White;
            textBox4_3.BackColor = Color.White; textBox4_4.BackColor = Color.White;
            textBox4_5.BackColor = Color.White;
            textBox5_1.BackColor = Color.White; textBox5_2.BackColor = Color.White;
            textBox5_3.BackColor = Color.White; textBox5_4.BackColor = Color.White;
            textBox5_5.BackColor = Color.White;
            textBox6_1.BackColor = Color.White; textBox6_2.BackColor = Color.White;
            textBox6_3.BackColor = Color.White; textBox6_4.BackColor = Color.White;
            textBox6_5.BackColor = Color.White;
        }

        void CheckColor()
        {
            if(count == 1)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox1_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox1_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox1_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox1_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox1_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox1_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox1_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox1_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox1_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox1_5.BackColor = Color.LightGreen; }
            }
            else if(count == 2)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox2_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox2_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox2_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox2_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox2_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox2_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox2_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox2_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox2_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox2_5.BackColor = Color.LightGreen; }
            }
            else if(count == 3)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox3_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox3_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox3_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox3_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox3_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox3_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox3_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox3_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox3_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox3_5.BackColor = Color.LightGreen; }
            }
            else if(count == 4)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox4_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox4_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox4_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox4_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox4_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox4_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox4_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox4_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox4_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox4_5.BackColor = Color.LightGreen; }
            }
            else if(count == 5)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox5_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox5_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox5_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox5_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox5_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox5_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox5_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox5_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox5_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox5_5.BackColor = Color.LightGreen; }
            }
            else if(count == 6)
            {
                for (int i = 1; i < 6; i++)
                {
                    if (myAnswer_textBox.Text[0] == realAns_textBox.Text[i] && i != 1) { textBox6_1.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[1] == realAns_textBox.Text[i] && i != 2) { textBox6_2.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[2] == realAns_textBox.Text[i] && i != 3) { textBox6_3.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[3] == realAns_textBox.Text[i] && i != 4) { textBox6_4.BackColor = Color.Yellow; }
                    if (myAnswer_textBox.Text[4] == realAns_textBox.Text[i] && i != 5) { textBox6_5.BackColor = Color.Yellow; }
                }
                if (myAnswer_textBox.Text[0] == realAns_textBox.Text[1]) { textBox6_1.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[1] == realAns_textBox.Text[2]) { textBox6_2.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[2] == realAns_textBox.Text[3]) { textBox6_3.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[3] == realAns_textBox.Text[4]) { textBox6_4.BackColor = Color.LightGreen; }
                if (myAnswer_textBox.Text[4] == realAns_textBox.Text[5]) { textBox6_5.BackColor = Color.LightGreen; }
            }
        }

        private void check_button_Click(object sender, EventArgs e)
        {
            if (secret)
            {
                ;
            }
            if (wordle)
            {
                CheckColor();
                if (count == 1)
                {
                    textBox1_1.Text = myAnswer_textBox.Text[0].ToString(); textBox1_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox1_3.Text = myAnswer_textBox.Text[2].ToString(); textBox1_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox1_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                else if (count == 2)
                {
                    textBox2_1.Text = myAnswer_textBox.Text[0].ToString(); textBox2_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox2_3.Text = myAnswer_textBox.Text[2].ToString(); textBox2_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox2_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                else if (count == 3)
                {
                    textBox3_1.Text = myAnswer_textBox.Text[0].ToString(); textBox3_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox3_3.Text = myAnswer_textBox.Text[2].ToString(); textBox3_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox3_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                else if (count == 4)
                {
                    textBox4_1.Text = myAnswer_textBox.Text[0].ToString(); textBox4_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox4_3.Text = myAnswer_textBox.Text[2].ToString(); textBox4_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox4_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                else if (count == 5)
                {
                    textBox5_1.Text = myAnswer_textBox.Text[0].ToString(); textBox5_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox5_3.Text = myAnswer_textBox.Text[2].ToString(); textBox5_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox5_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                else if (count == 6)
                {
                    textBox6_1.Text = myAnswer_textBox.Text[0].ToString(); textBox6_2.Text = myAnswer_textBox.Text[1].ToString();
                    textBox6_3.Text = myAnswer_textBox.Text[2].ToString(); textBox6_4.Text = myAnswer_textBox.Text[3].ToString();
                    textBox6_5.Text = myAnswer_textBox.Text[4].ToString();
                }
                if (count > 6 || "4" + myAnswer_textBox.Text == realAns_textBox.Text)
                {
                    count = 0;
                }
                count++;
            }

            string myAns = "1" + myAnswer_textBox.Text + User;
            Send(myAns);
        }

        private void CheckCorrect(int get_score)
        {
            if (get_score == 0)
            {
                MessageBox.Show("Incorrect Answer!", "DING-DONG", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Boom_pictureBox.Image = client.Properties.Resources.boom;
                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer();
                SoundPlayer sp2 = new SoundPlayer("boom.wav");
                sp2.Play();
                MessageBox.Show("Correct Answer!", "BING-BONG", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Boom_pictureBox.Image = client.Properties.Resources.black;
            }
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (logIn_button.Enabled == false)
            {
                Send("9" + User);
                T.Close();
            }
        }
    }
}