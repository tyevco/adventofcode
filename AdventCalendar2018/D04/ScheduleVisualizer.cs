using System.Text;

namespace AdventCalendar2018.D04
{
    public static class ScheduleVisualizer
    {
        public static void Visualize(Guard guard)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Date   ID     Minute");

            sb.Append("              ");
            for (int t = 0; t < 60; t++)
            {
                sb.Append(t / 10);
            }
            sb.AppendLine();

            sb.Append("              ");
            for (int t = 0; t < 60; t++)
            {
                sb.Append(t % 10);
            }
            sb.AppendLine();


            foreach (var entry in guard.TimeEntries)
            {
                sb.Append($"{entry.Key.Substring(5, 5)}  #{guard.Id}  ");

                foreach (var minute in entry.Value)
                {
                    if (minute > 0)
                        sb.Append("#");
                    else
                        sb.Append(".");
                }

                sb.AppendLine();
            }

            sb.AppendLine();

            sb.AppendLine($"Total Minutes: {guard.GetTotalTimeAsleep()}");
            sb.AppendLine($"Favorite Minutes: {string.Join(", ", guard.GetTimeMostAsleep())}");


            System.IO.File.WriteAllText($"{guard.Id}_o.txt", sb.ToString());
        }
    }
}
