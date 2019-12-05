namespace ToDo.ViewModels
{
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using ToDo.Models;

    /// <summary>
    /// ViewModel for main page (the only one yet) for the TodoApp
    /// </summary>
    public class MainPageVM : ReactiveObject
    {
        private int _filter;

        private State _state = State.Default;

        public MainPageVM()
        {
            // Create commands
            this.CreateNew = ReactiveCommand.Create(this.ExecuteCreateNew);
            this.ViewAll = ReactiveCommand.Create(() => this.Filter = 0);
            this.ViewActive = ReactiveCommand.Create(() => this.Filter = 1);
            this.ViewInactive = ReactiveCommand.Create(() => this.Filter = 2);
        }

        public State State
        {
            get => this._state;

            private set
            {
                this._state = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(this.Todos));
            }
        }

        public int Filter
        {
            get => this._filter;

            set
            {
                this.RaiseAndSetIfChanged(ref this._filter, value);
                this.RaisePropertyChanged(nameof(this.Todos));
            }
        } // 0-all, 1-active, 2-inactives

        public IEnumerable<Todo> Todos
        {
            get
            {
                switch (this.Filter)
                {
                    case 0:
                        return this._state.Todos;

                    case 1:
                        return this._state.ActiveTodos;

                    case 2:
                        return this._state.InactiveTodos;
                }

                throw new InvalidOperationException();
            }
        }

        [Reactive]
        public string NewTodoText { get; set; }

        public ICommand CreateNew { get; }

        public ICommand ViewAll { get; }

        public ICommand ViewActive { get; }

        public ICommand ViewInactive { get; }

        private void ExecuteCreateNew()
        {
            Todo newTodo = new Todo(this.NewTodoText);
            this.State.Add(newTodo);
            this.NewTodoText = string.Empty;
        }

        public void ChangeState(Todo todo, bool isDone)
        {
            todo.IsDone = isDone;
        }

        public void ChangeText(Todo todo, string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                // If the user removed all the text of a todo,
                // treat it as a delete
                this.RemoveTodo(todo);
                return;
            }

            todo.Text = newText;
        }

        public void RemoveTodo(Todo todo)
        {
            this.State.Remove(todo);
        }
    }
}