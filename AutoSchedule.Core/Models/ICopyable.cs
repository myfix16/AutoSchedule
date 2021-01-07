using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchedule.Core.Models
{
    interface ICopyable<T>
    {
        public T ShallowCopy();

        public T DeepCopy();
    }
}
