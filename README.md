# Municipal Services Application - South Africa

**Student Name:** Kone Moshapo  
**Project:** Municipal Services Application (Parts 1, 2 & 3)  
**Development Period:** 2026  
**Technology Stack:** C# .NET Framework 4.7.2, Windows Forms  
**Target Platform:** Windows Desktop  
**Repository:** [https://github.com/Kone-M/MunicipalServicesApp2](https://github.com/Kone-M/MunicipalServicesApp2)

---

## 📋 Project Overview

The **Municipal Services Application** is a comprehensive Windows Forms desktop application designed to bridge the gap between South African citizens and their local municipalities. The primary goal of this application is to provide a centralized, user-friendly platform where residents can report community issues, stay informed about local events, and track the status of their service requests. 

This project was developed in three distinct phases to meet the Portfolio of Evidence (PoE) requirements:
- **Part 1 (Foundation):** Built the "Report Issues" module, focusing heavily on user engagement, UI layout, file attachment handling, and strict data validation.
- **Part 2 (Data Filtering):** Expanded the application with the "Local Events and Announcements" module, leveraging built-in .NET collections (Dictionaries, Queues, Stacks, HashSets) to create a fast search system and a personalized recommendation engine.
- **Part 3 (Advanced Algorithms):** Completed the application by adding the "Service Request Status" module. This phase required the creation of **custom, from-scratch data structures** (Binary Search Trees, Min-Heaps, and Graphs) to manage, prioritize, and track municipal tickets efficiently.

---

## 🚀 Detailed Features Implemented

### Part 1: Report Issues Module
- **Issue Reporting System:** A robust form allowing users to submit detailed reports regarding municipal problems.
- **Categorized Input:** Users can specify the location, select a category from a predefined dropdown (Sanitation, Roads, Utilities, etc.), and provide a detailed description.
- **File Attachment Functionality:** Users can attach images or documents (JPG, PNG, PDF, DOCX) to support their reports using the `OpenFileDialog`. The application uses `System.IO.Path.GetFileName()` to safely extract the filename without throwing path exceptions.
- **User Engagement Features:** To encourage community participation, the form includes a dynamic progress bar and rotating encouraging messages (e.g., "Your report makes a difference!") that update as the user fills out the form.
- **Data Validation & Error Handling:** The system prevents empty submissions by validating that the Location and Description fields are filled (using `string.IsNullOrWhiteSpace()`) before allowing a user to submit. MessageBoxes warn the user of missing information.

### Part 2: Local Events & Data Filtering
- **Smart Search System:** Users can search for events by typing keywords (matching Title, Description, or Location) and filtering by specific categories. The system utilizes `Dictionary` lookups to ensure this is an O(1) operation rather than O(n).
- **Recommendation Engine:** A personalized algorithm that suggests events to the user based on a multi-tiered scoring system:
  - *User Preferences:* Suggests events in categories the user has searched for (tracked via `HashSet`).
  - *Search History:* Suggests similar events based on the last 5 searches (tracked via `Queue`).
  - *Priority:* Pushes high-priority municipal events to the top of the list.
- **Event Management & Display:** Events are displayed in a ListBox sorted by date. Clicking an event reveals full details in a dedicated RichTextBox.
- **History Tracking:** A "View History" button (using a `Stack`) shows the last 5 events viewed. A "Clear Search History" button resets the Queue, Stack, and HashSet.

### Part 3: Service Request Status (Custom Data Structures)
- **Custom Binary Search Tree (BST):** Implemented entirely from scratch. Allows users to search for specific service requests by their unique Request ID (e.g., "REQ-001") in O(log n) time. 
- **Custom Min-Heap:** Implemented from scratch. Automatically prioritizes the display of service requests. High-priority issues (Priority 1 - e.g., Water Leak) float to the top of the DataGridView without requiring manual sorting of the list.
- **Graph & Graph Traversal:** Uses a custom `WorkflowGraph` class (Adjacency List) to map the complex relationship between statuses. A Breadth-First Search (BFS) generates the "Workflow Path" for each ticket (e.g., Reported -> Assigned -> In Progress -> Resolved).
- **Ticket Management UI:** A comprehensive DataGridView allowing users to view all requests, search by ID, and update the status of a selected request using a ComboBox.

---
### 📸 Application Screenshots

*Main Menu*
<img width="679" height="485" alt="Screenshot 2026-11-13 201154" src="https://github.com/user-attachments/assets/2747579a-4826-4740-90fa-31025585f509" />

*Report Issues (Part 1)*
<img width="828" height="485" alt="Screenshot 2026-11-13 201247" src="https://github.com/user-attachments/assets/4f2ae194-6244-4f2e-a927-95fb1fda50f6" />

*Local Events & Announcements (Part 2)*
<img width="776" height="585" alt="Screenshot 2026-11-13 201215" src="https://github.com/user-attachments/assets/4c582f44-a211-426c-9b64-9255626dad0d" />

*Service Request Status Tracker (Part 3)*
<img width="577" height="385" alt="Screenshot 2026-11-13 201307" src="https://github.com/user-attachments/assets/e2992fbc-5954-48e4-9d33-9a86d92cb06b" />

---

## 🛠 Technical Requirements & Implementation Details

### Custom Data Structures (Part 3)
To meet the strict requirements of Part 3, these structures were written manually in `ServiceRequestData.cs` without relying on .NET built-in classes.

| Structure | Implementation Details | Efficiency / Big O |
| :--- | :--- | :--- |
| **Binary Search Tree (BST)** | The `RequestBST` class uses recursive `Insert` and `Search` methods. Nodes are compared using `string.Compare` on the RequestID. | **Search:** O(log n) average. **Insert:** O(log n) average. |
| **Min-Heap** | The `RequestMinHeap` class uses a `List<ServiceRequest>`. It uses `HeapifyUp` (after insertion) and `HeapifyDown` (after extraction) to maintain the heap property based on the `Priority` integer. | **Insert:** O(log n). **ExtractMin:** O(log n). **Peek (Get Root):** O(1). |
| **Graph & BFS** | The `WorkflowGraph` class uses a `Dictionary<string, List<string>>` as an adjacency list. A Queue is used to perform a Breadth-First Search to find the path to "Resolved". | **Traversal:** O(V + E) where V is vertices (statuses) and E is edges (transitions). |

### Built-in Data Structures (Part 2)

| Structure | Purpose | Implementation in Code | Efficiency |
| :--- | :--- | :--- | :--- |
| **Dictionary** | Category-based event lookup | `Dictionary<string, List<Event>>` | O(1) average time for lookup by category. |
| **SortedDictionary** | Date-based event organization | `SortedDictionary<DateTime, List<Event>>` | O(log n) insertion, keeps events sorted automatically. |
| **Queue** | Recent searches (FIFO) | `Queue<Event>` | O(1) enqueue/dequeue, limits memory to 5 items. |
| **Stack** | Viewed events (LIFO) | `Stack<Event>` | O(1) push/pop, instantly retrieves most recent item. |
| **HashSet** | Unique categories & preferences | `HashSet<string>` | O(1) lookup, prevents duplicate categories. |

### Core Algorithms
- **Heapify Logic:** The Min-Heap uses index calculations (`2 * index + 1` for left child, `2 * index + 2` for right child) to swap elements and maintain the heap invariant, ensuring Priority 1 is always at index 0.
- **BFS Traversal:** When a user updates a status, the `GetProgressPath()` method is called. It enqueues the starting status, dequeues it, adds it to a path list, and enqueues all adjacent nodes (next steps) until all reachable statuses are mapped.
- **Search Filtering (Part 2):** Utilizes LINQ `.Where()` clauses combined with `Dictionary` and `HashSet` lookups to filter events efficiently without iterating over the entire dataset.

---

## 📥 Installation & Setup

### Prerequisites
Before running this application, ensure your system meets the following requirements:
- **Operating System:** Windows 10 or 11.
- **.NET Framework:** Version 4.7.2 or later must be installed.
- **Development Environment:** Visual Studio 2019 or later (required only for compilation).
- **Disk Space:** Minimum 50MB of free space.

### Step-by-Step Compilation Guide

1.  **Clone the Repository**
    Open your command prompt or terminal and run:
    ```bash
    git clone https://github.com/Kone-M/MunicipalServicesApp2.git
