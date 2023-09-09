using ProniaBackEnd.Database;

namespace ProniaBackEnd.Services
{
    public class OrderCodeGenerator
    {
        private readonly AppDbContext _appDbContext;

        private const int MinNumber = 10000;
        private const int MaxNumber = 99999;
        public OrderCodeGenerator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public string GenarateAndReturnUniqueOrderCode()
        {
            Random random = new Random();

            string trackignCode = "OR" + random.Next(MinNumber, MaxNumber).ToString();

            while (_appDbContext.Orders.Any(e => e.TracingCode == trackignCode))
            {
                trackignCode = "OR" + random.Next(MinNumber, MaxNumber).ToString();
            }

            return trackignCode;
        }
    }
}
