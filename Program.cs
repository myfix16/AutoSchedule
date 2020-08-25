using System;
using System.Linq;
using System.Collections.Generic;

namespace AutoSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Retrieve class data from OCR outcome.
            List<Class> allClasses = null;

            // Generate all possible solutions.
            var weightedAllClasses = allClasses.OrderByDescending(c => c.weight).ToList();

            var allEnrollments = ClassSelector.Enroll(weightedAllClasses);

            // Something else.
        }
    }
}
