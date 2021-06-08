using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum_Snackis.Models
{
    public interface IInsertGateway
    {
        Task<List<Insert>> GetInserts(); 
    }
}
