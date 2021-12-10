using Cosmos.System.Network.IPv4;
using System.Text;
using Cosmos.System;
using Cosmos.System.Network.IPv4.TCP;
using System;
using PrismOS.Essential;

namespace PrismOS
{
    public class Kernel : Cosmos.System.Kernel
    {
        protected override void Run()
        {
            Network.Framework.Init();

            const int Port = 80;
            EndPoint End = new(Address.Zero, Port);
            Address Addr = new(1, 1, 1, 1);

            try
            {
                using TcpClient xClient = new(Addr, Port);
                xClient.Connect(Addr, Port);
                System.Console.WriteLine("Connected!");

                xClient.Send(Encoding.ASCII.GetBytes(HTTP.GenHttp("www.github.com", "/index.html")));

                System.Console.WriteLine("Request send, awaiting response...");

                string data = Encoding.ASCII.GetString(xClient.Receive(ref End));

                System.Console.WriteLine("Got response!\n\n");
                System.Console.WriteLine(data);
                
                xClient.Close();
                Read(data);
            }
            catch(Exception Ex)
            {
                System.Console.WriteLine("Error: " + Ex.Message);
            }

            while (true) {}

            UI.Framework.Window XW = new(200, 200, 300, 300, 0);
            XW.Children.Add(new UI.Framework.Label(50, 50, XW));

            while (true)
            {
                XW.Draw();

                if (Essential.Math.IsPrime(KeyboardManager.ReadKey().KeyChar))
                {
                    XW.Children[0].Text = "The number is prime.";
                }
                else
                {
                    XW.Children[0].Text = "The number is not prime.";
                }
            }
        }

        public static void Read(string Data)
        {
            foreach (string str in Data.Replace("><", ">\n").Split("\n\n")[1].Split("\n"))
            {
                System.Console.WriteLine(
                    "Found the tag '"
                    + str.Replace("<", "").Split(">")[0] +
                    "' with the contents of '" +
                    str.Replace("<", "").Split(">")[1]);
            }
        }
    }
}