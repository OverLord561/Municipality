using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public class Query<T> : IQuery<T> where T : class
    {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 5;
    }

    public interface IQuery<T> where T : class
    {
        int Page { get; set; }

        int Size { get; set; }
    }
}
