using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathFinderGUI
{
    public struct PanelGridContext
    {
        public Action<MouseEventArgs, Vertex2D> CellClickEventHandler;
        public int GridSize;
        public int Height;
        public int Width;
        public Func<Panel, bool> IsWall;
    }

    public class PanelGrid : Panel
    {
        public PanelGridContext Context { get; }
        private readonly Color defaultPanelBackgroundColor = Color.White;
        private readonly int tileSizeX;
        private readonly int tileSizeY;

        public Panel[,] BoardCells { get; private set; }

        public PanelGrid(PanelGridContext ctx)
        {
            Context = ctx;
            tileSizeX = ctx.Width / ctx.GridSize;
            tileSizeY = ctx.Height / ctx.GridSize;
            InitializeGrid();
        }

        public Color GetCellColorByPosition(Vertex2D pos)
        {
            return BoardCells[pos.X, pos.Y].BackColor;
        }

        public bool IsPositionOffGrid(Vertex2D pos)
        {
            return pos.X < 0 || pos.X >= BoardCells.GetLength(0) || pos.Y < 0 || pos.Y >= BoardCells.GetLength(1);
        }

        public void SetCellColorAtPosition(Vertex2D pos, Color color)
        {
            BoardCells[(int)pos.X, (int)pos.Y].BackColor = color;
        }

        private void CreateAndAddPanelToBoard(Point position)
        {
            var newPanel = new Panel
            {
                Size = new Size(tileSizeX, tileSizeY),
                Location = new Point(tileSizeY * position.X, tileSizeX * position.Y),
                BackColor = defaultPanelBackgroundColor,
                BorderStyle = BorderStyle.FixedSingle,
                Name = position.ToString(),
            };
            newPanel.Click += OnPanelClick;
            PanelToBoard(position, newPanel);
        }

        private void CreatePanelGrid()
        {
            for (var y = 0; y < Context.GridSize; y++)
                for (var x = 0; x < Context.GridSize; x++)
                    CreateAndAddPanelToBoard(new Point(x, y));
        }

        private Vertex2D GetCoordinatesOfCell(Panel value)
        {
            for (var x = 0; x < Context.GridSize; x++)
            {
                for (var y = 0; y < Context.GridSize; y++)
                {
                    if (BoardCells[x, y].Equals(value))
                        return new Vertex2D(x, y);
                }
            }
            return new Vertex2D(-1, -1);
        }

        private void InitializeGrid()
        {
            BoardCells = new Panel[Context.GridSize, Context.GridSize];
            CreatePanelGrid();
            Width = Context.Width;
            Height = Context.Height;
        }

        private void OnPanelClick(object sender, EventArgs e)
        {
            var panelPosition = GetCoordinatesOfCell(sender as Panel);
            Context.CellClickEventHandler?.Invoke(e as MouseEventArgs, panelPosition);
        }

        private void PanelToBoard(Point position, Panel newPanel)
        {
            Controls.Add(newPanel);
            BoardCells[position.X, position.Y] = newPanel;
        }

        public IWeightedGraph GetWeightedGraph()
        {
            var graph = new AdjacencyMatrix();
            for (var x = 0; x < Context.GridSize; x++)
            {
                for (var y = 0; y < Context.GridSize; y++)
                {
                    if (Context.IsWall(BoardCells[x,y]))
                    {
                        continue;
                    }
                    var neighbours = GetLocationNeighbours(new Vertex2D(x, y));
                    foreach (var element in neighbours)
                    {
                        graph.Add(new Vertex2D(x, y), new Node(element, 1));
                    }
                }
            }
            return graph;
        }

        private List<Vertex2D> GetLocationNeighbours(Vertex2D location)
        {
            var neighbours = new List<Vertex2D>();
            List<Vertex2D> directions = new List<Vertex2D>()
            {
                new Vertex2D(0, -1),
                new Vertex2D(0, 1),
                new Vertex2D(-1, 0),
                new Vertex2D(1, 0),
            };

            foreach (var dir in directions)
            {
                var newPosition = location.Sum(dir);
                if (IsPositionOnGrid(newPosition) && IsPositionNoWall(newPosition))
                {
                    neighbours.Add(newPosition);
                }
            }

            return neighbours;
        }

        private bool IsPositionNoWall(Vertex2D newPosition)
        {
            return !Context.IsWall(BoardCells[newPosition.X, newPosition.Y]);
        }

        public bool IsPositionOnGrid(Vertex2D newPosition)
        {
            return !IsPositionOffGrid(newPosition);
        }
    }
}
