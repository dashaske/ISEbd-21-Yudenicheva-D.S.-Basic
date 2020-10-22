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

        //Обработка нажатия кнопки "Посадить военный самолет"
        private void buttonSetWarPlane_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var air = new WarPlane(100, 1000, dialog.Color, dopColor: Color.Gray);
                if (airportCollection[listBoxAirport.SelectedItem.ToString()] + air)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Не удалось посадить военный самолет");
                }
            }
        }

        //Обработка нажатия кнопки "Посадить бомбардировщик"
        private void buttonSetBomber_Click(object sender, EventArgs e)
        {
            if (listBoxAirport.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var bomber = new Bomber(100, 1000, dialog.Color, dialogDop.Color,
                        true, true, true);
                        if (airportCollection[listBoxAirport.SelectedItem.ToString()] + bomber)
                        {
                            Draw();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось посадить бомбардировщик");
                        }
                    }
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
    }
}
