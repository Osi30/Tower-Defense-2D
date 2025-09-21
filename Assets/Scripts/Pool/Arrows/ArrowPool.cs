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
    public async Task<List<Arrow>> GetArrows(int number)
    {
        return await base.GetPoolElements(number);
    }

    public Arrow GetOneArrow()
    {
        return GetArrows(1).Result.First();
    }
}
