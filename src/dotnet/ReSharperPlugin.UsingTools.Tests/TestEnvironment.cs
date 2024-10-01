using System.Threading;
using JetBrains.Application.BuildScript.Application.Zones;
using JetBrains.ReSharper.Feature.Services;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.TestFramework;
using JetBrains.TestFramework;
using JetBrains.TestFramework.Application.Zones;
using NUnit.Framework;

[assembly: Apartment(ApartmentState.STA)]

namespace ReSharperPlugin.UsingTools.Tests
{
    [ZoneDefinition]
    public class UsingToolsTestEnvironmentZone : ITestsEnvZone, IRequire<PsiFeatureTestZone>, IRequire<IUsingToolsZone> { }

    [ZoneMarker]
    public class ZoneMarker : IRequire<ICodeEditingZone>, IRequire<ILanguageCSharpZone>, IRequire<UsingToolsTestEnvironmentZone> { }

    [SetUpFixture]
    public class UsingToolsTestsAssembly : ExtensionTestEnvironmentAssembly<UsingToolsTestEnvironmentZone> { }
}
