using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventCalendar.Day03
{
    public class FabricMapper
    {
        public static FabricMap Map(IList<string> claims, int width, int height)
        {
            IList<FabricClaim> claimList = new List<FabricClaim>();

            foreach (var claim in claims)
            {
                claimList.Add(Claim(claim));
            }

            return new FabricMap(claimList, width, height);
        }

        public static FabricClaim Claim(string claim)
        {
            return new FabricClaim(claim);
        }
    }
}
