using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            button6.ForeColor = Color.FromArgb(255, 23, 33, 43);
            button7.ForeColor = Color.White;
        }

        public List<float> NumberList = new List<float>();
        int Delay = 100, rmax, rmin, iterations = 0, passes = 0;
        Graphics g;
        float ColumWidth = 5f, ColumHeight = 300f;
        bool pause, unsortedArrayExist;
        string language = "English";
        private void button1_Click(object sender, EventArgs e)
        {
            if (inProcess == false)
            {
                int num, ellipsy = 20;
                float r, ellipsx = 25f;
                panel1.Refresh();
                g = panel1.CreateGraphics();
                Random random = new Random();

                if (textBox1.Text == "")
                {
                    textBox1.Text = "3";
                }

                num = Convert.ToInt32(textBox1.Text);
                rmin = Convert.ToInt32(textBox2.Text);
                rmax = Convert.ToInt32(textBox3.Text);
                if (rmax <= rmin)
                {
                    if (language == "English")
                    {
                        MessageBox.Show("The minimum number to generate an array must be less than the maximum", "Error");
                    }
                    else if (language == "Ukrainian")
                    {
                        MessageBox.Show("Мiнiмальне число згенерованого масиву має бути бiльше за максимальне", "Помилка");
                    }

                    return;
                }

                listView1.Items.Clear();
                NumberList.Clear();

                for (int i = 0; i < num; i++)
                {
                    if (i % 20 == 0 && i != 0)
                    {
                        ellipsx = 25;
                        ellipsy += 50;
                    }
                    r = random.Next(rmin, rmax + 1);
                    NumberList.Add(r);
                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (r - rmin) * (50) / (rmax - rmin))), 79, 137)), ellipsx - ((r - rmin) * 25 / (rmax - rmin)) , ColumHeight + ellipsy + (rmax - r) * 25 / (rmax - rmin), (r - rmin) * 50 / (rmax - rmin), (r - rmin) * 50 / (rmax - rmin));
                    ellipsx += 50;
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), i * ColumWidth, (rmax - r) * ColumHeight / (rmax - rmin), ColumWidth, (r - rmin) * ColumHeight / (rmax - rmin));
                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, (rmax - r) * ColumHeight / (rmax - rmin), 1, (r - rmin) * ColumHeight / (rmax - rmin));
                }
                foreach (int i in NumberList)
                {
                    listView1.Items.Add(Convert.ToString(i));
                }
                unsortedArrayExist = true;
            }
            
        }
        public bool inProcess = false;
        double Workingtime = 0;

        private async void button2_Click(object sender, EventArgs e)
        {
            pause = false;
            button5.Text = " | |";
            iterations = 0;
            passes = 0;
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\kuzmi\Desktop\Курсова\Cursova\Sounds\Beep.wav");
            if (inProcess == false)
            {
                if (unsortedArrayExist)
                {
                    if (language == "English")
                    {
                        button2.Text = "Stop sorting";
                    }
                    else if (language == "Ukrainian")
                    {
                        button2.Text = "Зупинити сортування";
                    }
                    Workingtime = 0;
                    inProcess = true;
                    comboBox1.Enabled = false;
                    timer1.Enabled = true;
                    if (comboBox1.Text == "Bubble sort")
                    {
                        label6.Visible = true;
                        while (true)
                        {
                            int ellipsy = 20;
                            int ellipsx = 0;
                            for (int i = 0; i < NumberList.Count - 1; i++)
                            {
                                if (NumberList[i] > NumberList[i + 1])
                                {
                                    //Stop check
                                    if (inProcess == false)
                                    {
                                        return;
                                    }
                                    float i0, i1;
                                    //Actually sorting algorithm
                                    i0 = NumberList[i];
                                    i1 = NumberList[i + 1];
                                    NumberList[i] = i1;
                                    NumberList[i + 1] = i0;
                                    //List visualisation
                                    listView1.Items[i].Text = Convert.ToString(i1);
                                    listView1.Items[i + 1].Text = Convert.ToString(i0);
                                    listView1.Items[i + 1].Focused = true;
                                    listView1.Items[i + 1].Selected = true;
                                    //Statistics label
                                    if (language == "English")
                                    {
                                        label6.Text = "Current number: " + i0 + ", current position: " + (i + 2);
                                        iterations++;
                                        label7.Text = "Number of iterations: " + iterations;
                                        passes++;
                                        label8.Text = "Number of passes: " + passes;
                                    }
                                    else if (language == "Ukrainian")
                                    {
                                        label6.Text = "Поточне число: " + i0 + ", поточна позицiя: " + (i + 2);
                                        iterations++;
                                        label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                        passes++;
                                        label8.Text = "Кiлькiсть проходжень: " + passes;
                                    }
                                    //Sound
                                    simpleSound.Play();
                                    //Colums
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, 0, ColumWidth, ColumHeight);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (i + 1) * ColumWidth, 0, ColumWidth, ColumHeight);

                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), 1, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), (i + 1) * ColumWidth, (rmax - NumberList[i + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (i + 1) * ColumWidth, (rmax - NumberList[i + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[i + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Change circles location on next row
                                    if (i + 1 >= 20 && i + 1 < 40)
                                    {
                                        ellipsy = 70;
                                        ellipsx = 1000;
                                    }
                                    else if (i + 1 >= 40 && i + 1 < 60)
                                    {
                                        ellipsy = 120;
                                        ellipsx = 2000;
                                    }
                                    else if (i + 1 >= 60 && i + 1 < 80)
                                    {
                                        ellipsy = 170;
                                        ellipsx = 3000;
                                    }
                                    else if (i + 1 >= 80 && i + 1 < 100)
                                    {
                                        ellipsy = 220;
                                        ellipsx = 4000;
                                    }
                                    else if (i + 1 >= 100 && i + 1 < 120)
                                    {
                                        ellipsy = 270;
                                        ellipsx = 5000;
                                    }
                                    else if (i + 1 >= 120 && i + 1 < 140)
                                    {
                                        ellipsy = 320;
                                        ellipsx = 6000;
                                    }
                                    else if (i + 1 >= 140 && i + 1 < 160)
                                    {
                                        ellipsy = 370;
                                        ellipsx = 7000;
                                    }
                                    else if (i + 1 >= 160 && i + 1 < 180)
                                    {
                                        ellipsy = 420;
                                        ellipsx = 8000;
                                    }
                                    else if (i + 1 >= 180 && i + 1 < 200)
                                    {
                                        ellipsy = 470;
                                        ellipsx = 9000;
                                    }
                                    //Circles
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (i * 50), ColumHeight + ellipsy, 50, 50);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + ((i + 1) * 50), ColumHeight + ellipsy, 50, 50);
                                    if (i != 0 && (i == 19 || i == 39 || i == 59 || i == 79 || i == 99 || i == 119 || i == 139 || i == 159 || i == 179 || i == 199))
                                    {
                                        g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + 1000 + (i * 50), ColumHeight + ellipsy - 50, 50, 50);
                                        g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[i] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 1000 + 25 + (i * 50) - ((NumberList[i] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy - 50 + (rmax - NumberList[i]) * 25 / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin));
                                    }

                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[i] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + (i * 50) - ((NumberList[i] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i]) * 25 / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + ((i + 1) * 50) - ((NumberList[i + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i + 1]) * 25 / (rmax - rmin), (NumberList[i + 1] - rmin) * (50) / (rmax - rmin), (NumberList[i + 1] - rmin) * (50) / (rmax - rmin));
                                    //Delay
                                    if (pause)
                                    {
                                        timer1.Enabled = false;
                                        while (true)
                                        {
                                            await Task.Delay(1);
                                            if (!pause)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        timer1.Enabled = true;
                                        await Task.Delay(Delay);
                                    }
                                    //Green colums back to normal
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (i + 1) * ColumWidth, (rmax - NumberList[i + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (i + 1) * ColumWidth, (rmax - NumberList[i + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[i + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Green circles back to normal
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[i + 1] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((i + 1) * 50) - ((NumberList[i + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i + 1]) * 25 / (rmax - rmin), (NumberList[i + 1] - rmin) * (50) / (rmax - rmin), (NumberList[i + 1] - rmin) * (50) / (rmax - rmin));
                                    //List back to normal
                                    listView1.Items[i + 1].Focused = false;
                                    listView1.Items[i + 1].Selected = false;
                                    //Is list sorted check
                                    if (IsSorted())
                                    {
                                        if (language == "English")
                                        {
                                            button2.Text = "Start sorting";
                                        }
                                        else if (language == "Ukrainian")
                                        {
                                            button2.Text = "Почати сортування";
                                        }
                                        label6.Visible = false;
                                        comboBox1.Enabled = true;
                                        inProcess = false;
                                        simpleSound.Stop();
                                        unsortedArrayExist = false;
                                        timer1.Enabled = false;
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else if (comboBox1.Text == "Shaker sort")
                    {
                        label6.Visible = true;
                        for (var i = 0; i < NumberList.Count / 2; i++)
                        {
                            var swapFlag = false;
                            //From left to right
                            for (var j = i; j < NumberList.Count - i - 1; j++)
                            {
                                int ellipsy = 20;
                                int ellipsx = 0;
                                if (NumberList[j] > NumberList[j + 1])
                                {
                                    //Stop check
                                    if (inProcess == false)
                                    {
                                        return;
                                    }
                                    //Actually sorting algorithm
                                    float i0 = NumberList[j];
                                    float i1 = NumberList[j + 1];
                                    NumberList[j] = i1;
                                    NumberList[j + 1] = i0;
                                    swapFlag = true;
                                    //List visualisation
                                    listView1.Items[j].Text = Convert.ToString(i1);
                                    listView1.Items[j + 1].Text = Convert.ToString(i0);
                                    listView1.Items[j + 1].Focused = true;
                                    listView1.Items[j + 1].Selected = true;
                                    //Statistics label
                                    if (language == "English")
                                    {
                                        label6.Text = "Current number: " + i0 + ", current position: " + (j + 2);
                                        iterations++;
                                        label7.Text = "Number of iterations: " + iterations;
                                        passes++;
                                        label8.Text = "Number of passes: " + passes;
                                    }
                                    else if (language == "Ukrainian")
                                    {
                                        label6.Text = "Поточне число: " + i0 + ", поточна позицiя: " + (j + 2);
                                        iterations++;
                                        label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                        passes++;
                                        label8.Text = "Кiлькiсть проходжень: " + passes;
                                    }
                                    //Sound
                                    simpleSound.Play();
                                    //Colums
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, 0, ColumWidth, ColumHeight);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j + 1) * ColumWidth, 0, ColumWidth, ColumHeight);

                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), (j + 1) * ColumWidth, (rmax - NumberList[j + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j + 1) * ColumWidth, (rmax - NumberList[j + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[j + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Change circles location on next row
                                    if (j + 1 >= 20 && j + 1 < 40)
                                    {
                                        ellipsy = 70;
                                        ellipsx = 1000;
                                    }
                                    else if (j + 1 >= 40 && j + 1 < 60)
                                    {
                                        ellipsy = 120;
                                        ellipsx = 2000;
                                    }
                                    else if (j + 1 >= 60 && j + 1 < 80)
                                    {
                                        ellipsy = 170;
                                        ellipsx = 3000;
                                    }
                                    else if (j + 1 >= 80 && j + 1 < 100)
                                    {
                                        ellipsy = 220;
                                        ellipsx = 4000;
                                    }
                                    else if (j + 1 >= 100 && j + 1 < 120)
                                    {
                                        ellipsy = 270;
                                        ellipsx = 5000;
                                    }
                                    else if (j + 1 >= 120 && j + 1 < 140)
                                    {
                                        ellipsy = 320;
                                        ellipsx = 6000;
                                    }
                                    else if (j + 1 >= 140 && j + 1 < 160)
                                    {
                                        ellipsy = 370;
                                        ellipsx = 7000;
                                    }
                                    else if (j + 1 >= 160 && j + 1 < 180)
                                    {
                                        ellipsy = 420;
                                        ellipsx = 8000;
                                    }
                                    else if (j + 1 >= 180 && j + 1 < 200)
                                    {
                                        ellipsy = 470;
                                        ellipsx = 9000;
                                    }
                                    //Circles
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (j * 50), ColumHeight + ellipsy, 50, 50);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + ((j + 1) * 50), ColumHeight + ellipsy, 50, 50);
                                    if (j != 0 && (j == 19 || j == 39 || j == 59 || j == 79 || j == 99 || j == 119 || j == 139 || j == 159 || j == 179 || j == 199))
                                    {
                                        g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + 1000 + (j * 50), ColumHeight + ellipsy - 50, 50, 50);
                                        g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 1000 + 25 + (j * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy - 50 + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    }

                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + (j * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + ((j + 1) * 50) - ((NumberList[j + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j + 1]) * 25 / (rmax - rmin), (NumberList[j + 1] - rmin) * (50) / (rmax - rmin), (NumberList[j + 1] - rmin) * (50) / (rmax - rmin));
                                    //Delay
                                    if (pause)
                                    {
                                        timer1.Enabled = false;
                                        while (true)
                                        {
                                            await Task.Delay(1);
                                            if (!pause)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        timer1.Enabled = true;
                                        await Task.Delay(Delay);
                                    }
                                    //Green colums back to normal
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (j + 1) * ColumWidth, (rmax - NumberList[j + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j + 1) * ColumWidth, (rmax - NumberList[j + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[j + 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Green circles back to normal
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j + 1] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((j + 1) * 50) - ((NumberList[j + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j + 1]) * 25 / (rmax - rmin), (NumberList[j + 1] - rmin) * (50) / (rmax - rmin), (NumberList[j + 1] - rmin) * (50) / (rmax - rmin));
                                    //List back to normal
                                    listView1.Items[j + 1].Focused = false;
                                    listView1.Items[j + 1].Selected = false;
                                }
                            }

                            //From right to left
                            for (var j = NumberList.Count - 2 - i; j > i; j--)
                            {
                                int ellipsy = 20;
                                int ellipsx = 0;
                                if (NumberList[j - 1] > NumberList[j])
                                {
                                    //Stop check
                                    if (inProcess == false)
                                    {
                                        return;
                                    }
                                    //Actually sorting algorithm
                                    float i0 = NumberList[j - 1];
                                    float i1 = NumberList[j];
                                    NumberList[j - 1] = i1;
                                    NumberList[j] = i0;
                                    swapFlag = true;
                                    //List visualisation
                                    listView1.Items[j - 1].Text = Convert.ToString(i1);
                                    listView1.Items[j].Text = Convert.ToString(i0);
                                    listView1.Items[j - 1].Focused = true;
                                    listView1.Items[j - 1].Selected = true;
                                    //Statistics label
                                    if (language == "English")
                                    {
                                        label6.Text = "Current number: " + i1 + ", current position: " + j;
                                        iterations++;
                                        label7.Text = "Number of iterations: " + iterations;
                                        passes++;
                                        label8.Text = "Number of passes: " + passes;
                                    }
                                    else if (language == "Ukrainian")
                                    {
                                        label6.Text = "Поточне число: " + i1 + ", поточна позицiя: " + j;
                                        iterations++;
                                        label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                        passes++;
                                        label8.Text = "Кiлькiсть проходжень: " + passes;
                                    }
                                    //Sound
                                    simpleSound.Play();
                                    //Colums
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - 1) * ColumWidth, 0, ColumWidth, ColumHeight);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, 0, ColumWidth, ColumHeight);

                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), (j - 1) * ColumWidth, (rmax - NumberList[j - 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j - 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - 1) * ColumWidth, (rmax - NumberList[j - 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[j - 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Change circles location on next row
                                    if (j + 1 >= 20 && j + 1 < 40)
                                    {
                                        ellipsy = 70;
                                        ellipsx = 1000;
                                    }
                                    else if (j + 1 >= 40 && j + 1 < 60)
                                    {
                                        ellipsy = 120;
                                        ellipsx = 2000;
                                    }
                                    else if (j + 1 >= 60 && j + 1 < 80)
                                    {
                                        ellipsy = 170;
                                        ellipsx = 3000;
                                    }
                                    else if (j + 1 >= 80 && j + 1 < 100)
                                    {
                                        ellipsy = 220;
                                        ellipsx = 4000;
                                    }
                                    else if (j + 1 >= 100 && j + 1 < 120)
                                    {
                                        ellipsy = 270;
                                        ellipsx = 5000;
                                    }
                                    else if (j + 1 >= 120 && j + 1 < 140)
                                    {
                                        ellipsy = 320;
                                        ellipsx = 6000;
                                    }
                                    else if (j + 1 >= 140 && j + 1 < 160)
                                    {
                                        ellipsy = 370;
                                        ellipsx = 7000;
                                    }
                                    else if (j + 1 >= 160 && j + 1 < 180)
                                    {
                                        ellipsy = 420;
                                        ellipsx = 8000;
                                    }
                                    else if (j + 1 >= 180 && j + 1 < 200)
                                    {
                                        ellipsy = 470;
                                        ellipsx = 9000;
                                    }
                                    //Circles
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + ((j - 1) * 50), ColumHeight + ellipsy, 50, 50);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (j * 50), ColumHeight + ellipsy, 50, 50);
                                    if (j != 0 && (j == 19 || j == 39 || j == 59 || j == 79 || j == 99 || j == 119 || j == 139 || j == 159 || j == 179 || j == 199))
                                    {
                                        g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + 1000 + (j * 50), ColumHeight + ellipsy - 50, 50, 50);
                                        g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 1000 + 25 + (j * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy - 50 + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    }

                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((j) * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + ((j - 1) * 50) - ((NumberList[j - 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j - 1]) * 25 / (rmax - rmin), (NumberList[j - 1] - rmin) * (50) / (rmax - rmin), (NumberList[j - 1] - rmin) * (50) / (rmax - rmin));
                                    //Delay
                                    if (pause)
                                    {
                                        timer1.Enabled = false;
                                        while (true)
                                        {
                                            await Task.Delay(1);
                                            if (!pause)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        timer1.Enabled = true;
                                        await Task.Delay(Delay);
                                    }
                                    //Green colums back to normal
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (j - 1) * ColumWidth, (rmax - NumberList[j - 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j - 1] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - 1) * ColumWidth, (rmax - NumberList[j - 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[j - 1] - rmin) * ColumHeight / (rmax - rmin));
                                    //Green circles back to normal
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j - 1] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((j - 1) * 50) - ((NumberList[j - 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j - 1]) * 25 / (rmax - rmin), (NumberList[j - 1] - rmin) * (50) / (rmax - rmin), (NumberList[j - 1] - rmin) * (50) / (rmax - rmin));
                                    //List back to normal
                                    listView1.Items[j - 1].Focused = false;
                                    listView1.Items[j - 1].Selected = false;
                                }
                            }

                            //If there were no exchanges, break
                            if (!swapFlag)
                            {
                                break;
                            }
                        }
                        if (language == "English")
                        {
                            button2.Text = "Start sorting";
                        }
                        else if (language == "Ukrainian")
                        {
                            button2.Text = "Почати сортування";
                        }
                        label6.Visible = false;
                        comboBox1.Enabled = true;
                        inProcess = false;
                        simpleSound.Stop();
                        unsortedArrayExist = false;
                        timer1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Insertion Sort")
                    {
                        label6.Visible = true;
                        for (int i = 0; i < NumberList.Count(); i++)
                        {
                            var item = NumberList[i];
                            var currentIndex = i;
                            int ellipsy = 20;
                            int ellipsx = 0;
                            while (currentIndex > 0 && NumberList[currentIndex - 1] > item)
                            {
                                //Stop check
                                if (inProcess == false)
                                {
                                    return;
                                }
                                //Actually sorting algorithm
                                NumberList[currentIndex] = NumberList[currentIndex - 1];
                                currentIndex--;
                                //List visualisation
                                listView1.Items[currentIndex].Focused = true;
                                listView1.Items[currentIndex].Selected = true;
                                listView1.Items[currentIndex].Text = Convert.ToString(item);
                                listView1.Items[currentIndex + 1].Text = Convert.ToString(NumberList[currentIndex + 1]);
                                //Statistics label
                                if (language == "English")
                                {
                                    label6.Text = "Current number: " + item + ", current position: " + (currentIndex + 1);
                                    iterations++;
                                    label7.Text = "Number of iterations: " + iterations;
                                    passes++;
                                    label8.Text = "Number of passes: " + passes;
                                }
                                else if (language == "Ukrainian")
                                {
                                    label6.Text = "Поточне число: " + item + ", поточна позицiя: " + (currentIndex + 1);
                                    iterations++;
                                    label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                    passes++;
                                    label8.Text = "Кiлькiсть проходжень: " + passes;
                                }
                                //Sound
                                simpleSound.Play();
                                //Colums
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), currentIndex * ColumWidth, 0, ColumWidth, ColumHeight);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (currentIndex + 1) * ColumWidth, 0, ColumWidth, ColumHeight);

                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), currentIndex * ColumWidth, (rmax - item) * ColumHeight / (rmax - rmin), ColumWidth, (item - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), currentIndex * ColumWidth, (rmax - item) * ColumHeight / (rmax - rmin), 1, (item - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (currentIndex + 1) * ColumWidth, (rmax - NumberList[currentIndex + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[currentIndex + 1] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (currentIndex + 1) * ColumWidth, (rmax - NumberList[currentIndex + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[currentIndex + 1] - rmin) * ColumHeight / (rmax - rmin));
                                //Change circles location on next row
                                if (currentIndex + 1 >= 0 && currentIndex + 1 < 20)
                                {
                                    ellipsy = 20;
                                    ellipsx = 0;
                                }
                                else if (currentIndex + 1 >= 20 && currentIndex + 1 < 40)
                                {
                                    ellipsy = 70;
                                    ellipsx = 1000;
                                }
                                else if (currentIndex + 1 >= 40 && currentIndex + 1 < 60)
                                {
                                    ellipsy = 120;
                                    ellipsx = 2000;
                                }
                                else if (currentIndex + 1 >= 60 && currentIndex + 1 < 80)
                                {
                                    ellipsy = 170;
                                    ellipsx = 3000;
                                }
                                else if (currentIndex + 1 >= 80 && currentIndex + 1 < 100)
                                {
                                    ellipsy = 220;
                                    ellipsx = 4000;
                                }
                                else if (currentIndex + 1 >= 100 && currentIndex + 1 < 120)
                                {
                                    ellipsy = 270;
                                    ellipsx = 5000;
                                }
                                else if (currentIndex + 1 >= 120 && currentIndex + 1 < 140)
                                {
                                    ellipsy = 320;
                                    ellipsx = 6000;
                                }
                                else if (currentIndex + 1 >= 140 && currentIndex + 1 < 160)
                                {
                                    ellipsy = 370;
                                    ellipsx = 7000;
                                }
                                else if (currentIndex + 1 >= 160 && currentIndex + 1 < 180)
                                {
                                    ellipsy = 420;
                                    ellipsx = 8000;
                                }
                                else if (currentIndex + 1 >= 180 && currentIndex + 1 < 200)
                                {
                                    ellipsy = 470;
                                    ellipsx = 9000;
                                }
                                //Circles
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (currentIndex * 50), ColumHeight + ellipsy, 50, 50);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + ((currentIndex + 1) * 50), ColumHeight + ellipsy, 50, 50);

                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + (currentIndex * 50) - ((item - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - item) * 25 / (rmax - rmin), (item - rmin) * (50) / (rmax - rmin), (item - rmin) * (50) / (rmax - rmin));
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (item - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((currentIndex + 1) * 50) - ((NumberList[currentIndex + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[currentIndex + 1]) * 25 / (rmax - rmin), (NumberList[currentIndex + 1] - rmin) * (50) / (rmax - rmin), (NumberList[currentIndex + 1] - rmin) * (50) / (rmax - rmin));
                                //Delay
                                if (pause)
                                {
                                    timer1.Enabled = false;
                                    while (true)
                                    {
                                        await Task.Delay(1);
                                        if (!pause)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    timer1.Enabled = true;
                                    await Task.Delay(Delay);
                                }
                                //Green colums back to normal
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (currentIndex + 1) * ColumWidth, (rmax - NumberList[currentIndex + 1]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[currentIndex + 1] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (currentIndex + 1) * ColumWidth, (rmax - NumberList[currentIndex + 1]) * ColumHeight / (rmax - rmin), 1, (NumberList[currentIndex + 1] - rmin) * ColumHeight / (rmax - rmin));
                                //Green circles back to normal
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[currentIndex + 1] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((currentIndex + 1) * 50) - ((NumberList[currentIndex + 1] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[currentIndex + 1]) * 25 / (rmax - rmin), (NumberList[currentIndex + 1] - rmin) * (50) / (rmax - rmin), (NumberList[currentIndex + 1] - rmin) * (50) / (rmax - rmin));
                                //List back to normal
                                listView1.Items[currentIndex].Focused = false;
                                listView1.Items[currentIndex].Selected = false;
                            }
                            //Actually sorting algorithm
                            NumberList[currentIndex] = item;
                            //List visualisation
                            listView1.Items[currentIndex].Text = Convert.ToString(item);
                            //Colums
                            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), currentIndex * ColumWidth, 0, ColumWidth, ColumHeight);

                            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), currentIndex * ColumWidth, (rmax - item) * ColumHeight / (rmax - rmin), ColumWidth, (item - rmin) * ColumHeight / (rmax - rmin));
                            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), currentIndex * ColumWidth, (rmax - item) * ColumHeight / (rmax - rmin), 1, (item - rmin) * ColumHeight / (rmax - rmin));
                            //Circles
                            g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (currentIndex * 50), ColumHeight + ellipsy, 50, 50);

                            g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (item - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + (currentIndex * 50) - ((item - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - item) * 25 / (rmax - rmin), (item - rmin) * (50) / (rmax - rmin), (item - rmin) * (50) / (rmax - rmin));
                        }
                        if (language == "English")
                        {
                            button2.Text = "Start sorting";
                        }
                        else if (language == "Ukrainian")
                        {
                            button2.Text = "Почати сортування";
                        }
                        label6.Visible = false;
                        comboBox1.Enabled = true;
                        inProcess = false;
                        simpleSound.Stop();
                        unsortedArrayExist = false;
                        timer1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Selection Sort")
                    {
                        label6.Visible = true;
                        label9.Visible = true;
                        for (var i = 0; i < NumberList.Count; i++)
                        {
                            int ellipsy = 20;
                            int ellipsx = 0;
                            int ellipsymin = 20;
                            int ellipsxmin = 0;
                            var min = i;
                            for (var j = i + 1; j < NumberList.Count; j++)
                            {
                                if (NumberList[min] > NumberList[j])
                                {
                                    min = j;
                                }
                                //Stop check
                                if (inProcess == false)
                                {
                                    return;
                                }
                                //Statistics label
                                if (language == "English")
                                {
                                    passes++;
                                    label8.Text = "Number of passes: " + passes;
                                }
                                else if (language == "Ukrainian")
                                {
                                    passes++;
                                    label8.Text = "Кiлькiсть проходжень: " + passes;
                                }
                                //List visualisation
                                listView1.Items[j].Focused = true;
                                listView1.Items[j].Selected = true;
                                //Colums
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                //Change circles location on next row
                                if (j >= 0 && j < 20)
                                {
                                    ellipsy = 20;
                                    ellipsx = 0;
                                }
                                else if (j >= 20 && j < 40)
                                {
                                    ellipsy = 70;
                                    ellipsx = 1000;
                                }
                                else if (j >= 40 && j < 60)
                                {
                                    ellipsy = 120;
                                    ellipsx = 2000;
                                }
                                else if (j >= 60 && j < 80)
                                {
                                    ellipsy = 170;
                                    ellipsx = 3000;
                                }
                                else if (j >= 80 && j < 100)
                                {
                                    ellipsy = 220;
                                    ellipsx = 4000;
                                }
                                else if (j >= 100 && j < 120)
                                {
                                    ellipsy = 270;
                                    ellipsx = 5000;
                                }
                                else if (j >= 120 && j < 140)
                                {
                                    ellipsy = 320;
                                    ellipsx = 6000;
                                }
                                else if (j >= 140 && j < 160)
                                {
                                    ellipsy = 370;
                                    ellipsx = 7000;
                                }
                                else if (j >= 160 && j < 180)
                                {
                                    ellipsy = 420;
                                    ellipsx = 8000;
                                }
                                else if (j >= 180 && j < 200)
                                {
                                    ellipsy = 470;
                                    ellipsx = 9000;
                                }
                                //Circles
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + (j * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                //Delay
                                if (pause)
                                {
                                    timer1.Enabled = false;
                                    while (true)
                                    {
                                        await Task.Delay(1);
                                        if (!pause)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    timer1.Enabled = true;
                                    await Task.Delay(Delay);
                                }
                                //List back to normal
                                listView1.Items[j].Focused = false;
                                listView1.Items[j].Selected = false;
                                //Green colums back to normal
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                //Green circles back to normal
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + (j * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                            }
                            if (min != i)
                            {
                                //Actually sorting algorithm & List visualisation
                                var lowerValue = NumberList[min];
                                listView1.Items[min].Text = Convert.ToString(NumberList[i]);
                                NumberList[min] = NumberList[i];
                                listView1.Items[i].Text = Convert.ToString(lowerValue);
                                NumberList[i] = lowerValue;
                                listView1.Items[i].Focused = true;
                                listView1.Items[i].Selected = true;
                                listView1.Items[min].Focused = true;
                                listView1.Items[min].Selected = true;
                                //Statistics label
                                if (language == "English")
                                {
                                    label6.Text = "First current number: " + NumberList[i] + ", first current position: " + (i + 1);
                                    label9.Text = "Second current number: " + NumberList[min] + ", second current position: " + (min + 1);
                                    iterations++;
                                    label7.Text = "Number of iterations: " + iterations;
                                }
                                else if (language == "Ukrainian")
                                {
                                    label6.Text = "Перше поточне число: " + NumberList[i] + ", перша поточна позицiя: " + (i + 1);
                                    label9.Text = "Друге поточне число: " + NumberList[min] + ", друга поточна позицiя: " + (min + 1);
                                    iterations++;
                                    label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                }
                                //Sound
                                simpleSound.Play();
                                //Colums
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), min * ColumWidth, 0, ColumWidth, ColumHeight);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, 0, ColumWidth, ColumHeight);

                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)), min * ColumWidth, (rmax - NumberList[min]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[min] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), min * ColumWidth, (rmax - NumberList[min]) * ColumHeight / (rmax - rmin), 1, (NumberList[min] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), 1, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                //Change circles location on next row
                                if (i >= 0 && i < 20)
                                {
                                    ellipsy = 20;
                                    ellipsx = 0;
                                }
                                else if (i >= 20 && i < 40)
                                {
                                    ellipsy = 70;
                                    ellipsx = 1000;
                                }
                                else if (i >= 40 && i < 60)
                                {
                                    ellipsy = 120;
                                    ellipsx = 2000;
                                }
                                else if (i >= 60 && i < 80)
                                {
                                    ellipsy = 170;
                                    ellipsx = 3000;
                                }
                                else if (i >= 80 && i < 100)
                                {
                                    ellipsy = 220;
                                    ellipsx = 4000;
                                }
                                else if (i >= 100 && i < 120)
                                {
                                    ellipsy = 270;
                                    ellipsx = 5000;
                                }
                                else if (i >= 120 && i < 140)
                                {
                                    ellipsy = 320;
                                    ellipsx = 6000;
                                }
                                else if (i >= 140 && i < 160)
                                {
                                    ellipsy = 370;
                                    ellipsx = 7000;
                                }
                                else if (i >= 160 && i < 180)
                                {
                                    ellipsy = 420;
                                    ellipsx = 8000;
                                }
                                else if (i >= 180 && i < 200)
                                {
                                    ellipsy = 470;
                                    ellipsx = 9000;
                                }

                                if (min >= 0 && min < 20)
                                {
                                    ellipsymin = 20;
                                    ellipsxmin = 0;
                                }
                                else if (min >= 20 && min < 40)
                                {
                                    ellipsymin = 70;
                                    ellipsxmin = 1000;
                                }
                                else if (min >= 40 && min < 60)
                                {
                                    ellipsymin = 120;
                                    ellipsxmin = 2000;
                                }
                                else if (min >= 60 && min < 80)
                                {
                                    ellipsymin = 170;
                                    ellipsxmin = 3000;
                                }
                                else if (min >= 80 && min < 100)
                                {
                                    ellipsymin = 220;
                                    ellipsxmin = 4000;
                                }
                                else if (min >= 100 && min < 120)
                                {
                                    ellipsymin = 270;
                                    ellipsxmin = 5000;
                                }
                                else if (min >= 120 && min < 140)
                                {
                                    ellipsymin = 320;
                                    ellipsxmin = 6000;
                                }
                                else if (min >= 140 && min < 160)
                                {
                                    ellipsymin = 370;
                                    ellipsxmin = 7000;
                                }
                                else if (min >= 160 && min < 180)
                                {
                                    ellipsymin = 420;
                                    ellipsxmin = 8000;
                                }
                                else if (min >= 180 && min < 200)
                                {
                                    ellipsymin = 470;
                                    ellipsxmin = 9000;
                                }
                                //Circles
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsxmin + (min * 50), ColumHeight + ellipsymin, 50, 50);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (i * 50), ColumHeight + ellipsy, 50, 50);

                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)), -ellipsxmin + 25 + (min * 50) - ((NumberList[min] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsymin + (rmax - NumberList[min]) * 25 / (rmax - rmin), (NumberList[min] - rmin) * (50) / (rmax - rmin), (NumberList[min] - rmin) * (50) / (rmax - rmin));
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx + 25 + (i * 50) - ((NumberList[i] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i]) * 25 / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin));
                                //Delay
                                if (pause)
                                {
                                    timer1.Enabled = false;
                                    while (true)
                                    {
                                        await Task.Delay(1);
                                        if (!pause)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    timer1.Enabled = true;
                                    await Task.Delay(Delay);
                                }
                                //Green colums back to normal
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), 1, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), min * ColumWidth, (rmax - NumberList[min]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[min] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), min * ColumWidth, (rmax - NumberList[min]) * ColumHeight / (rmax - rmin), 1, (NumberList[min] - rmin) * ColumHeight / (rmax - rmin));
                                //Green circles back to normal
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[min] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsxmin + 25 + ((min) * 50) - ((NumberList[min] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsymin + (rmax - NumberList[min]) * 25 / (rmax - rmin), (NumberList[min] - rmin) * (50) / (rmax - rmin), (NumberList[min] - rmin) * (50) / (rmax - rmin));
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[i] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + ((i) * 50) - ((NumberList[i] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i]) * 25 / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin));
                                //List back to normal
                                listView1.Items[i].Focused = false;
                                listView1.Items[i].Selected = false;
                                listView1.Items[min].Focused = false;
                                listView1.Items[min].Selected = false;
                            }
                        }
                        if (language == "English")
                        {
                            button2.Text = "Start sorting";
                        }
                        else if (language == "Ukrainian")
                        {
                            button2.Text = "Почати сортування";
                        }
                        label6.Visible = false;
                        label9.Visible = false;
                        comboBox1.Enabled = true;
                        inProcess = false;
                        simpleSound.Stop();
                        unsortedArrayExist = false;
                        timer1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Shell Sort")
                    {
                        label6.Visible = true;
                        label9.Visible = true;
                        int ellipsy1 = 20;
                        int ellipsx1 = 0;
                        int ellipsy2 = 20;
                        int ellipsx2 = 0;
                        //Distance between elements that are compared
                        var d = NumberList.Count / 2;
                        while (d >= 1)
                        {
                            for (var i = d; i < NumberList.Count; i++)
                            {
                                var j = i;
                                while ((j >= d) && (NumberList[j - d] > NumberList[j]))
                                {
                                    //Stop check
                                    if (inProcess == false)
                                    {
                                        return;
                                    }
                                    //List visualisation
                                    listView1.Items[j].Text = Convert.ToString(NumberList[j - d]);
                                    listView1.Items[j - d].Text = Convert.ToString(NumberList[j]);
                                    listView1.Items[j].Focused = true;
                                    listView1.Items[j].Selected = true;
                                    listView1.Items[j - d].Focused = true;
                                    listView1.Items[j - d].Selected = true;
                                    //Statistics label
                                    if (language == "English")
                                    {
                                        label6.Text = "First current number: " + NumberList[j] + ", first current position: " + (j - d + 1);
                                        label9.Text = "Second current number: " + NumberList[j - d] + ", second current position: " + (j + 1);
                                        iterations++;
                                        label7.Text = "Number of iterations: " + iterations;
                                        passes++;
                                        label8.Text = "Number of passes: " + passes;
                                    }
                                    else if (language == "Ukrainian")
                                    {
                                        label6.Text = "Перше поточне число: " + NumberList[j] + ", перша поточна позицiя: " + (j - d + 1);
                                        label9.Text = "Друге поточне число: " + NumberList[j - d] + ", друга поточна позицiя: " + (j + 1);
                                        iterations++;
                                        label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                        passes++;
                                        label8.Text = "Кiлькiсть проходжень: " + passes;
                                    }
                                    //Sound
                                    simpleSound.Play();
                                    //Colums
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, 0, ColumWidth, ColumHeight);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - d) * ColumWidth, 0, ColumWidth, ColumHeight);

                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)),j * ColumWidth, (rmax - NumberList[j - d]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j - d] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j - d]) * ColumHeight / (rmax - rmin), 1, (NumberList[j - d] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), (j - d) * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - d) * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    //Change circles location on next row
                                    if (j >= 0 && j < 20)
                                    {
                                        ellipsy1 = 20;
                                        ellipsx1 = 0;
                                    }
                                    else if (j >= 20 && j < 40)
                                    {
                                        ellipsy1 = 70;
                                        ellipsx1 = 1000;
                                    }
                                    else if (j >= 40 && j < 60)
                                    {
                                        ellipsy1 = 120;
                                        ellipsx1 = 2000;
                                    }
                                    else if (j >= 60 && j < 80)
                                    {
                                        ellipsy1 = 170;
                                        ellipsx1 = 3000;
                                    }
                                    else if (j >= 80 && j < 100)
                                    {
                                        ellipsy1 = 220;
                                        ellipsx1 = 4000;
                                    }
                                    else if (j >= 100 && j < 120)
                                    {
                                        ellipsy1 = 270;
                                        ellipsx1 = 5000;
                                    }
                                    else if (j >= 120 && j < 140)
                                    {
                                        ellipsy1 = 320;
                                        ellipsx1 = 6000;
                                    }
                                    else if (j >= 140 && j < 160)
                                    {
                                        ellipsy1 = 370;
                                        ellipsx1 = 7000;
                                    }
                                    else if (j >= 160 && j < 180)
                                    {
                                        ellipsy1 = 420;
                                        ellipsx1 = 8000;
                                    }
                                    else if (j >= 180 && j < 200)
                                    {
                                        ellipsy1 = 470;
                                        ellipsx1 = 9000;
                                    }

                                    if ((j - d) >= 0 && (j - d) < 20)
                                    {
                                        ellipsy2 = 20;
                                        ellipsx2 = 0;
                                    }
                                    else if ((j - d) >= 20 && (j - d) < 40)
                                    {
                                        ellipsy2 = 70;
                                        ellipsx2 = 1000;
                                    }
                                    else if ((j - d) >= 40 && (j - d) < 60)
                                    {
                                        ellipsy2 = 120;
                                        ellipsx2 = 2000;
                                    }
                                    else if ((j - d) >= 60 && (j - d) < 80)
                                    {
                                        ellipsy2 = 170;
                                        ellipsx2 = 3000;
                                    }
                                    else if ((j - d) >= 80 && (j - d) < 100)
                                    {
                                        ellipsy2 = 220;
                                        ellipsx2 = 4000;
                                    }
                                    else if ((j - d) >= 100 && (j - d) < 120)
                                    {
                                        ellipsy2 = 270;
                                        ellipsx2 = 5000;
                                    }
                                    else if ((j - d) >= 120 && (j - d) < 140)
                                    {
                                        ellipsy2 = 320;
                                        ellipsx2 = 6000;
                                    }
                                    else if ((j - d) >= 140 && (j - d) < 160)
                                    {
                                        ellipsy2 = 370;
                                        ellipsx2 = 7000;
                                    }
                                    else if ((j - d) >= 160 && (j - d) < 180)
                                    {
                                        ellipsy2 = 420;
                                        ellipsx2 = 8000;
                                    }
                                    else if ((j - d) >= 180 && (j - d) < 200)
                                    {
                                        ellipsy2 = 470;
                                        ellipsx2 = 9000;
                                    }
                                    //Circles
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx2 + ((j - d) * 50), ColumHeight + ellipsy2, 50, 50);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx1 + (j * 50), ColumHeight + ellipsy1, 50, 50);

                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)), -ellipsx2 + 25 + ((j - d) * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy2 + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx1 + 25 + (j * 50) - ((NumberList[j - d] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy1 + (rmax - NumberList[j - d]) * 25 / (rmax - rmin), (NumberList[j - d] - rmin) * (50) / (rmax - rmin), (NumberList[j - d] - rmin) * (50) / (rmax - rmin));
                                    //Delay
                                    if (pause)
                                    {
                                        timer1.Enabled = false;
                                        while (true)
                                        {
                                            await Task.Delay(1);
                                            if (!pause)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        timer1.Enabled = true;
                                        await Task.Delay(Delay);
                                    }
                                    //List back to normal
                                    listView1.Items[j].Focused = false;
                                    listView1.Items[j].Selected = false;
                                    listView1.Items[j - d].Focused = false;
                                    listView1.Items[j - d].Selected = false;
                                    //Green colums back to normal
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), j * ColumWidth, (rmax - NumberList[j - d]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j - d] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), j * ColumWidth, (rmax - NumberList[j - d]) * ColumHeight / (rmax - rmin), 1, (NumberList[j - d] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), (j - d) * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (j - d) * ColumWidth, (rmax - NumberList[j]) * ColumHeight / (rmax - rmin), 1, (NumberList[j] - rmin) * ColumHeight / (rmax - rmin));
                                    //Green circles back to normal
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx2 + 25 + ((j - d) * 50) - ((NumberList[j] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy2 + (rmax - NumberList[j]) * 25 / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin), (NumberList[j] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[j - d] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx1 + 25 + ((j) * 50) - ((NumberList[j - d] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy1 + (rmax - NumberList[j - d]) * 25 / (rmax - rmin), (NumberList[j - d] - rmin) * (50) / (rmax - rmin), (NumberList[j - d] - rmin) * (50) / (rmax - rmin));

                                    //Actually sorting algorithm
                                    float temp = NumberList[j];
                                    NumberList[j] = NumberList[j - d];
                                    NumberList[j - d] = temp;
                                    j -= d;
                                    
                                }
                            }
                            d = d / 2;
                        }
                        if (language == "English")
                        {
                            button2.Text = "Start sorting";
                        }
                        else if (language == "Ukrainian")
                        {
                            button2.Text = "Почати сортування";
                        }
                        label6.Visible = false;
                        label9.Visible = false;
                        comboBox1.Enabled = true;
                        inProcess = false;
                        simpleSound.Stop();
                        unsortedArrayExist = false;
                        timer1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Quick Sort")
                    {
                        label6.Visible = true;
                        label9.Visible = true;
                        int ellipsy1 = 20;
                        int ellipsx1 = 0;
                        int ellipsy2 = 20;
                        int ellipsx2 = 0;

                        int findNextR(int l, int size)
                        {
                            for (int i = l; i < size; ++i)
                            {
                                if (language == "English")
                                {
                                    passes++;
                                    label8.Text = "Number of passes: " + passes;
                                }
                                else if (language == "Ukrainian")
                                {
                                    passes++;
                                    label8.Text = "Кiлькiсть проходжень: " + passes;
                                }
                                if (NumberList[i] < 0)
                                    return i;
                            }
                            return size - 1;
                        }

                        async Task<int> partition(int l, int r)
                        {
                            float pivot = NumberList[(l + r) / 2];
                            while (l <= r)
                            {
                                while (NumberList[r] > pivot)
                                    r--;
                                while (NumberList[l] < pivot)
                                    l++;
                                if (l <= r)
                                {
                                    //Stop check
                                    if (inProcess == false)
                                    {
                                        return 0;
                                    }
                                    //List visualisation
                                    listView1.Items[r].Text = Convert.ToString(NumberList[l]);
                                    listView1.Items[l].Text = Convert.ToString(NumberList[r]);
                                    listView1.Items[l].Focused = true;
                                    listView1.Items[l].Selected = true;
                                    listView1.Items[r].Focused = true;
                                    listView1.Items[r].Selected = true;
                                    //Statistics label
                                    if (language == "English")
                                    {
                                        label6.Text = "First current number: " + NumberList[r] + ", first current position: " + (r);
                                        label9.Text = "Second current number: " + NumberList[l] + ", second current position: " + (l);
                                        iterations++;
                                        label7.Text = "Number of iterations: " + iterations;
                                        passes++;
                                        label8.Text = "Number of passes: " + passes;
                                    }
                                    else if (language == "Ukrainian")
                                    {
                                        label6.Text = "Перше поточне число: " + NumberList[r] + ", перша поточна позицiя: " + (r);
                                        label9.Text = "Друге поточне число: " + NumberList[l] + ", друга поточна позицiя: " + (l);
                                        iterations++;
                                        label7.Text = "Кiлькiсть iтерацiй: " + iterations;
                                        passes++;
                                        label8.Text = "Кiлькiсть проходжень: " + passes;
                                    }
                                    //Sound
                                    simpleSound.Play();
                                    //Colums
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), r * ColumWidth, 0, ColumWidth, ColumHeight);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (l) * ColumWidth, 0, ColumWidth, ColumHeight);

                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)), r * ColumWidth, (rmax - NumberList[l]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[l] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), r * ColumWidth, (rmax - NumberList[l]) * ColumHeight / (rmax - rmin), 1, (NumberList[l] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), (l) * ColumWidth, (rmax - NumberList[r]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[r] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), (l) * ColumWidth, (rmax - NumberList[r]) * ColumHeight / (rmax - rmin), 1, (NumberList[r] - rmin) * ColumHeight / (rmax - rmin));
                                    //Change circles location on next row
                                    if (r >= 0 && r < 20)
                                    {
                                        ellipsy1 = 20;
                                        ellipsx1 = 0;
                                    }
                                    else if (r >= 20 && r < 40)
                                    {
                                        ellipsy1 = 70;
                                        ellipsx1 = 1000;
                                    }
                                    else if (r >= 40 && r < 60)
                                    {
                                        ellipsy1 = 120;
                                        ellipsx1 = 2000;
                                    }
                                    else if (r >= 60 && r < 80)
                                    {
                                        ellipsy1 = 170;
                                        ellipsx1 = 3000;
                                    }
                                    else if (r >= 80 && r < 100)
                                    {
                                        ellipsy1 = 220;
                                        ellipsx1 = 4000;
                                    }
                                    else if (r >= 100 && r < 120)
                                    {
                                        ellipsy1 = 270;
                                        ellipsx1 = 5000;
                                    }
                                    else if (r >= 120 && r < 140)
                                    {
                                        ellipsy1 = 320;
                                        ellipsx1 = 6000;
                                    }
                                    else if (r >= 140 && r < 160)
                                    {
                                        ellipsy1 = 370;
                                        ellipsx1 = 7000;
                                    }
                                    else if (r >= 160 && r < 180)
                                    {
                                        ellipsy1 = 420;
                                        ellipsx1 = 8000;
                                    }
                                    else if (r >= 180 && r < 200)
                                    {
                                        ellipsy1 = 470;
                                        ellipsx1 = 9000;
                                    }

                                    if (l >= 0 && l < 20)
                                    {
                                        ellipsy2 = 20;
                                        ellipsx2 = 0;
                                    }
                                    else if (l >= 20 && l < 40)
                                    {
                                        ellipsy2 = 70;
                                        ellipsx2 = 1000;
                                    }
                                    else if (l >= 40 && l < 60)
                                    {
                                        ellipsy2 = 120;
                                        ellipsx2 = 2000;
                                    }
                                    else if (l >= 60 && l < 80)
                                    {
                                        ellipsy2 = 170;
                                        ellipsx2 = 3000;
                                    }
                                    else if (l >= 80 && l < 100)
                                    {
                                        ellipsy2 = 220;
                                        ellipsx2 = 4000;
                                    }
                                    else if (l >= 100 && l < 120)
                                    {
                                        ellipsy2 = 270;
                                        ellipsx2 = 5000;
                                    }
                                    else if (l >= 120 && l < 140)
                                    {
                                        ellipsy2 = 320;
                                        ellipsx2 = 6000;
                                    }
                                    else if (l >= 140 && l < 160)
                                    {
                                        ellipsy2 = 370;
                                        ellipsx2 = 7000;
                                    }
                                    else if (l >= 160 && l < 180)
                                    {
                                        ellipsy2 = 420;
                                        ellipsx2 = 8000;
                                    }
                                    else if (l >= 180 && l < 200)
                                    {
                                        ellipsy2 = 470;
                                        ellipsx2 = 9000;
                                    }
                                    //Circles
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx2 + (l * 50), ColumHeight + ellipsy2, 50, 50);
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx1 + (r * 50), ColumHeight + ellipsy1, 50, 50);

                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 200)), -ellipsx2 + 25 + (l * 50) - ((NumberList[r] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy2 + (rmax - NumberList[r]) * 25 / (rmax - rmin), (NumberList[r] - rmin) * (50) / (rmax - rmin), (NumberList[r] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 28, 208, 92)), -ellipsx1 + 25 + (r * 50) - ((NumberList[l] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy1 + (rmax - NumberList[l]) * 25 / (rmax - rmin), (NumberList[l] - rmin) * (50) / (rmax - rmin), (NumberList[l] - rmin) * (50) / (rmax - rmin));
                                    //Delay
                                    if (pause)
                                    {
                                        timer1.Enabled = false;
                                        while (true)
                                        {
                                            await Task.Delay(1);
                                            if (!pause)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        timer1.Enabled = true;
                                        await Task.Delay(Delay);
                                    }
                                    //List back to normal
                                    listView1.Items[l].Focused = false;
                                    listView1.Items[l].Selected = false;
                                    listView1.Items[r].Focused = false;
                                    listView1.Items[r].Selected = false;
                                    //Green colums back to normal
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), r * ColumWidth, (rmax - NumberList[l]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[l] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), r * ColumWidth, (rmax - NumberList[l]) * ColumHeight / (rmax - rmin), 1, (NumberList[l] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), l * ColumWidth, (rmax - NumberList[r]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[r] - rmin) * ColumHeight / (rmax - rmin));
                                    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), l * ColumWidth, (rmax - NumberList[r]) * ColumHeight / (rmax - rmin), 1, (NumberList[r] - rmin) * ColumHeight / (rmax - rmin));
                                    //Green circles back to normal
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[r] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx2 + 25 + (l * 50) - ((NumberList[r] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy2 + (rmax - NumberList[r]) * 25 / (rmax - rmin), (NumberList[r] - rmin) * (50) / (rmax - rmin), (NumberList[r] - rmin) * (50) / (rmax - rmin));
                                    g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[l] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx1 + 25 + (r * 50) - ((NumberList[l] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy1 + (rmax - NumberList[l]) * 25 / (rmax - rmin), (NumberList[l] - rmin) * (50) / (rmax - rmin), (NumberList[l] - rmin) * (50) / (rmax - rmin));
                                    //Actually sorting algorithm
                                    float tmp = NumberList[r];
                                    NumberList[r] = NumberList[l];
                                    NumberList[l] = tmp;
                                    l++;
                                    r--;
                                }
                            }
                            return l;
                        }
                        int left = 0;
                        int right = NumberList.Count - 1;
                        int q, f = 0;
                        int tmpr = right;
                        while (true)
                        {
                            f--;
                            while (left < tmpr)
                            {
                                q = await partition(left, tmpr);
                                if (inProcess == false)
                                {
                                    for (int i = 0; i < NumberList.Count; i++)
                                    {
                                        if (language == "English")
                                        {
                                            passes++;
                                            label8.Text = "Number of passes: " + passes;
                                        }
                                        else if (language == "Ukrainian")
                                        {
                                            passes++;
                                            label8.Text = "Кiлькiсть проходжень: " + passes;
                                        }
                                        NumberList[i] = Math.Abs(NumberList[i]);
                                    }
                                    return;
                                }
                                NumberList[tmpr] = -NumberList[tmpr];
                                tmpr = q - 1;
                                ++f;
                            }
                            if (f < 0)
                                break;
                            left++;
                            tmpr = findNextR(left, NumberList.Count);
                            NumberList[tmpr] = -NumberList[tmpr];
                        }

                        if (language == "English")
                        {
                            button2.Text = "Start sorting";
                        }
                        else if (language == "Ukrainian")
                        {
                            button2.Text = "Почати сортування";
                        }
                        label6.Visible = false;
                        label9.Visible = false;
                        comboBox1.Enabled = true;
                        inProcess = false;
                        simpleSound.Stop();
                        unsortedArrayExist = false;
                        timer1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Bogosort")
                    {
                        label6.Visible = true;
                        label9.Visible = true;
                        int ellipsy = 20;
                        int ellipsx = 0;
                        int ellipsymin = 20;
                        int ellipsxmin = 0;
                        //Statistics label
                        label6.Text = "Random";
                        while (true)
                        {
                            //Statistics label
                            iterations++;
                            label7.Text = "Number of iterations: " + iterations;
                            Random random = new Random();
                            var n = NumberList.Count;
                            while (n > 1)
                            {
                                //Statistics label
                                passes++;
                                label8.Text = "Number of passes: " + passes;
                                n--;
                                var i = random.Next(n + 1);
                                var temp = NumberList[i];
                                //Stop check
                                if (inProcess == false)
                                {
                                    return;
                                }
                                //List visualisation
                                listView1.Items[i].Text = Convert.ToString(NumberList[n]);
                                listView1.Items[n].Text = Convert.ToString(temp);
                                //Actually sorting algorithm
                                NumberList[i] = NumberList[n];
                                NumberList[n] = temp;
                                //Colums
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, 0, ColumWidth, ColumHeight);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), n * ColumWidth, 0, ColumWidth, ColumHeight);

                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), i * ColumWidth, (rmax - NumberList[i]) * ColumHeight / (rmax - rmin), 1, (NumberList[i] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 20, 79, 137)), n * ColumWidth, (rmax - NumberList[n]) * ColumHeight / (rmax - rmin), ColumWidth, (NumberList[n] - rmin) * ColumHeight / (rmax - rmin));
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), n * ColumWidth, (rmax - NumberList[n]) * ColumHeight / (rmax - rmin), 1, (NumberList[n] - rmin) * ColumHeight / (rmax - rmin));
                                //Change circles location on next row
                                if (i >= 0 && i < 20)
                                {
                                    ellipsy = 20;
                                    ellipsx = 0;
                                }
                                else if (i >= 20 && i < 40)
                                {
                                    ellipsy = 70;
                                    ellipsx = 1000;
                                }
                                else if (i >= 40 && i < 60)
                                {
                                    ellipsy = 120;
                                    ellipsx = 2000;
                                }
                                else if (i >= 60 && i < 80)
                                {
                                    ellipsy = 170;
                                    ellipsx = 3000;
                                }
                                else if (i >= 80 && i < 100)
                                {
                                    ellipsy = 220;
                                    ellipsx = 4000;
                                }
                                else if (i >= 100 && i < 120)
                                {
                                    ellipsy = 270;
                                    ellipsx = 5000;
                                }
                                else if (i >= 120 && i < 140)
                                {
                                    ellipsy = 320;
                                    ellipsx = 6000;
                                }
                                else if (i >= 140 && i < 160)
                                {
                                    ellipsy = 370;
                                    ellipsx = 7000;
                                }
                                else if (i >= 160 && i < 180)
                                {
                                    ellipsy = 420;
                                    ellipsx = 8000;
                                }
                                else if (i >= 180 && i < 200)
                                {
                                    ellipsy = 470;
                                    ellipsx = 9000;
                                }

                                if (n >= 0 && n < 20)
                                {
                                    ellipsymin = 20;
                                    ellipsxmin = 0;
                                }
                                else if (n >= 20 && n < 40)
                                {
                                    ellipsymin = 70;
                                    ellipsxmin = 1000;
                                }
                                else if (n >= 40 && n < 60)
                                {
                                    ellipsymin = 120;
                                    ellipsxmin = 2000;
                                }
                                else if (n >= 60 && n < 80)
                                {
                                    ellipsymin = 170;
                                    ellipsxmin = 3000;
                                }
                                else if (n >= 80 && n < 100)
                                {
                                    ellipsymin = 220;
                                    ellipsxmin = 4000;
                                }
                                else if (n >= 100 && n < 120)
                                {
                                    ellipsymin = 270;
                                    ellipsxmin = 5000;
                                }
                                else if (n >= 120 && n < 140)
                                {
                                    ellipsymin = 320;
                                    ellipsxmin = 6000;
                                }
                                else if (n >= 140 && n < 160)
                                {
                                    ellipsymin = 370;
                                    ellipsxmin = 7000;
                                }
                                else if (n >= 160 && n < 180)
                                {
                                    ellipsymin = 420;
                                    ellipsxmin = 8000;
                                }
                                else if (n >= 180 && n < 200)
                                {
                                    ellipsymin = 470;
                                    ellipsxmin = 9000;
                                }
                                //Circles
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsxmin + (n * 50), ColumHeight + ellipsymin, 50, 50);
                                g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 23, 33, 43)), -ellipsx + (i * 50), ColumHeight + ellipsy, 50, 50);

                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[n] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsxmin + 25 + (n * 50) - ((NumberList[n] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsymin + (rmax - NumberList[n]) * 25 / (rmax - rmin), (NumberList[n] - rmin) * (50) / (rmax - rmin), (NumberList[n] - rmin) * (50) / (rmax - rmin));
                                g.FillEllipse(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, (Convert.ToInt32(2.55 * (NumberList[i] - rmin) * (50) / (rmax - rmin))), 79, 137)), -ellipsx + 25 + (i * 50) - ((NumberList[i] - rmin) * 25 / (rmax - rmin)), ColumHeight + ellipsy + (rmax - NumberList[i]) * 25 / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin), (NumberList[i] - rmin) * (50) / (rmax - rmin));
                            }
                            //Sound
                            simpleSound.Play();
                            //Delay
                            if (pause)
                            {
                                timer1.Enabled = false;
                                while (true)
                                {
                                    await Task.Delay(1);
                                    if (!pause)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                timer1.Enabled = true;
                                await Task.Delay(Delay);
                            }
                            //Is list sorted check
                            if (IsSorted())
                            {
                                if (language == "English")
                                {
                                    button2.Text = "Start sorting";
                                }
                                else if (language == "Ukrainian")
                                {
                                    button2.Text = "Почати сортування";
                                }
                                label6.Visible = false;
                                comboBox1.Enabled = true;
                                inProcess = false;
                                simpleSound.Stop();
                                unsortedArrayExist = false;
                                timer1.Enabled = false;
                                return;
                            }
                        }
                    }
                }
            }
            else
            {

                if (language == "English")
                {
                    label6.Text = "First current number: 0, first current position: 0";
                    label9.Text = "Second urrent number: 0, second current position: 0";
                    button2.Text = "Start sorting";
                }
                else if (language == "Ukrainian")
                {
                    label6.Text = "Перше поточне число: 0, перша поточна позицiя: 0";
                    label9.Text = "Друге поточне число: 0, друга поточна позицiя: 0";
                    button2.Text = "Почати сортування";
                }
                label6.Visible = false;
                label9.Visible = false;
                comboBox1.Enabled = true;
                inProcess = false;
                simpleSound.Stop();
                timer1.Enabled = false;
            }
        }

        private bool IsSorted()
        {
            for (int i = 0; i < NumberList.Count - 1; i++)
            {
                if (NumberList[i] > NumberList[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Delay > 25)
            {
                Delay -= 25;
            }
            else
            {
                Delay = 1;
            }
            if (language == "English")
            {
                label3.Text = "Delay = " + Delay;
            }
            else if (language == "Ukrainian")
            {
                label3.Text = "Затримка = " + Delay;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Delay == 1)
            {
                Delay = 25;
            }
            else
            {
                Delay += 25;
            }
            if (language == "English")
            {
                label3.Text = "Delay = " + Delay;
            }
            else if (language == "Ukrainian")
            {
                label3.Text = "Затримка = " + Delay;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Delay == 1)
            {
                Workingtime += 100;
            }
            else
            {
                Workingtime += (1000 / (Convert.ToDouble(Delay)));
            }
            if (language == "English")
            {
                label2.Text = "Working time (considering a one second delay): " + (Workingtime / 100).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (language == "Ukrainian")
            {
                label2.Text = "Час роботи (враховуючи затримку в одну секунду): " + (Workingtime / 100).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (Convert.ToInt32(textBox1.Text) > 200)
                {
                    textBox1.Text = "200";
                }
                if (Convert.ToInt32(textBox1.Text) < 3)
                {
                    textBox1.Text = "3";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            language = "Ukrainian";
            button6.ForeColor = Color.White;
            button7.ForeColor = Color.FromArgb(255, 23, 33, 43);
            label1.Text = "Введiть кiлькiсть чисел у масивi";
            label2.Text = "Час роботи (враховуючи затримку в одну секунду): ";
            label3.Text = "Затримка = " + Delay;
            label4.Text = "Згенерувати числа вiд";
            label5.Text = "до";
            label6.Text = "Поточне число: " + "поточна позицiя: ";
            label9.Text = "Друге поточне число: " + "друга поточна позицiя: ";
            label7.Text = "Кiлькiсть iтерацiй: " + iterations;
            label8.Text = "Кiлькiсть проходжень: " + passes;
            button1.Text = "Згенерувати масив";
            if (inProcess == false)
            {
                button2.Text = "Почати сортування";
            }
            else
            {
                button2.Text = "Зупинити сортування";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            language = "English";
            button6.ForeColor = Color.FromArgb(255, 23, 33, 43);
            button7.ForeColor = Color.White;
            label1.Text = "Enter the number of elements in the array";
            label2.Text = "Working time (considering a one second delay): ";
            label3.Text = "Delay = " + Delay;
            label4.Text = "Generate numbers from";
            label5.Text = "to";
            label6.Text = "Current number: " + "current position: ";
            label9.Text = "Second current number: " + "second current position: ";
            label7.Text = "Number of iterations: " + iterations;
            label8.Text = "Number of passes: " + passes;
            button1.Text = "Generate an array";
            if (inProcess == false)
            {
                button2.Text = "Start sorting";
            }
            else
            {
                button2.Text = "Stop sorting";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (inProcess == true)
            {
                if (unsortedArrayExist)
                {
                    if (pause == false)
                    {
                        pause = true;
                        button5.Text = " >";
                    }
                    else
                    {
                        pause = false;
                        button5.Text = " | |";
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
