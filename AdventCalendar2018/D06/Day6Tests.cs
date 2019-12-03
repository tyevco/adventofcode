using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdventCalendar2018.D06
{
    public class Day6Tests
    {
        [Fact]
        public void ActualDataPart1()
        {
            var map = makeActualMap();

            map.GenerateAreaTracking();

            var points = map.Points.Where(x => !x.IsInfinite).OrderBy(x => map.GetArea(x));

            var lastPoint = points.Last();

            Assert.Equal(34, lastPoint.Id);
            Assert.Equal(3006, map.GetArea(lastPoint));

            var seek = map.GeneratePointSeek(10000);

            var total = 0;
            foreach (var column in seek)
            {
                total += column.Count(x => x < 10000);
            }

            Assert.Equal(42998, total);
        }

        [Fact]
        public void SampleDataMapsCorrectly()
        {
            ManhattanMap map = new ManhattanMap();

            var A = map.AddPoint(1, 1);
            AssertMapBounds(map, 2, 0, 0, 2);

            var B = map.AddPoint(1, 6);
            AssertMapBounds(map, 7, 0, 0, 2);

            var C = map.AddPoint(8, 3);
            AssertMapBounds(map, 7, 0, 0, 9);

            var D = map.AddPoint(3, 4);
            AssertMapBounds(map, 7, 0, 0, 9);

            var E = map.AddPoint(5, 5);
            AssertMapBounds(map, 7, 0, 0, 9);

            var F = map.AddPoint(8, 9);
            AssertMapBounds(map, 10, 0, 0, 9);

            var tracking = map.GenerateAreaTracking();

            AssertTrackingMatches(
                tracking,
                new int[] { 0, 0, 0, 0, 0, -1, 2, 2, 2, 2 },
                new int[] { 0, 0, 0, 0, 0, -1, 2, 2, 2, 2 },
                new int[] { 0, 0, 0, 3, 3, 4, 2, 2, 2, 2 },
                new int[] { 0, 0, 3, 3, 3, 4, 2, 2, 2, 2 },
                new int[] { -1, -1, 3, 3, 3, 4, 4, 2, 2, 2 },
                new int[] { 1, 1, -1, 3, 4, 4, 4, 4, 2, 2 },
                new int[] { 1, 1, 1, -1, 4, 4, 4, 4, -1, -1 },
                new int[] { 1, 1, 1, -1, 4, 4, 4, 5, 5, 5 },
                new int[] { 1, 1, 1, -1, 4, 4, 5, 5, 5, 5 },
                new int[] { 1, 1, 1, -1, 5, 5, 5, 5, 5, 5 },
                new int[] { 1, 1, 1, -1, 5, 5, 5, 5, 5, 5 }
            );

            Assert.True(A.IsInfinite);
            Assert.True(B.IsInfinite);
            Assert.True(C.IsInfinite);
            Assert.False(D.IsInfinite);
            Assert.False(E.IsInfinite);
            Assert.True(F.IsInfinite);

            Assert.Equal(9, map.GetArea(D));
            Assert.Equal(17, map.GetArea(E));

            var seekData = map.GeneratePointSeek(32);

            Assert.Equal(30, seekData[3][4]);


        }

        [Fact]
        public void TwoClosePointsGenerateCorrectly()
        {
            ManhattanMap map = new ManhattanMap();

            var p0 = map.AddPoint(1, 1);
            var p1 = map.AddPoint(2, 2);

            AssertMapBounds(map, 3, 0, 0, 3);

            var tracking = map.GenerateAreaTracking();

            AssertTrackingMatches(tracking,
                new int[] { 0, 0, -1, -1 },
                new int[] { 0, 0, -1, -1 },
                new int[] { -1, -1, 1, 1 },
                new int[] { -1, -1, 1, 1 }
            );

            Assert.True(p0.IsInfinite);
            Assert.True(p1.IsInfinite);

            Assert.Equal(4, map.GetArea(p0));
            Assert.Equal(4, map.GetArea(p1));
        }

        private void AssertTrackingMatches(IList<IList<int>> tracking, params int[][] values)
        {
            Assert.Equal(values.Length, tracking.Count);

            for (int y = 0; y < tracking.Count; y++)
            {
                var trackingColumn = tracking[y];
                var valueColumn = values[y];

                Assert.Equal(valueColumn.Length, trackingColumn.Count);

                for (int x = 0; x < trackingColumn.Count; x++)
                {
                    Assert.Equal(valueColumn[x], trackingColumn[x]);
                }
            }
        }

        private void AssertMapBounds(ManhattanMap map, int top, int left, int bottom, int right)
        {
            Assert.Equal(top, map.Top);
            Assert.Equal(left, map.Left);
            Assert.Equal(bottom, map.Bottom);
            Assert.Equal(right, map.Right);
        }

        private ManhattanMap makeActualMap()
        {
            ManhattanMap map = new ManhattanMap();

            map.AddPoint(77, 279);
            map.AddPoint(216, 187);
            map.AddPoint(72, 301);
            map.AddPoint(183, 82);
            map.AddPoint(57, 170);
            map.AddPoint(46, 335);
            map.AddPoint(55, 89);
            map.AddPoint(71, 114);
            map.AddPoint(313, 358);
            map.AddPoint(82, 88);
            map.AddPoint(78, 136);
            map.AddPoint(339, 314);
            map.AddPoint(156, 281);
            map.AddPoint(260, 288);
            map.AddPoint(125, 249);
            map.AddPoint(150, 130);
            map.AddPoint(210, 271);
            map.AddPoint(190, 258);
            map.AddPoint(73, 287);
            map.AddPoint(187, 332);
            map.AddPoint(283, 353);
            map.AddPoint(66, 158);
            map.AddPoint(108, 97);
            map.AddPoint(237, 278);
            map.AddPoint(243, 160);
            map.AddPoint(61, 52);
            map.AddPoint(353, 107);
            map.AddPoint(260, 184);
            map.AddPoint(234, 321);
            map.AddPoint(181, 270);
            map.AddPoint(104, 84);
            map.AddPoint(290, 109);
            map.AddPoint(193, 342);
            map.AddPoint(43, 294);
            map.AddPoint(134, 211);
            map.AddPoint(50, 129);
            map.AddPoint(92, 112);
            map.AddPoint(309, 130);
            map.AddPoint(291, 170);
            map.AddPoint(89, 204);
            map.AddPoint(186, 177);
            map.AddPoint(286, 302);
            map.AddPoint(188, 145);
            map.AddPoint(40, 52);
            map.AddPoint(254, 292);
            map.AddPoint(270, 287);
            map.AddPoint(238, 216);
            map.AddPoint(299, 184);
            map.AddPoint(141, 264);
            map.AddPoint(117, 129);

            return map;
        }
    }
}
