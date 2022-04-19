using Common.DataPoints;
using SampleCore.Interfaces;
using Utility.Collections;

namespace SampleCore.Sensors.DataStore;

internal class SensorDataStore : ISensorDataStore
{
    protected readonly CircularBuffer<DateTime> _dateTime;
    protected readonly CircularBuffer<double> _valueUserCalibrated;
    protected readonly CircularBuffer<double> _valueFactoryCalibrated;
    protected readonly CircularBuffer<double> _valueCalculated;

    protected bool _isActive = true;
    public bool IsActive
    {
        get { return _isActive; }
        set { SetIsActive(value); }
    }

    public virtual DataPoint this[DateTime timeStamp] => GetValue(timeStamp);

    public SensorDataStore(int capacity)
    {
        _dateTime = new CircularBuffer<DateTime>(capacity);
        _valueUserCalibrated = new CircularBuffer<double>(capacity);
        _valueFactoryCalibrated = new CircularBuffer<double>(capacity);
        _valueCalculated = new CircularBuffer<double>(capacity);
    }

    protected virtual void SetIsActive(bool value)
    {
        if (!value)
        {
            _dateTime.Clear(fullReset: false);
            _valueUserCalibrated.Clear(fullReset: false);
            _valueFactoryCalibrated.Clear(fullReset: false);
            _valueCalculated.Clear(fullReset: false);
        }

        _isActive = value;
    }

    protected virtual DataPoint GetValue(DateTime timeStamp)
    {
        if (_dateTime.Front() == timeStamp)
        {
            return new DataPoint(_valueCalculated[0], timeStamp);
        }

        if (_dateTime.Front() < timeStamp)
        {
            throw new ArgumentOutOfRangeException(nameof(timeStamp), timeStamp, $"Is after latest {nameof(_dateTime)} {_dateTime.Front()}");
        }

        if (_dateTime.Back() > timeStamp)
        {
            throw new ArgumentOutOfRangeException(nameof(timeStamp), timeStamp, $"Is before first {nameof(_dateTime)} {_dateTime.Back()}");
        }

        return new DataPoint(_valueCalculated[((int)_dateTime.Front().Subtract(timeStamp).TotalSeconds) - 1], timeStamp);
    }

    public void AddTestValue(double testValue, DateTime timeStamp)
    {
        _valueCalculated.PushFront(testValue);
        _dateTime.PushFront(timeStamp);
    }
}