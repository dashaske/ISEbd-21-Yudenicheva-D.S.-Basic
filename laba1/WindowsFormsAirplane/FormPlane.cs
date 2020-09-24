﻿using System;
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
    public partial class FormPlane : Form
    {
        private Bomber plane;
        public FormPlane()
        {
            InitializeComponent();
        }

        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxAirplane.Width, pictureBoxAirplane.Height);
            Graphics gr = Graphics.FromImage(bmp);
            plane.DrawAirplane(gr);
            pictureBoxAirplane.Image = bmp;
        }

       
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            plane = new Bomber(rnd.Next(100, 300), rnd.Next(1000, 2000), Color.Gray,
           Color.Black, true, true, true);
            plane.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxAirplane.Width,
           pictureBoxAirplane.Height);
            Draw();
        }


        private void buttonMove_Click(object sender, EventArgs e)
        {
            //получаем имя кнопки
            string name = (sender as Button).Name;
            switch (name)
            {
                case "buttonUp":
                    plane.MovePlane(Direction.Up);
                    break;
                case "buttonDown":
                    plane.MovePlane(Direction.Down);
                    break;
                case "buttonLeft":
                    plane.MovePlane(Direction.Left);
                    break;
                case "buttonRight":
                    plane.MovePlane(Direction.Right);
                    break;
            }
            Draw();
        }
    }
}
