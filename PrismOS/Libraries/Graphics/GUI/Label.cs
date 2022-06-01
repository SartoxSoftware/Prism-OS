﻿namespace PrismOS.Libraries.Graphics.GUI
{
    public class Label : Element
    {
        public Canvas.Font Font = Canvas.Font.Default;
        public string Text;
        public bool Center;

        public override void Update(Canvas Canvas, Window Parent)
        {
            Canvas.DrawString(Parent.X + X + (Width / 2), Parent.Y + Y + (Height / 2), Text, Font, Theme.Foreground, Center);
        }
    }
}