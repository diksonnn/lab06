using System;

class Program
{
    static void Main(string[] args)
    {
        int n;
        do
        {
            Console.Write("Введите количество критериев (не более 9): ");
            n = int.Parse(Console.ReadLine());

            if (n < 1 || n > 9)
            {
                Console.WriteLine("Количество критериев должно быть от 1 до 9.");
            }
        } while (n < 1 || n > 9);

        double[,] comparisons = new double[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                double value;
                do
                {
                    Console.Write($"Введите отношение критерия {i + 1} к критерию {j + 1} (от 1 до 9): ");
                    value = double.Parse(Console.ReadLine());

                    if (value < 1 || value > 9)
                    {
                        Console.WriteLine("Значение должно быть от 1 до 9.");
                    }
                } while (value < 1 || value > 9);

                comparisons[i, j] = value;
                comparisons[j, i] = 1 / value;
            }
        }

        double[] weights = AnalyzeAHP(n, comparisons);

        Console.WriteLine("Весовые коэффициенты:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Критерий {i + 1}: {weights[i]:F2}");
        }
    }

    static double[] AnalyzeAHP(int n, double[,] comparisons)
    {
        double[] weights = new double[n];

        for (int i = 0; i < n; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < n; j++)
            {
                rowSum += comparisons[i, j];
            }

            for (int j = 0; j < n; j++)
            {
                comparisons[i, j] /= rowSum;
            }
        }

        for (int j = 0; j < n; j++)
        {
            double columnSum = 0;
            for (int i = 0; i < n; i++)
            {
                columnSum += comparisons[i, j];
            }
            weights[j] = columnSum / n;
        }

        double weightsSum = 0;
        for (int i = 0; i < n; i++)
        {
            weightsSum += weights[i];
        }

        for (int i = 0; i < n; i++)
        {
            weights[i] /= weightsSum;
        }

        return weights;
    }
}