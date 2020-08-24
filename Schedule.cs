using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSchedule
{
    class Schedule
    {
        public List<Class> classes;

        public Schedule()
        {

        }

        /// <summary>
        /// Validate whether one class can be successfully added.
        /// </summary>
        /// <param name="newClass"></param>
        /// <returns></returns>
        public bool Validate(Class newClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a new class into the schedule.
        /// </summary>
        /// <returns>Whether add is successful.</returns>
        public bool AddClass(Class newClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Drop a class from the schedule.
        /// </summary>
        /// <param name="className"></param>
        /// <returns>Whether drop is successful.</returns>
        public bool DropClass(string className)
        {
            throw new NotImplementedException();
        }
    }
}
