using System;

namespace Advent.Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExerciseAttribute : Attribute
    {
        public ExerciseAttribute() : base() { }
    }
}
