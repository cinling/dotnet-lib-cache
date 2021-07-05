using Cinling.Lib.Structs.Vos;

namespace Cinling.LibCache.Structs.Vos {
    
    /// <summary>
    /// 
    /// </summary>
    public class FileCacheVo : BaseVo {
        /// <summary>
        /// Expired at
        /// </summary>
        public long E { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public object C { get; set; }
    }
}