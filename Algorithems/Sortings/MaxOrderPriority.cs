using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithems.Sortings
{
    public class MaxOrderPriority
    {
        private List<int> heap = new List<int>();

        public MaxOrderPriority()
        {}


        public MaxOrderPriority(IEnumerable<int> items)
        {
            heap = new List<int>(items);
            BuildHeap();
        }

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;

        public void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count -1);
        }

        public int PeekMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Priority is empty.");
            return heap[0];
        }

        public int ExtractMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Priority is empty");

            int max = heap[0];
            int lastIndex = heap.Count - 1;

            heap[0] = heap[lastIndex];

            heap.RemoveAt(lastIndex);
            if (!IsEmpty)  SiftDown(0);
            
            return max;
        }

        private void SiftUp(int index)
        {
            int current = index;

            int parrent = (current - 1) / 2;
            while (current>0)
            {
                if (heap[parrent] < heap[current])
                {
                    Swap(parrent,current);
                    current = parrent;
                }
                else
                {
                    break;
                }
            }
        }

        private void SiftDown(int index)
        {
            int current = index;
            int n = heap.Count;

            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int largest = current;

                if (left < n && heap[left] > heap[largest])
                {
                    largest = left;
                }

                if (right < n && heap[right] > heap[largest])
                {
                    largest = right;
                }

                if(largest == current) break;

                Swap(current,largest);
                current = largest;


            }
        }

        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }

        private void BuildHeap()
        {
            int n = heap.Count;
            for (int i = n/2-1; i >=0; i--)
            {
                SiftDown(i);
            }
        }
    }
}
