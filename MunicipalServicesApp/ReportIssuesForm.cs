using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MunicipalServicesApp
{
    public partial class ReportIssuesForm : Form
    {
        private string attachedFilePath = "";
        private int progressStep = 0;
        private string[] encouragingMessages = {
            "Thank you for helping improve our community!",
            "Your report makes a difference!",
            "Together we build a better municipality!",
            "Community heroes report issues!",
            "Your voice matters in local governance!"
        };

        public ReportIssuesForm()
        {
            // Remove InitializeComponent() and call our setup method directly
            SetupReportIssuesForm();
        }

        private void SetupReportIssuesForm()
        {
            this.Text = "Report Issues - Municipal Services";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Report an Issue";
            titleLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(280, 20);
            this.Controls.Add(titleLabel);

            // Location Input
            Label locationLabel = new Label();
            locationLabel.Text = "Location:";
            locationLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            locationLabel.AutoSize = true;
            locationLabel.Location = new Point(50, 70);
            this.Controls.Add(locationLabel);

            TextBox locationTextBox = new TextBox();
            locationTextBox.Name = "locationTextBox";
            locationTextBox.Size = new Size(300, 25);
            locationTextBox.Location = new Point(150, 70);
            locationTextBox.Font = new Font("Arial", 10, FontStyle.Regular);
            this.Controls.Add(locationTextBox);

            // Category Selection
            Label categoryLabel = new Label();
            categoryLabel.Text = "Category:";
            categoryLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            categoryLabel.AutoSize = true;
            categoryLabel.Location = new Point(50, 110);
            this.Controls.Add(categoryLabel);

            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.Name = "categoryComboBox";
            categoryComboBox.Size = new Size(300, 25);
            categoryComboBox.Location = new Point(150, 110);
            categoryComboBox.Font = new Font("Arial", 10, FontStyle.Regular);
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.AddRange(new string[] {
                "Sanitation", "Roads", "Utilities", "Public Safety",
                "Water Services", "Electricity", "Parks and Recreation",
                "Waste Management", "Public Transport", "Other"
            });
            categoryComboBox.SelectedIndex = 0;
            this.Controls.Add(categoryComboBox);

            // Description Box
            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Description:";
            descriptionLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(50, 150);
            this.Controls.Add(descriptionLabel);

            RichTextBox descriptionRichTextBox = new RichTextBox();
            descriptionRichTextBox.Name = "descriptionRichTextBox";
            descriptionRichTextBox.Size = new Size(500, 100);
            descriptionRichTextBox.Location = new Point(150, 150);
            descriptionRichTextBox.Font = new Font("Arial", 10, FontStyle.Regular);
            this.Controls.Add(descriptionRichTextBox);

            // Media Attachment
            Label attachmentLabel = new Label();
            attachmentLabel.Text = "Attachment:";
            attachmentLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            attachmentLabel.AutoSize = true;
            attachmentLabel.Location = new Point(50, 270);
            this.Controls.Add(attachmentLabel);

            Button attachButton = new Button();
            attachButton.Text = "Attach File";
            attachButton.Size = new Size(100, 30);
            attachButton.Location = new Point(150, 270);
            attachButton.BackColor = Color.LightSteelBlue;
            attachButton.Font = new Font("Arial", 9, FontStyle.Regular);
            attachButton.Click += AttachButton_Click;
            this.Controls.Add(attachButton);

            Label attachmentStatusLabel = new Label();
            attachmentStatusLabel.Name = "attachmentStatusLabel";
            attachmentStatusLabel.Text = "No file attached";
            attachmentStatusLabel.Font = new Font("Arial", 9, FontStyle.Italic);
            attachmentStatusLabel.AutoSize = true;
            attachmentStatusLabel.Location = new Point(260, 275);
            attachmentStatusLabel.ForeColor = Color.Gray;
            this.Controls.Add(attachmentStatusLabel);

            // Engagement Feature - Progress Bar
            Label progressLabel = new Label();
            progressLabel.Text = "Report Progress:";
            progressLabel.Font = new Font("Arial", 10, FontStyle.Regular);
            progressLabel.AutoSize = true;
            progressLabel.Location = new Point(50, 320);
            this.Controls.Add(progressLabel);

            ProgressBar progressBar = new ProgressBar();
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(500, 20);
            progressBar.Location = new Point(150, 320);
            progressBar.Minimum = 0;
            progressBar.Maximum = 4;
            progressBar.Value = 0;
            this.Controls.Add(progressBar);

            // Engagement Feature - Encouraging Messages
            Label messageLabel = new Label();
            messageLabel.Name = "messageLabel";
            messageLabel.Text = encouragingMessages[0];
            messageLabel.Font = new Font("Arial", 10, FontStyle.Italic);
            messageLabel.AutoSize = true;
            messageLabel.Location = new Point(150, 350);
            messageLabel.ForeColor = Color.Green;
            this.Controls.Add(messageLabel);

            // Submit Button
            Button submitButton = new Button();
            submitButton.Text = "Submit Report";
            submitButton.Size = new Size(120, 35);
            submitButton.Location = new Point(280, 390);
            submitButton.BackColor = Color.ForestGreen;
            submitButton.ForeColor = Color.White;
            submitButton.Font = new Font("Arial", 10, FontStyle.Bold);
            submitButton.Click += SubmitButton_Click;
            this.Controls.Add(submitButton);

            // Back to Main Menu Button
            Button backButton = new Button();
            backButton.Text = "Back to Main Menu";
            backButton.Size = new Size(120, 35);
            backButton.Location = new Point(50, 390);
            backButton.BackColor = Color.SteelBlue;
            backButton.ForeColor = Color.White;
            backButton.Font = new Font("Arial", 9, FontStyle.Regular);
            backButton.Click += (s, e) => this.Close();
            this.Controls.Add(backButton);
        }

        private void AttachButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Document Files|*.pdf;*.doc;*.docx|All Files|*.*";
            openFileDialog.Title = "Select a file to attach";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachedFilePath = openFileDialog.FileName;
                var statusLabel = this.Controls.Find("attachmentStatusLabel", true)[0] as Label;
                statusLabel.Text = Path.GetFileName(attachedFilePath);
                statusLabel.ForeColor = Color.Blue;

                // Update progress
                UpdateProgress();
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            var locationTextBox = this.Controls.Find("locationTextBox", true)[0] as TextBox;
            var categoryComboBox = this.Controls.Find("categoryComboBox", true)[0] as ComboBox;
            var descriptionRichTextBox = this.Controls.Find("descriptionRichTextBox", true)[0] as RichTextBox;

            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                MessageBox.Show("Please enter a location.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(descriptionRichTextBox.Text))
            {
                MessageBox.Show("Please enter a description of the issue.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create and store the issue
            ReportedIssue newIssue = new ReportedIssue
            {
                Location = locationTextBox.Text,
                Category = categoryComboBox.SelectedItem.ToString(),
                Description = descriptionRichTextBox.Text,
                AttachmentPath = attachedFilePath,
                ReportDate = DateTime.Now
            };

            // In a real application, you would save this to a database
            // For now, we'll just show a success message

            MessageBox.Show($"Issue reported successfully!\n\nLocation: {newIssue.Location}\nCategory: {newIssue.Category}\nThank you for your contribution!",
                          "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void UpdateProgress()
        {
            progressStep++;
            var progressBar = this.Controls.Find("progressBar", true)[0] as ProgressBar;
            var messageLabel = this.Controls.Find("messageLabel", true)[0] as Label;

            if (progressStep <= progressBar.Maximum)
            {
                progressBar.Value = progressStep;
                messageLabel.Text = encouragingMessages[Math.Min(progressStep, encouragingMessages.Length - 1)];
            }
        }
    }
}