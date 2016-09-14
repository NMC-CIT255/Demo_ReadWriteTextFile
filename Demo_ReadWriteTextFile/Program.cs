using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo_ReadWriteTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            List<HistoricScore> historicScores = new List<HistoricScore>();
            historicScores = ReadPlayerHistory();

            DisplayHistoricScores(historicScores);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private static void DisplayHistoricScores(List<HistoricScore> historicScores)
        {
            foreach (var historicScore in historicScores)
            {
                Console.WriteLine($"{historicScore.PlayerName} - {historicScore.PlayerScore}");
            }
        }

        private static void WritePlayerHistory(string playerName, int playerScore)
        {

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
            // initialize a StreamReader object for reading
            //
            StreamReader sReader = new StreamReader(DataSettings.dataFilePath);

            //
            // read all data from the data file
            //
            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
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
