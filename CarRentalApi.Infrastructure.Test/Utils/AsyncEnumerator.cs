using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Test.Async
{
    public class AsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public AsyncEnumerator(IEnumerator<T> enumerator) =>
            _enumerator = enumerator ?? throw new ArgumentNullException();

        public ValueTask<bool> MoveNextAsync() =>
            ValueTask.FromResult(_enumerator.MoveNext());

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync()
        {
            _enumerator.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}