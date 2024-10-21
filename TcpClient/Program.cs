using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

var serverAddress = "26.230.244.228"; 
var port = 27001; 

var path = @"C:\Users\Legion\Desktop\sinaq\koala.jpg";

try
{
    FileInfo file = new FileInfo(path);
    TcpClient client = new TcpClient(serverAddress, port);
    using (NetworkStream stream = client.GetStream())
    {
        
        var fileNameBytes = Encoding.UTF8.GetBytes(file.Name);
        stream.Write(fileNameBytes, 0, fileNameBytes.Length); 

        
        var fileLengthBytes = Encoding.UTF8.GetBytes(file.Length.ToString());
        stream.Write(fileLengthBytes, 0, fileLengthBytes.Length); 

        
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            var buffer = new byte[5000];
            int bytesRead;
            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream.Write(buffer, 0, bytesRead);
            }
        }
        Console.WriteLine("Fayl gnoderildi.");
    }
    client.Close();
}
catch (Exception ex)
{
    Console.WriteLine($"Xeta bash verdi: {ex.Message}");
}
