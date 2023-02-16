using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private int RectSZ = 10;

    private GenirateLabirint labirint;

    private void Form1_Load(object sender, EventArgs e)
    {
        pictureBox1.Image = new Bitmap(Width, Height);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        groupBox.Location = new Point(-400, -400);
        groupBox.Enabled = false;

        var x = int.TryParse(XPos.Text, out int X) ? Limit(X, Width / RectSZ) : 0;
        var y = int.TryParse(YPos.Text, out int Y) ? Limit(Y, Height / RectSZ) : 0;

        RectSZ = int.TryParse(Size.Text, out int sz) ? sz : 10;

        labirint = new GenirateLabirint(pictureBox1, timer1, new Point(x, y), RectSZ);

        Start();
    }
    private int Limit(int number, int MaxDiapazon)
    {
        return number < 0 ? 0 : number >= MaxDiapazon ? MaxDiapazon - 1 : number;
    }
    private void Start()
    {
        if (FastGenerate.Checked)
        {
            labirint.GenirateFull();
            CreatePath();
        }
        else
        {
            timer1.Start();
        }
    }
    private void Timer1_Tick(object sender, EventArgs e)
    {
        labirint.GenirateOnTick();
        if (!timer1.Enabled)
        {
            CreatePath();
        }
    }
    private void CreatePath()
    {
        CreatePath path = new CreatePath(pictureBox1, RectSZ, labirint.graph);
        path.FindPath();
    }
}
    