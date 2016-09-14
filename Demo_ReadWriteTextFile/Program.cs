using System.IO;
using System.Text;

namespace Demo_ReadWriteTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerName;
            int playerScore;

            playerName = "Bonzo";
            playerScore = 55;

            WritePlayerHistory("Bonzo", 56);
        }

        private static void WritePlayerHistory(string playerName, int playerScore)
        {
            for (int i = 0; i < 10; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append(playerName);
                sb.Append(",");
                sb.Append(playerScore++);

                using (StreamWriter sw = new StreamWriter("Data//PlayerHistory.txt", true))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
        }


        private static void ReadPlayerHistory()
    }
}
