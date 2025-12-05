
internal class Program
{
    private static void Main(string[] args)
    {

        string inputFileName = "input.txt"; // Number of rols removed: 8758
        //string inputFileName = "test.txt"; // Number of rols removed: 43
        string[] rows = File.ReadAllLines(inputFileName);


        int RL = rows.Length; //rows length
        int CL = rows[0].Length; //columns lengrh



        int numberOfRolsAccessed = 0;
        int totalNumberOfRolsRemoved= 0;

        bool isInitalState = true;


        //go first round (is initial state) and then if numberOfRolsAccessed is greater then 0
        int round = 1;

        while (numberOfRolsAccessed > 0 || isInitalState)
        {
            numberOfRolsAccessed = 0;


            List<Tuple<int, int>> removedFromPositions = new List<Tuple<int, int>>();


            for (int i = 0; i < RL; i++)
            {
                string row = rows[i];

                for (int j = 0; j < CL; j++)
                {

                    if (!row[j].Equals('@'))
                    {
                        continue;
                    }


                    int x = CountNeighbors(i, j, rows, RL, CL);

                    if (x < 4)
                    {
                        numberOfRolsAccessed++;

                        removedFromPositions.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            totalNumberOfRolsRemoved += numberOfRolsAccessed;

            foreach (var position in removedFromPositions)
            {
                string row = rows[position.Item1];
                char[] rowAsCharsArray = row.ToCharArray();
                rowAsCharsArray[position.Item2] = '.';

                rows[position.Item1] = new string(rowAsCharsArray);

            }

            Console.WriteLine("Round [{0}] Number of rols accessed: {1}. Removed: {2}", round, numberOfRolsAccessed, totalNumberOfRolsRemoved);

            round++;
            isInitalState = false;
        }


        

        Console.WriteLine("Number of rols removed: {0}", totalNumberOfRolsRemoved);

    }

    private static int CountNeighbors(int rowIndex, int columnIndex, string[] rows, int rowsLength, int columnLength)
    {
        // Neighbors positions
        // (x-1, y-1)  (x-1, y)   (x-1, y+1)
        // (x,y-1)        @       (x,y+1)
        // (x+1,y-1)   (x+1,y)    (x+1,y+1)

        //row difference
        int[] dr = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
        //column difference
        int[] dc = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };


        int neighborRollsCount = 0;

        for (int i = 0; i < 8; i++)
        {
            int nr = rowIndex + dr[i]; // Calculate neighbor's row index
            int nc = columnIndex + dc[i]; // Calculate neighbor's column index

            // The approach is to check if the neighbor's coordinates (nr, nc) are
            // strictly within the grid boundaries (0 to R-1 and 0 to C-1).
            if (nr >= 0 && nr < rowsLength && nc >= 0 && nc < columnLength)
            {
                // If the neighbor is within bounds, check its content.
                // If the neighbor is also a paper roll ('@'), increment the count.
                if (rows[nr][nc] == '@')
                {
                    neighborRollsCount++;
                }
            }

        }

        return neighborRollsCount;
    }
}