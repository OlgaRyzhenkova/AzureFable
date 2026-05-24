using AzureFable.Models;
using System;
using System.Collections.Generic;

namespace AzureFable.Services
{
    internal class MazeGenerator : IMazeGenerator
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
        private readonly IReadOnlyList<EnemySpawnRule> _enemySpawnRules;

        public MazeGenerator() : this(DefaultEnemySpawnRules.Create())
        {
        }

        public MazeGenerator(IReadOnlyList<EnemySpawnRule> enemySpawnRules)
        {
            ArgumentNullException.ThrowIfNull(enemySpawnRules);

            _enemySpawnRules = enemySpawnRules;
        }

        public Maze Generate()
        {
            int rows = Layout.Length;
            ValidateLayoutRows(rows);

            int columns = Layout[0].Length;
            ValidateLayout(rows, columns);

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
            PlaceEnemies(maze, freeCells);
            PlaceHearts(maze, freeCells, 2);

            return maze;
        }

        private void ValidateLayoutRows(int rows)
        {
            if (rows == 0)
            {
                throw new MazeGenerationException("Maze layout cannot be empty.");
            }
        }

        private void ValidateLayout(int rows, int columns)
        {
            if (columns == 0)
            {
                throw new MazeGenerationException("Maze layout cannot be empty.");
            }

            foreach (string row in Layout)
            {
                if (row.Length != columns)
                {
                    throw new MazeGenerationException("Maze layout rows must have the same length.");
                }
            }
        }

        private Floor GetRandomFreeCell(List<Floor> freeCells)
        {
            if (freeCells.Count == 0)
            {
                throw new MazeGenerationException("Maze does not have enough free cells for all objects.");
            }

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

        private void PlaceEnemies(Maze maze, List<Floor> freeCells)
        {
            foreach (EnemySpawnRule rule in _enemySpawnRules)
            {
                for (int i = 0; i < rule.Count; i++)
                {
                    Floor cell = GetRandomFreeCell(freeCells);
                    Enemy enemy = rule.CreateEnemy();
                    enemy.SetPosition(cell.X, cell.Y);
                    maze.AddEnemy(enemy);
                }
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
