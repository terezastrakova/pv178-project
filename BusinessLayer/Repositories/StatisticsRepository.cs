using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace BusinessLayer.Repositories
{
    public class StatisticsRepository
    {
        private readonly ApplicationDbContext _context;
        public StatisticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetProgressAsync(string userId, int setId)
        {
            return await _context.UserVocabularySets
                .Where(uvs => uvs.UserId == userId && uvs.VocabularySetId == setId)
                .Select(uvs => uvs.Progress)
                .FirstOrDefaultAsync();
        }
        public async Task UpdateUserSetProgress(string userId, int setId, int progress)
        {
            var uvSet = await _context.UserVocabularySets
                .FirstOrDefaultAsync(uvs => uvs.UserId == userId && uvs.VocabularySetId == setId);

            if (uvSet != null)
            {
                uvSet.Progress = progress;
                await _context.SaveChangesAsync();
            }
        }
    }
}
