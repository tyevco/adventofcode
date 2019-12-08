using Advent.Utilities;
using Advent.Utilities.Attributes;
using System;
using System.IO;
using System.Linq;

namespace AdventCalendar2019.D08
{
    [Exercise("Day 8: Space Image Format")]
    class Y2019D08 : FileSelectionConsole, IExercise
    {
        public void Execute()
        {
            Start("D08/Data");
        }

        protected override void Execute(string file)
        {
            int width = Helper.ReadIntInput("Width");
            int height = Helper.ReadIntInput("Height");
            var fileData = File.ReadAllText(file);

            int[] pixelData = fileData.Select(c => int.Parse(c.ToString())).ToArray();

            Func<int, int, int> getIndex = (int x, int y) => x + y * width;

            int imgSize = width * height;

            int layerCount = pixelData.Length / imgSize;

            char[] finalImage = Helper.CreateArray<char>(imgSize, ' ');

            Timer.Monitor(() =>
            {
                int[][] layers = new int[layerCount][];

                for (int i = 0; i < layerCount; i++)
                {
                    int[] layerData = new int[imgSize];

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            var pixelIndex = getIndex(x, y);
                            layerData[pixelIndex] = pixelData[(imgSize * i) + pixelIndex];
                        }
                    }

                    layers[i] = layerData;
                }

                int fewestZeroCount = int.MaxValue;
                int[] fewestZeroLayer = null;

                foreach (var layerData in layers)
                {
                    int zeroCount = layerData.Count(x => x == 0);

                    if (fewestZeroCount > zeroCount)
                    {
                        fewestZeroCount = zeroCount;
                        fewestZeroLayer = layerData;
                    }
                }

                if (fewestZeroLayer != null)
                {
                    Helper.PrintArray(fewestZeroLayer, width);

                    int count1 = fewestZeroLayer.Count(x => x == 1);
                    int count2 = fewestZeroLayer.Count(x => x == 2);
                    Console.WriteLine($"{count1} x  {count2} = {(count1 * count2)}");
                }
                else
                {
                    Console.WriteLine("No output found...");
                }

                for (int i = 0; i < imgSize; i++)
                {
                    for (int n = 0; n < layers.Length; n++)
                    {
                        if (layers[n][i] != 2)
                        {
                            finalImage[i] = layers[n][i] == 1 ? '█' : ' ';
                            break;
                        }
                    }
                }

                Helper.PrintArray(finalImage, width);
            });
        }

    }
}
