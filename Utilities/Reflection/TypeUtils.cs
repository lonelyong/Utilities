using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Utilities.Reflection
{
    public static class TypeUtils
    {
        private static readonly Type[] _numericTypes = new Type[] {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
        };

        private static readonly Type[] _baseValueType = new Type[] {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime),
            typeof(Guid)
        };

		private static readonly Type[] _intergerTypes = new Type[] {
			typeof(byte),
			typeof(sbyte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
		};

        static TypeUtils()
        {
            
        }

        public static bool IsNumericType(this Type type)
        {
            return _numericTypes.Contains(type);
        }

		public static bool IsIntergerType(this Type type)
		{
			return _intergerTypes.Contains(type);
		}

        public static bool IsNullableNumericType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments().First().IsNumericType();
        }

        public static bool IsBaseValueType(this Type type)
        {
            return _baseValueType.Contains(type);
        }

        public static bool IsNullableBaseValueType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments().First().IsBaseValueType();
        }

        public static Type FindClosestAncestor(this Type type, IEnumerable<Type> types)
        {
            types = types.Where(t => type.IsSubclassOf(t));
            if (types.Any())
            {
                var maxGeneration = types.Max(t => GetGenerations(t, type));
                return types.FirstOrDefault(t => GetGenerations(t, type) == maxGeneration);
            }
            return null;
        }

        public static int GetGenerations(this Type type, Type typeOther)
        {
            if (type.IsSubclassOf(typeOther))
            {
                var type1 = typeOther;
                typeOther = type;
                type = type1;
            }
            var generations = 0;
            while (typeOther != type)
            {
                generations++;
                typeOther = typeOther.BaseType;
            }
            return generations;
        }
    }
}
