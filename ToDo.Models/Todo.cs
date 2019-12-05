namespace ToDo.Models
{
    using ReactiveUI.Fody.Helpers;

    public class Todo : IdBasedObject
    {
        [Reactive]
        public bool IsDone { get; set; }

        [Reactive]
        public string Text { get; set; }

        public Todo(string text)
            : base()
        {
            this.Text = text;
        }

        public void ToggleDone()
        {
            this.IsDone = !this.IsDone;
        }
    }
}
