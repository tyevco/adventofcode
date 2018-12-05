using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.Day3
{
    public class FabricMap
    {
        private IList<FabricClaim> claims;
        int[][] claimCounts;

        public FabricMap(IList<FabricClaim> claims, int width, int height)
        {
            this.claims = claims;
            claimCounts = new int[width][];

            for (int x = 0; x < width; x++)
            {
                claimCounts[x] = new int[height];
            }
        }

        public void GenerateCounts()
        {
            for (int x = 0; x < claimCounts.Length; x++)
            {
                for (int y = 0; y < claimCounts[x].Length; y++)
                {
                    claimCounts[x][y] = claims.Count(c => c.HasClaim(x, y));
                }
            }
        }

        public IList<string> GetOverlaps()
        {
            IList<string> overlaps = new List<string>();

            for (int x = 0; x < claimCounts.Length; x++)
            {
                for (int y = 0; y < claimCounts[x].Length; y++)
                {
                    if (claimCounts[x][y] > 1)
                    {
                        overlaps.Add($"{x},{y}");
                    }
                }
            }

            return overlaps;
        }

        public IList<FabricClaim> GetNonOverlappingClaims()
        {
            IList<FabricClaim> claimList = claims.Where(x => claims.Count(y => x.Intersects(y)) == 1).ToList();

            return claimList;
        }
    }
}
