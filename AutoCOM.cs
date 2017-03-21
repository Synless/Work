using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCOM
{
    public class AutoCOM : SerialPort
    {
        protected bool found = false;
        int[] _baudrate = { 9600, 19200, 38400, 115200 };

        AutoCOM(string _querry, string[] _answers, int _maxPort = 20, int waitTime = 20)
        {
            //GET ALL COM PORT CURRENTLY AVAILABLE (LIKE UNDER DEVICE MANAGER)
            string[] _ports = SerialPort.GetPortNames();

            //SCANNING THROUGH BAUDRATE, LIKELY TO BE 9600 OR 19200
            foreach (int b in _baudrate)
            {
                BaudRate = b;
                //SCANNING THROUGH PORTS
                foreach (string p in _ports)
                {
                    PortName = p;
                    try
                    {
                        Open();
                        WriteLine(_querry);
                        System.Threading.Thread.Sleep(waitTime);
                        string received = ReadExisting();
                        Close();

                        //IF THE ANWSER IS CONTAINED IN THE _ANSWER ARRAY, THEN WE CAN'T STOP HERE, JOB'S DONE
                        foreach (string anwser in _answers)
                        {
                            if (anwser.Contains(received))
                            {
                                found = true;
                                return;
                            }
                        }
                    }
                    catch
                    {
                        found = false;
                        return;
                    }
                    if (PortName.ToString() == ("COM" + _maxPort.ToString()))
                    {
                        break;
                    }
                }                
            }
        }
    }
}
