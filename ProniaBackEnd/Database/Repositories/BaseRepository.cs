namespace ProniaBackEnd.Database.Repositories
{
    public class BaseRepository<TDomain>
    {
        public static List<TDomain> _entries = new List<TDomain>() ;

        public void Add(TDomain entry)
        {
            _entries.Add(entry);
        }

        public void Delete(TDomain entry)
        {
            _entries.Remove(entry);
        }

        public TDomain GetBy(Predicate<TDomain> predicate )
        {
            foreach (TDomain entry in _entries)
            {
                if (predicate(entry))
                {
                    return entry;
                }
            }
            return default;
        }

        public List<TDomain> GetAll()
        {
            return _entries;
        }

        


    }
}
