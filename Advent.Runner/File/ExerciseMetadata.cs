namespace Advent.Runner.File
{
    public class ExerciseMetadata
    {
        public int Day { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public string FirstExerciseHtml { get; set; }

        public string FirstAnswer { get; set; }

        public string SecondExerciseHtml { get; set; }

        public string SecondAnswer { get; set; }

        public bool PartTwoUnlocked => !string.IsNullOrEmpty(SecondExerciseHtml);

        public bool PartOneAnswered => !string.IsNullOrEmpty(FirstAnswer);

        public bool PartTwoAnswered => !string.IsNullOrEmpty(SecondAnswer);
    }
}
