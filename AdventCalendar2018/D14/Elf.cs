using System.Collections.Generic;

namespace AdventCalendar2018.D14
{
    public class Elf
    {
        public char CurrentRecipe => Node.Value;

        public int RecipeValue => CurrentRecipe - 48;

        public int NextRecipe => 1 + RecipeValue;

        public LinkedListNode<char> Node { get; set; }
    }
}
