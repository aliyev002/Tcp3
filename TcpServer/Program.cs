using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

var localAddress = IPAddress.Any;
var port = 27001;

TcpListener listener = new TcpListener(localAddress, port);
listener.Start();
Console.WriteLine("Server isleyir...");

while (true)
{
    using (TcpClient client = listener.AcceptTcpClient())
    using (NetworkStream stream = client.GetStream())
    {
        
        var fileNameBytes = new byte[1024]; 
        int fileNameLength = stream.Read(fileNameBytes, 0, fileNameBytes.Length);
        var fileName = Encoding.UTF8.GetString(fileNameBytes, 0, fileNameLength);




        

        
        var fileLengthBytes = new byte[1024]; 
        int fileLengthLength = stream.Read(fileLengthBytes, 0, fileLengthBytes.Length);
        var fileLength = long.Parse(Encoding.UTF8.GetString(fileLengthBytes, 0, fileLengthLength));
        var path = $"{DateTime.Now:HH.mm.ss}_{fileName}";
        
        using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            var buffer = new byte[5000];
            int bytesRead;
            long totalReceived = 0;
            while (totalReceived < fileLength && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, bytesRead);
                totalReceived += bytesRead;
            }
        }
        Console.WriteLine($"Fayl qebul edildi və saxlanildi: {path}");
    }
}
