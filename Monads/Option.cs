using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads {
    public  interface IOption<out T> {
        T Value { get; }
        bool HasValue { get; }
        bool IsEmpty { get; }
    }

    public static class Option {
        public static IOption<T> Some<T>(T value) {
            return (new SomeOption<T>(value));

        }

        public static IOption<T> Empty<T>() {
                return new None<T>();
        }
    }

    public class None<T> : IOption<T> {
        public T Value {
            get {
                throw new InvalidOperationException();
            }
        }

        public bool HasValue {
            get {
                return false;
            }
        }

        public bool IsEmpty {
            get {
                return false;
            }
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            var opt = obj as None<T>;
            return opt != null;
        }
    }

    public class SomeOption<T> : IOption<T> {
        private readonly T value;
        public SomeOption(T value) {
            this.value = value;
        }

        public T Value {
            get {
                return value;
            }
        }

        public bool HasValue {
            get {
                return true;
            }
        }

        public bool IsEmpty {
            get {
                return false;
            }
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            var opt = obj as IOption<T>;
            return opt == null ? false : opt.HasValue && opt.Value.Equals(this.Value);
        }
    }
}