using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA_Task_05.Models
{
    internal class Map
    {
        private List<MapObject> _objects;
        private readonly int _width;
        private readonly int _height;

        public Map(IEnumerable<MapObject> objects)
        {
            _objects = objects.ToList();
            _width = _objects.Max(o => o.Position.X);
            _height = _objects.Max(o => o.Position.Y);
        }

        public void Initialize()
        {
            var start = GetObjectsByType<Start>().First();
            // WARN: Hard-coded
            _objects.Add(new Player(start.Position, 100, 5));
            
            var spawners = GetObjectsByType<Spawner>();
            foreach (var spawner in spawners)
            {
                _objects.Add(spawner.Spawn());
            }
        }

        public IEnumerable<MapObject> GetAllObjects()
        {
            return _objects;
        }

        public MapObject GetObjectAt(int x, int y)
        {
            if (x < 0 || y < 0 || x > _width || y > _height)
            {
                return null;
            }

            return _objects.First(o => o.Position == new Point(x, y));
        }

        public MapObject GetObjectAt(Point position)
        {
            return GetObjectAt(position.X, position.Y);
        }

        public IEnumerable<T> GetObjectsByType<T>() where T : MapObject
        {
            return (IEnumerable<T>)_objects.Where(o => o is T);
        }

        public void Invalidate()
        {
            _objects.RemoveAll(mo => mo is Entity entity && entity.Health <= 0);
        }

        public static Map FromFile(string path)
        {
            int x = 0, y = 0;
            var objects = new List<MapObject>();

            using (var sr = new StreamReader(path))
            {
                while (sr.EndOfStream == false)
                {
                    var line = sr.ReadLine();

                    foreach (var ch in line)
                    {
                        var pos = new Point(x, y);

                        switch (ch)
                        {
                            case '.':
                                objects.Add(new Start(pos));
                                break;
                            case '*':
                                objects.Add(new Flag(pos));
                                break;
                            case '#':
                                objects.Add(new Wall(pos));
                                break;
                            // WARN: Hard-coded
                            case '!':
                                objects.Add(new Spawner(pos, new Enemy("Slime", pos, 10, 10)));
                                break;
                            default:
                                objects.Add(new MapObject(pos));
                                break;
                        }

                        y++;
                    }

                    x++;
                }
            }

            return new Map(objects);
        }
    }
}
