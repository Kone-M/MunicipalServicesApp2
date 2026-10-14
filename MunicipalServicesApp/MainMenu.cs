using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MunicipalServicesApp
{
    public partial class MainMenu : Form
    {
        // Data structure to store reported issues
        private List<ReportedIssue> reportedIssues = new List<ReportedIssue>();

        public MainMenu()
        {
            // Remove InitializeComponent() and call our setup method directly
            SetupMainMenu();
        }

        private void SetupMainMenu()
        {
            this.Text = "Municipal Services Application - South Africa";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.LightBlue;

            // Main title
            Label titleLabel = new Label();
            titleLabel.Text = "Municipal Services Application";
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(150, 50);
            this.Controls.Add(titleLabel);

            // Report Issues button
            Button reportIssuesBtn = new Button();
            reportIssuesBtn.Text = "1. Report Issues";
            reportIssuesBtn.Size = new Size(200, 40);
            reportIssuesBtn.Location = new Point(200, 120);
            reportIssuesBtn.BackColor = Color.SteelBlue;
            reportIssuesBtn.ForeColor = Color.White;
            reportIssuesBtn.Font = new Font("Arial", 10, FontStyle.Bold);
            reportIssuesBtn.Click += ReportIssuesBtn_Click;
            this.Controls.Add(reportIssuesBtn);

            // Local Events button
            Button localEventsBtn = new Button();
            localEventsBtn.Text = "2. Local Events and Announcements";
            localEventsBtn.Size = new Size(200, 40);
            localEventsBtn.Location = new Point(200, 180);
            localEventsBtn.BackColor = Color.SteelBlue;
            localEventsBtn.ForeColor = Color.White;
            localEventsBtn.Font = new Font("Arial", 10, FontStyle.Bold);
            localEventsBtn.Click += LocalEventsBtn_Click;
            this.Controls.Add(localEventsBtn);

            // Service Request Status button (disabled for now)
            Button serviceStatusBtn = new Button();
            serviceStatusBtn.Text = "3. Service Request Status (Coming Soon)";
            serviceStatusBtn.Size = new Size(200, 40);
            serviceStatusBtn.Location = new Point(200, 240);
            serviceStatusBtn.BackColor = Color.Gray;
            serviceStatusBtn.ForeColor = Color.White;
            serviceStatusBtn.Font = new Font("Arial", 10, FontStyle.Bold);
            serviceStatusBtn.Enabled = false;
            this.Controls.Add(serviceStatusBtn);
        }

        private void ReportIssuesBtn_Click(object sender, EventArgs e)
        {
            ReportIssuesForm reportForm = new ReportIssuesForm();
            reportForm.ShowDialog();
        }

        private void LocalEventsBtn_Click(object sender, EventArgs e)
        {
            LocalEventsForm eventsForm = new LocalEventsForm();
            eventsForm.ShowDialog();
        }
    }

    // Data class for reported issues
    public class ReportedIssue
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string AttachmentPath { get; set; }
        public DateTime ReportDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}