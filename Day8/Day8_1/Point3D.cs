namespace Day8_1
{
    /// <summary>
    /// Struct to represent a point in 3D space
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    public struct Point3D(long x, long y, long z)
    {
        public long X = x, Y = y, Z = z;
    }
}
