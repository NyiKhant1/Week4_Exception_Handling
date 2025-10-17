using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Week4_Exception_Handling
{
    class SensorReading
    {
        private string name;
        private double readingvalue;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Value
        {
            get { return readingvalue; }
            set { readingvalue = value; }
        }

        public SensorReading (string name, double readingvalue)
        {
            Name = name;
            Value = readingvalue;
        }
        public override string ToString()
        {
            return ($"Sensor: {Name} - Value: {Value}");
        }

    }
    public class SenesorOfflineException : Exception
    {
        public SenesorOfflineException(string message) : base(message)
        { }
    }
    public class ReadingOutOfRangeException : Exception
    {
        public ReadingOutOfRangeException (string message) : base (message)
        {}
    }
    class Sensor
    {
        private string name;
        private double min;
        private double max;
        private bool isOnline;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Min
        {
            get { return min; }
            set { min = value; }
        }
        public double Max
        {
            get { return max; }
            set { max = value; }
        }
        public bool IsOnline
        {
            get { return isOnline; }
            set { isOnline = value; }
        }
        public void ValidateReading(double reading)
        {
            if (!IsOnline)
            {
                throw new SenesorOfflineException($"{Name} is Offline");
            }
            if (reading < min || reading > max)
            {
                throw new ReadingOutOfRangeException($"{Name} sensor reading {reading} is out of range ");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] SensorName = { "Temperature", "Humidty", "Motion" };
            string[] RawReading = { "22.5", "bad_data", "-5.0" };

            SensorReading[] readings = new SensorReading[SensorName.Length];

            for (int i = 0; i < SensorName.Length; i++)
            {
                double reading = 0.0;
                try
                {
                    reading = Convert.ToDouble(RawReading[i]);
                    if (reading < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(reading));
                    }
                }
                catch (FormatException )
                {

                    Console.WriteLine($"Format error: '{RawReading[i]}' is not a valid number. Defaulting to 0.0.");
                    reading = 0.0;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Range error: {ex.Message} Defaulting to 0.0");
                    reading = 0.0;

                }

                readings[i] = new SensorReading(SensorName[i], reading);
            }
            Console.WriteLine("Final Sensor Reading ");
            for (int i = 0; i < readings.Length; i++)
            {
                Console.WriteLine(readings[i].ToString());
            }
            Sensor[] sensors = new Sensor[3];
            sensors[0] = new Sensor { Name = "Temperature " , Min = -20 , Max = 50 , IsOnline = true};
            sensors[1] = new Sensor { Name = "Humidity", Min = 0, Max = 100, IsOnline = true };
            sensors[2] = new Sensor { Name = "Motion", Min = 0, Max = 1, IsOnline = false };
            double[] SampleReading = { 22.5, 120.0, 1.0 };

            for (int i = 0; i < sensors.Length; i++ )
            {
                Console.WriteLine("--- Validating Sample Reading ---");

                double SafeReading = SampleReading[i];
                try
                {
                    sensors[i].ValidateReading(SampleReading[i]);
                    Console.WriteLine("Reading is valid");
                }
                catch (SenesorOfflineException ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                    SafeReading = 0.0;
                }
                catch (ReadingOutOfRangeException ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                    SafeReading = 0.0;
                }
                readings[i] = new SensorReading(sensors[i].Name, SafeReading);
            }
        }
    }
}
