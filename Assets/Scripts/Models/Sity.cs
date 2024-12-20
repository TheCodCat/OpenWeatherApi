namespace Assets.Scripts.Models
{
    [System.Serializable]
    public class Sity
    {
        public Coord coord;
        public string district;
        public string name;
        public int population;
        public string subject;
    }
}
public struct Coords
{
    public float lat;
    public float lon;
}
