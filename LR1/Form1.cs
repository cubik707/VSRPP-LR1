using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LR1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen p = new Pen(Color.Bisque, 5);
        SolidBrush brush = new SolidBrush(Color.Red);
        List<Rectangle> shapes = new List<Rectangle>(); // Список для хранения квадратов
        List<Rectangle> undoneShapes = new List<Rectangle>(); // Список для хранения удаленных квадратов
        int startX, startY, endX, endY;
        bool isAltPressed = false;
        Bitmap texture;

        public Form1()
        {
            InitializeComponent();
            texture = new Bitmap("D:\\univer\\5 sem\\VSRPP\\LR1\\img.jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            foreach (var rect in shapes)
            {
                g.DrawRectangle(p, rect);
                TextureBrush textureBrush = new TextureBrush(texture);
                g.FillRectangle(textureBrush, rect);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startX = e.X;
                startY = e.Y;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                endX = e.X;
                endY = e.Y;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int width = Math.Abs(endX - startX);
                int height = Math.Abs(endY - startY);
                int x = Math.Min(startX, endX);
                int y = Math.Min(startY, endY);

                Rectangle rect = new Rectangle(x, y, width, height);
                shapes.Add(rect);
                Invalidate(); // Перерисовать форму
            }
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Alt)
            {
                isAltPressed = true;
                p.Color = Color.Green; // Изменить цвет пера на зеленый
            }
        }

        private void Form1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Alt)
            {
                isAltPressed = false;
                p.Color = Color.Bisque; // Изменить цвет пера на исходный
            }
        }

        private void undoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (shapes.Count > 0)
            {
                Rectangle lastShape = shapes[shapes.Count - 1];
                shapes.RemoveAt(shapes.Count - 1);
                undoneShapes.Add(lastShape);
                Invalidate(); // Перерисовать форму
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoneShapes.Count > 0)
            {
                Rectangle lastUndoneShape = undoneShapes[undoneShapes.Count - 1];
                undoneShapes.RemoveAt(undoneShapes.Count - 1);
                shapes.Add(lastUndoneShape);
                Invalidate(); // Перерисовать форму
            }
        }
    }
  }
