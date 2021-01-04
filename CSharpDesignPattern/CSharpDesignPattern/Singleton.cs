#define Singleton
#if Singleton
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSharpDesignPattern
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    //// no point...
    //public class SingletonDatabase : IDatabase
    //{
    //    private Dictionary<string, int> capitals;
    //    public SingletonDatabase()
    //    {
    //        Console.WriteLine("Initializing Database");
    //        List<string> lines = File.ReadAllLines("capitals.txt").ToList();
    //        foreach(var line in lines.Select((content, i) => ( content, i )))
    //        {
    //            if (line.i % 2 != 0)
    //                capitals.Add(line.content, int.Parse(lines[line.i + 1]));
    //        }
    //    }

    //    public int GetPopulation(string name)
    //    {
    //        return capitals[name];
    //    }
    //}



    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();
        private static int InstanceCount;
        public static int Count => InstanceCount;

        // singleton!
        private SingletonDatabase()
        {
            InstanceCount++;
            Console.WriteLine("Initializing Database");
            List<string> lines = File.ReadAllLines("capitals.txt").ToList();
            foreach (var line in lines.Select((content, i) => (content, i)))
            {
                if (line.i % 2 == 0)
                    capitals.Add(line.content, int.Parse(lines[line.i + 1]));
            }
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        // singleton!
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    class Singleton
    {
        static void Main(string[] args)
        {
            // // no point...
            //var db = new SingletonDatabase();

            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine(db.GetPopulation(city));

        }
    }
}

#endif
