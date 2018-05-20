using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Игра
{
    public partial class Form1 : Form
    {
        //abstract class CBase
        //{
        //    int x, y;
        //    Bitmap image;

        //    virtual public bool intersect(CBase)
        //    {

        //    }
        //}

        //class E1:CBase
        //{
        //    public int Left(x)...
        //}

        static int curHeight = 50;
        static Random r = new Random();
        static List<Bottle> list = new List<Bottle>();
        static int n;
        int time = 0;
        Bird mainBird;
        List<Image> bottleList = new List<Image>() { Properties.Resources.Bottle1, Properties.Resources.Bottle2, Properties.Resources.Bottle3, Properties.Resources.Bottle4, Properties.Resources.Bottle5 };

        public class Bottle
        {
            public int x;
            public int y;
            public PictureBox image;
            Thread thr;
            int wight = r.Next(150);
            int speed = r.Next(7);

            public Bottle(int Y, PictureBox I)
            {
                x = -150;
                y = Y;
                image = I;
                thr = new Thread(newBottleThread);
                thr.IsBackground = true;
                thr.Start();
            }

            bool intersect(int y0)
            {
                if (x + 100 <= 700)
                    return false;
                if (x >= 800)
                    return false;
                if (y + 40 <= y0)
                    return false;
                if (y > y0 + 100)
                    return false;
                return true;
            }

            void newBottleThread()
            {
                while (x <= 1000)
                {
                    Thread.Sleep(10);
                        x += speed;
                    if (intersect(curHeight))
                    {
                        n = n - wight;
                        wight = 0;
                        if (n < 0)
                        {
                            MessageBox.Show("Game over");
                            System.Environment.Exit(0);
                        }
                    }
                }
                //i++;
                //if (i % 4 == 0)
                //{
                //i = i - 1;
                //lock (list)
                //{
                //    list.Remove(list[0]);
                //}
                //speed++;
                //}
                thr.Abort();
            }
        }
    
        public class Bird
        {
            public int x;
            public int y;
            public PictureBox image;
            Thread thr;
            int health = r.Next(500);

            public Bird(PictureBox I)
            {
                x = 700;
                y = 50;
                image = I;
                thr = new Thread(BirdThread);
                thr.IsBackground = true;
                thr.Start();
            }

            void BirdThread()
            {
                while (true)
                {
                    if (curHeight > 500)
                        curHeight = -50;
                    else if (curHeight < -50)
                        curHeight = 500;
                    else
                    {
                        Thread.Sleep(10);
                        curHeight++;
                    }
                }
            }
        }
        
        public void ShowImageOn(PictureBox p, int x, int y)
        {
            Invoke((MethodInvoker)(() => { p.Left = x; p.Top = y; }));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (time % (r.Next(70) + 1) == 0)
            {
                PictureBox newBottle = new PictureBox();
                newBottle.Height = 40; newBottle.Width = 100;
                newBottle.BackgroundImage = bottleList[r.Next(5)];
                newBottle.BackgroundImageLayout = ImageLayout.Stretch;
                newBottle.BackColor = Color.Transparent;
                Invoke((MethodInvoker)(() => { Controls.Add(newBottle); }));
                list.Add(new Bottle(r.Next(500), newBottle));
            }

            //Graphics g = Graphics.FromImage(BackGround.BackgroundImage);
            //g.DrawImage(Properties.Resources._56cdae63c14b6153137135d0, 0, 0, 1200, 700);
            for (int j = 0; j < list.Count; j++)
                ShowImageOn(list[j].image, list[j].x, list[j].y);
            ShowImageOn(mainBird.image, 700, curHeight);

            label2.Text = n.ToString();
            time++;

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            curHeight -= 15;
        }

        public Form1()
        {
            InitializeComponent();
        }

        //public void Bird()
        //{
        //    while (true)
        //    {
        //        if (curHeight > 500)
        //            curHeight = 0;
        //        else if (curHeight < -50)
        //            curHeight = 500;
        //        else
        //        {
        //            Thread.Sleep(10);
        //            curHeight++;
        //        }
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = r.Next(500);
            PictureBox birdImage = new PictureBox();
            birdImage.Height = 100; birdImage.Width = 100;
            birdImage.BackgroundImage = Properties.Resources._128__1_;
            birdImage.BackgroundImageLayout = ImageLayout.Stretch;
            birdImage.BackColor = Color.Transparent;
            Invoke((MethodInvoker)(() => { Controls.Add(birdImage); }));
            mainBird = new Bird(birdImage);
            timer1.Start();
            button1.Enabled = false;
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    curHeight -= 15;
        //}
    }
}
