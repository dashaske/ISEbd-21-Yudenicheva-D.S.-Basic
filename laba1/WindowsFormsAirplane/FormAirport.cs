using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAirplane
{
    public partial class FormAirport : Form
    {
        private readonly AirportCollection airportCollection;

        public FormAirport()
        {
            InitializeComponent();
            airportCollection = new AirportCollection(pictureBoxAirport.Width, pictureBoxAirport.Height);
            Draw();
        }

        //Заполнение listBoxLevels
        private void ReloadLevels()
        {
            int index = listBoxAirport.SelectedIndex;
            listBoxAirport.Items.Clear();
            for (int i = 0; i < airportCollection.Keys.Count; i++)
            {
                listBoxAirport.Items.Add(airportCollection.Keys[i]);
            }
            if (listBoxAirport.Items.Count > 0 && (index == -1 || index >=
            listBoxAirport.Items.Count))
            {
                listBoxAirport.SelectedIndex = 0;
            }
            else if (listBoxAirport.Items.Count > 0 && index > -1 && index <
            listBoxAirport.Items.Count)
            {
                listBoxAirport.SelectedIndex = index;
            }
        }

        // Метод отрисовки парковки
        private void Draw()
        {
            if (listBoxAirport.SelectedIndex > -1)
            {
                Bitmap bmp = new Bitmap(pictureBoxAirport.Width, pictureBoxAirport.Height);
                Graphics gr = Graphics.FromImage(bmp);
                airportCollection[listBoxAirport.SelectedItem.ToString()].Draw(gr);
                pictureBoxAirport.Image = bmp;
            }
        }

        //Обработка нажатия кнопки "Добавить парковку"
        private void buttonAddAirport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNewLevelName.Text))
            {
                MessageBox.Show("Введите название аэропорта!", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            airportCollection.AddAirport(textBoxNewLevelName.Text);
            textBoxNewLevelName.Text = "";
            ReloadLevels();
        }

        //Обработка нажатия кнопки "Удалить парковку"
        private void buttonDelAirport_Click(object sender, EventArgs e)
        {
            if (listBoxAirport.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить аэропорт { listBoxAirport.SelectedItem.ToString()}?", "Удаление", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    airportCollection.DelAirport(listBoxAirport.Text);
                    ReloadLevels();
                }
            }
        }
        //Обработка кнопки "Забрать"
        private void buttonTakeBomber_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxPlace.Text != "")
            {
                var air = airportCollection[listBoxAirport.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBoxPlace.Text);
                if (air != null)
                {
                    FormPlane form = new FormPlane();
                    form.SetPlane(air);
                    form.ShowDialog();
                }
                Draw();
            }
        }
        //Метод обработки выбора элемента на listBoxLevels
        private void listBoxAirport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }
        // Обработка нажатия кнопки "Добавить самолет"
        private void buttonAddPlane_Click(object sender, EventArgs e)
        {
            FormAirplaneConfig formWaterTransportConfig = new FormAirplaneConfig();
            formWaterTransportConfig.AddEvent(AddPlane);
            formWaterTransportConfig.ShowDialog();
        }
        /// Метод добавления самолета
        private void AddPlane(Plane plane)
        {
            if (plane != null && listBoxAirport.SelectedIndex > -1)
            {
                if ((airportCollection[listBoxAirport.SelectedItem.ToString()]) + plane)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Самолет не удалось посадить");
                }
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (airportCollection.SaveData(saveFileDialog.FileName))
                {
                    MessageBox.Show("Сохранение прошло успешно =)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не сохранено =(", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (airportCollection.LoadData(openFileDialog.FileName))
                {
                    MessageBox.Show("Загружено =)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadLevels();
                    Draw();
                }
                else
                {
                    MessageBox.Show("Не загружено, повторите попытку снова!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
