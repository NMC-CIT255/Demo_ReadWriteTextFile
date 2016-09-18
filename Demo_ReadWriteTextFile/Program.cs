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
            //
            // run the application loop
            //
            MainMenu();

            Console.WriteLine("Thank you for using our program.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// application loop
        /// </summary>
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
                        // historic scores will be stored internally in the application as a list of objects
                        //
                        List<HistoricScore> historicScores = new List<HistoricScore>();

                        //
                        // attempt to read from the data file
                        //
                        try
                        {
                            historicScores = ReadPlayerHistory();
                            DisplayHistoricScores(historicScores);
                        }
                        //
                        // catch the first I/O error
                        //
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");

                            DisplayContinuePrompt();
                        }

                        break;
                    //
                    // add a new historic score
                    //
                    case '2':
                        string playerHistory;

                        playerHistory =  DisplayGetAPlayerHistory();

                        //
                        // attempt to write to file
                        //
                        try
                        {
                            WritePlayerHistory(playerHistory);
                        }
                        //
                        // catch the first I/O error
                        //
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");

                            DisplayContinuePrompt();
                        }

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


        /// <summary>
        /// helper method to display continue prompt
        /// </summary>
        private static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// helper method to display a header with text
        /// </summary>
        private static void DisplayHeader(string headerText)
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(headerText);
            Console.WriteLine();
        }
        
        /// <summary>
        /// display a list of historic score
        /// </summary>
        /// <param name="historicScores"></param>
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

        /// <summary>
        /// display prompts to get a new player score to save
        /// </summary>
        /// <returns>string to write line to text file</returns>
        private static string DisplayGetAPlayerHistory()
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

            return sb.ToString();
        }

        /// <summary>
        /// write player history to text file
        /// </summary>
        /// <param name="playerHistory">string that is the line to write to the text file</param>
        private static void WritePlayerHistory(string playerHistory)
        {
            try
            {
                //
                // initialize a StreamWriter object for writing to a file
                //
                StreamWriter sWriter = new StreamWriter(DataSettings.DataFilePath, true);

                //
                // read all data from the data file
                //
                using (sWriter)
                {
                    sWriter.WriteLine(playerHistory);
                }
            }
            //
            // an I/O error was encountered
            //
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Read all historic player scores
        /// </summary>
        /// <returns>list of historic scores</returns>
        private static List<HistoricScore> ReadPlayerHistory()
        {
            const char delineator = ','; // delineator in a CSV file

            List<HistoricScore> historicScores = new List<HistoricScore>();

            //
            // create lists to hold the historic score strings
            //
            List<string> historicScoresStringList = new List<string>();

            try
            {
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
            }
            //
            // an I/O error was encountered
            //
            catch (Exception)
            {
                throw;
            }

            //
            // separate each line of text from the file into HistoricScore objects
            //
            if (historicScoresStringList != null)
            {
                //
                // separate lines into fields and build out the list of historic scores
                //
                foreach (string historicScore in historicScoresStringList)
                {
                    //
                    // use the Split method and the delineator on the array to separate each property into an array of properties
                    //
                    string[] fields = historicScore.Split(delineator);        /// </summary>

                    //
                    // populate the historic scores list with HistoricScore objects
                    //
                    historicScores.Add(new HistoricScore() { PlayerName = fields[0], PlayerScore = Convert.ToInt32(fields[1]) });
                }
            }

            return historicScores;
        }
    }
}
