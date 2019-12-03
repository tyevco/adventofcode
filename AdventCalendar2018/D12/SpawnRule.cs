using System;

namespace AdventCalendar2018.D12
{
    internal class SpawnRule
    {
        Predicate<Pot> secondLeftPredicate;
        Predicate<Pot> firstLeftPredicate;
        Predicate<Pot> targetPredicate;
        Predicate<Pot> firstRightPredicate;
        Predicate<Pot> secondRightPredicate;

        public PlantStatus Response { get; private set; }

        public string Rule { get; private set; }

        public string Outcome { get; private set; }

        public SpawnRule(string fullRule)
        {
            var parts = fullRule.Split(" => ");
            var ruleString = parts[0];
            Rule = ruleString;
            Outcome = parts[1];

            if (ruleString.Length != 5)
                throw new Exception("Invalid Rule!");

            if (ruleString[0] == '.')
                secondLeftPredicate = (p) => p == null || !p.HasPlant;
            else
                secondLeftPredicate = (p) => p != null && p.HasPlant;

            if (ruleString[1] == '.')
                firstLeftPredicate = (p) => p == null || !p.HasPlant;
            else
                firstLeftPredicate = (p) => p != null && p.HasPlant;

            if (ruleString[2] == '.')
                targetPredicate = (p) => p == null || !p.HasPlant;
            else
                targetPredicate = (p) => p != null && p.HasPlant;

            if (ruleString[3] == '.')
                firstRightPredicate = (p) => p == null || !p.HasPlant;
            else
                firstRightPredicate = (p) => p != null && p.HasPlant;

            if (ruleString[4] == '.')
                secondRightPredicate = (p) => p == null || !p.HasPlant;
            else
                secondRightPredicate = (p) => p != null && p.HasPlant;

            Response = parts[1] == "." ? PlantStatus.Decay : PlantStatus.Alive;

        }

        public PlantStatus Check(Pot secondLeft, Pot firstLeft, Pot target, Pot firstRight, Pot secondRight)
        {
            if (secondLeftPredicate(secondLeft) && firstLeftPredicate(firstLeft) && targetPredicate(target) && firstRightPredicate(firstRight) && secondRightPredicate(secondRight))
            {
                return Response;
            }

            return PlantStatus.Nothing;
        }

        public override string ToString()
        {
            return $"{Rule} => {Outcome}";
        }
    }
}