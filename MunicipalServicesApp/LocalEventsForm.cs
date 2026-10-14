using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        // Advanced Data Structures for Part 2
        private Dictionary<string, List<Event>> eventsByCategory = new Dictionary<string, List<Event>>();
        private SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();
        private Queue<Event> recentSearches = new Queue<Event>();
        private Stack<Event> viewedEvents = new Stack<Event>();
        private HashSet<string> uniqueCategories = new HashSet<string>();
        private HashSet<string> userPreferences = new HashSet<string>();

        // Priority queue simulation using SortedDictionary
        private SortedDictionary<int, Event> priorityEvents = new SortedDictionary<int, Event>();

        public LocalEventsForm()
        {
            // Remove InitializeComponent() and call our setup methods directly
            InitializeSampleData();
            SetupLocalEventsForm();
        }

        private void InitializeSampleData()
        {
            // Sample events for demonstration
            var events = new List<Event>
            {
                new Event("Community Clean-up", "Public Safety", "Join your neighbors for a community clean-up day",
                         new DateTime(2024, 2, 15), "City Park", 1),
                new Event("Budget Meeting", "Government", "Municipal budget planning meeting",
                         new DateTime(2024, 2, 20), "Town Hall", 2),
                new Event("Youth Sports Day", "Sports", "Annual youth sports competition",
                         new DateTime(2024, 2, 25), "Sports Complex", 3),
                new Event("Arts Festival", "Culture", "Local artists showcase their work",
                         new DateTime(2024, 3, 1), "Cultural Center", 1),
                new Event("Road Maintenance Info", "Infrastructure", "Information session on road maintenance plans",
                         new DateTime(2024, 2, 18), "Community Center", 2),
                new Event("Farmers Market", "Commerce", "Weekly farmers market with local produce",
                         new DateTime(2024, 2, 17), "Market Square", 3),
                new Event("Emergency Preparedness", "Public Safety", "Workshop on emergency preparedness",
                         new DateTime(2024, 2, 22), "Fire Station", 1),
                new Event("Music Concert", "Entertainment", "Live music performance by local artists",
                         new DateTime(2024, 3, 5), "City Amphitheater", 2)
            };

            // Initialize data structures
            foreach (var eventItem in events)
            {
                // Dictionary by category
                if (!eventsByCategory.ContainsKey(eventItem.Category))
                    eventsByCategory[eventItem.Category] = new List<Event>();
                eventsByCategory[eventItem.Category].Add(eventItem);

                // SortedDictionary by date
                if (!eventsByDate.ContainsKey(eventItem.Date))
                    eventsByDate[eventItem.Date] = new List<Event>();
                eventsByDate[eventItem.Date].Add(eventItem);

                // HashSet for unique categories
                uniqueCategories.Add(eventItem.Category);

                // Priority queue simulation
                priorityEvents[eventItem.Priority] = eventItem;
            }
        }

        private void SetupLocalEventsForm()
        {
            this.Text = "Local Events and Announcements";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Local Events & Announcements";
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(280, 20);
            this.Controls.Add(titleLabel);

            // Search Section
            SetupSearchSection();

            // Events Display Section
            SetupEventsDisplay();

            // Recommendations Section
            SetupRecommendationsSection();

            // Navigation buttons
            SetupNavigationButtons();

            // Load initial events
            LoadAllEvents();
        }

        private void SetupSearchSection()
        {
            // Search Label
            Label searchLabel = new Label();
            searchLabel.Text = "Search Events:";
            searchLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            searchLabel.AutoSize = true;
            searchLabel.Location = new Point(50, 70);
            this.Controls.Add(searchLabel);

            // Search TextBox
            TextBox searchTextBox = new TextBox();
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(200, 25);
            searchTextBox.Location = new Point(160, 70);
            searchTextBox.Font = new Font("Arial", 10, FontStyle.Regular);
            this.Controls.Add(searchTextBox);

            // Category Filter
            Label categoryLabel = new Label();
            categoryLabel.Text = "Category:";
            categoryLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new Point(380, 70);
            this.Controls.Add(categoryLabel);

            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(150, 25);
            categoryComboBox.Location = new Point(450, 70);
            categoryComboBox.Font = new Font("Arial", 10, FontStyle.Regular);
            categoryComboBox.Items.Add("All Categories");
            foreach (var category in uniqueCategories)
            {
                categoryComboBox.Items.Add(category);
            }
            categoryComboBox.SelectedIndex = 0;
            this.Controls.Add(categoryComboBox);

            // Search Button
            Button searchButton = new Button();
            searchButton.Text = "Search";
            searchButton.Size = new Size(80, 25);
            searchButton.Location = new Point(620, 70);
            searchButton.BackColor = Color.SteelBlue;
            searchButton.ForeColor = Color.White;
            searchButton.Font = new Font("Arial", 9, FontStyle.Bold);
            searchButton.Click += SearchButton_Click;
            this.Controls.Add(searchButton);
        }

        private void SetupEventsDisplay()
        {
            // Events ListBox
            ListBox eventsListBox = new ListBox();
            eventsListBox.Name = "eventsListBox";
            eventsListBox.Size = new Size(500, 200);
            eventsListBox.Location = new Point(50, 120);
            eventsListBox.Font = new Font("Arial", 9, FontStyle.Regular);
            eventsListBox.SelectedIndexChanged += EventsListBox_SelectedIndexChanged;
            this.Controls.Add(eventsListBox);

            // Event Details Label
            Label detailsLabel = new Label();
            detailsLabel.Text = "Event Details:";
            detailsLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            detailsLabel.AutoSize = true;
            detailsLabel.Location = new Point(50, 340);
            this.Controls.Add(detailsLabel);

            // Event Details RichTextBox
            RichTextBox detailsRichTextBox = new RichTextBox();
            detailsRichTextBox.Name = "detailsRichTextBox";
            detailsRichTextBox.Size = new Size(500, 120);
            detailsRichTextBox.Location = new Point(50, 370);
            detailsRichTextBox.Font = new Font("Arial", 9, FontStyle.Regular);
            detailsRichTextBox.ReadOnly = true;
            detailsRichTextBox.BackColor = Color.LightGray;
            this.Controls.Add(detailsRichTextBox);
        }

        private void SetupRecommendationsSection()
        {
            // Recommendations Label
            Label recLabel = new Label();
            recLabel.Text = "Recommended Events:";
            recLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            recLabel.AutoSize = true;
            recLabel.Location = new Point(570, 120);
            this.Controls.Add(recLabel);

            // Recommendations ListBox
            ListBox recListBox = new ListBox();
            recListBox.Name = "recListBox";
            recListBox.Size = new Size(200, 150);
            recListBox.Location = new Point(570, 150);
            recListBox.Font = new Font("Arial", 8, FontStyle.Regular);
            this.Controls.Add(recListBox);
        }

        private void SetupNavigationButtons()
        {
            // Back Button
            Button backButton = new Button();
            backButton.Text = "Back to Main Menu";
            backButton.Size = new Size(120, 35);
            backButton.Location = new Point(50, 510);
            backButton.BackColor = Color.SteelBlue;
            backButton.ForeColor = Color.White;
            backButton.Font = new Font("Arial", 9, FontStyle.Regular);
            backButton.Click += (s, e) => this.Close();
            this.Controls.Add(backButton);

            // View History Button (Stack demonstration)
            Button historyButton = new Button();
            historyButton.Text = "View History";
            historyButton.Size = new Size(100, 35);
            historyButton.Location = new Point(180, 510);
            historyButton.BackColor = Color.DarkOrange;
            historyButton.ForeColor = Color.White;
            historyButton.Font = new Font("Arial", 9, FontStyle.Regular);
            historyButton.Click += HistoryButton_Click;
            this.Controls.Add(historyButton);

            // Clear Search History Button (Queue demonstration)
            Button clearHistoryButton = new Button();
            clearHistoryButton.Text = "Clear Search History";
            clearHistoryButton.Size = new Size(120, 35);
            clearHistoryButton.Location = new Point(290, 510);
            clearHistoryButton.BackColor = Color.OrangeRed;
            clearHistoryButton.ForeColor = Color.White;
            clearHistoryButton.Font = new Font("Arial", 9, FontStyle.Regular);
            clearHistoryButton.Click += ClearHistoryButton_Click;
            this.Controls.Add(clearHistoryButton);
        }

        private void LoadAllEvents()
        {
            var eventsListBox = this.Controls.Find("eventsListBox", true)[0] as ListBox;
            eventsListBox.Items.Clear();

            // Using SortedDictionary to display events in date order
            foreach (var dateEvents in eventsByDate)
            {
                foreach (var eventItem in dateEvents.Value)
                {
                    eventsListBox.Items.Add($"{dateEvents.Key:MMM dd}: {eventItem.Title} - {eventItem.Category}");
                }
            }

            // Update recommendations
            UpdateRecommendations();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchTextBox = this.Controls.Find("searchTextBox", true)[0] as TextBox;
            var categoryComboBox = this.Controls.Find("categoryComboBox", true)[0] as ComboBox;
            var eventsListBox = this.Controls.Find("eventsListBox", true)[0] as ListBox;

            string searchTerm = searchTextBox.Text.ToLower();
            string selectedCategory = categoryComboBox.SelectedItem.ToString();

            eventsListBox.Items.Clear();

            // Search logic using Dictionary and HashSet for efficiency
            var searchResults = new List<Event>();

            if (selectedCategory == "All Categories")
            {
                // Search across all categories
                foreach (var categoryEvents in eventsByCategory.Values)
                {
                    searchResults.AddRange(categoryEvents.Where(ev =>
                        ev.Title.ToLower().Contains(searchTerm) ||
                        ev.Description.ToLower().Contains(searchTerm) ||
                        ev.Location.ToLower().Contains(searchTerm)));
                }
            }
            else
            {
                // Search within specific category using Dictionary
                if (eventsByCategory.ContainsKey(selectedCategory))
                {
                    searchResults = eventsByCategory[selectedCategory].Where(ev =>
                        ev.Title.ToLower().Contains(searchTerm) ||
                        ev.Description.ToLower().Contains(searchTerm) ||
                        ev.Location.ToLower().Contains(searchTerm)).ToList();
                }
            }

            // Add to recent searches queue (limit to 5)
            foreach (var result in searchResults.Take(3))
            {
                if (recentSearches.Count >= 5)
                    recentSearches.Dequeue(); // Remove oldest search
                recentSearches.Enqueue(result);
            }

            // Update user preferences based on search
            if (!string.IsNullOrEmpty(searchTerm))
            {
                userPreferences.Add(selectedCategory);
            }

            // Display results
            foreach (var eventItem in searchResults.OrderBy(ev => ev.Date))
            {
                eventsListBox.Items.Add($"{eventItem.Date:MMM dd}: {eventItem.Title} - {eventItem.Category}");
            }

            if (searchResults.Count == 0)
            {
                eventsListBox.Items.Add("No events found matching your search criteria.");
            }

            // Update recommendations based on search
            UpdateRecommendations();
        }

        private void EventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventsListBox = this.Controls.Find("eventsListBox", true)[0] as ListBox;
            var detailsRichTextBox = this.Controls.Find("detailsRichTextBox", true)[0] as RichTextBox;

            if (eventsListBox.SelectedIndex >= 0)
            {
                string selectedItem = eventsListBox.SelectedItem.ToString();

                // Find the event in our data structures
                var selectedEvent = FindEventByDisplayString(selectedItem);

                if (selectedEvent != null)
                {
                    // Add to viewed events stack
                    viewedEvents.Push(selectedEvent);

                    // Display event details
                    detailsRichTextBox.Text = $"Title: {selectedEvent.Title}\n\n" +
                                            $"Category: {selectedEvent.Category}\n\n" +
                                            $"Date: {selectedEvent.Date:dd MMMM yyyy}\n\n" +
                                            $"Location: {selectedEvent.Location}\n\n" +
                                            $"Description: {selectedEvent.Description}\n\n" +
                                            $"Priority: {selectedEvent.Priority}";
                }
            }
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            var detailsRichTextBox = this.Controls.Find("detailsRichTextBox", true)[0] as RichTextBox;

            if (viewedEvents.Count > 0)
            {
                // Show viewing history using Stack
                detailsRichTextBox.Text = "Recently Viewed Events (Most Recent First):\n\n";
                int count = 1;
                foreach (var eventItem in viewedEvents)
                {
                    detailsRichTextBox.Text += $"{count}. {eventItem.Title} ({eventItem.Date:MMM dd})\n";
                    count++;
                    if (count > 5) break; // Show only last 5
                }
            }
            else
            {
                detailsRichTextBox.Text = "No event viewing history available.";
            }
        }

        private void ClearHistoryButton_Click(object sender, EventArgs e)
        {
            // Clear both stacks and queues
            recentSearches.Clear();
            viewedEvents.Clear();
            userPreferences.Clear();

            var detailsRichTextBox = this.Controls.Find("detailsRichTextBox", true)[0] as RichTextBox;
            detailsRichTextBox.Text = "Search and viewing history cleared.";

            UpdateRecommendations();
        }

        private void UpdateRecommendations()
        {
            var recListBox = this.Controls.Find("recListBox", true)[0] as ListBox;
            recListBox.Items.Clear();

            // Recommendation algorithm based on user preferences and search history
            var recommendations = new List<Event>();

            // Priority-based recommendations (using SortedDictionary)
            if (priorityEvents.Count > 0)
            {
                recommendations.AddRange(priorityEvents.Values.Take(2));
            }

            // Category-based recommendations (using user preferences HashSet)
            foreach (var preference in userPreferences)
            {
                if (eventsByCategory.ContainsKey(preference))
                {
                    var categoryEvents = eventsByCategory[preference]
                        .Where(ev => ev.Date >= DateTime.Today)
                        .OrderBy(ev => ev.Date)
                        .Take(2);

                    recommendations.AddRange(categoryEvents);
                }
            }

            // Recent searches-based recommendations (using Queue)
            foreach (var recentSearch in recentSearches)
            {
                var similarEvents = eventsByCategory.Values
                    .SelectMany(list => list)
                    .Where(ev => ev.Category == recentSearch.Category && ev != recentSearch)
                    .Take(1);

                recommendations.AddRange(similarEvents);
            }

            // Remove duplicates and limit to 5 recommendations
            var uniqueRecommendations = recommendations
                .Distinct()
                .OrderBy(ev => ev.Date)
                .Take(5)
                .ToList();

            if (uniqueRecommendations.Count > 0)
            {
                foreach (var rec in uniqueRecommendations)
                {
                    recListBox.Items.Add($"{rec.Date:MMM dd}: {rec.Title}");
                }
            }
            else
            {
                // Fallback: Show upcoming events
                var upcomingEvents = eventsByDate
                    .Where(kv => kv.Key >= DateTime.Today)
                    .SelectMany(kv => kv.Value)
                    .Take(3);

                foreach (var eventItem in upcomingEvents)
                {
                    recListBox.Items.Add($"{eventItem.Date:MMM dd}: {eventItem.Title}");
                }
            }
        }

        private Event FindEventByDisplayString(string displayString)
        {
            // Helper method to find event from display string
            foreach (var dateEvents in eventsByDate)
            {
                foreach (var eventItem in dateEvents.Value)
                {
                    if (displayString.Contains(eventItem.Title) && displayString.Contains(eventItem.Category))
                    {
                        return eventItem;
                    }
                }
            }
            return null;
        }
    }

    // Event class for Part 2
    public class Event
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int Priority { get; set; } // 1=High, 2=Medium, 3=Low

        public Event(string title, string category, string description, DateTime date, string location, int priority)
        {
            Title = title;
            Category = category;
            Description = description;
            Date = date;
            Location = location;
            Priority = priority;
        }
    }
}