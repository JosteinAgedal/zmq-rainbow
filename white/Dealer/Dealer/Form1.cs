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

namespace Dealer
{

	public partial class Form1 : Form
	{


		public Form1()
		{
			InitializeComponent();

			using (var context = ZmqContext.Create())
			{
				using (var socket = context.CreateSocket(SocketType.SUB))
				{
					//socket.SubscribeAll();
					System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
					Byte[] bytes = encoding.GetBytes("White");
					socket.Subscribe(bytes);

					socket.Connect("tcp://127.0.0.1:30002");

					while (true)
					{
						var rcvdMsg = socket.Receive(Encoding.UTF8);
						Console.WriteLine(rcvdMsg);
					}
				}

			}

		}
	}
}
