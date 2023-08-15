namespace ProniaBackEnd.Database.Base
{
    public class BaseRepository<TDomain>
        where TDomain : BaseEntity
    {
        public static List<TDomain> _entries = new List<TDomain>();

        public void Add(TDomain entry)
        {
            _entries.Add(entry);
        }

        public void Delete(TDomain entry)
        {
            _entries.Remove(entry);
        }

        public TDomain GetBy(Predicate<TDomain> predicate)
        {
            foreach (TDomain entry in _entries)
            {
                if (predicate(entry))
                {
                    return entry;
                }
            }
            return null;
        }

        public IEnumerable<TDomain> GetAll()
        {
            return _entries;
        }




    }
}
