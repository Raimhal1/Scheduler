using System;
using System.Collections.Generic;

namespace MyScheduler
{
    interface IWriteInfo
    {
        void Write(List<DayEvent> Events);
    }
}
