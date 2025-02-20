using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ostool.IntegrationTests.Setup.Extensions
{
    public static class BogusExtension
    {
        public static Faker<T> WithRecord<T>(this Faker<T> faker) where T : class
        {
            faker.CustomInstantiator(_ => (RuntimeHelpers.GetUninitializedObject(typeof(T)) as T)!);
            return faker;
        }
    }
}