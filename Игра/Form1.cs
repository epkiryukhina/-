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

        static int i = 0;
        static int speed = 1;
        static Thread bird;
        static int curHeight = 50;
        static Random r = new Random(0);
        static List<Bottle> list = new List<Bottle>();
        static int n;

        public class Bottle
        {
            public int x;
            public int y;
            public Image bottle;
            Thread thr;
            int wight = r.Next(150);

            public Bottle(int Y, Image I)
            {
                x = -150;
                y = Y;
                bottle = I;
                thr = new Thread(newBottleThread);
                thr.IsBackground = true;
                thr.Start();
            }

            bool intersect(int y0)
            {
                if (x + 100 <= 550)
                    return false;
                if (x >= 650)
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
                i++;
                if (i % 4 == 0)
                {
                    i = i - 4;
                    lock (list)
                    {
                        list.Remove(list[0]);
                        list.Remove(list[1]);
                        list.Remove(list[2]);
                    }
                    speed++;
                }
                thr.Abort();
            }
        }
    
        
        public void ShowImageOn(Image i, int x, int y, Graphics g, bool type)
        {
            if (type)
                g.DrawImage(i, x, y, 100, 40);
            else
                g.DrawImage(i, x, y, 100, 100);
            BackGround.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {       
            //pictureBox2.Top = curHeight;
            if ((list.Count == 0) || (list.Last().x >= 400))
                list.Add(new Bottle(r.Next(250), Properties.Resources._0_8cbe3_3871c069_L));

            Graphics g = Graphics.FromImage(BackGround.BackgroundImage);
            g.DrawImage(Properties.Resources._56cdae63c14b6153137135d0, 0, 0, 1200, 700);
            for (int j = i; j < list.Count; j++)
                ShowImageOn(list[j].bottle, list[j].x, list[j].y, g, true);
            ShowImageOn(Properties.Resources._128__1_, 550, curHeight, g, false);

            label2.Text = n.ToString();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            curHeight -= 15*speed;
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void Bird()
        {
            while (true)
            {
                if (curHeight > 500)
                    curHeight = 0;
                else if (curHeight < -50)
                    curHeight = 500;
                else
                {
                    Thread.Sleep(10);
                    curHeight += speed;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            n = r.Next(500);
            bird = new Thread(new ThreadStart(Bird));
            bird.IsBackground = true;
            bird.Start();
            timer1.Start();
        }
    }
}
