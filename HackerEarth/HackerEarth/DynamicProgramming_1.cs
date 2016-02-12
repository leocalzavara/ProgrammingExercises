using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerEarth
{
    // Dynamic Programming - I
    // https://www.hackerearth.com/notes/dynamic-programming-i-1/

    class DynamicProgramming_1
    {
        //static int[] input = { 2, 3, 5, 1, 4 };
        static int[] input = new int[30];
        static int N = input.Length;

        static int totalCalculations = 0;

        static int[,] calcMatrix = new int[N, N];

        public void Run()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < N; i++)
            {
                input[i] = rand.Next(5, 100);
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    calcMatrix[i, j] = -1;
                }
            }

            var startDate = DateTime.Now;
            Console.WriteLine("Inicio: {0}", startDate.ToString("HH:mm:ss.fff"));

            //int total = Calc_Recursion(0, N - 1);
            int total = Calc_Memoizing(0, N - 1);

            var endDate = DateTime.Now;
            Console.WriteLine("Fim: {0}", endDate.ToString("HH:mm:ss.fff"));

            Console.WriteLine("Total = {0}", total);
            Console.WriteLine("Tempo de Execução: {0} segundos", (endDate - startDate).TotalSeconds);
            Console.WriteLine("Cálculos Executados: {0}", totalCalculations);
        }

        int Calc_Recursion(int begin, int end)
        {
            if (begin > end)
                return 0;

            totalCalculations++;

            int year = N - (end - begin + 1) + 1;

            return Math.Max(
                Calc_Recursion(begin + 1, end) + (year * input[begin]),
                Calc_Recursion(begin, end - 1) + (year * input[end]));
        }

        int Calc_Memoizing(int begin, int end)
        {
            if (begin > end)
                return 0;

            if (calcMatrix[begin, end] != -1)
                return calcMatrix[begin, end];

            totalCalculations++;

            int year = N - (end - begin + 1) + 1;

            return calcMatrix[begin, end] = Math.Max(
                Calc_Memoizing(begin + 1, end) + (year * input[begin]),
                Calc_Memoizing(begin, end - 1) + (year * input[end]));
        }
    }
}
