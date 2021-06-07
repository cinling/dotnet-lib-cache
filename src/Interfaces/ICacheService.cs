namespace LibCache.Interfaces {
    public interface ICacheService {

        public T set<T>(string key, T value);
        
    }
}