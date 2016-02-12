using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerEarth
{
    /*
    Samu and Shopping
    https://www.hackerearth.com/code-monk-dynamic-programming/algorithm/samu-and-shopping/description/
    
    Samu is in super market and in a mood to do a lot of shopping. She needs to buy shirts, pants and shoes for herself and her family. There are N different shops. Each shop contains all these three items but at different prices. Now Samu has a strategy that she won't buy the same item from the current shop if she had already bought that item from the shop adjacent to the current shop.

    Now Samu is confused, because although she want to follow her strategy strictly but at the same time she want to minimize the total money she spends on shopping. Being a good programmer, she asks for your help.

    You are provided description about all N shops i.e costs of all three items in each shop. You need to help Samu find minimum money that she needs to spend such that she buys exactly one item from every shop.

    Input Format: 
    First line contain number of test cases T. Each test case in its first line contain N denoting the number of shops in Super Market. Then each of next N lines contains three space separated integers denoting cost of shirts, pants and shoes in that particular shop.

    Output Format:
    For each test case, output the minimum cost of shopping taking the mentioned conditions into account in a separate line.

    Constraints:
    1 ≤ T ≤ 10 
    1 ≤ N ≤ 10^5
    Cost of each item (shirt/pant/shoe) does not exceed 10^4

    Sample Input
    1
    3
    1 50 50
    50 50 50
    1 50 50
    
    Sample Output
    52
    
    Explanation
    There are two ways, each one gives 52 as minimum cost. One way is buy shirt from first shop, pant from second shop and shirt from third shop or she can buy shirt from first shop, shoe from second shop and shirt from third shop.

    Both ways, cost comes up to 1 + 50 + 1 = 52
    */

    class SamuAndShopping
    {
        static int T, N;
        static int[][] shopItems;
        static int[, ,] cacheMatrix;

        public void Run()
        {
            T = Int32.Parse(Console.ReadLine().Trim());

            for (int t = 0; t < T; t++)
            {
                N = Int32.Parse(Console.ReadLine().Trim());
                shopItems = new int[N][];

                for (int n = 0; n < N; n++)
                {
                    shopItems[n] = Console.ReadLine().Trim().Split(' ').Select(i => Int32.Parse(i)).ToArray();
                }

                cacheMatrix = new int[N, 3, 3];
                for (int n = 0; n < N; n++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            cacheMatrix[n, i, j] = -1;
                        }
                    }
                }

                var x = Calc(0, 0);
                var y = Calc(0, 1);
                var z = Calc(0, 2);

                var min_x_y = Math.Min(x, y);
                var answer = Math.Min(min_x_y, z);

                Console.WriteLine(answer);
            }
        }

        static int Calc(int n, int selectedItem)
        {
            if (n >= N)
                return 0;

            if (selectedItem == 0)
            {
                if (cacheMatrix[n, 1, 2] != -1)
                    return cacheMatrix[n, 1, 2];

                return cacheMatrix[n, 1, 2] = Math.Min(shopItems[n][selectedItem] + Calc(n + 1, 1), shopItems[n][selectedItem] + Calc(n + 1, 2));
            }

            if (selectedItem == 1)
            {
                if (cacheMatrix[n, 0, 2] != -1)
                    return cacheMatrix[n, 0, 2];

                return cacheMatrix[n, 0, 2] = Math.Min(shopItems[n][selectedItem] + Calc(n + 1, 0), shopItems[n][selectedItem] + Calc(n + 1, 2));
            }

            if (selectedItem == 2)
            {
                if (cacheMatrix[n, 0, 1] != -1)
                    return cacheMatrix[n, 0, 1];

                return cacheMatrix[n, 0, 1] = Math.Min(shopItems[n][selectedItem] + Calc(n + 1, 0), shopItems[n][selectedItem] + Calc(n + 1, 1));
            }

            return 0;
        }
    }
}
