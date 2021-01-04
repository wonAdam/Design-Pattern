#define OCP
#if OCP

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpDesignPattern
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
    
        public Product(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    // wrong
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
        {
            foreach (var p in products)
                if (p.Color == color && p.Size == size)
                    yield return p;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> FilterBy(IEnumerable<T> items, ISpecification<T> spec);
    }


    public class ColorSpecification : ISpecification<Product>
    {
        Color color;
        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return this.color == t.Color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        Size size;
        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return this.size == t.Size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first;
        ISpecification<T> second;

        public AndSpecification(ISpecification<T> first,ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }
        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> FilterBy(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
                if (spec.IsSatisfied(item))
                    yield return item;
        }
    }




    class SOLID_OpenClosed
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree= new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");
            foreach(var p in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {p.Name} is green");
            }


            var bf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (var p in bf.FilterBy(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }


            var bf2 = new BetterFilter();
            Console.WriteLine("Large Blue products (new):");
            foreach (var p in bf2.FilterBy(
                products, 
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Blue), 
                    new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {p.Name} is large and blue");
            }
        }
    }
}

#endif
