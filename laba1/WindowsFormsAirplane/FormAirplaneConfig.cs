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
    public partial class FormAirplaneConfig : Form
    {
        //Переменная - выбранный самолет
        ITransport plane = null;

        //Событие
        private event Action<Plane> eventAddPlane;
        public FormAirplaneConfig()
        {
            InitializeComponent();
            panelRed.MouseDown += panelColor_MouseDown;
            panelGray.MouseDown += panelColor_MouseDown;
            panelYellow.MouseDown += panelColor_MouseDown;
            panelOrange.MouseDown += panelColor_MouseDown;
            panelBlack.MouseDown += panelColor_MouseDown;
            panelGreen.MouseDown += panelColor_MouseDown;
            panelWhite.MouseDown += panelColor_MouseDown;
            panelBlue.MouseDown += panelColor_MouseDown;
            buttonCancel.Click += (object sender, EventArgs e) => { Close(); };
        }

        private void DrawPlane()
        {
            if (plane != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxPlane.Width, pictureBoxPlane.Height);
                Graphics gr = Graphics.FromImage(bmp);
                plane.SetPosition(5, 5, pictureBoxPlane.Width, pictureBoxPlane.Height);
                plane.DrawFly(gr);
                pictureBoxPlane.Image = bmp;
            }
        }

        //Добавления события
        public void AddEvent(Action<Plane> ev)
        {
            if (eventAddPlane == null)
            {
                eventAddPlane = new Action<Plane>(ev);
            }
            else
            {
                eventAddPlane += ev;
            }
        }


        // Передаем информацию при нажатии на Label
        private void labelWarPlane_MouseDown(object sender, MouseEventArgs e)
        {
            labelWarPlane.DoDragDrop(labelWarPlane.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }



        // Передаем информацию при нажатии на Label
        private void labelBomber_MouseDown(object sender, MouseEventArgs e)
        {
            labelBomber.DoDragDrop(labelBomber.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        // Проверка получаемой информации (ее типа на соответствие требуемому)
        private void panelPlane_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Действия при приеме перетаскиваемой информации (создается либо военный, либо бомбардировщик)
        private void panelPlane_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Военный самолет":
                    plane = new WarPlane((int)numericUpDownMaxSpeed.Value,
                        (int)numericUpDownWeight.Value, Color.White, Color.Black);
                    break;
                case "Бомбардировщик":
                    plane = new Bomber((int)numericUpDownMaxSpeed.Value,
                        (int)numericUpDownWeight.Value, Color.White, Color.Black,
                        checkBoxFastening.Checked, checkBoxGun.Checked, checkBoxStar.Checked);
                    break;
            }
            DrawPlane();
        }

        //Отправляем цвет с панели
        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            Control panelColor = (Control)sender;
            panelColor.DoDragDrop(panelColor.BackColor, DragDropEffects.Move | DragDropEffects.Copy);
        }

        // Проверка получаемой информации (ее типа на соответствие требуемому)  
        private void labelBaseColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        // Принимаем основной цвет
        private void labelBaseColor_DragDrop(object sender, DragEventArgs e)
        {
            if (plane != null)
            {
                plane.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawPlane();
            }
        }

        // Принимаем дополнительный цвет   
        private void labelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (plane is Bomber)
            {
                Bomber bomber = (Bomber)plane;
                bomber?.SetDopColor((Color)e.Data.GetData(typeof(Color)));
                DrawPlane();
            }
        }

        // Добавление самолета
        private void buttonOk_Click(object sender, EventArgs e)
        {
            eventAddPlane?.Invoke((Plane)plane);
            Close();
        }
    }
}
