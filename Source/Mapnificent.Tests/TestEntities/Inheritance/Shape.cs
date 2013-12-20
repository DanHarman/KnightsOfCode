using System;

namespace KoC.Mapnificent.Tests.TestEntities.Inheritance
{
    public class Shape
    {
        public string Name { get; set; }
        public int Id;

        public virtual int Weird 
        {
            get { return 10; }
            private set {Console.WriteLine("HAI");}
        }
    }
}