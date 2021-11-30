namespace NDP.MathUtils
{
    public class MathUtils
    {
        /// <summary>
        /// Returns greatest common factor of set of integers
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static int GreatestCommonFactor(params int[] ints)
        {
            for (int i = ints[GetMinIndex(ints)]; i > 0; i--)
            {
                bool next = false;

                for (int j = 0; j < ints.Length; j++)
                {
                    if (ints[j] % i != 0)
                    {
                        next = true;
                        break;
                    }
                }

                if (next)
                {
                    continue;
                }
                return i;
            }
            return 1;
        }

        /// <summary>
        /// Returns index of minimal element in array
        /// </summary>
        /// <param name="ints"></param>
        /// <returns></returns>
        public static int GetMinIndex(int[] ints)
        {
            int index = 0;
            int min = ints[0];
            for (int i = 0; i < ints.Length; i++)
            {
                if (min < ints[i])
                {
                    index = i;
                }
            }
            return index;
        }

        public static int[] Swap(int[] ints, int indexFrom, int indexTo)
        {
            int[] nums = ints;
            int from = ints[indexFrom];
            int to = ints[indexTo];
            nums[indexFrom] = to;
            nums[indexTo] = from;

            return nums;
        }
    }
}
