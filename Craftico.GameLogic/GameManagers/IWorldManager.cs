using Craftico.Models;

namespace Craftico.GameLogic.GameManagers
{
    public interface IWorldManager
    {
        WorldTile GetTile(int x, int y);
    }
}
