using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace WindowsFormsAirplane
{
    public partial class FormAirport : Form
    {
        private readonly AirportCollection airportCollection;

        private readonly Logger logger;

        public FormAirport()
        {
            InitializeComponent();
            airportCollection = new AirportCollection(pictureBoxAirport.Width, pictureBoxAirport.Height);
            logger = LogManager.GetCurrentClassLogger();
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
            logger.Info($"Добавили аэропорт {textBoxNewLevelName.Text}");
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
                    logger.Info($"Удалили аэропорт {listBoxAirport.SelectedItem.ToString()}");
                    ReloadLevels();
                }
            }
        }
        //Обработка кнопки "Забрать"
        private void buttonTakeBomber_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxPlace.Text != "")
            {
                try
                {
                    var air = airportCollection[listBoxAirport.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBoxPlace.Text);
                    if (air != null)
                    {
                        FormPlane form = new FormPlane();
                        form.SetPlane(air);
                        form.ShowDialog();
                    }
                    logger.Info($"Забрали самолет {air} с места {maskedTextBoxPlace.Text}");
                    Draw();
                }
                catch (AirportNotFoundException ex)
                {
                    logger.Warn("Вызвана ошибка AirportNotFoundException");
                    MessageBox.Show(ex.Message, "Не найдено", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Warn("Вызвана неизвестная ошибка при удалении самолета");
                    MessageBox.Show(ex.Message, "Неизвестная ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //Метод обработки выбора элемента на listBoxLevels
        private void listBoxAirport_SelectedIndexChanged(object sender, EventArgs e)
        {
            logger.Info($"Перешли на аэропорт {listBoxAirport.SelectedItem.ToString()}");
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
                try
                {
                    if ((airportCollection[listBoxAirport.SelectedItem.ToString()]) + plane)
                    {
                        Draw();
                        logger.Info($"Самолет {plane} добавлен!");
                    }
                    else
                    {
                        MessageBox.Show("Самолет не удалось посадить");
                    }
                    Draw();
                }
                catch (AirportOverflowException ex)
                {
                    logger.Warn("Вызвано исключение переполнения аэропорта!");
                    MessageBox.Show(ex.Message, "Переполнение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (AirportAlreadyHaveException ex)
                {
                    MessageBox.Show(ex.Message, "Дублирование", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Warn("Вызвана неизвестная ошибка!");
                    MessageBox.Show(ex.Message, "Неизвестная ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    airportCollection.SaveData(saveFileDialog.FileName);
                    MessageBox.Show("Сохранение прошло успешно", "Результат",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    logger.Info("Сохранено в файл " + saveFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    logger.Warn("Вызвана неизвестная ошибка при сохранении");
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при сохранении!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    airportCollection.LoadData(openFileDialog.FileName);
                    MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    logger.Info("Загружено из файла " + openFileDialog.FileName);
                    ReloadLevels();
                    Draw();
                }
                catch (AirportOccupiedPlaceException ex)
                {
                    logger.Warn("Вызвана ошибка AirportOccupiedPlaceException");
                    MessageBox.Show(ex.Message, "Занятое место", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException ex)
                {
                    logger.Warn(ex.Message);
                    MessageBox.Show(ex.Message, "Ошибка при загрузке!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    logger.Warn("Вызвана неизвестная ошибка при загрузке");
                    MessageBox.Show(ex.Message, "Неизвестная ошибка при загрузке",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (listBoxAirport.SelectedIndex > -1)
            {
                airportCollection[listBoxAirport.SelectedItem.ToString()].Sort();
                Draw();
                logger.Info("Сортировка уровней");
            }
        }
    }
}
