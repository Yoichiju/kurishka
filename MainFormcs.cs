using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WinFormsApp5
{
    public partial class MainFormcs : Form
    {
        private DataTable table = new DataTable();

        public MainFormcs()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupDataGridViewStyle();
            InitializeDataTable();
            SetupDataGridViewColumns();
            dtpDate.Value = DateTime.Now;
            txtProduct.Focus();
        }

        private void SetupDataGridViewStyle()
        {
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 138);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        private void InitializeDataTable()
        {
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Продукт", typeof(string));
            table.Columns.Add("Объем (м³)", typeof(double));
            table.Columns.Add("Дата реализации", typeof(DateTime));

            
        }

        private void AddTestData()
        {
            AddRow("Дизельное топливо", 125.5, new DateTime(2026, 1, 15));
            AddRow("Бензин АИ-92", 89.3, new DateTime(2026, 1, 16));
            AddRow("Мазут", 450.0, new DateTime(2026, 1, 17));
        }

        private void SetupDataGridViewColumns()
        {
            dataGridView1.DataSource = table;

            dataGridView1.Columns["ID"].HeaderText = "№";
            dataGridView1.Columns["ID"].Width = 50;
            dataGridView1.Columns["Продукт"].HeaderText = "Наименование нефтепродукта";
            dataGridView1.Columns["Продукт"].Width = 200;
            dataGridView1.Columns["Объем (м³)"].HeaderText = "Объем, м³";
            dataGridView1.Columns["Объем (м³)"].Width = 100;
            dataGridView1.Columns["Дата реализации"].HeaderText = "Дата реализации";
            dataGridView1.Columns["Дата реализации"].Width = 120;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    AddRow(txtProduct.Text, double.Parse(txtVolume.Text), dtpDate.Value);
                    ClearInputs();
                    MessageBox.Show("Реализация добавлена успешно!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Объем должен быть числом!", "Ошибка ввода",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Удалить выбранную реализацию?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = dataGridView1.SelectedRows[0].Index;
                    table.Rows.RemoveAt(rowIndex);
                    UpdateIds();
                    MessageBox.Show("Реализация удалена!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProduct.Text))
            {
                MessageBox.Show("Введите наименование нефтепродукта!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProduct.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtVolume.Text) || !double.TryParse(txtVolume.Text, out double volume) || volume <= 0)
            {
                MessageBox.Show("Введите корректный объем (число > 0)!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVolume.Focus();
                return false;
            }

            return true;
        }

        private void AddRow(string product, double volume, DateTime date)
        {
            var newRow = table.NewRow();
            newRow["ID"] = table.Rows.Count + 1;
            newRow["Продукт"] = product;
            newRow["Объем (м³)"] = volume;
            newRow["Дата реализации"] = date;
            table.Rows.Add(newRow);
        }

        private void ClearInputs()
        {
            txtProduct.Clear();
            txtVolume.Clear();
            dtpDate.Value = DateTime.Now;
            txtProduct.Focus();
        }

        private void UpdateIds()
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["ID"] = i + 1;
            }
        }
    }
}
