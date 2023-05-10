using System;
using System.Windows.Forms;
using TZI.Helpers;
using SimpleRabin;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace TZI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void ceaserBtn_Click(object sender, EventArgs e)
        {
            int key;
            String text = ceaserText.Text;
            try
            {
                key = int.Parse(ceaserKey.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while parsing key!\nThe key will be zero");
                key = 0;
            }
            key = CeaserHelper.KeyGen(key);
            ceaserOutput.Text = "Output:";
            if (checkBox1.Checked) { ceaserOutput.Text += CeaserHelper.Decipher(text, key); }
            else { ceaserOutput.Text += CeaserHelper.Encipher(text, key); }
        }

        private void vijinerBtn_Click(object sender, EventArgs e)
        {
            string text = vijinerText.Text;
            string key = vijinerKey.Text;
            vijinerOutput.Text = "Output:";
            if (vijinerCheck.Checked) { vijinerOutput.Text += VijinerHelper.Decipher(text, key); }
            else { vijinerOutput.Text += VijinerHelper.Encipher(text, key); }
        }

        private void atbashBtn_Click(object sender, EventArgs e)
        {
            string text = atbashText.Text;
            atbashOutput.Text = "Output:";
            if (atbashChecked.Checked) { atbashOutput.Text += AtbashHelper.Decipher(text); }
            else { atbashOutput.Text += AtbashHelper.Encipher(text); }
        }

        private void polibiBtn_Click(object sender, EventArgs e)
        {
            string text = polibiText.Text;
            string key = polibiKey.Text;
            polibiOutput.Text = "Output:";
            if (PolibiCheck.Checked) { polibiOutput.Text += PolibiHelper.Decipher(text, key); }
            else { polibiOutput.Text += PolibiHelper.Encipher(text, key); }
        }

        private void rabinBtn_Click(object sender, EventArgs e)
        {
            string text = rabinText.Text;
            int key;
            try { key = Int32.Parse(rabinKey.Text); }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while parsing key\n" +
                    "It'll be set as 77");
                key = 77;
            }
            Rabin r = new Rabin(key);
            Byte[] arr =  r.GetPublicKey();
            rabinKeyData.Text = "Public key:";
            rabinOne.Text = "Output 1:";
            rabinTwo.Text = "Output 2:";
            rabinThree.Text = "Output 3:";
            RabinFour.Text = "Output 4:";

            foreach(Byte b in arr) { rabinKeyData.Text += b.ToString(); }
            byte[] messageBytes = Encoding.UTF8.GetBytes(text);
            if (rabinCheck.Checked)
            {
                var (pBytes, qBytes) = r.GetPrivateKey();
                var (decrypted1, decrypted2, decrypted3, decrypted4) = Rabin.Decrypt(messageBytes, pBytes, qBytes);
                rabinOne.Text += Encoding.UTF8.GetString(decrypted1);
                rabinTwo.Text += Encoding.UTF8.GetString(decrypted2);
                rabinThree.Text += Encoding.UTF8.GetString(decrypted3);
                RabinFour.Text += Encoding.UTF8.GetString(decrypted4);

            }
            else
            {
                byte[] result = Rabin.Encrypt(messageBytes, arr);
                rabinOne.Text += Encoding.UTF8.GetString(result);
            }
        }

        private void adfgvxBtn_Click(object sender, EventArgs e)
        {
            string text = adfgvxText.Text; 
            string key = adfgvxKey.Text;
            adfgvxOutput.Text = "Output:";
            ADFGVXHelper controller = new ADFGVXHelper(key);
            if(adfgvxCheck.Checked) { adfgvxOutput.Text += controller.DecryptMessage(text); }
            else { adfgvxOutput.Text += controller.EncryptMessage(text); }
        }

        private void aesBtn_Click(object sender, EventArgs e)
        {
            string text = aesText.Text;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.KeySize = 256;
                aesKeyOutput.Text = Encoding.UTF32.GetString(aes.Key);
                byte[] key = aes.Key;
                aesOutput.Text = "Output:";
                if(aesCheck.Checked) { aesOutput.Text += AESHelper.DecryptStringFromBytes(Encoding.UTF32.GetBytes(text.ToCharArray()),key); }
                else { aesOutput.Text += Encoding.UTF32.GetString(AESHelper.EncryptStringToBytes(text, key)); }
            }
        }
    }
}
