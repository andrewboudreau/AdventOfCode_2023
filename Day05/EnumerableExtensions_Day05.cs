using System.Collections;

namespace Day00;

public static partial class EnumerableExtensions
{
    public static IEnumerable<long> LongRange(long start, long count)
    {
        long max = start + count - 1;
        if (count < 0 || max < start) // Check for overflow
        {
            throw new ArgumentOutOfRangeException();
        }

        if (count == 0)
        {
            return Enumerable.Empty<long>();
        }

        return new RangeIterator(start, count);
    }
}

internal class RangeIterator : IEnumerable<long>, IEnumerator<long>
{
    private readonly long _start;
    private readonly long _end;
    private long _current;

    public RangeIterator(long start, long count)
    {
        _start = start;
        _end = start + count;
        _current = start - 1;
    }

    public IEnumerator<long> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        if (_current < _end - 1)
        {
            _current++;
            return true;
        }
        return false;
    }

    public long Current => _current;

    object IEnumerator.Current => Current;

    public void Reset()
    {
        _current = _start - 1;
    }

    public void Dispose()
    {
        // Dispose if necessary
    }
}
