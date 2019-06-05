using System;
using System.Linq;
namespace AlgorithmicTraining.Lib
{
    public class FindTwoLostItemsInArrayTask
    {


        /// <summary>
        /// Find two missing numbers in the sequence of numbers from 0 to N
        /// </summary>
        /// <param name="initArray">Array of numbers from 0 to N without two numbers</param>
        /// <returns> Two missing numbers </returns>
        public static (double firstLostItem, double secondLostItem)? FindTwoMissingItems(int[] initArray)
        {
            if (initArray == null || initArray.Length == 0) return null;


            var arrayLength =  (long) initArray.Length;
            var n = arrayLength + 1;

            // I) -----------------------------------------------------------------------------------------
            
            // calc full sum of items and sum of squares of itemsum 
            // sum of items S = 1 + 2 + 3 + ... + N = n*(n+1)/2
            var fullSumOfItems = (n * (n + 1)) / 2;

            // sum of square of items P = 1*1 + 2*2 + 3*3 + ... + N*N = 2n*(n+1)*(2*n+1)/6
            var fullSumOfSquareItems = (n * (n + 1) * (2 * n + 1)) / 6;


            // II) ----------------------------------------------------------------------------------------
            // calc sum of items and sum of squares of items
            // S`
            long actualSumOfItems = 0;
            // P`
            long actualSumOfSquareItems = 0;

            for (int i = 0; i < arrayLength; i++)
            {
                var currentItem = (long) initArray[i];

                actualSumOfItems += currentItem;
                actualSumOfSquareItems += currentItem * currentItem;
            }

            // III) ---------------------------------------------------------------------------------------

            /*          Get the system of equation:
             *          
             *          x + y + S` = S          =>      x + y = S - S` = S``
             *  
             *          x^2 + y^2 + P`= P       =>      x^2 + y^2 = P - P` = P``
             *
             *          It is system of equation => normalizing:
             *
             *              x^2+y^2 = (x+y)^2 - 2xy     =>  xy = (S``^2 - P``)/2        => y = (S``^2 - P``)/2*x
             *
             *              x + y = S``     =>      x + (S``^2 - P``)/2*x = S``     =>
             *
             *          z^2 - S``z + (S``^2 - P``)/2 = 0    (quadratic equation)
             *
             *          Solution of quadratic equation:
             *          D = b^2 - 4ac   =>   S``^2 - 4*(S``^2 - P``)/2  => 2P`` - S^2   (D must be > 0 by design of problem definition)
             *          
             *          x,y = (-b [+/-] sqrt(D))/2a   => (S`` [+/-] sqrt(2P`` - S^2))/2
             */

            //S``
            var diffS = fullSumOfItems - actualSumOfItems;
            //P``
            var diffP = fullSumOfSquareItems - actualSumOfSquareItems;

            var x1 = (diffS + Math.Sqrt(2 * diffP - diffS * diffS)) / 2;
            var x2 = (diffS - Math.Sqrt(2 * diffP - diffS * diffS)) / 2;

            return (x1, x2);
        }
    }
}
