using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceStatusForm : Form
    {
        // Instantiate our Advanced Data Structures
        private RequestBST requestTree = new RequestBST();
        private RequestMinHeap priorityHeap = new RequestMinHeap();
        private WorkflowGraph workflowGraph = new WorkflowGraph();

        private DataGridView gridRequests;
        private TextBox txtSearch;
        private ComboBox cmbUpdateStatus;

        public ServiceStatusForm()
        {
            SetupServiceForm();
            LoadSampleRequests();
            RefreshDataGrid();
        }

        private void LoadSampleRequests()
        {
            AddNewRequest(new ServiceRequest("REQ-001", "Water leak on Main St", "Reported", 1));
            AddNewRequest(new ServiceRequest("REQ-002", "Pothole on 5th Ave", "Assigned", 2));
            AddNewRequest(new ServiceRequest("REQ-003", "Broken streetlight", "In Progress", 3));
            AddNewRequest(new ServiceRequest("REQ-004", "Sewage overflow", "Reported", 1));
            AddNewRequest(new ServiceRequest("REQ-005", "Graffiti on park wall", "Reported", 3));
        }

        private void AddNewRequest(ServiceRequest req)
        {
            requestTree.Insert(req);      // Store in BST
            priorityHeap.Insert(req);     // Store in Heap
        }

        private void SetupServiceForm()
        {
            this.Text = "Service Request Status Tracker";
            this.Size = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Label title = new Label
            {
                Text = "Track Service Requests (Priority Sorted via Min-Heap)",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(20, 15),
                AutoSize = true
            };
            this.Controls.Add(title);

            Label lblSearch = new Label { Text = "Enter Request ID (BST Search):", Location = new Point(20, 55), AutoSize = true };
            this.Controls.Add(lblSearch);

            txtSearch = new TextBox { Location = new Point(220, 52), Width = 150 };
            this.Controls.Add(txtSearch);

            Button btnSearch = new Button { Text = "Search", Location = new Point(380, 50), Width = 80, BackColor = Color.SteelBlue, ForeColor = Color.White };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            Button btnRefresh = new Button { Text = "Show All (Priority Order)", Location = new Point(470, 50), Width = 180, BackColor = Color.SteelBlue, ForeColor = Color.White };
            btnRefresh.Click += (s, e) => RefreshDataGrid();
            this.Controls.Add(btnRefresh);

            gridRequests = new DataGridView
            {
                Location = new Point(20, 90),
                Size = new Size(790, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            this.Controls.Add(gridRequests);

            Label lblUpdate = new Label { Text = "Update Status to (Graph Traversal):", Location = new Point(20, 410), AutoSize = true };
            this.Controls.Add(lblUpdate);

            cmbUpdateStatus = new ComboBox { Location = new Point(240, 407), Width = 150 };
            cmbUpdateStatus.Items.AddRange(new string[] { "Reported", "Assigned", "In Progress", "Resolved" });
            cmbUpdateStatus.SelectedIndex = 0;
            this.Controls.Add(cmbUpdateStatus);

            Button btnUpdate = new Button { Text = "Update Selected", Location = new Point(400, 405), Width = 120, BackColor = Color.ForestGreen, ForeColor = Color.White };
            btnUpdate.Click += BtnUpdate_Click;
            this.Controls.Add(btnUpdate);

            Button btnClose = new Button { Text = "Back to Main Menu", Location = new Point(670, 405), Width = 140, BackColor = Color.Gray, ForeColor = Color.White };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);
        }

        private void RefreshDataGrid()
        {
            gridRequests.Rows.Clear();
            gridRequests.Columns.Clear();
            gridRequests.Columns.Add("ID", "Request ID");
            gridRequests.Columns.Add("Desc", "Description");
            gridRequests.Columns.Add("Status", "Status");
            gridRequests.Columns.Add("Priority", "Priority");
            gridRequests.Columns.Add("Path", "Workflow Path");

            // Fetch from Heap to ensure highest priority (1) is at the top
            var sortedRequests = priorityHeap.GetSortedList();

            foreach (var req in sortedRequests)
            {
                string path = workflowGraph.GetProgressPath(req.Status);
                gridRequests.Rows.Add(req.RequestID, req.Description, req.Status, req.Priority, path);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var req = requestTree.Search(txtSearch.Text.Trim().ToUpper());
            if (req != null)
            {
                gridRequests.Rows.Clear();
                gridRequests.Rows.Add(req.RequestID, req.Description, req.Status, req.Priority, workflowGraph.GetProgressPath(req.Status));
            }
            else
            {
                MessageBox.Show("Request ID not found.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (gridRequests.SelectedRows.Count > 0)
            {
                string id = gridRequests.SelectedRows[0].Cells["ID"].Value.ToString();
                var req = requestTree.Search(id);

                if (req != null)
                {
                    req.Status = cmbUpdateStatus.SelectedItem.ToString();
                    MessageBox.Show($"Request {id} status updated to {req.Status}.\nNext steps: {workflowGraph.GetProgressPath(req.Status)}",
                                    "Status Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select a request from the grid first.");
            }
        }
    }
}