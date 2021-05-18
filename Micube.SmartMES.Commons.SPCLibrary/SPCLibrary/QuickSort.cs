#region using

#endregion

using System;

namespace Micube.SmartMES.Commons.SPCLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class QuickSort
    {
        /// <summary>
        /// Sort
        /// </summary>
        /// <param name="pitem"></param>
        /// <param name="ilow"></param>
        /// <param name="ihigh"></param>
        /// <param name="isdesc"></param>
        public static void Sort(double[] pitem, int ilow, int ihigh, bool isdesc)
        {
            int ipivotpoint = 0;
            if (ihigh > ilow)
            {
                ipivotpoint = Partition(pitem, ilow, ihigh, isdesc);
                Sort(pitem, ilow, ipivotpoint - 1, isdesc);
                Sort(pitem, ipivotpoint + 1, ihigh, isdesc);
            }
        }

        /// <summary>
        /// 분리
        /// </summary>
        /// <param name="pitem"></param>
        /// <param name="ilow"></param>
        /// <param name="ihigh"></param>
        /// <param name="isdesc"></param>
        /// <returns></returns>
        private static int Partition(double[] pitem, int ilow, int ihigh, bool isdesc)
        {
            int i = 0;
            int j = 0;
            double pivot = pitem[ilow];
            j = ilow;

            //기준값(pivot)보다 작은 값들은 왼쪽으로 보냄
            if (isdesc == false)
            {
                for (i = ilow + 1; i <= ihigh; i++)
                {
                    if (pitem[i] < pivot)
                    {
                        j++;
                        Swap(pitem, i, j);
                    }
                }
                Swap(pitem, ilow, j); //오름차순
            }
            else
            {
                for (i = ilow + 1; i <= ihigh; i++)
                {
                    if (pitem[i] > pivot)
                    {
                        j++;
                        Swap(pitem, i, j);
                    }
                }
                Swap(pitem, j, ilow); //내림차순
            }
            return j;
        }

        /// <summary>
        /// 치환
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void Swap(double[] a, int i, int j)
        {
            try
            {
                double temp;
                temp = a[i];
                a[i] = a[j];
                a[j] = temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
