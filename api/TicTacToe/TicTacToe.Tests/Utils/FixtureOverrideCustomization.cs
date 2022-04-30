using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using AutoFixture;
using AutoFixture.Kernel;

namespace TicTacToe.Tests.Utils
{
    [ExcludeFromCodeCoverage]
    public class FixtureOverrideCustomization<T>
    {
        public IFixture Fixture { get; }

        private readonly ICollection<ISpecimenBuilder> _builders = new List<ISpecimenBuilder>();

        public FixtureOverrideCustomization(IFixture fixture)
        {
            Fixture = fixture;
        }

        public FixtureOverrideCustomization<T> WithOverride<TProp>(Expression<Func<T, TProp>> expr, TProp value)
        {
            var builder = new OverridePropertyBuilder<T, TProp>(expr, value);
            _builders.Add(builder);
            Fixture.Customizations.Add(builder);
            return this;
        }

        public T Create(bool cleanOverridesAfterCreate = true)
        {
            var result = Fixture.Create<T>();
            if (cleanOverridesAfterCreate)
                CleanOverrides();

            return result;
        }

        public IEnumerable<T> CreateMany(int count = 3, bool cleanOverridesAfterCreate = true)
        {
            var result = Fixture.CreateMany<T>(count);
            if (cleanOverridesAfterCreate)
                CleanOverrides();

            return result;
        }

        public void CleanOverrides()
        {
            foreach (var specimenBuilder in _builders)
            {
                Fixture.Customizations.Remove(specimenBuilder);
            }
        }
    }

    public static class CompositionExt
    {
        public static FixtureOverrideCustomization<T> BuildReadonly<T>(this IFixture fixture)
            => new(fixture);
    }
}
