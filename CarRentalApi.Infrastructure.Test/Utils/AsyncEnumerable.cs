using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace CarRental.Infrastructure.Test.Async
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>
    {
        public AsyncEnumerable(Expression expression): base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new()) => 
            new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }
}