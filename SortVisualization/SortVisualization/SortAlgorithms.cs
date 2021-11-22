using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualization
{
    class SortAlgorithms
    {
        Action<uint[], int, int> swap;
        Func<TimeSpan> delay;
        public SortAlgorithms(Action<uint[], int, int> swap, Func<TimeSpan> delay)
        {
            this.swap = swap;
            this.delay = delay;
        }

        public async void QuickSort(uint[] array)
        {
            await Task.Run(() => Sort(array, 0, array.Length - 1));

            int Partition(uint[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i] < array[maxIndex])
                    {
                        pivot++;
                        Swap(array, pivot, i);
                    }
                }
                pivot++;
                Swap(array, pivot, maxIndex);
                return pivot;
            }
            uint[] Sort(uint[] array, int minIndex, int maxIndex)
            {
                if (minIndex >= maxIndex)
                    return array;

                var pivotIndex = Partition(array, minIndex, maxIndex);
                Sort(array, minIndex, pivotIndex - 1);
                Sort(array, pivotIndex + 1, maxIndex);

                return array;
            }
            async void Swap(uint[] arr, int e1, int e2)
            {
                swap(arr, e1, e2);
                await Task.Delay(delay());
            }
        }
        public async void ShellSort(uint[] array)
        {
            int d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    int j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        swap(array, j, j - d);
                        await Task.Delay(delay());
                        j -= d;
                    }
                }
                d /= 2;
            }
        }
    }
}
