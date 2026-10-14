# Municipal Services Application - South Africa

**Student Name:** Kone Moshapo
**Project:** Municipal Services Application (Parts 1 & 2)
**Development Period:** 2026
**Technology Stack:** C# .NET Framework 4.7.2, Windows Forms
**Target Platform:** Windows Desktop

---

## 📋 Project Overview

A comprehensive C# Windows Forms application developed for South African municipalities to facilitate citizen engagement and service management. The application implements advanced data structures and algorithms to provide efficient municipal services.

### Part 1: Report Issues Module
- ✅ **Issue Reporting System** with location, category, and description
- ✅ **File Attachment** functionality for images and documents
- ✅ **User Engagement Features** with progress tracking and encouraging messages
- ✅ **Data Validation** and error handling
- ✅ **Navigation** between forms

### Part 2: Local Events & Advanced Data Structures
- ✅ **Advanced Data Structures** implementation:
  - `Dictionary` for category-based event organization
  - `SortedDictionary` for chronological event sorting
  - `Queue` for search history management
  - `Stack` for viewing history tracking
  - `HashSet` for unique category and user preference management
  - Priority Queue simulation using `SortedDictionary`
- ✅ **Smart Search System** with category filtering and keyword matching
- ✅ **Recommendation Engine** based on user preferences, search history, and event priority
- ✅ **Event Management** with detailed information display
- ✅ **History Tracking** for user interactions (View History & Clear History features)

---

## 🛠 Technical Requirements Met

### Data Structures Implemented

| Structure | Purpose | Implementation |
| :--- | :--- | :--- |
| **Dictionary** | Category-based event lookup | `Dictionary<string, List<Event>>` |
| **SortedDictionary** | Date-based event organization | `SortedDictionary<DateTime, List<Event>>` |
| **Queue** | Recent searches (FIFO) | `Queue<Event>` |
| **Stack** | Viewed events (LIFO) | `Stack<Event>` |
| **HashSet** | Unique categories & preferences | `HashSet<string>` |
| **List** | General data storage | `List<ReportedIssue>` |

### Algorithms & Features
- **Search Algorithm** with category filtering and text matching.
- **Recommendation Algorithm** based on:
  - User preferences (`HashSet`)
  - Search history (`Queue`)
  - Priority events (`SortedDictionary`)
- **Progress Tracking** with dynamic messaging in the Report Issues form.
- **File Handling** with `OpenFileDialog` integration.

---

## 📥 Installation & Setup

### Prerequisites
- **Operating System:** Windows 7 or later
- **.NET Framework:** 4.7.2 or later
- **Development Environment:** Visual Studio 2019 or later (for compilation)
- **Storage:** Minimum 50MB free space

### Step-by-Step Compilation Guide

1.  **Clone the Repository**
    ```bash
    git clone https://github.com/Kone-M/MunicipalServicesApp2.git
