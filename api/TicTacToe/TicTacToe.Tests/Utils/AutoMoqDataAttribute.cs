using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TicTacToe.Tests.Utils;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute(params Type[] customizationTypes)
        : base(() => CreateFixture(customizationTypes))
    {
    }

    private static IFixture CreateFixture(params Type[] customizationTypes)
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization
        {
            ConfigureMembers = true
        });
        foreach (var type in customizationTypes)
        {
            var customization = (ICustomization)Activator.CreateInstance(type);
            fixture.Customize(customization);
        }

        return fixture;
    }
}