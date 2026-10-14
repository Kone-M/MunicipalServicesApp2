# Municipal Services Application - South Africa

**Student Name:** Kone Moshapo  
**Project:** Municipal Services Application (Parts 1 & 2)  
**Development Period:** 2026  
**Technology Stack:** C# .NET Framework 4.7.2, Windows Forms  
**Target Platform:** Windows Desktop  
**Repository:** [https://github.com/Kone-M/MunicipalServicesApp2](https://github.com/Kone-M/MunicipalServicesApp2)

---

## 📋 Project Overview

The **Municipal Services Application** is a comprehensive Windows Forms desktop application designed to bridge the gap between South African citizens and their local municipalities. The primary goal of this application is to provide a centralized, user-friendly platform where residents can report community issues, stay informed about local events, and eventually track the status of their service requests. 

This project was developed in two distinct phases. **Part 1** focused on building the foundational "Report Issues" module, emphasizing user engagement and data validation. **Part 2** significantly expanded the application by introducing the "Local Events and Announcements" module, which leverages complex, advanced data structures and algorithms to deliver a seamless search experience and a personalized recommendation engine.

---

## 🚀 Features Implemented

### Part 1: Report Issues Module
- ✅ **Issue Reporting System:** A robust form allowing users to submit detailed reports regarding municipal problems.
- ✅ **Categorized Input:** Users can specify the location, select a category from a predefined dropdown (Sanitation, Roads, Utilities, etc.), and provide a detailed description.
- ✅ **File Attachment Functionality:** Users can attach images or documents (JPG, PNG, PDF, DOCX) to support their reports using the `OpenFileDialog`.
- ✅ **User Engagement Features:** To encourage community participation, the form includes a dynamic progress bar and rotating encouraging messages (e.g., "Your report makes a difference!") that update as the user fills out the form.
- ✅ **Data Validation & Error Handling:** The system prevents empty submissions by validating that the Location and Description fields are filled before allowing a user to submit.

### Part 2: Local Events & Advanced Data Structures
- ✅ **Advanced Data Structures:** The core of Part 2 relies on efficient data management. The application utilizes:
  - `Dictionary` for O(1) category-based event organization.
  - `SortedDictionary` for automatic chronological sorting of events by date.
  - `Queue` for managing recent searches (FIFO).
  - `Stack` for tracking viewed events (LIFO).
  - `HashSet` for managing unique categories and user preferences without duplicates.
  - Priority Queue simulation using `SortedDictionary` for high-priority events.
- ✅ **Smart Search System:** Users can search for events by typing keywords (matching Title, Description, or Location) and filtering by specific categories.
- ✅ **Recommendation Engine:** A personalized algorithm that suggests events to the user based on their category preferences, recent search history, and event priority levels.
- ✅ **Event Management & Display:** Events are displayed in an aesthetically pleasing ListBox, sorted by date. Clicking an event reveals full details in a dedicated RichTextBox.
- ✅ **History Tracking:** A "View History" button (using a Stack) shows the last 5 events viewed, while a "Clear Search History" button resets the Queue, Stack, and HashSet for a fresh start.

---

## 🛠 Technical Requirements Met

### Data Structures Implemented

| Structure | Purpose | Implementation in Code | Efficiency |
| :--- | :--- | :--- | :--- |
| **Dictionary** | Category-based event lookup | `Dictionary<string, List<Event>>` | O(1) average time for lookup by category. |
| **SortedDictionary** | Date-based event organization | `SortedDictionary<DateTime, List<Event>>` | O(log n) insertion, keeps events sorted automatically. |
| **Queue** | Recent searches (FIFO) | `Queue<Event>` | O(1) enqueue/dequeue, limits memory to 5 items. |
| **Stack** | Viewed events (LIFO) | `Stack<Event>` | O(1) push/pop, instantly retrieves most recent item. |
| **HashSet** | Unique categories & preferences | `HashSet<string>` | O(1) lookup, prevents duplicate categories. |
| **List** | General data storage | `List<ReportedIssue>` | Dynamic array for storing submitted issues. |

### Algorithms & Features
- **Search Algorithm:** Utilizes LINQ `.Where()` clauses combined with `Dictionary` and `HashSet` lookups to filter events efficiently without iterating over the entire dataset.
- **Recommendation Algorithm:** A multi-tiered scoring system that pulls data from:
  - User preferences (`HashSet`) - suggests events in categories the user has searched for.
  - Search history (`Queue`) - suggests similar events based on the last 5 searches.
  - Priority events (`SortedDictionary`) - pushes high-priority municipal events to the top of the list.
- **Progress Tracking:** Uses integer counters and `Math.Min` to safely update UI elements (ProgressBar and Label) without exceeding array bounds.
- **File Handling:** Utilizes `System.IO.Path.GetFileName()` to safely extract filenames from the `OpenFileDialog` without crashing.

---

## 📥 Installation & Setup

### Prerequisites
Before running this application, ensure your system meets the following requirements:
- **Operating System:** Windows 7, 8, 10, or 11.
- **.NET Framework:** Version 4.7.2 or later must be installed.
- **Development Environment:** Visual Studio 2019 or later (required only for compilation).
- **Disk Space:** Minimum 50MB of free space.

### Step-by-Step Compilation Guide

1.  **Clone the Repository**
    Open your command prompt or terminal and run:
    ```bash
    git clone https://github.com/Kone-M/MunicipalServicesApp2.git
