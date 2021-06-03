using BackendSaiKitchen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendSaiKitchen.CustomModel
{
    public interface IBlobManager
    {
        Task Uplaod(Blob File);
    }
}
