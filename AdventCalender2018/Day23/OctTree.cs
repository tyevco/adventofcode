using System.Collections.Generic;

namespace Day23
{
    public partial class OctTree
    {
        public int MinimumX { get; private set; }
        public int MinimumY { get; private set; }
        public int MinimumZ { get; private set; }
        public int MaximumX { get; private set; }
        public int MaximumY { get; private set; }
        public int MaximumZ { get; private set; }

        public int MidX => (MaximumX + MinimumX) / 2;
        public int MidY => (MaximumY + MinimumY) / 2;
        public int MidZ => (MaximumZ + MinimumZ) / 2;

        public OctTree UNE => GetOrCreate(Region.UNE);
        public OctTree UNW => GetOrCreate(Region.UNW);
        public OctTree USE => GetOrCreate(Region.USE);
        public OctTree USW => GetOrCreate(Region.USW);
        public OctTree LNE => GetOrCreate(Region.LNE);
        public OctTree LNW => GetOrCreate(Region.LNW);
        public OctTree LSE => GetOrCreate(Region.LSE);
        public OctTree LSW => GetOrCreate(Region.LSW);

        public IEnumerable<OctTree> Regions => RegionMap.Values;

        IDictionary<Region, OctTree> RegionMap { get; } = new Dictionary<Region, OctTree>();

        public OctTree(int minX, int minY, int minZ, int maxX, int maxY, int maxZ)
        {
            MinimumX = minX;
            MinimumY = minY;
            MinimumZ = minZ;
            MaximumX = maxX;
            MaximumY = maxY;
            MaximumZ = maxZ;
        }

        public bool SphereIntersects(int x, int y, int z, int r)
        {
            return false;
        }

        private OctTree GetOrCreate(Region region)
        {
            OctTree tree;
            if (RegionMap.ContainsKey(region))
            {
                tree = RegionMap[region];
            }
            else
            {
                int minX, minY, minZ, maxX, maxY, maxZ;
                int midX = MidX, midY = MidY, midZ = MidZ;

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
                RegionMap.Add(region, tree);
            }

            return tree;
        }
    }
}
