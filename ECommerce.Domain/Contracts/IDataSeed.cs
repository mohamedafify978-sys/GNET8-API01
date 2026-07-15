using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IDataSeed
    {
        Task SeedDataAsync(CancellationToken ct=default);

    }
}
