using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class CreatePath
{   
    /// <summary>
    /// Первоначальная настройка генератора пути
    /// Метод поиска - поиск в ширину
    /// </summary>
    /// <param name="pictureBox">PictureBox, на который всё будет отображаться</param>
    /// <param name="RectSZ"> Размер клетки </param>
    /// <param name="graph"> Готовый граф длая решения </param>
    public CreatePath(PictureBox pictureBox,int RectSZ,Dictionary<Point, List<Point>> graph)
    {
        this.pictureBox = pictureBox;
        this.graph = graph;
        this.RectSZ = RectSZ;

        RectX = pictureBox.Width / RectSZ;
        RectY = pictureBox.Height / RectSZ;

        goal = new Point(RectX - 1, RectY - 1);
        g = Graphics.FromImage(pictureBox.Image);

        queue.Enqueue(start);
        visited[start] = start;
        curNode = start;
    }

    private Graphics g;

    private Dictionary<Point, List<Point>> graph = new Dictionary<Point, List<Point>>();
    private Dictionary<Point, Point> visited = new Dictionary<Point, Point>();

    private Queue<Point> queue = new Queue<Point>();

    private Point start = new Point(0, 0);
    private Point curNode;
    private Point goal;
    private Point LastPoint;

    private int RectSZ;
    private int RectX;
    private int RectY;

    private PictureBox pictureBox;

    public void FindPath()
    {
        while (queue.Count > 0)
        {
            curNode = queue.Dequeue();
            List<Point> Next = graph[curNode];

            foreach (var item in Next)
            {
                if (!visited.ContainsKey(item))
                {
                    queue.Enqueue(item);
                    visited[item] = curNode;
                }
            }
            if (curNode == goal)
            {
                break;
            }
        }
        DrawFull();
    }

    private int SizeLine = 1;
    private void DrawFull()
    {
        LastPoint = this.curNode;
        while (this.curNode != start)
        {
            DrawLine();
            LastPoint = curNode;
            curNode = visited[curNode];
        }
        DrawLine();

        pictureBox.Invalidate();
    }
    private void DrawLine()
    {
        g.DrawLine(new Pen(Color.White, SizeLine),
                LastPoint.X * RectSZ + RectSZ / 2, LastPoint.Y * RectSZ + RectSZ / 2,
                curNode.X * RectSZ + RectSZ / 2, curNode.Y * RectSZ + RectSZ / 2);
    }
}