using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTEST.Classes
{
    public class MyService
    {
        public event Action RefreshRequested;
        
        public void  CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
