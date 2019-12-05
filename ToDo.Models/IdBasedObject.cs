namespace ToDo.Models
{
    using ReactiveUI;
    using System;

    public abstract class IdBasedObject : ReactiveObject, IEquatable<IdBasedObject>
    {
        public Guid Id { get; }

        protected IdBasedObject()
            : this(Guid.NewGuid())
        {
        }

        protected IdBasedObject(Guid id)
        {
            this.Id = id;
        }

        public override bool Equals(object obj) => this.Equals(obj as IdBasedObject);

        public override int GetHashCode() => this.Id.GetHashCode();

        public bool Equals(IdBasedObject other)
        {
            if (other is null)
            {
                return false;
            }

            return this.Id.Equals(other.Id);
        }

        public static bool operator ==(IdBasedObject left, IdBasedObject right)
        {
            if (left is null || right is null)
            {
                return Object.Equals(left, right);
            }

            return left.Equals(right);
        }

        public static bool operator !=(IdBasedObject left, IdBasedObject right)
        {
            if (left is null || right is null)
            {
                return !Object.Equals(left, right);
            }

            return !left.Equals(right);
        }
    }
}
