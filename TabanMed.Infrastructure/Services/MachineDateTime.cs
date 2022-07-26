
using Common;

namespace TabanMed.Infrastructure.Services
{
    public class MachineDateTime : IDateTime
    {
        public int CurrentYear => DateTime.Now.Year;

        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
