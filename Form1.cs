using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows; 

namespace Grafopostroitel_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ant.InitializeContexts();
        }

        static double R_V = 20.0;
        public struct Indextype
        {
            public double x, y;
            public int i;
            public double r,g,b;// color
            public string s;
        };
        public struct Node { 
            public int next, w, fin;
            public double r, g, b;
            public string s;
        }
        static int N = 0;
        static int Qend = 0, Qbegin = 0, speed = 300;
        const int Nmax = 30;
        static Indextype[] index = new Indextype[Nmax];
        static Node[] A = new Node[Nmax * 4];
        const int R = 20;
        public struct Queue
        {
            public double r, g, b;
            public int i1, i2;
            public int W;
            public string s;
            public static void AddToQueue(int from, int to, double r, double g, double b)
            {
                //ребро
                queue[Qend].r = r;
                queue[Qend].g = g;
                queue[Qend].b = b;
                queue[Qend].i1 = from;
                queue[Qend].i2 = to;
                queue[Qend].W = -1;
                Qend++;
            }
            public static void AddToQueue(int v, double r, double g, double b, int plusInt)
            {
                //вершина з надписом
                queue[Qend].r = r;
                queue[Qend].g = g;
                queue[Qend].b = b;
                queue[Qend].i1 = v;
                queue[Qend].i2 = -1;
                queue[Qend].W = plusInt;
                Qend++;
            }
            public static void AddToQueue(int v, double r, double g, double b)
            {
                //вершина
                queue[Qend].r = r;
                queue[Qend].g = g;
                queue[Qend].b = b;
                queue[Qend].i1 = v;
                queue[Qend].i2 = -1;
                queue[Qend].W = -1;
                Qend++;
            }
            public static void AddToQueue(int from, int to, int weight, double r, double g, double b)
            {
                //ребро з надписом
                queue[Qend].r = r;
                queue[Qend].g = g;
                queue[Qend].b = b;
                queue[Qend].i1 = from;
                queue[Qend].i2 = to;
                queue[Qend].W = weight;
                Qend++;
            }
            public static void AddToQueue(string s)
            {
                //ребро з надписом
                queue[Qend].r = -1;
                queue[Qend].g = -1;
                queue[Qend].b = -1;
                queue[Qend].i1 = -1;
                queue[Qend].i2 = -1;
                queue[Qend].W = -1;
                Qend++;
            }
        }
        static Queue[] queue = new Queue[Nmax * Nmax];
        static int flag = -1, NewR = -1, DelR = -1, aLast = 1, O = 0;
        static double R_sc = 12;

        static int last = 0;
        public struct List
        {
            public int start, fin, w;
        };
        List[] list;
        int[,] d;

        static void small_circle(double x, double y, double r, double g, double b)
        {
            Gl.glColor3d(r, g, b);

            for (double i = 0; i < 2 * Math.PI; i += 0.01f)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(x, y);
                Gl.glVertex2d(x + R_sc * Math.Cos(i), y + R_sc * Math.Sin(i));
                Gl.glEnd();
            }
        }
        static void circle(double x, double y, double r, double g, double b)
        {
            Gl.glColor3d(r, g, b);

            for (double i = 0; i < 2 * Math.PI; i += 0.01f)
            {
                Gl.glBegin(Gl.GL_LINES);
                Gl.glVertex2d(x, y);
                Gl.glVertex2d(x + R_V * Math.Cos(i), y + R_V * Math.Sin(i));
                Gl.glEnd();
            }
        }
       

       
        public double multiplier(double a) // кількість цифер у числі)
        {
            double Eps = 0.01;
            double counter = 0;
            a = a + Eps / 1000000;
            if (a < 0)
            {
                counter++;
                a = Math.Abs(a);
            }
            if (a - ((int)a) > Eps) counter++;
            int temp = (int)a;

            do
            {
                counter++;
                temp = temp / 10;
            } while (temp > 0);
            a = a % 1 + Eps / 10000;
            while (a > Eps)
            {
                a = a * 10;
                a = a - ((int)a);
                counter++;
            }
            return counter;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // инициализация Glut 
            Glut.glutInit();
            //Glut.glutInitDisplayMode(1);
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);

            // очистка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, ant.Width, ant.Height);

            //постійна перемальовка))?!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!---------
            // Glut.glutIdleFunc(Mal);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будет сконфигурированный настройки проекции 
            Glu.gluOrtho2D(0.0, ant.Width, 0.0, ant.Height);
            Gl.glLineWidth(3);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Weight.Checked = true;
            if (Weight.Checked) numericW.Visible = true;



        }

        private void but_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void but_GO_Click(object sender, EventArgs e)
        {
            // очищаем буфер цвета 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            // очищаем текущую матрицу 
            Gl.glLoadIdentity();
            // устанавливаем текущий цвет - красный 
            Gl.glColor3f(255, 0, 0);
            // дожидаемся конца визуализации кадра 
            Gl.glFlush();

            // посылаем сигнал перерисовки элемента AnT. 
            ant.Invalidate();
        }

        private void ant_MouseMove(object sender, MouseEventArgs e)
        {
            double X = e.X, Y = ant.Height - e.Y;
            Status_XY.Text = X.ToString() + ' ' + Y.ToString();
            if (e.Button == MouseButtons.Left && flag != -1)
            {
               // Status_click.Text = "i click";
                NewR = -1;
                DelR = -1;
                //InfoStatus.Text = "";
                int flag1 = -1;
                for (int i = 0; i < N && flag1 < 0; i++)
                {
                    if (((index[i].x - X) * (index[i].x - X) + (index[i].y - Y) *
                            (index[i].y - Y) <= 4 * R * R) && flag != i)
                        flag1 = i;
                }
                if (!(X < R || Y < R || X > ant.Width - R || Y > ant.Height))
                    if (flag1 >= 0)
                    {
                        double a = Math.Sqrt((index[flag1].x - X) * (index[flag1].x - X) +
                                        (index[flag1].y - Y) * (index[flag1].y - Y));
                        if (a != 0)
                        {
                            double tX = index[flag1].x + 2 * R * (X - index[flag1].x) / a,
                                tY = index[flag1].y + 2 * R * (Y - index[flag1].y) / a;

                            int flag2 = -1;
                            for (int i = 0; i < N && flag2 < 0; i++)
                            {
                                if
                                    (((index[i].x - tX) * (index[i].x - tX) +
                                        (index[i].y - tY) * (index[i].y - tY)
                                        <= 4 * R * R) && flag != i && flag1 != i)
                                    flag2 = i;
                            }
                            if (flag2 < 0)
                            {
                                index[flag].x = tX;
                                index[flag].y = tY;
                            }
                        }

                    }
                    else
                    {
                        index[flag].x = X;
                        index[flag].y = Y;
                    }
                Mal();
            }
        }

        private void онулитиГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            N = 0;
            aLast = 0;
            Array.Clear(A, 0, 4 * Nmax - 1);
            Array.Clear(index, 0, Nmax - 1);
            Array.Clear(list, 0, 4 * Nmax - 1);
            Mal();
        }

        private void орієнтованийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Orient.Checked)
            {
                O = aLast;
                Orient.Checked = true;
            }
            else
                if (aLast != O)
                    InfoStatus.Text = "Помилка, є орієнтовані ребра.";
                else
                    Orient.Checked = false;
            Mal();

        }

        private void Weight_Click(object sender, EventArgs e)
        {
            if (!Weight.Checked)
            {
                int i = 0, temp;
                for (i = 0; i < N && i >= 0; i++)
                {
                    temp = index[i].i;
                    while (temp != 0)
                        if (A[temp].w == 0) { i = -2; temp = 0; }
                        else temp = A[temp].next;
                }

                if (i == N)
                {
                    Weight.Checked = true;
                    numericW.Visible = true;
                }
                else InfoStatus.Text = "Помилка, є ребра з нульовою вагою.";
            }
            else
            {
                numericW.Visible = false;
                Weight.Checked = false;
            }
            Mal();
        }
        //------------------------------------------------------------------------------------
        public void Mal()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();

            double X1, X2, Y1, Y2;
            int i, temp;
            
            for (i = 0; i < N; i++)
            {
                temp = index[i].i;
                while (temp != 0)
                {
                    X1 = index[i].x;
                    Y1 = index[i].y;
                    X2 = index[A[temp].fin].x;
                    Y2 = index[A[temp].fin].y;
                    Gl.glColor3d(A[temp].r, A[temp].g, A[temp].b);
                    Gl.glBegin(Gl.GL_LINES);
                    Gl.glVertex2d(X1, Y1);
                    Gl.glVertex2d(X2, Y2);
                    Gl.glEnd();

                    if (Weight.Checked)
                    {
                        for (double qq = multiplier(A[temp].w); qq > 0; qq -= 0.5)
                            small_circle((X1 + X2) / 2 + qq * R_sc / 3, (Y1 + Y2) / 2, A[temp].r, A[temp].g, A[temp].b);
                     //   small_circle((X1 + X2) / 2, (Y1 + Y2) / 2, A[temp].r, A[temp].g, A[temp].b);
                        Gl.glColor3f(1, 1, 0);
                        Gl.glRasterPos2d((X1 + X2) / 2 - 5, (Y1 + Y2) / 2 - 5);
                        Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, A[temp].w.ToString());


                        if (A[temp].s != ":)")
                        {

                            Gl.glColor3d(0.1, 0.1, 0);
                            Gl.glRasterPos2d((X1 + X2) / 2 + 5, (Y1 + Y2) / 2 + 5);
                            Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, A[temp].s);
                        }
                    }

                    temp = A[temp].next;
                }
            }
            if (Orient.Checked)
            {
                for (i = 0; i < N; i++)
                {
                    temp = index[i].i;
                    while (temp != 0)
                    {
                        X1 = index[i].x;
                        Y1 = index[i].y;
                        X2 = index[A[temp].fin].x;
                        Y2 = index[A[temp].fin].y;
                        Gl.glLineWidth(6);
                        Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(X2 - (X2 - X1) / 4, Y2 - (Y2 - Y1) / 4);
                        Gl.glVertex2d(X2, Y2);
                        Gl.glEnd();
                        temp = A[temp].next;
                    }
                }
                Gl.glLineWidth(3);
            }

            for (i = 0; i < N; i++)
            {
                circle(index[i].x, index[i].y, index[i].r,index[i].g,index[i].b);
                Gl.glColor3f(1, 1, 1);
                Gl.glRasterPos2d(index[i].x - 5 + 1, index[i].y - 5 + 3);
                Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, (i + 1).ToString());

                if (index[i].s != ":)")
                {
                    for (double qq = 0; qq <= index[i].s.Length;qq+=0.5 )
                        small_circle(index[i].x + R_V + qq * R_sc / 2, index[i].y + R_V, index[i].r, index[i].g, index[i].b);

                    Gl.glColor3f(1, 1, 0);
                    Gl.glRasterPos2d(index[i].x+R_V-5, index[i].y +R_V-5);
                    Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, '+'+index[i].s);
                }
            }
            Gl.glFlush();
            ant.Invalidate();
        }
        private void перемалюватиГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mal();
        }

        private void ant_MouseDown(object sender, MouseEventArgs e)
        {
            double X = e.X, Y = ant.Height - e.Y;

            flag = -1;
            for (int i = 0; i < N && flag < 0; i++)
            {
                if ((index[i].x - X) * (index[i].x - X) + (index[i].y - Y) *
                    (index[i].y - Y) <= 4 * R * R)
                    flag = i;
            }
            if (flag < 0)
            {
                if (X > R_V && Y > R_V && X < ant.Width - R_V && e.Y > R_V)
                {
                    index[N].x = X;
                    index[N].y = Y;
                    index[N].i = 0;
                    index[N].r = 0;
                    index[N].g = 1;
                    index[N].b = 1;
                    index[N].s = ":)";
                    N++;
                    InfoStatus.Text = "Додано вершину " + N.ToString() + ".";
                }
                NewR = -1; DelR = -1;
            }
            else
            {

                if (Control.ModifierKeys == Keys.Control)
                {
                    NewR = -1;
                    int temp, father = 0;
                    if (DelR == -1)
                    {
                        DelR = flag;
                        InfoStatus.Text = "Виталити ребро з вершини " + (DelR + 1).ToString() +
                            " до вершини...";
                        Status_click.Text = "DelR" + DelR.ToString();
                    }
                    else
                    {
                        Status_click.Text = "WTF" + DelR.ToString() + ' ' + flag.ToString();

                        InfoStatus.Text = "Ребро за вершини " + (DelR + 1).ToString()
                                             + " у вершину " + (flag + 1).ToString() + " видалено.";
                        temp = index[DelR].i;
                        if (A[index[DelR].i].fin == flag)
                        {
                            index[DelR].i = A[index[DelR].i].next;
                        }
                        else
                        {
                            while (temp > 0)
                                if (A[temp].fin == flag)
                                {
                                    A[father].next = A[temp].next;
                                    temp = -1;
                                }
                                else { father = temp; temp = A[temp].next; }
                            if (temp != -1) InfoStatus.Text = "Даного ребра не існує!";
                        }
                        if (!Orient.Checked)
                        {
                            temp = DelR; DelR = flag; flag = temp;
                            temp = index[DelR].i;
                            if (A[index[DelR].i].fin == flag)
                            {
                                index[DelR].i = A[index[DelR].i].next;
                            }
                            else
                            {
                                while (temp > 0)
                                    if (A[temp].fin == flag)
                                    {
                                        A[father].next = A[temp].next;
                                        temp = -1;
                                    }
                                    else { father = temp; temp = A[temp].next; }
                                if (temp != -1) InfoStatus.Text = "Даного ребра не існує!";
                            }
                        }
                        DelR = -1;
                    }
                }
                else
                {
                    Status_click.Text = "NewR";
                    DelR = -1;
                    if (NewR == -1)
                    {
                        NewR = flag;

                        InfoStatus.Text = "Ребро з вершини " + (NewR + 1).ToString() + " у вершину...";
                        if (Weight.Checked)
                        {
                            InfoStatus.Text = InfoStatus.Text +
                                "  (з вагою:" + numericW.Value.ToString() + ")";
                        }
                    }
                    else if (NewR != flag)
                        if (Weight.Checked && numericW.Value == 0)
                        {
                            InfoStatus.Text = "Введіть вагу ребра!!!";
                            //Sleep(500);
                            /*  InfoStatus.Text = "Ребро з вершини " + (NewR + 1).ToString() + " у вершину..."
                                                  + "  (з вагою:" + EditW.Text + ")";*/
                        }
                        else
                        {
                            if (!Orient.Checked)
                            {
                                A[aLast].next = index[flag].i;
                                index[flag].i = aLast;

                                A[aLast].fin = NewR;
                                A[aLast].w = Convert.ToInt32(numericW.Value);
                                A[aLast].r = 0;
                                A[aLast].g = 0;
                                A[aLast].b = 0;
                                A[aLast].s = ":)";
                                aLast++;
                            }
                            A[aLast].next = index[NewR].i;
                            index[NewR].i = aLast;
                            A[aLast].r = 0;
                            A[aLast].g = 0;
                            A[aLast].b = 0;
                            A[aLast].fin = flag;
                            A[aLast].w = Convert.ToInt32(numericW.Value);
                            A[aLast].s = ":)";
                            aLast++;

                            InfoStatus.Text = "Ребро за вершини " + (NewR + 1).ToString()
                                + " у вершину " + (flag + 1).ToString() + " додано";
                            if (Weight.Checked)
                            {
                                InfoStatus.Text = InfoStatus.Text + " з вагою " + numericW.Value.ToString();
                            }
                            InfoStatus.Text = InfoStatus.Text + ".";
                            NewR = -1;
                        }
                }
            }
            Mal();
        }

        private void ant_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double X = e.X, Y = ant.Height - e.Y;
            int flaag = -1;
            int i;
            NewR = -1;
            DelR = -1;
            Status_click.Text = "double click";
            InfoStatus.Text = "";
            for (i = 0; i < N && flaag < 0; i++)
            {
                if (((index[i].x - X) * (index[i].x - X) + (index[i].y - Y) *
                        (index[i].y - Y) <= 4 * R * R))
                    flaag = i;
            }
            if (flaag != -1)
            {
                for (i = flaag; i < N; i++)
                {
                    index[i].i = index[i + 1].i;
                    index[i].x = index[i + 1].x;
                    index[i].y = index[i + 1].y;
                }
                N--;
                int temp = flaag, father = 0;
                for (i = 0; i < N; i++)
                {
                    temp = index[i].i;
                    if (A[index[i].i].fin == flaag)
                    {
                        index[i].i = A[index[i].i].next;

                    }
                    else
                        while (temp != 0)
                        {
                            if (A[temp].fin == flaag)
                            {
                                A[father].next = A[temp].next;
                                A[temp].fin = 0;
                                A[temp].w = 0;
                                A[temp].next = 0;
                                temp = 0;
                            }
                            else
                            {
                                father = temp;
                                temp = A[temp].next;
                            }
                        }
                }
                for (i = 0; i < aLast; i++)
                {
                    if (A[i].fin > flaag)
                        A[i].fin--;
                }
                InfoStatus.Text = "Вершину " + (flaag + 1).ToString() + " видалено.";
            }
            Mal();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            ant.Height = this.Height - 6 - 33 - 20 - 20;
            ant.Width = this.Width - 6 - 10;
            numericW.Top = 0;
            numericW.Left = this.Width - 117;

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, ant.Width, ant.Height);

            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будет сконфигурированный настройки проекции 
            Glu.gluOrtho2D(0.0, ant.Width, 0.0, ant.Height);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Mal();

        }

        private void TimerVisual_Tick(object sender, EventArgs e)
        {
            if (queue[Qbegin].i1 >=0 && queue[Qbegin].i2 >=0 )
            {
                int temp = index[queue[Qbegin].i1].i;
                while (temp != 0 && A[temp].fin != queue[Qbegin].i2)
                    temp = A[temp].next;
                if (temp != 0)
                {
                    A[temp].r = queue[Qbegin].r;
                    A[temp].g = queue[Qbegin].g;
                    A[temp].b = queue[Qbegin].b;
                    if (queue[Qbegin].W >= 0)
                        A[temp].s = queue[Qbegin].W.ToString();
                }
                temp = index[queue[Qbegin].i2].i;
                while (temp != 0 && A[temp].fin != queue[Qbegin].i1)
                    temp = A[temp].next;
                if (temp != 0)
                {
                    A[temp].r = queue[Qbegin].r;
                    A[temp].g = queue[Qbegin].g;
                    A[temp].b = queue[Qbegin].b;
                    if (queue[Qbegin].W >= 0)
                        A[temp].s = queue[Qbegin].W.ToString();
                }

            }
            else if (queue[Qbegin].i1 >= 0)
            {
                index[queue[Qbegin].i1].r = queue[Qbegin].r;
                index[queue[Qbegin].i1].g = queue[Qbegin].g;
                index[queue[Qbegin].i1].b = queue[Qbegin].b;
                if (queue[Qbegin].W >= 0)
                    index[queue[Qbegin].i1].s = queue[Qbegin].W.ToString();
            }
            if (queue[Qbegin].i1 < 0)
            {
                for (int i = 0; i < N; i++)
                {
                    index[i].r = 0;
                    index[i].g = 1;
                    index[i].b = 1;
                    index[i].s = ":)";
                }
                for (int i = 0; i < aLast; i++)
                {
                    A[i].r = 0;
                    A[i].g = 0;
                    A[i].b = 0;
                    A[i].s = ":)";
                }
            }
            Qbegin++;
            Mal();

            if (Qbegin == Qend)
            {
                if (index[N - 1].x == 10 && index[N - 1].y == 10) N--;
                for (int i = 0; i < N; i++)
                {
                    index[i].r = 0;
                    index[i].g = 1;
                    index[i].b = 1;
                    index[i].s = ":)";
                }
                for (int i = 0; i < aLast; i++)
                {
                    A[i].r = 0;
                    A[i].g = 0;
                    A[i].b = 0;
                    A[i].s = ":)";
                }
                TimerVisual.Enabled = false;
                TimerVisual.Interval = 10;
                Qbegin = 0;
                Qend = 0;
            }
            else
                TimerVisual.Interval = speed;
        }

        private void DFS_Click(object sender, EventArgs e)
        {
            int[] stack = new int[Nmax];
            bool[] used = new bool[Nmax];
            bool flag = false;
            int top = 1, temp;
            stack[0] = 0;
            used[0] = true;
            Queue.AddToQueue(0,0,0,1);

            while (top > 0)
            {
                flag = false;
                temp = index[stack[top - 1]].i;
                while (temp != 0 && !flag)
                {
                    if (!used[A[temp].fin])
                    {
                        stack[top] = A[temp].fin;
                        used[A[temp].fin] = true;
                        flag = true;
                    }
                    else
                        temp = A[temp].next;
                }

                if (flag)
                {
                    Queue.AddToQueue(stack[top],0,0,1);
                    top++;

                }
                else
                {
                    top--;
                    Queue.AddToQueue(stack[top], 0.5, 0.5, 1);

                }
            }
            TimerVisual.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"C:\Users\Павло\Desktop");
            N = Convert.ToInt32(sr.ReadLine());
            Orient.Checked = Convert.ToBoolean(sr.ReadLine());
            Weight.Checked = Convert.ToBoolean(sr.ReadLine());

            for (int p = 0; p < N; p++)
            {
                int[] m = sr.ReadLine().Split(new char[] { ' ' },
              StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                index[p].i = m[0];
                index[p].x = m[1];
                index[p].y = m[2];
            }

            aLast = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < aLast; i++)
            {
                int[] m = sr.ReadLine().Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                A[i].fin = m[0];
                A[i].next = m[1];
                A[i].w = m[2];
            }

            sr.Close();
            Mal();
        }

        private void BFS_Click(object sender, EventArgs e)
        {
            int[] q = new int[Nmax];
            bool[] used = new bool[Nmax];
            int head = 1, tail = 0, temp;
            q[0] = 0;
            used[0] = true;

            Queue.AddToQueue(0,0,0,1);

            while (head > tail)
            {
                temp = index[q[tail]].i;
                Queue.AddToQueue(q[tail], 0, 0, 1);
                tail++;
                while (temp != 0)
                {
                    if (!used[A[temp].fin])
                    {
                        q[head] = A[temp].fin;
                        used[A[temp].fin] = true;


                        Queue.AddToQueue(q[head], 0.5, 0.5, 1);
                        head++;
                    }
                    temp = A[temp].next;
                }
            }
            TimerVisual.Enabled = true;
        }

        private void дужеПовильноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 1500;
        }

        private void швидкоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 100;
        }

        private void нормальноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 500;
        }

        private void повільноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 850;
        }

        static void swap1(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        void indexToList()
        {
            list = new List[aLast * 2+1];
            int temp = 0, j;
            last = 0;
            for (int i = 0; i < N; i++)
            {
                temp = index[i].i;
                while (temp != 0)
                {
                    list[last].start = i;
                    list[last].fin = A[temp].fin;
                    list[last].w = A[temp].w;

                    for (j = last - 1; j >= 0 && list[j].w > list[j + 1].w; j--)
                    {
                        swap1(ref list[j].w, ref list[j + 1].w);
                        swap1(ref list[j].start, ref list[j + 1].start);
                        swap1(ref list[j].fin, ref list[j + 1].fin);
                    }

                    last++;
                    temp = A[temp].next;
                }
            }
        }

        public void indexToMatrix(int diagonalElement)
        {
            d = new int[N, N];
            int i,temp;
            for (i = 0; i < N; i++)
            {
                d[i, i] = diagonalElement;
                temp = index[i].i;
                while (temp > 0)
                {
                    d[i, A[temp].fin] = A[temp].w;
                    temp = A[temp].next;
                }
            }
        }

        public struct colors
        {
            public double r, g, b;
        }

        private void алгоритмКрускалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            indexToList();
            int[] color = new int[Nmax];

            int L = 0, k;
            int[,] ans = new int[Nmax, 2];
            bool flag;
            for (int i = 0; i < N; i++)
                color[i] = i;
            for (int i = 0; i < last && L + 1 < N; i++)
            {
                if (color[list[i].start] == color[list[i].fin])
                {
                    flag = true;
                    for (k = 0; k < L && flag; k++)
                    {
                        if (ans[k, 0] == list[i].start)
                            if (ans[k, 1] == list[i].fin) flag = false;
                        if (ans[k, 1] == list[i].start)
                            if (ans[k, 0] == list[i].fin) flag = false;
                    }
                    if (flag && L != 0)
                    {

                        Queue.AddToQueue(list[i].start,list[i].fin, 1, 0, 0.0);
                    }
                }
                else
                {
                    int temp = color[list[i].fin];
                    for (k = 0; k < N; k++)
                    {
                        if (color[k] == temp) color[k] = color[list[i].start];
                    }
                    ans[L, 0] = list[i].start;
                    ans[L, 1] = list[i].fin;
                    L++;
                    Queue.AddToQueue(list[i].start, list[i].fin, 0, 1, 0.0);

                }
            }
            TimerVisual.Enabled = true;
        }

        public int comp(List a, List b)
        {
            return a.w - b.w;
        }

        public class ReverseComparer : IComparer<List>
        {
            public int Compare(List x, List y)
            {
                return x.w - y.w;
            }
        }

        private void примаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            indexToList();
            int i, v = 1;
            bool[] used = new bool[Nmax];
            bool flag = true;

            ReverseComparer rc = new ReverseComparer();
            Array.Sort(list, 0, last, rc);
            used[0] = true;
            Queue.AddToQueue(0, 0.5, 0.5, 0.9);
           

            while (v < N && v < 20)
            {
                i = 0;
                flag = true;
                while (flag && last > i)
                    if (used[list[i].start] && !used[list[i].fin]) flag = false;
                    else i++;
                if (!flag)
                {
                    used[list[i].fin] = true;
                    v++;
                    Queue.AddToQueue(list[i].start, list[i].fin, 0, 1, 0.0);
                    Queue.AddToQueue(list[i].fin, 0.0, 1, 0.0);
                }
                else status.Text = "problems!";


            }
            TimerVisual.Enabled = true;
        }

        void vDeixtra(bool visual,int start,ref int[] len)
        {
            bool[] used = new bool[Nmax];
            int temp, i, v, j;
            for (i = 0; i < N; i++)
                len[i] = int.MaxValue;
            len[start] = 0;
            for (i = 0; i < N; i++)
            {
                v = -1;
                for (j = 0; j < N; j++)
                    if (!used[j] && (v == -1 || len[v] > len[j]))
                        v = j;
                if (v == -1 || len[v] == int.MaxValue)
                    break;
                used[v] = true;

                temp = index[v].i;
                while (temp > 0)
                {
                    if (len[A[temp].fin] > len[v] + A[temp].w)
                    {
                        len[A[temp].fin] = len[v] + A[temp].w;
                        if (visual)
                        {//візуалізація деікстри
                            Queue.AddToQueue(A[temp].fin, 0.5, 0.5, 0.9, len[A[temp].fin]);
                        }
                    }
                    temp = A[temp].next;
                }
            }
        }

        private void алгоритмДеікстриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] len = new int[Nmax];
            vDeixtra(true,0,ref len);
            TimerVisual.Enabled = true;
        }

        bool Ford_Belman(bool visual,int start,ref int[] d)
        {
            int i, temp, j;
            for (i = 1; i < N; i++)
                d[i] = int.MaxValue;
            d[start] = 0;
            bool circle = false;

            for (i = 0; i < N; i++)
            {
                circle = false;
                for (j = 0; j < N; j++)
                {
                    temp = index[j].i;
                    while (temp > 0)
                    {
                        if (d[j] + A[temp].w < d[A[temp].fin])
                        {
                            d[A[temp].fin] = d[j] + A[temp].w;
                            if (visual)
                            {
                                Queue.AddToQueue(A[temp].fin, 0.0, 1, 1, d[A[temp].fin]);
                                Queue.AddToQueue(A[temp].fin, j,1,0,1.0);

                            }
                            circle = true;
                        }
                        temp = A[temp].next;
                    }
                }
            }
            if(!visual)
            {   Qbegin = 0;
                Qend = 0;
            }
            else
            for (i = 0; i < N; i++)
            {
                Queue.AddToQueue(i, 0.5, 0.5, 0.9, d[i]);
            }
            if (visual) TimerVisual.Enabled = true;
            return circle;
         
        }

        private void FordBelman_Click(object sender, EventArgs e)
        {
            int[] d = new int[Nmax];
            Ford_Belman(true,0,ref d);
        }

        private void зберегтиГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter s = new StreamWriter(saveFileDialog.FileName);

            s.WriteLine(N);
            s.WriteLine(Orient.Checked);
            s.WriteLine(Weight.Checked);

            for (int i = 0; i < N; i++)
            {
                s.WriteLine(index[i].i + " " + index[i].x + " " + index[i].y + " " + index[i].r + " " + index[i].g + " " + index[i].b);
            }
            s.WriteLine(aLast);

            for (int i = 0; i < aLast; i++)
            {
                s.WriteLine(A[i].fin + " " + A[i].next + " " + A[i].w + " " + A[i].r + " " + A[i].g + " " + A[i].b);
            }

            s.Close();
            Mal();
        }

        private void відкритиЗбереженийГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader sr = new StreamReader(openFileDialog.FileName);

            N = Convert.ToInt32(sr.ReadLine());
            Orient.Checked = Convert.ToBoolean(sr.ReadLine());
            Weight.Checked = Convert.ToBoolean(sr.ReadLine());

            for (int p = 0; p < N; p++)
            {
                int[] m = sr.ReadLine().Split(new char[] { ' ' },
              StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                index[p].i = m[0];
                index[p].x = m[1];
                index[p].y = m[2];
                index[p].r = m[3];
                index[p].g = m[4];
                index[p].b = m[5];
                index[p].s = ":)";
            }

            aLast = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < aLast; i++)
            {
                int[] m = sr.ReadLine().Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                A[i].fin = m[0];
                A[i].next = m[1];
                A[i].w = m[2];
                A[i].r = m[3];
                A[i].g = m[4];
                A[i].b = m[5];
                A[i].s = ":)";
            }

            sr.Close();
            Mal();
        }

        private void FloidUorshal_Click(object sender, EventArgs e)
        {
            int i, j, k, temp;
            int[,] W = new int[Nmax,Nmax];

            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                    W[i, j] = int.MaxValue / 2 - 50;

            for (i = 0; i < N; i++)
            {
                W[i, i] = 0;
                temp = index[i].i;
                while (temp > 0)
                {
                    W[i, A[temp].fin] = A[temp].w;
                    temp = A[temp].next;
                }
            }
            k = 0;
            for (k = 0; k < N; k++)
                for (i = 0; i < N; i++)
                    for (j = 0; j < N; j++)
                        if (W[i, j] > W[i, k] + W[k, j])
                            W[i, j] = W[i, k] + W[k, j];
            string show_string = "  ";
            for (i = 0; i < N; i++)
            {
                show_string += ((i + 1).ToString() + " ");
            }
            show_string += "\n";
            for (i = 0; i < N; i++)
            {
                show_string += ((i + 1).ToString() + " ");
                for (j = 0; j < N; j++)
                {
                     show_string += W[i, j].ToString() + " ";
                }
                show_string += "\n";
            }
            MessageBox.Show(show_string, "Відповідь (Флойда-Уоршала)", MessageBoxButtons.OK);

            StreamWriter s = new StreamWriter("Floid_Uorshal_answer.txt");
            s.Write("  ");
            for (i = 0; i < N; i++)
            {
                s.Write((i + 1) + " ");
            }
            s.WriteLine("");
            for (i = 0; i < N; i++)
            {
                s.Write((i + 1) + " ");
                for (j = 0; j < N; j++)
                {
                    s.Write(W[i, j].ToString() + " ");
                }
                s.WriteLine("");
            }
            s.Close();

        }

        private void алгоритмФордаФалкерсонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            indexToList();
            int i = 0;
            int[,] c = new int[Nmax, Nmax];
            int[,] E = new int[Nmax, Nmax];
            int[,] f = new int[Nmax, Nmax];
            int[] q = new int[Nmax];
            int[] ne = new int[Nmax];
            int[] p = new int[Nmax];
            int[] flowq = new int[Nmax];
            int head, tail, u, v, ans = 0;
            for (i = 0; i < last; i++)
            {
                if (c[list[i].start, list[i].fin] == 0)
                {
                    ne[list[i].start]++; E[list[i].start, ne[list[i].start]] = list[i].fin;
                    ne[list[i].fin]++; E[list[i].fin, ne[list[i].fin]] = list[i].start;
                }
                c[list[i].start, list[i].fin] = list[i].w;
            }
            while (true)
            {

                p[N - 1] = -1;
                Array.Clear(flowq, 0, Nmax);
                flowq[0] = int.MaxValue;
                head = 0; tail = 1; q[0] = 0;
                while (head < tail)
                {
                    u = q[head]; head++;
                    for (i = 1; i <= ne[u]; i++)
                    {
                        v = E[u, i];
                        if ((c[u, v] - f[u, v] > 0) && (flowq[v] == 0))
                        {
                            q[tail] = v; tail++;
                            p[v] = u;
                            if (c[u, v] - f[u, v] < flowq[u])
                                flowq[v] = c[u, v] - f[u, v];
                            else flowq[v] = flowq[u];
                            if (v == N - 1) break;
                        }
                    }
                }
                if (p[N - 1] == -1) break;
                u = N - 1;
                while (u != 0)
                {
                    f[p[u], u] = f[p[u], u] + flowq[N - 1];

                    Queue.AddToQueue(p[u], u, f[p[u], u],1,0,1);
                    /*
                    queue[Qend].W = -c[p[u], u];
                    queue[Qend].i1 = p[u];
                    queue[Qend].i2 = u;
                    queue[Qend].s = "+" + (f[p[u], u]).ToString();*/

                    u = p[u];
                }
                ans = ans + flowq[N - 1];
                Queue.AddToQueue(N - 1, 0.0, 1, 1, ans);
            }
            Queue.AddToQueue(N - 1, 1.0, 0, 0, ans);
            Status_click.Text = "---" + ans.ToString() + "---";
            MessageBox.Show(ans.ToString(), "Відповідь: Форда-Фалкерсона", MessageBoxButtons.OK);
            TimerVisual.Enabled = true;

        }

        private void алгоритмЕдмонсаКарпаToolStripMenuItem_Click(object sender, EventArgs e)
        {

            indexToList();
            int i = 0;
            int[,] c = new int[Nmax, Nmax];
            int[,] E = new int[Nmax, Nmax];
            int[,] f = new int[Nmax, Nmax];
            int[] q = new int[Nmax];
            int[] ne = new int[Nmax];
            int[] p = new int[Nmax];
            int[] flowq = new int[Nmax];
            int head, tail, u, v, ans = 0;
            for (i = 0; i < last; i++)
            {
                if (c[list[i].start, list[i].fin] == 0)
                {
                    ne[list[i].start]++; E[list[i].start, ne[list[i].start]] = list[i].fin;
                    ne[list[i].fin]++; E[list[i].fin, ne[list[i].fin]] = list[i].start;
                }
                c[list[i].start, list[i].fin] = list[i].w;
            }
            while (true)
            {

                p[N - 1] = -1;
                Array.Clear(flowq, 0, Nmax);
                flowq[0] = int.MaxValue;
                head = 0; tail = 1; q[0] = 0;
                while (head < tail)
                {
                    u = q[head]; head++;
                    for (i = 1; i <= ne[u]; i++)
                    {
                        v = E[u, i];
                        if ((c[u, v] - f[u, v] > 0) && (flowq[v] == 0))
                        {
                            q[tail] = v; tail++;
                            p[v] = u;
                            if (c[u, v] - f[u, v] < flowq[u])
                                flowq[v] = c[u, v] - f[u, v];
                            else flowq[v] = flowq[u];
                            if (v == N - 1) break;
                        }
                    }
                }
                if (p[N - 1] == -1) break;
                u = N - 1;
                while (u != 0)
                {
                    f[p[u], u] = f[p[u], u] + flowq[N - 1];
                    Queue.AddToQueue(p[u], u, f[p[u], u], 1, 0, 1);

                    u = p[u];
                }
                ans = ans + flowq[N - 1];
                Queue.AddToQueue(N - 1, 0.0, 1, 1, ans);
            }
            Status_click.Text = "---" + ans.ToString() + "---";
            Queue.AddToQueue(N - 1, 1.0, 0, 0, ans);
            MessageBox.Show(ans.ToString(), "Відповідь: Едмондса-Карпа", MessageBoxButtons.OK);
            TimerVisual.Enabled = true;
        }

        private void алгоритмДжонсонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] h = new int[Nmax];

            index[N].x = 10;
            index[N].y = 10;
            index[N].r = 0;
            index[N].g = 1;
            index[N].b = 1;
            index[N].s = ":)";
            int aLast_temp = aLast,i,temp;
            for (i = 0; i < N; i++ )
            {
                A[aLast].fin = i;
                A[aLast].next = aLast-1;
                A[aLast].s = ":)";
                A[aLast].r = 0.1;
                A[aLast].g = 0;
                A[aLast].b = 0;
                A[aLast].w = 0;
                aLast++;
            }
            A[aLast_temp].next = 0;
            index[N].i = aLast - 1;
            N++;
            Mal();
            Orient.Checked = true;
            speed = 300;
            Queue.AddToQueue(N - 1, 0.5, 0.5, 0.9);
            if (Ford_Belman(true, N - 1, ref h))
            {
                aLast = aLast_temp;
                N--;
                speed = 0;
                MessageBox.Show("граф має від'ємні цикли\n");
            }

            for(i=0; i < N; i++)
            {
                temp = index[i].i;
                while(temp>0)
                {
                    Queue.AddToQueue(A[temp].fin, i, (h[i] - h[A[temp].fin]), 0.1, 0.1, 0.1);
                    
                    A[temp].w = A[temp].w + h[i] - h[A[temp].fin];
                    temp = A[temp].next;
                }
            }

            int[] len = new int[Nmax];
            vDeixtra(true,0,ref len);
            for (i = 0; i < N-1; i++)
            {
                len[i] = len[i] - h[0] + h[i];
                Queue.AddToQueue(i, 0.0, 0, 0.9, len[i]);
            }
            TimerVisual.Enabled = true;
            MessageBox.Show(len[N - 2].ToString(), "ans", MessageBoxButtons.OK);

            for (i = 0; i < N; i++)
            {
                temp = index[i].i;
                while (temp > 0)
                {
                    A[temp].w = A[temp].w - h[i] + h[A[temp].fin];
                    temp = A[temp].next;
                }
            }
            aLast = aLast_temp;
           // N--;
        }

        bool get(int nmb, int x)
        { return (x & (1 << nmb)) != 0; }

        private void Komivoyaje_pereborMasok()
        {
            int inf = int.MaxValue / 2, NMAX = N;
            int n, i, j, k, m, temp, ans;
            int[,] t = new int[1 << NMAX, NMAX];
            n = N;
            int[] prev = new int[N + 1];
            int[] prevv = new int[N + 1];

            indexToMatrix(int.MaxValue/2);

            t[1, 0] = 0;
            m = 1 << n;
            for (i = 1; i < m; i += 2)
            {
                for (int qq = 0; qq <= n; qq++)
                    if (get(qq, i))
                    {
                        Queue.AddToQueue(qq, 0.5, 0, 0);
                    }
                for (j = (i == 1) ? 1 : 0; j < n; ++j)
                {

                    t[i, j] = inf;
                    if (j > 0 && get(j, i))
                    {
                        Queue.AddToQueue(j, 0, 0.5, 0);
                        temp = i ^ (1 << j);
                        for (k = 0; k < n; ++k)
                            if (get(k, i) && d[k, j] > 0)
                                if (t[i, j] >= t[temp, k] + d[k, j])
                                {
                                    prevv[j] = prev[j];
                                    prev[j] = k;
                                    t[i, j] = t[temp, k] + d[k, j];
                                 
                                    Queue.AddToQueue(k, j, 0, 1, 0.0);
                                }
                                else
                                {
                                    Queue.AddToQueue(k, j, 1, 0, 0.0);
                                }
                    }
                }
                Queue.AddToQueue("clear");
            }
            string s = " ";
            for (i = 0; i < n; i++)
            {
                s += prevv[i].ToString() + " ";
            }
            for (j = 1, ans = inf; j < n; ++j)
                if (d[j, 0] > 0) ans = Math.Min(ans, t[m - 1, j] + d[j, 0]);
            if (ans == inf) MessageBox.Show("Шлях не знайдено.");
            else MessageBox.Show(ans.ToString() + '\n' + s);
            TimerVisual.Enabled = true;
        }

        #region simulated anneling
        // Calculate the acceptance probability
        private static double acceptanceProbability(double delta, double temperature)
        {
            // If the new solution is worse, calculate an acceptance probability
            return Math.Exp((delta) / temperature);
        }
        private void createRandomPath(ref int[] mas)
        {
            int[] tempMas = new int[N];
            for (int i = 0; i < N; i++)
            {
                tempMas[i] = i;
            }
            tempMas = tempMas.OrderBy(n => Guid.NewGuid()).ToArray();
            for (int i = 0; i < N; i++)
            {
                mas[i] = tempMas[i];
            }
            mas[N] = mas[0];
        }
        private string SimulatedAnnealing(double temperature = 10, double coolingRate = 0.003)
        {

            int temp, i, sum = 0, randomStep = 0;
            string s = "";
            int[] Path = new int[N + 1];
            int[,] d = new int[N, N];
            Random random = new Random();

            for (i = 0; i < N; i++)
            {
                d[i, i] = 0;
                temp = index[i].i;
                while (temp > 0)
                {
                    d[i, A[temp].fin] = A[temp].w;
                    temp = A[temp].next;
                }
            }

            createRandomPath(ref Path);
            s = "З шляху:\n" + (Path[0] + 1).ToString();
            for (i = 1; i <= N; i++)
            {
                s += "->" + (Path[i] + 1).ToString();
            }
            for (i = 0; i < N; i++)
            {
                sum += d[Path[i], Path[i + 1]];
            }
            s += '\n' + "вартість шляху: " + sum.ToString();

            while (temperature > 1)
            {
                //выбираем два случайных города
                //первый и последний индексы не трогаем
                int p1 = 0, p2 = 0;
                while (p1 == p2)
                {
                    p1 = random.Next(1, Path.Length - 1);
                    p2 = random.Next(1, Path.Length - 1);
                }
                //проверка расстояний
                double sum1 = d[Path[p1 - 1], Path[p1]] + d[Path[p1], Path[p1 + 1]] +
                              d[Path[p2 - 1], Path[p2]] + d[Path[p2], Path[p2 + 1]];
                double sum2 = d[Path[p1 - 1], Path[p2]] + d[Path[p2], Path[p1 + 1]] +
                              d[Path[p2 - 1], Path[p1]] + d[Path[p1], Path[p2 + 1]];
                if (sum2 >= sum1)
                {
                    temp = Path[p1];
                    Path[p1] = Path[p2];
                    Path[p2] = temp;
                }
                else
                {
                    if (acceptanceProbability(sum1 - sum2, temperature) > random.NextDouble())
                    {
                        temp = Path[p1];
                        Path[p1] = Path[p2];
                        Path[p2] = temp;
                        randomStep++;
                    }
                }

                // Cool system
                temperature *= 1 - coolingRate;
            }

            s += "\nОтримано шлях:(rand step = " + randomStep.ToString() + ")" + '\n' + (Path[0] + 1).ToString();
            sum = 0;
            for (i = 1; i <= N; i++)
            {
                s += "->" + (Path[i] + 1).ToString();
            }
            for (i = 0; i < N; i++)
            {
                sum += d[Path[i], Path[i + 1]];
            }
            s += '\n' + "вартість шляху: " + sum.ToString();
            return s;
        }
        #endregion
 
        private void button3_Click(object sender, EventArgs e) // pause|continue
        {
            if (Qbegin > 1)
            {
                TimerVisual.Enabled = !TimerVisual.Enabled;
                if (butPause.Text == "pause")
                    butPause.Text = "continue";
                else
                    butPause.Text = "pause";
            }
        }

        private void button4_Click(object sender, EventArgs e)//stop
        {
            if (TimerVisual.Enabled)
            {
                Qbegin = Qend - 2;
                TimerVisual.Interval = 2;
            }
        }

        private string GreedyAlgorithm() //жадний алгоритм для задачі Комівояжера
        {

            int i, j, res = 0, min, tmp = 0;
            int[] ans = new int[N];
            bool[] visited = new bool[N];
            Array.Clear(visited, 0, N);
            int start = Convert.ToInt32(numericW.Value);
            indexToMatrix(int.MaxValue/2);
            visited[start] = true;
            Queue.AddToQueue(start, 0, 1, 0);
            tmp = start;
            for (i = 0; i < N - 1; i++)
            {
                min = -1;
                for (j = 0; j < N; j++)
                    if (!visited[j] && d[tmp, j] > 0)
                    {
                        if (min == -1)
                        {
                            min = j;
                            Queue.AddToQueue(tmp, j, 0, 1, 0.0);
                        }
                        else
                            if (d[tmp, j] < d[tmp, min])
                            {
                                Queue.AddToQueue(tmp, min, 1, 0, 0.0);
                                min = j;

                                Queue.AddToQueue(tmp, j, 0, 1, 0.0);
                            }
                            else
                            {
                                Queue.AddToQueue(tmp, j, 1, 0, 0.0);
                            }
                    }
                ans[res] = min;
                if (min < 0)
                {
                    return "We have problem :(";
                }
                visited[min] = true;
                tmp = min;

                Queue.AddToQueue(tmp, 0, 1, 0.0);
                res++;
            }

            Queue.AddToQueue(tmp, start, 0, 1, 0.0);
            string s = "Отримано шлях:" + '\n' + (start + 1).ToString() + "->";
            int sum = 0;
            for (i = 0; i < N - 1; i++)
            {
                s += (ans[i] + 1).ToString() + "->";
            }
            sum = d[start, ans[0]] + d[ans[N - 2], start];
            for (i = 0; i < N - 2; i++)
            {
                sum += d[ans[i], ans[i + 1]];
            }
            s += (start + 1).ToString();
            s += '\n' + "вартість шляху: " + sum.ToString();
            return s;
        }

        private void жаднийАлгоритмToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GreedyAlgorithm());
            TimerVisual.Enabled = true;
        
        }

        private void алгоритмІмітаціїВідпалуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(SimulatedAnnealing(10,0.03));
        }

        int[] genMas;
        int[] answerBruteForceSearch;
        int sumAnswerBruteForceSearch;
        private void goCheck()
        {
            int sum = 0;
            genMas[N]=genMas[0];
            for(int i=0; i < N; i++)
            {
                sum += d[genMas[i],genMas[i+1]];
            }
            if (sum < sumAnswerBruteForceSearch)
            {
                sumAnswerBruteForceSearch = sum;
                for(int i=0; i <=N;i++)
                {
                    answerBruteForceSearch[i] = genMas[i];
                }
            }
        }
        private void gen(int pos, int val)
        {
            bool flag = true;
            for(int i=0; i < pos && flag; i++)
            {
                if (genMas[i] == val) flag = false;
            }
            if (flag)
            {
                genMas[pos] = val;
                if (pos + 1 == N)
                {
                    goCheck();
                }
                else
                    for (int i = 0; i < N; i++)
                        gen(pos + 1, i);
            }
        }

        private void повнийПеребірToolStripMenuItem_Click(object sender, EventArgs e)
        {

            genMas = new int[N + 1];
            answerBruteForceSearch = new int[N + 1];
            sumAnswerBruteForceSearch = int.MaxValue;
            indexToMatrix(int.MaxValue / 2);
            for (int i = 0; i < N; i++)
            {
                gen(0, i);
            }
            string s = "Отримано шлях:" + '\n' + (answerBruteForceSearch[0] + 1).ToString();
            for (int i = 1; i <= N; i++)
            {
                s += "->" + (answerBruteForceSearch[i] + 1).ToString();
            }
            s += '\n' + "вартість шляху: " + sumAnswerBruteForceSearch.ToString();
            MessageBox.Show(s);
        }
        #region Метод віток та границь
        int[] ROUTE = new int[N];
        int PATH_WEIGHT;
        void FITSP( int S)
        {
            int END1 = 0, END2 = 0, FARTHEST = 0, I, INDEX, INSCOST, MAXDIST, NEWCOST, NEXTINDEX;
            int[] CYCLE = new int[N];
            int[] DIST = new int[N];
            for (I = 0; I < N; I++) CYCLE[I] = 0;
            CYCLE[S] = S;
            for (I = 0; I < N; I++) DIST[I] = d[S, I];
            PATH_WEIGHT = 0;
            int J = 0;
            for (I = 0; I < N- 1; I++)
            {
                MAXDIST = -int.MaxValue/2;
                for (J = 0; J < N; J++)
                    if (CYCLE[J] == 0)
                        if (DIST[J] > MAXDIST)
                        {
                            MAXDIST = DIST[J];
                            FARTHEST = J;
                        }

                INSCOST = int.MaxValue/2; INDEX = S;
                for (J = 0; J <= I; J++)
                {
                    NEXTINDEX = CYCLE[INDEX];
                    NEWCOST = d[INDEX, FARTHEST] + d[FARTHEST, NEXTINDEX] - d[INDEX, NEXTINDEX];
                    if (NEWCOST < INSCOST)
                    {


                        INSCOST = NEWCOST;
                        END1 = INDEX; END2 = NEXTINDEX;
                    }
                    INDEX = NEXTINDEX;
                }
                CYCLE[FARTHEST] = END2; CYCLE[END1] = FARTHEST;
                PATH_WEIGHT = PATH_WEIGHT + INSCOST;
                for (J = 0; J < N; J++)
                    if (CYCLE[J] == 0)
                        if (d[FARTHEST, J] < DIST[J]) DIST[J] = d[FARTHEST, J];
            }
            INDEX = S;
            for (I = 0; I < N; I++)
            {
                ROUTE[I] = INDEX; INDEX = CYCLE[INDEX];
            }
        }

        struct SWAPRECORD
        {

            public int X1, X2, Y1, Y2, Z1, Z2, GAIN;
            public bool CHOICE;
        };
        void SWAPCHECK(ref SWAPRECORD SWAP)
        {
            int DELWEIGHT, MAX;


            SWAP.GAIN = 0;
            DELWEIGHT = d[SWAP.X1, SWAP.X2] + d[SWAP.Y1, SWAP.Y2] + d[SWAP.Z1, SWAP.Z2];
            MAX = DELWEIGHT - (d[SWAP.Y1, SWAP.X1] + d[SWAP.Z1, SWAP.X2] + d[SWAP.Z2, SWAP.Y2]);
            if (MAX > SWAP.GAIN)
            {
                SWAP.GAIN = MAX; SWAP.CHOICE = false;
            }
            MAX = DELWEIGHT - (d[SWAP.X1, SWAP.Y2] + d[SWAP.Z1, SWAP.X2] + d[SWAP.Y1, SWAP.Z2]);
            if (MAX > SWAP.GAIN)
            {
                SWAP.GAIN = MAX; SWAP.CHOICE = true;
            }
        }
        
        SWAPRECORD BESTSWAP,SWAP;
        int INDEX;
        int[] PTR = new int[N];
        void REVERSE(int START, int FINISH)
        {
            int AHEAD, LAST, NEXT;
            if (START != FINISH)
            {
                LAST = START; NEXT = PTR[LAST];
                do
                {
                    AHEAD = PTR[NEXT]; PTR[NEXT] = LAST;
                    LAST = NEXT; NEXT = AHEAD;
                }
                while (LAST != FINISH);
            }
        }

        void THREEOPT()
        {
            int I, J, K;
            for (I = 0; I < N - 1; I++) { PTR[ROUTE[I]] = ROUTE[I + 1]; }
            PTR[ROUTE[N-1]] = ROUTE[0];
            do
            {
                BESTSWAP.GAIN = 0;
                SWAP.X1 = 1;
                for (I = 0; I <  N - 1; I++)
                {
                    SWAP.X2 = PTR[SWAP.X1]; SWAP.Y1 = SWAP.X2;
                    for (J = 1; J < N - 3; J++)
                    {
                        SWAP.Y2 = PTR[SWAP.Y1]; SWAP.Z1 = PTR[SWAP.Y2];
                        for (K = J + 1; K < N - 1; K++)
                        {
                            SWAP.Z2 = PTR[SWAP.Z1];
                            SWAPCHECK(ref SWAP);
                            if (SWAP.GAIN > BESTSWAP.GAIN) BESTSWAP = SWAP;
                            SWAP.Z1 = SWAP.Z2;
                        }
                        SWAP.Y1 = SWAP.Y2;
                    }
                    SWAP.X1 = SWAP.X2;
                }
                if (BESTSWAP.GAIN > 0)
                {
                    if (BESTSWAP.CHOICE == false)
                    {
                        REVERSE(BESTSWAP.Z2, BESTSWAP.X1);
                        PTR[BESTSWAP.Y1] = BESTSWAP.X1; PTR[BESTSWAP.Z2] = BESTSWAP.Y2;
                    }
                    else
                    {
                        PTR[BESTSWAP.X1] = BESTSWAP.Y2; PTR[BESTSWAP.Y1] = BESTSWAP.Z2;
                    }
                    PTR[BESTSWAP.Z1] = BESTSWAP.X2;
                }
            } while (BESTSWAP.GAIN != 0);
            INDEX = 0;
            for (I = 0; I < N - 1; I++)
            {
                ROUTE[I] = INDEX; INDEX = PTR[INDEX];
            }
        }

        void TWOOPT()
        {

            int AHEAD, I, I1, I2, INDEX, J, J1, J2, LAST, LIMIT, MAX, MAX1, NEXT, S1 = 0, S2 = 0, T1 = 0, T2 = 0;
            int[] PTR = new int[N+1];

            for (I = 0; I < N - 1; I++) { PTR[ROUTE[I]] = ROUTE[I + 1]; }
            PTR[ROUTE[N-1]] = ROUTE[0];
            do
            {
                MAX = 0; I1 = 0;
                for (I = 0; I < N - 2; I++)
                {
                    if (I == 0) LIMIT = N - 1;
                    else LIMIT = N;
                    I2 = PTR[I1]; J1 = PTR[I2];
                    for (J = I + 2; J <= LIMIT; J++)
                    {
                        J2 = PTR[J1];
                        MAX1 = d[I1, I2] + d[J1, J2] - (d[I1, J1] + d[I2, J2]);
                        if (MAX1 > MAX)
                        {

                            S1 = I1; S2 = I2;
                            T1 = J1; T2 = J2;
                            MAX = MAX1;
                        }
                        J1 = J2;
                    }
                    I1 = I2;
                }
                if (MAX > 0)
                {
                    PTR[S1] = T1;
                    NEXT = S2; LAST = T2;
                    do
                    {
                        AHEAD = PTR[NEXT]; PTR[NEXT] = LAST;
                        LAST = NEXT; NEXT = AHEAD;
                    } while (NEXT != T2);
                    PATH_WEIGHT = PATH_WEIGHT - MAX;
                }
            } while (MAX > 0);//?
            INDEX = 0;
            for (I = 0; I < N - 1; I++)
            {

                ROUTE[I] = INDEX; INDEX = PTR[INDEX];
            }
        }
        #endregion
        private void методВітокТаГраницьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            indexToMatrix(0);
            int i;
            PATH_WEIGHT = 0;
            for (i = 0; i < N; i++) ROUTE[i] = i;
            FITSP(0);
            TWOOPT();
            THREEOPT();
            string s = "";
            for (i = 0; i < N; i++)
                s += ROUTE[i].ToString() + "->";
            s += "0 = " + PATH_WEIGHT.ToString();
            MessageBox.Show(s);
        }
        //end-------------------------------------------------
    }
}
