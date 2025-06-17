namespace lab5_1
{
    public class Disk
    {
        public string Name { get; set; }
        public double VolumeGB { get; set; }

        public Disk(string name, double volumeGb)
        {
            Name = name;
            VolumeGB = volumeGb;
        }

        public override string ToString()
        {
            return $"Диск: \"{Name}\", Обсяг: {VolumeGB} ГБ";
        }
    }
}