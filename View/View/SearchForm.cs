using System;
using System.Windows.Forms;

namespace View
{
    public partial class SearchForm : Form
    {
        private string _oldConsumption = string.Empty;
        private string _oldDistance = string.Empty;
        private string _oldTotal = string.Empty;

        private readonly DataGridView _dataTable;

        public SearchForm(DataGridView DataTable)
        {
            InitializeComponent();

            _dataTable = DataTable;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty &&
                textBox3.Text == string.Empty &&
                textBox2.Text == string.Empty)
            {
                MessageBox.Show("Введите хотя бы одно значение", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            dataGridView1.Rows.Clear();

            double consumption = 0.0;
            double distance = 0.0;
            double totalConsumption = 0.0;

            if (textBox2.Text != string.Empty)
            {
                consumption = double.Parse(textBox2.Text);
            }

            if (textBox3.Text != string.Empty)
            {
                distance = double.Parse(textBox3.Text);
            }

            if (textBox4.Text != string.Empty)
            {
                totalConsumption = double.Parse(textBox4.Text);
            }

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                var c = _dataTable.Rows[i];

                if ((consumption == 0.0 || Convert.ToDouble(c.Cells[1].Value) == consumption) &&
                    (distance == 0.0 || Convert.ToDouble(c.Cells[2].Value) == distance) &&
                    (totalConsumption == 0.0 || Convert.ToDouble(c.Cells[4].Value) == totalConsumption))
                {
                    DataGridViewRow newRow = new DataGridViewRow();

                    foreach (DataGridViewCell cell in c.Cells)
                    {
                        newRow.Cells.Add(new DataGridViewTextBoxCell { Value = cell.Value });
                    }

                    dataGridView1.Rows.Add(newRow);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void consumptionBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty && !double.TryParse(textBox2.Text, out _))
            {
                textBox2.Text = _oldConsumption;
                return;
            }

            _oldConsumption = textBox2.Text;
        }

        private void distanceBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != string.Empty && !double.TryParse(textBox3.Text, out _))
            {
                textBox3.Text = _oldDistance;
                return;
            }

            _oldDistance = textBox3.Text;
        }

        private void totalBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text != string.Empty && !double.TryParse(textBox4.Text, out _))
            {
                textBox4.Text = _oldTotal;
                return;
            }

            _oldTotal = textBox4.Text;
        }
    }
}
