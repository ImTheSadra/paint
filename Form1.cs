
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace paint;

public partial class Form1 : Form
{
    Point oldPos;
    Color color;
    Bitmap bitmap;
    public Form1()
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.ClientSize = new(800, 600);
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        this.ResizeRedraw = true;

        this.bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

        this.Text = "Pint";

        Button btn = new();
        btn.Text = "select a color";
        btn.Height = 50;
        btn.Width = 100;
        btn.Location = new(10, 10);
        btn.Click += this.colorSelections;
        this.Controls.Add(btn);

        Button save = new();
        save.Text = "save";
        save.Height = 50;
        save.Width = 100;
        save.Location = new(btn.Location.X*2 + btn.Width, btn.Location.Y);
        save.Click += this.saveTo;

        this.Controls.Add(save);

        System.Windows.Forms.Timer timer = new();
        timer.Interval = 2;
        timer.Tick += this.loop;
        timer.Start();

        this.oldPos = this.getMousePos();
    }

    private void saveTo(object? sender, EventArgs e)
    {
        SaveFileDialog dialog = new();
        dialog.Filter = "PNG Image|*.png";
        if (dialog.ShowDialog() == DialogResult.OK){
            Graphics g = this.CreateGraphics();
            this.bitmap = new(this.Width, this.Height, g);
            this.bitmap.Save(dialog.FileName, ImageFormat.Png);
            MessageBox.Show("Image saved successfully.");
        }
    }

    private void colorSelections(object? sender, EventArgs e)
    {
        ColorDialog dialog = new();

        if (dialog.ShowDialog() == DialogResult.OK){
            this.color = dialog.Color;
        }
    }

    private Point getMousePos(){
        Point result = Cursor.Position;

        result.X -= this.Location.X;
        result.Y = (result.Y - this.Location.Y) - (this.Height - this.ClientSize.Height); // (this.Location.Y - (this.Height - this.ClientSize.Height));

        return result;
    }

    private void loop(object? sender, EventArgs e)
    {
        Graphics g = this.CreateGraphics();
        Point pos = this.getMousePos();
        if ((Control.MouseButtons & MouseButtons.Left) == MouseButtons.Left)
        {
            SolidBrush brush = new SolidBrush(this.color);
            Pen pen = new Pen(brush, 4);
            g.DrawLine(pen, this.oldPos, pos);
        }
        this.oldPos = pos;
    }
}