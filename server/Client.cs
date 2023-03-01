using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Client
    {
        public static int dataBufferSize = 4069;
        public int id;
        public TCP tcp;

        public Client(int _id)
        {
            id= _id;

            tcp = new TCP(id);
        }

        public class TCP
        {
            public TcpClient socket;
            private NetworkStream stream;
            private byte[] recieveBuffer;
            private readonly int id;

            public TCP(int _id)
            {
                id= _id;
            }

            public void Connect(TcpClient _socket)
            {
                socket = _socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();

                recieveBuffer= new byte[dataBufferSize];

                stream.BeginRead(recieveBuffer, 0, dataBufferSize, RecieveCallback, null);

                //TODO: Send weolcome package
            }

            private void RecieveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLenght = stream.EndRead(_result);
                    if(_byteLenght <= 0 ) 
                    {
                        //TODO: Disconnect
                        return;
                    }

                    byte[] _data = new byte[_byteLenght];
                    Array.Copy(recieveBuffer, _data, _byteLenght);

                    //TODO: Handle Data
                    stream.BeginRead(recieveBuffer, 0, dataBufferSize, RecieveCallback, null);
                }
                catch (Exception e)
                {

                    Console.WriteLine($"Error recieving TCP data {e}");
                    //TODO: disconnect;
                }
            }
        }
    }
}
