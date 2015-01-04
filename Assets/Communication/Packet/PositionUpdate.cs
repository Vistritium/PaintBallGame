namespace Assets.Communication.Packet
{
    public struct PositionUpdate
    {
        public uint id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }
}