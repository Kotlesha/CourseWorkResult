using CourseWorkResult.Controllers;
using CourseWorkResult.Models;
using Spire.Doc;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWorkResult.Views
{
    public partial class LaminateCalculation : Form
    {
        private static Laminate Laminate;
        private static CalculationData CalculationData;
        private static CalculationResult CalculationResult;

        public LaminateCalculation(Laminate laminate)
        {
            InitializeComponent();

            lengthLaminate.Value = laminate.Length;
            widthLaminate.Value = laminate.Width;
            packageCount.Value = laminate.Amount;
            price.Value = laminate.Price;
            Laminate = laminate;
        }

        private void Count_Click(object sender, EventArgs e)
        {
            if (minLength.Value > (int)Math.Round(lengthLaminate.Value / 2, MidpointRounding.AwayFromZero))
            {
                minLength.Value = Math.Round(lengthLaminate.Value / 2, MidpointRounding.AwayFromZero);
            }

            CalculationData calculationData = new CalculationData(Laminate, lengthRoom.Value, widthRoom.Value, (int)indent.Value, (int)minLength.Value);
            CalculationResult calculationResult = Calculation.Calculate(calculationData);
            CalculationData = calculationData;
            CalculationResult = calculationResult;

            Amount.Text = calculationResult.Amount.ToString();
            Square.Text = calculationResult.Square.ToString() + " м2";
            CountOfPackages.Text = calculationResult.CountOfPackages.ToString();
            ResultPrice.Text = calculationResult.ResultPrice.ToString();
            Leftover.Text = calculationResult.Leftover.ToString();
            leftoverSquare.Text = calculationResult.LeftoverSquare.ToString() + " м2";
            SaveData.Enabled = true;
        }

        private void SaveData_Click(object sender, EventArgs e)
        {
            var document = new Document();
            var section = document.AddSection();
            var header = section.HeadersFooters.Header;
            var headerParagraph = header.AddParagraph();
            headerParagraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
            var headerText = headerParagraph.AppendText("Калькулятор ламината\n");
            headerText.CharacterFormat.FontName = "Times New Roman";
            headerText.CharacterFormat.FontSize = 12;
            headerText.CharacterFormat.Bold = true;
            headerText = headerParagraph.AppendText("Результаты расчёта:");
            headerText.CharacterFormat.FontName = "Times New Roman";
            headerText.CharacterFormat.FontSize = 15;
            var paragraph = section.AddParagraph();
            var textRange = paragraph.AppendText(string.Concat(CalculationData.ToString(), CalculationResult.ToString()));
            textRange.CharacterFormat.FontSize = 14;
            paragraph.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Left;
            
            saveFileDialog1.Filter = "Word Document|*.docx";
            saveFileDialog1.FileName = "Отчёт.docx";
            saveFileDialog1.Title = "Сохранение отчёта";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                document.SaveToFile(saveFileDialog1.FileName, FileFormat.Docx);
            }
        }
    }
}
