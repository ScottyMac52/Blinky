using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Blinky
{
    public class MorseCodeBlinker : MorsePatternGenerator
    {
        public static int DotDelay = 120;
        public static int DashDelay = 250;
        public static int CharacterDelay = 500;
        public static int WordDelay = 750;

        public OutputPort OutputPort { get; }

        public MorseCodeBlinker(OutputPort port)
        {
            OutputPort = port;
        }

        public virtual void Write(string outputMessage)
        {
            if (OutputPort == null) return;
            var morseCode = GetCodeFromString(outputMessage);
            var morseSegments = morseCode.Split(CharacterSep);
            for (var x = 0; x < morseSegments.Length; x++)
            {
                var currentSegment = morseSegments[x];
                for (var y = 0; y < currentSegment.Length; y++)
                {
                    switch (currentSegment[y])
                    {
                        case '.':
                            Dot();
                            break;
                        case '-':
                            Dash();
                            break;
                        case '\\':
                            WrdDelay();
                            break;
                        default:
                            WrdDelay();
                            break;
                    }
                    CharDelay();
                }
            }
        }

        private void Dot()
        {
            OutputPort.Write(true);
            Thread.Sleep(DotDelay);
            OutputPort.Write(false);
        }

        private void Dash()
        {
            OutputPort.Write(true);
            Thread.Sleep(DashDelay);
            OutputPort.Write(false);
        }

        private void CharDelay()
        {
            Thread.Sleep(CharacterDelay);
        }

        private void WrdDelay()
        {
            Thread.Sleep(WordDelay);
        }
    }
}
