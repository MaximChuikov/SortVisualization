using System;
using System.Threading.Tasks;

namespace SortVisualization
{
    class SortAlgorithms
    {
        Action<uint[], int, int> swap;
        Func<TimeSpan> delay;
        Action<string, string, string> addLog;
        public SortAlgorithms(Action<uint[], int, int> swap, Func<TimeSpan> delay, Action<string, string, string> addLog)
        {
            this.addLog = addLog;
            this.swap = swap;
            this.delay = delay;
        }
        public void QuickSort(uint[] array)
        {
            uint counter = 0;
            Sort(array, 0, array.Length - 1);

            async Task<int> Partition(uint[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i < maxIndex; i++)
                {
                    if (array[i] < array[maxIndex])
                    {
                        pivot++;
                        swap(array, pivot, i);
                        addLog($"Действие номер {++counter}", $"Ширина сортировки {maxIndex - minIndex}", $"swap {pivot} и {i}");
                        await Task.Delay(delay());
                    }
                }
                pivot++;
                swap(array, pivot, maxIndex);
                addLog($"Действие номер {++counter}", $"Ширина сортировки {maxIndex - minIndex}", $"swap {pivot} и {maxIndex}");
                await Task.Delay(delay());
                return pivot;
            }
            async void Sort(uint[] array, int minIndex, int maxIndex)
            {
                if (minIndex >= maxIndex)
                    return;
                var pivotIndex = await Partition(array, minIndex, maxIndex);
                Sort(array, minIndex, pivotIndex - 1);
                Sort(array, pivotIndex + 1, maxIndex);
                return;
            }
        }
        public async void ShellSort(uint[] array)
        {
            uint counter = 0;
            int d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    int j = i;
                    while ((j >= d) && (array[j - d] > array[j]))
                    {
                        swap(array, j, j - d);
                        addLog($"Действие номер {++counter}", $"Ширина сортировки {d}", $"swap {j} и {j - d}");
                        await Task.Delay(delay());
                        j -= d;
                    }
                }
                d /= 2;
            }
        }
    }
}
