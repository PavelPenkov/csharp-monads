using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monads {
    public static class IOptionOps {
        public static IOption<TOut> Select<TIn, TOut>(this IOption<TIn> option, Func<TIn,TOut> f) {
            return option.HasValue ? Option.Some(f(option.Value)) : Option.Empty<TOut>();
        }

        public static IOption<TOut> SelectMany<TIn, TMid, TOut>(this IOption<TIn> option, Func<TIn, IOption<TMid>> midf, Func<TIn, TMid, TOut> outf) {
            if (option.IsEmpty) return Option.Empty<TOut>();
            var mid = midf(option.Value);
            if (mid.IsEmpty) return Option.Empty<TOut>();
            return Option.Some(outf(option.Value, mid.Value));
        }

        public static IOption<T> Where<T>(this IOption<T> option, Func<T, bool> predicate) {
            if (option.HasValue) {
                return predicate(option.Value) ? option : Option.Empty<T>();
            } else {
                return option;
            }
        }
    }
}
