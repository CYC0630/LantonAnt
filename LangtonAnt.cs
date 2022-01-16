using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.OpenGl;

namespace Ant
{
    public partial class LangtonAnt : Form
    {
        private readonly Ant ant;
        private readonly Dictionary<Coordinate, TileColor> table;
        private readonly Camera camera = new Camera();
        private bool light_open;

        public LangtonAnt()
        {
            InitializeComponent();
            OpenGLControl.InitializeContexts();
            ant = new Ant();
            table = new Dictionary<Coordinate, TileColor>();
            light_open = true;
        }

        //初始化
        private void MyInitial()
        {
            Gl.glClearColor(1.0F, 1.0F, 1.0F, 1.0F);
            Gl.glClearDepth(1.0); /* 深度緩衝器 */

            Gl.glColorMaterial(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE);

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING); /* 開啟光影計算 */
            Gl.glEnable(Gl.GL_LIGHT0); /* 全域光 */
            Gl.glEnable(Gl.GL_NORMALIZE); /* 正規化法向量*/

            float[] global_ambient = { 0.2F, 0.2F, 0.2F, 1.0F };
            float[] global_diffuse = { 0.6F, 0.6F, 0.6F, 1.0F };
            float[] global_specular = { 1.0F, 1.0F, 1.0F, 1.0F };
            float[] global_position = { 0.0F, 2.0F, 0.0F, 1.0F };
            float[] global_direction = { 0.0F, -1.0F, 0.0F };

            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE, Gl.GL_TRUE); /* 雙面打光效果 */
            Gl.glLightModeli(Gl.GL_LIGHT_MODEL_LOCAL_VIEWER, Gl.GL_TRUE);
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, global_ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT, global_ambient);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, global_diffuse);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPECULAR, global_specular);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, global_position); /* 設定光的位置 */
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_SPOT_DIRECTION, global_direction); /* 設定光的方向 */

            camera.SetPosition(0.0, 10.0, 0.0);
            camera.SetDirection(0.0, -10.0, 0.0);
        }

        private void OpenGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            //int key = (int)e.KeyCode;
            //int speed = 10;
            //if (key >= 48 && key <= 57) //0 ~ 9
            //{
            //    speed -= key - 58;
            //    TimerTick.Interval = speed * 10;
            //}

            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.NumPad0:
                    TimerTick.Interval = 100;
                    break;

                case Keys.D1:
                case Keys.NumPad1:
                    TimerTick.Interval = 90;
                    break;

                case Keys.D2:
                case Keys.NumPad2:
                    TimerTick.Interval = 80;
                    break;

                case Keys.D3:
                case Keys.NumPad3:
                    TimerTick.Interval = 70;
                    break;

                case Keys.D4:
                case Keys.NumPad4:
                    TimerTick.Interval = 60;
                    break;

                case Keys.D5:
                case Keys.NumPad5:
                    TimerTick.Interval = 50;
                    break;

                case Keys.D6:
                case Keys.NumPad6:
                    TimerTick.Interval = 40;
                    break;

                case Keys.D7:
                case Keys.NumPad7:
                    TimerTick.Interval = 30;
                    break;

                case Keys.D8:
                case Keys.NumPad8:
                    TimerTick.Interval = 20;
                    break;

                case Keys.D9:
                case Keys.NumPad9:
                    TimerTick.Interval = 10;
                    break;

                case Keys.A:
                    if (e.Control)
                        camera.HSlide(-0.1);
                    else if (e.Alt)
                        camera.Roll(1.0);
                    else
                        camera.Pan(1.0);
                    break;

                case Keys.D:
                    if (e.Shift)
                        camera.HSlide(0.1);
                    else if (e.Alt)
                        camera.Roll(-1.0);
                    else
                        camera.Pan(-1.0);
                    break;

                case Keys.E:
                    if (e.Control)
                        camera.VSlide(0.1);
                    else
                        camera.Tilt(1.0);
                    break;

                case Keys.Q:
                    if (e.Control)
                        camera.VSlide(-0.1);
                    else
                        camera.Tilt(-1.0);
                    break;

                case Keys.W:
                    camera.Slide(0.1);
                    break;

                case Keys.S:
                    camera.Slide(-0.1);
                    break;

                case Keys.L:
                    if (light_open = !light_open)
                        Gl.glEnable(Gl.GL_LIGHT0);
                    else
                        Gl.glDisable(Gl.GL_LIGHT0);
                    break;
            }

            OpenGLControl.Refresh();
        }

        private void OpenGLControl_Load(object sender, EventArgs e)
        {
            MyInitial();
            camera.SetViewVolume(45, OpenGLControl.Size.Width, OpenGLControl.Size.Height, 0.1, 100);
        }

        private void OpenGLControl_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            camera.LookAt();

            foreach (var square in table)
            {
                Coordinate coordinate = square.Key;
                byte color = (square.Value == TileColor.white) ? (byte)128 : (byte)255; //黑轉白 白轉黑

                Gl.glEnable(Gl.GL_COLOR_MATERIAL);

                Gl.glPushMatrix();
                Gl.glTranslated(coordinate.x - 0.5, 0.0, coordinate.z - 0.5);
                Face();
                Gl.glColor3ub(color, color, color);
                Gl.glPopMatrix();

                Gl.glDisable(Gl.GL_COLOR_MATERIAL);
            }
        }

        private void OpenGLControl_Resize(object sender, EventArgs e)
        {
            camera.SetViewVolume(45, OpenGLControl.Size.Width, OpenGLControl.Size.Height, 0.1, 100);
        }

        private void TimerTick_Tick(object sender, EventArgs e)
        {
            Coordinate here = new Coordinate()
            {
                x = ant.coordinate.x,
                z = ant.coordinate.z
            };

            if (table.ContainsKey(here))
            {
                if (table[here] == TileColor.white) //原本所在的位置是白色
                {
                    ant.TurnRight();
                    table[here] = TileColor.black;
                    ant.Move();
                }
                else //原本所在的位置是黑色
                {
                    ant.TurnLeft();
                    table[here] = TileColor.white;
                    ant.Move();
                }
            }
            else //位在不存在的地方 需要初始化
            {
                ant.TurnRight();
                table.Add(here, TileColor.black);
                ant.Move();
            }

            OpenGLControl.Refresh();
        }

        //private void Axes()
        //{
        //    Gl.glLineWidth(3.0F);
        //    Gl.glBegin(Gl.GL_LINES);
        //    Gl.glColor3ub(255, 0, 0);
        //    Gl.glVertex3d(0.0, 0.0, 0.0);
        //    Gl.glVertex3d(1.0, 0.0, 0.0);
        //    Gl.glColor3ub(0, 255, 0);
        //    Gl.glVertex3d(0.0, 0.0, 0.0);
        //    Gl.glVertex3d(0.0, 1.0, 0.0);
        //    Gl.glColor3ub(0, 0, 255);
        //    Gl.glVertex3d(0.0, 0.0, 0.0);
        //    Gl.glVertex3d(0.0, 0.0, 1.0);
        //    Gl.glEnd();
        //}

        private void Face(int slices = 10)
        {
            double dx = 1.0 / slices;
            double dz = 1.0 / slices;

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3d(0.0, 1.0, 0.0);
            for (double x = 0; x < 1.0; x += dx)
            {
                for (double z = 0; z < 1.0; z += dz)
                {
                    Gl.glTexCoord2d(x, z);
                    Gl.glVertex3d(x, 0.0, z);
                    Gl.glTexCoord2d(x, z + dz);
                    Gl.glVertex3d(x, 0.0, z + dz);
                    Gl.glTexCoord2d(x + dx, z + dz);
                    Gl.glVertex3d(x + dx, 0.0, z + dz);
                    Gl.glTexCoord2d(x + dx, z);
                    Gl.glVertex3d(x + dx, 0.0, z);
                }
            }
            Gl.glEnd();
        }
    }

    internal class Coordinate
    {
        internal int x;
        internal int z;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Coordinate))
                return false;

            Coordinate c = obj as Coordinate;
            return x == c.x && z == c.z;
        }

        public override int GetHashCode() => (x + 31415) ^ (z - 92653);
    }

    internal class Ant
    {
        internal Coordinate coordinate;
        internal int rotation;

        public Ant()
        {
            coordinate = new Coordinate()
            {
                x = 0,
                z = 0
            };
            rotation = 90;
        }

        public void Move()
        {
            switch (rotation)
            {
                case 0:
                    coordinate.x++;
                    break;

                case 90:
                    coordinate.z++;
                    break;

                case 180:
                    coordinate.x--;
                    break;

                case 270:
                    coordinate.z--;
                    break;
            }
        }

        public void TurnLeft()
        {
            rotation += 90;
            if (rotation >= 360)
                rotation = 0;
        }

        public void TurnRight()
        {
            rotation -= 90;
            if (rotation < 0)
                rotation = 270;
        }
    }

    internal enum TileColor
    {
        black,
        white
    }
}
