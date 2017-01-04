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

using Tao.OpenGl;
using Tao.FreeGlut;
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
        };
        public struct Node { public int next, w, fin;}
        static int N = 0;
        static int Qend=0,Qbegin=0,speed=300;
        const int Nmax = 30;
        static Indextype[] index = new Indextype[Nmax];
        static Node[] A = new Node[Nmax * 4];
        const int R = 20;
        public struct Queue
        {
            public double r, g, b;
            public double x1,x2,y1,y2;
            public int W;
            public string s;
        }
        static Queue[] queue = new Queue[Nmax*Nmax];
        static int flag = -1,NewR = -1, DelR = -1,aLast=1,O=0;
        static double R_sc=12;

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
        static void circle(double x, double y,double r,double g, double b)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            // инициализация Glut 
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_SINGLE);

            // очистка окна 
            Gl.glClearColor(255, 255, 255, 1);

            // установка порта вывода в соответствии с размерами элемента anT 
            Gl.glViewport(0, 0, ant.Width, ant.Height);
           
            //постійна перемальовка))₴!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!---------
           // Glut.glutIdleFunc(Mal);
            
            // настройка проекции 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // теперь необходимо корректно настроить 2D ортогональную проекцию 
            // в зависимости от того, какая сторона больше 
            // мы немного варьируем то, как будет сконфигурированный настройки проекции 
            Glu.gluOrtho2D (0.0, ant.Width, 0.0, ant.Height);
            Gl.glLineWidth(3);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Weight.Checked = true;
            if (Weight.Checked) EditW.Visible = true;

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

        private void but_TEST_Click(object sender, EventArgs e)
        {
            Gl.glRasterPos2d(10,10);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, 1111.ToString());
            Gl.glFlush();
            ant.Invalidate();

        }

        private void ant_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }
        private void ant_MouseMove(object sender, MouseEventArgs e)
        {
            double X = e.X, Y = ant.Height - e.Y;
            Status_XY.Text = X.ToString() + ' ' + Y.ToString();
            if (e.Button == MouseButtons.Left && flag != -1)
            {
                Status_click.Text = "i click";
                NewR = -1;
                DelR = -1;
                InfoStatus.Text = "";
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

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void InfoStatus_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void онулитиГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            N = 0;
            aLast = 0;
            Array.Clear(A, 0, 4*Nmax - 1);
            Array.Clear(index, 0, Nmax - 1);
            Array.Clear(list, 0, 4*Nmax - 1);
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
                    EditW.Visible = true;
                }
                else InfoStatus.Text = "Помилка, є ребра з нульовою вагою.";
            }
            else
            {
                EditW.Visible = false;
                Weight.Checked = false;
            }
            Mal();
        }
