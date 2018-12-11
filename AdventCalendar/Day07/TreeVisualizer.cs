using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCalendar.Day07
{
    public class TreeVisualizer
    {
        private int count;

        StringBuilder log = new StringBuilder();

        public TreeVisualizer(int count)
        {
            this.count = count;

            log.Append("Second    ");
            for (int i = 0; i < count; i++)
            {
                log.Append($"Worker {(i + 1).ToString("00")}   ");
            }
            log.Append("Done  ");
            log.AppendLine();
        }

        public void AddTick(int time, IList<Worker> workers, string order)
        {

            log.AppendFormat($"  {time.ToString("0000")}    ");
            for (int i = 0; i < workers.Count; i++)
            {
                var worker = workers[i];
                log.Append($"   {worker.Activity?.Id ?? '.'} {(worker.Activity != null ? "[" + worker.TimeRemaining.ToString("00") + "]" : "    ")}   ");
            }
            log.Append(order);
            log.AppendLine();
        }

        public void Print()
        {
            System.IO.File.WriteAllText($"RunTime_{DateTime.Now.Ticks}.txt", log.ToString());
        }
    }
}