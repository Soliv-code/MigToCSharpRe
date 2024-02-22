using Plugins.ADM.MSSQL;

namespace ADM.Tests.Common
{
    public abstract class TestBase : IDisposable
    {
        protected readonly ADMContext Context;
        protected TestBase()
        {
            Context = ADMFactory.Create();
        }
        public void Dispose()
        {
            ADMFactory.Destroy(Context);
        }
    }
}
