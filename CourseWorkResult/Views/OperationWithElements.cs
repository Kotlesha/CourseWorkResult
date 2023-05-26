using CourseWorkResult.Controllers;
using CourseWorkResult.Controllers.Validation;
using CourseWorkResult.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseWorkResult.Views
{
    public partial class OperationWithElements : Form
    {
        public OperationWithElements()
        {
            InitializeComponent();
        }

        public OperationWithElements(Laminate laminate) : this()
        {
            name.Text = laminate.Name;
            manufacture.Text = laminate.Manufacture;
            length.Value = laminate.Length;
            width.Value = laminate.Width;
            price.Value = laminate.Price;
            packageCount.Value = laminate.Amount;
            name.Enabled = false;
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            var textBoxes = new List<TextBox>() { name, manufacture };

            foreach (var textbox in textBoxes)
            {
                if (!ValidateData.CheckString(textbox.Text.Trim(), out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textbox.Clear();
                    textbox.Focus();
                    return;
                }
            }

            Laminate laminate = new Laminate(name.Text, manufacture.Text, (int)length.Value, (int)width.Value, (int)price.Value, (int)packageCount.Value);

            if (!name.Enabled)
            {
                Update(laminate);
                name.Enabled = true;
                return;
            }

            Add(laminate);
        }

        private void Update(Laminate laminate)
        {
            string response = ClientsRequests.UpdateLaminate(laminate.Name, laminate);
            MessageBox.Show(response, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
        }

        private void Add(Laminate laminate)
        {
            string response = ClientsRequests.AddLaminate(laminate);
            MessageBox.Show(response, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (response.Equals("Ламинат добавлен успешно!"))
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                name.Clear();
                name.Focus();
            }
        }
    }
}
