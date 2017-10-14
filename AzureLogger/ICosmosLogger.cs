using System;
using System.Collections.Generic;
using System.Text;

namespace AzureLogger
{
    public interface ICosmosLogger
    {
        void Insert(string message);
    }
}
