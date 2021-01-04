//#define LSP
#if LSP

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDesignPattern
{
    public class Rectangle
    {
        //public int Width { get; set; }
        //public int Height { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)} : {Height}";
        }
    }

    public class Square : Rectangle
    {
        //public new int Width
        //{
        //    set
        //    {
        //        base.Width = base.Height = value;
        //    }
        //}

        //public new int Height
        //{
        //    set
        //    {
        //        base.Height = base.Width = value;
        //    }
        //}


        public override int Width
        {
            set
            {
                base.Width = base.Height = value;
            }
        }

        public override int Height
        {
            set
            {
                base.Height = base.Width = value;
            }
        }
    }


    class SOLID_LiskovSubstitution
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] arg)
        {
            Rectangle rc = new Rectangle(2,3);

            Console.WriteLine($"{rc} has area {Area(rc)}");

            Square sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");


            Rectangle sq2 = new Square();
            sq2.Width = 4;
            Console.WriteLine($"{sq2} has area {Area(sq2)}");


        }
    }
}


#endif
