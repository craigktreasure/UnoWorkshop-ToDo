namespace ToDo.Models
{
    using DynamicData;
    using ReactiveUI;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Linq;

    public class State : ReactiveObject
    {
        private readonly SourceCache<Todo, Guid> todos = new SourceCache<Todo, Guid>(x => x.Id);

        public static State Default => new State();

        public IObservable<IChangeSet<Todo, Guid>> Connect() => this.todos.Connect();

        private readonly ReadOnlyObservableCollection<Todo> _todos;

        public ReadOnlyObservableCollection<Todo> Todos => this._todos;

        public int RemainingTodos => this.ActiveTodos.Count();

        private readonly ReadOnlyObservableCollection<Todo> _activeTodos;

        public ReadOnlyObservableCollection<Todo> ActiveTodos => this._activeTodos;

        private readonly ReadOnlyObservableCollection<Todo> _inactiveTodos;

        public ReadOnlyObservableCollection<Todo> InactiveTodos => this._inactiveTodos;

        public State()
        {
            this.Connect()
                .ObserveOn(Scheduling.MainThreadScheduler)
                .Bind(out this._todos)
                .Subscribe();

            this.Connect()
                .AutoRefresh(p => p.IsDone)
                .Filter(x => !x.IsDone)
                .ObserveOn(Scheduling.MainThreadScheduler)
                .Bind(out this._activeTodos)
                .Subscribe();

            this.Connect()
                .AutoRefresh(p => p.IsDone)
                .Filter(x => x.IsDone)
                .ObserveOn(Scheduling.MainThreadScheduler)
                .Bind(out this._inactiveTodos)
                .Subscribe();
        }

        public void Add(Todo todo)
        {
            this.todos.AddOrUpdate(todo);
        }

        public void Remove(Todo todo)
        {
            this.todos.Remove(todo);
        }
    }
}
