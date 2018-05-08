using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace serialBat
{
    class Program
    {
        static void Main(string[] args)
        {
            string seridev, battden, pwrstat;
            try
            {
                pwrstat = System.IO.File.ReadAllText(@"config\pwrstat.ini");
            }
            catch (Exception)
            {
                Console.WriteLine("pwrstat.ini missing");
                Console.ReadKey();
                return;
            }
            try
            {
                battden = System.IO.File.ReadAllText(@"config\battden.ini");
            }
            catch (Exception)
            {
                Console.WriteLine("battden.ini missing");
                Console.ReadKey();
                return;
            }
            try
            {
                 seridev = System.IO.File.ReadAllText(@"config\seridev.ini");
            }
            catch (Exception)
            {
                Console.WriteLine("seridev.ini missing");
                Console.ReadKey();
                return;
            }

            int serRate, serint;
            try
            {
                serRate = int.Parse(System.IO.File.ReadAllText(@"config\serrate.ini"));
            }
            catch (Exception) { Console.WriteLine("Invalid value in serrate.ini");
                Console.ReadKey();
                return;
            }
            try
            {
                serint = int.Parse(System.IO.File.ReadAllText(@"config\serinterval.ini"));
            }
            catch (Exception) { Console.WriteLine("Invalid value in serinterval.ini");
                Console.ReadKey();
                return;
            }
            SerialPort serOut = new SerialPort();
            serOut.PortName = seridev;
            serOut.BaudRate = serRate;
            try { serOut.Open(); }
            catch (Exception) {
                Console.WriteLine("Failed to open serial port, check seridev.ini amd serport.ini");
                Console.ReadKey();
                return;
            }
            while (true)
            {
                var plstat = SystemInformation.PowerStatus.PowerLineStatus.ToString();
                int battrue = int.Parse(SystemInformation.PowerStatus.BatteryLifePercent.ToString("P0"));
                if ((pwrstat.Equals("true")) && (plstat.Equals("Online") ) ){ serOut.WriteLine("1"); }
                else if ((pwrstat.Equals("true"))&&(plstat!="Online")){ serOut.WriteLine("0"); }
                switch (battden)
                {
                    case "true":
                    case "100":
                        serOut.WriteLine(battrue.ToString());
                        break;
                    case "outof5":
                        serOut.WriteLine(Math.Round(battrue / 5.0, 0, MidpointRounding.AwayFromZero).ToString());
                        break;
                    case "outof3":
                        serOut.WriteLine(Math.Round(battrue / 3.0, 0, MidpointRounding.AwayFromZero).ToString());
                        break;
                    case "outof10":
                        serOut.WriteLine(Math.Round(battrue/10.0, 0, MidpointRounding.AwayFromZero).ToString());
                        break;
                    default:
                        Console.WriteLine("Please check battden.ini");
                        Console.ReadKey();
                        return;
                }
                System.Threading.Thread.Sleep(serint);
                
            }

        }
    }
}