namespace Aay07
{
    public class Worker
    {
        public StepNode Activity { get; private set; }

        public int TimeRemaining { get; private set; }

        public bool IsWorking
        {
            get
            {
                return Activity != null && TimeRemaining > 0;
            }
        }

        public void Tick()
        {
            if (Activity != null)
                TimeRemaining--;
        }

        public void SetActivity(StepNode stepNode, int baseTimeForEachStep)
        {
            Activity = stepNode;
            TimeRemaining = stepNode.ConstructionTime + baseTimeForEachStep;
            stepNode.Constructing = true;
        }

        public void ClearActivity()
        {
            Activity = null;
            TimeRemaining = 0;
        }
    }
}
