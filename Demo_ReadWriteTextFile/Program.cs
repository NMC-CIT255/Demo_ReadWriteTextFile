using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo_ReadWriteTextFile
{
    /// <summary>
    /// program to demonstrate reading and writing a small class structure to a data file
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            MainMenu();


            Console.WriteLine("Thank you for using our program.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void MainMenu()
        {
            bool usingMenu = true;

            while (usingMenu)
            {
                //
                // set up display area
                //
                Console.Clear();
                Console.CursorVisible = false;

                //
                // display the menu
                //
                Console.WriteLine("Please type the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine(
                    "\t" + "1. Display Historic Scores" + Environment.NewLine +
                    "\t" + "2. Add a New Score" + Environment.NewLine +
                    "\t" + "E. Exit" + Environment.NewLine);

                //
                // get and process the user's response
                // note: ReadKey argument set to "true" disables the echoing of the key press
                //
                ConsoleKeyInfo userResponse = Console.ReadKey(true);

                Console.CursorVisible = true;

                switch (userResponse.KeyChar)
                {
                    //
                    // display historic scores
                    //
                    case '1':
                        //
                        // read in all of the historic scores and place them in a list of HistoricScore objects
                        //
                        List<HistoricScore> historicScores = new List<HistoricScore>();
                        historicScores = ReadPlayerHistory();

                        DisplayHistoricScores(historicScores);
                        break;
                    //
                    // add a new historic score
                    //
                    case '2':
                        DisplayAddAPlayerHistory();
                        break;
                    //
                    // quit the program
                    //
                    case 'E':
                    case 'e':
                        usingMenu = false;
                        break;
                    default:
                        //
                        // TODO handle invalid menu responses from user
                        //
                        break;
                }
            }
        }

        private static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static void DisplayHeader(string headerText)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(headerText);
            Console.WriteLine();
        }

        private static void DisplayHistoricScores(List<HistoricScore> historicScores)
        {
            DisplayHeader("List of Historical Scores");

            foreach (var historicScore in historicScores)
            {
                //
                // note use of C# 6.0 string interpolation
                //
                Console.WriteLine($"{historicScore.PlayerName} - {historicScore.PlayerScore}");
            }

            DisplayContinuePrompt();
        }

        private static void DisplayAddAPlayerHistory()
        {
            //
            // get player name and score
            //
            Console.Write("Enter the player's name: ");
            string playersName = Console.ReadLine();

            Console.Write("Enter the player's score: ");
            string playersScore = Console.ReadLine();

            //
            // generate the record string for the data file using the 
            // StringBuilder class
            //
            StringBuilder sb = new StringBuilder();
            sb.Append(playersName + DataSettings.Delineator);
            sb.Append(playersScore + DataSettings.Delineator);

            //
            // initialize a StreamWriter object for writing to a file
            //
            StreamWriter sWriter = new StreamWriter(DataSettings.DataFilePath, true);

            //
            // read all data from the data file
            //
            using (sWriter)
            {
                sWriter.WriteLine(sb);
            }

            DisplayContinuePrompt();
        }


        private static List<HistoricScore> ReadPlayerHistory()
        {
            const char delineator = ','; // delineator in a CSV file

            List<HistoricScore> historicScores = new List<HistoricScore>();

            //
            // create lists to hold the historic score strings
            //
            List<string> historicScoresStringList = new List<string>();

            //
            // initialize a StreamReader object for reading from a file
            //
            StreamReader sReader = new StreamReader(DataSettings.DataFilePath);

            //
            // read all data from the data file
            //
            using (sReader)
            {
                //
                // keep reading lines of text until the end of the file is reached
                //
                while (!sReader.EndOfStream)
                {
                    historicScoresStringList.Add(sReader.ReadLine());
                }
            }

            //
            // separate lines into fields and build out the list of historic scores
            //
            foreach (string historicScore in historicScoresStringList)
            {
                //
                // use the Split method and the delineator on the array to separate each property into an array of properties
                //
                string[] fields = historicScore.Split(delineator);

                //
                // populate the historic scores list with HistoricScore objects
                //
                historicScores.Add(new HistoricScore() { PlayerName = fields[0], PlayerScore = Convert.ToInt32(fields[1]) });
            }

            return historicScores;
        }
    }
}
