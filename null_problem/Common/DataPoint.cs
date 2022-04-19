namespace Common.DataPoints;

readonly public struct DataPoint
{
    public double Data { get; }
    public DateTime Timestamp { get; }

    public DataPoint(double data, DateTime time)
    {
        Data = data;
        Timestamp = time;
    }
}