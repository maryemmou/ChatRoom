using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatRoom
{
    public partial class Room : Form
    {
        private string username;
        private Boolean isInitial = true;
        public Room(string username)
        {
            this.username = username;
            InitializeComponent();
            this.label1.Text = this.username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserController.Leave(this.username);
            this.Hide();
            Auth form = new Auth();
            form.Show();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void RefreshMessages()
        {
            string result = "";
            foreach (Message message in MessageController.getMessages())
            {
                string row = ""; 
                if (message.message.Equals("__joined__"))
                {
                    row = "User " + message.user.username + " has joined the chat. 🎉";
                }else if (message.message.Equals("__left__"))
                {
                    row = "User " + message.user.username + " has left the chat 👋";
                }
                else
                {
                    row = message.user.username + ":" + message.message;
                }

                result += row + Environment.NewLine;
            }

            if (!this.textBox1.Text.Equals(result))
            {
                this.textBox1.Text = "";
                this.textBox1.AppendText(result);
            }
        }

        public void RefreshActiveList()
        {
            string result = "";
            foreach (User user in UserController.getActiveUsers())
            {
                result += "✔ " + user.username + "\n";
            }

            if (!result.Equals(this.activeLabel.Text)) this.activeLabel.Text = result;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.RefreshActiveList();

            this.RefreshMessages();

            if (isInitial)
            {
                this.textBox1.SelectionStart = this.textBox1.TextLength;
                this.textBox1.ScrollToCaret();
                this.isInitial = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageController.Send(this.richTextBox1.Text, this.username);
            this.richTextBox1.Text = "";
            this.RefreshMessages();
        }

        private void Room_Load(object sender, EventArgs e)
        {
            this.RefreshActiveList();
            this.RefreshMessages();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
