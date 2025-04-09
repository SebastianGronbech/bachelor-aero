using System.IO.Ports;

string portName = "/dev/tty.usbmodem143301";
// string portName = "/dev/ttys004";  // Replace with actual path/COM port for Linux

var baudRate = 115200;
var parity = Parity.None;
var dataBits = 8;
var stopBits = StopBits.One;

foreach (var port in SerialPort.GetPortNames())
{
    Console.WriteLine($"Available port: {port}");
}

// Initialize SerialPort
var serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

// Set the event handler for data received
serialPort.DataReceived += (sender, e) =>
{
    string data = serialPort.ReadLine();
    Console.WriteLine("Received on port: " + data);
};

// Open serial port
if (!serialPort.IsOpen)
{
    serialPort.Open();
    Console.WriteLine($"Serial port {portName} opened successfully at {baudRate} baud.");
}

// Print bytes in read buffer
Console.WriteLine($"Bytes in read buffer: {serialPort.BytesToRead}");
if (serialPort.BytesToRead > 0)
{
    string data = serialPort.ReadExisting();
    Console.WriteLine($"Data read from port: {data}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

// Clean up
serialPort.Close();


