

namespace FindyBot3000.AzureFunction
{
    // This begs for a stateful azure function...
    public class MatrixModel
    {
        private const int TopRows = 12;
        private const int TopCols = 16;
        private const int BottomRows = 9;
        private const int BottomCols = 8;

        private bool[,] TopItems = new bool[TopRows, TopCols];
        private bool[,] BottomItems = new bool[BottomRows, BottomCols];

        public void AddItem(int row, int col)
        {
            if (row < 12)
            {
                this.TopItems[row, col] = true;
            }
            else if (row < 21)
            {
                this.BottomItems[row - 12, col] = true;
            }
        }

        public (int, int) GetNextAvailableBox(bool isSmallBox)
        {
            if (isSmallBox)
            {
                return this.GetBoxAndUpdate(TopItems, TopRows, TopCols);
            }
            else
            {
                (int row, int col) = this.GetBoxAndUpdate(BottomItems, BottomRows, BottomCols);

                // 12 rows of small boxes on top, with 9 rows of big boxes below.
                // Indexing for rows and columns start at top left.
                row += 12;

                return (row, col);
            }
        }

        private (int, int) GetBoxAndUpdate(bool[,] matrix, int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (matrix[row, col] == false)
                    {
                        matrix[row, col] = true;
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }
    }
}
