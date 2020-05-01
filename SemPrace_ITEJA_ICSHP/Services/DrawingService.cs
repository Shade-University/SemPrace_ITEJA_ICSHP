using LanguageLogic.AST.Statements.Functions;
using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GUI.Services
{
    public class DrawingService //Drawing service - draw to canvas via commands
    { //Really great will be interface for this service
        private Canvas canvas;

        private Point defaultPosition = new Point();
        private Point currentPosition = new Point();

        private Polygon turtle;
        private double angle;
        private PenStatus penStatus;

        private double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public DrawingService(Canvas canvas)
        {
            penStatus = PenStatus.DOWN;
            this.canvas = canvas;
            defaultPosition.X = (int)canvas.ActualWidth / 2;
            defaultPosition.Y = (int)canvas.ActualHeight / 2;

            currentPosition.X = (int)canvas.ActualWidth / 2;
            currentPosition.Y = (int)canvas.ActualHeight / 2;


            DrawTurtle();
            Angle(90);
        }

        private void DrawTurtle()
        {
            turtle = new Polygon();
            PointCollection points = new PointCollection() {
                new System.Windows.Point(0,0),
                new System.Windows.Point(0,8),
               new System.Windows.Point(-8,4),
            };
            turtle.Points = points;
            turtle.Fill = Brushes.Red;

            canvas.Children.Add(turtle);
            MoveTurtle();
        }
        private void MoveTurtle()
        {
            Canvas.SetTop(turtle, currentPosition.Y - 4);
            Canvas.SetLeft(turtle, currentPosition.X - 2);
        }
        private void RotateTurtle()
        {
            turtle.RenderTransform = new RotateTransform(angle);
        }

        public void Forward(object move)
        {
            double newPositionX = currentPosition.X - (Math.Cos(ConvertToRadians(angle)) * (double)move);
            double newPositionY = currentPosition.Y - (Math.Sin(ConvertToRadians(angle)) * (double)move);

            if (penStatus == PenStatus.DOWN)
            {
                Line line = new Line();
                line.StrokeThickness = 2;
                line.Stroke = Brushes.Red;

                line.X1 = currentPosition.X;
                line.Y1 = currentPosition.Y;
                line.X2 = newPositionX;
                line.Y2 = newPositionY;

                canvas.Children.Add(line);
            }

            currentPosition.X = (int)newPositionX;
            currentPosition.Y = (int)newPositionY;

            MoveTurtle();
        }
        public void Backward(object move)
        {

            double newPositionX = currentPosition.X + (Math.Cos(ConvertToRadians(angle)) * (double)move);
            double newPositionY = currentPosition.Y + (Math.Sin(ConvertToRadians(angle)) * (double)move);

            if (penStatus == PenStatus.DOWN)
            {
                Line line = new Line();
                line.StrokeThickness = 2;
                line.Stroke = Brushes.Red;

                line.X1 = currentPosition.X;
                line.Y1 = currentPosition.Y;
                line.X2 = newPositionX;
                line.Y2 = newPositionY;

                canvas.Children.Add(line);
            }

            currentPosition.X = (int)newPositionX;
            currentPosition.Y = (int)newPositionY;

            MoveTurtle();
        }
        public void Angle(object angle)
        {
            this.angle = Convert.ToDouble(angle);
            RotateTurtle();
        }
        public void Pen(PenStatus penStatus)
        {
            this.penStatus = penStatus;
        }
        public void Write(object text)
        {
            System.Windows.MessageBox.Show(text.ToString());
        }

    }
}
