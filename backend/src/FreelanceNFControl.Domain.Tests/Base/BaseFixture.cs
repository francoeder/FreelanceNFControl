using AutoFixture;

namespace FreelanceNFControl.Domain.Tests.Base
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
