using System;
using System.Collections.Generic;
using System.Text;
using APIUsage.Core;

namespace APIUsage.Data.Contract
{
    public interface IAPIUsageRepository
    {
        int SaveCall(APIUsage.Core.APIUsage apiCall);

        IEnumerable<APIUsage.Core.APIUsage> GetTotalCallsByToken(string token);


    }
}
