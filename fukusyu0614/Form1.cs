using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fukusyu0614
{
    public partial class Form1 : Form
    {

        int getCount;

        // count = 定数
        const int itemCount = 2;

        enum SCENE
        {
            TITLE,
            GAME,
            GAMEOVER,
            CLEAR,
            NONE
        }
        /// <summary>
        /// 現在のシーン
        /// </summary>
        SCENE nowScene;

        /// <summary>
        /// 切り替えたいシーン
        /// </summary>
        SCENE nextScene;
        int [] vx = new int[itemCount];
        int [] vy = new int[itemCount];

        Label[] labels = new Label[itemCount];
        //準備しただけ


        private static Random rand =new Random();


        int time = 100 * 5;


        public Form1()
        {
            InitializeComponent();

            nextScene = SCENE.TITLE;
            nowScene = SCENE.NONE;


            for (int i = 0; i < itemCount; i++)
            {
                //用意しなければ使えない
                labels[i] = new Label();
                labels[i].AutoSize = true;
                labels[i].Text = "('ω')";
                Controls.Add(labels[i]);
                labels[i].Font = label1.Font;
                labels[i].ForeColor = label1.ForeColor;
                labels[i].Left = rand.Next(ClientSize.Width - label1.Width);
                labels[i].Top = rand.Next(ClientSize.Height - label1.Height);

                vx[i] = rand.Next(-5, 6);
                vy[i] = rand.Next(-5, 6);
            }

            label1.Left = rand.Next(ClientSize.Width - label1.Width);
            label1.Top = rand.Next(ClientSize.Height - label1.Height);
           
            
            
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void initProc()
        {
            //nextSceneはNONEだったら、何もしない
            if (nextScene == SCENE.NONE) return;

            nowScene = nextScene;
            nextScene = SCENE.NONE;

            switch(nowScene)
            {
                case SCENE.TITLE:
                    label4.Visible = true;
                    break;
                
                case SCENE.GAME:
                    label4.Visible = false;
                    button1.Visible = false;
                    getCount = itemCount;
                    break;
                

            }
        }

        void updateProc()
        {
            if(nowScene==SCENE.GAME)
            {
                updateGame();
            }
        }

        void updateGame()
        {
           
            for (int i = 0; i < itemCount; i++)
            {
                labels[i].Left += vx[i];
                labels[i].Top += vy[i];

                label1.Left += vx[0];
                label1.Top += vy[0];

                if (labels[i].Left < 0)
                {
                    vx[i] = Math.Abs(vx[i]);
                }
                if (labels[i].Right > ClientSize.Width - labels[i].Width)
                {
                    vx[i] = -Math.Abs(vx[i]);
                }
                if (labels[i].Top < 0)
                {
                    vy[i] = Math.Abs(vy[i]);
                }
                if (labels[i].Bottom > ClientSize.Height - labels[i].Height)
                {
                    vy[i] = -Math.Abs(vy[i]);
                }


                if (label1.Left < 0)
                {
                    vx[0] = Math.Abs(vx[0]);
                }
                if (label1.Right > ClientSize.Width - label1.Width)
                {
                    vx[0] = -Math.Abs(vx[0]);
                }
                if (label1.Top < 0)
                {
                    vy[0] = Math.Abs(vy[0]);
                }
                if (label1.Bottom > ClientSize.Height - label1.Height)
                {
                    vy[0] = -Math.Abs(vy[0]);
                }



                Point mp = PointToClient(MousePosition);

                if ((mp.X >= labels[i].Left)
                    && (mp.X < labels[i].Right)
                    && (mp.Y >= labels[i].Top)
                    && (mp.Y < labels[i].Bottom)
                   )
                {
                    //timer1.Enabled = false;
                    labels[i].Visible = false;
                    labels[i].Enabled = false;
                    label3.Enabled = false;
                }

                if ((mp.X >= label1.Left)
                    && (mp.X < label1.Right)
                    && (mp.Y >= label1.Top)
                    && (mp.Y < label1.Bottom)
                   )
                {
                    timer1.Enabled = false;
                    label1.Visible = false;
                    label1.Enabled = false;
                }
            }
            time--;
            label2.Text = "Time: " + time;

            getCount--;
            if (getCount <= 0)
            {
                nextScene = SCENE.CLEAR;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            initProc();
            updateProc();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nextScene = SCENE.GAME;
        }
    }
}
