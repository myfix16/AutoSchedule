namespace AutoSchedule.Core.Models
{
    interface ICopyable<T>
    {
        public T ShallowCopy();

        public T DeepCopy();
    }
}
