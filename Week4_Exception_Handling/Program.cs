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
        
        }
    }
}
