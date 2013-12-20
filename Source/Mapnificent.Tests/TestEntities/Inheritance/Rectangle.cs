﻿using System;

namespace KoC.Mapnificent.Tests.TestEntities.Inheritance
{
    public class Rectangle : Shape
    {
        public double Length { get; set; }
        public double Breadth { get; set; }

        public override int Weird
        {
            get { return 9; }
        }
    }
}