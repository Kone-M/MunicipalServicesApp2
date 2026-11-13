using System;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    // 1. The Data Model
    public class ServiceRequest
    {
        public string RequestID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; } // 1 = High, 2 = Medium, 3 = Low
        public DateTime DateSubmitted { get; set; }

        public ServiceRequest(string id, string desc, string status, int priority)
        {
            RequestID = id;
            Description = desc;
            Status = status;
            Priority = priority;
            DateSubmitted = DateTime.Now;
        }
    }

    // 2. Binary Search Tree (BST) for efficient searching
    public class RequestBST
    {
        private class Node
        {
            public ServiceRequest Data;
            public Node Left, Right;
            public Node(ServiceRequest data) { Data = data; Left = Right = null; }
        }

        private Node root;

        public void Insert(ServiceRequest request)
        {
            root = InsertRec(root, request);
        }

        private Node InsertRec(Node root, ServiceRequest request)
        {
            if (root == null) return new Node(request);
            if (string.Compare(request.RequestID, root.Data.RequestID) < 0)
                root.Left = InsertRec(root.Left, request);
            else if (string.Compare(request.RequestID, root.Data.RequestID) > 0)
                root.Right = InsertRec(root.Right, request);
            return root;
        }

        public ServiceRequest Search(string id)
        {
            return SearchRec(root, id);
        }

        private ServiceRequest SearchRec(Node root, string id)
        {
            if (root == null || root.Data.RequestID == id) return root?.Data;
            if (string.Compare(id, root.Data.RequestID) < 0)
                return SearchRec(root.Left, id);
            return SearchRec(root.Right, id);
        }
    }

    // 3. Min-Heap to prioritize urgent requests
    public class RequestMinHeap
    {
        private List<ServiceRequest> heap = new List<ServiceRequest>();

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }

        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0) return null;
            var min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return min;
        }

        public List<ServiceRequest> GetSortedList()
        {
            var sorted = new List<ServiceRequest>();
            var tempHeap = new List<ServiceRequest>(heap);

            while (heap.Count > 0)
            {
                sorted.Add(ExtractMin());
            }

            foreach (var item in tempHeap) Insert(item);
            return sorted;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (heap[index].Priority >= heap[parent].Priority) break;
                Swap(index, parent);
                index = parent;
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;
            while (true)
            {
                int left = 2 * index + 1;
                int right = 2 * index + 2;
                int smallest = index;

                if (left <= lastIndex && heap[left].Priority < heap[smallest].Priority)
                    smallest = left;
                if (right <= lastIndex && heap[right].Priority < heap[smallest].Priority)
                    smallest = right;

                if (smallest == index) break;
                Swap(index, smallest);
                index = smallest;
            }
        }

        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }

    // 4. Graph to represent the workflow (Traversal)
    public class WorkflowGraph
    {
        private Dictionary<string, List<string>> adjList = new Dictionary<string, List<string>>();

        public WorkflowGraph()
        {
            AddEdge("Reported", "Assigned");
            AddEdge("Assigned", "In Progress");
            AddEdge("In Progress", "Resolved");
        }

        private void AddEdge(string from, string to)
        {
            if (!adjList.ContainsKey(from)) adjList[from] = new List<string>();
            adjList[from].Add(to);
        }

        public string GetProgressPath(string currentStatus)
        {
            if (!adjList.ContainsKey(currentStatus)) return "Resolved (No further steps).";

            Queue<string> queue = new Queue<string>();
            List<string> path = new List<string>();
            queue.Enqueue(currentStatus);

            while (queue.Count > 0)
            {
                string curr = queue.Dequeue();
                path.Add(curr);

                if (adjList.ContainsKey(curr))
                {
                    foreach (var next in adjList[curr])
                    {
                        if (!path.Contains(next))
                            queue.Enqueue(next);
                    }
                }
            }
            return string.Join(" -> ", path);
        }
    }
}