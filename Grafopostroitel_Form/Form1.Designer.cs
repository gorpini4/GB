namespace Grafopostroitel_Form
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ant = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.status = new System.Windows.Forms.StatusStrip();
            this.InfoStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_click = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_XY = new System.Windows.Forms.ToolStripStatusLabel();
            this.EditW = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.онулитиГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReDraw = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.відкритиЗбереженийГрафToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.параметриToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Orient = new System.Windows.Forms.ToolStripMenuItem();
            this.Weight = new System.Windows.Forms.ToolStripMenuItem();
            this.алгоритмиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DFS = new System.Windows.Forms.ToolStripMenuItem();
            this.BFS = new System.Windows.Forms.ToolStripMenuItem();
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.швидкоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нормальноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повільноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.дужеПовильноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.алгоритмКрускалаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.примаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Deixtra = new System.Windows.Forms.ToolStripMenuItem();
            this.FordBelman = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerVisual = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FloidUorshal = new System.Windows.Forms.ToolStripMenuItem();
            this.status.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ant
            // 
            this.ant.AccumBits = ((byte)(0));
            this.ant.AutoCheckErrors = false;
            this.ant.AutoFinish = false;
            this.ant.AutoMakeCurrent = true;
            this.ant.AutoSwapBuffers = true;
            this.ant.BackColor = System.Drawing.Color.White;
            this.ant.ColorBits = ((byte)(32));
            this.ant.DepthBits = ((byte)(16));
            this.ant.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ant.Location = new System.Drawing.Point(0, 27);
            this.ant.Name = "ant";
            this.ant.Size = new System.Drawing.Size(643, 365);
            this.ant.StencilBits = ((byte)(0));
            this.ant.TabIndex = 0;
            this.ant.Load += new System.EventHandler(this.ant_Load);
            this.ant.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ant_MouseDoubleClick);
            this.ant.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ant_MouseDown);
            this.ant.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ant_MouseMove);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InfoStatus,
            this.Status_click,
            this.Status_XY});
            this.status.Location = new System.Drawing.Point(0, 395);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(643, 22);
            this.status.TabIndex = 4;
            this.status.Text = "(C)!!!";
            this.status.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.status_ItemClicked);
            // 
            // InfoStatus
            // 
            this.InfoStatus.Name = "InfoStatus";
            this.InfoStatus.Size = new System.Drawing.Size(69, 17);
            this.InfoStatus.Text = "ІнфоСтатус";
            this.InfoStatus.Click += new System.EventHandler(this.InfoStatus_Click);
            // 
            // Status_click
            // 
            this.Status_click.Name = "Status_click";
            this.Status_click.Size = new System.Drawing.Size(68, 17);
            this.Status_click.Text = "Where click";
            this.Status_click.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // Status_XY
            // 
            this.Status_XY.Name = "Status_XY";
            this.Status_XY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Status_XY.Size = new System.Drawing.Size(45, 17);
            this.Status_XY.Text = "(QQQ);";
            this.Status_XY.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // EditW
            // 
            this.EditW.Location = new System.Drawing.Point(543, 4);
            this.EditW.Name = "EditW";
            this.EditW.Size = new System.Drawing.Size(100, 20);
            this.EditW.TabIndex = 6;
            this.EditW.Text = "1";
            this.EditW.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.параметриToolStripMenuItem,
            this.алгоритмиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(643, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.онулитиГрафToolStripMenuItem,
            this.ReDraw,
            this.зберегтиГрафToolStripMenuItem,
            this.відкритиЗбереженийГрафToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // онулитиГрафToolStripMenuItem
            // 
            this.онулитиГрафToolStripMenuItem.Name = "онулитиГрафToolStripMenuItem";
            this.онулитиГрафToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.онулитиГрафToolStripMenuItem.Text = "Онулити граф";
            this.онулитиГрафToolStripMenuItem.Click += new System.EventHandler(this.онулитиГрафToolStripMenuItem_Click);
            // 
            // ReDraw
            // 
            this.ReDraw.Name = "ReDraw";
            this.ReDraw.Size = new System.Drawing.Size(222, 22);
            this.ReDraw.Text = "Перемалювати граф";
            this.ReDraw.Click += new System.EventHandler(this.перемалюватиГрафToolStripMenuItem_Click);
            // 
            // зберегтиГрафToolStripMenuItem
            // 
            this.зберегтиГрафToolStripMenuItem.Name = "зберегтиГрафToolStripMenuItem";
            this.зберегтиГрафToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.зберегтиГрафToolStripMenuItem.Text = "Зберегти граф";
            this.зберегтиГрафToolStripMenuItem.Click += new System.EventHandler(this.зберегтиГрафToolStripMenuItem_Click);
            // 
            // відкритиЗбереженийГрафToolStripMenuItem
            // 
            this.відкритиЗбереженийГрафToolStripMenuItem.Name = "відкритиЗбереженийГрафToolStripMenuItem";
            this.відкритиЗбереженийГрафToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.відкритиЗбереженийГрафToolStripMenuItem.Text = "Відкрити збережений граф";
            this.відкритиЗбереженийГрафToolStripMenuItem.Click += new System.EventHandler(this.відкритиЗбереженийГрафToolStripMenuItem_Click);
            // 
            // параметриToolStripMenuItem
            // 
            this.параметриToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Orient,
            this.Weight});
            this.параметриToolStripMenuItem.Name = "параметриToolStripMenuItem";
            this.параметриToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.параметриToolStripMenuItem.Text = "Параметри";
            // 
            // Orient
            // 
            this.Orient.Name = "Orient";
            this.Orient.Size = new System.Drawing.Size(151, 22);
            this.Orient.Text = "Орієнтований";
            this.Orient.Click += new System.EventHandler(this.орієнтованийToolStripMenuItem_Click);
            // 
            // Weight
            // 
            this.Weight.Name = "Weight";
            this.Weight.Size = new System.Drawing.Size(151, 22);
            this.Weight.Text = "Зважений";
            this.Weight.Click += new System.EventHandler(this.Weight_Click);
            // 
            // алгоритмиToolStripMenuItem
            // 
            this.алгоритмиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DFS,
            this.BFS,
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem,
            this.алгоритмКрускалаToolStripMenuItem,
            this.примаToolStripMenuItem,
            this.Deixtra,
            this.FordBelman,
            this.FloidUorshal});
            this.алгоритмиToolStripMenuItem.Name = "алгоритмиToolStripMenuItem";
            this.алгоритмиToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.алгоритмиToolStripMenuItem.Text = "Алгоритми";
            // 
            // DFS
            // 
            this.DFS.Name = "DFS";
            this.DFS.Size = new System.Drawing.Size(257, 22);
            this.DFS.Text = "Пошук в глубину";
            this.DFS.Click += new System.EventHandler(this.DFS_Click);
            // 
            // BFS
            // 
            this.BFS.Name = "BFS";
            this.BFS.Size = new System.Drawing.Size(257, 22);
            this.BFS.Text = "Пошук в ширину";
            this.BFS.Click += new System.EventHandler(this.BFS_Click);
            // 
            // швидкістьВиконанняАлгоритмівToolStripMenuItem
            // 
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.швидкоToolStripMenuItem,
            this.нормальноToolStripMenuItem,
            this.повільноToolStripMenuItem,
            this.дужеПовильноToolStripMenuItem});
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem.Name = "швидкістьВиконанняАлгоритмівToolStripMenuItem";
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem.Text = "Швидкість виконання алгоритмів";
            this.швидкістьВиконанняАлгоритмівToolStripMenuItem.Click += new System.EventHandler(this.швидкістьВиконанняАлгоритмівToolStripMenuItem_Click);
            // 
            // швидкоToolStripMenuItem
            // 
            this.швидкоToolStripMenuItem.Name = "швидкоToolStripMenuItem";
            this.швидкоToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.швидкоToolStripMenuItem.Text = "Швидко";
            this.швидкоToolStripMenuItem.Click += new System.EventHandler(this.швидкоToolStripMenuItem_Click);
            // 
            // нормальноToolStripMenuItem
            // 
            this.нормальноToolStripMenuItem.Name = "нормальноToolStripMenuItem";
            this.нормальноToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.нормальноToolStripMenuItem.Text = "Нормально";
            this.нормальноToolStripMenuItem.Click += new System.EventHandler(this.нормальноToolStripMenuItem_Click);
            // 
            // повільноToolStripMenuItem
            // 
            this.повільноToolStripMenuItem.Name = "повільноToolStripMenuItem";
            this.повільноToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.повільноToolStripMenuItem.Text = "Повільно";
            this.повільноToolStripMenuItem.Click += new System.EventHandler(this.повільноToolStripMenuItem_Click);
            // 
            // дужеПовильноToolStripMenuItem
            // 
            this.дужеПовильноToolStripMenuItem.Name = "дужеПовильноToolStripMenuItem";
            this.дужеПовильноToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.дужеПовильноToolStripMenuItem.Text = "Дуже повільно";
            this.дужеПовильноToolStripMenuItem.Click += new System.EventHandler(this.дужеПовильноToolStripMenuItem_Click);
            // 
            // алгоритмКрускалаToolStripMenuItem
            // 
            this.алгоритмКрускалаToolStripMenuItem.Name = "алгоритмКрускалаToolStripMenuItem";
            this.алгоритмКрускалаToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.алгоритмКрускалаToolStripMenuItem.Text = "Алгоритм Крускала";
            this.алгоритмКрускалаToolStripMenuItem.Click += new System.EventHandler(this.алгоритмКрускалаToolStripMenuItem_Click);
            // 
            // примаToolStripMenuItem
            // 
            this.примаToolStripMenuItem.Name = "примаToolStripMenuItem";
            this.примаToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.примаToolStripMenuItem.Text = "Прима";
            this.примаToolStripMenuItem.Click += new System.EventHandler(this.примаToolStripMenuItem_Click);
            // 
            // Deixtra
            // 
            this.Deixtra.Name = "Deixtra";
            this.Deixtra.Size = new System.Drawing.Size(257, 22);
            this.Deixtra.Text = "Алгоритм Деікстри";
            this.Deixtra.Click += new System.EventHandler(this.алгоритмДеікстриToolStripMenuItem_Click);
            // 
            // FordBelman
            // 
            this.FordBelman.Name = "FordBelman";
            this.FordBelman.Size = new System.Drawing.Size(257, 22);
            this.FordBelman.Text = "Алгоритм Форда-Белмана";
            this.FordBelman.Click += new System.EventHandler(this.FordBelman_Click);
            // 
            // TimerVisual
            // 
            this.TimerVisual.Interval = 10;
            this.TimerVisual.Tick += new System.EventHandler(this.TimerVisual_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 83);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "sample";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "sample";
            this.saveFileDialog.Filter = "\"txt files|*.txt";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // FloidUorshal
            // 
            this.FloidUorshal.Name = "FloidUorshal";
            this.FloidUorshal.Size = new System.Drawing.Size(257, 22);
            this.FloidUorshal.Text = "Алгоритм Флойда-Воршелла";
            this.FloidUorshal.Click += new System.EventHandler(this.FloidUorshal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 417);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EditW);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ant);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl ant;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel Status_XY;
        private System.Windows.Forms.ToolStripStatusLabel Status_click;
        private System.Windows.Forms.ToolStripStatusLabel InfoStatus;
        private System.Windows.Forms.TextBox EditW;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem онулитиГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReDraw;
        private System.Windows.Forms.ToolStripMenuItem зберегтиГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem відкритиЗбереженийГрафToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem параметриToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Orient;
        private System.Windows.Forms.ToolStripMenuItem Weight;
        private System.Windows.Forms.ToolStripMenuItem алгоритмиToolStripMenuItem;
        private System.Windows.Forms.Timer TimerVisual;
        private System.Windows.Forms.ToolStripMenuItem DFS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem BFS;
        private System.Windows.Forms.ToolStripMenuItem швидкістьВиконанняАлгоритмівToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem швидкоToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нормальноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повільноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem дужеПовильноToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem алгоритмКрускалаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem примаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Deixtra;
        private System.Windows.Forms.ToolStripMenuItem FordBelman;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem FloidUorshal;
    }
}

