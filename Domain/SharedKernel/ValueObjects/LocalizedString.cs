using System;
using System.Linq.Expressions;
using Domain.Common.Interfaces;

namespace Domain.SharedKernel.ValueObjects
{
    public class LocalizedString
    {
        private LocalizedString() { }
        #region EXpresssin

        public static Expression<Func<T, bool>> GetEqualityExpression<T>(LocalizedString right) where T : ILocalizedName
        {
            Expression<Func<T, bool>> exp = left => left.Name.Arabic.Equals(right.Arabic) || left.Name.English.Trim().ToLower().Equals(right.English.Trim().ToLower()); return exp;
        }

        #endregion

        public LocalizedString(string arabic, string english)
        {
            Arabic = arabic;
            English = english;
        }

        public string Arabic { get; }
        public string English { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            LocalizedString other = (LocalizedString)obj;

            return Arabic.Equals(other.Arabic) && English.Trim().ToLower().Equals(other.English.Trim().ToLower());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Arabic != null ? Arabic.GetHashCode() : 0) * 397) ^ (English != null ? English.GetHashCode() : 0);
            }
        }

        public static bool operator ==(LocalizedString left, LocalizedString right)
        {

            return left != null &&
                   right != null &&
                   left.Arabic.Equals(right.Arabic) &&
                   left.English.Trim().ToLower().Equals(right.English.Trim().ToLower());
        }

        public static bool operator !=(LocalizedString left, LocalizedString right)
        {
            return !(left == right);
        }

        protected static bool EqualOperator(LocalizedString left, LocalizedString right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
                return false;

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(LocalizedString left, LocalizedString right)
        {
            return !(EqualOperator(left, right));
        }


    }
}
