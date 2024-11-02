using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Linq;

namespace View
{
    public partial class ViewForm : Form
    {
        public ViewForm()
        {
            InitializeComponent();
        }

        private void ViewForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        // Кнопка добавить
        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(comboBox1.Text);
            addForm.ShowDialog();

            if (addForm.Result == null)
            {
                return;
            }

            dataGridView1.Rows.Add(addForm.Result);
        }

        // Кнопка удалить
        private void removeButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        // Кнопка поиск
        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchForm search = new SearchForm(dataGridView1);
            search.ShowDialog();
        }

        // Кнопка сохранить
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();

            if (fd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<string[]>));
            List<string[]> data = new List<string[]>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var c = row.Cells;
                data.Add(new[]
                {
                    c[0].Value.ToString(),
                    c[1].Value.ToString(),
                    c[2].Value.ToString(),
                    c[3].Value?.ToString(),
                });
            }

            using (var file = File.Create(fd.FileName))
            {
                writer.Serialize(file, data);
                file.Close();
            }
        }

        // Кнопка загрузить
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                Multiselect = false
            };

            if (fd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var reader = new System.Xml.Serialization.XmlSerializer(typeof(List<string[]>));
            List<string[]> data;

            try
            {
                using (var file = new StreamReader(fd.FileName))
                {
                    data = (List<string[]>)reader.Deserialize(file);
                    file.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный файл данных", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dataGridView1.Rows.Clear();

            foreach (object[] row in data)
            {
                try
                {
                    Transport transport;
                    double distance = double.Parse(row[2].ToString());

                    if (row.Length == 4 || !comboBox1.Items.Contains(row[0].ToString()))
                    {
                        double consumption = double.Parse(row[1].ToString());
                        double speed = row[3] != null ? double.Parse(row[3].ToString()) : 0.0;

                        if (row[0].ToString() == "Машина")
                        {
                            transport = new Vehicle(consumption);
                        }
                        else if (row[0].ToString() == "Машина-гибрид")
                        {
                            transport = new Hybrid(consumption);
                        }
                        else if (row[0].ToString() == "Вертолет")
                        {
                            transport = new Helicopter(consumption, speed);
                        }
                        else
                        {
                            // Not reachable
                            continue;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некорректные данные", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dataGridView1.Rows.Clear();
                        return;
                    }

                    var newRow = new object[row.Length + 1];

                    for (int i = 0; i < row.Length; i++)
                    {
                        newRow[i] = row[i];
                    }

                    newRow[row.Length] = transport.FuelByDistance(distance).ToString("F2");
                    dataGridView1.Rows.Add(newRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
