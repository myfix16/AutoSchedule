namespace AutoSchedule.Core.Models
{
    internal interface ICopyable<out T>
    {
        public T ShallowCopy();

        public T DeepCopy();
    }
}
