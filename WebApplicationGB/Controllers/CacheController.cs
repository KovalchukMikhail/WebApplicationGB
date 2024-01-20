using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using WebApplicationGB.Infrastructure;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplicationGB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        public CacheController(IMemoryCache cache)
        {
            _cache = cache;
        }
        [HttpGet(template: "get_cache_statistics")]
        public ActionResult<string> GetCacheStatistics()
        {
            string fileName = "cache" + DateTime.Now.ToBinary().ToString() + ".txt";
            MemoryCacheStatistics statistic = _cache.GetCurrentStatistics();
            string content = $"CurrentEntryCount:{statistic.CurrentEntryCount};\nCurrentEstimatedSize:{statistic.CurrentEstimatedSize};\nTotalMisses:{statistic.TotalMisses};\nTotalHits:{statistic.TotalHits};\n";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);
            return "https://" + Request.Host.ToString() + "/static/" + fileName;
        }
    }
}
