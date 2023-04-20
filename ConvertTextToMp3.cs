using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace ConvertTextToMp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Make multi file

            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            string pathToFolder = Path.Combine(pathToDesktop, "Convert");

            string pathInputFile = Path.Combine(pathToFolder, "inputNumbers.txt");
            string pathOutputFile = Path.Combine(pathToFolder, "output.wav");


            string[] inputNumbers = File.ReadAllLines(pathInputFile);

            // Create a new instance of the SpeechSynthesizer class
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                // Set the voice to use for speech synthesis
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Teen);
                float num;
                string fileName;
                // Loop through each line in the text file
                for (int i = 0; i < inputNumbers.Length; i++)
                {
                    num = ((float)i + 1) / 100f;
                    Console.WriteLine(num);
                    fileName = "number_" + num.ToString("F2") + ".wav";
                    //Console.WriteLine(fileName);
                    // Convert the line to an mp3 file
                    using (var waveStream = new MemoryStream())
                    {
                        synth.SetOutputToWaveStream(waveStream);
                        synth.Speak(inputNumbers[i]);
                        waveStream.Seek(0, SeekOrigin.Begin);

                        using (var reader = new WaveFileReader(waveStream))
                        {
                            
                            var outPath = Path.Combine(pathToFolder, fileName);
                            WaveFileWriter.CreateWaveFile(outPath, reader);
                        }
                    }
                }
            }



            #endregion
            /*
            #region Test For Single Line and Single File
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            string pathToFolder = Path.Combine(pathToDesktop, "Convert");

            string pathInputFile = Path.Combine(pathToFolder, "input.txt");
            string pathOutputFile = Path.Combine(pathToFolder, "output.wav");

            string inputNumbers = File.ReadAllText(pathInputFile);

            Console.WriteLine(inputNumbers);

            // Create a new instance of the SpeechSynthesizer class
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                // Set the voice to use for speech synthesis
                synth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Child );


                using (var waveStream = new MemoryStream())
                {
                    synth.SetOutputToWaveStream(waveStream);
                    synth.Speak(inputNumbers);
                    waveStream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new WaveFileReader(waveStream))
                    {
                        WaveFileWriter.CreateWaveFile(pathOutputFile, reader);
                    }
                }

            }
            #endregion
            */

        }

        private void MakeNumber_Click(object sender, EventArgs e)
        {
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            string pathToFolder = Path.Combine(pathToDesktop, "Convert");

            string path = Path.Combine(pathToFolder, "inputNumbers.txt");

            using (StreamWriter sw = File.CreateText(path))
            {
                for (int i = 1; i <= 75; i++)
                {
                    sw.WriteLine(NumberToText(i));
                }
            }
        }

        private string NumberToText(int number)
        {
            string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] tens = { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

            if (number >= 1 && number < 10) return ones[number];
            else if (number >= 10 && number < 20) return teens[number - 10];
            else if (number >= 20 && number < 100)
            {
                int oneUnit = number % 10;
                int tenUnit = (number - oneUnit) / 10;
                return tens[tenUnit - 1] + " " + ones[oneUnit];
            }
            else return "out of range";
        }
    }
}
