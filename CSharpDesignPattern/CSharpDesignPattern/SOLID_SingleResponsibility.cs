//using System;
//using System.Collections.Generic;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;

//namespace CSharpDesignPattern
//{
//    class Journal
//    {
//        private readonly List<string> entries = new List<string>();
//        private static int count = 0;
//        public int AddEntry(string text)
//        {
//            entries.Add($"{++count} : {text}");
//            return count; // memento pattern
//        }
//        public void RemoveEntry(int index)
//        {
//            entries.RemoveAt(index);
//        }

//        public override string ToString()
//        {
//            return string.Join(Environment.NewLine, entries);
//        }


//        //wrong
//        //public void Save(string filename)
//        //{
//        //    File.WriteAllText(filename, ToString());
//        //}

//        //public static Journal Load(string filename)
//        //{

//        //}

//        //public void Load(Uri uri)
//        //{

//        //}
//    }


//    class Persistence<T>
//    {
//        public void SaveToFile(T j, string filename, bool overwrite = false)
//        {
//            if (overwrite || !File.Exists(filename))
//                File.WriteAllText(filename, j.ToString());
//        }
//    }




//    class SOLID_SingleResponsibility
//    {
//        static void Main(string[] args)
//        {
//            var j = new Journal();
//            j.AddEntry("I cried today");
//            j.AddEntry("I ate a bug");

//            Console.WriteLine(j.ToString());
//        }
//    }
//}
