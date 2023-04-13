using AutoFixture;

namespace FreelanceNFControl.Infra.Core.Tests.Base
{
    public class BaseFixture
    {
        public readonly IFixture Fixture;

        public BaseFixture()
        {
            Fixture = new Fixture();
        }
    }
}
