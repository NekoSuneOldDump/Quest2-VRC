﻿using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Quest2_VRC.PacketSender;

namespace Quest2_VRC
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            
            InitializeComponent();
            Invoke((MethodInvoker)(() => { Invoke((MethodInvoker)(() => { Console.SetOut(new TBStreamWriter(materialMultiLineTextBox1)); })); }));
            Check_Vars.CheckVars();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            materialMaskedTextBox1.Text = IPAddress.Loopback.ToString();
        }


        private void materialButton2_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { Invoke((MethodInvoker)(() => { Console.SetOut(new TBStreamWriter(materialMultiLineTextBox1)); })); }));

            ADB.StartADB(true, true);
            materialLabel3.Text = "ADB is running";
            materialButton1.Enabled = false;
            materialButton2.Enabled = false;
            materialButton3.Enabled = false;
            materialButton4.Enabled = false;

        }
        private void materialButton3_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { Invoke((MethodInvoker)(() => { Console.SetOut(new TBStreamWriter(materialMultiLineTextBox1)); })); }));
            OSC.StartOSC(false, true);
            materialLabel3.Text = "No ADB mode";
            materialButton1.Enabled = false;
            materialButton2.Enabled = false;
            materialButton3.Enabled = false;
            materialButton4.Enabled = false;

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { Invoke((MethodInvoker)(() => { Console.SetOut(new TBStreamWriter(materialMultiLineTextBox1)); })); }));
            ADB.StartADB(false, true);
            materialLabel3.Text = "Recive only";
            materialButton1.Enabled = false;
            materialButton2.Enabled = false;
            materialButton3.Enabled = false;
            materialButton4.Enabled = false;
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Invoke((MethodInvoker)(() => { Invoke((MethodInvoker)(() => { Console.SetOut(new TBStreamWriter(materialMultiLineTextBox1)); })); }));
            ADB.StartADB(true, false);
            materialLabel3.Text = "Send only";
            materialButton1.Enabled = false;
            materialButton2.Enabled = false;
            materialButton3.Enabled = false;
            materialButton4.Enabled = false;
        }



        public class TBStreamWriter : TextWriter
        {
            MaterialMultiLineTextBox _output = null;
            public TBStreamWriter(MaterialMultiLineTextBox output)
            {
                _output = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                _output.BeginInvoke((MethodInvoker)(() =>
                {
                    _output.AppendText(value.ToString());
                    _output.ScrollToCaret();
                }));
            }
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit and stop ADB",
        MessageBoxButtons.YesNo) == DialogResult.No)
            {                 
                e.Cancel = true;
            }
            else
            {
                Console.WriteLine("Exiting program due to external CTRL-C, or process kill, or shutdown, or program exiting");
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C platform-tools\\adb.exe kill-server";
                process.StartInfo = startInfo;
                process.Start();
            }

        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            
            IPString = materialMaskedTextBox1.Text;
            materialLabel4.Text = "IP set to: " + IPString;
            
            
            
        }
    }
}
