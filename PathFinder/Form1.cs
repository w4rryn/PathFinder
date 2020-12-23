using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PathFinderGUI
{
    public partial class Form1 : Form
    {
        private readonly PanelGrid gridMaze;
        private readonly Dictionary<CellStates, Color> stateModeColorMap = new Dictionary<CellStates, Color>
        {
            {CellStates.empty, Color.White },
            {CellStates.obstacle, Color.Red },
            {CellStates.start, Color.Green },
            {CellStates.target, Color.Blue },
        };

        private Vector2 startCell = new Vector2(-1, -1);
        private Vector2 targetCell = new Vector2(-1, -1);

        public Form1()
        {
            InitializeComponent();
            var ctx = new PanelGridContext
            {
                GridSize = 12,
                Height = board.Height,
                Width = board.Width,
                CellClickEventHandler = OnCellClickEvent
            };
            gridMaze = new PanelGrid(ctx);
            board.Controls.Add(gridMaze);
        }

        private enum CellStates
        {
            empty, start, target, obstacle
        }

        private CellStates GetSelectedCellStateMode()
        {
            if (radio_obstacle.Checked)
                return CellStates.obstacle;
            else if (radio_start.Checked)
                return CellStates.start;
            else if (radio_target.Checked)
                return CellStates.target;
            return CellStates.empty;
        }

        private bool IsCellStartCell(Vector2 cell)
        {
            return gridMaze.GetCellColorByPosition(cell) == stateModeColorMap[CellStates.start];
        }

        private bool IsCellTargetCell(Vector2 cell)
        {
            return gridMaze.GetCellColorByPosition(cell) == stateModeColorMap[CellStates.target];
        }

        private bool IsRightClick(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Right;
        }

        private void OnCellClickEvent(MouseEventArgs e, Vector2 position)
        {
            if (IsRightClick(e))
                ResetCell(position);
            else
                SetCellColorByMode(position);
        }

        private void ResetCell(Vector2 cell)
        {
            if (IsCellStartCell(cell))
                startCell = new Vector2(-1, -1);
            else if (IsCellTargetCell(cell))
                targetCell = new Vector2(-1, -1);
            ResetCellColorAtPosition(cell);
        }

        private void ResetCellColorAtPosition(Vector2 pos)
        {
            gridMaze.SetCellColorAtPosition(pos, stateModeColorMap[CellStates.empty]);
        }

        private void SetCellColorByMode(Vector2 pos)
        {
            CellStates cellMode = GetSelectedCellStateMode();
            UpdateStartOrTargetByMode(pos, cellMode);
            gridMaze.SetCellColorAtPosition(pos, stateModeColorMap[cellMode]);
        }

        private void UpdateStartOrTargetByMode(Vector2 pos, CellStates cellMode)
        {
            switch (cellMode)
            {
                case CellStates.start:
                    UpdateStartCellColor(pos);
                    break;
                case CellStates.target:
                    UpdateTargetCellColor(pos);
                    break;
            }
        }

        private void UpdateStartCellColor(Vector2 pos)
        {
            if (pos == targetCell)
                targetCell = new Vector2(-1, -1);
            if (!gridMaze.IsPositionOffGrid(startCell))
                ResetCellColorAtPosition(startCell);
            startCell = pos;
        }

        private void UpdateTargetCellColor(Vector2 pos)
        {
            if (pos == startCell)
                startCell = new Vector2(-1, -1);
            if (!gridMaze.IsPositionOffGrid(targetCell))
                ResetCellColorAtPosition(targetCell);
            targetCell = pos;
        }

        private void OnButtonStartSearchClick(object sender, EventArgs e)
        {
            int[,] grid = GetGridRepresentation();
        }

        private int[,] GetGridRepresentation()
        {
            var gridWidth = gridMaze.BoardCells.GetLength(0);
            var gridHeight = gridMaze.BoardCells.GetLength(1);
            int[,] grid = new int[gridWidth, gridHeight];
            for (int y = 0; y < gridHeight; y++)
                for (int x = 0; x < gridWidth; x++)
                    grid[x,y] = GetPathType(x, y);
            return grid;
        }

        private int GetPathType(int x, int y)
        {
            int cellType = 1;
            var cell = gridMaze.BoardCells[x, y];
            if (cell.BackColor == stateModeColorMap[CellStates.obstacle])
            {
                cellType = 0;
            }
            return cellType;
        }
    }
}