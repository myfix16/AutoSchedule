using System;
using System.Linq;
using System.Collections.Generic;

namespace AutoSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            // ToDo: Retrieve class data from OCR outcome.
            List<Class> allClasses = null;

            // Generate all possilbe solutions.
            var allEnrollments = ClassSelector.Enroll(allClasses);

            // Something else.
        }
    }
}
