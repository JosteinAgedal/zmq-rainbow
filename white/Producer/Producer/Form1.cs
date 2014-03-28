using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZeroMQ;

namespace Producer
{
	public partial class Form1 : Form
	{
		private long i = 0;

		public Form1()
		{
			InitializeComponent();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			timer1.Interval = 1000;
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			i++;
			timer1.Stop();
			try
			{
				using (var context = ZmqContext.Create())
				{
					using (var socket = context.CreateSocket(SocketType.DEALER))
					{
						socket.Connect("tcp://127.0.0.1:30000");
						//socket.SendTimeout = new TimeSpan(0, 0, 0, 2);
						socket.Send("Message No." + i, Encoding.UTF8);
						txtLog.Text += "Message No." + i + Environment.NewLine;
					}
				}

			}
			catch
			{				
			}
			timer1.Start();
		}



	}
}
