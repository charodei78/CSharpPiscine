using d01_ex01.Tasks;

namespace d01_ex01.Events
{
    public abstract record Event
    {
        protected Event(TaskState state)
        {
            State = state;
        }

        public TaskState State { get; init; }
    }
}