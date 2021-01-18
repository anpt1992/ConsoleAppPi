using Iot.Device.ServoMotor;
using Iot.Device.CpuTemperature;
using System;
using System.Threading;
using System.Device.Pwm;

namespace ConsoleAppPi
{
    class Program
    {
        static CpuTemperature temperature = new CpuTemperature();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Servo Motor!");
            using PwmChannel pwmChannel = PwmChannel.Create(0, 0, 50);
            using ServoMotor servoMotor = new ServoMotor(pwmChannel, 160, 700, 2200);
            WritePulseWidth(pwmChannel, servoMotor);



        }
        static void WritePulseWidth(PwmChannel pwmChannel, ServoMotor servoMotor)
        {
            servoMotor.Start();

            while (true)
            {
                Console.WriteLine("Enter a pulse width in microseconds ('Q' to quit). ");
                string? pulseWidth = Console.ReadLine();

                if (pulseWidth?.ToUpper() is "Q" || pulseWidth?.ToUpper() is null)
                {
                    break;
                }

                if (!int.TryParse(pulseWidth, out int pulseWidthValue))
                {
                    Console.WriteLine($"Can not parse {pulseWidth}.  Try again.");
                }

                servoMotor.WritePulseWidth(pulseWidthValue);
                Console.WriteLine($"Duty Cycle: {pwmChannel.DutyCycle * 100.0}%");
            }

            servoMotor.Stop();
        }

        static void WriteAngle(PwmChannel pwmChannel, ServoMotor servoMotor)
        {
            servoMotor.Start();

            while (true)
            {
                Console.WriteLine("Enter an angle ('Q' to quit). ");
                string? angle = Console.ReadLine();

                if (angle?.ToUpper() is "Q" || angle?.ToUpper() is null)
                {
                    break;
                }

                if (!int.TryParse(angle, out int angleValue))
                {
                    Console.WriteLine($"Can not parse {angle}.  Try again.");
                }

                servoMotor.WriteAngle(angleValue);
                Console.WriteLine($"Duty Cycle: {pwmChannel.DutyCycle * 100.0}%");
            }

            servoMotor.Stop();
        }
    }
}
