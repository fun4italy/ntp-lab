using System;
using System.Windows.Forms;

using Model;

namespace View
{
    public partial class AddForm : Form
    {
        private readonly Random _rnd = new Random();
        private readonly string _transport;

        private string _oldConsumption = string.Empty;
        private string _oldSpeed = string.Empty;
        private string _oldDistance = string.Empty;

        private object[] _row;

        public object[] Result => _row;

        public AddForm(string transport)
        {
            InitializeComponent();

            _transport = transport;
            groupBox1.Text = _transport;

            if (_transport == "Вертолет")
            {
                label2.Visible = true;
                textBox2.Visible = true;
            }
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
#if DEBUG
            button3.Visible = true;
#else
            button3.Visible = false;
#endif
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox3.Text == string.Empty ||
                (_transport == "Вертолет" && textBox2.Text == string.Empty))
            {
                MessageBox.Show("Введите все значения", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                double consumption = double.Parse(textBox1.Text);
                double distance = double.Parse(textBox3.Text);
                double speed = _transport == "Вертолет" ? double.Parse(textBox2.Text) : 0.0;

                if (_transport == "Машина")
                {
                    Vehicle vehicle = new Vehicle(consumption);
                    _row = new object[]
                    {
                        _transport,
                        consumption.ToString(),
                        distance.ToString(),
                        null,
                        vehicle.FuelByDistance(distance).ToString("F2")
                    };
                }
                else if (_transport == "Машина-гибрид")
                {
                    Hybrid hybrid = new Hybrid(consumption);
                    _row = new object[]
                    {
                        _transport,
                        consumption.ToString(),
                        distance.ToString(),
                        null,
                        hybrid.FuelByDistance(distance).ToString("F2")
                    };
                }
                else if (_transport == "Вертолет")
                {
                    Helicopter helicopter = new Helicopter(consumption, speed);
                    _row = new object[]
                    {
                        _transport,
                        consumption.ToString(),
                        distance.ToString(),
                        speed.ToString(),
                        helicopter.FuelByDistance(distance).ToString("F2")
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }

        private void randomButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = _rnd.Next(5, 50).ToString();
            textBox2.Text = _rnd.Next(50, 250).ToString();
            textBox3.Text = _rnd.Next(100, 1000).ToString();
        }

        private void consumptionBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && !double.TryParse(textBox1.Text, out _))
            {
                textBox1.Text = _oldConsumption;
                return;
            }

            _oldConsumption = textBox1.Text;
        }

        private void speedBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty && !double.TryParse(textBox2.Text, out _))
            {
                textBox2.Text = _oldSpeed;
                return;
            }

            _oldSpeed = textBox2.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
