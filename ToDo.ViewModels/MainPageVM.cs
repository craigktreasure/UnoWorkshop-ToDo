namespace ToDo.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using ToDo.Models;

    /// <summary>
    /// ViewModel for main page (the only one yet) for the TodoApp
    /// </summary>
    public class MainPageVM : INotifyPropertyChanged
    {
        private int _filter;

        private string _newTodoText;

        private State _state = State.Default;

        public MainPageVM()
        {
            // Create commands
            this.CreateNew = new SimpleCommand(this.ExecuteCreateNew);
            this.ViewAll = new SimpleCommand(() => this.Filter = 0);
            this.ViewActive = new SimpleCommand(() => this.Filter = 1);
            this.ViewInactive = new SimpleCommand(() => this.Filter = 2);
        }

        public State State
        {
            get => this._state;

            private set
            {
                this._state = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.Todos));
            }
        }

        public int Filter
        {
            get => this._filter;

            set
            {
                this._filter = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.Todos));
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

        public string NewTodoText
        {
            get => this._newTodoText;

            set
            {
                this._newTodoText = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand CreateNew { get; }

        public ICommand ViewAll { get; }

        public ICommand ViewActive { get; }

        public ICommand ViewInactive { get; }

        private void ExecuteCreateNew()
        {
            Todo newTodo = new Todo(this.NewTodoText);
            this.State = this.State.WithTodos(todos => todos.Add(newTodo));
            this.NewTodoText = string.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChangeState(Todo todo, bool isDone)
        {
            this.State = this.State.WithTodos(todos =>
            {
                Todo existing = todos.FirstOrDefault(t => t.KeyEquals(todo));
                Todo newTodo = existing.WithIsDone(isDone);
                return newTodo != existing ? todos.Replace(existing, newTodo) : todos;
            });
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

            this.State = this.State.WithTodos(todos =>
            {
                Todo existing = todos.FirstOrDefault(t => t.KeyEquals(todo));
                Todo newTodo = existing.WithText(newText);
                return newTodo != existing ? todos.Replace(existing, newTodo) : todos;
            });
        }

        public void RemoveTodo(Todo todo)
        {
            this.State = this.State.WithTodos(todos =>
            {
                Todo existing = todos.FirstOrDefault(t => t.KeyEquals(todo));
                return existing != null ? todos.Remove(existing) : todos;
            });
        }
    }
}