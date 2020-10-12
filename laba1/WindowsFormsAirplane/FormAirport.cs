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
        Airport<ITransport> airport;

        public FormAirport()
        {
            InitializeComponent();
            airport = new Airport<ITransport>(20, pictureBoxAirport.Width, pictureBoxAirport.Height);
            Draw();
        }
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxAirport.Width, pictureBoxAirport.Height);
            Graphics gr = Graphics.FromImage(bmp);
            airport.Draw(gr);
            pictureBoxAirport.Image = bmp;
        }
        private void buttonSetWarPlane_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var air = new WarPlane(100, 1000, dialog.Color, dopColor: Color.Gray);
                int place = airport + air;
                Draw();
            }
        }
        private void buttonSetBomber_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ColorDialog dialogDop = new ColorDialog();
                if (dialogDop.ShowDialog() == DialogResult.OK)
                {
                    var air = new Bomber(100, 1000, dialog.Color, dialogDop.Color, true, true, true);
                    int place = airport + air;
                    Draw();
                }
            }
        }
        private void buttonTakeBomber_Click(object sender, EventArgs e)
        {
            if (maskedTextBoxPlace.Text != "")
            {
                var air = airport - Convert.ToInt32(maskedTextBoxPlace.Text);
                if (air != null)
                {
                    FormPlane form = new FormPlane();
                    form.SetPlane(air);
                    form.ShowDialog();
                }
                Draw();
            }
        }            
    }
}
