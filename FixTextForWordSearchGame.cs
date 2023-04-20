using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace FixTextForWordSearchGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ConvertText_Click(object sender, EventArgs e)
        {
            string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            string pathToFolder = Path.Combine(pathToDesktop, "Convert");

            string pathInputFile = Path.Combine(pathToFolder, "Topic 1.txt");
            string pathOutputFile = Path.Combine(pathToFolder, "output.txt");

            /*
            #region Test for one line

            string line = "(?)BONFIRE NIGHT (?)NOVEMBER (?)FIFTH (?)PEOPLE (?)CELEBRATE (?)GUY FAWKES (?)GUNPOWDER PLOT (?)FIREWORK (?)DISPLAYS (?)BONFIRES (?)PARKS (?)GARDENS (?)OUTDOOR (?)FUN (?)ROCKETS (?)SPARKLERS (?)MATCHES (?)ALWAYS (?)USE (?)FIREWORKS (?)SAFELY";
            string[] words = line.Split(new string[] { "(?)" }, StringSplitOptions.None);
            string line2 = "";
            foreach (string word in words)
            {
                var newword = word.Replace(" ","");
                if(newword.Length<=8)
                line2 = line2+"," +newword;
            }
            string[] content = new string[1] { line2 };
            File.WriteAllLines(pathOutputFile, content);

            #endregion
            */

            
            #region For File
            // Read input file
            string[] lines = File.ReadAllLines(pathInputFile);

            List<string> result = new List<string>();
            // Process each line1
            for (int i = 0; i < lines.Length; i++)
            {
                string line1 = lines[i];
                string line2 = "";
                string[] words = line1.Split(new string[] { "(?)" }, StringSplitOptions.None);

                int count = 0;

                for (int j = 0; j < words.Length; j++)
                {
                    string word = words[j];

                    //ignore space in word
                    word = word.Replace(" ", "");

                    // each word must have atleast 8 letters and all is letter (abc..xyz)
                    if (word.Length <= 8 && !string.IsNullOrEmpty(word) && word.All(c => Char.IsLetter(c)))
                    {
                        line2 = line2+" "+word;
                        count++;
                    }
                }

                // line must not all white space and have at least 9 words
                if(string.IsNullOrWhiteSpace(line2)==false && count >=9)
                {
                    result.Add(line2);
                }
            }

            // Write output file
            File.WriteAllLines(pathOutputFile, result);
            #endregion
            

        }
    }
}
