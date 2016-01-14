using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Drive_Inspector
{
    public partial class Form_Password : Form
    {
        public Form_Password()
        {
            InitializeComponent();
            tbox_pass.Focus();

        }

     
        public string Password { get; set; }

        private void Form_Password_Load(object sender, EventArgs e)
        {

        }

        public string Encript(string s)
        {
            var encriptor = new MD5CryptoServiceProvider();
            byte[] bytes = new byte[s.Length];

            for (int i = 0; i < s.Length; i++)
                bytes[i] = BitConverter.GetBytes(s[i])[0];

            encriptor.ComputeHash(bytes);

            return BitConverter.ToString(encriptor.Hash);

        }

        private void accept_bttn_Click(object sender, EventArgs e)
        {
            Password = Encript(tbox_pass.Text);
       
        }

        private void tbox_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
