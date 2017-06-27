using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Light.GuardClauses;
using Light.GuardClauses.FrameworkExtensions;

namespace DiAndMvvm
{
    public static class RaisePropertyChangedExtensions
    {
        public static bool SetIfDifferent<T>(this IRaisePropertyChanged target, ref T field, T value, IEqualityComparer<T> comparer = null, [CallerMemberName] string memberName = null)
        {
            target.MustNotBeNull(nameof(target));
            comparer = comparer ?? EqualityComparer<T>.Default;

            if (comparer.EqualsWithHashCode(field, value))
                return false;

            field = value;
            target.OnPropertyChanged(memberName);
            return true;
        }
    }
}