//------------------------------------------------------------------------------------
        public void Mal()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glLoadIdentity();

            Gl.glColor3f(1, 0, 1);

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
                    Gl.glColor3f(0, 0, 1);
                    Gl.glBegin(Gl.GL_LINES);
                        Gl.glVertex2d(X1, Y1);
                        Gl.glVertex2d(X2, Y2);
                    Gl.glEnd();
                    
                    if (Weight.Checked)
                    {
                        small_circle((X1 + X2) / 2, (Y1 + Y2) / 2, 0, 0, 1);
                        Gl.glColor3f(1, 0, 0);
                        Gl.glRasterPos2d((X1 + X2) / 2 - 5, (Y1 + Y2) / 2 - 5);
                        Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, A[temp].w.ToString());
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
                circle(index[i].x, index[i].y,0,1,1);
                Gl.glColor3f(1,1,1);
                Gl.glRasterPos2d(index[i].x-5+1,index[i].y-5+3);
                Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, (i + 1).ToString());
            }
            //if (Qbegin < Qend)
            for (i = 0; i < Qbegin; i++)
                if (queue[i].W < 0)
                {

                    X1 = queue[i].x1; Y1 = queue[i].y1;
                    X2 = queue[i].x2; Y2 = queue[i].y2;
                    double cos = (X2 - X1) / Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2)),
                        sin = (Y2 - Y1) / Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2));
                    Gl.glColor3d(queue[i].r, queue[i].g, queue[i].b);
                    Gl.glBegin(Gl.GL_LINES);
                    Gl.glVertex2d(X1 + R_V * cos, Y1 + R_V * sin);
                    Gl.glVertex2d(X2 - R_V * cos, Y2 - R_V * sin);
                    Gl.glEnd();
                    if (Weight.Checked)
                    {
                        small_circle((X1 + X2) / 2, (Y1 + Y2) / 2,
                                      queue[i].r, queue[i].g, queue[i].b);
                        Gl.glColor3d(0,0,1);
                        Gl.glRasterPos2d((X1 + X2) / 2 - 5+1, (Y1 + Y2) / 2 - 5+3);
                        if (queue[i].s != ":)")   
                            Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15,
                                                  (-queue[i].W).ToString() + ' ' + queue[i].s);
                        else
                            Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, (-queue[i].W).ToString());
                 
                    }
                   
                    status.Text = "підсвітка ребра";
                    //Qbegin++;
                }
                else if (queue[i].W > 0)
                {
                    circle(queue[i].x1, queue[i].y1,
                        queue[i].r, queue[i].g, queue[i].b);
                    status.Text = "підсвітка вершини";
                    
                  
                   
                    if (queue[i].s != ":)")
                    {
                        small_circle(queue[i].x1 + 29, queue[i].y1,
                                     queue[i].r, queue[i].g, queue[i].b);   
                        
                        Gl.glColor3f(0, 1, 0); Gl.glRasterPos2d(queue[i].x1 - 5, queue[i].y1 - 5);
                        Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15,
                                              queue[i].W.ToString() + ' ' + queue[i].s);
                    }
                    else
                        Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15,queue[i].W.ToString());
                    
                  
                  
                    
                    //Qbegin++;
                }


            Gl.glFlush();
            ant.Invalidate();
        }
        private void перемалюватиГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mal();
        }

        private void ant_Load(object sender, EventArgs e)
        {

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
                    int temp, father=0;
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
                                "  (з вагою:" + EditW.Text + ")";
                        }
                    }
                    else if (NewR != flag)
                        if (Weight.Checked && EditW.Text == "0")
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
                                A[aLast].w = Convert.ToInt32(EditW.Text);
                                aLast++;
                            }
                            A[aLast].next = index[NewR].i;
                            index[NewR].i = aLast;

                            A[aLast].fin = flag;
                            A[aLast].w = Convert.ToInt32(EditW.Text);
                            aLast++;
                            
                            InfoStatus.Text = "Ребро за вершини " + (NewR + 1).ToString()
                                + " у вершину " + (flag + 1).ToString() + " додано";
                            if (Weight.Checked)
                            {
                                InfoStatus.Text = InfoStatus.Text + " з вагою " + EditW.Text;
                            }
                            InfoStatus.Text = InfoStatus.Text + ".";
                            NewR = -1;
                        }
                }
            }
            Mal();

            // but_TEST.Click;
            /*підсвітка
            if (DelR != -1)
            {
                Box->Canvas->Brush->Color = clSilver;
                Box->Canvas->Ellipse(index[DelR].X - R, index[DelR].Y - R,
                                 index[DelR].X + R, index[DelR].Y + R);
                Box->Canvas->TextOutA(index[DelR].X - 5, index[DelR].Y - 5, IntToStr(DelR + 1));
                Box->Canvas->Brush->Color = CDV->Color;
            }
            else
                if (NewR != -1)
                {
                    Box->Canvas->Brush->Color = clWindow;
                    Box->Canvas->Ellipse(index[NewR].X - R, index[NewR].Y - R,
                                         index[NewR].X + R, index[NewR].Y + R);
                    Box->Canvas->TextOutA(index[NewR].X - 5, index[NewR].Y - 5, IntToStr(NewR + 1));
                    Box->Canvas->Brush->Color = CDV->Color;
                }
                else
                    if (flag == -1)
                        if (!(X < R || Y < R || X > Box->ClientWidth - R || Y > Box->ClientHeight))
                        {
                            
                            Box->Canvas->Brush->Color = clRed;
                            Box->Canvas->Ellipse(index[N - 1].X - R, index[N - 1].Y - R,
                                                 index[N - 1].X + R, index[N - 1].Y + R);
                            Box->Canvas->TextOutA(index[N - 1].X - 5, index[N - 1].Y - 5, IntToStr(N));
                            Box->Canvas->Brush->Color = CDV->Color;

                        }
             */
            // click = true;
        }

        private void status_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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

            ant.Height = this.Height - 6 - 33-20-20;
            ant.Width = this.Width - 6-10;
            EditW.Top = 0;
            EditW.Left = this.Width - 117;
            
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
           /* if(queue[Qbegin].W < 0)
            {

                double X1 = queue[Qbegin].x1, Y1 = queue[Qbegin].y1,
                    X2 = queue[Qbegin].x2, Y2 = queue[Qbegin].y2;
                double cos = (X2 - X1) / Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2)),
                    sin = (Y2 - Y1) / Math.Sqrt((X1 - X2) * (X1 - X2) + (Y1 - Y2) * (Y1 - Y2));
                Gl.glColor3d(queue[Qbegin].r, queue[Qbegin].g, queue[Qbegin].b);
                Gl.glBegin(Gl.GL_LINES);
                    Gl.glVertex2d(X1 + R_V * cos, Y1 + R_V * sin);
                    Gl.glVertex2d(X2 - R_V * cos, Y2 - R_V * sin);      
                Gl.glEnd();
                Qbegin++;
            }
            else if(queue[Qbegin].W > 0)
            {
                circle(queue[Qbegin].x1,queue[Qbegin].y1,
                    queue[Qbegin].r, queue[Qbegin].g, queue[Qbegin].b);

                Gl.glColor3f(1, 1, 1);
                Gl.glRasterPos2d(queue[Qbegin].x1 - 5, queue[Qbegin].y1 - 5);
                Glut.glutBitmapString(Glut.GLUT_BITMAP_9_BY_15, (queue[Qbegin].W + 1).ToString());
             
                Qbegin++;
            }
            Gl.glFlush();
            ant.Invalidate();

           */
            Qbegin++;
            Mal(); 
           
            if (Qbegin == Qend)
            {
                TimerVisual.Enabled = false;
                TimerVisual.Interval = 10;
                Qbegin = 0;
                Qend = 0;
            }
            else
            {
                TimerVisual.Interval = speed;
            }
        }
        
        private void DFS_Click(object sender, EventArgs e)
        {
            int[] stack = new int[Nmax];
            bool[] used = new bool[Nmax];
            bool flag = false;
            int top=1,temp;
            stack[0] = 0;
            used[0] = true;
            
            queue[Qend].x1 = index[0].x;
            queue[Qend].y1 = index[0].y;
            queue[Qend].W = 1;
            queue[Qend].r = 0;
            queue[Qend].g = 0;
            queue[Qend].b = 1;
            queue[Qend].s = ":)";
            Qend++;

            while (top>0)
            {
                flag = false;
                temp = index[stack[top-1]].i;
                while (temp!=0 && !flag)
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
                    queue[Qend].x1 = index[stack[top]].x;
                    queue[Qend].y1 = index[stack[top]].y;
                    queue[Qend].W = stack[top] + 1;
                    queue[Qend].r = 0;
                    queue[Qend].g = 0;
                    queue[Qend].b = 1;
                    queue[Qend].s = ":)";
                    Qend++;
                    top++;

                }
                else
                {
                    top--;
                    queue[Qend].x1 = index[stack[top]].x;
                    queue[Qend].y1 = index[stack[top]].y;
                    queue[Qend].W = stack[top] + 1;
                    queue[Qend].r = 0.5;
                    queue[Qend].g = 0.5;
                    queue[Qend].b = 1;
                    queue[Qend].s = ":)";
                    Qend++;
                    
                }
            }
            TimerVisual.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //////



        }

        private void BFS_Click(object sender, EventArgs e)
        {
            int[] q = new int[Nmax];
            bool[] used = new bool[Nmax];
            int  head=1, tail=0,temp;
            q[0] = 0;
            used[0] = true;

            queue[Qend].x1 = index[0].x;
            queue[Qend].y1 = index[0].y;
            queue[Qend].W = 1;
            queue[Qend].r = 0;
            queue[Qend].g = 0;
            queue[Qend].b = 1;
            queue[Qend].s = ":)";
            Qend++;

            while (head > tail)
            {
                temp = index[q[tail]].i;

                queue[Qend].x1 = index[q[tail]].x;
                queue[Qend].y1 = index[q[tail]].y;
                queue[Qend].W = q[tail] + 1;
                queue[Qend].r = 0;
                queue[Qend].g = 0;
                queue[Qend].b = 1;
                queue[Qend].s = ":)";
                Qend++;  

                tail++;
                while (temp != 0)
                {
                    if (!used[A[temp].fin])
                    {
                        q[head] = A[temp].fin;
                        used[A[temp].fin] = true;
                      

                        queue[Qend].x1 = index[q[head]].x;
                        queue[Qend].y1 = index[q[head]].y;
                        queue[Qend].W = q[head] + 1;
                        queue[Qend].r = 0.5;
                        queue[Qend].g = 0.5;
                        queue[Qend].b = 1;
                        queue[Qend].s = ":)";
                        Qend++;  
                        head++;
                    }
                    temp = A[temp].next;
                }
            }
            TimerVisual.Enabled = true;
        }

        private void швидкістьВиконанняАлгоритмівToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void дужеПовильноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 1500;
        }

        private void fkujhbnvRheToolStripMenuItem_Click()
        {
        
        }

        private void швидкоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 300;
        }

        private void нормальноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 500;
        }

        private void повільноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            speed = 850;
        }
        
        static int last=0;
        public struct List 
        {
	        public int start, fin, w;
		};
        List[] list = new List[4*Nmax];
        static void swap1(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

void pereved()
{
	int temp = 0,j;
	last = 0;
	for (int i = 0; i < N; i++)
	   {
			temp =  index[i].i;
			while ( temp != 0)
			{
				list[last].start = i;
				list[last].fin = A[temp].fin;
				list[last].w = A[temp].w;

				for(j = last-1; j >=0 && list[j].w > list[j+1].w; j--)
				{
					swap1(ref list[j].w,ref list[j+1].w);
					swap1(ref list[j].start,ref list[j+1].start);
					swap1(ref list[j].fin,ref list[j+1].fin);
				}

				last++;
				temp = A[temp].next;
			}
	   }
}
        public struct colors{
            public double r,g,b;
        }
        private void алгоритмКрускалаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pereved();
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
                        queue[Qend].W = -list[i].w;
                        queue[Qend].x1 = index[list[i].start].x;
                        queue[Qend].x2 = index[list[i].fin].x;
                        queue[Qend].y1 = index[list[i].start].y ;
                        queue[Qend].y2 = index[list[i].fin].y;
                        queue[Qend].b = 0;
                        queue[Qend].g = 0;
                        queue[Qend].r = 1;
                        queue[Qend].s = ":)";
                        Qend++;
                    }
                }
                else
                {
                    int temp = color[list[i].fin];
                    for (k = 0; k < N; k++)
                    {
                        if (color[k] == temp) color[k] = color[list[i].start];
                    }
                    ans[L,0] = list[i].start;
                    ans[L,1] = list[i].fin;
                    L++;

                    queue[Qend].W = -list[i].w;
                    queue[Qend].x1 = index[list[i].start].x;
                    queue[Qend].x2 = index[list[i].fin].x;
                    queue[Qend].y1 = index[list[i].start].y;
                    queue[Qend].y2 = index[list[i].fin].y;
                    queue[Qend].b = 0;
                    queue[Qend].g = 1;
                    queue[Qend].r = 1;
                    queue[Qend].s = ":)";
                    Qend++;

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
            pereved();
            int i,v=1;
            bool[] used = new bool[Nmax];
            bool flag = true;

             ReverseComparer rc = new ReverseComparer();
            Array.Sort(list,0,last,rc);
            used[0] = true;

            queue[Qend].W = 1;
            queue[Qend].x1 = index[0].x;
            queue[Qend].y1 = index[0].y;
            queue[Qend].b = 0.5;
            queue[Qend].g = 0.5;
            queue[Qend].r = 0.9;
            queue[Qend].s = ":)";
            Qend++;

            while (v < N && v < 20)
            {
                i=0;
                flag = true;
                while (flag && last > i)
                    if (used[list[i].start] && !used[list[i].fin]) flag = false;
                    else i++;
                if (!flag)
                {
                    used[list[i].fin] = true;
                    v++;
                    queue[Qend].W = -list[i].w;
                    queue[Qend].x1 = index[list[i].start].x;
                    queue[Qend].x2 = index[list[i].fin].x;
                    queue[Qend].y1 = index[list[i].start].y;
                    queue[Qend].y2 = index[list[i].fin].y;
                    queue[Qend].b = 0;
                    queue[Qend].g = 1;
                    queue[Qend].r = 1;
                    queue[Qend].s = ":)";
                    Qend++;

                    queue[Qend].W = list[i].fin + 1;
                    queue[Qend].x1 = index[list[i].fin].x;
                    queue[Qend].y1 = index[list[i].fin].y;
                    queue[Qend].b = 0.5;
                    queue[Qend].g = 0.5;
                    queue[Qend].r = 0.9;
                    queue[Qend].s = ":)";
                    Qend++;
                }
                else status.Text = "problems!";
               

            }
            TimerVisual.Enabled = true;
        }

        private void алгоритмДеікстриToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool[] used = new bool[Nmax];
            int[] len = new int [Nmax];
            int temp,i,v,j;
            for(i=0; i < N; i++)
                len[i] = int.MaxValue;
            len[0] = 0; 
            for(i=0; i < N; i++)
            {
                v = -1;
                for(j=0; j<N; j++)
                    if(!used[j] && (v==-1 || len[v] > len[j]))
                        v = j;
                if (v == -1 || len[v] == int.MaxValue)
                    break;
                used[v] = true;

                temp = index[i].i;
                while (temp > 0)
                {
                    if (len[A[temp].fin] > len[v] + A[temp].w)
                    {
                        len[A[temp].fin] = len[v] + A[temp].w;


                        queue[Qend].W = A[temp].fin + 1;
                        queue[Qend].x1 = index[A[temp].fin].x;
                        queue[Qend].y1 = index[A[temp].fin].y;
                        queue[Qend].b = 0.5;
                        queue[Qend].g = 0.5;
                        queue[Qend].r = 0.9;
                        queue[Qend].s = "+" + len[A[temp].fin].ToString();
                        Qend++;
                    }
                    temp = A[temp].next;
                }



            }


            TimerVisual.Enabled = true;
        }

        private void FordBelman_Click(object sender, EventArgs e)
        {
            int[] d = new int[Nmax];
            int i,temp,j;
            for(i=1; i < N; i++)
                d[i] = int.MaxValue;
            d[0]=0;

            for(i=0; i < N; i++)
            {
                for (j = 0; j < N; j++)
                {
                    temp = index[j].i;
                    while (temp > 0)
                    {
                        if (d[j] + A[temp].w < d[A[temp].fin])
                        {
                            d[A[temp].fin] = d[j] + A[temp].w;
                            
                            queue[Qend].W = A[temp].fin + 1;
                            queue[Qend].x1 = index[A[temp].fin].x;
                            queue[Qend].y1 = index[A[temp].fin].y;
                            queue[Qend].b = 0;
                            queue[Qend].g = 1;
                            queue[Qend].r = 1;
                            queue[Qend].s = d[A[temp].fin].ToString();
                            Qend++;

                            queue[Qend].W = -list[i].w;
                            queue[Qend].x1 = index[A[temp].fin].x;
                            queue[Qend].x2 = index[j].x;
                            queue[Qend].y1 = index[A[temp].fin].y;
                            queue[Qend].y2 = index[j].y;
                            queue[Qend].b = 1;
                            queue[Qend].g = 0;
                            queue[Qend].r = 1;
                            queue[Qend].s = ":)";
                            Qend++;
                        }
                        temp = A[temp].next;
                    }
                }
            }
            for (i = 0; i < N; i++)
            {
                queue[Qend].W = i + 1;
                queue[Qend].x1 = index[i].x;
                queue[Qend].y1 = index[i].y;
                queue[Qend].b = 0.5;
                queue[Qend].g = 0.5;
                queue[Qend].r = 0.9;
                queue[Qend].s = d[i].ToString();
                Qend++;
            }
            TimerVisual.Enabled = true;
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
                s.WriteLine(index[i].i + " " + index[i].x + " " + index[i].y);
            }
            s.WriteLine(aLast);

            for (int i = 0; i < aLast; i++)
            {
                s.WriteLine( A[i].fin + " " + A[i].next + " " + A[i].w );
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
            string q;

            N = Convert.ToInt32( sr.ReadLine());
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

        private void FloidUorshal_Click(object sender, EventArgs e)
        {
            int i,j,k,temp;
            int[,] W = new int[4,4] ;

            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                    W[i, j] = int.MaxValue/2-50;

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
                        {
                            W[i, j] = W[i, k] + W[k, j];
                            
                            queue[Qend].W = i + 1;
                            queue[Qend].x1 = index[i].x;
                            queue[Qend].y1 = index[i].y;
                            temp = k + i + j;
                            queue[Qend].r = temp%2 - (temp%2==0?-1:1) / (temp + 3);
                            queue[Qend].g = 0;
                            queue[Qend].b = 1 / (k + 3);
                            queue[Qend].s = "+" + W[i, j].ToString();
                            Qend++;
                            queue[Qend].W = j + 1;
                            queue[Qend].x1 = index[j].x;
                            queue[Qend].y1 = index[j].y;
                            queue[Qend].r = temp % 2 - (temp % 2 == 0 ? -1 : 1) / (temp + 3);
                            queue[Qend].g = 0;
                            queue[Qend].b = 1 / (k + 3);
                            queue[Qend].s = "+" + W[i, j].ToString();
                            Qend++;
                        }
            k++;

            TimerVisual.Enabled = true;
        }
//end-------------------------------------------------
    }
}
