using Advent.Utilities;
using System.Collections.Generic;

namespace Day23
{
    public partial class OctTree
    {
        public int Hits { get; set; }

        public double MinimumX { get; private set; }
        public double MinimumY { get; private set; }
        public double MinimumZ { get; private set; }
        public double MaximumX { get; private set; }
        public double MaximumY { get; private set; }
        public double MaximumZ { get; private set; }

        public double MidX => (MaximumX + MinimumX) / 2d;
        public double MidY => (MaximumY + MinimumY) / 2d;
        public double MidZ => (MaximumZ + MinimumZ) / 2d;

        public double Volume => (MaximumX - MinimumX) * (MaximumY - MinimumY) * (MaximumZ - MinimumZ);

        public OctTree UNE => GetOrCreate(Region.UNE);
        public OctTree UNW => GetOrCreate(Region.UNW);
        public OctTree USE => GetOrCreate(Region.USE);
        public OctTree USW => GetOrCreate(Region.USW);
        public OctTree LNE => GetOrCreate(Region.LNE);
        public OctTree LNW => GetOrCreate(Region.LNW);
        public OctTree LSE => GetOrCreate(Region.LSE);
        public OctTree LSW => GetOrCreate(Region.LSW);

        public IEnumerable<OctTree> Regions => new List<OctTree> { UNE, UNW, USE, USW, LNE, LNW, LSE, LSW };

        IDictionary<Region, OctTree> RegionMap { get; } = new Dictionary<Region, OctTree>();

        public OctTree(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
        {
            MinimumX = minX;
            MinimumY = minY;
            MinimumZ = minZ;
            MaximumX = maxX;
            MaximumY = maxY;
            MaximumZ = maxZ;
        }

        private OctTree GetOrCreate(Region region)
        {
            OctTree tree = null;

            if (Volume > 1)
            {
                if (RegionMap.ContainsKey(region))
                {
                    tree = RegionMap[region];
                }
                else
                {
                    double minX, minY, minZ, maxX, maxY, maxZ;
                    double midX = MidX, midY = MidY, midZ = MidZ;

                    switch (region)
                    {
                        default:
                        case Region.UNE:
                            minX = midX; minY = midY; minZ = midZ;
                            maxX = MaximumX; maxY = MaximumY; maxZ = MaximumZ;
                            break;
                        case Region.UNW:
                            minX = MinimumX; minY = midY; minZ = midZ;
                            maxX = midX; maxY = MaximumY; maxZ = MaximumZ;
                            break;
                        case Region.USE:
                            minX = midX; minY = MinimumY; minZ = midZ;
                            maxX = MaximumX; maxY = midY; maxZ = MaximumZ;
                            break;
                        case Region.USW:
                            minX = MinimumX; minY = MinimumY; minZ = midZ;
                            maxX = midX; maxY = midY; maxZ = MaximumZ;
                            break;
                        case Region.LNE:
                            minX = midX; minY = midY; minZ = MinimumZ;
                            maxX = MaximumX; maxY = MaximumY; maxZ = midZ;
                            break;
                        case Region.LNW:
                            minX = MinimumX; minY = midY; minZ = MinimumZ;
                            maxX = midX; maxY = MaximumY; maxZ = midZ;
                            break;
                        case Region.LSE:
                            minX = midX; minY = MinimumY; minZ = MinimumZ;
                            maxX = MaximumX; maxY = midY; maxZ = midZ;
                            break;
                        case Region.LSW:
                            minX = MinimumX; minY = MinimumY; minZ = MinimumZ;
                            maxX = midX; maxY = midY; maxZ = midZ;
                            break;
                    }

                    tree = new OctTree(minX, minY, minZ, maxX, maxY, maxZ);

                    if (tree.Volume < 25)
                    {
                        tree = null;
                    }

                    RegionMap.Add(region, tree);
                }
            }

            return tree;
        }

        public IAABoundingBox GetBounds()
        {
            return new AABoundingBox(
                new Vector(MinimumX, MinimumY, MinimumZ),
                new Vector(MinimumX, MinimumY, MaximumZ),
                new Vector(MinimumX, MaximumY, MaximumZ),
                new Vector(MinimumX, MaximumY, MinimumZ),
                new Vector(MaximumX, MinimumY, MinimumZ),
                new Vector(MaximumX, MinimumY, MaximumZ),
                new Vector(MaximumX, MaximumY, MaximumZ),
                new Vector(MaximumX, MaximumY, MinimumZ)
                );
        }
    }
}
