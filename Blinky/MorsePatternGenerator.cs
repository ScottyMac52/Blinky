using System;
using System.Collections;
using NetMf.CommonExtensions;
using Microsoft.SPOT;
// ReSharper disable ReplaceWithStringIsNullOrEmpty

namespace Blinky
{
    public class MorsePatternGenerator : IMorsePatternGenerator
    {
        public static char CharacterSep = '|';
        public static char WordSep = '\\';
        protected Hashtable UsedCodeMap { get; private set; }
        protected Hashtable UsedReverseCodeMap { get; private set; }
        private static readonly Hashtable CodeMap = new Hashtable
        {
            {'a', ".-|"},
            {'b', "-...|"},
            {'c', "-.-.|"},
            {'d', "-..|"},
            {'e', ".|"},
            {'f', "..-.|"},
            {'g', "--.|"},
            {'h', "....|"},
            {'i', "..|"},
            {'j', ".---|"},
            {'k', "-.-|"},
            {'l', ".-..|"},
            {'m', "--|"},
            {'n', "-.|"},
            {'o', "---|"},
            {'p', ".--.|"},
            {'q', "--.-|"},
            {'r', ".-.|"},
            {'s', "...|"},
            {'t', "-|"},
            {'u', "..-|"},
            {'v', "...-|"},
            {'w', ".--|"},
            {'x', "-..-|"},
            {'y', "-.--|"},
            {'z', "--..|"},
            {'1', ".----|"},
            {'2', "..---|"},
            {'3', "...--|"},
            {'4', "....-|"},
            {'5', ".....|"},
            {'6', "-....|"},
            {'7', "--...|"},
            {'8', "---..|"},
            {'9', "----.|"},
            {'0', "-----|"},
            {' ', WordSep}
        };

        private static readonly Hashtable ReverseCodeMap = new Hashtable
        {
            {".-", 'a' },
            {"-...", 'b' },
            {"-.-.", 'c'},
            {"-..", 'd'},
            {".", 'e' },
            {"..-.", 'f'},
            {"--.", 'g'},
            {"....", 'h'},
            {"..", 'i'},
            {".---", 'j'},
            {"-.-", 'k'},
            {".-..", 'l'},
            {"--", 'm'},
            {"-.", 'n'},
            {"---", 'o'},
            {".--.", 'p'},
            {"--.-", 'q'},
            {".-.", 'r'},
            {"...", 's'},
            {"-", 't'},
            {"..-", 'u'},
            {"...-", 'v'},
            {".--", 'w'},
            {"-..-", 'x'},
            {"-.--", 'y'},
            {"--..", 'z'},
            {".----", '1'},
            {"..---", '2'},
            {"...--", '3'},
            {"....-", '4'},
            {".....", '5'},
            {"-....", '6'},
            {"--...", '7'},
            {"---..", '8'},
            {"----.", '9'},
            {"-----", '0'},
            {WordSep, ' '}
        };


        public MorsePatternGenerator()
        {
            IntializeUsedMap();
        }

        public string GetStringFromCode(string inputMessage)
        {
            var outputMessage = inputMessage;
            if (inputMessage == null || inputMessage.Length == 0)
            {
                return string.Empty;
            }
            var messageWords = inputMessage.Split(WordSep);
            foreach (var messageWord in messageWords)
            {
                if (messageWord == null || messageWord.Length == 0) continue;
                // We have a word so decode it
                var lettersInWord = messageWord.Split(CharacterSep);
                foreach (var character in lettersInWord)
                {
                    if (UsedReverseCodeMap.Contains(character)) continue;
                    var decodedValue = ReverseCodeMap[character].ToString();
                    outputMessage = outputMessage.Replace(character, decodedValue);
                    UsedReverseCodeMap.Add(character, decodedValue);
                }
            }
            return outputMessage;
        }

        public virtual string GetCodeFromString(string inputMessage)
        {
            inputMessage = inputMessage.ToLower();
            var outputMessage = inputMessage;
            for (var x = 0; x < inputMessage.Length; x++)
            {
                var nextChar = inputMessage[x];
                // If the character has not been used then we must look in the HashTable
                if (UsedCodeMap.Contains(nextChar)) continue;
                var codeValue = CodeMap[nextChar].ToString();
                outputMessage = outputMessage.Replace(nextChar.ToString(), codeValue);
                UsedCodeMap.Add(nextChar, codeValue);
            }
            return outputMessage;
        }

        private void IntializeUsedMap()
        {
            UsedCodeMap = new Hashtable();
            UsedReverseCodeMap = new Hashtable();
        }
    }
}
