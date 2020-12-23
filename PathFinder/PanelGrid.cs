using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PathFinderGUI
{
    public struct PanelGridContext
    {
        public Action<MouseEventArgs, Vector2> CellClickEventHandler;
        public int GridSize;
        public int Height;
        public int Width;
    }

    public class PanelGrid : Panel
    {
        private readonly PanelGridContext context;
        private readonly Color defaultPanelBackgroundColor = Color.White;
        private readonly int tileSizeX;
        private readonly int tileSizeY;

        public Panel[,] BoardCells { get; private set; }

        public PanelGrid(PanelGridContext ctx)
        {
            context = ctx;
            tileSizeX = ctx.Width / ctx.GridSize;
            tileSizeY = ctx.Height / ctx.GridSize;
            InitializeGrid();
        }

        public Color GetCellColorByPosition(Vector2 pos)
        {
            return BoardCells[(int)pos.X, (int)pos.Y].BackColor;
        }

        public bool IsPositionOffGrid(Vector2 pos)
        {
            return pos.X < 0 || pos.X >= BoardCells.GetLength(0) || pos.Y < 0 || pos.Y >= BoardCells.GetLength(1);
        }

        public void SetCellColorAtPosition(Vector2 pos, Color color)
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
            for (var y = 0; y < context.GridSize; y++)
                for (var x = 0; x < context.GridSize; x++)
                    CreateAndAddPanelToBoard(new Point(x, y));
        }

        private Vector2 GetCoordinatesOfCell(Panel value)
        {
            for (var x = 0; x < context.GridSize; x++)
            {
                for (var y = 0; y < context.GridSize; y++)
                {
                    if (BoardCells[x, y].Equals(value))
                        return new Vector2 { X = x, Y = y };
                }
            }
            return new Vector2 { X = -1, Y = -1 };
        }

        private void InitializeGrid()
        {
            BoardCells = new Panel[context.GridSize, context.GridSize];
            CreatePanelGrid();
            Width = context.Width;
            Height = context.Height;
        }

        private void OnPanelClick(object sender, EventArgs e)
        {
            var panelPosition = GetCoordinatesOfCell(sender as Panel);
            context.CellClickEventHandler?.Invoke(e as MouseEventArgs, panelPosition);
        }

        private void PanelToBoard(Point position, Panel newPanel)
        {
            Controls.Add(newPanel);
            BoardCells[position.X, position.Y] = newPanel;
        }
    }
}
