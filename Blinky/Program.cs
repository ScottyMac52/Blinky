using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

/* NOTE: make sure you change the deployment target from the Emulator to your Netduino before running this
 * Netduino sample app.  To do this, select "Project menu > Blinky Properties > .NET Micro Framework" and 
 * then change the Transport type to USB.  Finally, close the Blinky properties tab to save these settings. */

namespace Blinky
{
    /// <summary>
    /// The main entry point for the program
    /// </summary>
    public class Program
    {
        private static int iterationCount = 0;
        private const int MaxIterations = 10;

        public static void Main()
        {
            var led = new OutputPort(Pins.ONBOARD_LED, false);
            var morseCodeSender = new MorseCodeBlinker(led);

            if (!Battery.IsFullyCharged())
            {
                morseCodeSender.Write(PowerState.CurrentPowerLevel <= PowerLevel.Medium
                    ? "SOS "
                    : "SOS SOS SOS SOS SOS ");

                iterationCount++;
                if (iterationCount >= MaxIterations)
                {
                    PowerState.RebootDevice(false);
                }
            }
            else
            {
                while (true)
                {
                    morseCodeSender.Write("SOS SAVERS 2016 ");
                    iterationCount++;
                    if (iterationCount >= MaxIterations)
                    {
                        break;
                    }
                }

                PowerState.RebootDevice(false);
            }
        }
    }
}
