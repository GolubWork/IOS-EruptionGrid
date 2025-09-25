namespace _Scripts.GridSpawn
{
    [System.Serializable]
    public class GridRows
    {
        public int X, Y;

        [System.Serializable]
        public class Column
        {
            public bool[] rows;
            public Column(int y)
            {
                rows = new bool[y];
            }
        }

        public Column[] columns;
        
        public void InitializeGrid()
        {
            columns = new Column[X];
            for (int i = 0; i < X; i++)
            {
                columns[i] = new Column(Y);
            }
        }
        
        public bool IsEqual(GridRows other)
        {
            if (X != other.X || Y != other.Y) 
                return false;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (columns[i].rows[j] != other.columns[i].rows[j])
                        return false;
                }
            }

            return true;
        }
    }
}