class Program
{
    static void Main()
    {
        int n;
        bool isCorrect;
        do
        {
            Console.Write("Введите размер матрицы: ");
            isCorrect = int.TryParse(Console.ReadLine(), out n);
            if (!isCorrect || n <= 0)
                Console.WriteLine("Неверный ввод данных");
        } while (!isCorrect || n <= 0);

        int[,] magic_square = new int[n, n];

        if (n % 2 == 1)
        {
            int num = 1;
            int i = 0;
            int j = n / 2;

            while (num <= n * n)
            {
                magic_square[i, j] = num;
                num++;

                int new_i = i - 1;
                int new_j = j + 1;

                if (new_i < 0)
                    new_i = n - 1;
                if (new_j == n)
                    new_j = 0;

                if (magic_square[new_i, new_j] != 0)
                {
                    i++;
                    if (i == n)
                        i = 0;
                }
                else
                {
                    i = new_i;
                    j = new_j;
                }
            }
        }
        else if (n % 4 == 0)
        {
            int num = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    magic_square[i, j] = num;
                    num++;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if ((i - j) % 4 == 0 || (i + j) % 4 == 3)
                    {
                        magic_square[i, j] = n * n - (i * n + j);
                    }
                }
            }
        }
        else if (n % 4 == 2 && n >= 6)
        {
            int half = n / 2;
            int size_half = half * half;
            int[,] small = new int[half, half];

            int num2 = 1;
            int i2 = 0;
            int j2 = half / 2;
            while (num2 <= size_half)
            {
                small[i2, j2] = num2;
                num2++;

                int ni = i2 - 1;
                int nj = j2 + 1;
                if (ni < 0) ni = half - 1;
                if (nj == half) nj = 0;

                if (small[ni, nj] != 0)
                {
                    i2++;
                    if (i2 == half) i2 = 0;
                }
                else
                {
                    i2 = ni;
                    j2 = nj;
                }
            }

            for (int i = 0; i < half; i++)
            {
                for (int j = 0; j < half; j++)
                {
                    magic_square[i, j] = small[i, j];
                    magic_square[i, j + half] = small[i, j] + 2 * size_half;
                    magic_square[i + half, j] = small[i, j] + 3 * size_half;
                    magic_square[i + half, j + half] = small[i, j] + size_half;
                }
            }

            int part = (n - 2) / 4;
            for (int i = 0; i < half; i++)
            {
                for (int j = 0; j < part; j++)
                {
                    int tmp = magic_square[i, j];
                    magic_square[i, j] = magic_square[i + half, j];
                    magic_square[i + half, j] = tmp;
                }
            }
            for (int i = 0; i < half; i++)
            {
                for (int j = n - part + 1; j < n; j++)
                {
                    int tmp = magic_square[i, j];
                    magic_square[i, j] = magic_square[i + half, j];
                    magic_square[i + half, j] = tmp;
                }
            }
        }
        else
        {
            Console.WriteLine("Невозможно посторить магический квадрат");
            return;
        }

        PrintMatrix(magic_square);
    }

    static void PrintMatrix(int[,] array)
    {
        int[] column_widths = new int[array.GetLength(1)];

        for (int j = 0; j < array.GetLength(1); j++)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int width = array[i, j].ToString().Length;
                if (width > column_widths[j])
                    column_widths[j] = width;
            }
        }

        for (int i = 0; i < array.GetLength(0); i++)
        {
            //int sum = 0;
            for (int j = 0; j < array.GetLength(1); j++)
            {
                //sum += array[i, j];
                if (j == 0)
                    Console.Write("│");
                Console.Write("{0," + (column_widths[j] + 1) + "} ", array[i, j]);
            }
            Console.WriteLine("│ ");
            //Console.WriteLine("│ " + sum);
        }
    }
}
