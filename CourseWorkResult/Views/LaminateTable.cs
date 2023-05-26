using CourseWorkResult.Controllers;
using CourseWorkResult.Models;
using Equin.ApplicationFramework;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CourseWorkResult.Views
{
    public partial class LaminateTable : Form
    {
        public LaminateTable()
        {
            InitializeComponent();
            var data = new BindingListView<Laminate>(ClientsRequests.GetAllLaminates().ToList());
            Table.DataSource = data;
        }

        private DialogResult OpenForm(Form form)
        {
            Enabled = false;
            var result = form.ShowDialog();
            Enabled = true;
            return result;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var result = OpenForm(new OperationWithElements());

            if (result == DialogResult.OK)
            {
                Table.DataSource = new BindingListView<Laminate>(ClientsRequests.GetAllLaminates().ToList());

                if (!string.IsNullOrEmpty(Search.Text) || !string.IsNullOrWhiteSpace(Search.Text))
                {
                    Search_TextChanged(sender, e);
                    return;
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ObjectView<Laminate> laminateData = Table.SelectedRows[0].DataBoundItem as ObjectView<Laminate>;
            string response = ClientsRequests.RemoveLaminate(laminateData.Object.Name);
            MessageBox.Show(response, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var data = new BindingListView<Laminate>(ClientsRequests.GetAllLaminates()?.ToList());
            Table.DataSource = data;

            if (!string.IsNullOrEmpty(Search.Text) || !string.IsNullOrWhiteSpace(Search.Text))
            {
                Search_TextChanged(sender, e);
                return;
            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            ObjectView<Laminate> laminateData = Table.SelectedRows[0].DataBoundItem as ObjectView<Laminate>;
            var result = OpenForm(new OperationWithElements(laminateData.Object));

            if (result == DialogResult.OK)
            {
                var data = new BindingListView<Laminate>(ClientsRequests.GetAllLaminates()?.ToList());
                Table.DataSource = data;

                if (!string.IsNullOrEmpty(Search.Text) || !string.IsNullOrWhiteSpace(Search.Text))
                {
                    Search_TextChanged(sender, e);
                    return;
                }
            }
        }

        private void Choose_Click(object sender, EventArgs e)
        {
            ObjectView<Laminate> laminateData = Table.SelectedRows[0].DataBoundItem as ObjectView<Laminate>;
            OpenForm(new LaminateCalculation(laminateData.Object));
        }

        private void Table_DataSourceChanged(object sender, EventArgs e)
        {
            var count = Table.Rows.Count;

            foreach (Control control in Controls)
            {
                if (control is Button && control.Name != "Add")
                {
                    control.Enabled = count != 0;
                }
            }
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            var data = Table.DataSource as BindingListView<Laminate>;
            data.ApplyFilter(delegate (Laminate laminate) { return laminate.Manufacture.ToLower().Contains(Search.Text.Trim().ToLower()); });
            Table_DataSourceChanged(sender, e);
        }

        private void LaminateTable_FormClosed(object sender, FormClosedEventArgs e) => ClientsRequests.EndConnection();
    }
}
