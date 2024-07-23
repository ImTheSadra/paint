
namespace paint;

public partial class Form1 : Form
{
    Point oldPos;
    Color color;
    public Form1()
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = true;
        this.MinimizeBox = false;

        this.ResizeRedraw = true;

        this.Text = "Pint";

        Button btn = new();
        btn.Text = "select a color";
        btn.Height = 50;
        btn.Width = 100;
        btn.Location = new(10, 10);
        btn.Click += this.colorSelections;
        this.Controls.Add(btn);

        System.Windows.Forms.Timer timer = new();
        timer.Interval = 2;
        timer.Tick += this.loop;
        timer.Start();

        this.oldPos = this.getMousePos();
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
