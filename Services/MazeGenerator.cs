using AzureFable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFable.Services
{
    internal class MazeGenerator
    {
        private static readonly string[] Layout = new string[]
        {
            "#######################",
            "#...........#.........#",
            "#.###.#####.#.#######.#",
            "#.#...#.....#.......#.#",
            "#.#.###.###.#######.#.#",
            "#.#.#...#.#.........#.#",
            "#.#.#.###.###########.#",
            "#.#.#...#.............#",
            "#.#.#####.###.#######.#",
            "#.#.......#...#.......#",
            "#.#########.###.#####.#",
            "#...........#.........#",
            "#######################"
        };

        private readonly Random _random = new Random();

        public Maze Generate()
        {
            int rows = Layout.Length;
            int columns = Layout[0].Length;
            Hero hero = new Hero();
            Maze maze = new Maze(rows, columns, hero);

            List<Floor> freeCells = new List<Floor>();

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (Layout[y][x] == '#')
                    {
                        maze.SetCell(new Wall(x, y));
                    }
                    else
                    {
                        Floor floor = new Floor(x, y);
                        maze.SetCell(floor);
                        freeCells.Add(floor);
                    }
                }
            }

            PlaceHero(maze, freeCells);
            PlaceKey(maze, freeCells);
            PlaceEnemies(maze, freeCells, 3);
            PlaceGhosts(maze, freeCells, 2);
            PlaceHearts(maze, freeCells, 2);

            return maze;
        }

        private Floor GetRandomFreeCell(List<Floor> freeCells)
        {
            int index = _random.Next(freeCells.Count);
            Floor cell = freeCells[index];
            freeCells.RemoveAt(index);
            return cell;
        }

        private void PlaceHero(Maze maze, List<Floor> freeCells)
        {
            Floor cell = GetRandomFreeCell(freeCells);
            maze.Hero.SetPosition(cell.X, cell.Y);
        }

        private void PlaceKey(Maze maze, List<Floor> freeCells)
        {
            Floor cell = GetRandomFreeCell(freeCells);
            Key key = new Key();
            cell.PlaceItem(key);
            maze.AddItem(key);
        }

        private void PlaceEnemies(Maze maze, List<Floor> freeCells, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Floor cell = GetRandomFreeCell(freeCells);
                StandingEnemy enemy = new StandingEnemy();
                enemy.SetPosition(cell.X, cell.Y);
                maze.AddEnemy(enemy);
            }
        }

        private void PlaceGhosts(Maze maze, List<Floor> freeCells, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Floor cell = GetRandomFreeCell(freeCells);
                Ghost ghost = new Ghost();
                ghost.SetPosition(cell.X, cell.Y);
                maze.AddEnemy(ghost);
            }
        }

        private void PlaceHearts(Maze maze, List<Floor> freeCells, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Floor cell = GetRandomFreeCell(freeCells);
                Heart heart = new Heart();
                cell.PlaceItem(heart);
                maze.AddItem(heart);
            }
        }
    }
}
