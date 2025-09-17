using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ArrowPool : BasePool<Arrow>
{
    /// <summary>
    /// Get Arrows From Pool
    /// </summary>
    /// <param name="number"></param>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public async Task<List<Arrow>> GetArrows(int number, bool isActive)
    {
        return await base.GetPoolElements(number, isActive);
    }

    public Arrow GetOneActiveArrow()
    {
        return GetArrows(1, true).Result.First();
    }
}
