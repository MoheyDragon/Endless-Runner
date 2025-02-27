using System.Collections.Generic;
namespace EndlessRunner
{
    public struct ChunkObjects
    {
        public List<Collectable> collectables;
        public List<Obstacle> obstacles;

        public ChunkObjects(List<Collectable> collectables, List<Obstacle> obstacles)
        {
            this.collectables = collectables;
            this.obstacles = obstacles;
        }
    }
}
