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

namespace Store
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			timer1.Interval = 500;
			timer1.Start();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();

			try
			{
				using (var context = ZmqContext.Create())
				{
					using (var frontend = context.CreateSocket(SocketType.DEALER))
					{						
						frontend.Bind("tcp://127.0.0.1:30000");
						var backend = context.CreateSocket(SocketType.PUB);
						backend.Bind("tcp://127.0.0.1:30002");
						while (true)
						{
							var rcvdMsg = frontend.Receive(Encoding.UTF8);
							//txtLog.Text += rcvdMsg + Environment.NewLine;

							backend.SendMore("White", Encoding.UTF8);
							backend.Send(rcvdMsg, Encoding.UTF8);
							
						}
					}

				}
			}
			catch (Exception)
			{
				
				throw;
			}

			timer1.Start();
		}
	}
}
